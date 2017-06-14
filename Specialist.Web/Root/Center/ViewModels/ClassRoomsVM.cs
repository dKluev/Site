using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Web.Root.Center.ViewModels {
	public class ClassRoomsVM:IViewModel {
		public string Title { get { return "Схемы классов"; } }

		public List<string> ClassRooms { get; set; } 
	}
}