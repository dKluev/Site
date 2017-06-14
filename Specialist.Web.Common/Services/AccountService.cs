using System;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog;
using Specialist.Entities.Passport.ViewModel;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Order;
using Specialist.Services.Passport;
using Specialist.Services.ViewModel;

namespace Specialist.Web.Common.Services {
	public class AccountService {

        [Dependency]
        public ShoppingCartVMService ShoppingCartVMService { get; set; }

        [Dependency]
        public IAuthService AuthService { get; set; }
		 
        [Dependency]
        public IOrderService OrderService { get; set; }

	    public ActionResult Login(Controller controller, Func<string,object, ActionResult> view, LogOnVM model) {
			AuthService.CurrentUser = null;
            AuthService.SignIn(model.Email, model.Remeber);
			if(CommonConst.IsMobile && AuthService.CurrentUser.IsCompany) {
				AuthService.CurrentUser = null;
				AuthService.SignOut();
				controller.ModelState.AddModelError("", "Только для частных лиц");
	            return view("LogOn", model);

			}
            OrderService.UpdateSessionOrderUser();

			ShoppingCartVMService.Clear();
	        if (model.Email == "ptolochko@specialist.ru" && model.ReturnUrl.IsEmpty()) {
		        return new RedirectResult("/account/logon");
	        }

	        if (AuthService.CurrentUser.IsTrainerRole && model.ReturnUrl.IsEmpty()) {
		        return new RedirectResult("/lms");
	        }

            if (!String.IsNullOrEmpty(model.ReturnUrl))
            {
                return new RedirectResult(model.ReturnUrl);
            }
            return new RedirectResult("/");
	    }
	}
}