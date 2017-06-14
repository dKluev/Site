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
			var template = date.Day == 2 ? " ��" : " �";
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
			return GetText("{0} " + discount + "%", _.List(" ������", " ����"), maxLength);
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
			Tuple.Create(_.List("���-�"), 2065789, 0),
			Tuple.Create(_.List("���1"), 1661182, 0),
			Tuple.Create(_.List("����1"), 1948620, 0),
			Tuple.Create(_.List("����2-�"), 1948648, 0),
			Tuple.Create(_.List("���1", "���2"), 2028904, 0),
			Tuple.Create(_.List("���1", "���2"), 2029276, 0),
//			Tuple.Create(_.List("���1", "���2"), 2126834, 0),
			Tuple.Create(_.List("��-�", "�������"), 2251522, 0),
			Tuple.Create(_.List("���1", "���2"), 2254131, 0),
			Tuple.Create(_.List("����1", "����2"), 2271471, 0),
			Tuple.Create(_.List("��-�", "�������"), 2327599, 0),
			Tuple.Create(_.List("���1"), 2384308, 0),
			Tuple.Create(_.List("���2-1", "���2-2", "���-�"), 2431642, 0),
			Tuple.Create(_.List("����1", "����2-�", "����3", "��-�", "���2-1", "���2-2", "���-�"), 2650798, 0),
			Tuple.Create(_.List("����1", "����2-�", "����3", "��-�", "���2-1", "���2-2", "���-�"), 2696941, 0),
			Tuple.Create(_.List("���-�"), 3210079, 0),
			Tuple.Create(_.List("����-�"), 3213929, 0),
			Tuple.Create(_.List("���-�"), 4624267, 0),
			Tuple.Create(_.List("���-�"), 4626028, 0),
			Tuple.Create(_.List("����1", "����2-�", "����3"), 5047805, 0),
			Tuple.Create(_.List("����1", "����2-�", "����3"), 12225416, 0),
			Tuple.Create(_.List("����2-�", "����3"), 14659126, 0),
			Tuple.Create(_.List("����2-�"), 15059083, 0),
			//Tuple.Create(_.List("����-�"), 1815757, 0),
			Tuple.Create(_.List("����-�"), 1815762, 0),
			Tuple.Create(_.List("����-�"), 1815812, 0),
			Tuple.Create(_.List("1�8�1"), 1815819, 0),
			Tuple.Create(_.List("1�8�1"), 1815830, 0), 
			Tuple.Create(_.List("1�8�1"), 1815838, 0),
			Tuple.Create(_.List("1�8��1"), 1815839, 0),
			Tuple.Create(_.List("1�8��-�"), 1815846, 0),
			Tuple.Create(
				_.List("1�8��", "1�820", "1�821", "1�82���", "1�822", "1�824", "1�825", "1�82���1", "1�81-�", "1�82", "1�823",
					"1�����"), 1815850, 0),
			Tuple.Create(_.List("����-�"), 1925883, 0),
	//		Tuple.Create(_.List("1�8�1"), 2027947, 0),
			Tuple.Create(_.List("1�8��-�"), 2028145, 0),
			Tuple.Create(_.List("����-�"), 2028399, 0),
			Tuple.Create(_.List("����-�"), 2078756, 0),
			Tuple.Create(_.List("����-�"), 2092423, 0),
			Tuple.Create(_.List("1�8�1"), 2101856, 0),
			Tuple.Create(_.List("1�8��1", "1�8��2"), 2101918, 0),
			Tuple.Create(_.List("����-�"), 2109933, 0),
			Tuple.Create(_.List("1�8�1"), 2208865, 0),
			Tuple.Create(_.List("1�8�", "1�8���"), 2286854, 0),
			Tuple.Create(
				_.List("1�8��", "1�820", "1�821", "1�82���", "1�822", "1�824", "1�825", "1�82���1", "1�81-�", "1�82", "1�823",
					"1�����"), 2290765, 0),
			Tuple.Create(
				_.List("1�8��", "1�820", "1�821", "1�82���", "1�822", "1�824", "1�825", "1�82���1", "1�81-�", "1�82", "1�823",
					"1�����"), 2290783, 0),
			Tuple.Create(_.List("����-�"), 2391589, 0),
			Tuple.Create(_.List("����-�"), 2396070, 0),
			Tuple.Create(_.List("����-�"), 2396073, 0),
			Tuple.Create(
				_.List("1�8��", "1�820", "1�821", "1�82���", "1�822", "1�824", "1�825", "1�82���1", "1�81-�", "1�82", "1�823",
					"1�����"), 2421212, 0),
			Tuple.Create(_.List("�����", "1�8���"), 2437168, 0),
			Tuple.Create(_.List("���-�"), 2437334, 0),
			Tuple.Create(_.List("1�8��-�"), 4016378, 0),
			Tuple.Create(_.List("����-�"), 4333477, 0),
			Tuple.Create(_.List("����-�"), 4355625, 0),
			Tuple.Create(_.List("����-�"), 4501839, 0),
			Tuple.Create(_.List("����-�"), 4703253, 0),
			Tuple.Create(_.List("1�822", "1�823", "1�824", "1�825"), 5102574, 0),
			Tuple.Create(_.List("1�8�1"), 5102783, 0),
			Tuple.Create(_.List("����-�"), 5788465, 0),
