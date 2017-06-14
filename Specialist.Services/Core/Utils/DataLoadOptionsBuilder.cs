using System;
using System.Data.Linq;
using System.Linq.Expressions;

namespace Specialist.Web.Core.Utils {
	public class DataLoadOptionsBuilder<T> {
		public DataLoadOptions Options { get; set; }

		public DataLoadOptionsBuilder(Action<DataLoadOptionsBuilder<T>> action) {
			Options = new DataLoadOptions();
			action(this);
		}

		public DataLoadOptionsBuilder<T> And<K>(params Expression<Func<K,object>>[] selectors) {
			foreach (var selector in selectors) {
				Options.LoadWith(selector);
			}
			return this;
		}

		public DataLoadOptionsBuilder<T> Load(params Expression<Func<T,object>>[] selectors) {
			foreach (var selector in selectors) {
				Options.LoadWith(selector);
			}
			return this;
		}
	}
}