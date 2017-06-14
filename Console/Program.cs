using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPlus;
using Newtonsoft.Json.Linq;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Web.Cms.Const;
using Specialist.Web.Cms.Logic;
using Specialist.Web.Cms.Root.Recommendations;
using Specialist.Web.Cms.Root.Socials;
using Specialist.Web.Cms.Root.YandexDirect.Logic;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Util;
using SpecialistTest.Common.Utils;
using SubSonic.DataProviders;
using SubSonic.Repository;
using WebSpider;
using SimpleUtils.Common.Extensions;
using Htmls = SimpleUtils.FluentHtml.Tags.Htmls;
using SimpleUtils.Extension;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Context.Logic;
using Specialist.Services.Common;
using Specialist.Web.Common.Utils.Files;
using Specialist.Web.WinService.Tasks;

namespace Console {
	public class Post{
   public Guid ID {get; set;}
   public string Title {get; set;}
   public string Body{get; set;}
}
	public class Program : Htmls {
		private static readonly Expression<Func<List<int>, int, int>> exp
			= (x, y) => x.FirstOrDefault(k => k == y);

		private static class ByIdCache<K> where K : class {
			public static Func<DataContext, object, K> GetByPKFunc = null;
			static ByIdCache() {
				var b = typeof(ITable).IsAssignableFrom(GetExpression().Body.Type);

				GetByPKFunc = GetExpression().Compile();
			}

			public static Func<DataContext, object, K> GetByPK() {
				Expression<Func<DataContext, object, K>> expression = GetExpression();
				return CompiledQuery.Compile(expression);
			}

			public static Expression<Func<DataContext, object, K>> GetExpression() {
				var _type = typeof (K);
				var itemParameter = Expression.Parameter(_type, "item");
				var pkParameter = Expression.Parameter(typeof (object), "pk");
				var dcParameter = Expression.Parameter(typeof (DataContext), "dc");
				Expression<Func<IQueryable<K>, K>> exp = x => x.FirstOrDefault(y => true);
				Expression<Func<DataContext, Table<K>>> exp2 = x => x.GetTable<K>();

				var firstOrDefaultInfo = ((MethodCallExpression) exp.Body).Method;
				var getTableInfo = ((MethodCallExpression) exp2.Body).Method;


				var pkPropertyName = LinqToSqlUtils.GetPKPropertyName(_type);
				var pkProperty = Expression.Property(
					itemParameter,
					pkPropertyName
					);
				return Expression.Lambda<Func<DataContext, object, K>>(
					Expression.Call(null,
						firstOrDefaultInfo,
						Expression.Call(dcParameter, getTableInfo), Expression.Lambda<Func<K, bool>>
							(
								Expression.Equal(
									Expression.Convert(pkProperty, typeof (object)),
									pkParameter
									),
								new[] {itemParameter}
							)), new[] {dcParameter, pkParameter});
			}
		}
		/*public static IEnumerable<Type> GetAllControllers(IEnumerable<Assembly> assemblies) {
			return assemblies.SelectMany(a => a.GetTypes()
				.Where(t => typeof(IController).IsAssignableFrom(t)));
		}*/
			public static Image GetImage(string url)
		{
			Stream str = null;
			HttpWebRequest wReq = (HttpWebRequest)WebRequest.Create(url);
			wReq.Proxy = null;
			try {
				HttpWebResponse wRes = (HttpWebResponse)(wReq).GetResponse();
			str = wRes.GetResponseStream();

			return Image.FromStream(str);
				
			}catch(Exception e) {
				return null;
			}
		}


		private void CreateCamp(int campaingId) {
				var compaign = DirectApiService.CallMethod< CampaignInfo>("GetCampaignsParams", 
				new {CampaignIDS = _.List(campaingId)}).First();
			compaign.Name = "[Черновик]" + compaign.Name;
			compaign.CampaignID = 0;
			compaign.StartDate = null;
			compaign.Strategy = new CampaignStrategy {
				StrategyName = "LowestCost"
			};
			var id = DirectApiService.CallMethodOne<int>("CreateOrUpdateCampaign", compaign);
		}

