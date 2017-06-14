using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Order.ViewModel {
	public class ExpressOrderCompleteVM:IViewModel {
		public Context.Order Order { get; set; }
		public string Title { get { return "Заказ успешно оформлен"; } }
	}
}