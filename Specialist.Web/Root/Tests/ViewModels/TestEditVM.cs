using System.Collections.Generic;
using System.Web.Mvc;
using Specialist.Entities.Tests;
using Specialist.Web.Common.Mvc.Binders;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestEditVM {
		public Test Test { get; set; }

		public string CourseName { get; set; }

		public List<TestModule> Modules { get; set; }
		public Dictionary<int,int> ModulePercents { get; set; }
	}
}