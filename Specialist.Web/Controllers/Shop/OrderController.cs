using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Order.ViewModel;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Exceptions;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc.ActionResults;
using Specialist.Web.Const;
using Specialist.Web.Controllers.Shop;
using Specialist.Web.Core;
using Specialist.Web.Core.Views;
using Specialist.Web.Pages;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Util;
using Specialist.Web.ViewModel.Orders;
using SpecialistTest.Common.Utils;
using Microsoft.Web.Mvc.Html;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Lms;
using Specialist.Entities.Order.Const;
using Specialist.Services.Common;
using Specialist.Services.Education;
using Specialist.Services.Profile;
using Specialist.Services.ViewModel;
using Specialist.Web.Common.Utils;
using Specialist.Web.Root.Orders.Services;

namespace Specialist.Web.Controllers {
	public class OrderController : ViewController{
		[Dependency]
		public ICartService CartService { get; set; }

		[Dependency]
		public IEmployeeService EmployeeService { get; set; }

		[Dependency]
		public IContractVMService ContractVMService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

		[Dependency]
		public ProfileService ProfileService { get; set; }

		[Dependency]
		public AlphaBankGenerator AlphaBankGenerator { get; set; }

		[Dependency]
		public IUserService UserService { get; set; }

		[Dependency]
		public IUserSettingsService UserSettingsService { get; set; }

		[Dependency]
		public IPriceService PriceService { get; set; }

		[Dependency]
		public IOrderService OrderService { get; set; }

		[Dependency]
		public IOrderDetailService OrderDetailsService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public IRepository<OurOrg> OurOrgService { get; set; }

		[Dependency]
		public IRepository2<Exam> ExamService { get; set; }

		[Dependency]
		public IRepository2<SimplePage> SimplePageService { get; set; }

		[Dependency]
		public IRepository<OurOrgBank> OurOrgBankService { get; set; }

		[Dependency]
		public StudentInGroupService StudentInGroupService { get; set; }

		[Dependency]
		public IRepository2<UserTest> UserTestService { get; set; }

		[Dependency]
		public ISectionVMService SectionVMService { get; set; }
		
		[Dependency]
		public IGroupService GroupService { get; set; }

		[Dependency]
        public IDictionariesService DictionariesService { get; set; }

		[Dependency]
		public ICityService CityService { get; set; }

		[Dependency]
		public SberbankService SberbankService { get; set; }

		[Dependency]
		public ShoppingCartVMService ShoppingCartVMService { get; set; }

		public ActionResult Register() {
			var order = OrderService.GetCurrentOrder();
			if (order == null)
				return RedirectToCart();
			if (User != null) {
				if (order.OrderExams.Any()) {
					var nextUrl = Url.Action<OrderController>(c2 => c2.Contract());
					return RedirectToAction<ProfileController>(c =>
						c.ExamQuestionnaire(nextUrl));
				}
				if (order.OrderDetails.Any(x => x.IsTestCert)) {
					return RedirectToAction(() => TestCertInfo());
				}
				return RedirectToAction(() => Contract());
			}

			return BaseView(Views.Order.Register2, new RegisterVM {
				Order = order
			});
		}


		public ActionResult ExpressRegister() {
			return BaseViewWithModel(new ExpressRegisterView(), new ExpressRegisterVM());
		}

		public ActionResult ExpressRegisterPost(ExpressRegisterVM model) {
			var order = OrderService.GetCurrentOrder();
			if (order == null || order.IsEmpty) {
				ModelState.AddModelError("", "Заказ отсутствует");
				return ErrorJson();
			}
            if (FluentValidate(model)) {
	            var user = new User {
		            Email = model.Email,
		            FirstName = model.FirstName,
					SecondName = model.SecondName,
					LastName = model.LastName,
					Password = Membership.GeneratePassword(6, 0),
		            UserContacts = new EntitySet<UserContact> {new UserContact(ContactTypes.Phone, model.Phone)}
	            };
				UserService.CreateUser(user);
                AuthService.SignIn(user.Email, true);
	            //MailService.RegistrationComplete(User, null, false);
				CheckOrder(order);
				OrderService.SubmitChanges();
	            OrderService.UpdateSessionOrderUser();
				CartService.SetPaymentType(PaymentTypes.ExpressOrder, order.OrderID);
				return Json(new FormResponseVM{Redirect = 
					Url.Action<OrderController>(c => c.ExpressRegisterComplete(order.OrderID))});
            }
			return ErrorJson();
		}