//			Tuple.Create(_.List("������"), 6967624, 0),
			Tuple.Create(
				_.List("1�820", "1�821", "1�822", "1�823", "1�824", "1�825", "1�81-�", "1�8���-�", "1�82", "1�83-�", "1�84-�",
					"1�85-�", "1�86", "1�87-�"), 8670065, 0),
			Tuple.Create(_.List("����"), 9352452, 0),
			Tuple.Create(_.List("�����-�"), 9352578, 0),
			Tuple.Create(_.List("1�8�1"), 9481794, 0),
			Tuple.Create(_.List("����-�", "1�8�1"), 9940542, 0),
			Tuple.Create(_.List("����-�"), 10000019, 0),
			Tuple.Create(_.List("����-�", "���-�"), 10989637, 0),
			Tuple.Create(_.List("����-�", "���-�"), 10991119, 0),
			Tuple.Create(_.List("������", "1�8����"), 10991339, 0),
			Tuple.Create(_.List("1�8���", "�����"), 10992277, 0),
			Tuple.Create(_.List("����-�", "1�8�1"), 11003415, 0),
			Tuple.Create(_.List("����-�"), 12147190, 0),
			Tuple.Create(_.List("��������"), 13520982, 0),
			Tuple.Create(_.List("���-�"), 13557833, 0),
			Tuple.Create(_.List("1�8�1"), 15087701, 0),
			Tuple.Create(_.List("1�8���", "�����"), 16949509, 0),
			Tuple.Create(_.List("����-�"), 21300151, 0),
			Tuple.Create(_.List("����-�"), 25975722, 0),
			Tuple.Create(_.List("����1-�"), 1297592, 779479),
			Tuple.Create(_.List("����-�"), 1616071, 779479),
			Tuple.Create(_.List("�����-�"), 1931771, 779479),
			Tuple.Create(_.List("��1-�"), 2301521, 779479),
			Tuple.Create(_.List("������-�"), 2816156, 779479),
			Tuple.Create(_.List("��1-�"), 2867402, 779479),
			Tuple.Create(_.List("������"), 6909842, 779479),
			Tuple.Create(_.List("���-�"), 16844808, 779479),
			Tuple.Create(_.List("����-�"), 18121036, 779479),
			Tuple.Create(_.List("�����-�"), 18423606, 779479),
			Tuple.Create(_.List("���"), 23944653, 779479),
			Tuple.Create(_.List("����20111"), 4616623, 1688161),
			Tuple.Create(_.List("���1-�"), 4616789, 1688161),
			Tuple.Create(_.List("���-�"), 4616942, 1688161),
			Tuple.Create(_.List("�-������-�"), 4617644, 1688161),
			Tuple.Create(_.List("��1-�"), 4617752, 1688161),
			Tuple.Create(_.List("�����-�"), 4617884, 1688161),
			Tuple.Create(_.List("����20111"), 4618541, 1688161),
			Tuple.Create(_.List("����20111"), 4619312, 1688161),
			Tuple.Create(_.List("��1-�"), 4619352, 1688161),
			Tuple.Create(_.List("����1-�"), 4619414, 1688161),
			Tuple.Create(_.List("����1-�"), 4619456, 1688161),
			Tuple.Create(_.List("��1-�"), 4622500, 1688161),
			Tuple.Create(_.List("����20111"), 4622920, 1688161),
			Tuple.Create(_.List("���-�"), 1817517, 851285),
			Tuple.Create(_.List("���-�"), 1817613, 851285),
			Tuple.Create(_.List("���-�"), 1818461, 851285),
			Tuple.Create(_.List("����"), 9532573, 851285),
			Tuple.Create(_.List("���-�"), 9623468, 851285),
			Tuple.Create(_.List("�50237"), 1832926, 920306),
			Tuple.Create(_.List("�10174"), 1833953, 920306),
			Tuple.Create(_.List("�10233"), 1833974, 920306),
			Tuple.Create(_.List("�6420�"), 1834255, 920306),
			Tuple.Create(_.List("�6420�"), 1834258, 920306),
			Tuple.Create(_.List("�-��2008-�"), 1834398, 920306),
			Tuple.Create(_.List("�10267"), 7352081, 920306),
			Tuple.Create(_.List("�10267"), 18943116, 920306),
			Tuple.Create(_.List("����1"), 1817122, 736714),
			Tuple.Create(_.List("�-��������"), 1858910, 736714),
			Tuple.Create(_.List("����1"), 2102304, 736714),
			Tuple.Create(_.List("����1"), 2145055, 736714),
			Tuple.Create(_.List("����1"), 3931402, 736714),
			Tuple.Create(_.List("��1-�"), 11986064, 736714),
			Tuple.Create(_.List("��1-�"), 2102285, 736714),
			Tuple.Create(_.List("������1-�"), 14003311, 736714),
			Tuple.Create(_.List("���1-�"), 1600813, 840641),
			Tuple.Create(_.List("���1"), 1601063, 840641),
			Tuple.Create(_.List("���2-1"), 1601066, 840641),
			Tuple.Create(_.List("����11"), 1601082, 840641),
			Tuple.Create(_.List("�����"), 1670838, 840641),
			Tuple.Create(_.List("�6234"), 11490726, 840641),
			Tuple.Create(_.List("����1"), 11463788, 1766371),
