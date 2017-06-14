using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Utils;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Common;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc.Extensions;
using Specialist.Web.Common.Utils.Logic;
using Specialist.Web.Core;
using Specialist.Web.Pages;
using System.Linq;
using Specialist.Services.Common.Extension;
using System.Collections.Generic;
using Specialist.Entities.Passport;
using Specialist.Services.Common.Interface;
using System.Net.Mail;
using Specialist.Entities.Profile.ViewModel;
using System.Text;
using SimpleUtils.Collections.Extensions;


namespace Specialist.Web.Controllers.Center
{
    public class ClientController : ViewController
    {

        [Dependency]
        public IRepository<SuccessStory> SuccesStoryService { get; set; }

        [Dependency]
        public IRepository<Response> ResponseService { get; set; }

        [Dependency]
        public ISectionService SectionService { get; set; }

        [Dependency]
        public IRepository<OrgResponse> OrgResponseService { get; set; }

        [Dependency]
        public IRepository<OrgProject> OrgProjectService { get; set; }

        [Dependency]
        public IRepository<SuccessStory> SuccessStoryService { get; set; }

        [Dependency]
        public IRepository<UserWork> UserWorkService { get; set; }

        [Dependency]
        public IRepository<UserWorkSection> UserWorkSectionService { get; set; }

        [Dependency]
        public IRepository<SimplePage> SimplePageService { get; set; }

        [Dependency]
        public IRepository2<UserInfo> UserInfoService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

         [Dependency]
        public ISectionVMService SectionVMService { get; set; }

         [Dependency]
         public IRepository<OrgVacancy> OrgVacancyService { get; set; }

         [Dependency]
         public IRepository<Resume> ResumeService { get; set; }

         [Dependency]
         public IMailService MailService { get; set; }

         [Dependency]
         public IRepository<User> UserService { get; set; }
         
        public ActionResult PrivatePerson(string urlName, int? pageIndex)
        {
            if(!pageIndex.HasValue && urlName != SimplePages.Urls.Works)
                return RedirectToAction(() => PrivatePerson(urlName, 1));
            var index = pageIndex.GetValueOrDefault() - 1;

            var model = new PrivatePersonVM();
            model.UrlName = urlName;
            if (model.IsSuccessStories)
                model.SuccessStories = SuccesStoryService.GetAll()
                    .IsActive().OrderByDescending(s => s.SuccessStoryID).ToPagedList(index, 10);

            else if (model.IsResponses)
                model.Responses = ResponseService.GetAll().IsActive()
					.OrderByDescending(x => x.Rating)
                    .ThenByDescending(r => r.ResponseID).ToPagedList(index, 10);

            else if(model.IsUserWorks) {
                var temp =
                    (from x in UserWorkService.GetAll().IsActive()
                    where !x.Section.IsMain
                    group x by x.Section_ID
                    into gr
                        select
                            new {
                                Section = gr.Key,
                                WorkSections = gr.Select(x => x.UserWorkSection).Distinct()
                            }).ToDictionary(x => x.Section, x => x.WorkSections.ToList());
	            var tree = SectionService.GetSectionsTree();
	            var allSections = temp.Select(x => x.Key).ToList();
	            var rootSections =
		            tree.Select(x =>
			            new {
				            Root = x,
				            Sections = x.SubSections.Where(ss =>
					            allSections.Contains(ss.Section_ID)).ToList()
			            })
			            .Where(x => x.Sections.Any()).ToList();
                     
                model.UserWorks = rootSections.Select(x => EntityWithList.New(x.Root,
                    x.Sections.Select(y => EntityWithList.New(y,
                        temp.GetValueOrDefault(y.Section_ID)))))
                    .ToList();
            }
			else {
                return RedirectToAction(() => PrivatePerson(SimplePages.Urls.SuccessStories, 1));
            	
            }

            return View(model);
        }

        public ActionResult CorporateClients(string urlName, int? pageIndex) {
            if (!pageIndex.HasValue)
                return RedirectToAction(() => CorporateClients(urlName, 1));
            var index = pageIndex.Value - 1;

            var model = new CorporateClientsVM();
            model.UrlName = urlName;
            model.Page = GetCorporateClients();


            if (model.IsResponses)
                model.Responses = OrgResponseService.GetAll().IsActive().OrderByDescending(r => r.OrgResponseID)
                    .ToPagedList(index, 10);

            if (model.IsProjects)
                model.OrgProjects = OrgProjectService.GetAll().IsActive()
                    .ToPagedList(index, 10);



            return View(model);
        }

      
        private SimplePage GetCorporateClients() {
            return SimplePageService.GetAll().BySysName(
                SimplePages.CorporateClients);
        }

