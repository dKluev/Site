using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Passport;
using Specialist.Services.Interface.Passport;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Util;

namespace Specialist.Web.ActionFilters
{
    public class AuthAttribute : AuthorizeAttribute
    {

    	public string Emails { get; set; }

    	public Role RoleList { get; set; }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var user = UnityRegistrator.Container.Resolve<IAuthService>().CurrentUser;
            if (user == null)
                return false;
			if(!Emails.IsEmpty())
	            return Emails.Split(',').Any(e => e  + "@specialist.ru" == user.Email);
        	if(RoleList != Role.None) {
        		return user.InRole(RoleList);
        	}
        	return true;
        }
    }
}