using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Services.Common.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Web.Common.Extension;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Views;
using Specialist.Web.Util;
using Specialist.Services.Core.Interface;
using Specialist.Services.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Pages;

namespace Specialist.Web.Controllers.Shop {
	public class CartController : ViewController {
		public const string IsStay = "isStay";

		[Dependency]
		public ICartService CartService { get; set; }

		[Dependency]
		public ShoppingCartVMService ShoppingCartVMService { get; set; }


		[Dependency]
		public ICourseService CourseService { get; set; }
		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public IOrderService OrderService { get; set; }


		[Dependency]
		public IGroupService GroupService { get; set; }

		public ActionResult RedirectToDetails() {
			ShoppingCartVMService.Clear();
			return RedirectToAction(() => Details());
		}

		public ActionResult BackOrDetails() {
			ShoppingCartVMService.Clear();
			if (!Request.Form.AllKeys.Contains(IsStay))
				return RedirectBack();
			if (Request.Form[IsStay] == true.ToString().ToLower())
				return RedirectBack();
			return RedirectToDetails();
		}

		public ActionResult Details() {
			CartService.UpdateDiscount(false);
			var model = CartService.GetCart(addTrackDiscounts: true);
			if(CommonConst.IsMobile && !model.Order.IsEmpty 
				&& model.Order.CustomerType == null) {
				CartService.UpdateOrder(OrderCustomerType.PrivatePerson);
				model.Order.CustomerType = OrderCustomerType.PrivatePerson;
			}
			if (!model.Order.IsEmpty && model.Order.CustomerType == null) {
				var name = new CustomerTypeChoiceVM {
					ActionUrl = Url.Action<EditCartController>(c => c.UpdateOrder(null)),
					CustomerType = OrderCustomerType.PrivatePerson
				};
				return BaseView(ViewNames.CustomerTypeChoice, name);
			}
			if (User != null && !User.IsCompany && model.IsEmpty) {
				var yesterday = DateTime.Today.AddDays(-1);
				model.LastCompleteOrder	= OrderService.GetAll(x => x.UserID == User.UserID
					&& x.PaymentType_TC != null && x.UpdateDate.Date >= yesterday &&
					x.OrderDetails.All(z => z.StudentInGroup.BerthType_TC != BerthTypes.Paid))
					.OrderByDescending(x => x.UpdateDate).FirstOrDefault();
			}
			return MView(Views.Cart.Details, model);
		}


		/*    [Authorize]
			public ActionResult AddUserInGroup(decimal groupID)
			{
				CartService.AddUserInGroup(groupID);
				return RedirectToAction<ProfileController>(c => c.Calendar());
			}*/

		[HttpPost]
		public ActionResult AddGroup(decimal groupID) {
			var gr = GroupService.GetByPK(groupID);
			if (gr != null) {
				if (!AddIfHalfTrack(gr.Course_TC)) {
					CartService.AddGroup(groupID);
				}
			}
			return BackOrDetails();
		}

/*
		[HttpPost]
		public ActionResult AddWebinarRecord(string courseTC) {
			CartService.AddWebinarRecord(courseTC);
			return BackOrDetails();
		}
*/


		private bool AddIfHalfTrack(string courseTC, string priceTypeTC = null) {
			if (!CourseTC.HalfTrackCourses.Contains(courseTC))
				return false;
			var trackTC = CourseTC.HalfTracks.First(x => x.Value.Contains(courseTC)).Key;
			CartService.AddTrack(trackTC, priceTypeTC);
			return true;
		}


		[HttpPost]
		public ActionResult AddCourse(string courseTC, string priceTypeTC) {
			courseTC = courseTC.ToUpper();
			priceTypeTC = CorrectPriceTypeByUser(priceTypeTC);
			if (CourseService.IsTrack(courseTC)) {
				CartService.AddTrack(courseTC, priceTypeTC);
			}
			else {
				if (!AddIfHalfTrack(courseTC, priceTypeTC)) {
					CartService.AddCourse(courseTC, priceTypeTC);
				}
			}
			return BackOrDetails();
		}


		[HttpPost]
		public ActionResult AddWithSecondCourse(string courseTC, string secondCourseTC) {
			if (User != null && User.IsCompany) {
				return BackOrDetails();
			}
			courseTC = courseTC.ToUpper();
			secondCourseTC = secondCourseTC.ToUpper();
			var secondCourses = CourseService.GetSecondCourses(courseTC);
			if (!secondCourses.Contains(secondCourseTC)) {
				return BackOrDetails();
			}
			CartService.AddCourse(courseTC, PriceTypes.Main);
			CartService.AddCourse(secondCourseTC, PriceTypes.Main, null, courseTC);
			return BackOrDetails();
		}

		public ActionResult AddCourseWithSocialLink() {
			var course = CourseService.GetByPK(CourseTC.BuhSem);
			return BaseViewWithModel(new CourseSocialLinkView(),
				new CourseSocialLinkVM { Course = course });
		}