        public ActionResult SuccessStory(int storyID) {
	        var successStory = SuccessStoryService.GetByPK(storyID);
	        var model = new SuccessStoryVM {
                SuccessStory = successStory,
				CourseLink = CourseService.AllCourseLinks().GetValueOrDefault(successStory.Course_TC),
                Images = UserImages.GetSuccessStoryFilesForResponse(storyID),
            };
            return View(model);
        }

		[HandleNotFound]
        public ActionResult OrgResponse(int responseID) {
            var model = new OrgResponseVM {
                Response = OrgResponseService.GetByPK(responseID),
                CorporateClients = GetCorporateClients()
            };
			if(model.Response == null)
				return null;
            return View(model);
        }

   

        public ActionResult UserWorks(int sectionID, int workSectionID, int? pageIndex) {
            if (!pageIndex.HasValue)
                return RedirectToAction(() => UserWorks(sectionID, workSectionID, 1));
            var index = pageIndex.Value - 1;

            var works = UserWorkService.GetAll().IsActive()
                .Where(uw => uw.Section_ID == sectionID);
            var model = new UserWorksVM {
                Section = SectionService.GetByPK(sectionID),
                
            };

            if (workSectionID != 0) {
                model.WorkSection = UserWorkSectionService.GetByPK(workSectionID);
                works = works.Where(uw => uw.WorkSectionID == workSectionID);
            }

            model.UserWorks = works.OrderByDescending(uw => uw.UserWorkID)
				.ToPagedList(index, 10);
            return View(model);
        }
        public ActionResult ChooseSectionResponses()
        {
            var controlModel =
                           SectionService.GetTreeWithSubsections();
            return View(CommonVM.New(controlModel, "Отзывы по направлениям"));
        }

       /* [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Resume()
        {
            var model = new ResumeVM();
            model.Result = "";
            return View(model);
        }*/

