using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.ViewModel;
using Specialist.Services.Interface.Passport;
using Specialist.Services.SitePart;
using Specialist.Web.Common.Logic;
using Specialist.Web.Const;
using Specialist.Web.Controllers;
using Specialist.Web.Common.Html;
using SimpleUtils;
using SimpleUtils.Extension;
using Specialist.Web.Controllers.Center;
using System.Linq;
using MvcContrib;
using Specialist.Web.Common.Mvc;
using Microsoft.Practices.Unity;

namespace Specialist.Web.Helpers
{
    public static class CommonSiteHelper
    {

       public static bool InRole(this HtmlHelper helper, Role roles)
        {
		        	var user = GetUser();
            return user != null && user.InRole(roles);
        }

	    public static User GetUser() {
		    return MvcApplication.Container.Resolve<IAuthService>().CurrentUser;
	    }

		public static string Calendar(this HtmlHelper helper, decimal groupId)
		{
			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
			var url = urlHelper.Action<GroupController>(c => c.Calendar(groupId));
			var form = new TagBuilder("form");
			form.Class("add-calendar");
			form.Attributes.Add("action", url);
			form.Attributes.Add("method", "post");
			var innerHtml = HtmlControls.ImgSubmit(Urls.Main("add_in.gif"))
				.Attr(new {title = "Добавить в календарь"})
				.ToString();
			innerHtml += HtmlControls.Hidden("groupId",groupId);
			form.InnerHtml = innerHtml;
			return form.ToString();
		
		}
	
        public static string BreadCrumbs(this ViewMasterPage page)
        {
            var view = page.ViewContext.View as WebFormView;
            if (view == null || page.Model == null)
                return null;
            var currentMainMenu = MainMenu.GetAll()
                .FirstOrDefault(x => x.HasModel(Common.Logic.BreadCrumbs.GetModel(page)));
            var breadCrumbs = new BreadCrumbs(page, currentMainMenu).Get();
            if(breadCrumbs.IsEmpty())
                return null;
	        var sp = "&nbsp;";
	        var sep = sp + "&gt;" + sp;
			return breadCrumbs.JoinWith(sep)
				.Replace(sep + Common.Logic.BreadCrumbs.Separator + sep,
				sp + Common.Logic.BreadCrumbs.Separator + sp)
				.Remove(sep + Common.Logic.BreadCrumbs.Separator);
        }
        
    }
}