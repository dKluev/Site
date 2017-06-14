using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Web.Cms.Root.Recommendations {
	public class SectionRecVM {
		public List<Section> Sections { get; set; }

		public List<FullCourseCoef> Coefs { get; set; }
		public List<FullCourseCoef> ExcludeCoefs { get; set; }
		public SectionRecVM() {
			Sections = new List<Section>();
			Coefs = new List<FullCourseCoef>();
			ExcludeCoefs = new List<FullCourseCoef>();
		}
	}
}