using Specialist.Entities.Catalog.Interface;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgStatusUpdateVM:IViewModel {
		public string Code { get; set; }

		public string Title { get { return "Обновить статус компании"; }}
	}
}