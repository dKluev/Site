using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using MvcContrib.Attributes;
using MvcContrib.Filters;
using Newtonsoft.Json;
using NLog;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Common;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Order.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Passport.ViewModel;
using Specialist.Entities.Profile;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Profile.MetaData;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Profile.ViewModel.Common;
using Specialist.Entities.Secondary;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services;
using Specialist.Services.Center.ViewModel;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Education.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Passport;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Core;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc.Binders;
using Specialist.Web.Common.Mvc.Controllers;
using Specialist.Web.Common.Utils;
using Specialist.Web.Common.Utils.Logic;
using Specialist.Web.Const;
using Specialist.Web.Controllers.Center;
using Specialist.Web.Controllers.Shop;
using Specialist.Web.Core;
using Specialist.Web.Core.Logic;
using Specialist.Web.Core.Views;
using Specialist.Web.Extension;
using Specialist.Web.Pages;
using Specialist.Web.Pages.Interfaces;
using Specialist.Web.Root.Profile.Logic;
using Specialist.Web.Root.Profile.Services;
using Specialist.Web.Root.Profile.Views;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Util;
using Specialist.Web.Common.Extension;
using SimpleUtils;
using SimpleUtils.Extension;
using System.Linq;
using System.Reflection;
using Specialist.Services.Common.Extension;
using SpecialistTest.Common.Utils;
using SpecialistTest.Web.Core.Mvc.Extensions;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Lms;
using Specialist.Services.Catalog;
using Specialist.Services.Profile;
using Specialist.Services.ViewModel;
using Specialist.Web.Cms.Root.Socials;
using Specialist.Web.Common.Mvc.Extensions;
using Specialist.Web.Common.Services;
using Specialist.Web.Controllers.Common;
using Group = Specialist.Entities.Context.Group;
using Specialist.Web.Helpers;
using Specialist.Web.Root.Graduate.Services;
using Specialist.Web.Root.Graduate.ViewModels;
using Specialist.Web.Root.Learning.Services;
using Specialist.Web.Root.SimpleTests.Logic;
using Logger = Specialist.Services.Utils.Logger;


namespace Specialist.Web.Controllers
{
    public class ProfileController: ViewController
    {
        [Dependency]
        public MyBusinessService MyBusinessService { get; set; }

        [Dependency]
        public AccountService AccountService { get; set; }

        [Dependency]
        public GeoService GeoService { get; set; }

        [Dependency]
        public UserInfoService UserInfoService { get; set; }

        [Dependency]
        public ProfileService ProfileService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public GroupCertService GroupCertService { get; set; }
        [Dependency]
        public SimpleValueService SimpleValueService { get; set; }

        [Dependency]
        public IDictionariesService DictionaryService { get; set; }

        [Dependency]
        public ICourseVMService CourseVmService { get; set; }

        [Dependency]
        public IUserService UserService { get; set; }


        [Dependency]
        public IRepository2<PiStudentPhoto> PiStudentPhotoService { get; set; }

        [Dependency]
        public CourseFileVMService CourseFileVMService { get; set; }


