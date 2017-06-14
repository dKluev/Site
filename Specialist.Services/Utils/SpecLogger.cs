using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Services.Utils {
	public static class SpecLogger {
	    public const string JsErrorKey = "JsError";
	    public const string NotFoundKey = "NotFound";
		private static List<string> keys = _.List(JsErrorKey, NotFoundKey); 
		static Dictionary<string, string> files;
		static Dictionary<string, ConcurrentBag<string>> caches;

		static SpecLogger() {
			files = keys.ToDictionary(x => x, x => HostingEnvironment.MapPath("~/temp/{0}log.txt".FormatWith(x)));
			caches = keys.ToDictionary(x => x, x => new ConcurrentBag<string>());
		}

		public static void JsError(string msg) {
			Log(JsErrorKey, msg);
		}
		public static void NotFound(string msg) {
			Log(NotFoundKey, msg);
		}

	    static void Log(string key, string msg) {
/*
		    var cache = caches[key];
			cache.Add(msg);
		    if (cache.Count >= 10) {
			    var errors = cache.ToArray();
				caches[key] = new ConcurrentBag<string>();
			    var file = files[key];
			    lock (file) {
					File.AppendAllLines(file, errors);
			    }
		    }
*/
	    }


	}
}