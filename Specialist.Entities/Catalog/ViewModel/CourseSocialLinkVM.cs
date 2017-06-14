using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.ViewModel {
	public class CourseSocialLinkVM:IViewModel {
		public Course Course { get; set; }
		public string Title { get { return "Запись на курс " + Course.WebName; } }
	}
}