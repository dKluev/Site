using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Extension;

namespace Specialist.Entities.Utils {
	public static class _ {
		
		public static List<T> List<T>(params T[] items) {
			return new List<T>(items);
		}
		public static HashSet<T> HashSet<T>(params T[] items) {
			return new HashSet<T>(items);
		}

		public static List<int> Range(int start, int count) {
			return Enumerable.Range(start, count).ToList();
		} 

		public static Dictionary<T, K> Dict<T, K>(T key, K value) {
			return new Dictionary<T,K>().FluentUpdate(d => d.Add(key, value));
		}
		
	}
}