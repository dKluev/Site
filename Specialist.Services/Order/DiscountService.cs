using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.MarketingActions;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Passport;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Interface.Order;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Lms;
using Specialist.Entities.Utils;
using Specialist.Services.Passport;
using Specialist.Services.Utils;
using Specialist.Web.Common.Utils.Logic;

namespace Specialist.Services.Order {
	public class DiscountService : IDiscountService {
		//        [Dependency]
		//        public IOrderService OrderService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public IPriceService PriceService { get; set; }

		[Dependency]
		public IRepository<MarketingAction> MarketingActionService { get; set; }

		[Dependency]
		public IStudentService StudentService { get; set; }

		[Dependency]
		public ISiteObjectService SiteObjectService { get; set; }

		[Dependency]
		public IRepository<Competition> CompetitionService { get; set; }

		[Dependency]
		public IRepository2<SpecPromoCode> SpecPromoCodeService { get; set; }


		//		public static List<IPNetwork> BeelineNetwork  = new List<IPNetwork>();

		/*	static DiscountService() {
			BeelineNetwork = BeelineIpList.Networks.Select(IPNetwork.Parse).ToList();
		}

		public bool IsBeeline() {
			IPAddress ipAddress;
			var userHostAddress = HttpContext.Current.Request.UserHostAddress;
			if(IPAddress.TryParse(userHostAddress, out ipAddress)) {
				return BeelineNetwork.Any(n => IPNetwork.Contains(n, ipAddress));
			}
			return false;

		}*/

		public int? GetPersonCategory() {
			/*	if(IsBeeline())
				return PersonCategories.Beeline;*/
			return null;
		}

		public void AddGroupDiscount(List<Discount> discounts, OrderDetail orderDetail) {
			var isWebinar = PriceTypes.IsWebinar(orderDetail.PriceType_TC);
			var orderGroup = orderDetail.Group;
			var groupDiscount = (byte?) PriceService.GetGroupDiscount(
				orderGroup ?? new Group {Course_TC = orderDetail.Course_TC}, isWebinar);
			if (groupDiscount.HasValue) {
				discounts.Add(new Discount {
						PercentValue = groupDiscount,
						Discount_ID = Discounts.GroupId,
						MarketingAction = MarketingActionService.GetAll()
							.FirstOrDefault(x => x.SysName == MarketingActions.HotGroup),
					});
			}
			
		}


		public Discount GetSpecPromoCode(OrderDetail orderDetail) {
			var promoCode = orderDetail.Order.PromoCode;
			if (promoCode == null) {
				return null;
			}
			var specPromoCode = SpecPromoCodeService.FirstOrDefault(x =>
				x.PromoCode == promoCode 
				&& x.ValidBeg <= DateTime.Today && DateTime.Today <= x.ValidEnd
				&& x.SpecPromoCodeCoursesDirections.Any(d => 
				(d.PromoCodeType_TC == "ÊÓÐÑ" && d.CourseDirectionCourse_TC == orderDetail.Course_TC)
				|| (d.PromoCodeType_TC == "ÊÍÍÊ" && d.CourseDirectionCourse_TC == orderDetail.Course.CourseDirectionA_TC))
				&& (x.DiscountPerc > 0 || x.DiscountRub > 0));
			if (specPromoCode == null) {
				return null;
			}
			return new Discount {
				Discount_ID = Discounts.PromocodeId,
				PercentValue = specPromoCode.DiscountPerc,
				MoneyValue = (int?) specPromoCode.DiscountRub,
				IsSummable = specPromoCode.IsSumOtherDiscounts,
				MaxPercentValue = specPromoCode.MaxSizeDiscountPerc
			};
            
		}

