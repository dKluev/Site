using System.Collections.Generic;
using SimpleUtils.Util;

namespace Specialist.Web.Cms.Core.ViewModel {
	public class JsTableVM {

		public string Id { get { return "table-" + Linguistics.UrlTranslite(Title); } }

		public string Title { get; set; }

		public Dictionary<string, string > Columns { get; set; }

		public IEnumerable<IEnumerable<object>> Rows { get; set; }

		public JsTableVM() {
			Columns = new Dictionary<string, string>();
			Rows = new List<List<object>>();
		}
	}
}