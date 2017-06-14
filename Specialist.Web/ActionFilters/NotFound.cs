using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Interface.Passport;
using System.Linq;
using Specialist.Web.Common.ViewModel;
using Specialist.Web.Const;
using SimpleUtils.Reflection.Extensions;

namespace Specialist.Web.ActionFilters {
	public class HandleNotFoundAttribute : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext filterContext) {
			if (filterContext.ActionParameters.All(pair => pair.Value == null
				|| pair.Value == pair.Value.GetType().Default())) {
				filterContext.Result = GetNotFoundResult(filterContext.Controller);
				;
			} else {
				base.OnActionExecuting(filterContext);
			}
		}

		private ViewResult GetNotFoundResult(ControllerBase controller) {
			var result = new ViewResult();
			result.ViewName = ViewNames.Error;
			result.ViewData = controller.ViewData;
			result.TempData = controller.TempData;
			var context = HttpContext.Current;
			context.Response.StatusCode = 404;
			result.ViewData.Model = new ErrorVM(404, context.Request.Url.PathAndQuery);
			return result;
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext) {
			if (filterContext.Result is RedirectResult)
				return;
			if(filterContext.Result is EmptyResult) {
				if(filterContext.Exception != null)
					return;
				filterContext.Result = GetNotFoundResult(filterContext.Controller);
				return;
			}
			var viewResult = filterContext.Result as ViewResult;

			if (viewResult != null && viewResult.ViewData.Model == null) {
				filterContext.Result = GetNotFoundResult(filterContext.Controller);
			}
		}

	}
}