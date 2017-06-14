using Microsoft.Web.Mvc;
using Specialist.Entities.Passport;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Mvc.Controllers;
using Specialist.Web.Controllers;
using Specialist.Web.Core.Logic;
using Specialist.Web.ViewModel.Orders;

namespace Specialist.Web.Root.Profile.Views {
	public class SimpleRegistrationView:BaseView<SimpleRegUser> {
		public override object Get() {
			return l(
                HtmlHelper.ActionLink<AccountController>(x=>x.LogOn(Model.Url), "Войти"),
				AjaxForm(Url.Action<SimpleRegController>(x => x.RegistrationPost(null)))[
				ControlFor(x => x.Name), 
				ControlFor(x => x.LastName), 
				ControlFor(x => x.Email), 
				HiddenFor(x => x.Url),
				SaveButton("Отправить")
				]);
		}
	}
}