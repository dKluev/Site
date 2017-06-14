using Specialist.Entities.Profile.ViewModel.Common;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.Logic;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Root.Profile.Views {
	public class ChangeNameRequestView:BaseView<ChangeNameRequestVM> {
		public override object Get() {
			return
				AjaxForm(Url.Action<ProfileController>(x => x.ChangeNameRequest(null)))[
					Label("Фамилия"), div.Class("editor-div")[
					InputText(Model.For(x => x.User.LastName), "")],
					Label("Имя"), div.Class("editor-div")[
					InputText(Model.For(x => x.User.FirstName), "")],
					Label("Отчество"), div.Class("editor-div")[
					InputText(Model.For(x => x.User.SecondName), "")],
					SaveButton("Отправить запрос на смену ФИО")
						];
		}
	}
}