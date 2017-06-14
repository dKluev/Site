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
			/* if(word == "����")
			 {
				 var lastNumber = number % 10;
				 if (lastNumber.BetweenInclude(5, 20)) return "������";
				 if (lastNumber == 1) return "����";
				 if (lastNumber.In(2, 3, 4)) return "�����";
				 return "������";
			 }*/
			var source = new Dictionary<string, string[]>
            {
                {"����", new []{"������", "����", "�����"}},
                {"�����", new []{"����", "�����", "�����"}},
                {"�������", new []{"���������", "�����", "��������"}},
                {"����������", new []{"������������", "����������", "�����������"}},
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
			return Regex.IsMatch(text, "[�-�]", RegexOptions.IgnoreCase);
		}

		static Dictionary<string, string> transliteSource =
			new Dictionary<string, string>();
		static Linguistics() {
			transliteSource.Add("�", "a");
			transliteSource.Add("�", "b");
			transliteSource.Add("�", "v");
			transliteSource.Add("�", "g");
			transliteSource.Add("�", "d");
			transliteSource.Add("�", "e");
			transliteSource.Add("�", "yo");
			transliteSource.Add("�", "zh");
			transliteSource.Add("�", "z");
			transliteSource.Add("�", "i");
			transliteSource.Add("�", "j");
			transliteSource.Add("�", "k");
			transliteSource.Add("�", "l");
			transliteSource.Add("�", "m");
			transliteSource.Add("�", "n");
			transliteSource.Add("�", "o");
			transliteSource.Add("�", "p");
			transliteSource.Add("�", "r");
			transliteSource.Add("�", "s");
			transliteSource.Add("�", "t");
			transliteSource.Add("�", "u");
			transliteSource.Add("�", "f");
			transliteSource.Add("�", "h");
			transliteSource.Add("�", "c");
			transliteSource.Add("�", "ch");
			transliteSource.Add("�", "sh");
			transliteSource.Add("�", "sch");
			transliteSource.Add("�", "j");
			transliteSource.Add("�", "i");
			transliteSource.Add("�", "j");
			transliteSource.Add("�", "e");
			transliteSource.Add("�", "yu");
			transliteSource.Add("�", "ya");
			transliteSource.Add("�", "A");
			transliteSource.Add("�", "B");
			transliteSource.Add("�", "V");
			transliteSource.Add("�", "G");
			transliteSource.Add("�", "D");
			transliteSource.Add("�", "E");
			transliteSource.Add("�", "Yo");
			transliteSource.Add("�", "Zh");
			transliteSource.Add("�", "Z");
			transliteSource.Add("�", "I");
			transliteSource.Add("�", "J");
			transliteSource.Add("�", "K");
			transliteSource.Add("�", "L");
			transliteSource.Add("�", "M");
			transliteSource.Add("�", "N");
			transliteSource.Add("�", "O");
			transliteSource.Add("�", "P");
			transliteSource.Add("�", "R");
			transliteSource.Add("�", "S");
			transliteSource.Add("�", "T");
			transliteSource.Add("�", "U");
			transliteSource.Add("�", "F");
			transliteSource.Add("�", "H");
			transliteSource.Add("�", "C");
			transliteSource.Add("�", "Ch");
			transliteSource.Add("�", "Sh");
			transliteSource.Add("�", "Sch");
			transliteSource.Add("�", "J");
			transliteSource.Add("�", "I");
			transliteSource.Add("�", "J");
			transliteSource.Add("�", "E");
			transliteSource.Add("�", "Yu");
			transliteSource.Add("�", "Ya");

		}

		public static string UrlTranslite(string text) {
			if (text.IsEmpty())
				return null;
			var regexp = new Regex(@"[^�-��-�a-zA-Z\s\d]");
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
			{1, "�������"},
			{2, "�������"},
			{3, "��������"},
			{4, "���������"},
			{5, "������"},
			{6, "�������"},
			{7, "��������"},
			{8, "��������"},
			{9, "��������"},
			{10, "��������"},
			{11, "�������������"},
			{12, "������������"},
			{13, "������������"},
			{14, "��������������"},
			{15, "������������"},
			{16, "�������������"},
			{17, "������������"},
			{18, "��������������"},
			{19, "��������������"},
			{20, "����������"},
		};
	}
}