using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Order.ViewModel {
	public class OrgOrderCompleteVM: IViewModel {
		public Context.Order Order { get; set; }
		public string Title { get {
			return "Заказ оформлен";
		} }
	}
}