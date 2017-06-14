using System.Collections.Generic;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class GroupTestEditVM {

		public GroupTest GroupTest { get; set; }


		public List<TestModule> Modules { get; set; }
		public Dictionary<int,int> ModulePercents { get; set; }
		 
	}
}