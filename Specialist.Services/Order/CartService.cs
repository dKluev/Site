using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Context.Logic;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Order;
using Microsoft.Practices.Unity;
using SimpleUtils.Extension;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Order.Extension;
using Specialist.Services.Common.Interface;
using Specialist.Services.Utils;
using SimpleUtils.Collections.Extensions;
using Specialist.Services.ViewModel;
using Specialist.Web.Const;

namespace Specialist.Services.Order
{
    public class CartService: ICartService 
    {
        [Dependency]
        public IOrderDetailService OrderDetailService { get; set; }

        [Dependency]
        public ExtrasService ExtrasService { get; set; }

        [Dependency]
        public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

        [Dependency]
        public IExamService ExamService { get; set; }

        [Dependency]
        public IRepository<OrderExam> OrderExamService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        [Dependency]
        public ShoppingCartVMService ShoppingCartVMService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public IRepository2<UserTest> UserTestService { get; set; }

        [Dependency]
        public ITrackService TrackService { get; set; }

        [Dependency]
        public ISpecialistExportService SpecialistExportService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public IDiscountService DiscountService { get; set; }

        [Dependency]
        public IAuthService AuthService { get; set; }

        [Dependency]
        public ISectionService SectionService { get; set; }

        [Dependency]
        public IEditCourseVMService EditCourseVMService { get; set; }

        [Dependency]
        public IRepository<OrderExtras> OrderExtrasService { get; set; }


        [Dependency]
        public IMailService MailService { get; set; }
        [Dependency]
        public IEmployeeService EmployeeService { get; set; }


	    public List<Employee> GetStudentTrainers(decimal? studentId) {
		    if (studentId.HasValue) {
			    var employees = EmployeeService.AllEmployees();
			    return StudentInGroupService.GetAll(x => x.Student_ID == studentId.Value)
					.Where(x => BerthTypes.AllPaidForCourses.Contains(x.BerthType_TC))
				    .Select(x => x.Group.Teacher_TC).Distinct().ToList().Select(x => employees.GetValueOrDefault(x))
				    .Where(x => x != null).OrderBy(x => x.LastName).ToList();
		    }
			return new List<Employee>();
	    }


    	public string GetOrderManagerTC(decimal orderId) {
			var orderDetails = OrderDetailService
				.GetAll(x => x.OrderID == orderId)
				.Select(x => new {x.UserTestId, x.Course_TC}).ToList()
				.Select(x => Tuple.Create(x.UserTestId, x.Course_TC)).ToList();
			if(!orderDetails.Any())
				return Employees.MainManager;
			if(orderDetails.All(x => x.Item1.HasValue))
				return Employees.TestCert;
			var employeeTC = Employees.MainManager;
    		if (OrderInSection(orderDetails, _.List(Sections.User, Sections.Smeta, Sections.SoftSkills))) {
    			employeeTC = Employees.GetLovkov();
    		}else if (OrderInSection(orderDetails, _.List(Sections.Hr, Sections.Kadr))) {
    			employeeTC = Employees.GetKarpovich();
    		}else if (OrderInSection(orderDetails, _.List(Sections.Logistic))) {
    			employeeTC = Employees.GetTaranenko();
    		}
			return employeeTC;
		}

    	private bool OrderInSection(List<Tuple<int?, string>> orderDetails, 
			List<int> sectionIds) {
    		var allSectionIds = SectionService.GetSectionsTree()
    			.Where(x => sectionIds.Contains(x.Section_ID))
				.SelectMany(s => s.SubSections.Where(x => x.IsActive))
				.Select(x => x.Section_ID).Union(sectionIds).ToList();
    		var courseTCs = CourseService.GetCourseTCListForSections(allSectionIds);
    		var contains = orderDetails.Any(x => courseTCs.Contains(x.Item2));
    		return contains;
    	}


