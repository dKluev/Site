using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Extension;
using Specialist.Entities.Passport;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Passport;
using System.Linq;

namespace Specialist.Web.ActionFilters
{
    public class UpdateAuthRoleList: ControllerActionInvoker
    {
    	public Role Role { get; set; }

    	public UpdateAuthRoleList(Role role) {
    		Role = role;
    	}

    	protected override FilterInfo GetFilters(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
        	foreach (var authAttribute in filters.AuthorizationFilters.OfType<AuthAttribute>()
				.Where(x => x.RoleList == Role.Admin)) {
        		authAttribute.RoleList = Role;
        	}
        	return filters;
        }
    }
}