using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;
using SimpleUtils.Collections;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Common;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Center.Extension;
using Specialist.Services.Common.Extension;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Pages;
using Specialist.Web.Root.Center.ViewModels;
using Specialist.Web.Root.Center.Views;
using Specialist.Web.Root.Contents.ViewModels;
using Specialist.Web.Util;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Services.Common.Utils;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Web.Helpers;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Controllers.Center {
	public class CenterController : ViewController {

		[Dependency]
		public IRepository<Vacancy> VacancyService { get; set; }

		[Dependency]
		public IRepository<Advice> AdviceService { get; set; }

		[Dependency]
		public IStudentService StudentService { get; set; }

		[Dependency]
		public IPriceService PriceService { get; set; }
		[Dependency]
		public IRepository2<Video> VideoService { get; set; }
		[Dependency]
		public IRepository2<VideoCategory> VideoCategoryService { get; set; }

		[Dependency]
		public IRepository2<Discount> DiscountService { get; set; }

		[Dependency]
		public IRepository<SimplePage> SimplePageService { get; set; }

		[Dependency]
		public IRepository<UsefulInformation> UsefulInformationService { get; set; }

		[Dependency]
		public IRepository<MarketingAction> MarketingActionService { get; set; }

		[Dependency]
		public IRepository<Poll> PollService { get; set; }

		[Dependency]
		public IEmployeeService EmployeeService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public ISiteObjectService SiteObjectService { get; set; }

		[Dependency]
		public ISiteObjectRelationService SiteObjectRelationService { get; set; }

		[Dependency]
		public ISectionService SectionService { get; set; }

		[Dependency]
		public ISectionVMService SectionVMService { get; set; }

		[Dependency]
		public ICourseVMService CourseVMService { get; set; }
		[Dependency]
		public ICertificationService CertificationService { get; set; }

		[Dependency]
		public IResponseService ResponseService { get; set; }

		[Dependency]
		public IRepository<Competition> CompetitionService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public IRepository<OrgResponse> OrgResponseService { get; set; }

		[Dependency]
		public IGroupService GroupService { get; set; }

		[Dependency]
		public IUserSettingsService UserSettingsService { get; set; }

		[Authorize]
		public ActionResult DownloadRegister(string file) {
			return Redirect(Urls.ForRegister(file));
		}

		[HandleNotFound]
		public ActionResult Vacancy(int vacancyID) {
			var vacancy = VacancyService.GetByPK(vacancyID);
			if (vacancy == null || !vacancy.IsActive)
				return null;
			return View(new VacancyVM {Career = GetCareer(), Vacancy = vacancy});
		}

		public ActionResult Advice(int adviceID, string urlName) {
			var advice = AdviceService.GetByPK(adviceID);
			if (advice == null) {
				return NotFound();
			}
			return View(new AdviceVM {Advice = advice});
		}

		[AjaxOnly]
		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		public ActionResult AdviceBlock() {
			return View(PartialViewNames.AdvicesBlock, ResponseService.GetRandomAdvices());
		}

		public ActionResult Advices(int? index) {
			if (!index.HasValue)
				return RedirectToAction(() => Advices(1));

			var advices = AdviceService.GetAll().IsActive()
				.OrderByDescending(x => x.AdviceID)
				.ToPagedList(index.Value - 1);
			return View(new AdviceListVM {
				Advices = advices,
			});
		}

		public ActionResult Polls(int? index) {
			if (!index.HasValue)
				return RedirectToAction(() => Polls(1));

			var polls = PollService.GetAll()
				.Where(p => p.PollOptions.Any(po => po.VoteCount > 0))
				.OrderByDescending(x => x.PollID)
				.ToPagedList(index.Value - 1);
			return View(new PollListVM() {
				Polls = polls,
			});
		}


		public ActionResult Vacancies(int? index) {
			if (!index.HasValue)
				return RedirectToAction(() => Vacancies(1));

			var vacancies = VacancyService.GetAll().IsActive()
				.Where(x => !x.IsPartner && x.Type != CenterVacancyType.Probation)
				.OrderByDescending(v => v.PublishDate).ToPagedList(index.Value - 1);
			return View(ViewNames.Vacancies, new VacancyListVM {
				Vacancies = vacancies,
				Career = GetCareer(),
			});
		}

		public ActionResult VacanciesForTeacher(int? index) {
			if (!index.HasValue)
				return RedirectToAction(() => VacanciesForTeacher(1));

			var vacancies = VacancyService.GetAll().IsActive().Where(x => !x.IsPartner)
				.Where(v => v.Type == CenterVacancyType.Teacher)
				.OrderByDescending(v => v.PublishDate).ToPagedList(index.Value - 1);
			return View(ViewNames.Vacancies, new VacancyListVM {
				Vacancies = vacancies,
				Career = GetCareer(),
			});
		}

		public ActionResult VacanciesForEmployee(int? index) {
			if (!index.HasValue)
				return RedirectToAction(() => VacanciesForEmployee(1));

			var vacancies = VacancyService.GetAll().IsActive().Where(x => !x.IsPartner)
				.Where(v => v.Type == CenterVacancyType.Emloyee)
				.OrderByDescending(v => v.PublishDate).ToPagedList(index.Value - 1);
			return View(ViewNames.Vacancies, new VacancyListVM {
				Vacancies = vacancies,
				Career = GetCareer(),
			});
		}


/*
		public ActionResult PartnerVacancies(int? index) {
			if (!index.HasValue)
				return RedirectToAction(() => PartnerVacancies(1));

			var vacancies = VacancyService.GetAll().IsActive()
				.Where(v => v.IsPartner)
				.OrderByDescending(v => v.PublishDate).ToPagedList(index.Value - 1);
			return View(ViewNames.Vacancies, new VacancyListVM {
				IsPartner = true,
				Vacancies = vacancies
			});
		}
*/

		private SimplePage GetCareer() {
			return SimplePageService.GetAll().BySysName(SimplePages.Career);
		}

		[HandleNotFound]
		public ActionResult MarketingAction(string urlName) {
			var action = MarketingActionService.FirstOrDefault(x => x.IsActive && x.UrlName == urlName);
			if(action == null)
				return null;
			if (action.MarketingAction_ID == Specialist.Entities.Const.MarketingActions.Unlimit) {
				var button = SimplePageVMService.GetUnlimitOrderButton(Url, User);
				action.Description = TemplateEngine.GetText(action.Description, new {OrderButton = button.ToString()});
			}
			var page = action.IsSpecialOffer ? SimplePages.SpecialActions : SimplePages.MarketingActions;
			return MView(Views.Center.MarketingAction, new MarketingActionVM {
				MarketingAction = action,
				MarketingActions = SimplePageService.GetAll().BySysName(page),
			});
		}

		[Authorize]
		public ActionResult MarketingActions() {
            //TODO: Спецпредложения https://spoint.specialist.ru/departments/projects/_layouts/listform.aspx?PageType=4&ListId={11B73C8A-2110-40C3-8169-06E92016FE8D}&ID=3435&ContentTypeID=0x010800B6F61EE8E28A8748843BA6EA5E5FB40A

            //this.StudentService.Get

            //User.St

            var actions = User.IsStudent //&& Students.Best2016.Contains(User.Student_ID.Value)
				? MarketingActionService.GetAll(a=> a.IsActive
				&& a.IsAdvert && !a.IsSecret && !a.IsSpecialOffer && !a.IsOrg).OrderByDescending(x=>x.UpdateDate).ToList()
                //&& x.MarketingAction_ID == Specialist.Entities.Const.MarketingActions.Best2016).ToList()
                : new List<MarketingAction>();
			return BaseView(Views.Center.MarketingActions, new MarketingActionsVM{Actions = 
				actions.OrderBy(x => x.WebSortOrder).ToList()});
		}

		public ActionResult UsefulInformation(string urlName) {
			var usefulInformation = UsefulInformationService.GetAll().ByUrlName(urlName);
			return View(new UsefulInformationVM {
				UsefulInfo = usefulInformation,
				UsefulInformation =
					SimplePageService.GetAll().BySysName(SimplePages.UsefulInformation)
			});
		}

		public ActionResult Certificate(string urlName) {
			return null;
		}

		public ActionResult Skype() {
			var employees = EmployeeService.GetAll().Where(e =>
				Employees.Skype.Contains(e.Employee_TC))
				.CommonList()
				.ToList();
			return View(CommonVM.New(employees, "Онлайн-консультация"));
		}


		[HandleNotFound]
		public ActionResult CourseTrainers(string urlName) {
			var course = CourseService.GetByUrlName(urlName);

			if (course == null) {
				return null;
			}
			var today = DateTime.Today;
			var employees = course.EmployeesCourses.Where(x => x.DateTo >= today
				&& x.IsActive)
				.Select(x => x.Employee).Where(x => x.SiteVisible).ToList();
			var title = "Все преподаватели курса " + "\"" + course.Name + "\"";
			return View(ViewNames.Trainers, CommonVM.New(employees, title));
		}

		[HandleNotFound]
		public ActionResult SectionResponses(string urlName) {
			var section = SectionService.GetAll().ByUrlName(urlName);
			if(section == null)
				return null;
			var responses = SectionVMService
				.GetResponses(section.Section_ID)
				.Where(rr => rr.Type == RawQuestionnaireType.CourseComment)
				.OrderByDescending(r => r.UpdateDate)
				.Take(20).ToList();

			var title = "Отзывы по направлению  \"" + section.Name + "\"";

			return View(ViewNames.Responses, CommonVM.New(responses, title));
		}

		[HandleNotFound]
		public ActionResult CourseResponses(string urlName, int? pageIndex) {
            if (!pageIndex.HasValue)
                return RedirectToAction(() => CourseResponses(urlName, 1));
			var course = CourseService.GetAll(c => c.IsActive && c.UrlName == urlName)
				.OrderBy(x => x.Course_ID).FirstOrDefault();
			if (course == null)
				return null;
			var responses = ResponseService.GetAllForCourse(course.Course_TC)
				.OrderByDescending(x => x.Rating)
				.ThenByDescending(x => x.UpdateDate).ToPagedList(pageIndex.GetValueOrDefault(1) - 1,10);
			var orgResponses =
				OrgResponseService.GetAll(r => r.Course_TC == course.Course_TC)
					.Take(20).ToList();
			var model = new CourseResponsesVM {
				Course = course,
				Responses = responses,
				OrgResponses = orgResponses
			};
			return View(ViewNames.CourseResponses, model);
		}
		[HandleNotFound]
		public ActionResult SectionTrainers(string urlName) {
			var section = SectionService.GetAll().ByUrlName(urlName);
			if(section == null)
				return null;
			var courseTCs = CourseVMService
				.GetCourseTCListForTotalSection(section.Section_ID);

			var groups = GroupService.GetGroupsForCourses(courseTCs)
				.Where(g => g.Teacher_TC != null).ToList();
			var groupCountByTrainers = groups.GroupBy(x => x.Teacher_TC)
				.ToDictionary(x => x.Key, x => x.Count());
			var trainers = groups 
				.Select(x => x.Teacher)
				.Distinct(x => x.Employee_TC)
				.Select(x => new {Teacher = x,
					Count = groupCountByTrainers.GetValueOrDefault(x.Employee_TC)})
				.Where(x => x.Teacher.FinalSiteVisible && x.Count > 0)
				.OrderByDescending(x => x.Count).Select(x => x.Teacher)
				.ToList();

			return View(ViewNames.Trainers, CommonVM.New(trainers, "Преподаватели направления: " + section.Name));
		}

		public ActionResult CertificationTrainers(string urlName) {
			var trainers = CertificationService.GetAll().ByUrlName(urlName).EmployeeCertifications.Select(ec => ec.Employee)
				.Where(e => e.SiteVisible).ToList();
			return View(ViewNames.Trainers, CommonVM.New(trainers, "Все преподаватели сертификации"));
		}

		public ActionResult TrainerSections() {
			var rootSections = SectionService.GetTreeWithSubsections();

			return View(rootSections);
		}

		public ActionResult TrainerCertifications() {
			return View();
		}

		public ActionResult Competitions() {
			var model = new CompetitionsVM(
				CompetitionService.GetAll()
					.OrderBy(x => x.WebSortOrder).IsActive());
			//            model.CurrentUserID = User.UserID;
			return View(ViewNames.Competitions, model);
		}

		[ModelStateToTempData]
		[Authorize]
		public ActionResult Competition(int competitionID) {
			var competition = CompetitionService.GetByPK(competitionID);

			return View(ViewNames.Competition,
				new CompetitionVM {
					Competition = competition,
	                    IsJoin = competition.UserCompetitions.Any(uc => uc.UserID == User.UserID),
					IsWinner = competition.WinnerID == User.UserID,
				});
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ModelStateToTempData]
		public ActionResult Competition(CompetitionVM model) {
			model.UploadFile = UserFiles.GetUploadFile(Request.Files[0]);

			if (FluentValidate(model)) {
				var competition =
					model.Competition
						= CompetitionService.GetByPK(model.Competition.CompetitionID);
				if (competition.UserCompetitions.All(uc => uc.UserID != User.UserID))
					competition.UserCompetitions.Add(
						new UserCompetition {UserID = User.UserID});
				CompetitionService.SubmitChanges();
				MailService.SendCompetitionRequest(model);
			}
			return RedirectBack();
		}

		public ActionResult JubileeForm() {
			return BaseView(Views.Center.JubileeForm, new JubileeFormVM());
		}

		[HttpPost]
		public ActionResult JubileeFormPost(JubileeFormVM model) {
			var message = _.List(model.FullName, model.CompanyName, model.Message, model.VideoLink)
				.JoinWith(H.br.ToString());
			var fileName = Session[SessionJubileeFileKey].NotNullString();
			if(!fileName.IsEmpty())
				fileName = UserImages.GetJubileeFileSys(fileName);
			MailService.SendJubilee(message, fileName);
			return Content("ok");
		}

		public ActionResult SeminarRegistration(decimal groupId) {
			var gr = GroupService.GetByPK(groupId);
			if(!gr.WebinarExists && gr.GroupCalc.NumOfStudents >= gr.MaxNumOfStudents)
				return BaseView(new PagePart("Регистрация закрыта"));
			return BaseView(Views.Center.SeminarRegisterForm, 
				new SeminarRegisterFormVM{Group = gr});
		}

		public ActionResult MtsEmployee() {
			var model = new MtsEmployeeFormVM();
			var dates = MtsEmployeeFormVM.CourseTCList.ToDictionary(x => x,
				x => GroupService.GetGroupsForCourse(x).Select(y => y.DateInterval + " " +
					StringUtils.AngleBrackets( y.Complex.Name) + " " + y.Complex.Address).Distinct()
					.Select(z => new {id=z, name=z}.As<object>()).ToList());
			model.Courses = CourseService.GetCourseLinkList(MtsEmployeeFormVM.CourseTCList, false).ToList();
			model.Dates = dates;
			return BaseView(Views.Center.MtsEmployeeForm, model);
		}

		[HttpPost]
		public ActionResult MtsEmployeePost(MtsEmployeeFormVM model) {
			var data =
				SimpleUtils.FluentAttributes.Utils.EntityUtils.ToStrings(model,
					x => x.FullName,
					x => x.Organization,
					x => x.Email,
					x => x.Phone,
					x => x.Number,
					x => x.Course,
					x => x.Date
					);
			var message = DictionaryUtils.ToHtml(data);
			MailService.SendSeminarRegistration(message);
			MailService.Send(Services.Common.MailService.info, 
				Services.Common.MailService.secrko,message, "Сотрудник МТС",Services.Common.MailService.akavinkina);
			return Content("ok");
		}



		public ActionResult OrgCatalog() {
			return BaseView(Views.Center.OrgCatalogForm, new OrgCatalogFormVM());
		}


		public ActionResult OrgCatalogPost(OrgCatalogFormVM model) {
			var data =
				SimpleUtils.FluentAttributes.Utils.EntityUtils.ToStrings(model,
					x => x.CompanyName,
					x => x.FullName,
					x => x.Position,
					x => x.Index,
					x => x.Address,
					x => x.Email,
					x => x.Phone,
					x => x.Count
					);
			var data2 =
				SimpleUtils.FluentAttributes.Utils.EntityUtils.ToStrings(model,
					x => x.SmFullName,
					x => x.SmEmail,
					x => x.SmPhone
					);
			var message = DictionaryUtils.ToHtml(data) + 
				(model.IamStudyManager ? "<br/>Решение по обучению принимаю я" : DictionaryUtils.ToHtml(data2));
			MailService.Send(Services.Common.MailService.info, 
				Services.Common.MailService.corporatedepartment,message, "Заказ каталога курсов");
			return Content("ok");
		}


		public ActionResult OrderPaperCatalog() {
			return BaseView(Views.Center.PaperCatalogForm, new PaperCatalogFormVM());
		}


		[HttpPost]
		public ActionResult OrderPaperCatalogPost(PaperCatalogFormVM model) {
			var data =
				SimpleUtils.FluentAttributes.Utils.EntityUtils.ToStrings(model,
					x => x.FullName,
					x => x.Email);
			var message = DictionaryUtils.ToHtml(data);
			MailService.SendOrderPaperCatalog(message);
			return Content("ok");
		}

		[HttpPost]
		public ActionResult SeminarRegistrationPost(decimal groupId,
			SeminarRegisterFormVM model) {
			var gr = GroupService.GetByPK(groupId);
			var data =
				SimpleUtils.FluentAttributes.Utils.EntityUtils.ToStrings(model,
					x => x.FullName,
					x => x.CompanyName,
					x => x.Email,
					x => x.Phone,
					x => x.Position,
					x => x.Region,
					x => x.StudyManger,
					x => x.Section,
					x => x.Courses,
					x => x.HowMany,
					x => x.WhereAbout
					);
			data.Add("Семинар",gr.Title);
			data.Add("Дата",gr.DateBeg.DefaultString());
			var message = DictionaryUtils.ToHtml(data);
			MailService.SendSeminarRegistration(message);
			return Content("ok");
		}

		[HttpPost]
		public ActionResult CollectionMctsPost(CollectionMctsFormVM model) {
			var message = DictionaryUtils.ToHtml(
                SimpleUtils.FluentAttributes.Utils.EntityUtils.ToStrings(model, 
				x => x.FullName, 
				x => x.Email, 
				x => x.Phone, 
				x => x.CompanyName, 
				x => x.City
				));
			MailService.SendCollectionMcts(message);
			return Content("ok");
		}


		public const string SessionJubileeFileKey = "SessionJubileeFileKey";
		public ActionResult UploadJubileeFile(IEnumerable<HttpPostedFileBase> userfile) {
			if(userfile == null || !userfile.Any())
				return Content("ok");
			var file = userfile.First();
			var sessionId = UserSettingsService.SessionID.ToString();
			var fileName = sessionId + Path.GetExtension(file.FileName);
			var fileNameSys = UserImages.GetJubileeFileSys(fileName);
			Session[SessionJubileeFileKey] = fileName;
			var result = SaveJubileeFile(file, fileNameSys);
			return Content(result ?? "ok");
		}

		private static string SaveJubileeFile(HttpPostedFileBase file, string fileNameSys) {
			if (file.ContentLength > UserImages.MaxJubileeFileSize.Bytes) {
				return "Size";
			}
			file.SaveAs(fileNameSys);
			return null;
		}

		[OutputCache(Duration = 60*60, VaryByParam = "none")]
		public ActionResult ActionsBlock() {
			var actions = MarketingActionService.GetAll().Where(ma => ma.IsAdvert
				&& !ma.IsSecret && !ma.IsSpecialOffer)
				.IsActive()
				.OrderBy(x => x.WebSortOrder)
				.ToList();
			return View(Views.Shared.Block.ActionsBlock, actions);
		}

		[HandleNotFound]
		public ActionResult Video(int id, string title) {
			if(id == 0)
				return null;
			VideoService.LoadWith(x => x.VideoCategory);
			var video = VideoService.GetByPK(id);
			if(video == null || !video.IsActive)
				return null;
        	var currentTitle = video.UrlTitle;
			if (title != currentTitle)
				return this.RedirectToAction(() => Video(id, currentTitle));
			return BaseView(Views.Center.Video, video);
		}
		[HandleNotFound]
		public ActionResult VideoCategory(int categoryId) {
            var user = AuthService.CurrentUser;
            /*if (user == null)
                return RedirectToAction<SimpleRegController>(c => c.Registration(Request.Url.AbsolutePath, "ВИДЕОСАЙТ"));*/

            if (categoryId == 0)
				return null;
			var category = VideoCategoryService.GetByPK(categoryId);
			if(category == null)
				return null;
			var videos = VideoService.GetAll(x => x.CategoryId == categoryId 
				&& x.IsActive).OrderByDescending(x => x.VideoID).ToList();
			var model = new VideoCategoryVM {
				Category = category,
				Videos = videos
			};
			return BaseViewWithModel(new VideoCategoryView(), model);
		}
		[NotMobileRedirect]
		public ActionResult About() {
			var view = MHtmls.LongList(MHtmls.Title("О Центре:"), 
				MHtmls.MainList(Url.Center().Info("Информация о Центре"), Url.Complexes()));
			return BaseView(
				new PagePart(view.ToString()),
				new PagePart(Views.Shared.Education.NearestGroupMobile,null));
		}
		[NotMobileRedirect]
		public ActionResult NewsAndActions() {
			var view = MHtmls.LongList(MHtmls.Title("Новости и акции"), 
				MHtmls.MainList(Url.NewsListLink(), 
				Url.ActionsLink()));
			return BaseView(
				new PagePart(view.ToString()), 
				new PagePart(Views.Shared.Education.NearestGroupMobile,null));
		}
		[NotMobileRedirect]
		public ActionResult Info() {

			var model = SimplePageService.GetAll().BySysName(SimplePages.MainPage);
			return BaseView(Views.Center.Info, model);
		}

		[NotMobileRedirect]
		public ActionResult Actions() {
            var actions = MarketingActionService.GetAll().IsActive()
                .Where(a => a.IsAdvert && !a.IsSecret && !a.IsSpecialOffer).OrderBy(a => a.WebSortOrder);
			var view = MHtmls.LongList(MHtmls.Title("Акции"), 
				Html.Site().MobileActions(actions));
			return BaseView(
				new PagePart(view.ToString()), 
				new PagePart(Views.Shared.Education.NearestGroupMobile,null));		
		}



		[NotMobileRedirect]
		public ActionResult ExpressOrder(ExpressOrderVM model) {
			var isPost = Request.HttpMethod.ToLower() == "post";
			if(isPost) {
				MailService.ExpressOrder(model);
				var view = MHtmls.LongList(MHtmls.Title("Экспресс-запрос менеджеру"),
					H.p.Class("res_message")["Ваше сообщение отправлено."],
					H.p["Наши менеджеры свяжутся с Вами в ближайшее время!"]);
				return BaseView(
					new PagePart(view.ToString()));
			}
			return BaseView(Views.Center.ExpressOrderMobile, new ExpressOrderVM());

		}
	

	}
}
