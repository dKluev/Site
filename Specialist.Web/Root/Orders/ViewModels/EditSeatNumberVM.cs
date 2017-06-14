using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Web.ViewModel.Orders {
	public class EditSeatNumberVM: IViewModel {
		public List<short> Available { get; set; }

		public short? Current { get; set; }

		public string ClassRoomTC { get; set; }

		public decimal OrderDetailId { get; set; }
		public string Title { get { return "Выбор места в классе"; } }
	}
}