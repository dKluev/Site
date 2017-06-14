using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SimpleUtils;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc;
using SimpleUtils.Extension;
using System.Linq;
using Specialist.Web.Common.Extension;
using Specialist.Web.Const;
using Group = Specialist.Entities.Context.Group;

namespace Specialist.Web.Common.Html
{
	public static class Htmls2 {
		public static string NoIndexBegin = "<!--noindex-->";
		public static string NoIndexEnd = "<!--/noindex-->";


		 public static string Rss(object link) {
			return
				@"<div class=""mblock_gray_l png"">
			<div class=""mblock_gray_r png"">
				<div class=""mblock_gray_c"">
					 {0}
					</div>
			</div>
		</div>".FormatWith(link);
		}


		public static string Submit(string name) {
			return
				@"<p class='submit_p'>" + 
				SubmitImageButton(name) + "</p>";
		}

		public static TagInput SubmitImageButton(string name) {
			return H.InputImage(Urls.Button(name)).Style("height:24px;");
		}

		public static string HeadRssLink(string href, string title) {
			return H.link.Rel("alternate").Type("application/rss+xml")
				.Title(title).Href(href).ToString();
		}


		public static string Chamfered() {
			return
				@"<div class=""block_chamfered_tl png""></div>
				<div class=""block_chamfered_tr png""></div>
				<div class=""block_chamfered_br png""></div>
				<div class=""block_chamfered_bl png""></div>";
		}

		public static string BorderBegin(object title = null, 
			bool gray = false, bool margin = false, bool notContent = false) {
			return "<div class='block_chamfered{1}{0}'>"
				.FormatWith(margin ? " margin" : "", notContent ? "" : "_content")+ 
				ChamBegin(gray) +
				(title.NotNullString().IsEmpty() 
					? "" :  H.h2.Class("h2_grey")[title].ToString());
		}

		public static string BorderEnd() {
			return BlockEnd() + "</div>";
		}

		public static string Cham(string text) {
			return ChamBegin() + text + BlockEnd();
		}