		public ActionResult ExpressRegisterComplete(decimal orderId) {
			var order = OrderService.GetByPKAndUserID(orderId, User.UserID);
			if (order == null)
				return RedirectToCart();
			var model = new ExpressOrderCompleteVM {Order = order};
			return View(model);
		}

		public ActionResult SberMerchant(string commonOrderId) {
			var order = OrderService.GetByCommonId(commonOrderId);

			if (!order.IsSig) {
				CartService.SetPaymentType(PaymentTypes.SberMerchant, order.OrderID);
			}
			if (order.IsSigPaid)
				return Content("Статус заказа ОПЛ");
            string url = null;
			try{
				url = SberbankService.GetUrl(order);
			} catch(Exception ex) {
				Logger.Exception(ex, User);
			}
			if (url == null) {
				return BaseViewWithTitle("Данный способ оплаты недоступен", 
					new PagePart( H.Anchor(Request.UrlReferrer.AbsoluteUri.Remove(CommonConst.CurrentRoot), "Выберите другой способ оплаты").ToString()));
			}
			return this.Redirect(url);
		}

		[ModelStateToTempData]
		[Authorize]
		public ActionResult SberbankInfo(decimal orderID) {
			if(CommonConst.IsMobile) {
				var order = OrderService.GetByPKAndUserID(orderID,
					AuthService.CurrentUser.UserID);
				if(order != null) {
					CartService.SetPaymentType(PaymentTypes.SberBank, orderID);
				}
				return BaseView(new PagePart(MHtmls.LongList(
					MHtmls.Title("Сбербанк"), 
					"Информация о заказ отправлена вам на почту").ToString())); 

			}

			var user = UserService.GetByPK(User.UserID);
			var userAddress = user
				.UserAddresses.FirstOrDefault();
			if (userAddress == null)
				userAddress = new UserAddress();
			userAddress.ForSberbank = true;

			var model = new SberbankInfoVM {
				OrderID = orderID,
				UserAddress = userAddress,
				Manager = GetSiteManager(orderID)
			};
			model.Contacts = new ContactsVM();
			Services.Profile.ProfileService.InitPhones(user, model.Contacts);
			return View(model);
		}

		[ModelStateToTempData]
		[Authorize]
		public ActionResult TestCertInfo() {
			var user = UserService.GetByPK(User.UserID);
			if(user.EngFullName.IsEmpty())
				user.EngFullName = Linguistics.Translite(user.LastName + " " + user.FirstName, true);
			var userAddress = user
				.UserAddresses.FirstOrDefault();
			if (userAddress == null)
				userAddress = new UserAddress{CountryID = Countries.Russian};
			var currentOrder = OrderService.GetCurrentOrder();
			if (currentOrder == null)
				return RedirectToCart();
			var isEngCert = currentOrder.OrderDetails.Any(x =>
				_.List(TestCertLang.Eng, TestCertLang.RusEng).Contains(x.Params.Lang));
			var isPaper = currentOrder.OrderDetails.Any(x =>
				x.Params.Type == TestCertType.Papper);
			var model = new TestCertInfoVM {
				Countries = DictionariesService.GetCountries(),
				UserAddress = userAddress,
				User = user,
				IsEngCert = isEngCert,
				IsPaper = isPaper
			};
			if(!isEngCert && !isPaper)
				return RedirectToAction(() => Contract());
			return View(model);
		}

			[HttpPost]
		[ModelStateToTempData]
		public ActionResult TestCertInfo(TestCertInfoVM model) {
			if (FluentValidate(model)) {
				if(model.IsPaper)
				ProfileService.UpdateAddressAndPhones(model.UserAddress, null);
				if(model.IsEngCert) {
					var user = UserService.GetByPK(User.UserID);
					user.EngFullName = model.User.EngFullName;
					UserService.SubmitChanges();
				}
				return RedirectToAction(() => Contract());
			}
			return RedirectToAction(() => TestCertInfo());
		}
		private Employee GetSiteManager(decimal orderId) {
			var employeeTC = CartService.GetOrderManagerTC(orderId);
			return EmployeeService.GetByPK(employeeTC);
		}

