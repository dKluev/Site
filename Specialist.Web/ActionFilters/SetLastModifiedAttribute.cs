using System.Web;
using System.Web.Mvc;
using Specialist.Entities.Logic;
using Specialist.Web.Util;

namespace Specialist.Web.ActionFilters
{
    public class SetLastModifiedAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var viewResult = filterContext.Result as ViewResult;
            if(viewResult == null)
                return;

            ResponseHeader.SetLastModifiedDate(viewResult.ViewData.Model);
        }




    }
}