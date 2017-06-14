using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using System.Linq;
using System.Reflection.Emit;

namespace SimpleUtils.Util {
	public static class Linguistics {
		public static string Plural(string word, int number) {
			/* if(word == "курс")
			 {
				 var lastNumber = number % 10;
				 if (lastNumber.BetweenInclude(5, 20)) return "курсов";
				 if (lastNumber == 1) return "курс";
				 if (lastNumber.In(2, 3, 4)) return "курса";
				 return "курсов";
			 }*/
			var source = new Dictionary<string, string[]>
            {
                {"курс", new []{"курсов", "курс", "курса"}},
                {"место", new []{"мест", "место", "места"}},
                {"экзамен", new []{"экзаменов", "экзам", "экзамена"}},
                {"сертификат", new []{"сертификатов", "сертификат", "сертификата"}},
            };

			if (!source.ContainsKey(word))
				return string.Empty;


			var forms = source[word];
			var form1 = forms[0];
			var form2 = forms[1];
			var form3 = forms[2];

			var lastNumber = number % 10;
			if (number.BetweenInclude(5, 20))
				return form1;
			if (lastNumber == 1)
				return form2;
			if (lastNumber.In(2, 3, 4))
				return form3;
			return form1;
		}

		public static bool IsRussian(string text) {
			if (text.IsEmpty()) return false;
			return Regex.IsMatch(text, "[а-я]", RegexOptions.IgnoreCase);
		}

		static Dictionary<string, string> transliteSource =
			new Dictionary<string, string>();
		static Linguistics() {
			transliteSource.Add("а", "a");
			transliteSource.Add("б", "b");
			transliteSource.Add("в", "v");
			transliteSource.Add("г", "g");
			transliteSource.Add("д", "d");
			transliteSource.Add("е", "e");
			transliteSource.Add("ё", "yo");
			transliteSource.Add("ж", "zh");
			transliteSource.Add("з", "z");
			transliteSource.Add("и", "i");
			transliteSource.Add("й", "j");
			transliteSource.Add("к", "k");
			transliteSource.Add("л", "l");
			transliteSource.Add("м", "m");
			transliteSource.Add("н", "n");
			transliteSource.Add("о", "o");
			transliteSource.Add("п", "p");
			transliteSource.Add("р", "r");
			transliteSource.Add("с", "s");
			transliteSource.Add("т", "t");
			transliteSource.Add("у", "u");
			transliteSource.Add("ф", "f");
			transliteSource.Add("х", "h");
			transliteSource.Add("ц", "c");
			transliteSource.Add("ч", "ch");
			transliteSource.Add("ш", "sh");
			transliteSource.Add("щ", "sch");
			transliteSource.Add("ъ", "j");
			transliteSource.Add("ы", "i");
			transliteSource.Add("ь", "j");
			transliteSource.Add("э", "e");
			transliteSource.Add("ю", "yu");
			transliteSource.Add("я", "ya");
			transliteSource.Add("А", "A");
			transliteSource.Add("Б", "B");
			transliteSource.Add("В", "V");
			transliteSource.Add("Г", "G");
			transliteSource.Add("Д", "D");
			transliteSource.Add("Е", "E");
			transliteSource.Add("Ё", "Yo");
			transliteSource.Add("Ж", "Zh");
			transliteSource.Add("З", "Z");
			transliteSource.Add("И", "I");
			transliteSource.Add("Й", "J");
			transliteSource.Add("К", "K");
			transliteSource.Add("Л", "L");
			transliteSource.Add("М", "M");
			transliteSource.Add("Н", "N");
			transliteSource.Add("О", "O");
			transliteSource.Add("П", "P");
			transliteSource.Add("Р", "R");
			transliteSource.Add("С", "S");
			transliteSource.Add("Т", "T");
			transliteSource.Add("У", "U");
			transliteSource.Add("Ф", "F");
			transliteSource.Add("Х", "H");
			transliteSource.Add("Ц", "C");
			transliteSource.Add("Ч", "Ch");
			transliteSource.Add("Ш", "Sh");
			transliteSource.Add("Щ", "Sch");
			transliteSource.Add("Ъ", "J");
			transliteSource.Add("Ы", "I");
			transliteSource.Add("Ь", "J");
			transliteSource.Add("Э", "E");
			transliteSource.Add("Ю", "Yu");
			transliteSource.Add("Я", "Ya");

		}

		public static string UrlTranslite(string text) {
			if (text.IsEmpty())
				return null;
			var regexp = new Regex(@"[^а-яА-Яa-zA-Z\s\d]");
			text = regexp.Replace(text, string.Empty);
			regexp = new Regex(@"\s+");
			text = regexp.Replace(text, "-");
			if (text.Length > 200)
				text = text.Substring(0, 200);
			if (text.IsEmpty())
				return null;
			if (text.Last() == '-')
				text = text.RemoveLast();
			return Translite(text);
		}

		public static string Translite(string text, bool saveCase = false) {
			if (!saveCase)
				text = text.ToLower();
			var result = new StringBuilder(text);
			foreach (var pair in transliteSource) {
				result.Replace(pair.Key, pair.Value);
			}
			return result.ToString();
		}

		public static string GetOrdinalName(int number) {
			return OrdinalNames.GetValueOrDefault(number) ?? number.ToString();
		}
		static Dictionary<int, string> OrdinalNames = new Dictionary<int, string> {
			{1, "первого"},
			{2, "второго"},
			{3, "третьего"},
			{4, "четвёртого"},
			{5, "пятого"},
			{6, "шестого"},
			{7, "седьмого"},
			{8, "восьмого"},
			{9, "девятого"},
			{10, "десятого"},
			{11, "одиннадцатого"},
			{12, "двенадцатого"},
			{13, "тринадцатого"},
			{14, "четырнадцатого"},
			{15, "пятнадцатого"},
			{16, "шестнадцатого"},
			{17, "семнадцатого"},
			{18, "восемнадцатого"},
			{19, "девятнадцатого"},
			{20, "двадцатого"},
		};
	}
}