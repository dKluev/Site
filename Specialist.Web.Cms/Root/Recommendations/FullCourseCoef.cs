using Specialist.Entities.Context;

namespace Specialist.Web.Cms.Root.Recommendations {
	public class FullCourseCoef {
		public Course Course { get; set; }

		public decimal Coef { get; set; }

		public FullCourseCoef(Course course, decimal coef) {
			Course = course;
			Coef = coef;
		}
	}
}