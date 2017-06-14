using System.Web.Routing;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using Specialist.Web.Cms.Const;

namespace SimpleUtils.FluentAttributes.Core.Controls
{
    public class RelationsLinkControl:ActionLinkControl
    {
        public RelationsLinkControl() : base("[Тег]", "SiteObjectEdit", 
            "SiteObjectRelation" + Specialist.Web.Cms.Const.Common.ControlPosfix)
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
            return new RouteValueDictionary(new {typeName = entity.GetType().FullName, id = LinqToSqlUtils.GetPK(entity) });
        }

        public override string ImageName
        {
            get
            {
                return "Relations";
            }
        }
    }
}