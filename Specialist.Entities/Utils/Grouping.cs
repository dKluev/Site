using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Specialist.Entities.Utils {
	public class Grouping<TKey,TElement>: List<TElement>, IGrouping<TKey, TElement> {

		public TKey Key { get; set; }

		public Grouping(TKey key, IEnumerable<TElement> collection) : base(collection) {
			Key = key;
		}
	}

	public static class Grouping
	{

		public static Grouping<TKey, TElement> New<TKey, TElement>(TKey key, IEnumerable<TElement> items) {
			return new Grouping<TKey, TElement>(key, items);
		}
	}
}