using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SimpleUtils;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Common.Utils;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc;
using SimpleUtils.Extension;
using System.Linq;
using System.Xml.Linq;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Const;
using Specialist.Services;
using Specialist.Web.Common.Utils;
using Specialist.Web.Common.Utils.Logic;
using SpecialistTest.Web.Core.Mvc.Extensions;

namespace Specialist.Web.Common.Html
{
	public static class Htmls {

		public static List<string> NamesForHide = 
			_.List("Apple", "Google", "Microsoft Office");

		public static List<string> NamesForShow =
			_.List("Access 2010/2007",
				"Word",
				"Excel",
				"PowerPoint",
				"Outlook",
				"Microsoft® Project",
				"SharePoint");

		public const string AdditionalStyle = "AdditionalStyle";
		public const string OfferUrlKey = "OfferUrlKey";

		public static bool IsNewSite { get; set; }

		public static TagLink CssLink(string name) {
			return H.link.Attr(rel: "stylesheet", type: "text/css",
				href: "/Content/{0}.css".FormatWith(name));
		}

		public static string CartInfo =
			H.Div("div-pointer")[H.sup["?"]].Title(
				"Вы можете выбрать удобную для Вас форму обучения. В зависимости от этого меняется стоимость курса. Ваша выгода при выборе дистанционного обучения составит от 15% до 40%. ")
				.ToString(SaveOptions.DisableFormatting);

		public static bool IsNewProfile {get { return true; }}

		public static bool IsNewVersion {get { return true; }}
		public static bool IsSecond {get { return HttpContext.Current.Request.Url.AbsoluteUri.Contains("second"); }}

		public static bool IsThird {get { return HttpContext.Current.Request.Url.AbsoluteUri.Contains("third"); }}

		public static bool IsFixedHeader {get {
			return false;
		}}

		public const string ShowMessageKey = "ShowMessageKey";
		public const string ShowErrorMessageKey = "ShowErrorMessageKey";
		public static TagDiv ShowMessage() {
			 var message = HttpContext.Current.Session[ShowMessageKey];
             HttpContext.Current.Session[ShowMessageKey] = null;
			if (message != null) {
				return H.div[message]
					.Style("background-color: lightgreen; font-weight: bold; padding: 10px; color: black;");
			}
			return null;

		}

		public static TagDiv ShowErrorMessage() {
			 var message = HttpContext.Current.Session[ShowErrorMessageKey];
             HttpContext.Current.Session[ShowErrorMessageKey] = null;
			if (message != null) {
				return H.div[message]
					.Style("background-color: pink; font-weight: bold; padding: 10px; color: black;");
			}
			return null;

		}

		public static string GetShortWithShowOnClick(string str, object id) {
			if (str.IsEmpty())
				return string.Empty;
			var parts = StringUtils.RemoveTags(str).Split(' ');
			var wordCount = 30;
			if (parts.Count() > wordCount) {
				var result = parts.Take(wordCount).JoinWith(" ");
				var rest = parts.Skip(wordCount).JoinWith(" ");
				result += Htmls.ShowOnClick(id, "далее", rest);
				return result;

			}
			return str;
		}
		public static string ShowOnClick(object id, string text, string content = null) {
			var result = " <a href='#' class='show-on-click not-link' rel='{0}'>{1}</a>"
				.FormatWith(id,text);
			if(content == null)
				return result;
			result += "<span class='show-on-click-{0}' style='display:none'>{1}</span>"
				.FormatWith(id, content);
			return result;
		}

		public static TagA Tel(string tel) {
			return H.Anchor("tel:" + tel, tel);
		}


		public static string SkypeTextBlock() {
			var context = new SpecialistWebDataContext();
			var isIcqOnline = new TimeSpan(9, 30, 00) <= DateTime.Now.TimeOfDay
				   && DateTime.Now.TimeOfDay <= new TimeSpan(18, 00, 00) &&
				   !DateTime.Now.DayOfWeek.In(DayOfWeek.Saturday, DayOfWeek.Sunday);
			var icqIcon = Images.Main(isIcqOnline ? "ico_icq.png" : "icq_offline.gif");

			var skypeIcon = Images.Main(isIcqOnline ? "skype_online.png" : "skype_offline.png");
			var desc = 
				context.SimplePages.Where(x => x.SysName == SimplePages.Skype).
				Select(x => x.Description).FirstOrDefault();
			return TemplateEngine.GetText(desc, new {icqIcon, skypeIcon});
		}

		public static string TextBlock(int? pageId = null) {
			var context = new SpecialistWebDataContext();
			var url = HttpContext.Current.Request.Url.AbsolutePath;
			var desc = (pageId.HasValue 
				? context.SimplePages.Where(x => x.SimplePageID == pageId)
				: context.SimplePages.Where(x => x.UrlName == url)).
					Select(x => x.Description).FirstOrDefault();
			return desc;
		}

		public static Dictionary<int,Tuple<string, string>> AllHtmlBlocks() {
			return CacheUtils.Get(MethodBase.GetCurrentMethod(), () => {
				return new SpecialistWebDataContext().HtmlBlocks
					.Select(x => new {x.Id, x.Name, x.Description}).ToList()
					.ToDictionary(x => x.Id, x => Tuple.Create(x.Name, x.Description));
			},24);
		}

		public static string AllPaymentTypes(ViewDataDictionary data) {
			return H.p["Оплачивая  обучение вы принимаете условия ", 
				H.Anchor(data[OfferUrlKey].ToString(), "договора-оферты") 
				+ "."] + HtmlBlock(HtmlBlocks.AllPaymentTypes);
		}

		public static string HtmlBlock(int id) {
			return AllHtmlBlocks()[id].Item2;
		}
		public static string DisplayNone(bool isDisplayNone = true)
		{
			return isDisplayNone ? " style='display:none' " : "";
		}