    	public OrderDetail CreateDetail(string courseTC, string trackTC, string priceTypeTC)
        {
            var cityTC = UserSettingsService.CityTC;
    		var customerType = OrderCustomerType.PrivatePerson;
			if(AuthService.CurrentUser != null)
				customerType = OrderCustomerType.GetType(AuthService.CurrentUser.IsCompany);
            var prices = PriceService.GetAllPricesForCourseFilterByCustomerTye(courseTC,
				customerType, trackTC); 
            
            var price = prices.FirstOrDefault(p => p.PriceType_TC == priceTypeTC);
            if (price == null)
                price = prices.AsQueryable().GetDefault();
            if (price == null) {
                return null;
            }
            var orderDetail =
                new OrderDetail
                {
                    Course_TC = courseTC,
                    Track_TC = trackTC,
                    PriceType_TC = price.GetOrDefault(x => x.PriceType_TC),
                    Price = price.GetOrDefault(x => x.Price),
                    Duration = price.GetOrDefault(x => x.StudyMonths),
                    Count = 1,
                    City_TC = cityTC,
                };
			AddObligatoryExtras(orderDetail);
            return orderDetail;
        }

        public OrderDetail CreateTestCertDetail(int userTestId)
        {
            var orderDetail =
                new OrderDetail
                {
                    Course_TC = CourseTC.Srt,
                    Price = ExtrasService.GetPrice(Extrases.TestCertificte),
					UserTestId = userTestId,
					PriceType_TC = PriceTypes.PrivatePersonWeekend,
                    Count = 1,
                };
        	orderDetail.Params.Type = TestCertType.Papper;
        	orderDetail.Params.Lang = TestCertLang.Eng;
			orderDetail.UpdateXmlParams();
        	AddForeignDelivery(orderDetail);
        	return orderDetail;
        }

    	public bool AddForeignDelivery(OrderDetail orderDetail) {
    		var user = AuthService.CurrentUser;
    		if (orderDetail.IsTestCert && orderDetail.Params.IsPaper && user != null && user.GetAddress() != null &&
    			user.GetAddress().CountryID != Countries.Russian) {
				if(orderDetail.OrderExtras.All(x => x.Extras_ID != Extrases.ForeignDelivery)) {
	    			orderDetail.OrderExtras.Add(new OrderExtras {Extras_ID = Extrases.ForeignDelivery, 
						Price = ExtrasService.GetPrice(Extrases.ForeignDelivery)});
				}
    			return true;
    		}
    		return false;
    	}

		void AddObligatoryExtras(OrderDetail orderDetail) {
			var extras = ExtrasService.Travel().GetValueOrDefault(orderDetail.Course_TC);
			if (extras != null) {
				var extrasId = extras.Extras_ID;
				orderDetail.OrderExtras.Add(new OrderExtras {Extras_ID = extrasId, 
						Price = ExtrasService.GetPrice(extrasId)});
			}
		}




    	public void AddGroup(decimal groupID)
        {
            var group = GroupService.GetByPK(groupID);
            var order = OrderService.GetOrCreateCurrentOrder();
            if (order.OrderDetails.Any(od => od.Course_TC == group.Course_TC)) {
            	var currentOrderDetail = order.OrderDetails.FirstOrDefault(x =>
            		!x.IsTrack && x.Course_TC == group.Course_TC);
				if(currentOrderDetail != null && CommonConst.IsMobile)
					EditCourseVMService.Update(new EditCourseVM{PriceTypeTC = string.Empty,
						OrderDetail = 
						new OrderDetail{OrderDetailID = currentOrderDetail.OrderDetailID,
							Group_ID = groupID}});
            	
                return;
            }
            var priceTypeTC = OrderService
                .GetPriceTypeForGroup(group, false, 
				order.CustomerType ?? OrderCustomerType.PrivatePerson);
			if(group.IsWebinarOnly) {
				priceTypeTC = PriceTypes.GetWebinar(order.IsOrganization);
			}
            var orderDetail = CreateDetail(group.Course_TC, null, 
                priceTypeTC);
            if (orderDetail == null)
                return;
            orderDetail.Group_ID = groupID;
            order.OrderDetails.Add(orderDetail);
			CleanTestCertsDopUslExamsAndSubmit(order);
        }

