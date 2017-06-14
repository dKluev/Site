using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using SimpleUtils.Common.Extensions;

namespace SimpleUtils.Utils {
	public static class StringUtils {
		public static string Convert(string value, Encoding src, Encoding trg) {
			Decoder dec = src.GetDecoder();
			byte[] ba = trg.GetBytes(value);
			int len = dec.GetCharCount(ba, 0, ba.Length);
			char[] ca = new char[len];
			dec.GetChars(ba, 0, ba.Length, ca, 0);
			return new string(ca);
		}
		public static string UrlToHtmlId(string url) {
			return Regex.Replace(url, @"[?/&=]", "-", RegexOptions.Compiled);

		}

		public static bool IsBasicLetter(char c) {
		    return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		}

		public static string UppercaseFirst(string s) {
			if(s.IsEmpty())
				return s;
			return char.ToUpper(s[0]) + s.Substring(1);
		}

		public static string GetUtmPart(string source, string medium, string compaign) {
			return "?utm_source=" + source + "&utm_medium=" + medium + "&utm_campaign=" + compaign;
		}

		public static string AddUtm(string text, string source, string medium, string compaign) {
			var urlPostfix = GetUtmPart(source, medium, compaign);
			return Regex.Replace(text, "(href=\".*?)\"", "$1" + urlPostfix + "\"");
		}

		public static string ConvertTo1251(string str) {
			return Convert(str, Encoding.UTF8, Encoding1251);
		}

		public static Encoding Encoding1251 {
			get { return Encoding.GetEncoding("windows-1251"); }
		}

		public static string SafeSubstring(string str, int length, int startIndex = 0) {
			if (str.IsEmpty())
				return str;
			if (str.Length <= length)
				return str;
			return str.Substring(startIndex, length);
		}


		public static long? ParseLong(string str) {
			long x;
			if (long.TryParse(str, out x))
				return x;
			return null;
		}

		public static int? ParseInt(string str) {
			int x;
			if (int.TryParse(str, out x))
				return x;
			return null;
		}

		public static DateTime? ParseDate(string str, string format) {
			DateTime x;
			if (DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out x))
				return x;
			return null;
		}

		public static int? ParseHex(string str) {
			int x;
			if (int.TryParse(str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out x))
				return x;
			return null;
		}

		public static string Tag(string str, string tag) {
			return string.Format("<{1}>{0}</{1}>", str, tag);
		}

		public static string AngleBrackets(string str) {
			if (str.IsEmpty())
				return str;
			return "«" + str.Trim() + "»";
		}

		public static string OnlyLetters(string str) {
			if (str.IsEmpty())
				return str;
			return new Regex("[^¸¨à-ÿÀ-ßa-zA-Z ]").Replace(str, "");
		}

		public static string EncodeBase64(string str) {
			if (str.IsEmpty()) return str;
			var encbuff = Encoding.UTF8.GetBytes(str);
			return System.Convert.ToBase64String(encbuff);
		}


		public static string DecodeBase64(string base64str) {
            if (base64str == null)
                return string.Empty;
			try {
				var decbuff = System.Convert.FromBase64String(base64str);
				return Encoding.UTF8.GetString(decbuff);
			}
			catch {
				return string.Empty;
			}
		}

		public static string ReplaceEx(string original,
			string pattern, string replacement) {
			int count, position0, position1;
			count = position0 = position1 = 0;
			string upperString = original.ToUpper();
			string upperPattern = pattern.ToUpper();
			int inc = (original.Length/pattern.Length)*
				(replacement.Length - pattern.Length);
			char[] chars = new char[original.Length + Math.Max(0, inc)];
			while ((position1 = upperString.IndexOf(upperPattern,
				position0)) != -1) {
				for (int i = position0; i < position1; ++i)
					chars[count++] = original[i];
				for (int i = 0; i < replacement.Length; ++i)
					chars[count++] = replacement[i];
				position0 = position1 + pattern.Length;
			}
			if (position0 == 0) return original;
			for (int i = position0; i < original.Length; ++i)
				chars[count++] = original[i];
			return new string(chars, 0, count);
		}