		[Authorize]
		public ActionResult CashInfo(decimal orderId) {
			CartService.SetPaymentType(PaymentTypes.Cash, orderId);
			var cities =
				CityService.GetAll().Where(x => x.City_TC == Cities.Moscow)
				.OrderBy(c => c.SortOrder);
			ViewData["Manager"] = GetSiteManager(orderId);
			return MView(Views.Order.CashInfo, cities);
		}


		[HttpPost]
		[ModelStateToTempData]
		public ActionResult SberbankInfo(SberbankInfoVM model) {
			if (FluentValidate(model)) {
				ProfileService.UpdateAddressAndPhones(model.UserAddress, model.Contacts);
				var order = OrderService.GetByPKAndUserID(model.OrderID,
					AuthService.CurrentUser.UserID);
				CartService.SetPaymentType(PaymentTypes.SberBank, order.OrderID);
				return RedirectToAction(() => Receipt(order.OrderID));
			}
			return RedirectToAction(() => SberbankInfo(model.OrderID));
		}

		[Authorize]
		[ModelStateToTempData]
		public ActionResult Confirm(decimal orderID) {
			var order = OrderService.GetByPKAndUserID(orderID, User.UserID);
			if (order == null)
				return new HttpUnauthorizedResult();
			return View(new OrderConfirmVM {
				Cart = CartService.GetCart(order.OrderID)
			});
		}

		[HttpPost]
		[ModelStateToTempData]
		public ActionResult Confirm(OrderConfirmVM model) {
			var orderId = model.OrderID;
			if (FluentValidate(model)) {
				var order = OrderService.GetByPKAndUserID(orderId, User.UserID);
				//order.IsOrganization
				MailService.OrderInfoForManager(order, model.ConfirmInfo);
				ShowMessage("Подтверждение об оплате успешно отправлена менеджеру");
			}
			return RedirectToAction(() => Confirm(orderId));
		}


		[Authorize]
		public ActionResult Contract() {
			ShoppingCartVMService.Clear();
			var order = OrderService.GetCurrentOrder();
			if (order == null || order.IsEmpty)
				return RedirectToCart();
			order.Manager_TC = CartService.GetOrderManagerTC(order.OrderID);
			order.Started = true;
			order.UpdateDate = DateTime.Now;
			foreach (var orderDetail in order.OrderDetails) {
				if(CartService.AddForeignDelivery(orderDetail))
					break;
			}

			OrderService.SubmitChanges();

			var model = ContractVMService.GetContractVM();
			if (model.Cart.Order.IsOrganization == User.IsCompany) {
				return RedirectToAction(() => PaymentTypeChoice(order.OrderID));
			}
			CartService.Clear();
			return RedirectToCart();
		}

		public ActionResult ShowContract() {
			return NotFound();
		}
		 
		

		private RedirectToRouteResult RedirectToCart() {
			return RedirectToAction<CartController>(c => c.Details());
		}

		public ActionResult PaymentComplete() {
			return BaseViewWithTitle("Вы успешно оплатили заказ",
				new PagePart(H.Anchor("/", "Перейти на главную страницу сайта").ToString()));
		}


		public ActionResult Special(decimal? id) {
			try {
				Order order = null;
				if (id.HasValue) {
					order = StudentInGroupService.GetOrder(id.Value);
					if(order == null) {
						return Content("Заказ не найден");
					}
					if (order.IsSigPaid)
						return Content("Статус заказа ({0}) ОПЛ ".FormatWith(order.OurOrgOrDefault));
				}

				if(order != null){
					ViewData[Htmls.OfferUrlKey] = Offers()[order.OurOrgOrDefault];
				}
				return View(order);
				
			} catch(Exception ex) {
				Logger.Exception(ex, User);
				return BaseView(new PagePart("Оплата недоступна"));
			}
		}


		public ActionResult SpecialOrg(decimal id) {
			return Content(StudentInGroupService.GetOrder(id).OurOrg_TC);
		}
		public ActionResult OrderOrg(decimal id) {
			var order = OrderService.GetByPK(id);
			return Content(PaymentDataCreator.GetOurOrg(order,CourseService.SeminarCourses()));
		}

