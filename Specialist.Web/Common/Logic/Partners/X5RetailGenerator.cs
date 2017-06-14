using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using MvcContrib;
using NLog;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Services.Interface;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Util;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Services.Common {
	public class X5RetailGenerator:XmlShopGenerator {
		 
		[Dependency]
    	public IPriceService PriceService { get; set; }

		[Dependency]
    	public IRepository2<CourseDirectionsA> CourseDirectionService { get; set; }

		[Dependency]
    	public IGroupService GroupService { get; set; }
		 
    	private UrlHelper _url;
        public XDocument Get(UrlHelper urlHelper) {
        	_url = urlHelper;
            var yaml =
             new XDocument(Programs());
            return yaml;
        }

        public string Csv(UrlHelper urlHelper) {
        	_url = urlHelper;
			var coursesData = GetCoursesData();
	        return CsvUtil.Render(coursesData);
        }

        public string Xlsx(UrlHelper urlHelper) {
        	_url = urlHelper;
			var coursesData = GetCoursesData().ToList();
			coursesData.Insert(0, _.List("Курс", "Цена", "Описание", "Ссылка", "Группы"));
	        return Convert.ToBase64String(ExcelUtl.CreateXls(coursesData.ToList()));
        }


//        public string Html(UrlHelper urlHelper) {
//        	_url = urlHelper;
//	        var htmlBody =
//		        "<!DOCTYPE html> <html> <head> <meta charset='UTF-8'> <title>Курсы</title> </head> <body> {0} </body> </html>";
//
//			var coursesData = GetCoursesData();
//	        var table = H.table[H.Head("Курс", "Описание", "Страница", "Группы", "Цена"),
//		        coursesData.Select(x => H.Row(x[0], x[1], H.Anchor(x[2]), x[3], x[4]))
//		        ].ToString();
//	        return htmlBody.FormatWith(table);
//        }


		public XElement Programs() {
			var coursesData = GetCoursesData();
			return X("courses", coursesData.Select(Course));
		}

		private IEnumerable<List<string>> GetCoursesData() {
			var courses = GetCourses().Where(x =>
				x.Prices.Any(y => y.PriceType_TC == PriceTypes.Corporate) && !x.Course.IsTrackBool);

			var courseDescs = GetCourseDescs();
			var courseShortDescs = GetCourseShortDescs();
			var coursesData = courses.Select(x => CourseData(x,
				StringUtils.RemoveTags(courseDescs.GetValueOrDefault(x.Course.Course_TC)),
				StringUtils.RemoveTags(courseShortDescs.GetValueOrDefault(x.Course.Course_TC))
				));
			return coursesData.Where(x => x != null);
		}

		List<string> CourseData(CommonCourseListItemVM item, string desc, string shortDesc) {

			try {
				var date = DateTime.Today.AddMonths(6);
				var groups = GroupService.GetGroupsForCourse(item.Course.Course_TC)
					.Where(x => x.DateBeg <= date)
					.Select(x => x.DateInterval).Distinct().ToList().JoinWith(", ");

				var c = item.Course;

				var url = CommonConst.SiteRoot +
					(c.IsTrackBool
						? _url.Action<TrackController>(x => x.Details(c.UrlName))
						: _url.Action<CourseController>(x => x.Details(c.UrlName)));
				return _.List(
					StringUtils.RemoveTags(c.Name),
					((int)item.GetPrice(PriceTypes.Corporate)).ToString(),
					shortDesc,
					url,
					groups);
			} catch (Exception e) {
				Logger.Exception(e, "course error " + item.Course.Course_ID + " " + item.Course.Course_TC);
				return null;
			}

		}

		private XElement Course(List<string> c) {
			return X("course",
				X("name", StringUtils.RemoveTags(c[0])),
				X("price", c[1]),
				X("shortDescription", c[2]),
				X("url", c[3]),
				X("groups", c[4])
				);
		}
	}
}