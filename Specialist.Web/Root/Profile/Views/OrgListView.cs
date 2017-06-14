using Specialist.Entities.Const;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Helpers;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgListView:BaseView<OrgListVM> {
		public override object Get() {
			return div[H.table.Class("table")[H.Head("Компания", "Файлы"),
				Model.Companies.Select(x => H.Row( 
					x.CompanyName, Url.OrgProfile().Files(x.CompanyID, "Файлы")))]];
		}
	}
}