		public static List<string> SafeSplit(string str, char separator = ',') {
			if (str.IsEmpty())
				return new List<string>();
			return str.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries)
				.Select(tc => tc.Trim()).ToList();
		}

		public static string RemoveNewline(string str) {
			return str.IsEmpty() ? str : Regex.Replace(str, @"\r\n?|\n", " ");
		}

		public static List<int> IntListSplit(string str) {
			var list = SafeSplit(str);
			return IntList(list);
		}

		public static List<int> IntList(List<string> list) {
			if (list.Any()) {
				if (ParseInt(list.First()).HasValue)
					return list.Select(x => ParseInt(x).GetValueOrDefault()).ToList();
			}
			return new List<int>();
		}

		public static string Unsplit(IEnumerable<object> list) {
			return list.Select(x => x.ToString().Trim()).JoinWith(",");
		}

		public static string GetFirstParagraph(string str) {
			if (str == null)
				return null;
			var match = Regex.Match(str,
				@"<p>(.*?)</p>", RegexOptions.Singleline);
			if (match.Success) {
				return match.Groups[1].Value;
			}
			return str;
		}

		public static string GetInnerHtml(string anchor) {
			if (anchor == null)
				return null;
			var match = Regex.Match(anchor,
				@">(.*?)<", RegexOptions.Singleline);
			if (match.Success) {
				return match.Groups[1].Value;
			}
			return string.Empty;
		}

		public static string AddTargetBlank(string tag) {
			return AddAttr(tag, "target", "_blank");
		}

		public static string AddAttr(string tag, string name, string value) {
			var index = tag.IndexOf(">");
			if (index < 0) return tag;
			var html = name + "=\"" + value + "\"";
			return tag.Insert(index, " " + html);

		}
		public static string GetHref(string anchor) {
			if (anchor == null)
				return null;
			var match = Regex.Match(anchor,
				@"href=""(.*?)""", RegexOptions.Singleline);
			if (match.Success) {
				return match.Groups[1].Value;
			}
			return string.Empty;
		}



		public static string ReplaceGLT(string str) {
			if (str.IsEmpty()) return str;
			var gt = "&gt;";
			var lt = "&lt;";
			return str.Replace(">", gt).Replace("<", lt)
				.Replace(lt + "pre" + gt, "<pre>")
				.Replace(lt + "/pre" + gt, "</pre>") ;
		}

		public static string RemoveTags(string str) {
			if (str == null)
				return null;
			var objRegEx = new Regex("<[^>]*>", RegexOptions.Compiled);

			return objRegEx.Replace(str, "");
		}

		public static string GetShortText(string str) {
			if (str.IsEmpty())
				return string.Empty;
			var parts = RemoveTags(str).Split(' ');
			var result = parts.Take(20).JoinWith(" ");
			if (parts.Count() > 20)
				result += "...";
			return result;
		}

		public static string CoursesPrefix(string str) {
			if (str.StartsWith("Êóðñ"))
				return str;
			return "Êóðñû " + str;
		}

		public static string GetRegGroupValue(string str, string pattern) {
			if (str.IsEmpty())
				return null;
			var match = Regex.Match(str, pattern);
			if (match.Groups.Count > 1)
				return match.Groups[1].Value;
			return null;
		}

		public static string GetMd5(string input) {
			using (var x = MD5.Create()) {
				var bs = Encoding.UTF8.GetBytes(input);
				bs = x.ComputeHash(bs);
				var s = new StringBuilder();
				foreach (var b in bs) {
					s.Append(b.ToString("x2").ToLower());
				}
				return s.ToString();
			}
		}

		public static string GetSha256(string input) {
			var Sb = new StringBuilder();
			using (var hash = SHA256Managed.Create()) {
				var enc = Encoding.UTF8;
				var result = hash.ComputeHash(enc.GetBytes(input));
				foreach (var b in result)
					Sb.Append(b.ToString("x2"));
			}
			return Sb.ToString();
		}

		public static bool IsSpecEmail(string email) {
			return email != null && email.ToLowerInvariant().EndsWith("@specialist.ru");
		}

		public static bool OnlyCyrillicAndSpace(string txt) {
			return !txt.IsEmpty() && Regex.IsMatch(txt, @"^[à-ÿÀ-ß\s]+$");
		}
		public static bool ContainsLatin(string txt) {
			return !txt.IsEmpty() && Regex.IsMatch(txt, "[a-zA-Z]");
			
		}

		public static string AddUrlParam(string url, string param, string value) {
			if (url.IsEmpty()) {
				return url;
			}
			var postfix = param + "=" + value;
			return url + (url.Contains("?") ? "&" : "?") + postfix;
		}

		public static string GetVimeoAlbumId(string url) {
			return GetRegGroupValue(url, @"vimeo.com/album/(\d+)");
		}

		public static bool IsVimeoUrl(string url) {
			return url != null && url.Contains("vimeo.com");
		}
	}
}