		public List<Discount> GetDiscountsFor(OrderDetail orderDetail) {
			Student student = null;
			if (orderDetail.Order == null)
				return new List<Discount>();
			var userID = orderDetail.Order.UserID;
//			var competitionDiscount = GetCompetitionDiscount(orderDetail);
//			if (competitionDiscount != null)
//				return new List<Discount> {new Discount {PercentValue = competitionDiscount}};

			if (userID.HasValue) {
				student = orderDetail.Order.User.Student;
			}
			return GetDiscountsFor(orderDetail, student);
		}

//		private byte? GetCompetitionDiscount(OrderDetail orderDetail) {
//			var competition = CompetitionService.GetAll()
//				.FirstOrDefault(c => c.WinnerID == orderDetail.Order.UserID &&
//					c.Course_TC == orderDetail.Course_TC && c.Discount != null);
//			return competition == null ? null : competition.Discount;
//		}


		public Discount GetPromocodeDiscount(OrderDetail orderDetail) {
			try {
				var promoCode = orderDetail.Order.PromoCode;
				if (promoCode.IsEmpty() || orderDetail.Order.User.GetOrDefault(x => x.IsCompany)) return null;
				var discount = new SpecialistWebDataContext().uspCheckPromoCode(promoCode,
					orderDetail.Course_TC).FirstOrDefault();
				if (discount == null || discount.Column1 <= 0) return null;
				return new Discount {
					Discount_ID = Discounts.PromocodeId,
					PercentValue = (byte?) discount.Column1
				};
			} catch (Exception ex) {
				Logger.Exception(ex, "promocode discount");
				return null;
			}
		}

		public List<Discount> GetDiscountsFor(OrderDetail orderDetail, Student student) {
			if (orderDetail.Track_TC != null || orderDetail.IsTestCert || orderDetail.IsDopUsl)
				return new List<Discount>();
			if ((orderDetail.Group != null
				&& (orderDetail.Group.Complex_TC == Cities.Complexes.Partners))
					|| orderDetail.PriceType_TC == null
						|| orderDetail.PriceType_TC.EndsWith(PriceTypes.Individual)) {
				return new List<Discount>();
			}

			var discounts = new List<Discount>();
			var secondCourseDiscount = GetSecondCourseDiscount(orderDetail);
			if (secondCourseDiscount != null) {
				return _.List(secondCourseDiscount);
			}
			AddGroupDiscount(discounts, orderDetail);
			if (orderDetail.Order.OrderDetails.Any(x => x.SecondCourse_TC == orderDetail.Course_TC)) {
				return discounts;
			}
			var promocodeDiscount = GetSpecPromoCode(orderDetail);
			if (promocodeDiscount != null) {
				discounts.Add(promocodeDiscount);
			}

			var result = GetMaxAndSummableDiscounts(orderDetail, discounts);
			var isWebinar = PriceTypes.IsWebinar(orderDetail.PriceType_TC);
			bool isWeekend = false;
			var orderGroup = orderDetail.Group;
			if (orderGroup != null) {
				isWeekend = orderGroup.DaySequence_TC != null;
			}
			CheckRealSpecDiscount(orderDetail, result, isWeekend,
				isWebinar || PriceTypes.IsMain(orderDetail.PriceType_TC));
			return result;
			/*

			string authorizationTypeTC = null;
			if (orderDetail.Course != null)
				authorizationTypeTC = orderDetail.Course.AuthorizationType_TC;
			var promoCode = orderDetail.Order.PromoCode;

			var graduateCourseTCList = new List<string>();
			decimal? previousOrderSum = null;
			string clabCardColorTC = null;
			if (student != null) {
//				clabCardColorTC = student.Card.GetOrDefault(x => x.ClabCardColor_TC);
				graduateCourseTCList = CourseService
					.GetAllForStudent(student.Student_ID).Select(c => c.Course_TC).ToList();
				previousOrderSum = StudentService.GetPreviousOrdersSum(student.Student_ID);
			}
			var isWebinar = PriceTypes.IsWebinar(orderDetail.PriceType_TC);
			byte beforeStartInDays = 0;
			DateTime? groupDateBegin = null;
			DateTime? groupDateEnd = null;
			string dayShiftTC = null;
			bool isWeekend = false;
			string groupCityTC = null;
			string groupComplexTC = null;
			var orderGroup = orderDetail.Group;
			var personCategoryId = GetPersonCategory();
			var groupDiscount = (byte?) PriceService.GetGroupDiscount(
				orderGroup ?? new Group {Course_TC = orderDetail.Course_TC}, isWebinar);
			if (orderGroup != null) {
				dayShiftTC = orderGroup.DayShift_TC;
				isWeekend = orderGroup.DaySequence_TC != null;
				groupDateBegin = orderGroup.DateBeg;
				groupDateEnd = orderGroup.DateEnd;
				groupComplexTC = orderGroup.Complex_TC;
				if (orderGroup.BranchOffice != null)
					groupCityTC = orderGroup.BranchOffice.TrueCity_TC;

				if (groupDateBegin.HasValue) {
					var totalDays = (groupDateBegin.Value - DateTime.Today).TotalDays;
					if (totalDays > byte.MaxValue)
						beforeStartInDays = byte.MaxValue;
					else
						beforeStartInDays = (byte) totalDays;
				}
			}


			var customerType = PriceTypes.GetCustomerTypes()
				.FirstOrDefault(pt => orderDetail.PriceType_TC.Contains(pt));
			if (isWebinar)
				customerType = PriceTypes.Webinar;

			var discounts = SiteObjectService.GetWithoutRelation<Discount>();

			if (orderDetail.Course != null)
				discounts = discounts.Union(
					SiteObjectService.GetDoubleRelation<Discount>(orderDetail.Course));

			discounts = discounts.Where(d => d.IsActive && d.MarketingAction.IsActive);
			discounts = discounts.Where(d => d.MarketingAction.PromoCode == null
				|| d.MarketingAction.PromoCode == promoCode);


			var discountAnalizer = new DiscountAnalizer();
			discounts = discountAnalizer.GetDiscounts(
				discounts, student, beforeStartInDays, groupDateBegin, groupDateEnd,
				dayShiftTC, isWeekend, customerType, previousOrderSum, authorizationTypeTC,
				personCategoryId, graduateCourseTCList, groupComplexTC, groupCityTC,
				orderDetail.Course_TC, clabCardColorTC);


			var loadedDiscounts = discounts.ToList();
			AddGroupDiscount(loadedDiscounts, groupDiscount);
			RemoveNewVersionCourseDiscount(student, orderDetail.Course_TC,
				loadedDiscounts);
			var result =
				GetMaxAndSummableDiscounts(orderDetail, loadedDiscounts);
			CheckForCourseMaxDiscount(orderDetail, result);
			CheckRealSpecDiscount(orderDetail, result, isWeekend,
				isWebinar || PriceTypes.IsMain(orderDetail.PriceType_TC));
			SocialUrlDiscount(orderDetail, result);
			return result;
*/
		}

