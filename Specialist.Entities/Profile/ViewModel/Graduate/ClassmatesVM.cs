using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.ViewModel {
	public class ClassmatesVM:IViewModel {
		public List<Tuple<CourseLink,List<User>>> Courses { get; set; }

		public string Title { get { return "Контакты одногруппников"; } }
	}
}