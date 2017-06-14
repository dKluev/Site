using System;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Utils;

namespace Console {
	public class JivoSiteParser {
		public void Parse() {
			var fileName = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.htm");
			var html = fileName.Select(GetData).JoinWith(Environment.NewLine);
			File.WriteAllText("result.html", 
				" <!DOCTYPE html> <html> <head> <meta charset='utf-8'></head> <body> {0} </body> </html>".FormatWith(html)) ;
		}

		private static string GetData(string fileName) {
			var doc = new HtmlDocument();
			doc.Load(fileName);
			var tableHtml = doc.DocumentNode.SelectNodes("//table[@class='table zeropadding transparent']").First();
			var h2 = "<h3> {0}</h3>".FormatWith(StringUtils.ConvertTo1251(doc.DocumentNode.SelectNodes("//h2[@class='trim']").First().InnerHtml));
			var table = StringUtils.ConvertTo1251(tableHtml.OuterHtml);
			return h2 + table;
		}
	}
}