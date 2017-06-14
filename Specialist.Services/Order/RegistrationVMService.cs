using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Passport;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;

namespace Specialist.Services.Order
{
    public class RegistrationVMService: IRegistrationVMService
    {
        [Dependency]
        public IAuthService AuthService { get; set; }

        [Dependency]
        public IDictionariesService DictionariesService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }


      /*  public RegistrationVM GetRegistration ()
        {
            var Model = new RegistrationVM{Order = OrderService.GetCurrentOrder()};
            if (HttpContext.Current.Request.IsAuthenticated)
            {

                if (Model.Order.CustomerType == CustomerTypes.Organization)
                {
                    if (Model.Order.CompanyID.HasValue)
                    {
                        if (FormsAuthenticationService.CurrentUser.Company.UserPhones.Count != 0)
                        {
                            Model.Phone = FormsAuthenticationService.CurrentUser.Company.UserPhones.
                                Where(x => x.ContactTypeID == 2).FirstOrDefault().Phone;
                            Model.Fax = FormsAuthenticationService.CurrentUser.Company.UserPhones.
                                Where(x => x.ContactTypeID == 3).FirstOrDefault().Phone;
                        }
                        if (FormsAuthenticationService.CurrentUser.Company.UserAddresses.Count != 0)
                        {
                            Model.Index =
                                FormsAuthenticationService.CurrentUser.Company.UserAddresses.FirstOrDefault().Index.
                                    ToString();
                            Model.CountryID =
                                FormsAuthenticationService.CurrentUser.Company.UserAddresses.FirstOrDefault().CountryID;
                            Model.State =
                                FormsAuthenticationService.CurrentUser.Company.UserAddresses.FirstOrDefault().State;
                            Model.City =
                                FormsAuthenticationService.CurrentUser.Company.UserAddresses.FirstOrDefault().City;
                            Model.UrAddress = FormsAuthenticationService.CurrentUser.Company.UserAddresses.
                                Where(x => x.ContactTypeID == 4).FirstOrDefault().Address;
                            Model.PhisicalAddress = FormsAuthenticationService.CurrentUser.Company.UserAddresses.
                                Where(x => x.ContactTypeID == 1).FirstOrDefault().Address;
                        }
                    }
                }
                else
                {
                    if (FormsAuthenticationService.CurrentUser.UserPhones.Count != 0)
                    {
                        Model.Phone = FormsAuthenticationService.CurrentUser.UserPhones.
                            Where(x => x.ContactTypeID == 2).FirstOrDefault().Phone;

                    }
                    if (FormsAuthenticationService.CurrentUser.UserAddresses.Count != 0)
                    {
                        Model.Index =
                            FormsAuthenticationService.CurrentUser.UserAddresses.FirstOrDefault().Index.ToString();
                        Model.CountryID =
                            FormsAuthenticationService.CurrentUser.UserAddresses.FirstOrDefault().CountryID;
                        Model.State = FormsAuthenticationService.CurrentUser.UserAddresses.FirstOrDefault().State;
                        Model.City = FormsAuthenticationService.CurrentUser.UserAddresses.FirstOrDefault().City;
                        Model.PhisicalAddress =
                            FormsAuthenticationService.CurrentUser.UserAddresses.Where(x => x.ContactTypeID == 1).
                                FirstOrDefault
                                ().Address;
                    }
                }
                if (FormsAuthenticationService.CurrentUser.UserPhones.Count != 0)
                {
                    Model.MobilePhone = FormsAuthenticationService.CurrentUser.UserPhones.
                        Where(x => x.ContactTypeID == 5).FirstOrDefault().Phone;
                }
            }

            DictionariesService.InitDictionary(Model);
            return Model;
        }*/

    /*    public void SaveRegistration (RegistrationVM model)
        {
            var UserAddress = new UserAddress();
            var UserPhone = new UserPhone();
            var MobilePhone = new UserPhone();
           
      //      model.Order.UserType = CartService.GetUserType();

            if (model.Index != "")
            {
                UserAddress.Index = Convert.ToInt32(model.Index);
            }
            UserAddress.City = model.City;
            UserAddress.State = model.State;
            UserAddress.CountryID = model.CountryID;
            UserAddress.Address = model.PhisicalAddress;
            UserAddress.ContactTypeID = 1;
            MobilePhone.Phone = model.MobilePhone;
            MobilePhone.ContactTypeID = 5;
            model.Order.User.UserPhones.Add(MobilePhone);
            UserPhone.Phone = model.Phone;
            UserPhone.ContactTypeID = 2;
            if (model.Order.CustomerType == CustomerTypes.Organization)
            {
                model.Order.Company.UserPhones.Add(UserPhone);
                model.Order.Company.UserAddresses.Add(UserAddress);
                if (model.UrAddress != "")
                {
                    var CompanyAddress = new UserAddress();
                    CompanyAddress.Address = model.UrAddress;
                    CompanyAddress.ContactTypeID = 4;
                    model.Order.Company.UserAddresses.Add(CompanyAddress);
                }
                if (model.Fax != null)
                {
                    var Fax = new UserPhone();
                    Fax.Phone = model.Fax;
                    Fax.ContactTypeID = 3;
                    model.Order.Company.UserPhones.Add(Fax);
                }
            }
            else
            {
                model.Order.User.UserPhones.Add(UserPhone);
                model.Order.User.UserAddresses.Add(UserAddress);
            }
            model.Order.Source_TC = model.Source;

//            OrderService.SaveOrder(model.Order);
        }*/

    }
}
