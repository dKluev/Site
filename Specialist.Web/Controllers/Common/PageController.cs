using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using Bing;
using GSearch;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Utils;
using Specialist.Entities.Announcement;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Common;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Center;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Common;
using Specialist.Services.Common.Extension;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Interface.Center;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Interface.SitePart;
using Specialist.Services.Order;
using Specialist.Services.Passport;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc;
using Specialist.Web.Common.Page;
using Specialist.Web.Common.Utils;
using Specialist.Web.Common.Utils.Logic;
using Specialist.Web.Common.ViewModel;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Pages.Interfaces;
using Specialist.Web.Root.Common.Services;
using Specialist.Web.Util;
using Group = Specialist.Entities.Context.Group;
using Specialist.Web.Common.Extension;
using Specialist.Web.Helpers;
using Specialist.Web.Pages;
using Specialist.Web.ViewModel.Orders;
using SpecialistTest.Web.Core.Mvc.Extensions;

namespace Specialist.Web.Controllers {
	public class PageController : ViewController {
		private const string GoogleApiKey =
			"ABQIAAAAYEc2qL_4J7vKPCtYF3_lrBQEV4ltqZT-alOEiEX-" +
				"P1hlc2NF9xTdghkUY3hi8JiIB2ysdTPiT71OxQ";


		[Dependency]
		public IOrderService OrderService { get; set; }

		[Dependency]
		public PiStudentEmailService PiStudentEmailService { get; set; }

		[Dependency]
		public IUserService UserService { get; set; }

		[Dependency]
		public IRepository2<Guide> GuideService { get; set; }

		[Dependency]
		public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

		[Dependency]
		public IRepository2<MarketingAction> MarketingActionService { get; set; }

		[Dependency]
		public IRepository2<Employee> EmployeeService { get; set; }

		[Dependency]
		public IEntityCommonService EntityCommonService { get; set; }

		[Dependency]
		public INewsService NewsService { get; set; }

		[Dependency]
		public ISiteObjectRelationService SiteObjectRelationService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public ExpressOrderService ExpressOrderService { get; set; }

		[Dependency]
		public ISiteObjectService SiteObjectService { get; set; }

		[Dependency]
		public IUserWorkService UserWorkService { get; set; }

		[Dependency]
		public IAnnounceService AnnounceService { get; set; }

		[Dependency]
		public ISectionService SectionService { get; set; }

		[Dependency]
		public IRepository2<Poll> PollService { get; set; }

		[Dependency]
		public ISimplePageVMService SimplePageVMService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public IUserSettingsService UserSettingsService { get; set; }

		[Dependency]
		public IBannerService BannerService { get; set; }

		[Dependency]
		public IRepository<Vacancy> VacancyService { get; set; }

		[Dependency]
		public IGroupService GroupService { get; set; }

		[Dependency]
		public IGroupVMService GroupVMService { get; set; }

		[Dependency]
		public ISimplePageService SimplePageService { get; set; }

		[Dependency]
		public GeoService GeoService { get; set; }

		[Dependency]
		public IRepository2<CityInfo> CityInfoService { get; set; }

		public ActionResult Process(string url) {
			if (url.EndsWith(".jpg") || url.EndsWith(".gif") || url.EndsWith(".png"))
				return null;
			if (url.StartsWith("job/vacancy")) {
				return Redirect("http://specialist.staya.vc/");
			}
			var model = SimplePageVMService.GetByUrl(url, this);
			if (model == null) {
				SpecLogger.NotFound(Request.Url.PathAndQuery);
				return NotFound(Request.Url.PathAndQuery);
			}
			if (Request.IsAjaxRequest()) {
				var spVM = model.As<SimplePageVM>();
				return Content(H.h1[spVM.Title] + spVM.Description.FirstPart);
			}
			return MView(ViewNames.SimplePage, model);
		}


