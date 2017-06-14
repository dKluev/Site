using System.Collections.Generic;

namespace Specialist.Web.Pages {
	public class FormResponseVM {
		public string Errors { get; set; }

		public string Url { get; set; }

		public string Redirect { get; set; }

		public List<string> Names { get; set; }

		public FormResponseVM(){}

		public FormResponseVM(string url) {
			Url = url;
		}

		public FormResponseVM(string errors, List<string> names) {
			Errors = errors;
			Names = names;
		}
	}
}