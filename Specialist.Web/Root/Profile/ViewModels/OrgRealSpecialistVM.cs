using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgRealSpecialistVM: IViewModel {

		public List<Student> Students { get; set; }

		public string Title {
			get { return "Настоящие специалисты"; }
		}
	}
}