using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Specialist.Entities.Passport;
using Specialist.Services.Passport;
using Microsoft.Web.Mvc;
using System.Linq;
using Specialist.Web.Common.Html;
using Specialist.Web.Helpers;

namespace Specialist.Web.Common.Extension
{
    public static class HtmlHelperExtension
    {
        public static SiteHtmls Site(this HtmlHelper helper) {
        	return new SiteHtmls(helper);
        }
    }





}