using System.Web.Mvc;
using Specialist.Entities.Catalog;

namespace Specialist.Web.ActionFilters {
	public class MobileCacheAttribute: OutputCacheAttribute {
		public MobileCacheAttribute() {
			Duration = 60*60;
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			if(CommonConst.IsMobile)
				base.OnActionExecuted(filterContext);
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			if(CommonConst.IsMobile)
				base.OnActionExecuting(filterContext);
		}

		public override void OnResultExecuted(ResultExecutedContext filterContext) {
			if(CommonConst.IsMobile)
				base.OnResultExecuted(filterContext);
		}

		public override void OnResultExecuting(ResultExecutingContext filterContext) {
			if(CommonConst.IsMobile)
				base.OnResultExecuting(filterContext);
		}
	}
}