		public static string ChamBegin(bool gray = false) {
			return
				@"<div class=""block_chamfered_t " + 
				(gray ?  " block_gray-bg" : "") + @""">
					<div class=""block_chamfered_b"">
					<div class=""block_chamfered_r"">
					<div class=""block_chamfered_l"">"
				
				+ Chamfered();
		}

		public static string BlockEnd() {
			return "</div></div></div></div>";
		}

		public static string FreshBegin() {
			return 
				@"<div class=""block fresh"">
	<div class=""fresh_l"">
		<div class=""fresh_r"">
			<div class=""fresh_b"">
				<div class=""fresh_c_bl"">
				</div>
				<div class=""fresh_c_br"">
				</div>";
		}


		public static string Menu2(object text, string color = "blue") {
			return
				@"
				<div class=""h2_menu2"">
					<div class=""h2_menu2_t_{1}"">
					</div>
					<h2 class=""h2_menu2_b_{1}"">
						{0}</h2>
				</div>".FormatWith(text, color);
		}

		private static void Write(string s)
		{
			HttpContext.Current.Response.Write(s);
		}

		private static bool IsIe6() {
		
			var browser = 
				HttpContext.Current.Request.Browser;
			return browser.Browser == "IE" && browser.MajorVersion == 6;
		}

		public static bool IsIe() {
			return HttpContext.Current.Request.Browser.Browser == "IE";
		}


		public static string Tabs(IEnumerable<string> names, object [] contents,
			bool useHash = false, int tabContentType = 0, int? activeTab = null) {

			var isScript = !activeTab.HasValue;
			var result = 
				"<div class='block_chamfered_content'>" +
				ChamBegin() + "<div class='{1} {0}'>"
				.FormatWith(useHash ? "use-hash" : "", isScript ? "tabs-control" : "");
			var tabs = "<ul class='bookmarks'>";
			var nameList = names.ToList();
			for (int i = 0; i < nameList.Count; i++) {
				var name = nameList[i];
				if (name == null)
					continue;
					string link;

				if(isScript) {
				var translit = Linguistics.UrlTranslite(name);

					link = "<a href='#{0}' rel='tab-{0}'>{1}</a>".FormatWith(translit,
						name);
				}
				else {
					link = name;
				}

				var tab = "<li class='{0}'>{1}</li>"
					.FormatWith(!isScript && i == activeTab ? "active" : "", link);
				tabs += tab;
			}
			tabs += "</ul>";
			result += tabs;
			names.ForEach((name, i) =>
			{
				var translit = Linguistics.UrlTranslite(name);
				result += "<div class='tab-{0} tab_content{2}' {1}>"
					.FormatWith(translit, Htmls.DisplayNone(!IsIe6() && isScript),
					tabContentType == 0 ? "" : tabContentType.ToString());
				if(i < contents.Length ) {
				result += contents[i];
				}
				result += "</div>";
			});
		
			return result + "</div>" + BlockEnd() + "</div>";
		}

		public static object MarkArrow(IEnumerable<object> objects) {
			if(!objects.Any()) return string.Empty;
			return H.ul.Class("mark_arrow")[objects.Select(o => H.li[o])];
		}

		public static void FormSection(Action action) {
			FormSection(string.Empty, null, action);
		}

		public static void FormSection(string title, Action action) {
			FormSection(string.Empty, title, action);
		}

		public static void FormSection(string tableFormPostfix, string title, Action action) {
			var begin = string.Empty;


			var showBorder = !(title.IsEmpty() || Regex.IsMatch(title, @"^\s+$"));
			if (showBorder)
				begin = BorderBegin(title, margin:true) + "<div class='tab_content2'>";
			begin += "<table class='table_form" + tableFormPostfix + "'>";
			Write(begin);
			action();
			var end = "</table>";
			if(showBorder)
				end += "</div>" + BorderEnd();
			Write(end);

		}
		public static TagA YouTube(Video video, int width) {
            
			return YouTube(video.YouTubeID, video.Name, width, video.AvailableEveryOne);
		}

		public static TagA YouTube(string youTubeId, string name, int width = 151, bool? availableEveryOne = null) {

            bool l_availableEveryOne = availableEveryOne != null ? Convert.ToBoolean(availableEveryOne) : false;

            var result = H.Anchor(
                l_availableEveryOne ? "https://www.youtube.com/watch?v=" + youTubeId :
                BaseUrl() + "SimpleReg/Registration",
                Htmls.YouTubeImage(youTubeId)
                    .Size(width, null)
                    .Class(l_availableEveryOne ? youTubeId : ""))
                        .Class(l_availableEveryOne ? "fancy-video" : "").Title(name);

            return result;
		}

        public static string BaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/")
                appUrl = "/" + appUrl;

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }

        public static string Vimeo(string videoId) {
			return
				"<iframe src='https://player.vimeo.com/video/{0}?badge=0&byline=0&portrait=0&title=0' width='640' height='480' frameborder='0' webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>"
					.FormatWith(videoId);
		}



//		public static string Vimeo(string albumId) {
//			return
//				"<iframe src='//player.vimeo.com/hubnut/album/{0}?color=44bbff&amp;background=000000&amp;slideshow=1&amp;video_title=1&amp;video_byline=1' width='400' height='300' frameborder='0' webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>"
//					.FormatWith(albumId);
//		}

		public static string AskTimetable(string courseTC, 
			string content = "”точнить расписание") {
			return "«воните по тел. " + CommonTexts.Phone;
			//			return H.a.Class("ask-timetable-link not-link").Rel(courseTC)
			//				.Href("#")[content];
		}

		public static string DiscountPrice(short? discount, decimal? price) {
			if(discount.HasValue && price > 0) {
				var priceWithDiscount = 
					OrderDetail.FloorToFifty(
					price.Value*(decimal) (1.0 - discount/100.0));
				return OldNewPrice(price, priceWithDiscount);
			}
			return price.MoneyString();
		}
		public static string OldNewPrice(decimal? oldPrice, decimal? newPrice) {
			if(oldPrice.HasValue && oldPrice > newPrice) {
				return H.l(H.span.Style("text-decoration: line-through;")[
					oldPrice.MoneyString()],
					H.span.Class("discount_color")[
						newPrice.MoneyString()]).ToString();
			}
			return newPrice.MoneyString();
		}

		public static string DiscountText(object discount, bool big = false) {
			return H.span[discount].Class("discount_color")
				.Style(big ? "font-size:12px;" : "").ToString();
		}
		public static string Discount(Group g, bool withDash = false) {
			if(g == null || g.Discount == null)
				return string.Empty;
			var result = string.Empty;
			var discountClass = "discount_color";
			if(CommonConst.IsMobile)
				discountClass = "discount";
			var span = H.span.Class(discountClass)[g.Discount + "%"];
			result = span.ToString();
			if(!g.IsOpenLearning && g.DateBeg.HasValue 
				&& g.DateBeg.Value.AddDays(-6) <= DateTime.Today) {
				var before = g.DateBeg.Value.AddDays(-4).Date;
				if(before < DateTime.Today) before = DateTime.Today;

				var sufix = before == DateTime.Today ? " на " : " до ";
				
				var dateText = "скидка" + sufix + before.DefaultString();
				result += (withDash ? " " : "<br/>") + dateText;
				if(CommonConst.IsMobile)
					span.Add(" " + dateText);
			}else if(CommonConst.IsMobile) {
				span.Add(" скидка");
			}
			if(CommonConst.IsMobile)
				return span.ToString();

			return  (withDash ? "Ч " : "") + result;
		}


		public static string JQueryDatePicker = H.l(
			H.JavaScript().Src("https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"),
			H.JavaScript().Src("https://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/i18n/jquery.ui.datepicker-ru.js"),
			H.JQuery(@"
	$.datepicker.setDefaults($.datepicker.regional['ru']);
	$('input[name=""User.BirthDate""').datepicker({ dateFormat: 'dd.mm.yy', changeMonth: true,  changeYear: true, yearRange: '-80:-10', showAnim: 'fadeIn' });")
			).ToString();




	}
}