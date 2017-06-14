using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Services.Order;
using Specialist.Services.Passport;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Logic;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Pages;
using Specialist.Web.Root.Orders.Views;
using Specialist.Web.Util;
using Specialist.Web.ViewModel.Orders;
using Specialist.Services.Common.Extension;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using Specialist.Services.Utils;

namespace Specialist.Web.Controllers.Shop
{
    public class EditCartController:ViewController
    {
        [Dependency]
        public ICartService CartService { get; set; }

        [Dependency]
        public IEditCourseVMService EditCourseVMService { get; set; }

        [Dependency]
        public IRepository<MarketingAction> MarketingActionService { get; set; }

        [Dependency]
        public IEmployeeService EmployeeService { get; set; }

        [Dependency]
        public IEditExamVMService EditExamVMService { get; set; }

        [Dependency]
        public IDiscountService DiscountService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        [Dependency]
        public ExtrasService ExtrasService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public IRepository<OrderExtras> OrderExtrasService { get; set; }

        [Dependency]
        public IRepository<SpecPromoCode> PromoCodeService { get; set; }
        public ActionResult RedirectToDetails()
        {
            return RedirectToAction<CartController>(x => x.Details());
        }

        public ActionResult Edit(EditCartVM editCartVM)
        {
	        if (!editCartVM.EditTrackTC.IsEmpty()) {
		        editCartVM.EditTrackTC = editCartVM.EditTrackTC.ToUpper();
	        }
			if(OrderService.GetCurrentOrder() == null)
				return RedirectToDetails();
            return View(editCartVM);
        }

        public ActionResult ToggleExam(decimal examID)
        {
            CartService.ToggleExam(examID);
            return RedirectToDetails();
        }

        public ActionResult ToggleCourse(decimal orderDetailID)
        {
            CartService.ToggleCourse(orderDetailID);
            return RedirectToDetails();
        }

        public ActionResult ToggleTrack(string trackTC)
        {
            CartService.ToggleTrack(trackTC);
            return RedirectToDetails();
        }

        public ActionResult ToggleWebinar(decimal orderdetailId)
        {
            var order = OrderService.GetCurrentOrder();
        	var orderDetail = order.OrderDetails.FirstOrDefault(x => x.OrderDetailID == orderdetailId);
			if(orderDetail != null) {
				EditCourseVMService.Update(new EditCourseVM{ PriceTypeTC = orderDetail.IsWebinar ? PriceTypes.PrivatePersonWeekend : PriceTypes.Webinar, OrderDetail = 
					new OrderDetail{OrderDetailID =orderDetail.OrderDetailID ,
						Group_ID = orderDetail.Group_ID
						}});
			}
            return RedirectToDetails();
        }

        public ActionResult UpdateOrder(string customerType)
        {
	        try {
		        CartService.UpdateOrder(customerType);
	        } catch(Exception e) {
		        var order = OrderService.GetCurrentOrder();
		        var courses = order.OrderDetails.Select(x => x.Course_TC).JoinWith(", ");
		        CartService.Clear();
				Logger.Exception(e, "UpdateOrder " + courses);
	        }
            return RedirectToDetails();
        }

