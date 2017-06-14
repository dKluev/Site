using SimpleUtils.FluentHtml.Tags;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Common.Cdn {
	public class CdnFile {
		public string Url { get; set; }
		public string Path { get; set; }
		public CdnFile(string url, string path) {
			Url = url;
			Path = path;
		}

		public TagA Anchor(object content) {
			return H.Anchor(Url, content);
		}
		public TagImg Img() {
			return H.Img(Url);
		}
	}
}