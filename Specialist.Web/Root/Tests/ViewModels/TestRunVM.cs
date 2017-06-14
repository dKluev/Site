using System.Collections.Generic;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestRunVM {
		public Test Test { get; set; }

		public UserTest UserTest { get; set; }

		public List<TestQuestion> OtherPreTestQuestions { get; set; }
		public TestRunVM() {OtherPreTestQuestions = new List<TestQuestion>();}
	}
}