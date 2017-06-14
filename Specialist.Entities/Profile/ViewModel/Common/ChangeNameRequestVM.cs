using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.ViewModel.Common {
	public class ChangeNameRequestVM:IViewModel {
		public User User { get; set; }


		public string Title {
			get { return "Запрос на смену ФИО"; }
		}
	}
}