        public ActionResult UploadFileForJob(IEnumerable<HttpPostedFileBase> userfile)
        {
            Session.Add("UploadFileForResume", userfile);

            return Content("ok");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Resume(ResumeVM fillResume)
        {
            if (ModelState.IsValid)
            {
                fillResume.Result = "";
                fillResume.Submit();

                var section = "";
                if (fillResume.s1) section = section + "Web специалисты ";
                if (fillResume.s2) section = section + ";" + "Администраторы сетей ";
                if (fillResume.s3) section = section + ";" + "Программисты ";
                if (fillResume.s4) section = section + ";" + "Бухгалтеры ";
                if (fillResume.s5) section = section + ";" + "Верстальщики ";
                if (fillResume.s6) section = section + ";" + "Дизайнеры ";
                if (fillResume.s7) section = section + ";" + "Менеджеры ";
                if (fillResume.s8) section = section + ";" + "Операторы ПК ";
                if (fillResume.s9) section = section + ";" + "Проектировщики ";
                if (fillResume.s10) section = section + ";" + "Специалисты по тех обслуживанию ПК ";
                if (fillResume.s11) section = section + ";" + "Логисты (Склад и грузоперевозки) ";
                if (fillResume.s12) section = section + ";" + "Секретари ";
                if (fillResume.s13) section = section + ";" + "Разное ";

                var user = (User)HttpContext.Session["CurrentUserSessionKey"];
                var resumes = new List<Resume>();

                var resume =
                    new Resume
                    {
                        UserID = user.UserID,
                        FirstName = fillResume.YourName,
                        SecondName = fillResume.YourPatronymic,
                        LastName = fillResume.YourSurname,
                        Age = Convert.ToInt16(fillResume.YourAge),
                        Sex = fillResume.YourSex,
                        Education = fillResume.YourEducation,
                        Position = fillResume.YourPosition,
                        Sections = section,
                        Experience = fillResume.YourExperience,
                        Profit = Convert.ToInt32(fillResume.YourProfit),
                        Currency = fillResume.YourCurrency,
                        City = fillResume.YourCity,
                        Metro = fillResume.YourMetro,
                        Period = fillResume.YourPeriod,
                        Email = fillResume.YourEmail,
                        Phone = fillResume.YourTelHome + ";" + fillResume.YourTelJob + ";" + fillResume.YourTelMob,
                        IsActive = true,
                        UpdateDate = DateTime.Now
                    };

                resumes.Add(resume);

                foreach (var res in resumes)
                {
                    ResumeService.Insert(res);
                }

                ResumeService.SubmitChanges();

                var message = new StringBuilder();

                message.Append("<p>");
                message.Append("Основная информация");
                message.Append("<br/>");
                message.AppendFormat("Имя {0} ", fillResume.YourName);
                message.Append("<br/>");
                message.AppendFormat("Отчество {0} ", fillResume.YourPatronymic);
                message.Append("<br/>");
                message.AppendFormat("фамилия {0} ", fillResume.YourSurname);
                message.Append("<br/>");
                message.AppendFormat("Пол {0} ", fillResume.YourSex);
                message.Append("<br/>");
                message.AppendFormat("Возраст {0} ", fillResume.YourAge);
                message.Append("<br/>");

                message.AppendFormat("Образование {0} ", fillResume.YourEducation);
                message.Append("<br/>");
                message.AppendFormat("Желаемая должность {0} ", fillResume.YourPosition);
                message.Append("<br/>");

                message.Append("Раздел ");
                if (fillResume.s1) message.Append("Веб-технологии ");
                if (fillResume.s2) message.Append("Системное администрирование ");
                if (fillResume.s3) message.Append("Программирование ");
                if (fillResume.s4) message.Append("Бухгалтерия / Финансы ");
                if (fillResume.s5) message.Append("Дизайн, графика, верстка, 3D ");
                if (fillResume.s6) message.Append("Кадры/управление персоналом ");
                if (fillResume.s7) message.Append("Административный персонал ");
                if (fillResume.s8) message.Append("Проектирование ");
                if (fillResume.s9) message.Append("Техническое обслуживание ПК, HelpDesk ");
                if (fillResume.s10) message.Append("Складское хозяйство / Логистика / ВЭД ");
                if (fillResume.s11) message.Append("Продажи / Закупки ");
                if (fillResume.s12) message.Append("Информационная безопасность ");
                if (fillResume.s13) message.Append("Маркетинг / Реклама / PR ");
                if (fillResume.s14) message.Append("Разное ");
                message.Append("<br/>");

                message.AppendFormat("Опыт работы {0} ", fillResume.YourExperience);
                message.Append("<br/>");
                message.AppendFormat("Заработная плата {0} ", fillResume.YourProfit);
                message.Append(fillResume.YourCurrency);
                message.Append(" в месяц ");
                message.Append("<br/>");
                message.AppendFormat("Город {0} ", fillResume.YourCity);
                message.Append("<br/>");
                message.AppendFormat("Ближайшая станция метро {0} ", fillResume.YourMetro);
                message.Append("<br/>");
                message.AppendFormat("Срок публикации резюме {0} ", fillResume.YourPeriod);
                message.Append("<br/>");
                message.Append("Контактная информация ");
                message.Append("<br/>");
                message.AppendFormat("E-mail {0} ", fillResume.YourEmail);
                message.Append("<br/>");
                message.Append("Телефон: ");
                message.Append("<br/>");
                message.AppendFormat("Домашний {0} ", fillResume.YourTelHome);
                message.Append("<br/>");
                message.AppendFormat("Служебный {0} ", fillResume.YourTelJob);
                message.Append("<br/>");
                message.AppendFormat("Мобильный {0} ", fillResume.YourTelMob);
                message.Append("<br/>");
                message.Append("</p>");

                MailAddress from = new MailAddress("info@specialist.ru");
                MailAddress to = new MailAddress("job@specialist.ru");
                UploadFile upFile = new UploadFile();
                if (Session["UploadFileForResume"] != null)
                {
                    var userfile = (IEnumerable<HttpPostedFileBase>)Session["UploadFileForResume"];
                    var fileName = "";
                    var index = userfile.First().FileName.LastIndexOf("\\");
                    if (index > 0)
                    {
                        fileName = userfile.First().FileName.Substring(index);
                    }
                    else fileName = userfile.First().FileName;
                    
                    upFile.ContentLength = userfile.First().ContentLength;
                    upFile.Name = fileName;
                    upFile.Stream = userfile.First().InputStream;
                }
                MailService.SendForResume(from, to, message.ToString(), "Резюме", upFile);


                return View(fillResume);
            }
            else
            {
                var model = new ResumeVM();
                model.Result = "";
                return View(model);
            }
        }
/*
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Vacancies()
        {
            var model = new VacanciesVM();
            model.Result = "";
            return View(model);
        }*/

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Vacancies(VacanciesVM fillVacancy)
        {
            if (ModelState.IsValid)
            {
                fillVacancy.Submit();

                var section = "";
                if (fillVacancy.s1) section = section + "Web специалисты ";
                if (fillVacancy.s2) section = section + ";" + "Администраторы сетей ";
                if (fillVacancy.s3) section = section + ";" + "Программисты ";
                if (fillVacancy.s4) section = section + ";" + "Бухгалтеры ";
                if (fillVacancy.s5) section = section + ";" + "Верстальщики ";
                if (fillVacancy.s6) section = section + ";" + "Дизайнеры ";
                if (fillVacancy.s7) section = section + ";" + "Менеджеры ";
                if (fillVacancy.s8) section = section + ";" + "Операторы ПК ";
                if (fillVacancy.s9) section = section + ";" + "Проектировщики ";
                if (fillVacancy.s10) section = section + ";" + "Специалисты по тех обслуживанию ПК ";
                if (fillVacancy.s11) section = section + ";" + "Логисты (Склад и грузоперевозки) ";
                if (fillVacancy.s12) section = section + ";" + "Секретари ";
                if (fillVacancy.s13) section = section + ";" + "Разное ";

                var orgVacancies = new List<OrgVacancy>();
                var YourAgeSince = 18;
                var YourAgeTill = 60;
                if (fillVacancy.YourAgeSince != null) YourAgeSince = Convert.ToInt16(fillVacancy.YourAgeSince);
                if (fillVacancy.YourAgeTill != null) YourAgeTill = Convert.ToInt16(fillVacancy.YourAgeTill);
                var user = (User)HttpContext.Session["CurrentUserSessionKey"];                

                var orgVacancy =
                    new OrgVacancy
                    {
                        UserID = user.UserID,
                        Position = fillVacancy.YourPosition,
                        AgeSince = YourAgeSince,
                        AgeTill = YourAgeTill,
                        Education = fillVacancy.YourEducation,
                        Experience = fillVacancy.YourExperience,
                        Sex = fillVacancy.YourSex,
                        Busy = fillVacancy.YourBusy,
                        Schedule = fillVacancy.YourSchedule,
                        ForeignLanguages = fillVacancy.YourLang + ";" + fillVacancy.YourLangLevel,
                        Profit = Convert.ToInt32(fillVacancy.YourProfit),
                        Currency = fillVacancy.YourCurrency,
                        City = fillVacancy.YourCity,
                        Metro = fillVacancy.YourMetro,
                        Description = fillVacancy.YourText,
                        Sections = section,
                        Period = fillVacancy.YourPeriod,
                        Company = fillVacancy.YourCompany,
                        OrgPosition = fillVacancy.YourPos,
                        Name = fillVacancy.YourFIO,
                        Phone = fillVacancy.YourTel,
                        Email = fillVacancy.YourEmail,
                        IsActive = true,
                        UpdateDate = DateTime.Now
                    };
                orgVacancies.Add(orgVacancy);

                foreach (var orgVac in orgVacancies)
                {
                    OrgVacancyService.Insert(orgVac);
                }

                OrgVacancyService.SubmitChanges();

                return View(fillVacancy);
            }
            else
            {
                var model = new VacanciesVM();
                model.Result = "";
                return View(model);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Letter()
        {
            var model = new LetterVM();
            model.Result = "";
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Letter(LetterVM fillLetter)
        {
            if (ModelState.IsValid)
            {
                fillLetter.Submit();

                var user = (User)HttpContext.Session["CurrentUserSessionKey"];

                MailService.JobConsultation(user);

                fillLetter.isStartSearch = true;
                return View(fillLetter);
            }
            else
            {
                var model = new LetterVM();
                model.Result = "";
                return View(model);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RequestForWebinar()
        {
            var model = new RequestForWebinarVM();
            model.Result = "Оставить заявку:";
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RequestForWebinar(RequestForWebinarVM fillRequestForWebinar)
        {
            if (ModelState.IsValid)
            {
                fillRequestForWebinar.Submit();
                return View(fillRequestForWebinar);
            }
            else
            {
                var model = new RequestForWebinarVM();
                model.Result = "Оставить заявку:";
                return View(model);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SearchParamsVacancy()
        {

            return View();
        }

      
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchParamsVacancy(SearchVacancyVM fillSearchVacancy)
        {
            fillSearchVacancy.isStartSearch = true;
            Session.Add("SearchVacancyVM", fillSearchVacancy);
            return View(fillSearchVacancy);
        }

        public ActionResult SearchResultVacancy()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SearchParamsResume()
        {

            return View();
        }

      
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchParamsResume(SearchResumeVM fillSearchResume)
        {
            fillSearchResume.isStartSearch = true;
            Session.Add("SearchResumeVM", fillSearchResume);
            return View(fillSearchResume);
        }

        public ActionResult SearchResultResume()
        {
            return View();
        }

        public ActionResult LetterSent()
        {
            return View();
        }

        
		[Auth(RoleList = Role.ContentManager)]
        public ActionResult SearchUser()
        {
            var model = new SearchUserVM();
            model.Result = "";
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
		[Auth(RoleList = Role.ContentManager)]
        public ActionResult SearchUser(SearchUserVM fillSearchUser)
        {

            var YourUserID = 0;
            if (fillSearchUser.YourUserID != null) YourUserID = Convert.ToInt32(fillSearchUser.YourUserID);

            var YourName = "";
            if (fillSearchUser.YourName != null) YourName = fillSearchUser.YourName;

            var YourPatronymic = "";
            if (fillSearchUser.YourPatronymic != null) YourPatronymic = fillSearchUser.YourPatronymic;

            var YourSurname = "";
            if (fillSearchUser.YourSurname != null) YourSurname = fillSearchUser.YourSurname;

            var YourEmail = "";
            if (fillSearchUser.YourEmail != null) YourEmail = fillSearchUser.YourEmail;
        	var admin = (short) Role.Admin;
			var users = UserService.GetAll().Where(
            		u => (u.Roles & admin) != admin);
            if (YourUserID > 0) {
            	fillSearchUser.PassportUser = users
                    .Where(uid => uid.UserID == YourUserID).ToList();
            }
            else
            {
	            if (!YourName.IsEmpty()) {
		            users = users
						.Where(nam => nam.FirstName.Contains(YourName));
	            }
	            if (!YourPatronymic.IsEmpty()) {
		            users = users
			            .Where(pat => pat.SecondName.Contains(YourPatronymic));
	            }
	            if (!YourSurname.IsEmpty()) {
		            users = users
			            .Where(sur => sur.LastName.Contains(YourSurname));
	            }
	            if (!YourEmail.IsEmpty()) {
		            users = users
			            .Where(eml => eml.Email.Contains(YourEmail));
	            }
                fillSearchUser.PassportUser = users.Take(30).ToList();
            }

            if (fillSearchUser.PassportUser.Count() > 25)
            {
                fillSearchUser.PassportUser = null;
                fillSearchUser.Result = "Уточните параметры поиска";
            }
            else fillSearchUser.Result = "";
            return View(fillSearchUser);            
        }

		public ActionResult CheckRegCoupon(string code, string email) {
		    var form = H.Form("")[
				H.label["Код"], H.InputText("code", code), 
				H.label["Email"], H.InputText("email", email).Style("width:300px;"), 
				H.Submit("Проверить")];
			var message = string.Empty;
			if (!code.IsEmpty() || !email.IsEmpty()) {
				User user = null;
				if (CouponUtils.IsCityPromocode(code)) {
					var userId = UserInfoService
						.FirstOrDefault(x => x.CityPromocode == code).GetOrDefault(x => x.UserId);
					user = UserService.GetByPK(userId);
				}
				else {
					user = code.IsEmpty()
						? UserService.FirstOrDefault(x => x.Email == email)
						: UserService.GetByPK(StringUtils.ParseHex(code).GetValueOrDefault());
				}
				code = user.GetOrDefault(x => CouponUtils.PromoCode(x.UserID));
				if (user == null) {
					message = "Пользователя не существует";
				} else {
					message = "Код <b>{0}</b> пользователя <b>{1}</b>. {2}".FormatWith(code, user.FullName,
						!user.RegCouponIsValid ? H.span["Дата купона истекла."].Style("color:red;") : null);

				}
			}
			message = System.Web.HttpContext.Current.Request.IsPost() ? message : null;
			var view = H.div[H.h3["Введите код или емейл"],form, H.br, message];
			return BaseViewWithTitle("Проверка купона", new PagePart(view.ToString()));
		}
	}
}