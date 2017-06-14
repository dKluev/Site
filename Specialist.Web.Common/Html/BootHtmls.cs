using System;
using System.Collections.Generic;
using System.Web;
using SimpleUtils.FluentHtml.Tags;
using System.Linq;
using SimpleUtils.Util;
using SimpleUtils.Utils;

namespace Specialist.Web.Common.Html {
	public class BootHtmls {
		public static TagDiv Panel(object title, object content) {

			return H.Div("panel panel-default")[
				H.Div("panel-heading")[title],
				H.Div("panel-body")[content]];
		}

		public static TagA IconBtn(string href, string icon) {
			return H.Anchor(href, 
				H.span.Class("glyphicon glyphicon-" + icon))
				.Class("btn btn-default");
		}

		public static TagOl Bread(TagA link) {
			return H.ol[H.li[link]].Class("breadcrumb");
		}

		public static TagDiv ShowMessage() {
			 var message = HttpContext.Current.Session[Htmls.ShowMessageKey];
             HttpContext.Current.Session[Htmls.ShowMessageKey] = null;
			if (message != null) {
				return H.div[message].Class("alert alert-success");
			}
			return null;

		}

		public static TagSpan Icon(string name) {
			return H.span.Class("glyphicon glyphicon-" + name);
		}
		public static TagDiv Warning(string text) {
			return H.Div("alert alert-warning")[text];
		}
		public static TagDiv Danger(string text) {
			return H.Div("alert alert-danger")[text];
		}
		public static TagDiv Info(string text) {
			return H.Div("alert alert-info")[text];
		}
		public static TagDiv Success(string text) {
			return H.Div("alert alert-success")[text];
		}

		public static TagDiv Table(params object[] x) {
			return H.Div("table-responsive")[H.table[x].Class("extable table table-striped table-hover table-bordered")];
		}
		public static TagTable TableSmall(params object[] x) {
			return H.table[x].Style("width:auto;").Class("extable table table-bordered");
		}

		public static TagDiv Tabs(List<Tuple<string, object>> items) {
			var head = H.ul[items.Select((x,i) => H.li[H.Anchor("#" + Linguistics.UrlTranslite(x.Item1), x.Item1)
					.Data("toggle","tab")].Class(i == 0 ? "active" : ""))].Class("nav nav-tabs");
			var body = H.Div("tab-content")[items.Select((x,i) => H.Div("tab-pane" + (i == 0 ? " active" : "")).Id(Linguistics.UrlTranslite(x.Item1))[x.Item2])];
			return H.div[head, body];
		}

		public static TagDiv Collapse(List<Tuple<string, object>> items) {
			return H.Div("panel-group")[items.Where(x => x != null).Select(x => {
				var id = "collapse" + Guid.NewGuid();
				var head =
					H.Div("panel-heading")[H.Anchor("#" + id, x.Item1).Data("parent", "#accordion").Data("toggle", "collapse")];
				var body = H.Div("panel-collapse collapse")[H.Div("panel-body")[x.Item2]].Id(id);
				return H.Div("panel panel-default")[head, body];
			} )];
		}

		public static TagInput SubmitButton(string text) {
			return H.Submit(text).Class("btn btn-primary");
		}
	}
}