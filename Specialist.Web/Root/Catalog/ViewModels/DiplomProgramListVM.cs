using System;
using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Entities.ViewModel {
	public class DiplomProgramListVM {
		public List<Tuple<Section, AllCourseListVM>> List { get; set; }
	}
}