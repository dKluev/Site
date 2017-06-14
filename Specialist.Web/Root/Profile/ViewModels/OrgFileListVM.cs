using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgFileListVM: IViewModel {
		public List<CompanyFile> CompanyFiles { get; set; }
		public string Title {
			get { return "Документы " + CompanyName; }
		}

		public string CompanyName { get; set; }
	}
}