using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class PrerequisiteTestVM {
		public Test Test { get; set; }

		public Course Course { get; set; }

		public CoursePrerequisite CoursePrerequisite { get; set; }

		public List<Course> PrerequisiteCourses { get; set; } 
	}
}