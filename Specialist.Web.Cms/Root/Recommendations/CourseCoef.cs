using Specialist.Entities.Context;

namespace Specialist.Web.Cms.Root.Recommendations {
	public class CourseCoef {
		 
		public int Id { get; set; }

		public string CourseTC { get; set; }

		public string CourseTC2 { get; set; }

		public decimal Coef { get; set; }

		public decimal Count { get; set; }

		public Course Course2 { get; set; }
	}
}