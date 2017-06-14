using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using Console;
using Facebook;
using Microsoft.Practices.Unity;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Common;
using Specialist.Services.Core.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Const;
using Specialist.Web.Cms.Core.ViewModel;
using Specialist.Web.Cms.Helper;
using Specialist.Web.Cms.Root.YandexDirect.Logic;
using Specialist.Web.Cms.ViewModel;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc.Extensions;
using Specialist.Web.WinService.Tasks;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Context.Const;
using Specialist.Services.Tests;
using Specialist.Web.Cms.Core;
using Specialist.Web.Root.Profile.Services;
using SpecialistTest.Common.Utils;
using Group = Specialist.Entities.Context.Group;

namespace Specialist.Web.Cms.Controllers {
	[Authorize]
	public class HomeController : Controller {

		[Microsoft.Practices.Unity.Dependency]
		public IRepository2<Questionnaire> QuestionnaireService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public IRepository2<Group> GroupService { get; set; }


		[Microsoft.Practices.Unity.Dependency]
		public AlbumVideoService AlbumVideoService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public UserTestResultService UserTestResultService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public IRepository2<User> UserService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public UserTestService UserTestService { get; set; }


		[Microsoft.Practices.Unity.Dependency]
		public IRepository2<Order> OrderService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

		DirectApiService DirectApiService = new DirectApiService();
		[Authorize]
		public ActionResult Index() {
//			new SocialTask().PostNews();
//			var message =
//				new MailMessage(MailService.info, 
//					new MailAddress("ptolochko@specialist.ru")) {
//					IsBodyHtml = true,
//					BodyEncoding = Encoding.UTF8,
//					Subject = "test",
//					Body = "test",
//				};
//				var client = new SmtpClient();
//			client.Send(message);
			//var client = new FacebookClient("AAACkYkfRHZA0BABg87J9EdNdg1rYWkXEwoV70PcIWloVqKhYnE9TN5FlEQPJrNJV5s0vt1O6kvJxZBw9ZC9dO6JyJjKgntOEeM78rIYVQZDZD");
			//var task = client.PostTaskAsync("/me/feed",
			//				new {
			//					message = "first" + "\r\n" + "second",
			//				});
			//task.Wait();

//			new SocialTask().TimerTick();
//			new YandexDirectTasks().TimerTick();
//			new TestCertPaidTask().TimerTick();
//			new EntityWithoutNewsTask().TimerTick();
//			new EntityActivationTask().TimerTick();
			return View();
		}

		public HtmlHelper Html {
			get {
				return new HtmlHelper(new ViewContext(ControllerContext,
					new WebFormView(ControllerContext, "web"), ViewData, TempData, new StringWriter()), new ViewPage());
			}
		}


