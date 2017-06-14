using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Extension;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Common.Site {
	public class CommonSiteHtmls: H {
		public static TagDiv Carousel(IEnumerable<object> tags, bool fitWidth = false, bool autoPlay = false, bool hideAll = false) {
			var @class = "block_carousel";
			var isCarousel = tags.Count() > 1;
			if (isCarousel)
				@class += " carousel-control";
			if (fitWidth)
				@class += " fit-width";
			if (autoPlay)
				@class += " auto-play";

/*
			var items = fitWidth
				? tags.Select(x => span.Class("carousel-item")[x]).Cast<object>()
				: tags.Select(x => Div("carousel-item")[x]);
*/
			return Div(@class)[
				tags.If(isCarousel, t => l(
					Div("arrow_l png"),
					Div("arrow_r png"))),
					tags.Select((x,i) => Div("carousel-item")[x]
						.Style(i == 0 || !hideAll ? "" : "display:none;"))
				];
		}
	}
}