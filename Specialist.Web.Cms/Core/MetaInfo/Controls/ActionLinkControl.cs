using System;
using System.Web.Routing;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;

namespace SimpleUtils.FluentAttributes.Core.Controls
{
    public class ActionLinkControl: ExtraControl
    {
        public string Action { get; set; }

        public string Controller { get; set; }

        public override string ImageName
        {
            get
            {
                return Action;
            }
        }

        public ActionLinkControl(string displayName, string action, 
            string controller) : base(displayName)
        {
            Action = action;
            Controller = controller;
        }

        public override RouteValueDictionary GetRouteValues(object entity)
        {
            return new RouteValueDictionary(new { id = LinqToSqlUtils.GetPK(entity) });
        }
    }
}