		[ValidateInput(false)]
		public ActionResult MainMenu() {
			var model = Html.MainMenu();


			return View(model);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Index(TrackVM model) {
			return View();
		}

		public static bool IsWordStatStart {
			[MethodImpl(MethodImplOptions.Synchronized)]
			get; 
			[MethodImpl(MethodImplOptions.Synchronized)]
			set;
		}

		public ActionResult WordStat() {
			if(!IsWordStatStart) {
				IsWordStatStart = true;
				var resportId = DirectApiService.CreateNewWordstatReport(new NewWordstatReportInfo {
					Phrases = Phrases.ForWordStat.ToArray()
				});

				var reportStatus = new WordstatReportStatusInfo();
				while (reportStatus.StatusReport != "Done") {
					reportStatus = DirectApiService.GetWordstatReportList().First(x => x.ReportID == resportId);
				}

				var wordstatReportInfos = DirectApiService.GetWordstatReport(resportId);

				var phrases = wordstatReportInfos.SelectMany(x => _.List(new WordstatItem {Phrase = x.Phrase})
					.AddFluent(x.SearchedWith).AddFluent(new WordstatItem()));

			var table = H.table[
				H.Head(
				"Фраза",     
				"Показы"),
				phrases.Select(p => H.Row(
					p.Phrase,
					p.Shows == 0 ? "" : p.Shows.ToString()))];
			IsWordStatStart = false;
			return File(new UTF8Encoding().GetBytes(table.ToString()), 
				"application/ms-excel", "wordstat" +DateTime.Today.ToString("ddMMyy") + ".xls");
				
			}
			return null;
		}


		public ActionResult Competitors(int? phraseId) {
			if (phraseId.HasValue) {
				var positions = YandexParser.GetSavedPositions().Where(x => x.PhraseId == phraseId);
				var model = new JsTableVM {
					Title = Phrases.ForCompetitors[phraseId.Value - 1],
					Columns = {{"Дата", "date-rus"}, {"1", null}, {"2", null}, {"3", null}},
					Rows = positions.Select(x => {
						var sites = x.Sites.Split(',');
						return _.List<object>(
							x.Date.ToString("dd.MM.yy HH:mm"),
							sites.ElementAtOrDefault(0),
							sites.ElementAtOrDefault(1),
							sites.ElementAtOrDefault(2));
					})
				};
				return View(ViewNames.JsTable, model);
			}
			return View();
		}

		[Auth(Emails = "mkurskiy,ptolochko,motorina,vdichev,eignatova,mukulikov")]
		public ActionResult UpdateDirect(int? campaignId, int? targetCampaignId) {
			var model = new UpdateDirectVM();
			if(campaignId.HasValue) {
				model = DirectApiBannerService.UpdateCampaign(campaignId.Value, targetCampaignId);
			}
			model.CampaignId = campaignId;
			model.TargetCampaignId = targetCampaignId;
			return View(model);
		}


		[Auth(Emails = "motorina,ptolochko")]
		public ActionResult ForecastDirect(int? campaignId) {
			if(campaignId.HasValue) {
				 DirectApiBannerService.WriteForecast(campaignId.Value);
			}
			var model = new ForecastDirectVM();
			model.Files = Directory.GetFiles(DirectApiBannerService.GetForecastFolder())
				.Select(Path.GetFileName)
				.OrderByDescending(x => x).Take(20).ToList();
			return View(model);
		}
		public ActionResult DirectBannerPrices(int? companyId, long? bannerId, string token) {
			var model = new DirectBannerPricesVM {
				BannerId = bannerId,
				Token = token ?? Session[YandexAuthService.SessionKey].NotNullString()
			};
			var service = token.IsEmpty() ? DirectApiService : new DirectApiService(token: token);
			if (companyId.HasValue) {
				var bannerIds = service.GetActiveBanners(
					new GetBannersInfo {CampaignIDS = new[] {companyId.Value}}).Select(x => x.BannerID).ToArray();
				var prices = service.GetBannerPhrases(bannerIds).ToList();
				var data = prices.Select(x => 
					_.List(x.BannerID.ToString(), x.Price.ToString(),  
					(x.Prices.ToList().Select((price,i) => Tuple.Create(i + 1, price < x.Price))
						.FirstOrDefault(y => y.Item2).GetOrDefault(y => y.Item1)).ToString(),  x.Phrase)
						.AddFluent(x.Prices.Select(z => z.ToString()))
					);
				return File(StringUtils.Encoding1251.GetBytes(CsvUtil.Render(data)), 
				"text/csv", "bannerprices.csv");
			}

			if(bannerId.HasValue) {
				model.Prices = service.GetBannerPhrases(new[] {bannerId.Value}).ToList();
			}
			return View(model);
		}

		public ActionResult SeCompetitors() {
			var db = YandexParser.GetYandexCompetitorDB();
			var model = new JsTableVM {
				Columns = {{"Дата", "date-rus"}, 
				{"Фраза", null}, 
				{"Директ", null}, 
				{"Яндекс", null},
				{"AdWords", null},
				{"Google", null}
				},
				Title = "Конкуренты",
				Rows = db.Items.Select(x => _.List<object>(
					x.Date.ToString("dd.MM.yy"), 
					Phrases.YandexCompetitors[x.PhraseId], 
					x.Direct.JoinWith("<br/>"), 
					GetList(x.YandexSearch),
					x.AdWords.JoinWith("<br/>"), 
					GetList(x.GoogleSearch)
					))
			};
			return View(ViewNames.JsTable, model);
		}
		string GetDomain(string text) {
			return Regex.Match(text, "https?://(.*?)[/\"]").Groups[1].Value;
		}
		private string GetList(List<string> list) {
			return H.ol[list.Select(x => H.li[GetDomain(x) + H.br + x])].ToString();
		}

		public ActionResult Error() {
			return View();
		}

		public ActionResult CmsPage(string title, object html) {
			return View("CmsPage", new CmsPage {Title = title, Html = html});
		}

		public ActionResult Message(string text) {
			return CmsPage(text, H.div);
		}

		public ActionResult SocialImage(string text) {
			if (!text.IsEmpty()) {
				ImageUtils.WriteImage(text, CdnFiles.Paths.TempSocialImage + "info.png");
			}
			return View("SocialImage", Tuple.Create(text));
		}

		public List<int> FilterUsersByCourse(List<int> userIds, string courseTC) {
			if (courseTC.IsEmpty()) {
				return userIds;
			}
			var usersWithCourse = UserService.GetAll(x => userIds.Contains(x.UserID)
				&& x.Student.StudentInGroups.Any(sig => sig.Group.Course_TC == courseTC))
				.Select(x => x.UserID).ToList();
			return userIds.Except(usersWithCourse).ToList();
		}

		public ActionResult UserEmails(string text, string courseTC) {
			if (System.Web.HttpContext.Current.Request.IsPost()) {
				var userIds = Regex.Split(text, @"[^\d]").Where(x => !x.IsEmpty()).Select(int.Parse).ToList();
				var usersWithOrders = OrderService.GetAll(x => x.UpdateDate >= DateTime.Today.AddDays(-7) 
					&& userIds.Contains(x.UserID.Value)
					&& !x.PaymentType_TC.Equals(null)).Select(x => x.UserID.Value).ToList();
				userIds = userIds.Except(usersWithOrders).ToList();
				userIds = FilterUsersByCourse(userIds, courseTC);
				var result = UserService.GetAll(x => userIds.Contains(x.UserID)).ToList();
				var data = result.Select(x => _.List(x.Email)).ToList();
			return File(StringUtils.Encoding1251.GetBytes(CsvUtil.Render(data)), 
				"text/csv", "emails.csv");
			}

			return View();

		}



		[Auth(RoleList = Role.Employee)]
		public ActionResult FacebookUsers() {
			var letters = _.List<char?>('А', 'Б');
			var week = DateTime.Today.AddDays(-7);
			var emails = QuestionnaireService.GetAll(x => x.InputDate > week
				&& letters.Contains(x.CourseLetter)
				&& letters.Contains(x.SkillsLetter))
				.SelectMany(x => x.Student.StudentEmails.Select(y => y.Email)).ToList()
				.Distinct().Where(x => x.Contains("@") && !x.Contains(" ")).JoinWith(Environment.NewLine);
			return File(Encoding.UTF8.GetBytes(emails), 
				"text/csv", "facebook-contacts-{0}.csv".FormatWith(DateTime.Today.DefaultString()));
		}

		public ActionResult GroupFreePlaces() {
			var dateEnd = DateTime.Today.AddDays(14);
			var courses = GroupService.GetAll().PlannedAndNotBegin()
				.Where(x => x.DateBeg < dateEnd && CourseDirections.Motorina.Contains(x.Course.CourseDirectionA_TC))
				.Select(x => new {
					x.Course_TC,
					Free = x.MaxNumOfStudents - x.GroupCalc.NumOfStudents
				}).ToList().OrderByDescending(x => x.Free).Select(x => _.List(x.Course_TC, x.Free.ToString()));
			var result = CsvUtil.Render(courses);
			return File(Encoding.UTF8.GetBytes(result), 
				"text/csv", "freeplaces-{0}.csv".FormatWith(DateTime.Today.DefaultString()));

		}


		[Auth(RoleList = Role.ContentManager)]
		public ActionResult RefreshCerts(decimal id) {
			UserImages.DeleteAllCerts(id);
			return Content("Сертификаты заказа {0} обновлены".FormatWith(id));
		}


		[Auth(RoleList = Role.ContentManager)]
		public ActionResult RefreshCertsStudent(decimal id) {
			var sigs = StudentInGroupService.GetAll(x => x.Student_ID == id).Select(x => x.StudentInGroup_ID).ToList();
			sigs.ForEach(UserImages.DeleteAllCerts);
			UserImages.DeleteAllCerts(id);
			return Content("Сертификаты выпускника {0} обновлены".FormatWith(id));
		}

			[Auth(RoleList = Role.ContentManager)]
		public ActionResult TestResults() {
			
			
			UserTestService.LoadWith(x => x.User, x => x.Test);
			var testIds = _.List(707, 706, 708, 705);
			var userTests = UserTestService.GetAll(x => testIds.Contains(x.TestId)).ToList();
			var data = UserTestResultService.GetResultData(_.List(userTests));
			return File(Encoding.GetEncoding(1251).GetBytes(CsvUtil.Render(data)), 
				"text/csv", "TestResults.csv");
			
		}


		[Auth(Emails = "ptolochko,yuhromova")]
		public ActionResult DeleteUser() {
			return CmsPage("Удаление пользователя",
				H.Form(Url.Action<HomeController>(x => x.DeleteUserPost(null)))[
					H.p[H.label["Email"], H.InputText("email", null).Style("width:300px;")],
					H.Submit("Удалить")
					]);
		}

        [HttpPost]
		[Auth(Emails = "ptolochko,yuhromova")]
		public ActionResult DeleteUserPost(string email) {
				UserService.EnableTracking();
				var user = UserService.FirstOrDefault(x => x.Email == email);
				if (user == null) {
					return Message("Пользователя с email {0} не существует".FormatWith(email));
				}
				user.Email = "del-" + user.Email;
				user.IsActive = false;
				user.Student_ID = null;
				user.LastName = "Иванов";
				user.FirstName = "Иван";
				user.SecondName = "Иванович";
				user.Employee_TC = null;
				UserService.SubmitChanges();
				return Message("Пользователь удален");
		}


		public ActionResult RefreshAlbum() {
			return CmsPage("Обновить албом",
				H.Form(Url.Action<HomeController>(x => x.RefreshAlbumPost(0)))[
					H.p[H.label["Номер альбома"], H.InputText("albumId", null).Style("width:300px;")],
					H.Submit("Обновить")
					]);
		}

		[HttpPost]
		public ActionResult RefreshAlbumPost(long albumId) {
			AlbumVideoService.EnableTracking();
			var album = AlbumVideoService.FirstOrDefault(x => x.AlbumId == albumId);
			if (album == null) {
				return Message("Альбом {0} не существует".FormatWith(albumId));
			}
			AlbumVideoService.DeleteAndSubmit(album);
			AlbumVideoService.AddVimeoAlbum(albumId);
			return Message("Альбом обновлен");
		}






	}
}