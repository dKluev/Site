using Specialist.Entities.Tests;

namespace Specialist.Web.Core.Views {
	public class TestRunDetailsVM {
		public Test Test { get; set; }

		public string CourseName { get; set; }

		public string CourseTC { get; set; }

		public int? ModuleSetId { get; set; }
	}
}