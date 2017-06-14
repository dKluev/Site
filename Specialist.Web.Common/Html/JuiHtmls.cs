using SimpleUtils.FluentHtml.Tags;

namespace Specialist.Web.Common.Html {
	public class JuiHtmls {
		public static TagDiv Error(string text) {
			return H.Div("ui-state-error ui-corner-all").Style("width:400px;padding:10px;margin:5px 0;")[text];
		} 
	}
}