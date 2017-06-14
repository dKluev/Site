using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleUtils.StrongRouting;
using Specialist.Entities.Passport;
using Specialist.Web.Cms.Controllers;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Mvc.Controllers;

namespace Specialist.Web.ActionFilters {
	public class AuthFilterProvider : IFilterProvider
{
    public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
    {
        var rd = controllerContext.RouteData;
        var controller = rd.GetRequiredString("controller");
    	var orderEntityController = MvcConst.GetControllerName(typeof (OrderEntityController));
    	var publicController = MvcConst.GetControllerName(typeof (PublicController)); 
    	var accountController = MvcConst.GetControllerName(typeof (AccountController)); 
    	var homeController = MvcConst.GetControllerName(typeof (HomeController)); 
		if(controller.In(publicController, accountController))
            return Enumerable.Empty<Filter>();
		if(controller == homeController)
			return new[]{ new Filter(new AuthorizeAttribute(), FilterScope.Action, 0)};
		if(controller == orderEntityController)
			return new[]{new Filter(new AuthAttribute {RoleList = Role.OrderManager}, FilterScope.Action, 0)};
			
		return new[]{new Filter(new AuthAttribute {RoleList = Role.Admin}, FilterScope.Action, 0)};
    }
}
 
}