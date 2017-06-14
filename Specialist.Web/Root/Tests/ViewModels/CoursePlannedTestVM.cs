using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class CoursePlannedTestVM:IViewModel {
		public List<TestModuleSet> ModuleSets { get; set; }

		public Course Course { get; set; }
		public string Title { get { return "Плановое тестирование по курсу: " + Course.WebName; } }

		public Dictionary<int, UserTest> Statuses { get; set; }
	}
}