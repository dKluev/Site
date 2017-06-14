using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgNextCoursesVM:IViewModel {
		public List<Course> Courses { get; set; }
		
		public string Title { get { return "Варианты продолжения"; } }
	}
}