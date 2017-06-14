using System.Collections.Generic;
using System.Linq;

namespace Specialist.Web.Pages {
	public class Select2VM {
		public class Select2Item {
			public object id { get; set; }
			public string text { get; set; }
			public Select2Item(object id, string text) {
				this.id = id;
				this.text = text;
			}
		}

		public static Select2Item Item(object id, string text) {
			return new Select2Item(id,text);
		}

		public bool more { get; set; }

		public List<Select2Item> results { get; set; }

		public Select2VM(IEnumerable<Select2Item> results, bool more = false) {
			this.more = more;
			this.results = results.ToList();
		}
	}
}