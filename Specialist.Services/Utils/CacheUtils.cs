using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Reflection.Extensions;
using Specialist.Services.Utils;

namespace Specialist.Web.Common.Utils {
	public static class CacheUtils {
		public static readonly ConcurrentDictionary<string,object> _locks = 
				new ConcurrentDictionary<string, object>();

		public static T Cache<T>(this MethodBase methodBase, Func<T> getValue, int hours = 1) where T:class {
			return Get(methodBase, getValue, hours);
		}
		public static T CacheWith<T>(this MethodBase methodBase, Func<T> getValue, object param, int hours = 1) where T:class {
			return Get(methodBase, getValue, hours, param);
		}
		public static T CacheDay<T>(this MethodBase methodBase, Func<T> getValue) where T:class {
			return Get(methodBase, getValue, 24);
		}

		public static string CacheInFileDay(this MethodBase methodBase, Func<string> getValue) {
			return System.IO.File.ReadAllText(CacheDay(methodBase, () => {
				var cacheKey = GetCacheKey(methodBase);
		        var file = "c:\\temp\\sitecache\\{0}.txt".FormatWith(cacheKey);
				var data = getValue();
				System.IO.File.WriteAllText(file, data);
				return file;
			}));  
		}

		public static T Get<T>(MethodBase methodBase, Func<T> getValue, int hours = 1, object param = null) where T : class {
			var cacheKey = GetCacheKey(methodBase);
			if (param != null) {
				cacheKey += "-" + param;
			}
			return Get(cacheKey, getValue, hours);
		}

		public static string GetCacheKey(MethodBase methodBase) {
			return methodBase.DeclaringType.Name + methodBase.Name;
		}

		public static T Get<T>(string cacheKey, Func<T> getValue, int hours = 1)
			where T : class {
			var cache = HttpRuntime.Cache;

			var cacheValue = cache[cacheKey] as T;
			if (cacheValue == null) {
				var currentLock = _locks.GetOrAdd(cacheKey, new object());
				lock (currentLock) {
					cacheValue = cache[cacheKey] as T;
					if (cacheValue == null) {
						T value = null;
						try {
							value = getValue();
						}
						catch (Exception e) {
							Logger.Exception(e, "cache error " + cacheKey);
							value = typeof (T).Create().As<T>();
						}
						cache.Insert(
							cacheKey, value, null,
							DateTime.Now.AddHours(hours),
							System.Web.Caching.Cache.NoSlidingExpiration);
						cacheValue = value;
					}
				}
			}
			return cacheValue;
		}

		public static void Clean<T>(Expression<Action<T>> method) {
			var key = GetCacheKey(method);
			HttpRuntime.Cache.Remove(key);
		}

		public static string GetCacheKey<T>(Expression<Action<T>> method) {
			return GetCacheKey(ExpressionUtils.GetMethodInfo(method));
		}

		/*	public static List<T> LinqCache<T>(this IQueryable<T> query) where T : class {
			string cacheKey = query.ToString();


			var cache = (List<T>) HttpContext.Current.Cache.Get(cacheKey);
			if (cache == null) {
				cache = query.ToList();
				var tableName = LinqToSqlUtils.GetTableName(typeof (T));
				var context = ((Table<T>) query).Context;
				var dependency = new SqlCacheDependency(context.Connection.Database,
					tableName);

				HttpContext.Current.Cache.Insert(cacheKey, cache, dependency);
			}
			return cache;
		}*/
	}
}