		public ActionResult SendForWebMaster(string title) {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = title
			});
		}

		public ActionResult TestResponse(string title) {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = title,
				Type = SendMessageVM.TestResponse
			});
		}
		public ActionResult SendInviteManager() {
			var model = new SendMessageVM {
				Type = SendMessageVM.InviteManager
			};
			model.Title = "Приглашение на переговоры";
			return BaseView(Views.Page.SendMessage, model);
		}

		public ActionResult GetPromocode(string title) {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = title,
				Type = SendMessageVM.Promocode
			});
		}

		[Authorize]
		public ActionResult CourseIdea() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Предложение по созданию нового курса",
				Type = SendMessageVM.CourseIdea
			});
		}

		public ActionResult MobileAppReview() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Отзыв о мобильном приложении",
				Type = SendMessageVM.MobileAppReview
			});
		}

		public ActionResult CatalogIdea() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Предложение по каталогу",
				Type = SendMessageVM.CatalogIdea
			});
		}

		public ActionResult WebinarSpecial() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Я хочу узнать про особые условия для обучения группы через вебинар",
				Type = SendMessageVM.WebinarSpecial
			});
		}
		public ActionResult CourseTender() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Хочу узнать про заказ обучения через тендер/аукцион",
				Type = SendMessageVM.CourseTender
			});
		}

		public ActionResult EnglishOrder(int id) {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Хочу изучать английский язык",
				Type = SendMessageVM.EnglishOrder,
				CustomValue = id.ToString()
			});
		}


		public ActionResult DevIdea() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Пожелание разработчикам",
				Type = SendMessageVM.DevIdea
			});
		}

		public ActionResult JobVacancy() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Вакансия",
				Type = SendMessageVM.JobVacancy
			});
		}
		public ActionResult JobManager() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Сообщение менеджеру службы трудоустройства",
				Type = SendMessageVM.JobManager
			});
		}

		public ActionResult CareerDay() {
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Title = "Заявка на участие в дне карьеры",
				Type = SendMessageVM.CareerDay
			});
		}

		[HttpPost]
		public ActionResult SendForWebMaster(SendMessageVM model) {
			if (isSpam(model.Message)) {
				return null;
			}
			if (model.Type.In(SendMessageVM.InviteManager,
				SendMessageVM.SendToLeader)) {
				MailService.SendForOrgManager(model.Message, model.Title);
			}
			else {
				MailAddress copy = null;
				if (model.Email.IsEmpty() && User != null)
					model.Email = User.Email;
				if (User != null) model.SenderName = User.FullName;
				var email = Services.Common.MailService.contmanagers;
				switch (model.Type) {
					case SendMessageVM.Promocode:
						email = Services.Common.MailService.promocode;
						break;
					case SendMessageVM.ForManager:
						copy = Services.Common.MailService.info;
						email = new MailAddress(
							EmployeeService.GetByPK(model.EmployeeTC).FirstEmail);
						break;
					case SendMessageVM.CourseIdea:
						email = Services.Common.MailService.courseIdea;
						break;
					case SendMessageVM.MobileAppReview:
						email = Services.Common.MailService.motorina;
						copy = Services.Common.MailService.ptolochko;
						break;
					case SendMessageVM.CatalogIdea:
						email = Services.Common.MailService.site;
						break;
					case SendMessageVM.TestResponse:
						email = Services.Common.MailService.testresponse;
						break;
					case SendMessageVM.WebinarSpecial:
						email = Services.Common.MailService.info;
						break;
					case SendMessageVM.CourseTender:
						email = Services.Common.MailService.corporatedepartment;
						break;
					case SendMessageVM.EnglishOrder:
						email = Services.Common.MailService.karpovich;
						model.Message += "<br/>" + Url.TestRun().Result(
							StringUtils.ParseInt(model.CustomValue).GetValueOrDefault(),
							"Ссылка на результат тестирования").AbsoluteHref();
						break;
					case SendMessageVM.DevIdea:
						email = Services.Common.MailService.devidea;
						break;
					case SendMessageVM.JobVacancy:
						email = Services.Common.MailService.job;
						break;
					case SendMessageVM.JobManager:
						email = Services.Common.MailService.job;
						break;
					case SendMessageVM.CareerDay:
						email = Services.Common.MailService.job;
						break;
					default:
						copy = Services.Common.MailService.ptolochko;
						break;
				}
				MailService.MessageFromSite(model, email, copy);
			}
			return AjaxOk();
		}


		static bool isSpam(string text) {
			return !text.IsEmpty() && text.Contains("[b][url=");
		}

		[Authorize]
		public ActionResult SendForManager(string tc) {
			var manager = EmployeeService.GetByPK(tc);
			if (manager.IsTrainer) {
				manager = EmployeeService.GetByPK(Employees.GetKarpovich());
			}
			if (manager.FirstEmail.IsEmpty()) {
				throw new Exception("email is empty " + tc);
			}
			return BaseView(Views.Page.SendMessage, new SendMessageVM {
				Type = SendMessageVM.ForManager,
				EmployeeTC = manager.Employee_TC,
				Employee = manager
			});
		}



		[HttpPost]
		[AjaxOnly]
		public ActionResult SendExpressOrder(ExpressOrderVM model) {
			if (model.ExpressCaptcha != UserSettingsService.CaptchaText) {
				UserSettingsService.CaptchaText = null;
				return Json("captcha");
			}
			if (model.Subscibe) {
				var error = PiStudentEmailService.SaveEmail(model.Name, model.Contact);
				if (!error.IsEmpty()) {
					return Json(new {error});
				}
				UserSettingsService.CaptchaText = null;
				return AjaxOk();
			}
			UserSettingsService.CaptchaText = null;
			if (model.CourseTC.IsEmpty())
				model.StudentInGroupId =
					ExpressOrderService.CreateOrder(model.Name, model.Contact);
			MailService.ExpressOrder(model);
			return AjaxOk();
		}

		[HttpPost]
		[AjaxOnly]
		public ActionResult SendQuickRegistration(QuickRegisterVM model) {
			if (model.Captcha != UserSettingsService.CaptchaText) {
				UserSettingsService.CaptchaText = null;
				return Json("captcha");
			}
			model.Email = model.Email.Trim();
            if (FluentValidate(model)) {
	            var user = new User {
		            Email = model.Email,
					FirstName = CommonTexts.EmptyFirstName,
					IsQuick = true,
					Password = Membership.GeneratePassword(6, 0),
	            };
				UserService.CreateUser(user);
				AuthService.SignOut();
                AuthService.SignIn(user.Email, true);
	            OrderService.UpdateSessionOrderUser();
				UserSettingsService.CaptchaText = null;
				return AjaxOk();
            }
			var error = ModelState.Values.SelectMany(x => x.Errors)
				.Select(x => x.ErrorMessage).JoinWith("<br/>");
			return Content(error);
		}


		private ActionResult AjaxOk() {
			if (Request.IsAjaxRequest())
				return Json("ok");
			return RedirectBack();
		}


		[AjaxOnly]
		[HttpPost]
		public ActionResult SendMessage(string text, string url) {
			MailService.SendMisprint(text + " " + H.Anchor(url));
			return Json("done");
		}

		private static BingSearchContainer bingSearch = null;

		private static List<string> bingAppIds = _.List(
			"tSJNKjLEJ9H4fv3mN9DmN/pw7fr41zF7ulCmVNKAImY=" 
			,"5h8l6dvD6ARSnvLIPjDtRe0kGL5+A2GtFkpa69YIrYk="
			,"NLFZPRB7rCfP3lvuv81vCOwjUmJfcsQGEdematblsq0="
			,"yKJBRCt07iFCsiaBs2WMdt/FyxnuCgWAMRpHgFt/URA="
			);

		private static int CurrentBingId = 0;

		static PageController() {
			InitBingSearch();
		}

		private static void InitBingSearch() {
			if(CurrentBingId < bingAppIds.Count) {
				bingSearch =
					new Bing.BingSearchContainer(
						new Uri("https://api.datamarket.azure.com/Data.ashx/Bing/Search/v1"));
				var appId = bingAppIds[CurrentBingId];
				bingSearch.Credentials = new NetworkCredential(appId, appId);
				
			}else {
				bingSearch = null;
				Logger.Exception(new Exception("bingSearch == null"), "bingSearch");
			}
		}

		private static readonly object _bingSearchLock = new object();

		private string GetSpellSuggesion(string text) {
			if(bingSearch == null)
				return null;
			lock (_bingSearchLock) {
				try {
					var r = bingSearch.SpellingSuggestions(text, null, "ru-RU", "Moderate",
						null, null).Execute().ToList();
					if (r.Any())
						return r.First().Value;
				}
				catch (Exception e) {
					CurrentBingId++;
					InitBingSearch();

				}
				return null;
			}
		}
	

		public ActionResult TopSearch() {
			var list = Specialist.Entities.Const.TopSearch.List;
			return Content(H.div[list.Select(x => Url.Link<PageController>(c => c.Search(x.Item1,1), x.Item1)
				.Style("font-size:{0}px;".FormatWith(10 + x.Item2 * 2)))].Style("text-align: center;").ToString());
		}

	public ActionResult Search(string text, int? pageIndex) {
			if (!pageIndex.HasValue)
				return RedirectToAction(() => Search(text, 1));
/*
			if (text == "c#") {
				text = "c sharp";
			}
			var gis = new GWebSearch();

			var isa = new WebSearchArgs();

			isa.ApiKey = GoogleApiKey;
			isa.ResultLang = SearchResultLang.Russian;
			isa.StartIndex = (pageIndex.GetValueOrDefault() - 1)*SearchVM.PageSize;
			isa.Terms = "site:http://www.specialist.ru " + text;

			isa.ResultSize = SearchResultSize.Large;
*/
			var model = new SearchVM();
/*
			model.PageIndex = pageIndex.GetValueOrDefault();
			model.Text = text;
			try {
				model.ResponseData = gis.Search(isa).Response;
			}
			catch (Exception e) {
				Logger.Exception(e,User);
			}
			if(model.PageIndex == 1 && !text.IsEmpty()) {
				model.Suggestion = GetSpellSuggesion(text);
			}
*/


			return View(model);
		}

		public ActionResult HotGroupsFor(object obj) {
			var model = obj == null 
				? new List<Announce>()  
				: AnnounceService.GetAllFor(obj).Take(CommonConst.AnnounceCount).ToList();
			if (!model.Any()) {
				ViewData["ForMain"] = true;
				model = AnnounceService.GetAllForMainCached();
			}
			return View(PartialViewNames.HotGroupBlock, model);
		}

		[AjaxOnly]
		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		public ActionResult HotGroupsForMain(string viewName) {
			var announces = GroupVMService.GetAllForMain();
			var model = new GroupsForMainVM();
			model.Groups = announces;
			return View(PartialViewNames.HotGroupsForMain, model);
		}

		public ActionResult VideoFor(object obj) {
			var model = new List<Video>();
			if(obj is Course)
				model = SiteObjectService.GetDoubleRelation<Video>(obj)
					.IsActive().ToList();
			else
				model = SiteObjectService.GetByRelationObject<Video>(obj)
					.IsActive().ToList();
			return View(PartialViewNames.VideoBlock, 
				model.OrderByDescending(x => x.VideoID).ToList());
		}

		public ActionResult GuideFor(object obj) {
			var model = new List<Guide>();
			var track = obj.As<Course>();
			var ids = new List<object>();
			if(track != null && track.IsTrackBool) {
				var courseTCs = CourseService.GetActiveTrackCourses()
					.GetValueOrDefault(track.Course_TC) ?? new List<string>();
				ids = courseTCs.Cast<object>().ToList();
			}else {
				ids = _.List(LinqToSqlUtils.GetPK(obj));
			}
			if(ids.Any()) {
				var guideIds =SiteObjectRelationService
						.GetRelation(typeof (Guide), Enumerable.Empty<object>(), 
						obj.GetType()).Where(x => ids.Contains(x.RelationObject_ID))
						.OrderBy(x => x.RelationOrder)
						.Select(x => x.Object_ID).ToList().Cast<int>().ToList();
				model = GuideService.GetAll(x => 
					guideIds.Contains(x.GuideID) && x.IsActive).Distinct().ToList()
					.OrderBy(x => guideIds.IndexOf(x.GuideID)).ToList();
			}
			return View(PartialViewNames.GuidesBlock, model.ToList());
		}


		public ActionResult VacanciesFor() {
			var vacancies = VacancyService.GetAll().IsActive().OrderByDescending(v =>
				v.IsHot
				).OrderByDescending(v => v.PublishDate).Take(CommonConst.MaxHotVacansyCount);
			if (vacancies.Any(v => v.IsHot))
				vacancies = vacancies.Where(v => v.IsHot);
			return View(PartialViewNames.VacanciesBlock, vacancies);
		}

		public ActionResult NewsFor(object obj) {
			var model = NewsService.GetFor(obj);
			if (model.Any()) {
				return View(PartialViewNames.NewsBlock, new NewsBlockVM {News = model, Entity = obj});
			}
			return NewsForMain();
		}

		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		public ActionResult NewsForMain() {
			var news = NewsService.GetAllForMain().Take(CommonConst.NewsCount).ToList();
			return View(PartialViewNames.NewsBlock, new NewsBlockVM {News = news});
		}

		public List<int> ActivePollIds() {
			return PollService.GetAll(x => x.IsActive).Select(x => x.PollID).OrderBy(x => x).ToList();
		}
		[AjaxOnly]
		public ActionResult GetPoll(int? id) {
//			id = id.GetValueOrDefault(54);
/*
			if (!id.HasValue) {
				id = PollService.FirstOrDefault(x => x.IsActive, x => x.PollID);
				if (id == 0) {
					return null;
				}
			}
*/
			PollService.LoadWith(x => x.PollOptions);
			var activePolls = ActivePollIds();
			var poll = id > 0 ? PollService.GetByPK(id.Value): PollService.GetByPK(activePolls.FirstOrDefault());
			if (poll == null)
				return null;
			var index = activePolls.IndexOf(poll.PollID);
			var nextId = index < 0 || index == (activePolls.Count - 1) ? 0 : activePolls[index + 1];
			var model = new PollVM {
				Poll = poll,
				NextPollId = nextId
			};
			return View(PartialViewNames.PollBlock, model);
		}


		[AjaxOnly]
		[OutputCache(Duration = 60*60, VaryByCustom = CacheManager.Poll)]
		public ActionResult Poll() {
			return GetPoll(null);
		}

		[AjaxOnly]
		public ActionResult PollVote(int pollOptionID, string pollAnswer, int pollID) {
			pollAnswer = StringUtils.SafeSubstring(pollAnswer, 500);
			PollService.EnableTracking();
			PollService.LoadWith(x => x.PollOptions);
			var poll = PollService.GetByPK(pollID);
			var option = poll.PollOptions
				.FirstOrDefault(po => po.PollOptionID == pollOptionID);
			if (option != null) {
				option.VoteCount += 1;
				PollService.SubmitChanges();
				var nextPolls = ActivePollIds().Skip(1);
				if (!nextPolls.Contains(pollID)) {
					UserSettingsService.VotedPollID = poll.PollID;
				}
				MailService.SendPollAnswer(poll, pollAnswer);
			}
			CacheManager.Update(CacheManager.Poll);
			return Json("done");
		}

		public ActionResult Banner() {
			var url = CommonConst.SiteRoot + Request.Url.AbsolutePath;
			var banners = BannerService.GetBanner(url).ShufflePriority(x => x.Priority);
			if (!banners.Any())
				return null;
			return Content(SiteHtmls.Banners(banners));
		}

		public List<Banner> FilterBanner(List<Banner> banners) {
			if (User == null || !User.IsStudent) {
				return banners;
			}
			var banner = banners.FirstOrDefault(x => Banners.Target.ContainsKey(x.Banner_ID));
			if (banner == null) {
				return banners;
			}
			var directories = Banners.Target[banner.Banner_ID];
			var show = StudentInGroupService.GetAll(x => x.Student_ID == User.Student_ID
				&& directories.Contains(x.Group.Course.CourseDirectionA_TC))
				.Any();
			if (show) {
				return _.List(banner);
			}
			return new List<Banner>();
		}

