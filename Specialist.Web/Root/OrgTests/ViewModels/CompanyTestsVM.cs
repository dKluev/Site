using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.OrgTests.ViewModels {
	public class CompanyTestsVM:IViewModel {
		public List<Test> Tests { get; set; }
		public string Title {
			get { return "Активные тесты компании"; }
		}
	}
}