		private static void ListDump<T>(IEnumerable<T> array) {
			WL(array.Select(x => x.ToString()).JoinWith(";"));
		}
		static DirectApiService DirectApiService = new DirectApiService(true);

		static void TestConnection() {
			var context =
				new SpecialistDataContext(
					"data source=195.19.32.198;initial catalog=SPECIALIST;user id=SpecWebDataEditor;password=ghjdbltw;MultipleActiveResultSets=True");
			var course = context.Courses.First();
		}
		private static void Main(string[] args) {
//			new SendsayApi().Test();
//			CreateCertEngMs();
//			ShowManagers();
//			CreateCertVendor();
//			CreateCertEngMs();
//			new TestExport().Run();

//			WL(new OdnoklassnikiService().GetToken());
//			var order = new SpecialistWebDataContext().Orders.First(x => x.OrderID == 614364);
//			var result = HttpUtils.Get("https://3dsec.sberbank.ru/payment/rest/register.do",
//				PaymentDataCreator.SberbankMerchant(order));
//			var tags = DirectApiService.GetBannersTags(new BannersRequestInfo {
//				BannerIDS = new [] {1058852}
//			}).ToList();
//			tags.First().TagIDS = new []{248609};
//			DirectApiService.UpdateBannersTags(tags.ToArray());
//			CreateCert();
			WL("done");
			System.Console.ReadKey();
		}

		private static void ShowManagers() {
			var start = DateTime.Today;
			var l = Enumerable.Range(0, 10).Select(x =>
				Tuple.Create(start.AddDays(x), Employees.TodayManger(start.AddDays(x)))).ToList();
			WL(l.Select(x => x.ToString()).JoinWith("\n"));
		}

		static void SendMail() {
			string MailMaster = "<html><body>{0}</body></thml>";
			using (var client = new SmtpClient()) {
				
				using (var message =
					new MailMessage(MailService.info, MailService.ptolochko) {
						IsBodyHtml = true,
						BodyEncoding = Encoding.UTF8,
						Subject = "test",
						Body = MailMaster.FormatWith("test test"),
					}) {
					client.Send(message);
				}
			}

		}

		static void Test() {
			var list = _.List<IGrouping<int,int>>(
				Grouping.New(1, Enumerable.Range(0, 6)),
				Grouping.New(2, Enumerable.Range(0, 4)),
				Grouping.New(3, Enumerable.Range(0, 2)),
				Grouping.New(4, Enumerable.Range(0, 8)),
				Grouping.New(5, Enumerable.Range(0, 3)),
				Grouping.New(6, Enumerable.Range(0, 5)));
			var result = list.GetColumns(2, 5);
			WL(result.Select(x => x.Count.ToString()).JoinWith(", "));
		}

/*
		public static void PostMessage()
    {
      new VkontakteService().PostStatusUpdate("1e62843b9eeda7617e843138dfe8a9b0edfc496f93f8ed29108456e6104c793c28e254d5c9af3445db5ee",
		  "test 2");
    }

*/


		private static List<float?> GetPrices(BannerPhraseInfo oldPhrase) {
			return _.List(oldPhrase.Max, oldPhrase.Min,
				oldPhrase.PremiumMax, oldPhrase.PremiumMin);
		}

		private static void ParseReccomendations() {
			var lines = File.ReadAllLines("profs.txt");
			var names = new SpecialistDataContext().Courses
				.Select(x => new {x.Course_TC, x.Name}).ToDictionary(x => x.Course_TC,
					x => x.Name);
			var result = new List<string>();
			foreach (var line in lines) {
				var courseTCs = StringUtils.SafeSplit(line.Split('\t')[2])
					.Where(x => !x.IsEmpty());
				result.Add(line + "\t\"{0}\"".FormatWith(courseTCs.Select(x =>
					"[{0}]{1}".FormatWith(x, names.GetValueOrDefault(x))).JoinWith(Environment.NewLine)));
			}
			File.WriteAllLines("result.txt", result);
		}

