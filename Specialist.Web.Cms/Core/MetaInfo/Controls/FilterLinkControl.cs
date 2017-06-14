using System.Web.Routing;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;

namespace SimpleUtils.FluentAttributes.Core.Controls
{
    public class FilterLinkControl:ActionLinkControl
    {
        public string ForeignProperty { get; set; }
        public FilterLinkControl(string displayName, string controller, string foreignProperty) : base(displayName, "List", controller)
        {
            ForeignProperty = foreignProperty;
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
                {"pageIndex", 1},
                {ForeignProperty, LinqToSqlUtils.GetPK(entity)}
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