        public void AddCourse(string courseTC, string priceTypeTC, 
			string socialUrl = null, string secondCourseTC = null)
        {
            var order = OrderService.GetOrCreateCurrentOrder();
	        if (order.OrderDetails.Any(od => od.Course_TC == courseTC)) {
		        if (!socialUrl.IsEmpty()) {
			        var socDetaisl = order.OrderDetails.FirstOrDefault(x => 
						x.Course_TC == courseTC && !x.IsTrack);
			        if (socDetaisl != null) {
						socDetaisl.SocialUrl = socialUrl;
			            OrderService.SubmitChanges();
			        }
		        }
                return;
	        }
            var orderDetail = CreateDetail(courseTC, null, priceTypeTC);
            if (orderDetail == null)
                return;
	        orderDetail.SocialUrl = socialUrl;
	        orderDetail.SecondCourse_TC = secondCourseTC;
            order.OrderDetails.Add(orderDetail);
			CleanTestCertsDopUslExamsAndSubmit(order);
        }
		
/*
		public void AddWebinarRecord(string courseTC) {
            var order = OrderService.GetOrCreateCurrentOrder();
			var details = order.OrderDetails.FirstOrDefault(x => x.Course_TC == courseTC);
            if(details == null)
            	return;
			InsertExtras(courseTC, details.OrderDetailID, Extrases.WebinarRecord);
			OrderExtrasService.SubmitChanges();
		}
*/


        public bool? AddTestCert(int userTestId)
        {
            var order = OrderService.GetOrCreateCurrentOrder();
	        if (order.HasCourses) {
		        return false;
	        }
            if (order.OrderDetails.Any(od => od.UserTestId == userTestId))
                return null;
        	UserTestPermission(userTestId);
        	var orderDetail = CreateTestCertDetail(userTestId);
            order.OrderDetails.Add(orderDetail);
            OrderService.SubmitChanges();
	        return true;
        }

    	private void UserTestPermission(int userTestId) {
    		var userId = UserTestService.GetValues(userTestId, x => x.UserId);
    		if (userId != AuthService.CurrentUser.UserID) {
    			throw new PermissionException();
    		}
    	}




    	public void AddTrack(string trackTC, string priceTypeTC)
        {
            var order = OrderService.GetOrCreateCurrentOrder();
            if (order.OrderDetails.Any(od => od.Track_TC == trackTC))
                return;
            if (priceTypeTC == null)
            {
                var price = PriceService.GetAllPricesForCourse(trackTC, null).AsQueryable()
                    .GetDefault();
                priceTypeTC = price.GetOrDefault(x => x.PriceType_TC);
            }
            var courses =
                CourseService.GetAllForTrack(trackTC);
            foreach (var course in courses) {
                var orderDetail = CreateDetail(course.Course_TC, trackTC, priceTypeTC);
                if (orderDetail == null)
                    return;
                order.OrderDetails.Add(orderDetail);
            }
    		CleanTestCertsDopUslExamsAndSubmit(order);
    	}

	    private void CleanTestCertsDopUslExamsAndSubmit(Entities.Context.Order order) {
		    foreach (var od in order.OrderDetails.Where(x => x.IsTestCert).ToList()) {
			    order.OrderDetails.Remove(od);
		    }
		    if (order.OrderDetails.Any(x => !x.IsDopUsl)) {
			    foreach (var od in order.OrderDetails.Where(x => x.IsDopUsl).ToList()) {
				    order.OrderDetails.Remove(od);
			    }
		    }
		    foreach (var od in order.OrderExams.ToList()) {
			    order.OrderExams.Remove(od);
		    }
		    OrderService.SubmitChanges();
	    }

