using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using SimpleUtils.Extension;
using Specialist.Services.Education;

namespace Specialist.Services.Order
{
    public class OrderService: Repository<Entities.Context.Order>, IOrderService
    {
        public OrderService(IContextProvider contextProvider) : base(contextProvider)
        {
        }

        [Dependency]
        public IAuthService AuthService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

		[Dependency]
		public StudentInGroupService StudentInGroupService { get; set; }


        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public ICityService CityService { get; set; }

        [Dependency]
        public IRepository2<Employee> EmployeeService { get; set; }

        public Entities.Context.Order GetCurrentOrder(bool inPlan = false)
        {
            Entities.Context.Order result = null;
            var user = AuthService.CurrentUser;
            var sessionID = UserSettingsService.SessionID;
            if (user != null)
                result = GetAll()
                    .Where(o => o.UserID == user.UserID 
                        && o.PaymentType_TC == null
                        && o.InPlan == inPlan).OrderByDescending(o => o.CreateDate)
                    .FirstOrDefault();
            if(result == null && sessionID != Guid.Empty)
                result = GetBySessionID(sessionID, inPlan);
            return result;
        }


        public bool ExistGroup(User user)
        {
            if (user.IsStudent)
            {
                var last = StudentInGroupService.GetAll(x => x.Student_ID == user.Student_ID)
                    .OrderByDescending(x => x.LastChangeDate).Select(x =>
                        new { x.Router_TC, x.Consultant_TC, x.Org_ID }).FirstOrDefault();

                if (last == null) return false;

                return true;
            }
            else
            {
                return false;
            }
        }

        public Employee GetUserManagerTC(User user) {
		    var mainManager = Employees.MainManager;
		    var managerTC = Employees.MainManager;
		    var isOrg = false;
			if (user.IsStudent) {
				var last = StudentInGroupService.GetAll(x => x.Student_ID == user.Student_ID)
					.OrderByDescending(x => x.LastChangeDate).Select(x =>
						new {x.Router_TC, x.Consultant_TC, x.Org_ID}).FirstOrDefault();
				if (last == null) {
					managerTC = GetUserManagerFromOrders(user.UserID);
				}
				else {
					isOrg = last.Org_ID.HasValue;
				    mainManager = Employees.GetMainManager(isOrg);
					managerTC = isOrg ? last.Consultant_TC : last.Router_TC;
				}

			}
			else {
				managerTC = GetUserManagerFromOrders(user.UserID);
			}
		    managerTC = managerTC ?? mainManager;
		    var employee = EmployeeService.GetByPK(managerTC);
			if (employee.IsDismiss || !employee.SiteVisible) {
				employee = EmployeeService.GetByPK(mainManager);
			}
		    return employee;
	    }

		string GetUserManagerFromOrders(int userId) {
return this.GetAll(x => x.UserID == userId 
			    && x.Manager_TC != null)
			    .OrderByDescending(x => x.UpdateDate)
			    .Select(x => x.Manager_TC).FirstOrDefault()
			    ?? Employees.MainManager;
			
		}

        public IQueryable<Entities.Context.Order> GetAllNotPaidByUser()
        {
            var user = AuthService.CurrentUser;
            if (user == null)
                return new List<Entities.Context.Order>().AsQueryable();
            return GetAll().Where(o => o.UserID == user.UserID && o.PaymentType_TC != null) 
                .Where(o => o.OrderDetails.Any())
                .Where(o => o.OrderDetails.First().StudentInGroup == null ||
                o.OrderDetails.First().StudentInGroup.BerthType_TC == BerthTypes.NotPaid);
        }


        public Entities.Context.Order GetOrCreateCurrentOrder()
        {
            return GetOrCreateCurrentOrder(false);
        }

        public Entities.Context.Order GetOrCreatePlanOrder()
        {
            return GetOrCreateCurrentOrder(true);
        }


        public Entities.Context.Order GetOrCreateCurrentOrder(bool inPlan)
        {
            return GetCurrentOrder(inPlan) ?? CreateCurrentOrder(inPlan);
        }

