using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgListVM: IViewModel {
		public List<Company> Companies { get; set; }
		public string Title {
			get { return "Компании"; }
		}
	}
}