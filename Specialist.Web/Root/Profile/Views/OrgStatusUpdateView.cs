using Specialist.Entities.Profile.ViewModel.Common;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.Logic;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Root.Profile.ViewModels;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgStatusUpdateView:BaseView<OrgStatusUpdateVM> {
		public override object Get() {
			return
				AjaxForm(Url.Action<OrgProfileController>(x => x.StatusUpdatePost(null)))[
					LabeledTextInput("Код",x => x.Code),
					SaveButton("Обновить статус")];
		}
	}
}