	    public Entities.Context.Order CreateCurrentOrder(bool inPlan) {
		    var user = AuthService.CurrentUser;
		    string customerType = null;
		    if (user != null)
			    customerType = user.IsCompany
				    ? OrderCustomerType.Organization
				    : OrderCustomerType.PrivatePerson;
		    var order = new Entities.Context.Order {
			    SessionID = user == null ? UserSettingsService.SessionID : Guid.Empty,
			    UserID = user == null ? (int?) null : user.UserID,
			    InPlan = inPlan,
			    CustomerType = customerType,
		    };
		    this.InsertAndSubmit(order);
		    return order;
	    }

	    public void UpdateSessionOrderUser()
        {
            var order = GetBySessionID(UserSettingsService.SessionID, false);
            if (order == null) return;
			if(order.UserID == null) 
	            order.UserID = AuthService.CurrentUser.UserID;
            order.SessionID = Guid.Empty;
            SubmitChanges();
        }



        private Entities.Context.Order GetBySessionID(Guid sessionID, bool inPlan)
        {
            var order = GetAll()
                .FirstOrDefault(x => x.SessionID == sessionID 
                    && x.PaymentType_TC == null && x.InPlan == inPlan);
            return order;
        }


        public Entities.Context.Order GetByPKAndUserID(decimal orderID,
            int userID)
        {
            var order = GetAll()
                .FirstOrDefault(x => x.OrderID == orderID 
                     && x.UserID == userID);
            return order;
        }

	    public Entities.Context.Order GetByCommonId(string commonOrderId) {
			var id = Entities.Context.Order.GetIdFromCommon(commonOrderId);
			var order = id.Item1 
				? GetByPK(id.Item2) 
				: StudentInGroupService.GetOrder(id.Item2);
		    return order;
	    }

        public string GetPriceTypeForGroup(Group group, 
            bool isBusiness, string customerType)
        {
	        if (PriceService.DopUslCourses().Contains(group.Course_TC)) {
		        return PriceTypes.DopUsl;
	        }
            var cityPrefix = PriceTypes.GetPrefix(
                CityService.GetCityPrefix(group.BranchOffice.TrueCity_TC));

            return cityPrefix + PriceTypes.GetForGroup(group,isBusiness,customerType);
        }


      /*  public void SaveOrder(Entities.Order.Order Order)
        {
            var context = new PassportDataContext();
            var OrderContext = new OrderDataContext();
            var User = new User();
            var Company = new Company();
            int? CompanyID = null;

            if (Order.CustomerType == CustomerTypes.Organization)
            {
                Company = Order.Company;
                if (Company != null && Company.CompanyID == 0)
                {
                    context.Companies.InsertOnSubmit(Company);
                }
                else
                {
                    Company = context.Companies.Single(c => c.CompanyID == Order.Company.CompanyID);
                    Company.CompanyName = Order.Company.CompanyName;
                    Company.INN = Order.Company.INN;
                    Company.KPP = Order.Company.KPP;

                }
                Order.CompanyID = Order.Company.CompanyID;
                context.SubmitChanges();
                Order.CompanyID = Company.CompanyID;
                CompanyID = Company.CompanyID;
            }
            if (Order.User.UserID == 0)
            {
                User = Order.User;
                User.GroupID = 10;
                User.CompanyID = CompanyID;
                context.Users.InsertOnSubmit(User);
            }
            else
            {
                User = context.Users.Single(c => c.UserID == Order.User.UserID); 
                User.LastName = Order.User.LastName;
                User.FirstName = Order.User.FirstName;
                User.SecondName = Order.User.SecondName;
                User.Email = Order.User.Email;
                User.CompanyID = CompanyID;
            }
            context.SubmitChanges();
            Order.UserID = Order.User.UserID;
            OrderContext.Orders.InsertOnSubmit(Order);
            OrderContext.SubmitChanges();
            var SessionID = UserSettingsService.SessionID;
            var OrderDetails = OrderContext.OrderDetails.
                Where(x => x.Order.SessionID == SessionID).ToList();
            decimal Sum = 0;
            foreach (var detail in OrderDetails)
            {
                detail.OrderID = Order.OrderID;
                Sum = Sum + detail.Price;
            }
            Order.Sum = Sum;
            OrderContext.SubmitChanges();
            if (Order.CustomerType == CustomerTypes.Organization)
            {
                    SaveCompanyAddress(Company, 1, Order, context);
                    SaveCompanyAddress(Company, 4, Order, context);
                    SaveCompanyPhone(Company, 2, Order, context);
                    SaveCompanyPhone(Company, 3, Order, context);               
            }
            else
            {
                SaveUserAddress(User, 1, Order, context);
                SaveUserPhone(User, 2, Order, context);
            }
             SaveUserPhone(User, 5, Order, context);
        }


        public void SaveUserAddress(User User, int AddressType, Entities.Order.Order  Order, PassportDataContext context)
        {

            if (Order.User.UserAddresses.FirstOrDefault(c => c.ContactTypeID == AddressType) != null)
            {
                var Address = new UserAddress();
                var NewAddress = Order.User.UserAddresses.Single(c => c.ContactTypeID == AddressType);
                if (User.UserAddresses.FirstOrDefault(x => x.ContactTypeID == AddressType) != null)
                {
                    Address = User.UserAddresses.Single(x => x.ContactTypeID == AddressType);
                }

                Address.City = NewAddress.City;
                Address.Index = NewAddress.Index;
                Address.CountryID = NewAddress.CountryID;
                Address.State = NewAddress.State;
                Address.Address = NewAddress.Address;
                if (Address.AddressID == 0)
                {
                    Address.ContactTypeID = AddressType;
                    Address.UserID = User.UserID;
                    context.UserAddresses.InsertOnSubmit(Address);

                }
                context.SubmitChanges();
            }

        }*/

