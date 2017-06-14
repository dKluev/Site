using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Catalog.Views {
	public class GuideVM: IViewModel {
		public Guide Guide { get; set; }
		public string Title {
			get { return "Путеводитель по курсам " + StringUtils.AngleBrackets(Guide.Name); }
		}
	}
}