//		public ActionResult SideBanner() {
//			var banner = BannerService.GetSideBanner();
//			if (banner == null)
//				return null;
//			return View(banner);
//		}

		public ActionResult NotFound(string aspxerrorpath) {
			return ErrorView(HttpStatusCode.NotFound, aspxerrorpath);
		}


		[ValidateInput(false)]
		public ActionResult Error(string aspxerrorpath) {
			return ErrorView(HttpStatusCode.InternalServerError, aspxerrorpath);
		}

		public ActionResult AskTimetable() {
			return View(new ExpressOrderVM(AuthService.CurrentUser));
		}

		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		[ChildActionOnly]
		public ActionResult StudyTypes() {
			var pages = SimplePageService.GetAll().BySysName(SimplePages.EducationTypes)
				.Children.OrderBy(x => x.WebSortOrder).ToList();
			return View(PartialViewNames.StudyTypes, pages);
		}

		[ChildActionOnly]
		public ActionResult UserWorksFor(object obj) {
			var userWorks = new List<UserWork>();
			if (obj is Section) {
				userWorks = UserWorkService.GetAllRandomForSection(
					obj.As<Section>().Section_ID)
					.Take(3).ToList();
			}
			else if (obj is Course) {
				userWorks = UserWorkService.GetAllRandomForBlock()
					.Where(uw => uw.Course_TC.Contains(
						obj.As<Course>().Course_TC)).Take(3).ToList();
			}
			else if (obj is Product) {
				var userWorkSectionId = UserWorkSections.Products.GetValueOrDefault(
					obj.As<Product>().Product_ID);
				if (userWorkSectionId > 0)
					userWorks = UserWorkService.GetAllRandomForBlock()
						.Where(uw => uw.WorkSectionID == userWorkSectionId).Take(3).ToList();
			}
			return View(PartialViewNames.UserWorkBlock, userWorks);
		}

		[AjaxOnly]
		[HttpPost]
		public ActionResult JavaScriptError(string msg, string url, string line) {
			SpecLogger.JsError(msg);
			return null;
		}


		public ActionResult NearestGroupMobile() {
			var groups = GetMobileGroups();
			return Content(Html.Site().MobileGroups(groups).NotNullString());
		}

		private List<Group> GetMobileGroups() {
			return CacheUtils.Get(MethodBase.GetCurrentMethod(),
				() => GroupVMService.GetAllForMain());
		}

