using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Web.ViewModel.Orders {
	public class ExtrasesVM {
		public ExtrasesVM() {
			SelectedExtrases = new List<decimal>();
		}

		public Dictionary<decimal, decimal> ExtrasPrices { get; set; }

		public OrderDetail OrderDetail { get; set; }

		public IEnumerable<Extras> Extrases { get; set; }

		public List<decimal> SelectedExtrases { get; set; }
	}
}