		private Discount GetSecondCourseDiscount(OrderDetail orderDetail) {
			if (!orderDetail.Order.IsOrganization && orderDetail.SecondCourse_TC != null
				&& !CourseService.NotSecondCourses().Contains(orderDetail.Course_TC)) {
				var authTC = orderDetail.Course.AuthorizationType_TC;
				var percentValue = AuthorizationTypes.GetSecondCourseDiscount(authTC);
				return new Discount {
					Discount_ID = Discounts.SecondCourseId,
					PercentValue = percentValue
				};
			}
			return null;
		}


		void SocialUrlDiscount(OrderDetail orderDetail, List<Discount> result) {
			if (!orderDetail.IsTrack && orderDetail.Course_TC == CourseTC.BuhSem 
				&& !orderDetail.SocialUrl.IsEmpty()) {
				result.Clear();
					result.Add(new Discount {
						Discount_ID = Discounts.SocialUrlId,
						PercentValue = Discounts.SocialUrlDiscount
					});
			}
		}

		private static void CheckRealSpecDiscount(OrderDetail orderDetail, List<Discount> result, 
			bool isWeekend, bool isWebinarOrMain) {
			var user = orderDetail.Order.User;
			if (orderDetail.Group_ID > 0 &&
				isWebinarOrMain && 
				user != null && 
				!orderDetail.IsTrack &&
				!user.IsCompany && 
				!orderDetail.IsTestCert &&
				user.Student != null &&
				user.Student.Card != null) {
				var card = user.Student.Card.ClabCardColor_TC.ToUpper();
				var discount = Discounts.RealSpec[card];
				var course = orderDetail.Course;
				var maxAuthDiscount = Discounts.MaxAuth.GetValueOrDefault(course.AuthorizationType_TC);
				if (maxAuthDiscount > 0 && maxAuthDiscount < discount) {
					discount = maxAuthDiscount;
				}

				discount = GetRealSpecDiscountByCondition(isWeekend, discount, 
					Discounts.WeekendCards, card, discount);
				var isSen = course.Course_TC.StartsWith(CourseTC.Sen);
				discount = GetRealSpecDiscountByCondition(isSen, discount, 
					Discounts.SenCards, card, Discounts.SenDiscount);

				var currentDiscount = result.Sum(x => x.PercentValue);
				if (discount > currentDiscount) {
					result.Clear();
					result.Add(new Discount {
						Discount_ID = Discounts.RealSpecId,
						PercentValue = discount
					});
				}
			}
		}

