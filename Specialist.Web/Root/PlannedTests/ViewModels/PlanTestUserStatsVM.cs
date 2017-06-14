using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.PlannedTests.ViewModels {
	public class PlanTestUserStatsVM:IViewModel {
		public class ModuleSetStat {
			public TestModuleSet Set { get; set; }

			public ModuleSetStat(TestModuleSet set, List<UserTest> userTests) {
				Set = set;
				UserTests = userTests;
			}

			public List<UserTest> UserTests { get; set; }
		}

		public decimal GroupId { get; set; }

		public Dictionary<int,string> UserNames { get; set; }

		public List<ModuleSetStat> ModuleSetStats { get; set; }

		public string Title { get { return "Статистика слушателей группы " + GroupId; } }
	}
}