using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgStudentVM:IViewModel {
		public Student Student { get; set; }

		public List<Group> Groups { get; set; }
		public string Title { get { return Student.FullName; } }
	}
}