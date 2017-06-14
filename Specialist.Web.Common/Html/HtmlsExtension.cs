using System.Drawing;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;

namespace Specialist.Web.Common.Html {
	public static class HtmlsExtension {
		public static TagImg Size(this TagImg tag, object width, object height) {
			return tag.Width(width).Height(height);
		}

		public static string GetHref(this TagA tag) {
			var attr = tag.Attribute("href");
			if (attr == null) return string.Empty;
			return attr.Value;
		}
		public static string ToFbLink(this TagA tag) {
			tag.AbsoluteHref();
			var href = tag.GetHref();
			var text = tag.Value;
			return text + " " + href;
		}

		public static string GetSrc(this TagImg tag) {
			var attr = tag.Attribute("src");
			if (attr == null) return string.Empty;
			return attr.Value;
		}

		public static TagA AbsoluteHref(this TagA tag) {
			var href = tag.Attribute("href").GetOrDefault(x => x.Value);
			tag.SetAttributeValue("href", CommonConst.SiteRoot + href);
			return tag;
		}
		public static TagA OpenInUiDialog(this TagA tag) {
			return tag.Class("not-link open-in-uidialog");
		}
		public static TagA OpenInDialog(this TagA tag) {
			return tag.Class("not-link open-in-dialog");
		}
		public static TagA Confirm(this TagA tag, string text) {
			tag.Onclick("if(confirm('{0}')) return true; else return false;"
				.FormatWith(text));
			return tag;
		}

		public static TagInput SetChecked(this TagInput tag, bool check) {
			if(check)
				tag.Checked("checked");
			return tag;
		}
		public static TagOption SetSelected(this TagOption tag, bool check) {
			if(check)
				tag.Selected("selected");
			return tag;
		}
//		public static taginput setdisabled(this taginput tag, bool disable) {
//			if(disable)
//				tag.disabled("disabled");
//			return tag;
//		}
	}
}