	    public void UpdateOrder(string customerType)
        {
            var order = OrderService.GetCurrentOrder();
		    if (order == null) {
			    return ;
		    }
//            var newPriceTypePart = PriceTypes.GetByCustomerType(customerType);
   /*         string prevPriceTypePart;
            if (order.CustomerType == null)
                prevPriceTypePart = newPriceTypePart;*/
//            var prevPriceTypePart = PriceTypes.GetByCustomerType(
//                OrderCustomerType.GetOpposite(customerType));
            order.CustomerType = customerType;
            if (customerType == OrderCustomerType.PrivatePerson)
            {
          /*      order.NumberOfStudents = null;
                order.StudentFIOs = null;*/
            }

        	var forDelete = new List<decimal>();
            foreach (var orderDetail in order.OrderDetails.Where(x => !x.IsTestCert))
            {
                if(orderDetail.Group_ID.HasValue)
                {
                    orderDetail.PriceType_TC = OrderService.GetPriceTypeForGroup(
                        orderDetail.Group,
                        PriceTypes.IsBusiness(orderDetail.PriceType_TC),
                        order.CustomerType);

                } else {
                    orderDetail.PriceType_TC = null;
                }

                var prices = PriceService
                    .GetAllPricesForCourseFilterByCustomerTye(orderDetail.Course_TC,
                    order.CustomerType, orderDetail.Track_TC).AsQueryable();

                PriceView price = null;
                if(orderDetail.PriceType_TC != null)
                {
                    price = prices
                        .FirstOrDefault(p => p.PriceType_TC == orderDetail.PriceType_TC);
                }

                if(price != null)
                    orderDetail.Price = price.Price;
                else
                {
                    var maxPrice = prices.GetDefault();
					if(maxPrice == null) {
						forDelete.Add(orderDetail.OrderDetailID);
					}else {
	                    orderDetail.Price = maxPrice.Price;
	                    orderDetail.PriceType_TC = maxPrice.PriceType_TC;
					}
                }
            }
        	foreach (var forDeleteId in forDelete) {
        		order.OrderDetails.Remove(order.OrderDetails.First(x => x.OrderDetailID == forDeleteId));
        	}
            OrderService.SubmitChanges();
			UpdateDiscount(true);

        }

        public void ToggleExam(decimal examID)
        {
            var order = OrderService.GetOrCreateCurrentOrder();
            var planOrder = OrderService.GetOrCreatePlanOrder();
            var orderExam = order.OrderExams.FirstOrDefault(oe => oe.Exam_ID == examID);
            if(orderExam != null)
                orderExam.OrderID = planOrder.OrderID;
            else
            {
                orderExam = planOrder.OrderExams.FirstOrDefault(oe => oe.Exam_ID == examID);
                orderExam.OrderID = order.OrderID;
            }

            OrderService.SubmitChanges();
        }

        public void ToggleCourse(decimal orderDetailID)
        {
            var order = OrderService.GetOrCreateCurrentOrder();
            var planOrder = OrderService.GetOrCreatePlanOrder();
            var orderDetail = order.OrderDetails
                .FirstOrDefault(oe => oe.OrderDetailID == orderDetailID);
            if (orderDetail != null)
                orderDetail.OrderID = planOrder.OrderID;
            else
            {
                orderDetail = planOrder.OrderDetails
                    .FirstOrDefault(oe => oe.OrderDetailID == orderDetailID);
                orderDetail.OrderID = order.OrderID;
            }

            OrderService.SubmitChanges();
        }

        public void ToggleTrack(string trackTC)
        {
            var order = OrderService.GetOrCreateCurrentOrder();
            var planOrder = OrderService.GetOrCreatePlanOrder();
            var orderDetails = order.OrderDetails
                .Where(oe => oe.Track_TC == trackTC);
            if (orderDetails.Any())
                foreach (var orderDetail in orderDetails)
                    orderDetail.OrderID = planOrder.OrderID;
            else
            {
                orderDetails = planOrder.OrderDetails.Where(oe => oe.Track_TC == trackTC);
                foreach (var orderDetail in orderDetails)
                    orderDetail.OrderID = order.OrderID;
            }

            OrderService.SubmitChanges();
        }


       


        public void DeleteCourse(decimal orderDetailID)
        {
            var order = OrderService.GetCurrentOrder();
			if(order == null || order.HasPayment)
				return;
            var orderDetail = order.OrderDetails
                .FirstOrDefault(od => od.OrderDetailID == orderDetailID);
			if(orderDetail == null)
				return;
	        var secondCourseOrder = order.OrderDetails
		        .FirstOrDefault(od => od.SecondCourse_TC == orderDetail.Course_TC);
	        if (secondCourseOrder != null) {
		        secondCourseOrder.SecondCourse_TC = null;
	        }
            order.OrderDetails.Remove(orderDetail);
            OrderService.SubmitChanges();
	        if (secondCourseOrder != null) {
		        UpdateDiscount(true);
	        }
        }

