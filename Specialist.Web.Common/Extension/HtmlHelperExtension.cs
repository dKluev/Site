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

namespace Specialist.Web.Common.Extension
{
    public static class HtmlHelperExtension
    {
		public static UrlHelper Url(this HtmlHelper helper) {
			return new UrlHelper(helper.ViewContext.RequestContext);
		}

/*
        public static void RenderPartialNotEmpty(this HtmlHelper helper, string viewName,
            IEnumerable model)
        {
            if (model == null || model.GetEnumerator().Current == null)
                helper.RenderPartial(viewName, model);
        }
*/

        public static MvcForm DefaultForm<T>(this HtmlHelper helper, Expression<Action<T>>
            selector) where T : Controller
        {
            return DefaultForm<T>(helper, selector, new {});
        }

        public static MvcForm DefaultForm<T>(this HtmlHelper helper, Expression<Action<T>>
            selector, object htmlAttributes) where T : Controller
        {
            var dictionary = new RouteValueDictionary(htmlAttributes);
            return 
                helper.BeginForm(selector, FormMethod.Post,
                   dictionary);
            
        }


       /* public static bool InRole(this HtmlHelper helper, params string[] roles)
        {
            var user = helper.ViewData[AuthService.CurrentUserSessionKey]
                as User;
            return user != null && roles.Any(user.InRole);
        }
*/



        public static TagBuilder File(this HtmlHelper htmlHelper, string name) {
            var builder = HtmlControls.File(name);
            ModelState state;
            if (htmlHelper.ViewData.ModelState.TryGetValue(name, out state) && (state.Errors.Count > 0)) {
                builder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
            return builder;

        }

        public static string CheckBoxList(this HtmlHelper htmlHelper, string name,
            List<SelectListItem> listInfo)
        {
            return htmlHelper.CheckBoxList(name, listInfo, null);
        }



        public static string CheckBoxList(this HtmlHelper htmlHelper, string name,
            List<SelectListItem> listInfo, object htmlAttributes)
        {
            return htmlHelper.CheckBoxList(name, listInfo,
                new RouteValueDictionary(htmlAttributes));
        }



        public static string CheckBoxList(this HtmlHelper htmlHelper, string name,
            List<SelectListItem> listInfo, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("The argument must have a value", "name");
            if (listInfo == null)
                throw new ArgumentNullException("listInfo");
            if (listInfo.Count < 1)
                throw new ArgumentException("The list must contain at least one value", "listInfo");

            var sb = new StringBuilder();
            foreach (var info in listInfo)
            {
                TagBuilder builder = new TagBuilder("input");
                if (info.Selected) builder.MergeAttribute("checked", "checked");
                builder.MergeAttributes(htmlAttributes);
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", info.Value);
                builder.MergeAttribute("name", name);
                builder.InnerHtml = info.Text;
                sb.Append(builder.ToString(TagRenderMode.Normal));
                sb.Append("<br />");
            }
            return sb.ToString();

        }

    }





}