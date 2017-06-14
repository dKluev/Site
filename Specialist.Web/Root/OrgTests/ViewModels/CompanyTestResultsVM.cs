using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.OrgTests.ViewModels {
	public class CompanyTestResultsVM:IViewModel {
		public Test Test { get; set; }
		public List<UserTest> Tests { get; set; }
		public string Title {
			get { return "Результаты " + Test.Name; }
		}
	}
}