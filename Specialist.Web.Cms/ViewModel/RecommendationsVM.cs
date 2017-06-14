using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Web.Cms.ViewModel {
	public class RecommendationsVM {
		public List<EntityStudySet> Products { get; set; }
		public List<EntityStudySet> Professions { get; set; }

		public int ProductId { get; set; }
		public int ProfessionId { get; set; }

		public string Email { get; set; }

		public List<Course> Courses { get; set; }

		public List<string> TestCourseTCs { get; set; }

		public List<string> CompleteCourseTCs { get; set; }
	}
}