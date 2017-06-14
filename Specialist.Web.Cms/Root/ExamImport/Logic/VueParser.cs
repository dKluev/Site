using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using PrometricGrabber;
using System.Linq;
using Specialist.Entities.Profile.Const;
using SimpleUtils.Common.Extensions;

namespace PrometricImport {
	public class VueParser {
		public List<ProviderExam> GetExams(string html) {
			if(html.Contains("There are not any active exams"))
				return new List<ProviderExam>();
			var table = html.Between("reportContainer", "</table>");
			var rows = Partition(table.BetweenAll("<tr>", "</tr>").Skip(1),3);
			return rows.Where(row => row.ElementAt(2).Contains("English")).Select(row => {
				var row1 = row.ElementAt(0);
				var row2 = row.ElementAt(1);
				var cols1 = row1.BetweenAll("<td", "/td>");
				var cols2 = row2.BetweenAll("<td", "/td>");
				var number = Regex.Match(cols1[0], @">\s+(.*?)\s+<").Groups[1].Value;
				if(number.IsEmpty())
					throw new Exception("exam number is empty");
				var name = Regex.Match(cols1[1], ">(.*?)&nbsp;<").Groups[1].Value;
				var price = Regex.Match(cols2[1], @">.*?(\d+)").Groups[1].Value;
				return new ProviderExam(name, number) {Price = int.Parse(price), 
					Languages = {ProviderConst.PrometricLang.English}};
			}).ToList();


		}

		public string GetVendor(string html) {
			var vendor = html.Between("<H1>", "</H1>");
			return vendor;
		}

		public static IEnumerable<IEnumerable<T>> Partition<T>(IEnumerable<T> source, int size) {
			T[] array = null;
			int count = 0;
			foreach (T item in source) {
				if (array == null) {
					array = new T[size];
				}
				array[count] = item;
				count++;
				if (count == size) {
					yield return new ReadOnlyCollection<T>(array);
					array = null;
					count = 0;
				}
			}
			if (array != null) {
				Array.Resize(ref array, count);
				yield return new ReadOnlyCollection<T>(array);
			}
		}

	}
}