//		public ActionResult CityBlock() {
//			var city = GeoService.GetCityName();
//			var cityDescs = MethodBase.GetCurrentMethod().CacheDay(() => 
//				CityInfoService.GetAll().ToDictionary(x => x.Name, x => x.Description));
//			var desc =cityDescs.GetValueOrDefault(city);
//			if (!desc.IsEmpty()) {
//				desc = Htmls2.Cham(desc);
//			}
//			return Content(desc);
//
//		}

		public ActionResult CityCoupon() {
//			if (!UserSettingsService.HideCityCoupon
//				&& !Request.IsAuthenticated && GeoService.CheckCityCoupon()) {
//				UserSettingsService.HideCityCoupon = true;
//				return View(Views.Shared.Common.CityCoupon);
//			}
			return null;
		}

		public Tuple<int> ActionCount() {
			return MethodBase.GetCurrentMethod().Cache(() => Tuple.Create(MarketingActionService.GetAll(x => x.IsActive).Count()));
		}

		public ActionResult MainMenu() {
			return View(Tuple.Create(Const.MainMenu.GetAll(), ActionCount().Item1));
		}

		[HttpPost]
		public ActionResult SendFormTo(string sendformemail) {
			if (!sendformemail.EndsWith("@specialist.ru")) return null;
			var ResponseKey = "ResponseMessage";
			var emailKey = "sendformemail";
			var subjectKey = "subject";
			var message = Request.Form[ResponseKey] ?? "Данные отправлены";
			var subject = Request.Form[subjectKey] ?? "Опрос";
			var all = _.List(ResponseKey, emailKey, subjectKey);
			var body = Request.Form.AllKeys.Where(x => !all.Contains(x)).Select(key => {
				var value = Request.Form[key];
				return H.l(H.b[key + ": "], value).ToString();
			}).JoinWith("<br/>");
			var files = new List<UploadFile>();
			foreach (var fileKey in Request.Files.AllKeys) {
				var file = Request.Files[fileKey];
				files.Add(new UploadFile {
					Name = Path.GetFileName(file.FileName),
					ContentLength = file.ContentLength,
					Stream = file.InputStream
				});
			}
			if (files.Any()) {
				try {
					MailService.SendSync(Services.Common.MailService.info, 
						new MailAddress(sendformemail) , body, subject, files);
				} catch (Exception e) {
					Logger.Exception(e, User);
					ShowErrorMessage("Не удалось отправить сообщение. Вы можете попробовать отправить его на прямую на адрес " + H.Email(sendformemail));
					return RedirectBack();
				}
				
			} else {
				MailService.Send(Services.Common.MailService.info, 
					new MailAddress(sendformemail), body, subject);
				
			}
			ShowMessage(message);
			return RedirectBack();

		}

	}
}
