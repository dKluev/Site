using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Passport;
using Specialist.Services.Interface.Passport;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Cms.Controllers;
using Specialist.Web.Common.Util;

namespace Specialist.Web.ActionFilters
{
    public class CmsAuthAttribute : AuthorizeAttribute
    {

    	public Role RoleList { get; set; }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
		    var routeData = httpContext.Request.RequestContext.RouteData;
		    var controller = routeData.GetRequiredString("controller");
		    var action = routeData.GetRequiredString("action");
	        var controllerType = typeof (CourseEntityController)
		        .Assembly.GetTypes().FirstOrDefault(x => x.Name == controller + "Controller");
	        var roles = RoleList;
	        if (controllerType != null) {
		        var method = controllerType.GetMethods().First(x => x.Name == action);

		        var attributes = method.GetCustomAttributes(typeof (AuthAttribute), false);
		        if (attributes.Length > 0) {
					roles = attributes[0].As<AuthAttribute>().RoleList;
				}
	        }
            var user = UnityRegistrator.Container.Resolve<IAuthService>().CurrentUser;
            if (user == null)
                return false;
        	if(roles != Role.None) {
        		return user.InRole(roles);
        	}
        	return true;
        }
    }
}