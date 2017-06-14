using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.Common.Util;

namespace Specialist.Web.Cms.Logic {
	public class PaperCatalogData {

        [Dependency]
        public IRepository2<Course> CourseService { get; set; }

        [Dependency]
        public IRepository2<PriceView> PriceViewService { get; set; }

		[Dependency]
		public IRepository2<Group> GroupService {
			get;
			set;
		}

		public string GetCsv() {
			var allPrices = PriceViewService.GetAll(x => x.Track_TC == null).ToList()
				.GroupByToDictionary(x => x.Course_TC, x => x);
			var allGroups = GroupService.GetAll().PlannedAndNotBegin()
				.Where(x => x.Color_TC == Colors.Yellow && x.Course.IsActive)
				.OrderBy(x => x.DateBeg).ToList().GroupByToDictionary(x => x.Course_TC, x => x);
			var result = new List<List<string>>();
			CourseService.LoadWith(x => x.CourseContents, x => x.AuthorizationType);
			var courses = CourseService.GetAll().Where(x => x.IsActive
				&& !CourseTC.HalfTrackCourses.Contains(x.Course_TC) &&
				!x.IsTrack.Value).OrderBy(x => x.Course_ID).ToList();
			foreach (var course in courses) {
				var courseTC = course.Course_TC;
				var row = new List<string>();
				var prices = allPrices.GetValueOrDefault(courseTC) ?? new List<PriceView>();
				var isWebinar = GetPrice(PriceTypes.Webinar, prices) > 0;
				var groupList = allGroups.GetValueOrDefault(courseTC) ?? new List<Group>();
				row.Add(courseTC);
				row.Add(course.Name);
				row.Add(((int)course.BaseHours) + " ак.ч.");
				row.Add(course.AuthorizationType.GetOrDefault(x => x.AuthorizationName));
				row.Add(course.IsProjectAllowed ? "П" : "");
				row.Add(isWebinar ? "В" : "");
				var description = course.Description;
				row.Add(ClearnText(description));
				var prerequisete = course.CoursePrerequisites.Select(
					x => (x.Text + " " + x.RequiredCourse.GetOrDefault(z => z.Name)).Trim()).Where(x => x != null).JoinWith(";");
				row.Add(ClearnText(prerequisete));
				row.Add(ClearnText(course.OnComplete));
				var contents = course.CourseContents.OrderBy(x => x.ModuleNumber)
					.Select(x => "Модуль " + x.ModuleNumber + ". " + x.ModuleName)
					.JoinWith(";");
				row.Add(ClearnText(contents));
				Func<string, bool> addPrice =
					type => {
						var price = GetPrice(type, prices);
						if (type == PriceTypes.PrivatePersonWeekend) {
							var discount = NearestGroupSet.HasMorningDiscount(groupList);
							var morningPrice = discount.HasValue ? OrderDetail .FloorToFifty( 
								(price*(100 - discount.Value))/100) : price;
							row.Add(morningPrice > 0 ? morningPrice.ToString() : "");
						}

						row.Add(price > 0 ? ((int)price).ToString() : "");

						return price > 0;
					};
				var types = _.List(PriceTypes.PrivatePersonWeekend,
					PriceTypes.Corporate);
				var hasPrice = false;
				foreach (var prefix in _.List("")) {
					foreach (var type in types) {
						hasPrice |= addPrice(prefix + type);
					}
				}
				addPrice(PriceTypes.Webinar);



				if (hasPrice) {
					var groups = groupList
						.Select(DateInterval).Distinct().JoinWith(" ");
					row.Add(groups);
					row.Add(groupList.Any(x => x.IsOpenLearning) ? "О" : "");
					var certs = new CourseVM {
						Course = course
					}
					.Certificates.Select(x => x.Name)
					.JoinWith(";");
					row.Add(certs);
					result.Add(row);
				}


			}

			var csv = result.Select(x => x.Select(y => y
				.GetOrDefault(z => z.Replace('\t', ' ').Trim())));
			return CsvUtil.Render(csv);
		}

		public string DateInterval(Group g) {
			if (!g.DateBeg.HasValue || !g.DateEnd.HasValue)
				return null;
			var dateInterval = g.DateBeg.Value.ToString("dd.MM") + "–" + g.DateEnd.Value.ToString("dd.MM.yy");
			if (g.IsOpenLearning) {
				dateInterval = dateInterval + "*";
			}
			return dateInterval;
		}
		private static string ClearnText(string description) {
			if (description.IsEmpty())
				return description;
			return StringUtils.RemoveTags(description.Trim()).Replace("\r", " ")
				.Replace("\n", " ").Replace("  ", " ");
		}

		private static decimal GetPrice(string type, IEnumerable<PriceView> prices) {
			var price = prices.FirstOrDefault(x => x.PriceType_TC == type)
				.GetOrDefault(x => x.Price);
			return price;
		}


	}
}