			public const string SocialBox = "<div style='margin-bottom:5px;' id='social_groups'></div>" +
			"<div id='ok_group_widget' style='margin-bottom:5px;'></div>" + 
			"<div style='margin-bottom:5px;' id='facebook-likebox'> </div>";

		public static string LinkButton
		{
			get
			{
				return " style='border-bottom:1px dotted #185A8D;text-decoration:none;' ";
			}
		}

		public static string MetaTitle(string title) {
			return
				@"
<meta name='og:title' content='{0}'/>
<title>{0}</title>
".FormatWith(title);

		}

		public static string Title(string title)
		{
			return title.IsEmpty() ? string.Empty : title.Tag("h1");
		}

		public static string BorderBegin()
		{
			return BorderBegin(null);
		}


		public static TagBuilder FloatLeft(this TagBuilder builder)
		{
			return builder.Class("float_left");
		}

		public static TagImg FloatLeft(this TagImg tag) {
			return tag.Class("float_left");
		}

		public static void FormSection(Action action) {
			FormSection(string.Empty, null, action);
		}

		public static void FormSection(string title, Action action) {
			FormSection(string.Empty, title, action);
		}

		public static void FormSection(string tableFormPostfix, string title, Action action) {
			Htmls2.FormSection(title, action);
		}

		public static string Submit(string name) {
			return Htmls2.Submit(name);
		}

		private static void Write(string s) {
			HttpContext.Current.Response.Write(s);
		}

		public static string BorderBegin(object title)
		{
			return Htmls2.BorderBegin(title);
		}


		public static string BorderEnd
		{
			get
			{
				return Htmls2.BorderEnd();
			}
		}

		
		public static object FormWithFile {
			get {
				return new {enctype = "multipart/form-data"};
			}
		}

		public static string Form(string action, string content) {
			return string.Format(
				"<form action={0} method=\"post\">{1}</form>",
				action.Quote(), content);
		}

		public static TagForm DeleteButton(string action) {
			return H.Form(action)["<input onclick=\"return confirmDialog();\""+
				" src='" + Urls.Main("del.gif") + "' title='Удалить'  type=\"image\">"]
				.Style("display:inline;");
		}

		public static TagImg YouTubeImage(string youTubeId) {
			return
				H.Img("https://img.youtube.com/vi/" + youTubeId +
					"/0.jpg");
		}

		public static string DefaultList(IEnumerable<string> list) {
			if(!list.Any())
				return string.Empty;
			return "<ul class='defaultUl'><li>"
				+ list.JoinWith("</li><li>") +
				"</li></ul>";
		}

		public const string BottomScriptKey = "BottomScriptKey";

		public static string AddThis(this HtmlHelper html, bool newsSubscribe = false) {
			SocialScripts();	
			return SocialLikes() + H.br + SocialButtons();

		}

		public static string SocialLikes() {
			return H.div.Id("social-buttons").Style("padding-top:10px;").ToString();
		}
		public static string SocialButtons() {
			return H.Div("share42init").FluentUpdate(x => 
					x.SetAttributeValue("data-path","/scripts/socials/")).ToString();
		}

		public static void SocialScripts() {
			HttpContext.Current.Session[BottomScriptKey] = H.JavaScript()
				.Src("/scripts/socials/buttons.min.js?v=8").ToString();
		}

		public static TagDiv CannotAddMessageToClub =
			H.Div("attention")["К сожалению, Вы не можете добавлять сообщения в клубе выпускников"];

	
	/*	public static string AddThis(this HtmlHelper html, bool newsSubscribe = false) {
			

			HttpContext.Current.Session[BottomScriptKey] = @"
  
	<script type='text/javascript' src='http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4c3326c564775345'></script>
			
			<script type='text/javascript'>
				var addthis_config = {
					ui_language: 'ru'
				}

				$(function () {
					function addThisReady() {
						$('a.addthis_button_facebook_like iframe').width(150);
					}
					if(typeof addthis != 'undefined')
						addthis.addEventListener('addthis.ready', addThisReady);

				});

  
				</script>
							";
			return @"
				<div class='addthis_toolbox addthis_default_style'>
					<a class='addthis_button_vk'></a>
					<a class='addthis_button_twitter'></a>
					<a class='addthis_button_google_plusone'></a>
					<a class='addthis_button_facebook_like'></a>
				</div>";
		}*/

		public static string CommonTitle(string name)
		{
			return "Курсы и вебинары {0} в Специалисте".FormatWith(name);
		}

		public static string ComplexDirection(string geoLocation, string title = "Как добраться на автомобиле") {
			return H.Anchor("http://maps.yandex.ru/?rt=~{0}&ll={0}"
				.FormatWith(geoLocation),title).Id("complex-direction-link").ToString();
		}

		public static object RegBlock() {
			return CouponUtils.RegistrationIsActive ? CdnFiles.Images.ImageMainCoupon("registration.png") : null;
		}

		public static object Mapster = 
				H.JavaScript().Src("/scripts/views/course/jquery.imagemapster.min.js").ToString() +
				H.JQuery(
					"$('#guide-img').mapster( {fill:false, stroke:true, strokeColor: '7FB3C8', strokeWidth: 2, clickNavigate: true});");

		public static object GroupSort(string title, string url) {
			return H.div[H.h3[title,
				H.span["Сортировать:",
					H.select[GroupListSortTypes.All.Select(x => H.option[x.Item2].Value(x.Item1))]
					.Id("group-sort-type-select").Data("url", url).Style("float:right")].Style("float: right; font-weight: normal;")
				].Id("group-sort-list-title").Hide(),
				H.div.Id("group-sort-list"),
				H.JQuery("controls.initSortGroupList()")
				];
		}

	}
}