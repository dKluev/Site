using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Hosting;
using HtmlAgilityPlus;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Utils;
using Specialist.Web.Cms.Util;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Cms.Root.YandexDirect.Logic {
	public class YandexParser {
		private const string anonym = "http://anonymouse.org/cgi-bin/anon-www.cgi/";
		private const string yandex = "http://yandex.ru/yandsearch?text={0}&lr=213";

		private const string google =
			"http://www.google.ru/search?sclient=psy-ab&hl=ru&newwindow=1&site=&source=hp&q={0}";

		public static List<SearchAbsPositions> GetPositionsFromYandex(DateTime date) {
			return Phrases.ForCompetitors.Select((x, i) => {
				Thread.Sleep(60000);
				return new SearchAbsPositions(date, i + 1, GetSpecialDirectSites(x));
			}).ToList();
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void UpdatePositions() {
			var date = DateTime.Now;
			var savedPositions = GetSavedPositions();
			var newPositions = GetPositionsFromYandex(date);
			savedPositions.InsertRange(0, newPositions);
			Serializer.Serialize(GetDataFilePath(), savedPositions);
		}

		public static List<SearchAbsPositions> GetSavedPositions() {
			var filePath = GetDataFilePath();
			if(!File.Exists(filePath))
				return new List<SearchAbsPositions>();
			return Serializer.Deserialize<List<SearchAbsPositions>>(filePath);
		}


		private static string GetDataFilePath() {
			return HostingEnvironment.MapPath("~/temp/searchabspositions.bin");
		}

		public static string YandexCompetitorsFile() {
			return HostingEnvironment.MapPath("~/temp/yandexcompetitors.bin");
		}

		public static string GetSpecialDirectSites(string phrase) {
			var sq = new SharpQuery(GetHtml(yandex.FormatWith(phrase)));
			var sites = GetSites(sq);

			return sites.JoinWith(",");
		}

		private static List<string> GetSites(SharpQuery sq) {
			return sq.Find("div.b-spec-adv div.b-serp-url span.b-serp-url__item").Select(x => x.InnerText)
				.Where(x => !x.Contains("Адрес") && !x.Contains("Москва")).ToList();
		}

		public static SeCompetitor GetYandexCompetitors(int phraseId) {
			var phrase = Phrases.YandexCompetitors[phraseId];
			var yandexPage = GetHtml(yandex.FormatWith(phrase)).Remove(anonym);
			var sq = new SharpQuery(yandexPage);
			var directText = DirectText(sq);
			var onSearchText = sq.Find("div.b-body-items a.b-serp-item__title-link")
				.Select(x => x.OuterHtml).ToList();

			var googlePage = GetHtml(google.FormatWith(phrase));
			sq = new SharpQuery(googlePage);
			var adWords = AdWordsText(sq);
			var googleSearch = sq.Find("h3.r a.l")
				.Select(x => x.OuterHtml).ToList();
			return new SeCompetitor(DateTime.Today, phraseId, 
				directText,
				onSearchText,
				adWords,
				googleSearch
				) ;
		}

		private static List<string> DirectText(SharpQuery sq) {
			var directText1 = sq.Find("div.b-spec-adv a.b-serp-item__title-link")
				.Select(x => x.OuterHtml);
			var directText2 = sq.Find("div.b-spec-adv div.b-serp-item__text")
				.Select(x => x.InnerHtml);
			var directText3 = GetSites(sq);
			var directText =
				directText1.Zip(directText2.Zip(directText3, (x, y) => x + "<br/>" + y), (x, y) => x + "<br/>" + y)
					.ToList();
			return directText;
		}

		private static string RepaceHref(string text) {
			var href = Regex.Match(text, "adurl=(.*?)[\"&]").Groups[1].Value;
			var inner = Regex.Match(text, "<a.*?>(.*?)</a>").Groups[1].Value;
			return H.Anchor(href, inner).ToString();

		}
		private static List<string> AdWordsText(SharpQuery sq) {
			var directText1 = sq.Find("#tads h3 a")
				.Select(x => RepaceHref(x.OuterHtml));
			var directText2 = sq.Find("#tads span.ac").Select(x => x.InnerHtml);
			var directText3 = sq.Find("#tads cite").Select(x => x.InnerHtml);
			var adWords =
				directText1.Zip(directText2.Zip(directText3, (x, y) => x + "<br/>" + y), (x, y) => x + "<br/>" + y)
					.ToList();
			return adWords;
		}

		public static string GetHtml(string url, Encoding encoding = null) {
			var request = (HttpWebRequest) WebRequest.Create(url);
			request.AllowAutoRedirect = true;
			request.Proxy = null;
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1";
			request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			request.Headers.Add("Accept-Charset", "utf-8");
			request.Headers.Add("Accept-Language", "en-us,en;q=0.5");
			request.Headers.Add("Accept-Language", "en-us,en;q=0.5");

			using (var response = request.GetResponse()) {
				using (var stream = response.GetResponseStream()) {
					using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8)) {
						var htmlText = reader.ReadToEnd();
						return htmlText;
					}
				}
			}
		}
		public static BinDatabase<SeCompetitor> GetYandexCompetitorDB() {
			return new BinDatabase<SeCompetitor>(YandexCompetitorsFile());
		} 
	}
}