using Specialist.Entities.Catalog.Interface;

namespace Specialist.Web.ViewModel.Orders {
	public class ExpressRegisterVM: IViewModel {
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string SecondName { get; set; }

		public string Phone { get; set; }
		public bool PersonalData { get; set; }

		public string Email { get; set; }
		public string Title { get { return "Заказ в 1 клик"; }}
	}
}