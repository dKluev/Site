using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.Logic;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Root.Profile.Views {
	public class MyBusinessView:BaseView<MyBusinessUser> {
		public override object Get() {
			return
				AjaxForm(Url.Action<ProfileController>(x => x.MyBusiness(null)))[
					Label("Промокод"), div.Class("editor-div")[
					InputText(Model.For(x => x.Code), "")],
					SaveButton("Зарегистрировать промокод")
						];
		}
	}
}