     /*   public void SaveCompanyAddress(Company Company, int AddressType, Entities.Order.Order Order, PassportDataContext context)
        {
            if (Order.Company.UserAddresses.FirstOrDefault(c => c.ContactTypeID == AddressType) != null)
            {
                var Address = new UserAddress();
                var NewAddress = Order.Company.UserAddresses.Single(c => c.ContactTypeID == AddressType);
                if (Company.UserAddresses.FirstOrDefault(x => x.ContactTypeID == AddressType) != null)
                {
                    Address = Company.UserAddresses.Single(x => x.ContactTypeID == AddressType);
                }
                Address.City = NewAddress.City;
                Address.Index = NewAddress.Index;
                Address.CountryID = NewAddress.CountryID;
                Address.State = NewAddress.State;
                Address.Address = NewAddress.Address;
                if (Address.AddressID == 0)
                {
                    Address.ContactTypeID = AddressType;
                    Address.CompanyID = Company.CompanyID;
                    context.UserAddresses.InsertOnSubmit(Address);

                }
                context.SubmitChanges();
            }
        }*/


     /*   private void SaveUserPhone(User User, int AddressType, Entities.Order.Order Order, PassportDataContext context)
        {
            if (Order.User.UserPhones.FirstOrDefault(c => c.ContactTypeID == AddressType) != null)
            {
                var Phone = new UserPhone();
                var NewPhone = Order.User.UserPhones.FirstOrDefault(c => c.ContactTypeID == AddressType).Phone;
                if (User.UserPhones.Count(x => x.ContactTypeID == AddressType) > 0)
                {
                    Phone = User.UserPhones.Single(x => x.ContactTypeID == AddressType);
                }
                Phone.Phone = NewPhone;
                if (Phone.PhoneID == 0)
                {
                    Phone.ContactTypeID = AddressType;
                    Phone.UserID = User.UserID;
                    context.UserPhones.InsertOnSubmit(Phone);
                }
                context.SubmitChanges();
            }
        }*/


     /*   private void SaveCompanyPhone(Company Company, int AddressType, Entities.Order.Order Order, PassportDataContext context)
        {
            if (Order.Company.UserPhones.FirstOrDefault(c => c.ContactTypeID == AddressType) != null)
            {
                var Phone = new UserPhone();
                var NewPhone = Order.Company.UserPhones.Single(c => c.ContactTypeID == AddressType).Phone;
                if (Company.UserPhones.FirstOrDefault(x => x.ContactTypeID == AddressType) != null)
                {
                    Phone = Company.UserPhones.Single(x => x.ContactTypeID == AddressType);
                }
                Phone.Phone = NewPhone;
                if (Phone.PhoneID == 0)
                {
                    Phone.ContactTypeID = AddressType;
                    Phone.CompanyID = Company.CompanyID;
                    context.UserPhones.InsertOnSubmit(Phone);
                }
                context.SubmitChanges();
            }
        }*/
    }
}