		private static byte GetRealSpecDiscountByCondition(bool condition, byte discount, 
			List<string> cards, string card, byte newDiscount) {
			if (condition) {
				discount =
					cards.Contains(card)
						? newDiscount 
						: (byte) 0;
			}
			return discount;
		}


/*
		public bool IsFriendPromocode(string promocode) {
			return CouponUtils.IsFriendPromocode(promocode) && StudentsInGroupsCalcService.GetAll(x => 
				x.FriendPromocode == promocode || x.OwnPromocode == promocode).Any();
		}

		private void AddFriendDiscount(OrderDetail orderDetail, string promoCode, List<Discount> result) {
			var discountId = Discounts.FriendId;
			var isPromocode = IsFriendPromocode(promoCode);
			AddPromocodeDiscount(orderDetail, result, isPromocode, discountId, Discounts.FriendMoney,
				Discounts.RegMaxDiscount);
		}
*/

		private static void AddPromocodeDiscount(OrderDetail orderDetail, List<Discount> result, 
			bool isPromocode, int discountId, int discountMoney, int maxDiscount) {
			if (isPromocode) {
				if (!orderDetail.Order.OrderDetails.Any(x =>
					x.Notes.GetOrDefault(z => z.Contains(discountId.ToString())))
					&& orderDetail.Price >= Discounts.RegMinPrice &&
					result.Sum(x => x.PercentValue) +
					discountMoney*100/orderDetail.Price <= maxDiscount
					) {
					result.Add(new Discount {
						Discount_ID = discountId,
						MoneyValue = discountMoney
					});
				}
			}
		}

//		private void AddGroupDiscount(List<Discount> loadedDiscounts, byte? groupDiscount) {
//			if (groupDiscount.HasValue) {
//				loadedDiscounts.Add(
//					new Discount {
//						PercentValue = groupDiscount,
//						Discount_ID = Discounts.GroupId,
//						MarketingAction = MarketingActionService.GetAll()
//							.FirstOrDefault(x => x.SysName == MarketingActions.HotGroup),
//					});
//			}
//		}


		private void CheckForCourseMaxDiscount(OrderDetail orderDetail,
			IEnumerable<Discount> discounts) {
			/*if(!orderDetail.Course.MaxDiscount.HasValue)
				return;*/
			var courseMaxDiscount = orderDetail.Course.MaxDiscount.GetValueOrDefault();
			var currentMaxDiscount = discounts.FirstOrDefault(d => !d.IsSummable);
			if (currentMaxDiscount != null && currentMaxDiscount.PercentValue.HasValue
				&& currentMaxDiscount.PercentValue > courseMaxDiscount
					&& currentMaxDiscount.MarketingAction.SysName != MarketingActions.HotGroup)
				currentMaxDiscount.PercentValue = (byte) courseMaxDiscount;
		}