		private static void TestSubSonic() {
			var p = ProviderFactory.GetProvider(@"Data Source=C:\dd\temp.sdf",
				"System.Data.SQLite");
			var rep = new SimpleRepository(p, SimpleRepositoryOptions.RunMigrations);
			var item = new Post {Body = "test", Title = "test"};
			rep.Add(item);
			var item2 = rep.Single<Post>(item.ID);
		}

		private static void CourseLinks2() {
			var courses = new SpecialistDataContext().Courses
				.Where(x => x.IsActive && x.UrlName != null && !x.IsTrack.GetValueOrDefault())
				.Select(x => new {x.Course_TC, x.UrlName, x.Name}).ToList();
			var files = Directory.GetFiles("links").ToList();
			var result = new List<Tuple<string, string, string>>();
			foreach (var file in files) {
				var lines = File.ReadAllLines(file).Skip(2).ToList();
				if (lines.Count == 0)
					continue;
				var data = courses.FirstOrDefault(x => x.Course_TC == Path.GetFileName(file));
				if (data == null)
					continue;
				var links = lines.Select(x => x.Split('\t')[1])
					.Where(x => x.Contains("www.specialist.ru") && !x.Contains(".asp"))
					.Select(x => Regex.Replace(x, "\\?.*", "")).Distinct().JoinWith("<br/>");
				result.Add(Tuple.Create(
					/*H.Anchor("http://www.specialist.ru/course/" + data.UrlName, data.Name).ToString()*/data.Name,
					links, data.Course_TC));
			}
			var text = table[result.Select(x => tr[td[x.Item3], td[x.Item1],
				td[x.Item2]])].ToString();
			File.WriteAllText("courselinks.html", HtmlHead + text);
		}

		private static void CourseLInks() {
			var courses = new SpecialistDataContext().Courses
				.Where(x => x.IsActive && x.UrlName != null && !x.IsTrack.GetValueOrDefault())
				.Select(x => new {x.Course_TC, x.UrlName}).ToList().Select(x =>
					Tuple.Create(x, "http://www.specialist.ru/course/" + x.UrlName));
			var url =
				"https://siteexplorer.search.yahoo.com/export;_ylt=A0oG7zEOG5xOZc8AUFfbl8kF?p={0}&bwm=i&fr=sfp";
			var doneUrls = Directory.GetFiles("links").Select(Path.GetFileName).ToList();
			var courses2 = courses.Where(x => !doneUrls.Contains(x.Item1.UrlName)).ToList();
			foreach (var course in courses2) {
				var fullurl = url.FormatWith(HttpUtility.UrlEncode(course.Item2));
				var text = YandexParser.GetHtml(fullurl);
				File.WriteAllText("links/" + course.Item1.UrlName, text);
			}
		}

		private const string HtmlHead = "<!doctype html> <meta charset=utf-8>";
		private static void CourseTable() {
			var context = new SpecialistDataContext();
			var courses = context.Courses
				.Where(x => x.IsActive && !x.IsTrack.GetValueOrDefault())
				.Select(x => new {x.Course_TC, x.Name, x.UrlName, x.Description, x.AuthorizationType_TC}).ToList();
			var courseGroups = context.Groups.Where(g => g.DateBeg >= DateTime.Today).Select(x => x.Course_TC)
				.Distinct().ToList();

			courses = courses.Where(x => x.AuthorizationType_TC !="МС" && !courseGroups.Contains(x.Course_TC)).ToList();
			var text = table[courses.Select(x => tr[td[x.Course_TC],
				td[H.Anchor("http://www.specialist.ru/course/" + x.UrlName, x.Name)], td[x.Description]])].ToString();
			File.WriteAllText("courses.html", HtmlHead + text);
		}