        public void DeleteTrack(string trackTC)
        {
            var order = OrderService.GetCurrentOrder();
			if(order == null || order.HasPayment)
				return;
            var orderDetailsForDelete = order.OrderDetails
                .Where(od => od.Track_TC == trackTC);
            OrderDetailService.DeleteAndSubmit(orderDetailsForDelete);

        }

        public void Clear()
        {
            var order = OrderService.GetCurrentOrder();
			if(order == null || order.HasPayment)
				return;
            OrderDetailService.DeleteAndSubmit(order.OrderDetails);
            OrderExamService.DeleteAndSubmit(order.OrderExams);
            OrderService.DeleteAndSubmit(order);
        }

        public int GetCoursesCount()
        {
            var order = OrderService.GetCurrentOrder();
            if (order == null)
                return 0;
            return order.OrderDetails.Count();
        }

        public void UpdateDiscount(bool force)
        {
            var order = OrderService.GetCurrentOrder();
            if (order == null)
                return;
            foreach (var orderDetail in order.OrderDetails)
            {
                if(!force && (orderDetail.PercentDiscount.HasValue ||
                    orderDetail.MoneyDiscount.HasValue))
                    continue;
				orderDetail.ClearDiscount();
                var discouns = DiscountService.GetDiscountsFor(orderDetail);
	            if (discouns.Any()) {
	            	orderDetail.Notes = discouns.Select(x => x.Discount_ID.ToString())
	            		.JoinWith(",");
	            }
                if(discouns.Any())
                {
                    orderDetail.PercentDiscount = (byte?) discouns.Sum(d => d.PercentValue);
                    orderDetail.MoneyDiscount = discouns.Sum(d => d.MoneyValue);
                }
                else
                {
                    orderDetail.PercentDiscount = 0;
                    orderDetail.MoneyDiscount = 0; 
                }
				
            }
            OrderService.SubmitChanges();
        }

        public CartVM GetCart(decimal? orderId = null, bool addTrackDiscounts = false)
        {
            Entities.Context.Order order = null;
            Entities.Context.Order planOrder;
        	if (orderId.HasValue) {
            	order = OrderService.GetByPK(orderId.Value);
            	planOrder = OrderService.GetCurrentOrder(true) ?? new Entities.Context.Order();
            }
            else {
            	order = OrderService.GetCurrentOrder() ?? new Entities.Context.Order();
            	planOrder = OrderService.GetCurrentOrder(true) ?? new Entities.Context.Order();
            }

	        var trainer = EmployeeService.AllEmployees().GetValueOrDefault(order.FavoriteTeacher1);

        	var cart = new CartVM(order)
            {
                InPlan = new OrderSeparator(planOrder),
                User = AuthService.CurrentUser,
				FavTrainer = trainer.GetOrDefault(x => x.FullName)
            };
            if(cart.OnlyIsCompaty && 
                order.CustomerType == OrderCustomerType.PrivatePerson)
            {
                UpdateOrder(OrderCustomerType.Organization);
            }

        	cart.CourseTCHasExtrases = order.OrderDetails.Where(od => od.Track_TC == null
				&& ExtrasService.GetFor(od).Any()).Select(od => od.Course_TC).ToList();

	        cart.ExtrasTexts = ExtrasService.Texts();
        	cart.IsNearestGroupOrderDetails = order.OrderDetails.Where(x =>
        		x.Group_ID.HasValue && GroupService.GetGroupsForCourse(x.Course_TC)
        			.FirstOrDefault().GetOrDefault(g => g.Group_ID == x.Group_ID))
        		.Select(x => x.OrderDetailID).ToList();

			if(addTrackDiscounts && order.CustomerType != OrderCustomerType.Organization
				&& !cart.OnlyIsCompaty) {
				var trackInCart = order.OrderDetails.Where(x => x.Track_TC != null).Select(x => x.Track_TC).ToList();
				var courseTCs = order.OrderDetails.Where(x => x.Track_TC.IsEmpty()).Select(x => x.Course_TC).ToList();
				if(courseTCs.Any()) {
					var trackCourses = CourseService.GetActiveTrackCourses().Select(x =>
						new {x.Key, CourseCount = x.Value.Intersect(courseTCs).Count()})
						.Where(x => x.CourseCount > 0).ToDictionary(x => x.Key, x => x.CourseCount);
					var trackTCs = trackCourses.Select(x => x.Key).Where(tc => !trackInCart.Contains(tc)).ToList();
					var tracks = CourseService.GetAll(x => trackTCs.Contains(x.Course_TC)).ToList();
					var trackDiscounts = TrackService.GetTrackDiscounts(tracks)
						.OrderByDescending(x => trackCourses[x.Track.Course_TC]).ThenBy(x => x.Price);
					cart.TrackDiscounts = trackDiscounts.ToList();
				}

				var allOrderCourseTCs = order.OrderDetails.Select(x => x.Course_TC).ToList();
				cart.WithWebinar =
					allOrderCourseTCs.Where(x => PriceService.GetAllPricesForCourse(x, null).Any(y => y.IsWebinar)).ToList();
			}

        	return cart;
        }

