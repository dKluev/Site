using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Order.Extension;
using Specialist.Services.UnityInterception;
using SimpleUtils.Extension;
using Specialist.Web.Common.Utils;

namespace Specialist.Services {
	public class PriceService : IPriceService {

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IRepository2<Course> CourseService { get; set; }

		public List<PriceView> GetAllPricesForCourse(string courseTC, string trackTC) {
			return (GetPricesForCourses().GetValueOrDefault(courseTC) 
				?? new List<PriceView>()).Where(x => x.Track_TC == trackTC).ToList();
		}

		public decimal? GetPriceByType(string courseTC, string priceTypeTC, string trackTC) {
			return GetAllPricesForCourse(courseTC,trackTC)
				.FirstOrDefault(x => x.PriceType_TC == priceTypeTC)
				.GetOrDefault(x => (decimal?) x.Price);
		}

		[Cached]
		public virtual Dictionary<string, List<PriceView>> GetPricesForCourses() {
			return
				GetAllCurrent().GroupBy(x => x.Course_TC)
					.ToDictionary(x => x.Key, x => x.ToList());
		}

		PriceView CreatePrice(List<PriceView> prices,  string courseTC, string fromType, string toType,
			Group group) {
			var price = prices.FirstOrDefault(x => x.PriceType_TC == fromType && x.Track_TC == null);
			if (price == null) {
				return null;
			}
			var newPrice = OrderDetail.FloorToFifty((price.Price*(100 - group.Discount.Value))/100);
			var priceType = new PriceType {
				PriceType_TC = toType,
				PriceListType_TC = PriceListTypes.Common
			};
			return new PriceView {
				Course_TC = courseTC,
				Price = newPrice,
				PriceType_TC = toType,
				PriceType = priceType 
			};
		}

		public List<PriceView> GetIntraExtraPrices(List<PriceView> current) {
			var byCourse = current.GroupByToDictionary(x => x.Course_TC, x => x);	
			var groups = GroupService.GetPlannedAndNotBegin().Where(x => x.IsIntraExtramural && x.Discount > 0)
				.ToList()
				.GroupBy(x => x.Course_TC).Select(x => x.First()).ToList();
			var torGroup = groups.FirstOrDefault(x => x.Course_TC == CourseTC.Tor16);
			var tor32Group = groups.FirstOrDefault(x => x.Course_TC == CourseTC.Tor32);
			if (torGroup != null && tor32Group == null) {
				groups.Add(new Group {
					Course_TC = CourseTC.Tor32,
					Discount = torGroup.Discount,
				});
			}
			return groups.SelectMany(x => {
				var prices = byCourse.GetValueOrDefault(x.Course_TC);
				if (prices == null) {
					return Enumerable.Empty<PriceView>();
				}
				return _.List(
					CreatePrice(prices, x.Course_TC, PriceTypes.Main, PriceTypes.IntraExtra, x),
					CreatePrice(prices, x.Course_TC, PriceTypes.Corporate, PriceTypes.IntraExtraOrg, x)
					);
			}).Where(x => x != null).ToList();
		}

		[Cached]
		public virtual Dictionary<string, int> CoursePriceIndex() {
			return GetAllCurrent().Where(x =>
				x.PriceType_TC == PriceTypes.PrivatePersonWeekend)
				.OrderBy(x => x.Price).Select((x, i) =>
					Tuple.Create(x.Course_TC, i))
				.DistinctToDictionary(x => x.Item1, x => x.Item2);
		}

		[Cached]
		public virtual HashSet<string> CourseWithWebinar() {
			return new HashSet<string>(GetAllCurrent()
				.Where(x => x.PriceType_TC == PriceTypes.Webinar).Select(x => x.Course_TC).Distinct());
		}
		[Cached]
		public virtual HashSet<string> CourseWithUnlimite() {
			var courseTCs = CourseService.GetAll(x => x.IsActive && x.IsUnlimitedTraining)
				.Select(x => x.Course_TC).ToList();
			return new HashSet<string>(courseTCs.Except(CourseWithUnlimitePrice().Keys));
		}
		[Cached]
		public virtual Dictionary<string, decimal> CourseWithUnlimitePrice() {
			return GetAllCurrent().Where(x => x.PriceType_TC == PriceTypes.Unlimited)
				.DistinctToDictionary(x => x.Course_TC, x => x.Price);
		}

