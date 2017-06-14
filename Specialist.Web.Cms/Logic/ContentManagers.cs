using Specialist.Web.ActionFilters;

namespace Specialist.Web.Cms.Logic {
	public class ContentManagers:AuthAttribute {
		public ContentManagers() {
			Emails = "ptolochko,ashirokov,eaorlova";
		}
	}
}
