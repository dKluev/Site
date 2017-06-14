using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Tests;
using Specialist.Web.ActionFilters;
using Specialist.Web.Core;
using Specialist.Web.Core.Views;
using Specialist.Web.Pages;
using Specialist.Web.Root.OrgTests.ViewModels;
using Specialist.Web.Root.Profile.Views;

namespace Specialist.Web.Controllers {
	[Auth(RoleList = Role.TestCorpManager)]
	public class OrgTestController:ViewController {

		[Dependency]
		public IRepository2<Test> TestService { get; set; }

		[Dependency]
		public UserTestResultService UserTestResultService { get; set; }

		[Dependency]
		public IRepository2<UserTest> UserTestService { get; set; }
		 
		public ActionResult Tests() {
			var tests = TestService
				.GetAll(x => x.CompanyId == User.CompanyID && x.Status == TestStatus.Active).ToList();
			var model = new CompanyTestsVM {
				Tests = tests
			};
			return BaseViewWithModel(new CompanyTestsView(), model);
		}

		public ActionResult Results(int id) {
			UserTestService.LoadWith(x => x.User);
			var userTests = GetUserTests(id, true);
			var model = new CompanyTestResultsVM {
				Tests = userTests,
				Test = TestService.GetByPK(id)
			};
			return BaseViewWithModel(new CompanyTestResultsView(), model);
		}

		private List<UserTest> GetUserTests(int id, bool isBest) {
			var userTests = UserTestService.GetAll(x => 
				x.User.EmployeeCompanyID == User.CompanyID
					&& x.TestId == id);

			if (isBest) {
				userTests = userTests.Where(x => x.IsBest);
			}
			return userTests.OrderByDescending(x => x.RunDate).Take(100).ToList();
		}

		public ActionResult DownloadResults(int id) {
			UserTestService.LoadWith(x => x.User, x=> x.Test);
			var userTests = _.List(GetUserTests(id, false));
			var data = UserTestResultService.GetResultData(userTests);
			return File(StringUtils.Encoding1251.GetBytes(CsvUtil.Render(data)), 
				"text/csv", "TestResults-{0}.csv".FormatWith(id));
			
		}


	}
}