		public decimal? GetUnlimitPrice(string courseTC) {
			if (CourseWithUnlimite().Contains(courseTC)) {
				return 0;
			}
			var price = CourseWithUnlimitePrice().GetValueOrDefault(courseTC);
			if (price > 0) {
				return price;
			}
			return null;
		}

		public List<PriceView> GetAllPricesForCourseFilterByCustomerTye(
			string courseTC, string customerType, string trackTC) {
			var priceTypePart = PriceTypes.GetByCustomerType(
				OrderCustomerType.GetOpposite(customerType));
			return GetAllPricesForCourse(courseTC,trackTC)
				.Where(pv => !pv.FixedPriceType.Contains(priceTypePart)).ToList();
		}

		[Cached]
		public virtual List<string> GetElearningCourses() {
			return
				GetAllCurrent().Where(p => PriceTypes.GetElearning().Contains(
					p.PriceType_TC)).Select(c => c.Course_TC).Distinct().ToList();
		}


		public List<PriceView> GetTrackCoursesPrices(string trackTC, string priceTypeTC) {
			return GetAllCurrent().Where(p => p.CommonPriceTypeTC == priceTypeTC
				&& p.Track_TC == trackTC).ToList();
		}

		public virtual List<PriceView> GetCurrent() {
				using (var context = new SpecialistDataContext()) {
					var loadOptions = new DataLoadOptions();
					//   context.Log = new StringWriter();
					loadOptions.LoadWith<PriceView>(pw => pw.PriceType);
					context.LoadOptions = loadOptions;
					var query = context.PriceViews.Where(x => PriceTypes.Current.Contains(x.PriceType_TC));
			#if(DEBUG)
					return query.Where(p =>
						CourseTC.TestTC.Contains(p.Course_TC)
							|| CourseTC.TestTC.Contains(p.Track_TC)).ToList();
			#endif
					return query.ToList();
				}
		}

		[Cached]
		public virtual List<PriceView> GetAllCurrent() {
			var current = GetCurrent();
			var intraExtra = GetIntraExtraPrices(current);
			current.AddRange(intraExtra);
			return current;
		}

		public HashSet<string> DopUslCourses() {
			return MethodBase.GetCurrentMethod().CacheDay(() => 
			new HashSet<string>(GetAllCurrent().Where(x => x.PriceType_TC == PriceTypes.DopUsl)
				.Select(x => x.Course_TC)));
		}

		public Dictionary<string, short?> WebinarDiscouns() {
			return MethodBase.GetCurrentMethod().Cache(() => CourseService.GetAll(x => x.DiscountWebinar > 0)
				.ToDictionary(x => x.Course_TC, x => x.DiscountWebinar));
		}

		public short? GetGroupDiscount(Group group, bool isWebinar) {
			if (isWebinar) {
				var webinarDiscount = WebinarDiscouns().GetValueOrDefault(group.Course_TC);
				return webinarDiscount > 0 ? webinarDiscount : group.Discount;

			}
			return group.Discount;
		}

		public decimal GetPrice(Extras extras, string courseTC) {
			decimal result = 0;
			switch (extras.ExtrasPriceType_TC) {
				case ExtrasPriceTypes.PPWeekend:
					var price = GetAllPricesForCourse(courseTC, null)
						.FirstOrDefault(p => p.PriceType_TC ==
							PriceTypes.PrivatePersonWeekend);
					if (price == null)
						return 0;
					result = extras.CalkCoeffCert.GetValueOrDefault()*price.Price;
					break;
				case ExtrasPriceTypes.Fix:
					return extras.CurrentPrice.GetValueOrDefault();
			}
			/*if (0 != ((int)result % 500)) {
				result = (((int)result + 500)/1000)*1000;
			}*/

			result = (((int) (result*(decimal) 0.1))*10);
			return result;
		}
	}
}
