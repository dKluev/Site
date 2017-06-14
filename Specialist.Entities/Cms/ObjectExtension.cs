using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using Specialist.Entities.Context;

namespace Specialist.Entities.Context
{
    public static class ObjectExtension
    {
        public static SiteObject GetSiteObject(this object obj)
        {
            return new SiteObject
            {
                ID = LinqToSqlUtils.GetPK(obj),
                Type = LinqToSqlUtils.GetTableName(obj)
            };
        }
    }
}