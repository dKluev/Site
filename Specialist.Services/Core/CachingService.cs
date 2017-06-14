using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Interface;
using Specialist.Services.Utils;

namespace Specialist.Services
{
    public class CachingService : ICachingService
    {
        static readonly MethodInfo CastMethod = typeof(Enumerable).GetMethod("Cast");
        static readonly MethodInfo ToListMethod = typeof(Enumerable).GetMethod("ToList");
        /*static readonly MethodInfo AsQueryableMethod = typeof(Queryable)
            .GetMethod("AsQueryable", new [] {typeof(IEnumerable<>)});*/

    	public CachingService() {
            if(HttpContext.Current != null)
               Cache = HttpContext.Current.Cache;
    	}

    	static readonly Expression<Func<IQueryable<object>, IQueryable<object>>> exp
            = x => x.AsQueryable();

        public static object LoadQueryable(object result)
        {
            var resultType = result.GetType();
            var targetType = resultType.GetGenericArguments().First();
            var value = ToListMethod.MakeGenericMethod(new[] { targetType })
              .Invoke(null, new[] { result });
            value = AsQueryableMethod.MakeGenericMethod(new[] { targetType })
                .Invoke(null, new[] { value });
            return value;
        }

        static readonly MethodInfo AsQueryableMethod = 
            ((MethodCallExpression)exp.Body).Method.GetGenericMethodDefinition();


        private static object _lock = new object();
    	public Cache Cache { get; set; }
      /*  public object GetFromCache(string key)
        {
            lock(_lock)
            {
                Cache
            }

        }*/

        [Dependency]
        public IUnityContainer UnityContainer { get; set; }

        private static List<string> _inQueue = new List<string>();

        private static bool AddInQueue(string cacheKey)
        {
            var added = false;
            lock (_inQueue)
            {
                if (!_inQueue.Contains(cacheKey))
                {
                    _inQueue.Add(cacheKey);
                    added = true;
                }
            }
            return added;
        }

        private static void RemoveFromQueue(string cacheKey)
        {
            lock (_inQueue)
            {
                if (_inQueue.Contains(cacheKey))
                    _inQueue.Remove(cacheKey);
            }
        }


        public void SetCache(string cacheKey, MethodBase methodBase)
        {
            if (!AddInQueue(cacheKey))
                return;
            var cache = Cache;
            ThreadPoolEx.QueueUserWorkItemWithCatch(
                state =>
                {
                    var service = UnityContainer.Resolve(methodBase.DeclaringType);
                    var result = methodBase.Invoke(service, new object[0]);
                    var resultType = result.GetType();
                    if(resultType.IsGenericType && typeof(IQueryable<>)
                        .MakeGenericType(resultType.GetGenericArguments())
                        .IsAssignableFrom(resultType)
                        )
                    {
                        var value = LoadQueryable(result);
                        cache.Insert(cacheKey, value,
                            null, DateTime.Now.AddHours(1),Cache.NoSlidingExpiration);
                    }
                    RemoveFromQueue(cacheKey);
                    
                });

        }



    }
}