		[AjaxOnly]
		public ActionResult SpecialLink(decimal? id) {
			try {
				return View(StudentInGroupService.GetOrder(id.Value));
			} catch(Exception ex) {
				Logger.Exception(ex, User);
				return Content("Оплата недоступна");
			}
		}

		[HttpPost]
		public ActionResult UpdateReasonsForLearning(List<OrderDetail> model) {
			var order = OrderService.GetCurrentOrder();
			if (order == null)
				return RedirectToCart();
			if (model != null) {
				foreach (var orderDetail in model) {
					var od = order.OrderDetails
						.FirstOrDefault(x => x.OrderDetailID == orderDetail.OrderDetailID);
					if (od != null) {
						var reasonForLearning = 
							StringUtils.SafeSubstring(orderDetail.ReasonForLearning, 2000);
						od.ReasonForLearning = reasonForLearning;
						if(od.Track_TC != null) {
							foreach (var trackOd in order
								.OrderDetails.Where(x => x.Track_TC == od.Track_TC)) {
								trackOd.ReasonForLearning = reasonForLearning;

							}

						}
						
					}
				}
				OrderService.SubmitChanges();
			}

			return RedirectToAction(() => PaymentTypeChoice(order.OrderID));
		}

		private void UpdateSeatNumbers(Order order) {
			var orderDetails = order.OrderDetails.Where(x => x.SeatNumber.HasValue
				&& x.Group_ID.HasValue).ToList();
			if(!orderDetails.Any()) {
				return;
			}
			var groupIds = orderDetails.Select(x => x.Group_ID.Value).ToList();

			var reservedSeatNumbers = CartService.GetReservedSeatNumbers(groupIds);

			foreach (var detail in orderDetails) {
				var reserved = reservedSeatNumbers[detail.Group_ID.Value];
				if(reserved.Contains(detail.SeatNumber.Value)) {
					detail.SeatNumber = null;
				}
			}
			
		}
		private void UpdatePrices(Order order) {
			foreach (var detail in order.GetCourseOrderDetails()) {
				var price = PriceService.GetAllPricesForCourse(detail.Course_TC, detail.Track_TC)
					.FirstOrDefault(x => x.PriceType_TC == detail.PriceType_TC);
				if (price != null) {
					detail.Price = price.Price;
				}
				else {
					order.OrderDetails.Remove(detail);
				}
			}
			foreach (var orderExam in order.OrderExams.ToList()) {
				var price = ExamService.GetValues(orderExam.Exam_ID, x => x.ExamPrice);
				if (price.GetValueOrDefault() > 0) {
					orderExam.Price = price.Value;
				}
				else {
					order.OrderExams.Remove(orderExam);
				}
			}
		}


		public ActionResult UpdateEducation(decimal orderId, string eduDocTypeTC) {
			var order = OrderService.GetByPKAndUserID(orderId, AuthService.CurrentUser.UserID);
			if (eduDocTypeTC.IsEmpty()) {
				SetComprehensiveReason(order);
			} else {
				SetQualProfReason(order);
			}
			ShoppingCartVMService.EduDocType = eduDocTypeTC.IsEmpty() ? null : eduDocTypeTC;
            OrderService.SubmitChanges();
			return RedirectToAction(() => PaymentTypeChoice(order.OrderID));
		}

		public bool UpdateLearningReason(Order order) {
			var orderDetails = order.GetCourseOrderDetails();
			if (!orderDetails.Any()) {
				return true;
			}
			if (orderDetails.Any(x => x.ReasonForLearning != null)) {
				return true;
			}
			if (order.HasSchool || order.HasEnglish) {
				SetComprehensiveReason(order);
				return true;
			}
			if (StudentInGroupService.HasEducationDocument(order.User.Student_ID)) {
				SetQualProfReason(order);
				return true;
			}
			return false;

		}

		private static void SetComprehensiveReason(Order order) {
			foreach (var orderDetail in order.GetCourseOrderDetails()) {
				orderDetail.ReasonForLearning = LearningReasons.Comprehensive;
			}
		}

