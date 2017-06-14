using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.ViewModel {
	public class AddSeminarVM:IViewModel {
		public string Title { get { return "Выбор способа прохождения семинара"; } }

		public Group Group { get; set; }

	}
}