using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using HtmlAgilityPack;
using HtmlAgilityPlus;
using HtmlAgilityPlus.Extensions;
using PrometricGrabber;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Examination.Const;
using Specialist.Services.Examination;
using WebSpider;

namespace PrometricImport {
    public static class VueSpider {

		private const string Folder = "VueExam";
		public static Dictionary<string, List<ProviderExam>> GetVendors() {
			LoadVendors();
			return ExamImportService.GetFiles(Folder).Select(File.ReadAllText)
				.Select(x => new {vendor = new VueParser().GetVendor(x), html = x})
				.Where(x => !x.vendor.IsEmpty()).ToDictionary(x => x.vendor,
				x => new VueParser().GetExams(x.html));
		}

		public static void LoadVendors() {
			GetHtml(vueRoot + "/VSSForms/WebSupportCenter.cfm",
				"UserName=ptolochko&Password=tra_7EpA&SiteID=62933");
			var urls = GetUrls().ToList();
			for (int i = 0; i < urls.Count; i++) {
				ExamImportService.WriteStatus("Загрузка вендоров", Providers.VueExamType,i, urls.Count);
				var url = urls[i];
				var html = GetHtml(url);
				ExamImportService.WriteFile(Folder + "/" + url.GetHashCode() + ".html", html);
			}
		}

		const string  vueRoot = "http://vss.pearsonvue.com/";
    	private static IEnumerable<string> GetUrls() {
    		var reportRoot = vueRoot + "VSSReports/";
    		var url = vueRoot + "VSSReports/VSSSiteClientList.cfm";

    		string result = GetHtml(url);
    		var doc = new HtmlDocument();
    		doc.LoadHtml(result);
    		return new SharpQuery(doc).Find("a[href^='VSSSiteClientLanguage']").Select(x => 
    			reportRoot + x.Attr("href").Replace("VSSSiteClientLanguage", "VSSSiteActiveClientExams"));
    	}

    	private static CookieCollection cookieCollection = new CookieCollection();
    	private static string GetHtml(string url, string postData = null) {
    		/*cookieCollection = new CookieCollection();
    		cookieCollection.Add(new Cookie("CFID","2407515","/","vss.pearsonvue.com"));
    		cookieCollection.Add(new Cookie("CFTOKEN","39533066","/","vss.pearsonvue.com"));
    		cookieCollection.Add(new Cookie("SITEID","62933","/","vss.pearsonvue.com"));
    		cookieCollection.Add(new Cookie("APPUSERID","206883622","/","vss.pearsonvue.com"));
    		cookieCollection.Add(new Cookie("HOSTID","200093189","/","vss.pearsonvue.com"));
    		cookieCollection.Add(new Cookie("LOGINTYPE","External","/","vss.pearsonvue.com"));*/
    		return Util.GetHtml(url, ref cookieCollection, postData);
    	}
    }
}