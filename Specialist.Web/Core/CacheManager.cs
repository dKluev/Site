using System.Collections.Concurrent;

namespace Specialist.Web.Core {
	public static class CacheManager {

		public const string Poll = "PollCacheCM";
		public const string Announce = "AnnounceCacheCM";
		public static readonly ConcurrentDictionary<string,int> CacheKeys = 
				new ConcurrentDictionary<string, int>();
		public static void Update(string key) {
			if (CacheKeys.ContainsKey(key)) {
				CacheKeys.AddOrUpdate(key, 1, (_, value) => value + 1);
			}
		}
		public static int Get(string key) {
			return CacheKeys[key];
		}
		static CacheManager() {
			CacheKeys.TryAdd(Poll, 0);
			CacheKeys.TryAdd(Announce, 0);
		}
	}
}