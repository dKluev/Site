using System;
using System.Data.Linq;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Order;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Pages;
using Specialist.Web.Root.Profile.Views;


namespace Specialist.Web.Controllers
{
    public class SimpleRegController: ViewController {

		[Dependency]
		public IUserService UserService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public IOrderService OrderService { get; set; }

		
        [Dependency]
        public IRepository2<SimpleRegUser> SimpleRegUserService { get; set; }

	   

        public ActionResult Registration(string returnUrl, string source_tc)
        {
            var user = new SimpleRegUser();
            user.Url = returnUrl;
            user.Source_TC = source_tc;
            return View(user);
            //return BaseViewWithModel(new SimpleRegistrationView(), user);
        }

        public ActionResult RegistrationWidget(string returnUrl, string source_tc)
        {
            var model = new SimpleRegUser();
            model.Url = returnUrl;
            model.Source_TC = source_tc;
            return PartialView("RegistrationWidget", model);
        }

        public ActionResult PartialRegistration()
        {
            ViewBag.Message = "Это частичное представление.";
            return PartialView();
        }

        [HttpPost]
        public ActionResult RegistrationPost(SimpleRegUser user) {
		    user.Email = user.Email.GetOrDefault(x => x.Trim());
		    if (FluentValidate(user)) {
				SimpleRegUserService.EnableTracking();
			    user.Token = Guid.NewGuid();
			    var oldRecords = SimpleRegUserService.GetAll(x => x.Email == user.Email).ToList();
			    oldRecords.ForEach(x => SimpleRegUserService.Delete(x));
			    SimpleRegUserService.Insert(user);
			    SimpleRegUserService.SubmitChanges();
			    				MailService.SimpleRegistration(user, CommonConst.SiteRoot + Url.SimpleReg().Urls.EmailConfirm(user.Token.ToString("N")));
                ShowMessage("На E-mail {0} отправлено письмо с ссылкой для подтверждения".FormatWith(user.Email));
                return RedirectBack(); 
                                //return MessageJson("На E-mail {0} отправлено письмо с ссылкой для подтверждения".FormatWith(user.Email));
		    }
            var error = ModelState.Values.SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage).JoinWith("<br/>");
            ShowErrorMessage(error);
			return RedirectBack();
	    }

		public ActionResult EmailConfirm(string id) {

			SimpleRegUserService.EnableTracking();
			var simpleRegUser = SimpleRegUserService.FirstOrDefault(x => x.Token == Guid.Parse(id));
			if (simpleRegUser == null) {
				return NotFound();
			}

			if (UserService.GetAll(x => x.Email == simpleRegUser.Email).Any()) {
				return BaseViewWithTitle("Регистрация",
					new PagePart("Пользователь с емейлом {0} уже зарегистрирован"
                    .FormatWith(simpleRegUser.Email)));
			}
            
            var user = new User {
				Email = simpleRegUser.Email,
				FirstName = simpleRegUser.Name,
				LastName = simpleRegUser.LastName,
				Password = Membership.GeneratePassword(6, 0),
                Source_TC = simpleRegUser.Source_TC
			};
			UserService.CreateUser(user);
			AuthService.SignIn(user.Email, true);
			OrderService.UpdateSessionOrderUser();
 
			SimpleRegUserService.DeleteAndSubmit(simpleRegUser);
			var url = simpleRegUser.Url.IsEmpty() ? Url.Profile().Urls.Details() : simpleRegUser.Url;
            MailService.RegistrationComplete(user, null, false);

            SpecialistExportService ses = new SpecialistExportService();
            ses.InsertStudentBySimpleUser(user);
			ShowMessage("Вы успешно зарегистрировались на сайте");
			return Redirect(url);
		}
	}
}