//			Tuple.Create(_.List("������"), 5175886, 1766371),
			Tuple.Create(_.List("��", "����"), 72201, 8095),
			Tuple.Create(_.List("�����-�"), 637610, 8095),
			Tuple.Create(_.List("����-�", "����1-�", "����2-�"), 1267856, 8095),
			Tuple.Create(_.List("����2-�"), 2029760, 8095),
			Tuple.Create(_.List("���-�"), 3487920, 8095),
			Tuple.Create(_.List("����"), 4075836, 8095),
			Tuple.Create(_.List("��-�"), 4078294, 8095),
			Tuple.Create(_.List("����1-�", "����2-�"), 4215407, 8095),
			Tuple.Create(_.List("�����-�"), 4227589, 8095),
			Tuple.Create(_.List("�����-�"), 4227597, 8095),
			Tuple.Create(_.List("���", "����", "�������"), 5596391, 8095),
			Tuple.Create(_.List("����"), 6599041, 8095),
			Tuple.Create(_.List("�������-�"), 6600502, 8095),
			Tuple.Create(_.List("�����-�"), 7771263, 8095),
			Tuple.Create(_.List("��"), 7907643, 8095),
			Tuple.Create(_.List("����"), 7907737, 8095),
	//		Tuple.Create(_.List("����-�"), 8199584, 8095),
			Tuple.Create(_.List("���"), 9723100, 8095),
			Tuple.Create(_.List("��", "����"), 9992592, 8095),
			Tuple.Create(_.List("������-�"), 1646922, 855394),
			Tuple.Create(_.List("�����-�"), 1817752, 855394),
			Tuple.Create(_.List("�����-�"), 1817779, 855394),
			Tuple.Create(_.List("���"), 1829258, 855394),
			Tuple.Create(_.List("������-�", "���"), 1829283, 855394),
			Tuple.Create(_.List("���-�"), 2090874, 855394),
			Tuple.Create(_.List("������-�", "���"), 4496538, 855394),
			Tuple.Create(_.List("�����-�"), 1817756, 855394),
			Tuple.Create(_.List("������"), 8258783, 855394),
			Tuple.Create(_.List("����1"), 9221031, 855394),
			Tuple.Create(_.List("���������"), 9457936, 855394),
			Tuple.Create(_.List("����1", "����2", "����1", "�������-�", "������1", "������2"), 18511793, 855394),
			Tuple.Create(_.List("����"), 18513832, 855394),
			Tuple.Create(_.List("�����-�"), 18517935, 855394),
			Tuple.Create(_.List("������1", "������2"), 22182257, 855394),
			Tuple.Create(_.List("����"), 16844245, 779479),
			Tuple.Create(_.List("����"), 12444187, 779479),
			Tuple.Create(_.List("����"), 12444202, 779479),
			Tuple.Create(_.List("������", "�����", "�����-�", "����-�"), 7216110, 8095),
			Tuple.Create(_.List("����1-�", "����2-�"), 3163699, 8095),
			Tuple.Create(_.List("�����"),	15584547,0),
