using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Caching;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Specialist.Services.Interface;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Reflection.Extensions;
using Specialist.Services.Utils;
using Specialist.Web.Common.Utils;

namespace Specialist.Services.UnityInterception {
	public class CachedAttribute : HandlerAttribute {
		public int HoursDuration { get; set; }

		public bool NotBlock { get; set; }

		public CachedAttribute(int hoursDuration = 1, bool notBlock = false) {
			NotBlock = notBlock;
			HoursDuration = hoursDuration;
		}

		public override ICallHandler CreateHandler(IUnityContainer container) {
			var handler = container.Resolve<CachedHandler>();
			handler.HourDuration = HoursDuration;
			handler.NotBlock = NotBlock;
			return handler;
		}

		public class CachedHandler : ICallHandler {
			[Dependency]
			public ICachingService CachingService { get; set; }

			public int? HourDuration { get; set; }

			public bool NotBlock { get; set; }

			public static readonly ConcurrentDictionary<string,object> _locks = 
				new ConcurrentDictionary<string, object>();

			public IMethodReturn Invoke(IMethodInvocation input,
				GetNextHandlerDelegate getNext) {
				if (CachingService.Cache == null)
					return getNext()(input, getNext);
				var cacheKey = CacheUtils.GetCacheKey(input.MethodBase);

				var cacheValue = CachingService.Cache[cacheKey];
				if (cacheValue != null)
					return input.CreateMethodReturn(cacheValue);
				var currentLock = _locks.GetOrAdd(cacheKey, new object());
				lock (currentLock) {
					cacheValue = CachingService.Cache[cacheKey];
					if (cacheValue != null)
						return input.CreateMethodReturn(cacheValue);
					if(!NotBlock)
						return AddResultToCache(getNext, input, cacheKey);

					ThreadPoolEx.QueueUserWorkItemWithCatch(state => 
						AddResultToCache(getNext, input, cacheKey));
					var result = input.MethodBase.As<MethodInfo>().ReturnType.Create();
					AddToCache(result, cacheKey);
					return input.CreateMethodReturn(result);
				}
			}

			private IMethodReturn AddResultToCache(GetNextHandlerDelegate getNext, IMethodInvocation input, string cacheKey) {
				var methodReturn = getNext()(input, getNext);

				if (methodReturn.Exception == null) {
					var value = methodReturn.ReturnValue;
					if (value is IQueryable) {
						value =
							Services.CachingService.LoadQueryable(methodReturn.ReturnValue);
					}

					AddToCache(value, cacheKey);
				}
				return methodReturn;
			}

			private void AddToCache(object value, string cacheKey) {
				CachingService.Cache.Insert(
					cacheKey, value, null,
					DateTime.Now.AddHours(HourDuration ?? 1),
					Cache.NoSlidingExpiration);
			}

			public int Order { get; set; }
		}
	}
}