		private static void CreateImage() {
			using (var image = Image.FromFile(UserImages.GetTestCertFileSys(0))) {
				ImageUtils.DrawTestString(image,
					"Evgeniya Sluchak", "Планирование деятельности и электронный документооборот с помощью Microsoft Outlook", "25.10.2000", 123456).Save("test.png");
			}
		}

		static void CreateCertEng() {
					using (var image = Image.FromFile(UserImages.GetGroupCertEngFileSys(6, false, true, false))) {
						ImageUtils.DrawGroupCertEngTextOld(image,
							"Ivanov Ivan", "Internet User sldfkjs sldkfj a a a a a a a a a a a a asldkfj sldkfjsdf sldkfj", DateTime.Today.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture), true).Save("test.gif");
					}
		}

		static void CreateCertEngMs() {
			var hd = false;
			var certType = VendorEngCertData.graphisoft;
			using (var image = Image.FromFile(UserImages.GetGroupCertEngFileSys(certType, hd, true, false))) {
				var data = new VendorEngCertData(hd, certType,
"Svanov Svan", "Meijin: Сборка современного игрового компьютера",
DateTime.Today, "Svan Svanov");

				ImageUtils.RenderVendorEngCertTexts(image, data).Save("cert{0}-{1}.png".FormatWith(certType, hd ? "hd" : ""));
			}
		}

//		static void CreateCertVendor() {
//			var vendorCertId = 3;
//			var data = new VendorEngCertData(
//				false,
//				vendorCertId,
//				"Иванов Иван Иванович",
//				"Программные методы восстановления данных с различных типов носителей",
//				DateTime.Now,
//				"Петров Петр Петрович");
//			using (var image = Image.FromFile(UserImages.GetGroupCertVendorFileSys(vendorCertId))) {
//				ImageUtils.RenderVendorEngCertTexts(image, data).Save("vendorcert{0}.png".FormatWith(vendorCertId));
//			}
//		}
		static void CreateCert() {

			using (var image = Image.FromFile(UserImages.GetGroupCertFileSys(10))) {
				var learn = "обучалась";
				var date = "c {0} по {1} {2} в".FormatWith(DateTime.Today.DefaultString(),
					DateTime.Today.DefaultString(), learn);
				ImageUtils.DrawGroupCertText2016(image,
					"Иванов Иван Иванович",
					"Планирование деятельности и электронный документооборот с помощью Microsoft Outlook",
					24,
					date, "№ " + "1231453", Sex.M, "ОЧУ «Специалист»", false).Save("test.png");
			}
		}

