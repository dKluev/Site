using Specialist.Entities.Const;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Helpers;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgFileListView:BaseView<OrgFileListVM> {
		public override object Get() {
			if(!Model.CompanyFiles.Any())
				return p["Пока ничего нет"];
			return div[H.table.Class("table")[H.Head("Дата", "Файл", ""),
				Model.CompanyFiles.Select(x => H.Row( 
					x.CreateDate.DefaultString(), x.Name, H.Anchor(CompanyFiles.GetFileUrl(x.Id), "Скачать")))]];
		}
	}
}