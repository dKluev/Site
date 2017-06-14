using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Center.ViewModels {
	public class MarketingActionsVM: IViewModel {

		public List<MarketingAction> Actions { get; set; }
		public string Title {
			get { return "Спецпредложения"; }
		}
	}
}