		public ActionResult AddCourseWithSocialLinkPost(string socialurl) {
			CartService.AddCourse(CourseTC.BuhSem, null, socialurl);
			return RedirectToDetails();
		}

		[HttpPost]
		public ActionResult AddCourseListPost(List<string> courseTCs) {
			foreach (var courseTC in courseTCs) {
					CartService.AddCourse(courseTC, PriceTypes.Corporate);
			}
			return Json("ok");
		}

		[HttpPost]
		public ActionResult AddTestCert(int userTestId) {
			var result = CartService.AddTestCert(userTestId);
			if (result.HasValue && result == false) {
				return BaseViewWithTitle("Заказ сертификата тестирования",
					new PagePart("К сожалению, в данный момент невозможен одновременный заказ курсов" +
						" и сертификатов тестирования.<br/>Для заказа сертификата - " 
						+ Url.Cart().Details("завершите заказ курсов или очистите корзину.")));
			}
			return BackOrDetails();
		}

		private string CorrectPriceTypeByUser(string priceTypeTC) {
			if (priceTypeTC == null || User == null)
				return priceTypeTC;
			if (priceTypeTC == PriceTypes.Corporate && !User.IsCompany)
				priceTypeTC = PriceTypes.PrivatePersonWeekend;
			if (priceTypeTC == PriceTypes.PrivatePersonWeekend && User.IsCompany)
				priceTypeTC = PriceTypes.Corporate;
			return priceTypeTC;
		}

		/*  //private method for order any group.  edited Ponomarev.
		  private void AddCartGroup(decimal groupID)
		  {
        	
		  }*/

		/*private bool AddCartCourse(string courseTC, string priceTypeTC, bool isOrderedGroup) {
			bool isOrdered = false;

			if (courseTC.In(CourseTC.Tor, CourseTC.Sks, CourseTC.Torsh)) {
				CartService.AddTrack(courseTC, priceTypeTC);
				isOrdered = true;
			} else {
				if (courseTC.In("ТОР1", "СКС1", "ТОР2", "СКС2")) {
					var newTC = "Т-" + courseTC.Substring(0, courseTC.Length - 1);
					CartService.AddTrack(newTC, priceTypeTC);
					isOrdered = true;
				} else if (courseTC.In("ТОРШ1", "ТОРШ2")) {
					CartService.AddTrack(CourseTC.Torsh, priceTypeTC);
					isOrdered = true;
				} else {
					if (!isOrderedGroup) {
						CartService.AddCourse(courseTC, priceTypeTC);
						isOrdered = true;
					}
				}
			}

			return isOrdered;
		}*/

		/*  [HttpPost]
		  public ActionResult AddCourseById(int courseID)
		  {
			  var course = 
				  CourseService.GetAll().First(c => c.Course_ID == courseID);
			  var courseTC = course.Course_TC;
			  if (course.IsTrack.GetValueOrDefault())
			  {
				  CartService.AddTrack(courseTC, null);
			  }
			  else
			  {
				  AddCartCourse(courseTC, null, false);
			  }

			  return BackOrDetails();
		  }
  */


		[ValidateInput(false)]
		public ActionResult State() {
			var cartState = ShoppingCartVMService.GetCartState();
			return PartialView(Tuple.Create(cartState));
		}

		[ValidateInput(false)]
		public ActionResult StateNew() {
			var cartState = ShoppingCartVMService.GetCartState();
			return PartialView(Tuple.Create(cartState));
		}

		public ActionResult DeleteCourse(decimal orderDetailID) {
			CartService.DeleteCourse(orderDetailID);
			return RedirectToDetails();
		}

		public ActionResult DeleteTrack(string trackTC) {
			CartService.DeleteTrack(trackTC);
			return RedirectToDetails();
		}

		public ActionResult Clear() {
			CartService.Clear();
			return RedirectToDetails();
		}

		[HttpPost]
		public ActionResult AddExam(decimal examID) {
			if (CartService.AddExam(examID)) {
				return BackOrDetails();
			}
			return BaseViewWithTitle("Заказ экзамена",
				new PagePart("К сожалению, в данный момент невозможен одновременный заказ курсов" +
					" и экзаменов.<br/>Для заказа экзамена - " 
					+ Url.Cart().Details("завершите заказ курсов или очистите корзину.")));
		}

		/*      [HttpPost]
			  public ActionResult AddExamSet(decimal examID) {
				  CartService.AddExamSet(examID);
				  return RedirectBack();
			  }*/

		[Authorize]
		public ActionResult OrderUnlimit() {
			MailService.OrderUnlimit();
			var content = H.b["Информация отправлена менеджеру. <br/>В ближайшее время с вами свяжутся."].ToString();
			return Request.IsAjaxRequest() ? Content(content) : BaseViewWithTitle("Безлимитное обучение", 
				new PagePart(content));
		}

		public ActionResult DeleteExam(decimal examID) {
			CartService.DeleteExam(examID);
			return RedirectToDetails();
		}
	}
}
