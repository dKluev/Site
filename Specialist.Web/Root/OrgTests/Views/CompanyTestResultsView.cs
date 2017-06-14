using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;
using Specialist.Web.Core.Views;
using Specialist.Web.Root.OrgTests.ViewModels;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Helpers;

namespace Specialist.Web.Root.Profile.Views {
	public class CompanyTestResultsView:BaseView<CompanyTestResultsVM> {
		public override object Get() {
			return div[GroupTestResultView.UserTestsView(Url, Model.Tests),
				Url.OrgTest().DownloadResults(Model.Test.Id,"Скачать")];
		}
	}
}