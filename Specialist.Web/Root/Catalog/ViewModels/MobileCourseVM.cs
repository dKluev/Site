using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Context;

namespace Specialist.Entities.ViewModel {
	public class MobileCourseVM:CourseBaseVM {

		public List<Group> Groups { get; set; }

		public Group Group { get; set; }

		public Section Section { get; set; }

		public bool CanOrder { get; set; }

	}
}