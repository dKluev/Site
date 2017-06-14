using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using SimpleUtils.Common.Extensions;

namespace Specialist.Services.Common.Extension
{
    public static class GenericQueryable
    {

        public static T ByUrlName<T>(this IQueryable<T> queryable,
            string urlName)
        {
            return queryable.Where("UrlName = @0", urlName).FirstOrDefault();
        }

		 public static IQueryable<T> IsNotNull<T>(this IQueryable<T> queryable)
        {
            return queryable.Where(x => x != null);
        }

        public static T BySysName<T>(this IQueryable<T> queryable,
           string sysName)
        {
            return queryable.Where("SysName = @0", sysName).FirstOrDefault();
        }

        public static IQueryable<T> IsActive<T>(this IQueryable<T> queryable)
        {
            return queryable.Where("IsActive");
        }

		public static IEnumerable<T> IsActive<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.AsQueryable().IsActive();
        }

        public static IQueryable<T> ByWebOrder<T>(this IQueryable<T> queryable) {
            return queryable.OrderBy("WebSortOrder");
        }

        public static T SysName<T>(this IQueryable<T> queryable, string sysName)
        {
            return queryable.Where("SysName = @0", sysName).First();
        }

        public static IQueryable<T> OrderByName<T>(this IQueryable<T> queryable)
        {
            return queryable.OrderBy("Name");
        }

        public static T ByPrimeryKey<T>(this IQueryable<T> queryable, 
            object primeryKey)
        {
            if (primeryKey == null)
                return default(T);
            var type = typeof(T);
            return queryable.Where(LinqToSqlUtils.GetPKPropertyName(type) +
               " = @0", primeryKey).FirstOrDefault();
        }

		public static IQueryable<T> GetRandom<T>(this IQueryable<T> queryable, 
			int count = 1) {
			var allCount = queryable.Count();
			if(allCount <= count)
				return queryable;
			return queryable.Skip(new Random().Next(allCount - count + 1)).Take(count);
		}

    	public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, Dictionary<string, object> filterParameters) where T : class
        {
            var filter = string.Empty;
            var values = new object[filterParameters.Count];
            var i = 0;
            foreach (var pair in filterParameters)
            {
                var key = pair.Key;
                values[i] = pair.Value;
                if (pair.Value.GetType() == typeof(string))
                    filter += string.Format("{0}.Contains(@{1})", key, i);
                else
                    filter += key + " = @" + i;
                if (i != filterParameters.Count - 1)
                    filter += " && ";
                i++;
            }

            if (filter == string.Empty)
                return queryable;

            return queryable.Where(filter, values);
        }

    }
}