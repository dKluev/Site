using Specialist.Entities.Profile.ViewModel.Common;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.Logic;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Root.Profile.ViewModels;
using Specialist.Web.Const;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgGroupSearchView:BaseView<OrgGroupSearchVM> {
		public override object Get() {
			var id = "group-search-list";
			object autoSearch = null;
			if (Model.AutoSearch) {
				autoSearch = JQuery("$('#org-search-button').click();");;
			}
			return div[
				autoSearch,

				AjaxForm(Url.OrgProfile().Urls.GroupSearchPost(null)).AddClass("content-load-form").Id(id)[
				Select2For(Model.CourseTC,x => x.CourseTC,
					Url.Action<OrgProfileController>(x => x.GetCoursesAuto(null))),
				Select2For(Model.StudentId,x => x.StudentId,
					Url.Action<OrgProfileController>(x => x.GetStudentsAuto(null))),
					ControlFor(x => x.LeftDataBeg), 
					ControlFor(x => x.RightDataBeg), 
					ControlFor(x => x.Current), 
					SaveButton("Искать").Id("org-search-button"),
					div.Class(id),
					div.Style("height:500px;")
						]];
		}
	}
}