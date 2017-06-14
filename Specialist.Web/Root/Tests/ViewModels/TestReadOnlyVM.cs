using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestReadOnlyVM {
		public Test Test { get; set; }

		public List<TestModule> Modules { get; set; } 

		public string EmployeeTC { get; set; }

		public Employee Checker { get; set; }
	}
}