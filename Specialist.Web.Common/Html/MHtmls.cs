using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;

namespace Specialist.Web.Common.Html {
	public static class MHtmls {
		public static string MainList(IEnumerable<object> list) {
			if(!list.Any())
				return string.Empty;
			return "<ul class='mainlist'><li>"
				+ list.Select(x => x.NotNullString()).JoinWith("</li><li>") +
				"</li></ul>";
		}

		public static TagForm PayForm(string url, NameValueCollection data, string name,
			string paymentType) {
			if (data.Count == 0) return null;
			var payForm = H.Form(url).Method("post")[
				data.AllKeys.Select(x => H.Hidden(x, data[x]))];
			return payForm[H.Submit(name).Id(paymentType).Class("busketsubmit")];
		}

		public static TagDiv Back(TagA link) {
			var text = link.FirstNode.ToString();
			link.RemoveNodes();
			link.Add(H.span.Class("signback")["< "]);
			link.Add(H.span.Class("textback")[text]);
			return H.Div("back")[link];
		}

		public static TagForm ButtonLink(string url, string name) {
			return H.Form(url).Method("get")[H.Submit(name).Class("busketsubmit")];
		}

		public static TagLi IndexMenu(string iconNumber, TagA link, bool hideArrow = false) {
			var text = " " + link.FirstNode.ToString();
			link.RemoveNodes();
			link.Add(Images.Mobile.Main("icon_{0}.png".FormatWith(iconNumber)).Class("icons"));
			link.Add(text);
			if(!hideArrow)
				link.Add(Arrow());
			return H.li.Class("mainlinks")[link];
		}

		public static TagDiv LongList(params object[] x) {
			return H.Div("longlist").Id("content")[x];
		}

		public static string MainList(params object[] list) {
			return MainList(list.AsEnumerable());

		}
		public static TagH2 Title(string title) {
			return H.h2[title];
		}
		public static TagSpan Arrow() {
			return H.span.Class("arrow1");
		}
	}
}