		private static void SetQualProfReason(Order order) {
			foreach (var orderDetail in order.GetCourseOrderDetails()) {
				orderDetail.ReasonForLearning = orderDetail.Track.GetOrDefault(x => x.IsDiplom)
					? LearningReasons.Professional
					: LearningReasons.Qualification;
			}
		}


		[Authorize]
		public ActionResult PaymentTypeChoice(decimal orderID) {
			var order = OrderService.GetByPKAndUserID(orderID,
				AuthService.CurrentUser.UserID);
			if (order == null)
				return RedirectToCart();


			if (order.IsOrganization)
				return RedirectToAction(() => OrgComplete(order.OrderID));
			if (!UpdateLearningReason(order) && !order.OrderDetails.All(x => x.IsDopUsl)) {
				var view =
					H.Form(Url.Order().Urls.UpdateEducation(orderID, null))[
						H.select[EducDocTypes.All.Select(x => H.option[x.Item2].Value(x.Item1))]
						.Name("eduDocTypeTC"),
						H.Hidden("orderId", orderID),
						H.br,
					    H.button["ОК"].Style("margin-top:10px;").Class("ui-button") ] ;
				return BaseViewWithTitle("Выберите Ваш вид образования, по которому Вы сможете предоставить подтверждающие документы (например, диплом или свидетельство)", new PagePart(view.ToString()));
			}

			ShoppingCartVMService.Clear();
			CheckOrder(order);
			OrderService.SubmitChanges();

			if (order.IsEmpty) {
				return RedirectToCart();
			}



			var orgTC = CartService.SetPaymentType(PaymentTypes.NoPayment, order.OrderID);
			if (order.OurOrg_TC == null) {
				order.OurOrg_TC = orgTC;
			}

			var model =
				new PaymentTypeChoiceVM {
					Order = order,
					Cart = new CartVM(order)
				};

			ViewData[Htmls.OfferUrlKey] = Offers()[order.OurOrgOrDefault];
			model.Order.AdmitadId = UserSettingsService.AdmitadId;
			if (model.Order.GetAdmitad().Any()) {
				UserSettingsService.AdmitadId = null;
			}
			return MView(Views.Order.PaymentTypeChoice, model);
		}

		public Dictionary<string, string> Offers() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var pages = SimplePageService.GetAll(x => SimplePages.Offers.All.Contains(x.SysName))
					.ToList();
				return pages.ToDictionary(x => SimplePages.Offers.Orgs[x.SysName],
					x => x.UrlName);

			});
		} 

		private void CheckOrder(Order order) {
			if(order.HasPayment)
				return;
			UpdatePrices(order);
			UpdateSeatNumbers(order);
			foreach (var detail in order.OrderDetails.Where(x => x.Group_ID.HasValue)) {
				if (!(GroupService.GetPlannedAndNotBegin().Any(x => x.Group_ID == detail.Group_ID)
					|| GroupService.WebinarOnly().Any(x => x.Group_ID == detail.Group_ID))
					) {
					detail.Group_ID = null;
					detail.ClearDiscount();
				}
			}

		}

		[AjaxOnly]
		public ActionResult SetPaymentType(string paymentType, decimal orderID) {
			CartService.SetPaymentType(paymentType, orderID);
			return Json("done");
		}

		public ActionResult ReceiptPayment(decimal orderID) {
			var order = OrderService.GetByPKAndUserID(orderID,
				AuthService.CurrentUser.UserID);
			if (order == null)
				return RedirectToAction<HomeController>(c => c.Index());
			CartService.SetPaymentType(PaymentTypes.SberBank, orderID);
			return View(order);
		}

		public ActionResult TerminalPayment(decimal orderID) {
			var order = OrderService.GetByPKAndUserID(orderID,
				AuthService.CurrentUser.UserID);
			if (order == null)
				return RedirectToAction<HomeController>(c => c.Index());
			CartService.SetPaymentType(PaymentTypes.Terminal, orderID);
			return View(order);
		}

