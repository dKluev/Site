using Specialist.Web.Core.Logic;

namespace Specialist.Web.Pages {
	public class PagePart {
		public string View { get; set; }

		public object Model { get; set; }

		public string Content { get; set; }

		public BaseView BaseView { get; set; }

		public PagePart(BaseView baseView) {
			BaseView = baseView;
			Model = ((dynamic) baseView).Model;
		}

		public PagePart(string content) {
			Content = content;
		}

		public PagePart(string view, object model) {
			View = view;
			Model = model;
		}
	}
}