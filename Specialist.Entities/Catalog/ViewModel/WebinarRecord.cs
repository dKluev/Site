using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.ViewModel {
	public class WebinarRecord {
//		public int Price { get { return (int)(DiscountPrice*1.3); } }

		public decimal DiscountPrice { get; set; }

		public Course Course { get; set; }
	}
}