Tuple.Create(_.List("��-�","������"),	7224282,0),
Tuple.Create(_.List("����"),	4075836,0),
Tuple.Create(_.List("����2-�"),	2029760,0),
Tuple.Create(_.List("���-�"),	3487920,0),
Tuple.Create(_.List("�����"),	11001462,0),
Tuple.Create(_.List("������-�","���"),	1829258,0),
Tuple.Create(_.List("�����-�","���"),	1817784,0),
Tuple.Create(_.List("������-�","���"),	1981791,0),
Tuple.Create(_.List("�����-�","���"),	18517759,0),
Tuple.Create(_.List("������-�","���"),	14784864,0),
Tuple.Create(_.List("�����-�","���"),	18517935,0),
Tuple.Create(_.List("1�8��1","1�8��2"),	21988105,0),
Tuple.Create(_.List("1�8��1","1�8��2"),	1815839,0),
Tuple.Create(_.List("1�8��1","1�8��2"),	2028298,0),
Tuple.Create(_.List("1�8��1","1�8��2"),	2101918,0),
Tuple.Create(_.List("1�8��-�","1�8����-"),	2286854,0)

			).Select(x => Tuple.Create(x.Item1, (long)x.Item2, x.Item3)).ToList();

		public static readonly List<List<string>> Months = _.List(
			_.List("������", "���"),
			_.List("�������", "���"),
			_.List("�����", "���"),
			_.List("������", "���"),
			_.List("���", "���"),
			_.List("����", "���"),
			_.List("����", "���"),
			_.List("�������", "���"),
			_.List("��������", "���"),
			_.List("�������", "���"),
			_.List("������", "���"),
			_.List("�������", "���")
			);

		private const string DiscountText = @"\s*(������|����\.?)(.*?)\d{1,2}%?!?";

		private static readonly string DateText = @"\s*((c|�|������)? \d{1,2}|�� 2) " +
			("(" + Months.Select(x => x.ElementAt(1)).JoinWith("|") + ")") + ".*";
	}
}