        public bool AddExam(decimal examID)
        {
            var order = OrderService.GetOrCreateCurrentOrder();
	        if (order.HasCourses) {
		        return false;
	        }
            if (order.OrderExams.Any(oe => oe.Exam_ID == examID))
                return true;

            var exam = ExamService.GetByPK(examID);
            if (!exam.ExamPrice.HasValue)
                return true;

            var orderExam = new OrderExam
            {
                Exam_ID = examID,
                Price = exam.ExamPrice.GetValueOrDefault()
            };

            order.OrderExams.Add(orderExam);
            OrderService.SubmitChanges();
	        return true;
        }


        public void DeleteExam(decimal examID)
        {
            var order = OrderService.GetOrCreateCurrentOrder();
			if(order == null || order.HasPayment)
				return;
            var orderExam = order.OrderExams.FirstOrDefault(oe => oe.Exam_ID == examID);
			if(orderExam == null)
				return;
            OrderExamService.DeleteAndSubmit(orderExam);
        }

		public string SetPaymentType(string paymentType, decimal orderID) {
			var order = OrderService.GetByPKAndUserID(orderID,
				AuthService.CurrentUser.UserID);
			if (order == null || order.PaymentType_TC == paymentType)
				return null;

			order.Manager_TC = GetOrderManagerTC(orderID);
			var sendToUser = order.PaymentType_TC == PaymentTypes.NoPayment;
			if (order.PaymentType_TC.IsEmpty()) {
				order.UpdateDate = DateTime.Now;
			}
			order.PaymentType_TC = paymentType;

			if (order.OurOrg_TC == null) {
				order.OurOrg_TC = PaymentDataCreator.GetOurOrg(order, CourseService.SeminarCourses());
			}

			OrderService.SubmitChanges();
			var eduDocTypeTC = ShoppingCartVMService.EduDocType;
			ShoppingCartVMService.EduDocType = null;
			Task.Factory.StartNew(() => SpecialistExportService.Export(order.OrderID, true, eduDocTypeTC))
				.ContinueWith(x => {
					if (x.Exception.InnerException.Message.StartsWith(
						CommonTexts.FullGroupError) 
						&& order.OrderDetails.Any(z => z.IsTestCert)) {
						MailService.TestCertFull();
					}
//					else {
//						Logger.Exception(x.Exception.InnerException, "specialist export error");
//					}
				}, TaskContinuationOptions.OnlyOnFaulted);

			MailService.OrderComplete(order,sendToUser);

			return order.OurOrg_TC;

		}


		public Dictionary<decimal, List<short>> GetReservedSeatNumbers(
			List<decimal> groupIds) {
			var reserved = StudentInGroupService.GetAll(x => groupIds.Contains(x.Group_ID)
				&& x.SeatNumber.HasValue).Select(x =>
					new {
						SeatNumber = x.SeatNumber.Value, x.Group_ID
					}).ToList()
					.GroupByToDictionary(x => x.Group_ID, x => x.SeatNumber);
			foreach (var groupId in groupIds) {
				if (!reserved.ContainsKey(groupId))
					reserved.Add(groupId, new List<short>());

			}
			return reserved;
		}

	}
}