		private List<Discount> GetMaxAndSummableDiscounts(OrderDetail orderDetail,
			List<Discount> loadedDiscounts) {
			var birthdayDiscount = GetBirthdayDiscount(orderDetail.Order.User,
				loadedDiscounts);
			Discount maxDiscount = null;
			decimal previousPrice = decimal.MaxValue;
			var discountWithMaxPercent = new List<Discount>();
			var result = new List<Discount>();
			foreach (var discount in loadedDiscounts) {
				if (discount.IsSummable) {
					if (discount.MaxPercentValue.HasValue && discount.PercentValue.HasValue)
						discountWithMaxPercent.Add(discount);
					else
						result.Add(discount);
					continue;
				}
				var price = orderDetail.Price;
				if (discount.PercentValue.HasValue)
					price = price*(decimal) (1 - discount.PercentValue.Value/100.0);
				else if (discount.MoneyValue.HasValue)
					price = price - discount.MoneyValue.Value;

				if (price < previousPrice) {
					maxDiscount = discount;
					previousPrice = price;
				}
			}
			if (maxDiscount != null)
				result.Add(maxDiscount);
			var reserves = result.Where(d => d.MarketingAction.GetOrDefault(x => x.SysName) == MarketingActions.Reserve).ToList();
			if (reserves.Any()) {
				return reserves;
			}
			if (birthdayDiscount != null) {
				result.Add(birthdayDiscount);
			}

			ProcessMaxPercent(discountWithMaxPercent, result);


			return result;
		}

		public void ProcessMaxPercent(List<Discount> discountWithMaxPercent,
			List<Discount> result) {
			var currentPercent = (byte) result.Sum(d => d.PercentValue.GetValueOrDefault());

			discountWithMaxPercent.Sort((d1, d2) => d2.MaxPercentValue.Value -
				d1.PercentValue.Value);
			var maxPercentReach = false;
			foreach (var discount in discountWithMaxPercent) {
				if (currentPercent >= discount.MaxPercentValue.Value)
					break;
				if (discount.PercentValue.Value + currentPercent >
					discount.MaxPercentValue.Value) {
					discount.PercentValue = (byte?) (
						discount.MaxPercentValue - currentPercent);
					maxPercentReach = true;
				}

				currentPercent += discount.PercentValue.Value;
				result.Add(discount);
				if (maxPercentReach)
					break;
			}
		}


		private void RemoveNewVersionCourseDiscount(Student student, string courseTC,
			List<Discount> loadedDiscounts) {
			var newVersionCourseDiscount = loadedDiscounts
				.FirstOrDefault(d => d.MarketingAction != null
					&& d.MarketingAction.SysName == MarketingActions.NewVersionCourse);
			loadedDiscounts.Remove(newVersionCourseDiscount);
			if (student == null || newVersionCourseDiscount == null)
				return;

			if (CourseService.GetStudentCourseWithParent(
				courseTC, student.Student_ID).Count() > 0)
				loadedDiscounts.Add(newVersionCourseDiscount);
		}

		private Discount GetBirthdayDiscount(User user, List<Discount> loadedDiscounts) {
			var birthdayDiscount = loadedDiscounts
				.FirstOrDefault(d => d.MarketingAction != null && d.MarketingAction.SysName == MarketingActions.Birthday);
			loadedDiscounts.Remove(birthdayDiscount);
			var hasBirthdateDiscount = false;
			if (user != null && user.BirthDate.HasValue) {
				var birthdate = user.BirthDate.Value;
				var daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year,
					birthdate.Month);
				var day = birthdate.Day > daysInMonth ? daysInMonth : birthdate.Day;
				var birthDateInCurrentYear = new DateTime(
					DateTime.Today.Year, birthdate.Month, day);
				hasBirthdateDiscount =
					DateTime.Today <= birthDateInCurrentYear
						&& birthDateInCurrentYear < DateTime.Today.AddDays(14);
			}
			if (!hasBirthdateDiscount)
				birthdayDiscount = null;
			return birthdayDiscount;
		}
	}
}
