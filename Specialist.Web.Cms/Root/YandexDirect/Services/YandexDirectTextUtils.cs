using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Utils;
using Specialist.Web.Cms.Root.YandexDirect.Logic;
using Specialist.Web.Common.Html;

namespace Console {
	public class YandexDirectTextUtils {
		private const string QueryStringText = @"\?.*";

		public static string BannerLink(BannerInfo banner) {
			var url =
				"http://direct.yandex.ru/registered/main.pl?cmd=editBanner&bid={0}&cid={1}"
					.FormatWith(banner.BannerID, banner.CampaignID);
			return H.Anchor(url, banner.Title).ToString();
		}

		private static string Remove(string text, string pattern) {
			var regexp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			return regexp.Replace(text, "");
		}

		private static bool Contains(string text, string pattern) {
			var regexp = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			return regexp.IsMatch(text);
		}

		public static string AddParam(string text, string name, string value) {
			return (text.IsEmpty() ? "?" : "&") + name + "=" + value;
		}

		public static string GetQueryString(BannerInfo banner, bool isAdditionalLink = false) {
			var result = "?utm_source=yandex&utm_medium=cpc&utm_campaign=" + banner.CampaignID;
			if (isAdditionalLink)
				return result + AddParam(result, "utm_content", "yd_doplink");
			result += AddParam(result, "utm_content", banner.BannerID.ToString());
//			result += AddParam(result, "utm_term", Linguistics.UrlTranslite(banner.Title));
			return result;
		}

		public static bool UpdateHref(BannerInfo banner) {
			var oldLinks = GetHrefs(banner);
			var href = banner.Href;
			banner.Href = GetNewHref(banner, href);
			foreach (var sitelink in banner.Sitelinks) {
				sitelink.Href = GetNewHref(banner, sitelink.Href, true);
			}
			if (banner.Sitelinks.Select(x => x.Href).Distinct().Count() != banner.Sitelinks.Length) {
				banner.Sitelinks.ForEach((sitelink, i) => {
					sitelink.Href = sitelink.Href + "&x=" + i;
				});

			}
			var newHrefs = GetHrefs(banner);
			return !oldLinks.SequenceEqual(newHrefs);
		}

		private static IEnumerable<string> GetHrefs(BannerInfo banner) {
			return banner.Sitelinks.Select(x => x.Href).ToList().AddFluent(banner.Href);
		}

		private static string GetNewHref(BannerInfo banner, string href, bool isAdditionalLink = false) {
			if(href.IsEmpty())
				return href;
			return Remove(href, QueryStringText) + GetQueryString(banner, isAdditionalLink);
		}

		public static string RemoveDateAndDiscount(string text) {
			return Remove(Remove(text, DateText), DiscountText);
		}

		public static string GetDateText(DateTime date, int maxLength) {
			var month = Months.ElementAt(date.Month - 1);
			var template = date.Day == 2 ? " Ñî" : " Ñ";
			template += " " + date.Day + " {0}";
			return GetText(template, month, maxLength);
		}

		private static string GetText(string template, IEnumerable<string> variants, int maxLength) {
			foreach (var m in variants) {
				var result = template.FormatWith(m);
				if (result.Length <= maxLength)
					return result;
			}
			return string.Empty;
		}

		public static string GetDiscountText(int discount, int maxLength) {
			return GetText("{0} " + discount + "%", _.List(" Ñêèäêà", " Ñêèä"), maxLength);
		}

		public static string GetUrlName(string href) {
			if (href.IsEmpty())
				return null;
			var match = Regex.Matches(href, @"course/([^?#]*)");
			if (match.Count > 0) {
				return match[0].Groups[1].Value.ToLower();
			}
			return null;
		}

