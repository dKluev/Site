using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.OrgTests.ViewModels;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Helpers;

namespace Specialist.Web.Root.Profile.Views {
	public class CompanyTestsView:BaseView<CompanyTestsVM> {
		public override object Get() {
			return div[table.Class("table")[H.Head("Тест", "Результаты"),
				Model.Tests.Select(x => Row(Url.Test().Details(x.Id,x.Name), 
					Url.OrgTest().Results(x.Id,"Результаты")))]];
		}
	}
}