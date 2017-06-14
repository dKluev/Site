using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Microsoft.Web.Mvc;
using Microsoft.Web.Mvc.Internal;
using Specialist.Entities.Catalog;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Common.Extension
{
    public static class LinkExtension
    {
        public static string Action<TController>(this UrlHelper helper, Expression<Action<TController>> action) where TController : Controller
        {
            var routeValue = GetRouteValuesFromExpression(action);
            return helper.RouteUrl(routeValue);
/*
            return helper.Action(routeValue["Action"].ToString(), 
                routeValue["Controller"].ToString(), );
*/
        }

        public static string ActionLinkImage<TController>(this HtmlHelper helper,
            Expression<Action<TController>> action, string imageRelativeUrl)
            where TController : Controller
        {
            return ActionLinkImage<TController>(helper, action, imageRelativeUrl,
                 Path.GetFileNameWithoutExtension(imageRelativeUrl));
        }

        public static string ActionLinkImage<TController>(this HtmlHelper helper,
            Expression<Action<TController>> action, string imageRelativeUrl, string alt)
            where TController : Controller
        {
            return ActionLinkImage<TController>(helper, action, imageRelativeUrl, alt, null);
        }

        public static string ActionLinkImage<TController>(this HtmlHelper helper,
            Expression<Action<TController>> action, string imageRelativeUrl, string alt,
            object htmlAttribute)
            where TController : Controller
        {
			if(imageRelativeUrl.IsEmpty())
				return string.Empty;
            var html = String.Format(helper.ActionLink(action, "{0}", htmlAttribute)
                .ToString(),
                                        helper.Image(imageRelativeUrl, alt));
            return html;
        }

    /*    public static string ActionLink<TController>(this HtmlHelper helper,
            Expression<Action<TController>> action, string text, string fragment)
            where TController : Controller {
            var html = helper.ActionLink(action, fragment);
          
            return html;
        }*/

        public static string ActionLinkImageEx<TController>(this HtmlHelper helper,
          Expression<Action<TController>> action, string imageRelativeUrl, string alt,
			object attrs = null)
          where TController : Controller
        {
            var html = String.Format(helper.ActionLinkEx(action, "{0}",attrs),
                                        helper.Image(imageRelativeUrl, alt));
            return html;
        }

        public static string ActionLinkImageEx<TController>(this HtmlHelper helper,
         Expression<Action<TController>> action, string content)
         where TController : Controller
        {
            var html = String.Format(helper.ActionLinkEx(action, "{0}"), content);
            return html;
        }

        private const string Href = "href=\"";
        public static string ActionLinkWithDomain<TController>(this HtmlHelper helper,
           Expression<Action<TController>> action, string linkText)
           where TController : Controller
        {
            var html = helper.ActionLink(action, linkText).ToString();
            var hrefIndex = html.IndexOf(Href) + Href.Length;
            html = html.Insert(hrefIndex, CommonConst.SiteRoot);
            return html;
        }

        public static string Action<TController>(this HtmlHelper helper, 
            Expression<Action<TController>> action) where TController : Controller
        {
            var values = GetRouteValuesFromExpression(action);
            return helper.Action(values["Action"].ToString(),values["Controller"].ToString(),values).ToString();
        }

        public static string ActionLinkEx<TController>(this HtmlHelper helper, 
            Expression<Action<TController>> action, string linkText, object htmlAttributes) where TController : Controller
        {
            var routeValuesFromExpression = 
                GetRouteValuesFromExpression(action);
            return helper.RouteLink(linkText, routeValuesFromExpression, 
                new RouteValueDictionary(htmlAttributes)).ToString();
        }

        public static string ActionLinkEx<TController>(this HtmlHelper helper, 
            Expression<Action<TController>> action, string linkText) 
            where TController : Controller
        {
            return helper.ActionLinkEx(action, linkText, null);
        }

      


        public static RouteValueDictionary GetRouteValuesFromExpression<TController>(Expression<Action<TController>> action) where TController : Controller
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            var body = action.Body as MethodCallExpression;
            if (body == null)
            {
                throw new ArgumentException("MustBeMethodCall", "action");
            }
            string name = typeof(TController).Name;
            if (!name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("TargetMustEndInController", "action");
            }
            name = name.Substring(0, name.Length - "Controller".Length);
            if (name.Length == 0)
            {
                throw new ArgumentException("CannotRouteToController", "action");
            }
            var rvd = new RouteValueDictionary();
            rvd.Add("Controller", name);
            rvd.Add("Action", body.Method.Name);
            AddParameterValuesFromExpressionToDictionary(rvd, body);
            return rvd;
        }

        public static RouteValueDictionary GetRouteValuesFromExpression(Expression<Action> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            var body = action.Body as MethodCallExpression;
            
            if (body == null)
            {
                throw new ArgumentException("MustBeMethodCall", "action");
            }
            string name = body.Method.DeclaringType.Name;
            if (!name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("TargetMustEndInController", "action");
            }
            name = name.Substring(0, name.Length - "Controller".Length);
            if (name.Length == 0)
            {
                throw new ArgumentException("CannotRouteToController", "action");
            }
            var rvd = new RouteValueDictionary();
            rvd.Add("Controller", name);
            rvd.Add("Action", body.Method.Name);
            AddParameterValuesFromExpressionToDictionary(rvd, body);
            return rvd;
        }

        private static void AddParameterValuesFromExpressionToDictionary(RouteValueDictionary rvd, MethodCallExpression call)
        {
            var parameters = call.Method.GetParameters();
            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    Expression expression = call.Arguments[i];
                    object obj2 = null;
                    ConstantExpression expression2 = expression as ConstantExpression;
                    if (expression2 != null)
                    {
                        obj2 = expression2.Value;
                    }
                    else
                    {
                        Expression<Func<object>> expression3 = 
                            Expression.Lambda<Func<object>>(
                            Expression.Convert(expression, typeof(object)), 
                            new ParameterExpression[0]);
                        obj2 = expression3.Compile()();
                    }
                    if(obj2 != null && obj2.GetType().IsClass 
                        && obj2.GetType() != typeof(string))
                    {
                        var values = new RouteValueDictionary(obj2);
                        foreach (var pair in values)
                        {
                            rvd.Add(pair.Key, pair.Value);
                        }
                    }
                    else
                        rvd.Add(parameters[i].Name, obj2);
                }
            }
        }

 

 


 

 



    }
}