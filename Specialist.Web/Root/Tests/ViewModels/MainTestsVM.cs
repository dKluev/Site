using System.Collections.Generic;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class MainTestsVM {
		public List<Section> Sections { get; set; }
		public bool IsSecond { get; set; }
		public string Description { get; set; }
	}
}