		public static readonly List<Tuple<List<string>, long, int>> Banners = _.List(
			Tuple.Create(_.List("ÒÐÊ-Á"), 2065789, 0),
			Tuple.Create(_.List("ÂÁÀ1"), 1661182, 0),
			Tuple.Create(_.List("ÝÊÑÏ1"), 1948620, 0),
			Tuple.Create(_.List("ÝÊÑÏ2-Ç"), 1948648, 0),
			Tuple.Create(_.List("ÁÊÏ1", "ÁÊÏ2"), 2028904, 0),
			Tuple.Create(_.List("ÁÊÏ1", "ÁÊÏ2"), 2029276, 0),
//			Tuple.Create(_.List("ÁÊÏ1", "ÁÊÏ2"), 2126834, 0),
			Tuple.Create(_.List("ÏÏ-Ä", "ÄÈÇÏÐÅÇ"), 2251522, 0),
			Tuple.Create(_.List("ÁÊÏ1", "ÁÊÏ2"), 2254131, 0),
			Tuple.Create(_.List("ÑÏÑÑ1", "ÑÏÑÑ2"), 2271471, 0),
			Tuple.Create(_.List("ÏÏ-Ä", "ÄÈÇÏÐÅÇ"), 2327599, 0),
			Tuple.Create(_.List("ÈÍÒ1"), 2384308, 0),
			Tuple.Create(_.List("ÀÊÑ2-1", "ÀÊÑ2-2", "ÀÊÑ-Ã"), 2431642, 0),
			Tuple.Create(_.List("ÝÊÑÏ1", "ÝÊÑÏ2-Ç", "ÝÊÑÏ3", "ÏÏ-Ä", "ÀÊÑ2-1", "ÀÊÑ2-2", "ÀÊÑ-Ã"), 2650798, 0),
			Tuple.Create(_.List("ÝÊÑÏ1", "ÝÊÑÏ2-Ç", "ÝÊÑÏ3", "ÏÏ-Ä", "ÀÊÑ2-1", "ÀÊÑ2-2", "ÀÊÑ-Ã"), 2696941, 0),
			Tuple.Create(_.List("ÒÐÊ-Á"), 3210079, 0),
			Tuple.Create(_.List("ÀËÓÊ-Â"), 3213929, 0),
			Tuple.Create(_.List("ÒÐÊ-Á"), 4624267, 0),
			Tuple.Create(_.List("ÒÐÊ-Á"), 4626028, 0),
			Tuple.Create(_.List("ÝÊÑÏ1", "ÝÊÑÏ2-Ç", "ÝÊÑÏ3"), 5047805, 0),
			Tuple.Create(_.List("ÝÊÑÏ1", "ÝÊÑÏ2-Ç", "ÝÊÑÏ3"), 12225416, 0),
			Tuple.Create(_.List("ÝÊÑÏ2-Ç", "ÝÊÑÏ3"), 14659126, 0),
			Tuple.Create(_.List("ÝÊÑÏ2-Ç"), 15059083, 0),
			//Tuple.Create(_.List("ÒÁÓÕ-È"), 1815757, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 1815762, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 1815812, 0),
			Tuple.Create(_.List("1Ñ8Á1"), 1815819, 0),
			Tuple.Create(_.List("1Ñ8Á1"), 1815830, 0), 
			Tuple.Create(_.List("1Ñ8Á1"), 1815838, 0),
			Tuple.Create(_.List("1Ñ8ÓÒ1"), 1815839, 0),
			Tuple.Create(_.List("1Ñ8ÇÓ-À"), 1815846, 0),
			Tuple.Create(
				_.List("1Ñ8ÎÏ", "1Ñ820", "1Ñ821", "1Ñ82ÇÀÏ", "1Ñ822", "1Ñ824", "1Ñ825", "1Ñ82ÀÄÌ1", "1Ñ81-Ã", "1Ñ82", "1Ñ823",
					"1ÑÑÊÂË"), 1815850, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 1925883, 0),
	//		Tuple.Create(_.List("1Ñ8Á1"), 2027947, 0),
			Tuple.Create(_.List("1Ñ8ÇÓ-À"), 2028145, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 2028399, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 2078756, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 2092423, 0),
			Tuple.Create(_.List("1Ñ8Á1"), 2101856, 0),
			Tuple.Create(_.List("1Ñ8ÓÒ1", "1Ñ8ÓÒ2"), 2101918, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 2109933, 0),
			Tuple.Create(_.List("1Ñ8Á1"), 2208865, 0),
			Tuple.Create(_.List("1Ñ8Ó", "1Ñ8ÓÐÂ"), 2286854, 0),
			Tuple.Create(
				_.List("1Ñ8ÎÏ", "1Ñ820", "1Ñ821", "1Ñ82ÇÀÏ", "1Ñ822", "1Ñ824", "1Ñ825", "1Ñ82ÀÄÌ1", "1Ñ81-Ã", "1Ñ82", "1Ñ823",
					"1ÑÑÊÂË"), 2290765, 0),
			Tuple.Create(
				_.List("1Ñ8ÎÏ", "1Ñ820", "1Ñ821", "1Ñ82ÇÀÏ", "1Ñ822", "1Ñ824", "1Ñ825", "1Ñ82ÀÄÌ1", "1Ñ81-Ã", "1Ñ82", "1Ñ823",
					"1ÑÑÊÂË"), 2290783, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 2391589, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 2396070, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 2396073, 0),
			Tuple.Create(
				_.List("1Ñ8ÎÏ", "1Ñ820", "1Ñ821", "1Ñ82ÇÀÏ", "1Ñ822", "1Ñ824", "1Ñ825", "1Ñ82ÀÄÌ1", "1Ñ81-Ã", "1Ñ82", "1Ñ823",
					"1ÑÑÊÂË"), 2421212, 0),
			Tuple.Create(_.List("ÓÏÍÀË", "1Ñ8ÓÑÍ"), 2437168, 0),
			Tuple.Create(_.List("ÍÀË-Ã"), 2437334, 0),
			Tuple.Create(_.List("1Ñ8ÇÓ-À"), 4016378, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 4333477, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 4355625, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 4501839, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 4703253, 0),
			Tuple.Create(_.List("1Ñ822", "1Ñ823", "1Ñ824", "1Ñ825"), 5102574, 0),
			Tuple.Create(_.List("1Ñ8Á1"), 5102783, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 5788465, 0),
//			Tuple.Create(_.List("ÎÑÍÁÓÕ"), 6967624, 0),
			Tuple.Create(
				_.List("1Ñ820", "1Ñ821", "1Ñ822", "1Ñ823", "1Ñ824", "1Ñ825", "1Ñ81-Ã", "1Ñ8ÇÀÏ-À", "1Ñ82", "1Ñ83-À", "1Ñ84-Á",
					"1Ñ85-Á", "1Ñ86", "1Ñ87-À"), 8670065, 0),
			Tuple.Create(_.List("ÌÑÔÎ"), 9352452, 0),
			Tuple.Create(_.List("ÓÏÐÓ×-À"), 9352578, 0),
			Tuple.Create(_.List("1Ñ8Á1"), 9481794, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È", "1Ñ8Á1"), 9940542, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 10000019, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È", "ÍÀË-Ã"), 10989637, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È", "ÍÀË-Ã"), 10991119, 0),
			Tuple.Create(_.List("ÁÞÄÆÅÒ", "1Ñ8ÁÞÄÆ"), 10991339, 0),
			Tuple.Create(_.List("1Ñ8ÓÑÍ", "ÓÏÍÀË"), 10992277, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È", "1Ñ8Á1"), 11003415, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 12147190, 0),
			Tuple.Create(_.List("ÁÓÕÑÒÐÎÉ"), 13520982, 0),
			Tuple.Create(_.List("ÊÐÎ-À"), 13557833, 0),
			Tuple.Create(_.List("1Ñ8Á1"), 15087701, 0),
			Tuple.Create(_.List("1Ñ8ÓÑÍ", "ÓÏÍÀË"), 16949509, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 21300151, 0),
			Tuple.Create(_.List("ÒÁÓÕ-È"), 25975722, 0),
			Tuple.Create(_.List("ÔËÝØ1-Á"), 1297592, 779479),
			Tuple.Create(_.List("ÎÐÈÑ-À"), 1616071, 779479),
			Tuple.Create(_.List("ÈÍÄÈÇ-Å"), 1931771, 779479),
			Tuple.Create(_.List("ÈË1-È"), 2301521, 779479),
			Tuple.Create(_.List("ÐÅÒÓØÜ-À"), 2816156, 779479),
			Tuple.Create(_.List("ÔØ1-Æ"), 2867402, 779479),
			Tuple.Create(_.List("ËÀÍÄÈÇ"), 6909842, 779479),
			Tuple.Create(_.List("ÂÌÆ-Ã"), 16844808, 779479),
			Tuple.Create(_.List("ÎÐÈÑ-À"), 18121036, 779479),
			Tuple.Create(_.List("ÈÍÄÈÇ-Å"), 18423606, 779479),
			Tuple.Create(_.List("ÂÈÄ"), 23944653, 779479),
			Tuple.Create(_.List("ÀÊÀÄ20111"), 4616623, 1688161),
			Tuple.Create(_.List("ÀÐÕ1-Â"), 4616789, 1688161),
			Tuple.Create(_.List("ÂÌÆ-Ã"), 4616942, 1688161),
			Tuple.Create(_.List("Ò-ÀÐÕÀÐÒ-À"), 4617644, 1688161),
			Tuple.Create(_.List("ÄÌ1-È"), 4617752, 1688161),
			Tuple.Create(_.List("ÖÈÂÈË-Á"), 4617884, 1688161),
			Tuple.Create(_.List("ÀÊÀÄ20111"), 4618541, 1688161),
			Tuple.Create(_.List("ÀÊÀÄ20111"), 4619312, 1688161),
			Tuple.Create(_.List("ÄÌ1-È"), 4619352, 1688161),
			Tuple.Create(_.List("ÂÐÝÉ1-Á"), 4619414, 1688161),
			Tuple.Create(_.List("ÑÎËÂ1-À"), 4619456, 1688161),
			Tuple.Create(_.List("ÄÌ1-È"), 4622500, 1688161),
			Tuple.Create(_.List("ÀÊÀÄ20111"), 4622920, 1688161),
			Tuple.Create(_.List("ÓÏÐ-Ã"), 1817517, 851285),
			Tuple.Create(_.List("ÐÌÈ-Â"), 1817613, 851285),
			Tuple.Create(_.List("ÐÌÈ-Â"), 1818461, 851285),
			Tuple.Create(_.List("ÈÒÈË"), 9532573, 851285),
			Tuple.Create(_.List("ÐÌÈ-Â"), 9623468, 851285),
			Tuple.Create(_.List("Ì50237"), 1832926, 920306),
			Tuple.Create(_.List("Ì10174"), 1833953, 920306),
			Tuple.Create(_.List("Ì10233"), 1833974, 920306),
			Tuple.Create(_.List("Ì6420Â"), 1834255, 920306),
			Tuple.Create(_.List("Ì6420Â"), 1834258, 920306),
			Tuple.Create(_.List("Ò-ÒÑ2008-À"), 1834398, 920306),
			Tuple.Create(_.List("Ì10267"), 7352081, 920306),
			Tuple.Create(_.List("Ì10267"), 18943116, 920306),
			Tuple.Create(_.List("ÑÅÒÈ1"), 1817122, 736714),
			Tuple.Create(_.List("Ò-ÑÅÒÈÔÁÑÄ"), 1858910, 736714),
			Tuple.Create(_.List("ÑÅÒÈ1"), 2102304, 736714),
			Tuple.Create(_.List("ÑÅÒÈ1"), 2145055, 736714),
			Tuple.Create(_.List("ÑÅÒÈ1"), 3931402, 736714),
			Tuple.Create(_.List("ÞÍ1-Á"), 11986064, 736714),
			Tuple.Create(_.List("ÞÍ1-Á"), 2102285, 736714),
			Tuple.Create(_.List("ÂÌÂÀÐÅ1-À"), 14003311, 736714),
			Tuple.Create(_.List("ÄÆÂ1-À"), 1600813, 840641),
			Tuple.Create(_.List("ÄÅË1"), 1601063, 840641),
			Tuple.Create(_.List("ÀÊÑ2-1"), 1601066, 840641),
			Tuple.Create(_.List("ÎÐÑÊ11"), 1601082, 840641),
			Tuple.Create(_.List("ÌÑÊÂË"), 1670838, 840641),
			Tuple.Create(_.List("Ì6234"), 11490726, 840641),
			Tuple.Create(_.List("ÈÖÍÄ1"), 11463788, 1766371),
//			Tuple.Create(_.List("ÄÈÇÖÈÑ"), 5175886, 1766371),
			Tuple.Create(_.List("ÌË", "ÌËÑÒ"), 72201, 8095),
			Tuple.Create(_.List("ÌÏÅÐÑ-À"), 637610, 8095),
			Tuple.Create(_.List("ÊÀÄÐ-Â", "ÊÀÄÐ1-Á", "ÊÀÄÐ2-Â"), 1267856, 8095),
			Tuple.Create(_.List("ÊÀÄÐ2-Â"), 2029760, 8095),
			Tuple.Create(_.List("ÒÎÌ-À"), 3487920, 8095),
			Tuple.Create(_.List("ÄÏÐÒ"), 4075836, 8095),
			Tuple.Create(_.List("ÌÏ-À"), 4078294, 8095),
			Tuple.Create(_.List("ÊÀÄÐ1-Á", "ÊÀÄÐ2-Â"), 4215407, 8095),
			Tuple.Create(_.List("ÌÏÅÐÑ-À"), 4227589, 8095),
			Tuple.Create(_.List("ÌÏÅÐÑ-À"), 4227597, 8095),
			Tuple.Create(_.List("ÑÄÑ", "ÑÌÅÒ", "ÀÄÌÑÌÅÒ"), 5596391, 8095),
			Tuple.Create(_.List("ÏÁÞË"), 6599041, 8095),
			Tuple.Create(_.List("ÐÓÊÏÎÄÐ-À"), 6600502, 8095),
			Tuple.Create(_.List("ÌÏÅÐÑ-À"), 7771263, 8095),
			Tuple.Create(_.List("ÌË"), 7907643, 8095),
			Tuple.Create(_.List("ÌËÑÒ"), 7907737, 8095),
	//		Tuple.Create(_.List("ÌÎÁÐ-À"), 8199584, 8095),
			Tuple.Create(_.List("ÑÄÑ"), 9723100, 8095),
			Tuple.Create(_.List("ÌË", "ÌËÑÒ"), 9992592, 8095),
			Tuple.Create(_.List("ÝÉ×ÒÌË-Á"), 1646922, 855394),
			Tuple.Create(_.List("ÂÌÀÐÊ-Á"), 1817752, 855394),
			Tuple.Create(_.List("ÂÌÀÐÊ-Á"), 1817779, 855394),
			Tuple.Create(_.List("ÂÅÁ"), 1829258, 855394),
			Tuple.Create(_.List("ÝÉ×ÒÌË-Á", "ÂÅÁ"), 1829283, 855394),
			Tuple.Create(_.List("ÕÌË-À"), 2090874, 855394),
			Tuple.Create(_.List("ÝÉ×ÒÌË-Á", "ÂÅÁ"), 4496538, 855394),
			Tuple.Create(_.List("ÂÌÀÐÊ-Á"), 1817756, 855394),
			Tuple.Create(_.List("ÎÏÐÂÅÁ"), 8258783, 855394),
			Tuple.Create(_.List("ÄÆÓÌ1"), 9221031, 855394),
			Tuple.Create(_.List("ÞÇÀÁÈËÈÒÈ"), 9457936, 855394),
			Tuple.Create(_.List("ÄÆÓÌ1", "ÄÆÓÌ2", "ÑÁÈË1", "ÑÁÈËÄÅÐ-Â", "ÂÏÐÅÑÑ1", "ÂÏÐÅÑÑ2"), 18511793, 855394),
			Tuple.Create(_.List("ÀßÊÑ"), 18513832, 855394),
			Tuple.Create(_.List("ÂÌÀÐÊ-Á"), 18517935, 855394),
			Tuple.Create(_.List("ÂÏÐÅÑÑ1", "ÂÏÐÅÑÑ2"), 22182257, 855394),
			Tuple.Create(_.List("ÄÈÇÈ"), 16844245, 779479),
			Tuple.Create(_.List("ÔÎÒÎ"), 12444187, 779479),
			Tuple.Create(_.List("ÔÎÒÎ"), 12444202, 779479),
			Tuple.Create(_.List("ÌÀÐÊÅÒ", "ÁÐÅÍÄ", "ÂÌÀÐÊ-Á", "ÄÈÇÐ-Â"), 7216110, 8095),
			Tuple.Create(_.List("ÊÀÄÐ1-Á", "ÊÀÄÐ2-Â"), 3163699, 8095),
			Tuple.Create(_.List("ÓÏÐÓ×"),	15584547,0),
Tuple.Create(_.List("ÌÏ-À","ÌÏÂÎÇÐ"),	7224282,0),
Tuple.Create(_.List("ÄÏÐÒ"),	4075836,0),
Tuple.Create(_.List("ÊÀÄÐ2-Â"),	2029760,0),
Tuple.Create(_.List("ÒÎÌ-À"),	3487920,0),
Tuple.Create(_.List("ÁÐÅÍÄ"),	11001462,0),
Tuple.Create(_.List("ÝÉ×ÒÌË-Á","ÂÅÁ"),	1829258,0),
Tuple.Create(_.List("ÂÌÀÐÊ-Á","ÑÌÌ"),	1817784,0),
Tuple.Create(_.List("ÝÉ×ÒÌË-Á","ÂÅÁ"),	1981791,0),
Tuple.Create(_.List("ÂÌÀÐÊ-Á","ÑÌÌ"),	18517759,0),
Tuple.Create(_.List("ÝÉ×ÒÌË-Á","ÂÅÁ"),	14784864,0),
Tuple.Create(_.List("ÂÌÀÐÊ-Á","ÑÌÌ"),	18517935,0),
Tuple.Create(_.List("1Ñ8ÓÒ1","1Ñ8ÓÒ2"),	21988105,0),
Tuple.Create(_.List("1Ñ8ÓÒ1","1Ñ8ÓÒ2"),	1815839,0),
Tuple.Create(_.List("1Ñ8ÓÒ1","1Ñ8ÓÒ2"),	2028298,0),
Tuple.Create(_.List("1Ñ8ÓÒ1","1Ñ8ÓÒ2"),	2101918,0),
Tuple.Create(_.List("1Ñ8ÇÓ-À","1Ñ8ÇÓÐÂ-"),	2286854,0)

			).Select(x => Tuple.Create(x.Item1, (long)x.Item2, x.Item3)).ToList();

		public static readonly List<List<string>> Months = _.List(
			_.List("ÿíâàðÿ", "ÿíâ"),
			_.List("ôåâðàëÿ", "ôåâ"),
			_.List("ìàðòà", "ìàð"),
			_.List("àïðåëÿ", "àïð"),
			_.List("ìàÿ", "ìàÿ"),
			_.List("èþíÿ", "èþí"),
			_.List("èþëÿ", "èþë"),
			_.List("àâãóñòà", "àâã"),
			_.List("ñåíòÿáðÿ", "ñåí"),
			_.List("îêòÿáðÿ", "îêò"),
			_.List("íîÿáðÿ", "íîÿ"),
			_.List("äåêàáðÿ", "äåê")
			);

		private const string DiscountText = @"\s*(ñêèäêà|ñêèä\.?)(.*?)\d{1,2}%?!?";

		private static readonly string DateText = @"\s*((c|ñ|íà÷àëî)? \d{1,2}|Ñî 2) " +
			("(" + Months.Select(x => x.ElementAt(1)).JoinWith("|") + ")") + ".*";
	}
}
