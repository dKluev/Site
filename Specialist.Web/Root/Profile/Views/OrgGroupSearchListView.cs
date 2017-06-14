using System.Linq;
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
	public class OrgGroupSearchListView:BaseView<OrgGroupSearchVM> {
		public override object Get() {
			var result = Model.Groups;
			if (!result.Any()) return H.b["Групп нет"];
			var table = new OrgProfileViews(Url)
				.OrgGroups(result);
			var id = "org-groups-" + Model.PageIndex;
			var button = 
				Model.Groups.IsNextPage ?
			div.Class(id)[
				AjaxForm(Url.OrgProfile().Urls.GroupSearchPost(null))
				.AddClass("content-load-form").Id(id)[
				HiddenFor(x => x.CourseTC),
				HiddenFor(x => x.StudentId),
				HiddenFor(x => x.LeftDataBeg), 
				HiddenFor(x => x.Current), 
				HiddenFor(x => x.RightDataBeg), 
				Hidden(Model.For(x => x.PageIndex), Model.PageIndex + 1),
				
				SaveButton("Загрузить еще")]] : null;
			return div[table, button];
		}
	}
}