		private static void ComplexGeoLocations() {
			var files = Directory.GetFiles("complex");
			var result = string.Empty;
			var complexes = new SpecialistDataContext().Complexes.Select(x => new {
				x.Complex_TC, x.UrlName
			}).ToList();
			foreach (var path in files) {
				var text = File.ReadAllText(path);
				var patterns = _.List(@"YMaps\.Placemark\(new YMaps\.GeoPoint",
					@"""Placemark"", new YMaps\.GeoPoint");
				var urlName = Path.GetFileNameWithoutExtension(path);
				var complexTC = complexes.First(x => x.UrlName == urlName).Complex_TC;
				foreach (var pattern in patterns) {
					var groups = Regex.Matches(text, pattern + @"\((.*?)\)");
					if (groups.Count > 0) {
						result += "{{\"{0}\",\"{1}\"}}"
							.FormatWith(complexTC, groups[0].Groups[1].Value.Replace(" ", "")) + Environment.NewLine;
						break;
					}
				}
			}

			WL(result);
		}

		private static void ImageFix() {
			var tests = new SpecialistTestDataContext().Tests.ToList();
			foreach (var test in tests) {
				var query = new SharpQuery("<div>" + test.Description + "</div>");
				var imgs = query.Find("img");
				if (imgs.Length == 0)
					continue;

				var scr = imgs.Attr("src");
				var url = scr;
				if (!url.Contains("http"))
					url = "http://test.specialist.ru" + url;
				var ext = Path.GetExtension(scr);
				var image = GetImage(url);
				if (image != null)
					image.Save(test.Id + ext);
			}
		}

		private static void RemoveDiscount() {
			/*	WL(DirectAutoText.RemoveDiscount("Test Скидка 15%"));
			WL(DirectAutoText.RemoveDiscount("Скид. 15%"));
			WL(DirectAutoText.RemoveDiscount("скид 15%"));
			WL(DirectAutoText.RemoveDiscount("Скидка 15%"));
			WL(DirectAutoText.RemoveDiscount("Скидка 1% Test"));
		*/
		}

		private static void RemoveDate() {
			WL(YandexDirectTextUtils.RemoveDateAndDiscount("С 15 мая"));
			WL(YandexDirectTextUtils.RemoveDateAndDiscount("Со 2 июн"));
			WL(YandexDirectTextUtils.RemoveDateAndDiscount("Со 2 июн!"));
		}

		private static void GetDate() {
			WL(YandexDirectTextUtils.GetDateText(new DateTime(2001, 10, 15), 20));
			WL(YandexDirectTextUtils.GetDateText(new DateTime(2001, 10, 15), 10));
			WL(YandexDirectTextUtils.GetDateText(new DateTime(2001, 10, 2), 20));
		}

		public static long Timer(Action action) {
			var sw = Stopwatch.StartNew();
			for (int j = 0; j < 500000; j++) {
				action();
			}
			sw.Stop();
			return sw.ElapsedMilliseconds;
		}


		private static void DirectApiImport() {
			var context = new SpecialistWebDataContext();
			var result = DirectApiService.GetActiveCompaignIDs();
			for (int j = 0; j < 3; j++) {
				//WL(DirectApiProxy.GetBannerPhrases(new [] {1297662}));
				var compaigns = result.Skip(10 * j).Take(10).ToList();
				//	WL(compaigns);
				var bannerIds = DirectApiService.GetBanners(new {
					CampaignIDS = compaigns,
					Filter = new {
						IsActive = new[] { "Yes" }
					}
				}).AsJEnumerable().Select(x => x.Value<int>("BannerID"))
					.Distinct()
					.ToList();
				//WL(banners);

				var phraseList = DirectApiService.GetBannerPhrasesFilter(bannerIds).AsJEnumerable();
				var phrases = phraseList
					.Select(x => new YdBannerPhrase {
						PhraseID = x["PhraseID"].Value<int>(),
						CampaignID = x["CampaignID"].Value<int>(),
						Phrase = x["Phrase"].Value<string>(),
						CurrentOnSearch = x["CurrentOnSearch"].Value<double>(),
						BannerID = x["BannerID"].Value<int>()
					});
				var final = phrases.Where(x => DirectApiService.Phrases.Contains(x.Phrase)).ToList();
				context.YdBannerPhrases.InsertAllOnSubmit(final);
			}

			context.SubmitChanges();
		}

		private static void Excel() {
			var file = "data1";
			var data1 = GetData(file);
			var data2 = GetData("data2");
			var empty = new[] { "none", "none", "none" };
			var result =
				from x in data1
				join y in data2 on x[0] equals y[0] into g
				from z in g.DefaultIfEmpty()
				select new {
					x = x ?? empty, z = z ?? empty
				};

			File.WriteAllLines("result", result.Select(r => string.Join("\t",
				r.x[0], r.z[0],
				r.x[1], r.z[1],
				r.x[2], r.z[2])));
		}

		private static IEnumerable<string[]> GetData(string file) {
			return File.ReadAllLines(file).Select(x => x.Split('\t'));
		}

		private static void WL(object obj) {
			System.Console.WriteLine(obj);
		}
	}
}