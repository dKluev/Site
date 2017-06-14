using System.Web.Routing;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;

namespace SimpleUtils.FluentAttributes.Core.Controls
{
    public class TabFocusControl:ActionLinkControl
    {
        public TabFocusControl(string displayName, string controller) : base(displayName, "Edit", controller)
        {
        }

        public override string Name
        {
            get
            {
                return typeof(ActionLinkControl).Name;
            }
        }

        public override RouteValueDictionary GetRouteValues(object entity)
        {
            return new RouteValueDictionary{
                {"id", LinqToSqlUtils.GetPK(entity)},
                {"tabFocus", DisplayName.GetHashCode()}
            };
        }

        public override string ImageName
        {
            get
            {
                return "FilterLink";
            }
        }
    }
}