using System.Web.Mvc;
using Specialist.Entities.Catalog;

namespace Specialist.Web.ActionFilters {
	public class NotMobileRedirect: ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			if(!CommonConst.IsMobile)
				filterContext.Result = new RedirectResult("/");
			base.OnActionExecuting(filterContext);
		}
	}
}