/*
		public ActionResult CyberPlat(decimal orderID) {
			var order = User == null
				? null
				: OrderService.GetByPKAndUserID(orderID, User.UserID);
			if (order == null) {
				order = StudentInGroupService.GetOrder(orderID);
				if (order.IsSigPaid)
					return Content("Статус заказа ОПЛ");
			}
			else {
				CartService.SetPaymentType(PaymentTypes.CyberPlat, order.OrderID);
			}
			if (order.TotalPriceWithDescount <= 0)
				return Content("Сумма к оплате 0 руб.");
			Response.ContentEncoding = Encoding.GetEncoding("windows-1251");
			return View(PaymentDataCreator.CyberPlat(order));
		}
*/


//		public ActionResult Qiwi(decimal orderID) {
//			var order = User == null
//				? null
//				: OrderService.GetByPKAndUserID(orderID, User.UserID);
//			if (order == null) {
//				order = StudentInGroupService.GetOrder(orderID);
//				if (order.IsSigPaid)
//					return Content("Статус заказа ОПЛ");
//			}
//			else {
//				CartService.SetPaymentType(PaymentTypes.Qiwi, order.OrderID);
//			}
//			if (order.TotalPriceWithDescount <= 0)
//				return Content("Сумма к оплате 0 руб.");
//			return View(PaymentDataCreator.Qiwi(order));
//		}

		[Authorize]
		public ActionResult Receipt(decimal orderID) {
			var order = OrderService.GetByPKAndUserID(orderID,
				AuthService.CurrentUser.UserID);
			if (order == null)
				return RedirectToAction<HomeController>(c => c.Index());
			var orgBank = GetOrgBank(order);
			var model = new ReceiptVM {
				Order = order,
				OurOrg = orgBank.Item1,
				OurOrgBank = orgBank.Item2,
			};
			return View(ViewNames.Receipt, model);
		}

		private Tuple<OurOrg, OurOrgBank> GetOrgBank(Order order) {
			var orgTC = order.OurOrgOrDefault;

			var ourOrgBank = OurOrgBankService.GetAll()
				.FirstOrDefault(o => o.IsUsedForSBReceipt && o.OurOrg_TC == orgTC);
			if (ourOrgBank == null) {
				Logger.Exception(new Exception("ourOrgBank == null"), User);
				ourOrgBank =
					OurOrgBankService.GetAll().OrderByDescending(x => x.Visible).ThenBy(x => x.BankSortOrder).First(
						o => o.OurOrg_TC == orgTC);
			}
			var ourOrg = OurOrgService.GetByPK(orgTC);
			var orgBank = Tuple.Create(ourOrg, ourOrgBank);
			return orgBank;
		}

		[Authorize]
		public ActionResult InvoicePayment(decimal orderID) {
			var order = OrderService.GetByPKAndUserID(orderID,
				AuthService.CurrentUser.UserID);
			if (order == null)
				return RedirectToCart();
			var model = new CartVM(order);
			var orgBank = GetOrgBank(order);
			model.OurOrg = orgBank.Item1;
			model.Bank = orgBank.Item2;
			var courseTCs = SectionVMService.CoursesForInvoice();
			model.NearestGroups = GroupService
				.GetGroupsForCourses(courseTCs, true, true)
				.Where(x => x.DateBeg > DateTime.Today.AddDays(3)).OrderBy(x => x.DateBeg).Take(5).ToList();
			model.BossFullName = GetFullName(model.OurOrg.Boss_TC);
			model.AccounterFullName = GetFullName(model.OurOrg.ChiefAccountant_TC);
			return View(model);
		}

		public string GetFullName(string empTC) {
			return EmployeeService.GetByPK(empTC).FullName;
		}

		[Authorize]
		public ActionResult OrgComplete(decimal orderID) {
			var order = OrderService.GetByPKAndUserID(orderID,
				AuthService.CurrentUser.UserID);
			if (order == null)
				return RedirectToCart();
			if(order.OrderDetails.Any(x => !x.IsOrg)) {
				OrderService.DeleteAndSubmit(order);
				return RedirectToCart();
			}
			order.OurOrg_TC = PaymentDataCreator.GetOurOrg(order, CourseService.SeminarCourses());
			if(order.PaymentType_TC == null)
				MailService.OrderInfoForManager(order, null, true);
			order.PaymentType_TC = PaymentTypes.Invoice;

			ShoppingCartVMService.Clear();
			CheckOrder(order);

			OrderService.SubmitChanges();


			var model = new OrgOrderCompleteVM {
				Order = order
			};
			return BaseView(Views.Order.OrgComplete, model);
		}

		public ActionResult UploadPassport(IEnumerable<HttpPostedFileBase> userfile) {
			var fileNameWithoutExt = UserImages.GetPassportFileSysWithoutExt(User.UserID);
			var result = SavePassport(userfile, fileNameWithoutExt);
			if(result == null)
				MailService.SendPassport();
			return Content(result ?? "ok");
		}


		private static string SavePassport(IEnumerable<HttpPostedFileBase> userfile, string filename) {
			var file = userfile.First();
			var extension = Path.GetExtension(file.FileName);
			filename +=  extension;
			if (!Urls.PassportExts.Any(x => x.EndsWith(extension)))
				return "Ext";
			if (file.ContentLength > UserImages.MaxPassportSize.Bytes) {
				return "Size";
			}
			file.SaveAs(filename);
			return null;
		}


		[Authorize]
		public ActionResult DownloadTestCertificate(decimal orderDetailId, bool second) {

			var orderDetail = OrderDetailsService.FirstOrDefault(x =>
				x.OrderDetailID == orderDetailId && BerthTypes.AllPaidForTestCerts.Contains(x.StudentInGroup.BerthType_TC));
			var userTestId = orderDetail.UserTestId.Value;
			UserTestService.LoadWith(x => x.Test, x => x.TestPassRule);
			var userTest = UserTestService.GetByPK(userTestId);
			TestCertificatePermission(orderDetail, userTest);
			var certificateFileSys = UserImages.GetTestCertFileSys(userTestId);
			try {
				System.IO.File.Delete(certificateFileSys);
			} catch (Exception e) {
				return Content("Сертификат уже скачивается");
			}
			var isEng = orderDetail.Params.Lang == TestCertLang.Eng;
			if (orderDetail.Params.Lang == TestCertLang.RusEng && second) {
				isEng = true;
			}
			using (var image = Image.FromFile(UserImages.GetTestCertFileSys(isEng ? 0 : 1))) {
				using (var result = ImageUtils.DrawTestString(image,
					isEng ? User.EngFullName : User.FullName,
					EntityUtils.GetTestCertName(isEng, userTest), userTest.RunDate.DefaultString(), userTest.Id))
					result.Save(certificateFileSys);
			}
			return File(certificateFileSys, "image/png", "certificate.png");
		}

		private void TestCertificatePermission(OrderDetail orderDetail, UserTest userTest) {
			if (userTest.UserId != User.UserID || orderDetail == null)
				throw new PermissionException();
		}

		[Authorize]
		public ActionResult TestCertificates() {
			var orderDetails = OrderDetailsService.GetAll(x =>
				x.Order.UserID == User.UserID && x.Course_TC == CourseTC.Srt && x.Order.PaymentType_TC != null)
				.OrderByDescending(x => x.CreateDate).ToList();
			return BaseViewWithModel(new TestCertificatesView(), new TestCertificatesVM(orderDetails));

		}



		[AjaxOnly]
		[HttpPost]
		[Authorize]
		public ActionResult IsGAExport(decimal orderId) {
			var order = OrderService.GetByPK(orderId);
			var isGaExport = order.IsGAExport;
			order.IsGAExport = true;
			OrderService.SubmitChanges();

			return Json(isGaExport);
		}
        [HandleNotFound]
		public ActionResult Credit(decimal id) {
	        string xml;
	        try {
		        xml = AlphaBankGenerator.FromCredit(id);
	        }
	        catch (AlphaBankGenerator.CreditDataException e) {
		        return BaseViewWithTitle("Кредит недоступен", new PagePart(e.Message + "<br/>" + Htmls.HtmlBlock(HtmlBlocks.NoCredit)));
	        }
			return BaseView(new PagePart(H.div[H.Form("https://anketa.alfabank.ru/alfaform-pos/endpoint")[
				H.textarea[xml].Name("InXML").Hide(),
				H.Submit("Перейти к оформлению кредита")
				].Id("form-credit").Enctype("application/x-www-form-urlencoded").Hide(),
				H.JQuery("$('#form-credit').submit()")
				].ToString()));
		}
	}
}