        [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public ICityService CityService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        [Dependency]
        public IMailService MailService { get; set; }

        [Dependency]
        public IStudentService StudentService { get; set; }

        [Dependency]
        public ISimplePageVMService SimplePageVMService { get; set; }

        [Dependency]
        public IRepository<SuccessStory> SuccesStoryService { get; set; }

        [Dependency]
        public ShoppingCartVMService ShoppingCartVMService { get; set; }

        [Dependency]
        public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

        [Dependency]
        public StudentInGroupService SigService { get; set; }

		[Dependency]
        public IRepository2<SigEvent> SigEventService { get; set; }

        [Dependency]
        public IRepository<QuestionAnswer> QuestionAnswerService { get; set; }

        [Dependency]
        public IRepository<GroupSurvey> GroupSurveyService { get; set; }

        [Dependency]
        public IRepository<Profession> ProfessionService { get; set; }

        [Dependency]
        public IRepository2<UserTest> UserTestService { get; set; }

        [Dependency]
        public StudentRatingService StudentRatingService { get; set; }

        [Dependency]
        public IEmployeeService EmployeeService { get; set; }

        [Dependency]
        public IEmployeeVMService EmployeeVMService { get; set; }

        [Dependency]
        public IRepository2<WorkPlace> WorkPlaceService { get; set; }


        [Auth(RoleList = Role.ContentManager)]
        public ActionResult ClearCache(string key) {
            var cache = HttpContext.Cache;
			if(System.Web.HttpContext.Current.Request.IsPost()) {
				if (key == CacheManager.Announce) {
					CacheUtils.Clean<GroupService>(x => x.GetPlannedAndNotBegin());
					CacheUtils.Clean<GroupService>(x => x.GetGroupsForCourses());
					CacheUtils.Clean<SectionService>(x => x.NotAnnounce());
					CacheUtils.Clean<CourseService>(x => x.SectionCourses());

					CacheManager.Update(CacheManager.Announce);
				} else {
					if (key == CacheUtils.GetCacheKey<SimpleTestParser>(x => x.All())) {
			        	cache.Remove("HtmlsAllHtmlBlocks");
					}
		        	cache.Remove(key);
					if (key == CacheUtils.GetCacheKey<CourseService>(x => x.GetActiveTrackCourses())) {
			        	cache.Remove(CacheUtils.GetCacheKey<CourseService>(x => x.TrackCourseCounts()));
					}

					if (key == CacheUtils.GetCacheKey<MainPageService>(x => x.Get())) {
			        	cache.Remove("HtmlsAllHtmlBlocks");
			        	cache.Remove("RootSectionsForMain1");
					}

				}
				return OkJson();
			}
        	return BaseView(new ClearCacheView(), cache);
        }

	    public ActionResult JobAjax() {
		    return Content(Htmls.HtmlBlock(HtmlBlocks.ProfileJob));
	    }

        [Authorize]
	    public ActionResult BangesAjaxNew() {
		    return Content(SiteHtmls.BadgesNew(ProfileService.GetBadges(Url)).ToString());
	    }


        [Authorize]
        public ActionResult Details()
        {
	        if (User.IsRestricted) {
		        return Redirect("/");
	        }
	        if (User.IsTrainerRole) {
		        return Redirect(Url.Lms().Urls.Details());
	        }
            var model = ProfileService.GetProfile();
	        if (model.User.IsCompany) {
		        model.CartStatus = ShoppingCartVMService.GetCartState();
	        }
			SetMenu(model);
	        if (Htmls.IsNewProfile && !User.IsCompany) {
//				ViewData[Htmls.AdditionalStyle] = "personal-style";
	            return MView(Views.Profile.NewVersion.Details, model);
	        }
            return MView(Views.Profile.Details, model);
        }

	    public void SetMenu(ProfileVM Model) {
		    var isCompany = Model.User.IsCompany;
		    var hourStats = Url.Profile().StudyTypeStats("Статистика чаcов обучения");
		    var myProfile = new ProfileMenu(isCompany ? "Профиль компании" : "Мой профиль", "card");
		    myProfile.Add(
				Html.ActionLink<ProfileController>(c => c.Public(Model.User.UserID), "Профиль"),
				Html.ActionLink<ProfileController>(c => c.EditProfile(), "Редактировать профиль"),
				Html.ActionLink<ProfileController>(c => c.ChangePassword(), "Сменить пароль или E-mail(логин)")
				);
		    if (!isCompany) {
			    myProfile.Add(
				    Html.ActionLink<ProfileController>(c => c.ExamQuestionnaire(null), "Анкета для экзамена"),
				    Html.ActionLink<ProfileController>(c => c.WorkPlace(), "Моя работа")
				    );
			    if (Model.HasUnlimit) {
				    myProfile.Add(Url.Profile().UploadStudentPhoto("Фото БО"));
			    }
		    }
		    myProfile.Add(Html.ActionLink<ProfileController>(c => c.Subscribes(), "Подписки"));

		    var club = new ProfileMenu("Клуб выпускников", "smile");
		    if (!isCompany) {
			    club.Add(Html.MessageSectionLink(new MessageSection {
				    MessageSectionID = MessageSections.GraduateClub,
				    Name = "Клуб выпускников"
			    }),
				    Model.GetLink(Html.ActionLink<ProfileController>(
					    c => c.Responses(1), "Отзывы")),
				    Model.GetLink(Html.Consultations())
				    );
				if (Model.HasHosting) {
					club.Add(Html.ActionLink<ProfileController>(c => c.HostingInfo(), "Бесплатный хостинг"));
				}
			    club.Add(Model.GetLink(H.Anchor(MainMenu.Urls.Job, "Трудоустройство")));
		    }

		    var job = new ProfileMenu("Служба трудоустройства", "jobsection");
		    if (isCompany) {
			    job.Add(Url.Page().JobVacancy("Разместить вакансию"));
			    job.Add(Url.Page().JobManager("Отправить сообщение менеджеру службы трудоустройства"));
			    job.Add(Url.Page().CareerDay("Отправить заявку на участие в дне карьеры"));
		    }


		    var test = new ProfileMenu("Тестирование", "tests");
		    if (!isCompany) {
			    test.Add(Url.UserTests(), Url.TestCertificates());
		    }
		    if (Model.User.InRole(Role.CorpManager | Role.TestAdmin)) {
			    test.Add(Url.GroupTests());
		    }
		    if (Model.User.InRole(Role.TestAdmin)) {
			    test.Add(Url.GroupTest().Prepare("Подготовка группы"));
		    }

		    if (Model.User.InRole(Role.TestAdmin | Role.Trainer)) {
			    test.Add(Url.TestEdit().List("Конструктор тестов"));
		    }

		    var testService = new ProfileMenu("Сервис тестирования", "tests");
		    if (Model.User.IsTestCorpManager) {
			    testService.Add(Url.OrgTest().Tests("Активные тесты компании"),
					Url.TestEdit().List("Конструктор тестов"));
		    }

		    var courses = new ProfileMenu("Мои курсы", "rasp");
		    if (!isCompany) {
			    courses.Add(Model.MyCourses(Url).ToArray());
				courses.Add(Model.GetLink(hourStats.ToString()));
				courses.Add(Model.GetLink(Url.Graduate().GroupCerts("Сертификаты").ToString()));
		    }
		    var social = new ProfileMenu("Мое общение", "letter");
		    var devidea = Url.Page().DevIdea("Пожелание разработчикам");
		    if (!isCompany) {
			    social.Add(Html.ManagerLink(Model.Manager.Employee_TC, "Персональный менеджер"),
				    Url.SendManager(Model.Manager.Employee_TC));

			    social.Add(Html.Forum());
			    if (Model.User.IsStudent) {
				    social.Add(Url.Link<ProfileController>(c => c.Classmates(), "Одногруппники"),
						Url.Profile().GroupPhotos(Model.User.UserID, "Мой фотоальбом"));
			    }
			    social.Add(Url.Center().Competitions("Участие в конкурсах центра"),
				    Html.Seminars());
			    social.Add(devidea);
			    if (Model.User.IsEmployee) {
				    social.Add(Url.OrgProfile().List("Компании"));
			    }
		    }
		    var discouns = new ProfileMenu("Мои скидки", "mydiscounts");
		    if (!isCompany) {
			    discouns.Add(Url.Link<CenterController>(c => c.MarketingActions(), "Мои акции"),
					Url.RealSpecialist()
					);
			    if (Model.IsBest) {
				    discouns.Add(Url.Link<GraduateController>(c => c.Best(), "Лучший выпускник"));
			    }

			    if (Model.IsExcelMaster) {
				    discouns.Add(Url.Link<ProfileController>(c => c.ExcelMaster(),
					    "Магистр Excel"));
			    }
		    }

		    var employee = new ProfileMenu("Обучение сотрудников", "rasp");

		    var privilege = new ProfileMenu("Программа привилегий", "mydiscounts");
		    var cart = new ProfileMenu("Корзина", "basket");
		    var feedback = new ProfileMenu("Обратная связь", "letter");
		    var useful = new ProfileMenu("Полезные сервисы", "smile");
		    if (isCompany) {
			    if (Model.User.IsSpecOrg) {
				    employee.Add(
						Url.OrgProfile().GroupSearch(false, "Сотрудники"),
						Url.OrgProfile().GroupSearch(true, "Текущее обучение сотрудников"),
						Url.OrgProfile().DownloadOrders("Скачать список оплат"),
						Url.OrgProfile().RealSpecialist("Настоящие специалисты"),
						hourStats);

				    feedback.Add(Html.ManagerLink(Model.User.SpecOrg.Manager_TC, "Персональный менеджер"),
					    Url.Link<PageController>(c => c.SendInviteManager(), "Отправить сообщение"),
					    Html.Forum());
					    
			    }

			    privilege.Add(
					H.Anchor(SimplePages.FullUrls.CorpActions, "Корпоративные акции и скидки"),
					H.Anchor(SimplePages.FullUrls.RealSpecialist, "Настоящий специалист"),
					Url.Center().Competitions("Участие в конкурсах центра"),
				    Html.Seminars() );

			    cart.Add(Url.Link<CartController>(oc => oc.Details(), "В корзине " + Model.CartStatus),
					Url.Group().Search(null, "Поиск расписания"));
			    if (Model.User.IsSpecOrg) {
					cart.Add( Url.CompanyFile().List("Документы"));
					}

			    useful.Add(
				    Url.OrgProfile().StatusUpdate("Обновить статус"),
					devidea,
				    H.Anchor(SimplePages.FullUrls.CatalogInfo, "Скачать каталог курсов"),
					Url.Graduate().CertificateValidation("Проверить сертификат на подлинность")
				    );
		    }


		    var result = _.List(_.List(myProfile, employee, privilege, club, testService, test)
				.Where(x => x.Links.Any()).ToList(),
			    _.List(cart, feedback, useful, courses, social, discouns,job).Where(x => x.Links.Any()).ToList());
		    Model.Menu = result;

	    }

		[HandleNotFound]
        public ActionResult Public(int userID) {
            var model = ProfileService.GetPublic(userID);
			if (model == null) {
				return null;
			}
            return BaseViewWithTitle(model.User.FullName, new PagePart(Views.Profile.Public, model));
        }

        [Authorize]
        public ActionResult ProfileAjax() {
            var model = ProfileService.GetPublic(User.UserID);
            return BaseView(Views.Profile.NewVersion.Public, model);
        }


        [Authorize]
        [ModelStateToTempData]
        public ActionResult EditProfile()
        {
            var model = ProfileService.GetEditProfileVM();
            model.HasPhoto = System.IO.File.Exists(GetUserPhotoFileSys());
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ModelStateToTempData]
        public ActionResult EditProfile(EditProfileVM model)
        {
            model.User.ForEdit = true;
			if (model.Contacts != null && model.Contacts.Socials != null) {
		        foreach (var userContact in model.Contacts
					.Socials.Where(x => !x.Contact.IsEmpty())) {
			        userContact.Contact = 
						userContact.Contact.Remove("http://").Remove("https://");
		        }
			}
            if(FluentValidate(model) & CheckUserPhoto())
            {
                ProfileService.Update(model);
                UserImages.SaveUserPhoto(Request.Files, User.UserID);
				ShowMessage("Данные сохранены");
            }
	        if (User.IsStudent) {
		        var sigIds = StudentInGroupService.GetAll(x => x.Student_ID == User.Student_ID)
			        .Select(x => x.StudentInGroup_ID).ToList();
		        GroupCertService.DeleteGroupCertsEng(sigIds);
	        }
            return RedirectBack();
        }

        public ActionResult DeletePhoto()
        {
            var filename = GetUserPhotoFileSys();
            System.IO.File.Delete(filename);
            return RedirectBack();
        }

        public ActionResult DeleteStoryImage(int index)
        {
            var story = GetStory();
            var filename = UserImages.GetSuccessStoryFilesSys(story.SuccessStoryID)[index];
            System.IO.File.Delete(filename);
            return RedirectBack();
        }

        [ModelStateToTempData]
		[Authorize]
        public ActionResult SuccessStory()
        {
            var story = GetStory();
            var model = new EditSuccessStoryVM
                {
                    SuccessStory = story,
                    Images = UserImages
                    .GetSuccessStoryFilesForResponse(story.SuccessStoryID),
                    Professions = ProfessionService.GetAll().IsActive()
                        .OrderBy(x => x.Name).ToList(),
                };
            return View(model);

        }

        [HttpPost]
        [ModelStateToTempData]
        public ActionResult SuccessStory(EditSuccessStoryVM model)
        {
            model.Images = UserImages.GetSuccessStoryFilesForRequest();
            if(FluentValidate(model) & CheckStoryImages())
            {
                var story = GetStory();
            	story.UpdateDate = DateTime.Now;
                if (story.SuccessStoryID == 0)
                {
                    story = model.SuccessStory;
                    story.UserID = User.UserID;
                    SuccesStoryService.InsertAndSubmit(story);
                }
                else
                {
                    story.Update(model.SuccessStory, 
                        x => x.Description,
                        x => x.Profession_ID);
                    story.IsActive = false;
                    SuccesStoryService.SubmitChanges();
                }
                UserImages.SaveStoryFiles(Request.Files, story.SuccessStoryID);
            }
            return RedirectBack();

        }


        private SuccessStory GetStory()
        {
            return SuccesStoryService.GetAll()
                       .FirstOrDefault(x => x.UserID == User.UserID) 
                   ?? new SuccessStory();
        }

        private string GetUserPhotoFileSys()
        {
            return UserImages.GetUserPhotoFileSys(User.UserID);
        }

		public ActionResult RealSpecialist() {
			var student = AuthService.GetCurrentStudent();
			return MView(Views.Profile.RealSpecialist, 
				new RealSpecialistVM {Student = student,
				User = User});
		}

        private bool CheckUserPhoto()
        {
            if (Request.Files.Count == 0)
                return true;
            var file = Request.Files[0];
            if(file.ContentLength == 0)
                return true;
            if(!file.FileName.ToLower().EndsWith(Urls.PhotoExt))
                ModelState.AddModelError("Photo", "Фото только в формате jpg");
            if(file.ContentLength > UserImages.MaxImageSize.Bytes)
                ModelState.AddModelError("Photo", "Слишком большой файл");
            return ModelState.IsValid;
                        
        }

	    bool CheckStudentPhoto() {
            if (Request.Files.Count == 0)
                return false;
            var file = Request.Files[0];
            if(file.ContentLength == 0)
                return false;
            if(!file.FileName.ToLower().EndsWith(Urls.PhotoExt))
                ModelState.AddModelError("Photo", "Фото только в формате jpg");

		    using (var img = Image.FromStream(file.InputStream)) {
			    if (img.Width != 320 || img.Height != 240) {
	                ModelState.AddModelError("Photo", "Размер фото должен быть 320x240. Размер {0} {1}x{2}".FormatWith(file.FileName, img.Width, img.Height));
			    }
		    }
            return ModelState.IsValid;
		    
	    }

        private bool CheckStoryImages()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file.ContentLength == 0)
                    continue;
                var propertyName = "Images[" + i + "].File";
                if (!file.FileName.ToLower().EndsWith(Urls.PhotoExt))
                    ModelState.AddModelError(propertyName, "Фото только в формате jpg");
                if (file.ContentLength > UserImages.MaxImageSize.Bytes)
                    ModelState.AddModelError(propertyName, "Слишком большой файл");
                
            }
            return ModelState.IsValid;

        }

        [Authorize]
        [ModelStateToTempData]
        public ActionResult ExamQuestionnaire(string nextUrl)
        {
            return View(new EditExamQuestionnaireVM(ProfileService.GetExamQuestionnaire()) );
        }

        [Authorize]
        [HttpPost]
        [ModelStateToTempData]
        public ActionResult ExamQuestionnaire(UserExamQuestionnaire model,
            string nextUrl)
        {
            if (FluentValidate(model)) {
                ProfileService.Update(model);
            }
            else {
                return RedirectBack();
            }
            if(nextUrl != null)
                return Redirect(nextUrl);
			ShowMessage("Анкета успешно сохранена. " +
				"Для заказа тестирования перейдите на страницу интересующего Вас " +
				"<a href='http://www.specialist.ru/courses#vendori'>вендора</a>");
            return RedirectBack();
        }

    

     /*   public ActionResult Calendar()
        {
            var model = ProfileService.Calendar();
            return View(model);
        }*/

		[Authorize]
        public ActionResult Responses(int? pageIndex)
        {
            pageIndex = pageIndex ?? 1;
            var model = ProfileService.GetResponses(pageIndex.Value - 1);
            return View(model);
        }

	    public ActionResult CustomerTypeChoice(string nextUrl) {
            var name = new CustomerTypeChoiceVM
            {
				NextUrl = nextUrl,
				IsRegister = true,
                ActionUrl = Url.Action<ProfileController>(c => c.Register(null,null, null)),
                CustomerType = OrderCustomerType.PrivatePerson
            };
            return BaseView(ViewNames.CustomerTypeChoice, name);
		    
	    }


        [ModelStateToTempData]
        public virtual ActionResult Register(string customerType,string nextUrl, string token)
        {
            if (customerType == null) {
	            return RedirectToAction(() => CustomerTypeChoice(nextUrl));
            }
        	AuthService.SignOut();
            var model = new RegisterVM();
            model.User.Sex = true;
	        model.NextUrl = nextUrl;
            InitModel(model, customerType);
	        if (!token.IsEmpty()) {
				var fbUser = new FacebookService(token).GetFbUser();

			    var tryAction = TryFacebookLogin(model.NextUrl, fbUser, null);
		        if (tryAction != null) {
			        return tryAction;
		        }
		        model.User.Email = fbUser.Email;
		        model.User.LastName = fbUser.LastName;
		        model.User.FirstName = fbUser.FirstName;
		        model.User.SecondName = fbUser.MiddleName;
		        model.User.FbUserId = fbUser.Id;
		        model.User.Password = Membership.GeneratePassword(6, 0);
		        model.User.FbToken = token;
		        model.User.BirthDate = fbUser.BirthDate;
	        }
            return View(model);
        }

        public virtual ActionResult RegisterControl(string nextUrl, string customerType)
        {
            var model = new RegisterVM();
            model.User.Sex = true;
            model.NextUrl = nextUrl;
            InitModel(model, customerType);
            return View(PartialViewNames.RegisterControl, model);
        }

        private void InitModel(RegisterVM model, string customerType) {
	        var metires = DictionaryService.GetMetiers();
            model.CustomerType = customerType;
            model.Phone.ContactType = ContactTypes.Mobile;
            model.Countries = DictionaryService.GetCountries();
	        model.WorkBranches = DictionaryService.GetWorkBranches();
	        model.Metiers = model.WorkBranches.ToDictionary(x => x.Branch_ID,
		        x => (metires.GetValueOrDefault(x.Branch_ID) ?? new List<Metier>())
			        .Select(z => new {id = z.Metier_ID, name = z.MetierName}.As<object>()).ToList());
            model.Sources = DictionaryService.GetSources();
            model.UserAddress.CountryID = Countries.Russian;
            var cityTC = UserSettingsService.CityTC;
            if(cityTC != null)
                model.UserAddress.City = CityService.GetByPK(cityTC).CityName;
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [ModelStateToTempData]
        
        public virtual ActionResult RegisterPost(RegisterVM model) {
	        AuthService.SignOut();
            if(model.User.Email != null)
                model.User.Email = model.User.Email.ToLower();
            model.CaptchaValid = model.CaptchaText == UserSettingsService.CaptchaText;
            if(!model.CaptchaValid) {
                UserSettingsService.CaptchaText = null;
                model.CaptchaText = null;
            }
            if (!model.IsCompany)
                model.User.Company = null;
            if (FluentValidate(model))
            {

                var editProfile = new EditProfileVM();
                editProfile.UserAddress = model.UserAddress;
                editProfile.User = model.User;
                var phone = model.Phone.Phone;
                switch (model.Phone.ContactType)
                {
                    case ContactTypes.Phone:
                        editProfile.Contacts.Phone = phone;
                        break;
                    case ContactTypes.Mobile:
                        editProfile.Contacts.Mobile = phone;
                        break;
                    case ContactTypes.WorkPhone:
                        editProfile.Contacts.WorkPhone = phone;
                        break;
                }
//	            model.User.BirthDate = model.GetBirthday;
                ProfileService.Update(editProfile);
                AuthService.SignIn(model.User.Email, true);
				AddCityPromocode();
			    MailService.RegistrationComplete(User, null, false);
                if (model.NextUrl.IsEmpty())
                    return RedirectToAction(() => Details());
                return Redirect(model.NextUrl);
            }

	        return RedirectToAction(() => Register(model.CustomerType,model.NextUrl,model.User.FbToken));
        }

	    private Tuple<bool, string> GetCoupon() {
		    var isCity = UserInfoService.HasCityCoupon(User);
		    var couponUrl = isCity ? CreateCityCoupon() : CreateRegCoupon();
		    return Tuple.Create(isCity, couponUrl);
	    }

	    [Authorize]
        [ModelStateToTempData]
		[Authorize]
        public virtual ActionResult ChangePassword()
        {
            return View(new ChangePasswordVM{NewEmail = User.Email});
        }

        [HttpPost]
        [ModelStateToTempData]
        public ActionResult ChangePassword(ChangePasswordVM model)
        {
            if(FluentValidate(model))
            {
                var user = UserService.GetByPK(User.UserID);
                if(!model.NewPassword.IsEmpty())
                    user.Password = model.NewPassword;
                if(user.Email != model.NewEmail && !model.NewEmail.IsEmpty())
                {
                    user.Email = model.NewEmail.ToLower();
                }

                UserService.SubmitChanges();
                AuthService.SignIn(user.Email, true);
                AuthService.RefreshUser();
                ShowMessage("Данные обновлены");
            }

            return RedirectToAction<ProfileController>(c => c.ChangePassword());

        }

        public ActionResult RestorePassword(string email)
        {
            return BaseView(Views.Profile.RestorePassword, new RestorePasswordVM {
            	Email = email
            });
        }

        [HttpPost]
		[AjaxOnly]
		public ActionResult RestorePassword(RestorePasswordVM model) {
			model.Email = model.Email.ToLowerInvariant();
			var user = UserService.GetByEmail(model.Email);
			if (StringUtils.IsSpecEmail(model.Email)) {
				var employee = EmployeeService.GetAll(x => x.EMail.Contains(model.Email)).ToList()
					.FirstOrDefault(x => model.Email.Equals(x.FirstSpecEmail,
					StringComparison.InvariantCultureIgnoreCase));
				if (employee != null) {
					user = RegisterEmployee(employee, user);
				}
			}
			if (user != null) {
				SendRestoredPassword(model, user);
			}
	        var found = true;
	        if (model.Message.IsEmpty()) {
				model.Message = "Пользователя с таким E-mail'ом не существует.";
		        found = false;
	        }
			return JsonGet(new {message = model.Message, found = found});
		}

		private void SendRestoredPassword(RestorePasswordVM model, User user) {
			MailService.RestorePassword(user);
			model.Message = "Письмо с паролем отправлено к вам на почтовый ящик."/* +
				Url.Link<AccountController>(c => c.LogOn("/"), "Войти на сайт")*/;
		}


		[Authorize]
		public ActionResult HostingInfo() {
			return View(ViewNames.SimplePage,
				new SimplePageVM(SimplePageVMService.GetHostingInfo()));
		}

		[Authorize]
		public ActionResult Learning() {
			return BaseView(Views.Profile.NewVersion.Learning, GetLearningVM());
		}

	    private LearningVM GetLearningVM() {
		    var student = AuthService.GetCurrentStudent();
		    var parentCourseTCs = student == null
			    ? new List<string>()
			    : student.StudentInGroups.Select(x => x.Group.Course.ParentCourse_TC).ToList();
		    var studentCourseTCs = student == null
			    ? new List<string>()
			    : student.StudentInGroups.Select(x => x.Group.Course_TC).ToList();
		    var nextCourses = new List<CourseLink>();
		    var nextGroups = new List<Group>();
		    if (parentCourseTCs.Any()) {
			    var nextCourseTCs = _.List(CourseTC.Time1)
				    .Concat(CourseService.GetNextCourseTCs(parentCourseTCs))
				    .Except(parentCourseTCs.Concat(studentCourseTCs));
			    nextCourses = CourseService.GetCourseLinkList(nextCourseTCs).Take(10).ToList();
			    nextGroups = GroupService.GetGroupsForCourses(nextCourseTCs)
				    .Where(x => x.Discount.HasValue).OrderBy(x => x.DateBeg).Take(10).ToList();
		    }
		    var sigEvents = new List<SigEvent>();
		    if (student != null) {
			    var sigIds = student.StudentInGroups.Select(x => x.StudentInGroup_ID).ToList();
			    if (sigIds.Any()) {
				    var eventIds = _.List<decimal>(30, 31, 32, 33, 34);
				    sigEvents = SigEventService.GetAll(x =>
					    sigIds.Contains(x.StudentInGroup_ID)
						    && eventIds.Contains(x.Event_ID)).ToList();
			    }
		    }

		    var manager = OrderService.GetUserManagerTC(User);
		    var model = new LearningVM {
			    Student = student,
			    User = User,
			    NextCourses = nextCourses,
			    SigEvents = sigEvents,
			    NextGroups = nextGroups,
			    Manager = manager,
		    };
		    return model;
	    }

	    [Authorize]
		public ActionResult Classmates() {
			if(User.Student == null)
				return BaseView(new PagePart("Вы не являетесь выпускником"));
			var groupIds = StudentInGroupService
				.GetAll(x => x.Student_ID == User.Student_ID.Value 
					&& BerthTypes.AllPaidForCourses.Contains(x.BerthType_TC) 
					&& !CourseTC.AllSpecialWithoutHalf.Contains(x.Group.Course_TC))
				.Select(x => x.Group_ID).ToList();
			var courses = StudentInGroupService.GetAll(x => groupIds.Contains(x.Group_ID))
				.Select(x => new {x.Student_ID, x.Group.Course_TC})
				.GroupByToDictionary(x => x.Course_TC, x => x.Student_ID);
			var studentIds = courses.SelectMany(x => x.Value).ToList();
			var courseTCs = courses.Select(x => x.Key).ToList();
			var users = UserService.GetAll(x => 
				studentIds.Contains(x.Student_ID.Value) && !x.HideContacts)
				.DistinctToDictionary(x => x.Student_ID, x => x);
			var links = CourseService.GetCourseLinkList(courseTCs, true).ToList();
			var result = courses.Select(x => Tuple.Create( 
				links.FirstOrDefault(z => z.CourseTC == x.Key),
					x.Value.Select(y => users.GetValueOrDefault(y))
						.Where(z => z != null && z.UserID != User.UserID).ToList()))
						.Where(x => x.Item1 != null)
				.ToList();
			var model = new ClassmatesVM {Courses = result};
			return BaseView(Views.Profile.Classmates, model);

		}


		[Authorize]
		public ActionResult ChangeStatus() {
			return View(new ChangeStatusVM());
		}

		[HttpPost]
		public ActionResult ChangeStatus(ChangeStatusVM model) {
            //TODO:Активация кабинета https://spoint.specialist.ru/departments/projects/_layouts/listform.aspx?PageType=4&ListId={11B73C8A-2110-40C3-8169-06E92016FE8D}&ID=3611&ContentTypeID=0x010800B6F61EE8E28A8748843BA6EA5E5FB40A
            var isChanged = ProfileService.ChangeStatus(model);
			if (!isChanged)
				ModelState.AddModelError("_FORM", "Неверные логин или пароль");

			return View(new ChangeStatusVM {
				IsStudent = isChanged
			});
		}

		[ModelStateToTempData]
		[Authorize]
		public ActionResult Subscribes() {
			var user = UserService.GetByPK(User.UserID);
			var model = new SubscribesVM {
				User = user,
				Subscribes = (SubscribeType)user.Subscribes,
				MailListTypes = (MailListType)user.MailListTypes
			};
			ViewData[Htmls.AdditionalStyle] = "profile-subscribe";
			return BaseView(Views.Profile.Subscribes, model);
		}

		[HttpPost]
		[ModelStateToTempData]
		public ActionResult Subscribes([ModelBinder(typeof(FlagEnumModelBinder))] SubscribesVM model) {

			model.User.Subscribes = (byte)model.Subscribes;
			model.User.MailListTypes = (byte)model.MailListTypes;
			if (!FluentValidate(model))
				return RedirectBack();
			var user = UserService.GetByPK(User.UserID);
			user.Update(model.User, x => x.MailListSubscribed, x => x.Subscribes, x => x.MailListTypes);
			UserService.SubmitChanges();
			ShowMessage("Данные сохранены");
			if (user.Subscribes > 0) {
				MailService.PaperCatalogSubscribe(user);
			}
			
			return RedirectBack();
		}

	    string CreateCityCoupon() {
		    var userInfo = UserInfoService.FirstOrDefault(x => x.UserId == User.UserID);
		    if (userInfo != null && userInfo.CityPromocode != null) {
			    var promoCode = userInfo.CityPromocode;
				var fileSys = UserImages.GetCityCouponFileSys(User.UserID);
				return CreateCoupon(UserImages.GetCityCouponFileSys(0), fileSys, promoCode);
		    }
		    return null;
	    }

		string CreateRegCoupon() {
			var promoCode = CouponUtils.PromoCode(User.UserID);
			var fileSys = UserImages.GetRegCouponFileSys(User.UserID);
			return CreateCoupon(UserImages.GetRegCouponFileSys(0), fileSys, promoCode);
		}

	    private string CreateCoupon(string template, string fileSys, string promoCode) {
			try {
			    if (!System.IO.File.Exists(fileSys)) {
				    using (var image = Image.FromFile(template)) {
					    ImageUtils.DrawRegCouponText(image,
						    User.FullName,
						    promoCode, User.RegCouponEndDate).Save(fileSys);
				    }
			    }
			    var url = Urls.SysToWeb(fileSys);
			    return url;
			}
			catch (Exception e) {
				Logger.Exception(e, User);
			}
			return null;
	    }


	    [Authorize]
		public ActionResult Library() {
			var user = UserService.GetByPK(User.UserID);
			if (!user.IsStudent)
				return RedirectToAction<ProfileController>(c => c.Details());
		    var courseTrainerTCs = (User.InRole(Role.Trainer) 
				? EmployeeVMService.GetEmployeeCourses(user.Employee_TC)
					.Select(x => Tuple.Create(x, user.Employee_TC)).ToList()
				: StudentService.GetPaidCourseAndTrainerTCs(user.Student_ID.Value)).Distinct().ToList();
		    var courseTCs = courseTrainerTCs.Select(x => x.Item1).ToList();
		    var courseNames = CourseService.GetAllCourseNames();
		    var courses = courseTCs.Select(x => 
				courseNames.GetValueOrDefault(x)).Where(x => x != null).ToList();
		    var files = CourseFileVMService.GetFiles(courseTrainerTCs)
				.GroupByToDictionary(x => x.CourseTC, x => x);
		    var courseFiles = courses.Select(x => Tuple.Create(x, files.GetValueOrDefault(x.Course_TC)))
			    .Where(x => x.Item2 != null).ToList();
			return BaseView(Views.Profile.Library, new LibraryVM {
				Files = courseFiles
			});
		}

		private User RegisterEmployee(Employee employee, User user) {
			if (user == null) {
				user = new User {
					FirstName = employee.FirstName,
					LastName = employee.LastName,
					SecondName = employee.MiddleName,
					Email = employee.FirstSpecEmail.ToLowerInvariant(),
					Password = Membership.GeneratePassword(8, 0),
					IsActive = true,
				};
				UserService.Insert(user);
			}
			if (employee.IsTrainer) {
				user.Roles = (short)Role.Trainer;
			}
			if (user.Employee_TC == null) {
				user.Employee_TC = employee.Employee_TC;
			}
			UserService.SubmitChanges();
			return user;
		}
		[Authorize]
		public ActionResult Tests(int index) {
			if (index == 0) {
				return this.RedirectToAction<ProfileController>(c => c.Tests(1));
			}
			UserTestService.LoadWith(x => x.Test);
			var all = UserTestService.GetAll(x => x.UserId == User.UserID
				&& x.Course_TC == null && x.TestModuleSetId == null);
			var counts = all.GroupBy(x => x.TestId)
				.Select(x => new {x.Key, Count = x.Count()})
				.ToDictionary(x => x.Key, x => x.Count);
			var userTests = all.Where(x => x.IsBest && 
				(x.Test.Status == TestStatus.Active || 
					UserTestStatus.PassStatuses.Contains(x.Status)))
				.OrderByDescending(x => x.RunDate).ToPagedList(index - 1, 50);
			return BaseView(Views.Profile.Tests, new UserTestsVM(userTests) {
				TestTryCounts = counts,
				User = User
			});
		}



		public ActionResult Survey(int sigId) {

			return View(new QuestionAnswer {
				UserID = sigId
			});
		}

		[HttpPost]
		public ActionResult Survey(QuestionAnswer model) {
			var questionAnswer =
				QuestionAnswerService.FirstOrDefault(x => x.UserID == model.UserID);
			if (questionAnswer == null) {
				model.VoteDate = DateTime.Now;
				if (!model.Question2.IsEmpty() && model.Question2.Length > 1000)
					model.Question2 = model.Question2.Substring(0, 1000);

				if (!model.Question4.IsEmpty() && model.Question4.Length > 1000)
					model.Question4 = model.Question4.Substring(0, 1000);
				if (!model.Question5.IsEmpty() && model.Question5.Length > 1000)
					model.Question5 = model.Question5.Substring(0, 1000);
				QuestionAnswerService.InsertAndSubmit(model);
			}
			ShowMessage("Ваши ответы приняты. Спасибо за участие.");

			return View(new QuestionAnswer());
		}


		public ActionResult GroupSurvey(decimal studentId, decimal groupId) {

			var groupSurvey =
				GroupSurveyService.FirstOrDefault(x => x.Group_ID == groupId
				&& x.Student_ID == studentId) ?? new GroupSurvey {
				Student_ID = studentId,
				Group_ID = groupId,
				Group = GroupService.GetByPK(groupId)
			};
			return View(groupSurvey);
		}

		[HttpPost]
		public ActionResult GroupSurvey(GroupSurvey model) {
			var groupSurvey =
				GroupSurveyService.FirstOrDefault(x => x.Group_ID == model.Group_ID
				&& x.Student_ID == model.Student_ID);
			model.Reply1 = StringUtils.SafeSubstring(model.Reply1, 3000);
			model.Reply2 = StringUtils.SafeSubstring(model.Reply2, 3000);
			if (!model.Reply1.IsEmpty() || !model.Reply2.IsEmpty()) {
				if (groupSurvey == null) {
					model.InputDate = DateTime.Now;
					GroupSurveyService.Insert(model);
				} else {
					groupSurvey.InputDate = DateTime.Now;
					groupSurvey.Update(model, 
						x => x.Reply1, 
						x => x.Reply2);
				}
				GroupSurveyService.SubmitChanges();
				ShowMessage("Ваши ответы приняты. Спасибо за участие.");
			}

			return RedirectBack();
		}


		public ActionResult ExcelMaster() {
			return View(CommonVM.New(new object(), "Магистр Excel"));
		}
		[Authorize]
		public ActionResult MyBusiness() {
			return BaseViewWithModel(new MyBusinessView(), new MyBusinessUser());
		}
		[Authorize]
		[HttpPost]
		public ActionResult MyBusiness(MyBusinessUser model) {
			if (!MyBusinessCodes.All.Contains(model.Code)) {
				ModelState.AddModelError("", "Промокод не существует");
				return ErrorJson();
			}
			model.UserId = User.UserID;
			if (!MyBusinessService.Save(model)) {
				ModelState.AddModelError("", "Вы уже зарегистрированы");
			}

			if (!ModelState.IsValid)
				return ErrorJson();
			return OkJson();
		}

		[Authorize]
		public ActionResult ChangeNameRequest() {
			var model = new ChangeNameRequestVM {
				User = User
			};
			return BaseViewWithModel(new ChangeNameRequestView(), model);
		}
		[Authorize]
		[HttpPost]
		public ActionResult ChangeNameRequest(ChangeNameRequestVM model) {
			var link = Url.Link<ProfileController>(c => c.ChangeName(User.UserID, model.User.FirstName, model.User.SecondName, model.User.LastName), "Изменить").AbsoluteHref().ToString();
			MailService.SendChangeNameRequest(link, User, model.User);
			return OkJson();
		}

		[Auth(RoleList = Role.ContentManager)]
		public ActionResult ChangeName(int userId,string firstName, string secondName, string lastName) {
			var user = UserService.GetByPK(userId);
			user.FirstName = firstName;
			user.SecondName = secondName;
			user.LastName = lastName;
			UserService.SubmitChanges();

			return BaseView(new PagePart("done"));
		}


		[Authorize]
		public ActionResult WorkPlace() {
			var model = WorkPlaceService.FirstOrDefault(x => x.UserId == User.UserID)
				?? new WorkPlace();
			return BaseViewWithModel(new WorkPlaceView(), model);
		}

		[HttpPost]
		public ActionResult WorkPlacePost(WorkPlace model) {
			model = model ?? new WorkPlace();
			if (!LinqToSqlValidator.Validate(ModelState, model))
				return ErrorJson();
			WorkPlaceService.EnableTracking();
			model.UserId = User.UserID;
			var oldModel = WorkPlaceService.FirstOrDefault(x => x.UserId == User.UserID);
			if (oldModel == null) {
				WorkPlaceService.InsertAndSubmit(model);
				return OkJson();
			}
			oldModel.Update(model, 
				x => x.Name, 
				x => x.Site, 
				x => x.FullName, 
				x => x.Email, 
				x => x.Phone);
			WorkPlaceService.SubmitChanges();
			return OkJson();
		}

		[Auth(RoleList = Role.Trainer)]
		public ActionResult SocialConnect() {
			return BaseView(Views.Profile.SocialConnect, User);
		}
		[Auth(RoleList = Role.Trainer)]
		public ActionResult SocialConnects() {
			var trainerRole = (short) Role.Trainer;
			var users = UserService.GetAll(x => (x.Roles & trainerRole) == trainerRole
				&& x.Employee_TC != null && x.FbToken != null).ToList();
			var model = CommonVM.New(users, "Преподаватели залогиненые в Facebook");
			var view = InlineBaseView.New(model, x =>
				H.Ul(x.Model.Data.Select(z => z.FullName)));
			return BaseViewWithModel(view, model);
		}

		[Authorize]
	    public ActionResult LinkFacebook(string token) {
			var fbUser = new FacebookService(token).GetFbUser();
			var returnUrl = Url.Profile().Urls.EditProfile();
			var user = UserService.GetByEmail(User.Email);
			user.FbUserId = fbUser.Id;
			var user2 = UserService.FirstOrDefault(x => x.FbUserId == fbUser.Id);
			if (user2 != null) {
				user2.FbUserId = null;
			}
			UserService.SubmitChanges();
			AuthService.RefreshUser();
			return Redirect(returnUrl);
	    }

	    public ActionResult FacebookLogin(string token, string returnUrl) {
		    if (token.IsEmpty()) {
			    return RedirectBack();
		    }
		    var failAction = RedirectToAction(() => Register(OrderCustomerType.PrivatePerson, returnUrl, token));
			var fbUser = new FacebookService(token).GetFbUser();
		    return TryFacebookLogin(returnUrl, fbUser, failAction);
	    }

	    private ActionResult TryFacebookLogin(string returnUrl, FacebookService.FbUser fbUser, RedirectToRouteResult failAction) {
		    var user = UserService.FirstOrDefault(x => x.FbUserId == fbUser.Id);
		    if (user == null) {
			    return failAction;
		    }
		    return AccountService.Login(this, MView, new LogOnVM {
			    Email = user.Email,
			    Remeber = true,
			    ReturnUrl = returnUrl
		    });
	    }

	    [Auth(RoleList = Role.Trainer)]
		public ActionResult FaceBookAccessToken(string token) {
			var user = UserService.GetByPK(User.UserID);
			var client = new FacebookClient();
			var task = client.PostTaskAsync("/oauth/access_token",
				new {
					client_id = "180742018637213",
					client_secret = "c8cf1bbd1bd1640ff7430ecc9316646b",
					grant_type = "fb_exchange_token",
					fb_exchange_token = token
				});
			task.Wait();
			user.FbToken = JsonConvert.DeserializeObject(task.Result.ToString())
				.As<dynamic>().access_token ;
			UserService.SubmitChanges();
			AuthService.RefreshUser();
			return null;
		}

	    [Auth(RoleList = Role.Trainer)]
	    public ActionResult VKontakteAccessToken(string tokenUrl) {
		    var token = Regex.Match(tokenUrl, "access_token=(.*?)&").Groups[1].Value;
		    var user = UserService.GetByPK(User.UserID);
		    user.VkToken = token;
			UserService.SubmitChanges();
			AuthService.RefreshUser();
			return RedirectBack();
	    }

		public ActionResult GroupPhotos(int userId) {
			var studentId = UserService.GetValues(userId, x => x.Student_ID);
			var groupIds = StudentInGroupService.GetAll(x => x.Student_ID == studentId)
				.Select(x => x.Group_ID).ToList();
			var groupTrainer = Images.GetGroupsPhoto();
			var resGroupIds = groupTrainer.Keys.Intersect(groupIds).Cast<object>().ToList();
			var links = CourseService.AllCourseLinks();
			var groups = GroupService.GetByPK(resGroupIds).Select(x => new {x.Group_ID, x.Course_TC})
				.ToList().Select(x => Tuple.Create(x.Group_ID, links[x.Course_TC])).ToList();
			var model = new GroupPhotosVM {
				Groups = groups,
				GroupTrainer = groupTrainer
			};
			var view = InlineBaseView.New(model, x =>
				groups.Any()
				? H.div[groups.Select(z => H.div[H.h3[Html.CourseLink(z.Item2)], 
					Images.Gallary(new Employee{Employee_TC = groupTrainer[z.Item1].Item1},
					filter:z.Item1.ToString())]),
					H.h3["Поделитесь с друзьями!"],
					Html.AddThis() ]
				: H.div[H.strong["Пока ничего нет"]]);
			return BaseViewWithModel(view, model);

		}

	    void SendCouponEmail() {
		    if (User.CanHaveCoupon) {
			    var coupon = GetCoupon();
		    }
	    }

		[Auth]
	    public ActionResult SendCoupon() {
			SendCouponEmail();
			ShowMessage("Купон отправлен");
			return RedirectBack();
		}
	    public ActionResult TestError() {
		    throw new Exception("test");
	    }
		[Auth(RoleList = Role.Admin)]
	    public ActionResult SetRostov() {
		    UserSettingsService.CityName = GeoService.Rostov;
		    return NotFound();
	    }

		[Auth]
	    public ActionResult UpdateEmployeeCompany(int id) {
		    if (!Companies.TestService.Contains(id))
			    return NotFound();
		    UserService.GetByPK(User.UserID).EmployeeCompanyID = id;
			UserService.SubmitChanges();
			AuthService.RefreshUser();
//		    var companyName = CompanyService.GetValues(id, x => x.CompanyName);
		    return Redirect(Url.Profile().Urls.Details());
	    }

	    void AddCityPromocode() {
			UserInfoService.EnableTracking();
		    var promocode = CouponUtils.CityPromocode(User.UserID);
		    if (GeoService.IsCityCoupon) {
			    var userInfo = new UserInfo {
				    UserId = User.UserID,
				    CityPromocode = promocode
			    };
				UserInfoService.InsertAndSubmit(userInfo);
			    SimpleValueService.IncreaseCouponCount();
		    }
	    }

		string TopListView(TopStudentListVM list, int rating) {
			var allPositions = new List<string>(list.List);
			if (rating > TopStudentListVM.TopCount) {
				allPositions.Insert(0,H.strong[rating + ". " + User.FullName].ToString());
			}
			return TopListTemplate(allPositions);
		}

	    private static string TopListTemplate(List<string> allPositions) {
		    return H.div[Images.Main("best.jpg").FloatLeft(), H.p[allPositions.JoinWith("<br/>")]
			    .Style("margin-left:100px;")].ToString();
	    }

		[Authorize]
	    public ActionResult StudyTypeStats() {
			if (!User.IsStudent && !User.IsSpecOrg) return BaseView(
				new PagePart(H.h1["Статистика не доступна. Обновите статус на сайте."].ToString()));
		    var stats = User.Student_ID > 0 
				? SigService.GetStats(User.Student_ID.Value, null)
				: SigService.GetStats(null, User.Org_ID);
		    return View(stats);
	    }

		
	    public ActionResult TopStudents() {
		    var view = string.Empty;
		    if (User == null) {
			    return null;
		    }
		    if (User.IsCompany) {
			    if (User.IsSpecOrg) {
				    var students = StudentRatingService.GetTopStudents(User.Org_ID);
				    if (students.Any()) {
					    view = TopListTemplate(students);
				    }
				    else {
					    return null;
				    }
			    } else {
				    return null;
			    }
		    } else {
				var model = StudentRatingService.GetTopStudents();
				var rating = User.Student_ID.HasValue
					? StudentRatingService.GetForStudent(User.Student_ID.Value, x => x.Rating)
					: 0;
				view = TopListView(model, rating);
			    
		    }
			return Content(H.div[H.h2["Рейтинг слушателей"], view].ToString());
		}

        [Authorize]
	    public ActionResult UploadStudentPhoto() {
			PiStudentPhotoService.EnableTracking();
		    var photoExists = PiStudentPhotoService.GetAll(x => x.Student_ID == User.Student_ID).Any();
            if(System.Web.HttpContext.Current.Request.IsPost() && !photoExists && CheckStudentPhoto()) {
	            var photo = new PiStudentPhoto {
		            Student_ID = User.Student_ID.Value,
		            Photo = HttpUtils.FileToBytes(Request.Files[0])
	            };
	            photoExists = true;
	            PiStudentPhotoService.InsertAndSubmit(photo);
            }

		    return View(new UploadStudentPhotoVM{PhotoExists = photoExists});
	    }

        [Auth(RoleList = Role.Admin)]
	    public ActionResult OkRefreshToken() {
		    return this.Redirect(OdnoklassnikiUtl.AccessUrl);
	    }

        [Auth(RoleList = Role.Admin)]
	    public ActionResult HowsMySsl() {
		    return Content(HttpUtils.Get("https://www.howsmyssl.com/"));
	    }

    }
}
