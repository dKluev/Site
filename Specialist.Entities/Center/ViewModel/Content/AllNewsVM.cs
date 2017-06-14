using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Center.ViewModel {
	public class AllNewsVM : IViewModel
    {
		public int Year { get; set; }
        public string Title {
            get { return "Все новости за " + Year + " год"; }
        }
    }
}