        public ActionResult EditCourse(decimal orderDetailID)
        {
            var model = EditCourseVMService.GetCourseForEdit(orderDetailID);
			if(model == null)
				return null;
            if (model.IsTrack)
            {
                model.IsTrackCourse = true;
                return PartialView(PartialViewNames.EditTrackCourse, model);
            }
            return PartialView(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditCourse(EditCourseVM model)
        {
            if (model.PriceTypeTC == null) model.PriceTypeTC = string.Empty;
            EditCourseVMService.Update(model);
            return RedirectToDetails();
        }

        public ActionResult EditExam(decimal examID)
        {
            return PartialView(EditExamVMService.Get(examID));
        }

        [HttpPost]
        public ActionResult EditExam(EditExamVM model)
        {
            EditExamVMService.Update(model);
            return RedirectToDetails();
        }

        public ActionResult EditTestCert(int userTestId)
        {
			 var model = OrderService.GetCurrentOrder().OrderDetails
                .FirstOrDefault(oe => oe.UserTestId == userTestId);
            return PartialView(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditTestCert(int orderDetailId, byte type, byte lang)
        {
			 var orderDetail = OrderService.GetCurrentOrder().OrderDetails
                .FirstOrDefault(oe => oe.OrderDetailID == orderDetailId);
			if(lang == TestCertLang.RusEng)
				orderDetail.Count = 2;
			else {
				orderDetail.Count = 1;
			}
			if(type == TestCertType.Papper)
				orderDetail.Price = ExtrasService.GetPrice(Extrases.TestCertificte);
			if(type == TestCertType.Image) {
				orderDetail.Price = ExtrasService.GetPrice(Extrases.TestCertificteImage);
				orderDetail.OrderExtras.Clear();
			}
        	orderDetail.Params.Lang = lang;
        	orderDetail.Params.Type = type;
			orderDetail.UpdateXmlParams();
			OrderService.SubmitChanges();
            return RedirectToDetails();
        }
        public ActionResult EditTrack(string trackTC) {
            var model = EditCourseVMService.GetCourseForEdit(trackTC);
			if(model == null)
				return null;
            return PartialView(PartialViewNames.EditTrack, model);
        }

        [HttpPost]
        public ActionResult SelectGroups(List<OrderDetail> orderDetails)
        {
            EditCourseVMService.UpdateTrack(orderDetails);
            return RedirectToDetails();
        }

		[AjaxOnly]
		public ActionResult UpdateFavoriteTrainer(string fullName) {

			var order = OrderService.GetCurrentOrder();
			var trainer = EmployeeService.GetAllTrainers().FirstOrDefault(x => x.FullName == fullName);
			if (!fullName.IsEmpty() && trainer == null) {
				return Json("error");
			}

			order.FavoriteTeacher1 = trainer.GetOrDefault(x => x.Employee_TC);
		    OrderService.SubmitChanges();

			return Json("ok");
		}


		[AjaxOnly]
		public ActionResult UpdatePromocode(string promocode) {
			if(promocode.IsEmpty())
				return null;

			var promocodeExists = PromoCodeService.GetAll(x =>
				x.ValidBeg <= DateTime.Today && DateTime.Today <= x.ValidEnd && x.PromoCode == promocode).Any();

			var order = OrderService.GetCurrentOrder();
			if(order == null || !promocodeExists)
				return null;

			order.PromoCode = promocode;
		    OrderService.SubmitChanges();
			CartService.UpdateDiscount(true);

			return Json("ok");
		}
		public ActionResult EditExtras(decimal orderDetailID) {

            var orderDetail = OrderService.GetCurrentOrder()
                .OrderDetails.FirstOrDefault(od => od.OrderDetailID == orderDetailID);
			if(orderDetail == null)
				return null;
			var extrases = ExtrasService.GetFor(orderDetail);
			var model = new ExtrasesVM {
				SelectedExtrases = orderDetail.OrderExtras.Select(x => x.Extras_ID).ToList(),
				OrderDetail = orderDetail,
				ExtrasPrices = extrases.ToDictionary(e => e.Extras_ID,
					e => PriceService.GetPrice(e, orderDetail.Course_TC)),
				Extrases = extrases
			};
			NoCache(Response);
			return BaseViewWithModel(new EditExtrasView(),model);
		}

		[HttpPost]
		public ActionResult EditExtras(ExtrasesVM model) {
			var orderDetailId = model.OrderDetail.OrderDetailID;
			var orderDetail = OrderService.GetCurrentOrder()
				.OrderDetails.First(od => od.OrderDetailID ==
					orderDetailId);
			var currentExtras = OrderExtrasService.GetAll()
				.Where(oe => oe.OrderDetailID == orderDetailId)
				.ToList();
			var remove = currentExtras.Where(tc =>
			!model.SelectedExtrases.Contains(tc.Extras_ID)
			&& !Extrases.IsTravel(tc.Extras_ID)).ToList();
			for (var index = 0; index < remove.Count; index++) {
				var category = remove[index];
				OrderExtrasService.Delete(category);
			}

			var add = model.SelectedExtrases.Where(id =>
				!currentExtras.Select(tc => tc.Extras_ID).Contains(id));
			foreach (var extrasId in add) {
				var courseTc = orderDetail.Course_TC;
				InsertExtras(courseTc, orderDetailId, extrasId);
			}
			OrderExtrasService.SubmitChanges();

			return RedirectBack();

		}


		public ActionResult EditSeatNumber(decimal orderDetailID) {

			var currentOrder = OrderService.GetCurrentOrder();
			if (currentOrder == null)
				return null;
            var orderDetail = currentOrder
                .OrderDetails.FirstOrDefault(od => od.OrderDetailID == orderDetailID);
			if(orderDetail == null || orderDetail.Group_ID == null 
				|| orderDetail.IsWebinar)
				return null;
			var groupId = orderDetail.Group_ID.Value;
			var roomTC = orderDetail.Group.ClassRoom_TC;
			var reserved = CartService.GetReservedSeatNumbers(_.List(groupId)).First().Value;
			var available = Enumerable.Range(1, orderDetail.Group.MaxNumOfStudents)
				.Where(x => !reserved.Contains((short)x)).Select(x => (short)x).ToList();
			var model = new EditSeatNumberVM {
				Available = available,
				Current = orderDetail.SeatNumber,
				ClassRoomTC = roomTC,
				OrderDetailId = orderDetailID
			};
			var response = Response;
			NoCache(response);
			return BaseViewWithModel(new EditSeatNumberView(), model);
		}

	    private static void NoCache(HttpResponseBase response) {
		    response.CacheControl = "no-cache";
		    response.AddHeader("Pragma", "no-cache");
		    response.Expires = -1;
	    }


	    [HttpPost]
		public ActionResult EditSeatNumber(EditSeatNumberVM model) {
			var orderDetailId = model.OrderDetailId;
			var orderDetail = OrderService.GetCurrentOrder()
				.OrderDetails.FirstOrDefault(od => od.OrderDetailID ==
					orderDetailId);
			if(orderDetail != null) {
				orderDetail.SeatNumber = model.Current;
				OrderService.SubmitChanges();
			}
			return RedirectBack();

		}


    	public void InsertExtras(string courseTc, decimal orderDetailId, decimal extrasId) {
    		OrderExtrasService.Insert(new OrderExtras {
    			Extras_ID = extrasId,
    			OrderDetailID = orderDetailId,
    			Price = PriceService.GetPrice(ExtrasService.GetByPK(extrasId),
    				courseTc)
    		});
    	}
    
    }
}