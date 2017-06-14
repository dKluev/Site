using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.Practices.Unity;
using SimpleUtils.Reflection.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Common.Logic;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Extension;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Common.Extension;
using Specialist.Services.Common.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Tests;
using Specialist.Web.Common.Utils;
using Specialist.Web.Const;
using Specialist.Web.Entities.Center;
using Specialist.Web.Pages.Interfaces;
using Specialist.Services.Center.Extension;
using System.Linq.Dynamic;
using System.Web.Mvc;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Center;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Passport;
using Specialist.Entities.ViewModel;
using Specialist.Web.Common.Extension;
using Specialist.Web.Controllers;
using Specialist.Web.Helpers;
using Specialist.Web.Root.Center.ViewModels;
using Specialist.Web.Root.Tests.ViewModels;
using Htmls = Specialist.Web.Common.Html.Htmls;
using Tuple = System.Tuple;

namespace Specialist.Web.Pages
{
    public class SimplePageVMService : ISimplePageVMService
    {
        [Dependency]
        public IEmployeeService EmployeeService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public ICourseListVMService CourseListVMService { get; set; }

        [Dependency]
        public ISimplePageService SimplePageService { get; set; }

        [Dependency]
        public IEntityCommonService EntityCommonService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

		[Dependency]
		public IRepository2<Video> VideoService { get; set; }


		[Dependency]
		public IComplexService ComplexService { get; set; }

		[Dependency]
		public IRepository2<Vendor> VendorService { get; set; }


		[Dependency]
		public IRepository<VideoCategory> VideoCategoryService { get; set; }
		[Dependency]
		public IRepository2<Vacancy> VacancyService { get; set; }
        [Dependency]
        public ICityService CityService { get; set; }

        [Dependency]
        public IAuthService AuthService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IGroupVMService GroupVMService { get; set; }
        [Dependency]
        public ISectionService SectionService { get; set; }

        [Dependency]
        public TestService TestService { get; set; }

        [Dependency]
        public IRepository<MarketingAction> MarketingActionService { get; set; }

        [Dependency]
        public IResponseService ResponseServise { get; set; }

        [Dependency]
        public IRepository<UsefulInformation> UsefulInformationService { get; set; }

        public EntityCommonVM GetByUrl(string url, PageController controller)
        {
            var page = SimplePageService.GetByUrl(url);
            if(page == null)
                return null;
            if (page.UseTabs && page.Children.Any())
                page = page.Children.OrderBy(x => x.WebSortOrder).First();

            var model = EntityCommonService.Get(page);
			if(page.SysName == SimplePages.HotGroups)
				page.Name = "Группы " + CommonTexts.Discounts + CommonTexts.OnDay;

            switch (page.SysName)
            {
                case SimplePages.UsefulInformation:
                    AddControlForUsefulInformation(model);
                    break;
                case SimplePages.Trainers:
                    AddControlForTrainers(model);
                    break;
                case SimplePages.Managers:
                    AddControlForManagers(model);
                    break;
                case SimplePages.Locations:
                    AddControlForLocations(model);   
                    break;
                case SimplePages.HotGroups:
                    AddControlForHotGroups(model, controller);   
                    break;
                case SimplePages.WeekendCourses:
                    AddControlForWeekendCourses(model);   
                    break;
                case SimplePages.OnlineTesting:
                    AddControlForOnlineTesting(model);   
                    break;
                case SimplePages.NewYearCourses:
                    AddControlForNewYearCourses(model);   
                    break;
                case SimplePages.OpenClasses:
                    AddControlForOpenClasses(model);   
                    break;
                case SimplePages.PartnerVacancy:
                    AddControlForPartnerVacancy(model);   
                    break;
                case SimplePages.Probation:
                    AddControlForProbation(model);   
                    break;
                case SimplePages.ClassIpCamera:
                    AddControlForClassIpCamera(model);   
                    break;
                case SimplePages.Career:
                    AddControlForCareer(model);   
                    break;
                case SimplePages.Job:
                    AddControlForCareer(model);
                    break;
                case SimplePages.PersonalManager:
                    AddControlForPersonalManager(model);   
                    break;
                case SimplePages.OflManager:
                    AddControlForOflManager(model);   
                    break;
                case SimplePages.MarketingActions:
                    AddControlForMarketingActions(model);
                    break;
                case SimplePages.SpecialActions:
                    AddControlForSpecialActions(model);
                    break;
                case SimplePages.Discounts:
                    AddControlForDiscounts(model);
                    break;
                case SimplePages.Reserve:
                    AddControlForReserve(model);
                    break;
                case SimplePages.Webinar:
                    AddControlForWebinar(model);
                    break;
                case SimplePages.WebinarResponses:
                    AddControlForWebinarResponses(model);
                    break;
                case SimplePages.IntraExtraResponses:
                    AddControlForIntraExtraResponses(model);
                    break;
                case SimplePages.DiplomResponses:
                    AddControlForDiplomResponses(model);
                    break;
                case SimplePages.Center:
                    AddControlCenter(model);
                    break;
                case SimplePages.Recruiter2011:
                    AddControlRecruiter2011(model);
                    break;
                case SimplePages.SpecialistTV:
                    AddControlSpecialistTV(model);
                    break;
                case SimplePages.CollectionMcts:
                    AddControlForCollectionMcts(model);
                    break;
                case SimplePages.SignUpWebinar:
                    AddControlForSingUpWebinar(model);
                    break;
                case SimplePages.MicrosoftSeminars:
                    AddControlForMicrosoftSeminars(model);
                    break;
				case SimplePages.TestingCenter:
                    AddControlForTestingCenter(model);
                    break;
				 case SimplePages.IntramuralExtramural:
                    AddControlForIntramuralExtramural(model);
                    break;
				 case SimplePages.AboutDiplom:
                    AddControlForAboutDiplom(model);
                    break;
				 case SimplePages.TrainingPrograms:
                    AddControlForTrainingPrograms(model, controller);
                    break;
				 case SimplePages.CorpOffers:
                    AddControlForCorpOffers(model);
                    break;
				 case SimplePages.Unlimit:
                    AddControlForUnlimit(model, controller);
                    break;
				 case SimplePages.UnlimitEng:
                    AddControlForUnlimit(model, controller);
                    break;
				 case SimplePages.UnlimitWithCharge:
                   AddControlForUnlimitWithCharge(model);
                   break;
				 case SimplePages.WebinarInv:
                   AddControlForWebinarInv(model);
                   break;
				 case SimplePages.UnlimitWithoutCharge:
                   AddControlForUnlimitWithoutCharge(model);
                   break;
				 case SimplePages.UnlimitNoChargeEng:
                   AddControlForUnlimitWithoutCharge(model,true);
                   break;

            }
            return model;
        }

    	private void AddControlCenter(SimplePageVM model) {
    		var graduates = 
				model.EntityWithTags.FirstOrDefault(x => 
					x.Entity.As<SimplePage>().SysName == SimplePages.Graduates);
			if(graduates == null)
				return;
			var privatePerson = new List<SimplePage> {
    			new SimplePage{UrlName = SimplePages.Urls.SuccessStories, Title = "Истории успеха" },
    			new SimplePage{UrlName = SimplePages.Urls.Responses, Title = "Отзывы"},
    			new SimplePage{UrlName = SimplePages.Urls.Works, Title = "Работы выпускников"},
    		};
    		foreach (var simplePage in privatePerson) {
    			simplePage.UrlName = "client/privateperson/"
    				+ simplePage.UrlName;
    		}
			
			graduates.List.InsertRange(0, privatePerson.Cast<IEntityCommonInfo>());

    		var locations = 
				model.EntityWithTags.FirstOrDefault(x => 
					x.Entity.As<SimplePage>().SysName == SimplePages.Locations);
			if(locations == null)
				return;
            var city = 
                CityService.GetAll().First(c => c.City_TC == Cities.Moscow).As<IEntityCommonInfo>();
    		var entityes = _.List(city).AddFluent(ComplexService.List()
    			.Select(x => x.Value).Where(x => x.IsPublished));

    		locations.List = entityes;
    	}


    	private void AddControlForManagers(SimplePageVM model)
        {
            model.Controls.Add(
                new SimplePageVM.Control(
                    PartialViewNames.EmployeeList, EmployeeService
                        .GetAll().Where(e => e.EmpGroup_TC != EmpGroups.Trainer 
                            && e.EmpGroup_TC != EmpGroups.Dismiss && 
							!Employees.SpecialTrainers.Contains(e.Employee_TC))
                        .CommonList().ToList()));
        }

        public SimplePage GetHostingInfo()
        {
            var user = AuthService.CurrentUser;
            var student = user.Student;
            var simplePage = SimplePageService.GetAll().SysName(SimplePages.HostingInfo);
            simplePage.Description = TemplateEngine.GetText(simplePage.Description,
                new
                {
                    user.FullName,
                    student.HostingSite,
                    student.HostingLogin,
                    student.HostingPassword
                });
            return simplePage;
        }

        public static string GetCityNote(City city)
        {
            return 
                city.MainComplex.Address.Tag("p") +
                city.PhoneList.FirstOrDefault().Tag("p") +
                HtmlControls.MailTo(city.Email).ToString().Tag("p");
        }

        public string GetManagerNote(Employee employee) {
            var note = string.Empty;
            if(AuthService.CurrentUser != null)
                note += HtmlControls.MailTo(employee.FirstEmail)
                    .ToString().Tag("p");
            return note;
        }

        public void AddControlForLocations(SimplePageVM model)
        {
            var cities = 
                CityService.GetAll().OrderBy(c => c.SortOrder);

            model.Controls.Add(
				new SimplePageVM.Control(PartialViewNames.Cities, cities));
			model.Controls.Add(
				new SimplePageVM.Control(PartialViewNames.SectionsResponses, null));
       
        }

		public List<int> AllTestSections() {

			return CacheUtils.Get(MethodBase.GetCurrentMethod(), () => {
				var relations = TestService.GetTestSectionRelations();
				return relations.Where(x => x.Object.IsActive).Select(x => x.RelationObject_ID).Distinct().ToList()
					.Cast<int>().ToList();
			},24);
    } 

        public void AddControlForOnlineTesting(SimplePageVM model) {
	        var sections = SectionService.GetSectionsTree();

        	var testSections = AllTestSections();
			sections = sections.Where(x => testSections.Contains(x.Section_ID)).ToList();
        	var mainTestsVm = new MainTestsVM {Sections = sections};
			var isSecond = Htmls.IsSecond;
			if(isSecond) {
				mainTestsVm.Description = model.Entity.Description;
				model.Description = new TextWithInfoTags(string.Empty);
			}
        	mainTestsVm.IsSecond = isSecond;
        	model.Controls.Add(
				new SimplePageVM.Control(Views.Page.TestSections, mainTestsVm));
       
        }

     
        public void AddControlForTrainers(SimplePageVM model) {
        	var controlModel =
        		EmployeeService.GetAllTrainers();
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.AllTrainers, controlModel));          
        }
        
        public void AddControlForHotGroups(SimplePageVM model, PageController controller)
        {
            model.Controls.Add(
                new SimplePageVM.Control(Htmls.GroupSort("Выберите свой курс со скидкой!", 
					controller.Url.Group().Urls.HotGroupsWithSort(0))
					.ToString()));
        }

    	private void AddControlForWeekendCourses(SimplePageVM model) {
            var groups = GroupService.GetPlannedAndNotBegin()
				.Where(g => DaySequences.GetAll.Contains(g.DaySequence_TC)
				&& g.DateBeg < DateTime.Now.AddMonths(1)).NotSpecial();
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.NearestGroupList, 
                    new NearestGroupsVM(groups.ToList())));
    	}

    	private void AddControlForNewYearCourses(SimplePageVM model) {
            var cityTC = UserSettingsService.CityTC;
    		var newYearGroups = GroupService.GetPlannedAndNotBegin()
    			.Where(g => g.DateBeg >= new DateTime(2012,1,1) && g.DateEnd <= new DateTime(2012,1,12));
    		var groups = newYearGroups.ByCity(cityTC)
				.ToList();
			if(cityTC != Cities.Moscow)
				groups.AddRange(newYearGroups.Where(g => g.WebinarExists));
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.NearestGroupList, 
                    new NearestGroupsVM(groups.ToList())));
    	}

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }


        public void AddControlForCorpOffers(SimplePageVM model) {
            var actions = MarketingActionService.GetAll().IsActive()
                .Where(a => a.IsAdvert && !a.IsSecret && a.IsOrg).OrderBy(a => a.WebSortOrder);
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.ActionList,
                    actions.ToList()));
        }
        public void AddControlForUnlimit(SimplePageVM model, PageController controller) {
	        var urlHelper = controller.Url;
	        var user = controller.User;
	        var link = GetUnlimitOrderButton(urlHelper, user);
	        var desc = TemplateEngine.GetText(model.Entity.Description, new {OrderButton = link.ToString()});
	        
	        model.Description = new TextWithInfoTags(desc);
        }

	    public static TagA GetUnlimitOrderButton(UrlHelper urlHelper, User user) {
		    var link = urlHelper.Cart().OrderUnlimit(Images.Main("cart-unlimit.gif"))
			    .Style("text-decoration: none;");
		    if (user != null) {
			    link.Class("open-in-dialog");
		    }
		    return link;
	    }

	    public void AddControlForMarketingActions(SimplePageVM model) {
            var actions = MarketingActionService.GetAll().IsActive()
                .Where(a => a.IsAdvert && !a.IsSecret && !a.IsSpecialOffer).OrderBy(a => a.WebSortOrder);
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.Actions,
                    actions.ToList()));
        }

	    public void AddControlForSpecialActions(SimplePageVM model) {
            var actions = MarketingActionService.GetAll().IsActive()
                .Where(a => a.IsSpecialOffer).OrderBy(a => a.WebSortOrder);
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.ActionList,
                    actions.ToList()));
        }

        public void AddControlForUsefulInformation(SimplePageVM model) {
            var usefulInformation = UsefulInformationService.GetAll().IsActive()
                .OrderBy(a => a.UpdateDate);
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.UsefulInformation,
                    usefulInformation.ToList()));
        }

        public void AddControlForPartnerVacancy(SimplePageVM model) {
            var vacancies = VacancyService.GetAll().IsActive().Where(v => v.IsPartner)
                .OrderByDescending(v => v.PublishDate).Take(50).ToList();
    		
            model.Controls.Add(
                new SimplePageVM.Control(Views.Page.PartnerVacancies, 
                    vacancies));
        }
        public void AddControlForProbation(SimplePageVM model) {
            var vacancies = VacancyService.GetAll().IsActive().Where(v => v.Type == CenterVacancyType.Probation)
                .OrderByDescending(v => v.PublishDate).Take(50).ToList();
    		
            model.Controls.Add(
                new SimplePageVM.Control(Views.Page.PartnerVacancies, 
                    vacancies));
        }

        public void AddControlForOpenClasses(SimplePageVM model) {
        	var openClasses = GroupService.GetPlannedAndNotBegin()
        		.Where(x => x.IsOpenLearning).Take(30).ToList();
    		
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.NearestGroupList, 
                    new NearestGroupsVM(openClasses)));

            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.OpenClasses,null));
        }
        public void AddControlForUnlimitWithCharge(SimplePageVM model) {
	        var courseTCs = PriceService.CourseWithUnlimitePrice();
	        var courses = courseTCs.Select(x => Tuple.Create(
				CourseService.GetAllCourseNames().GetValueOrDefault(x.Key), x.Value))
				.Where(x => x.Item1 != null).ToList();
	        AddCourseTable(model, courses, true);
        }

	    public void AddControlForUnlimitWithoutCharge(SimplePageVM model, bool isEng = false) {
		    AddUnlimitWithoutChargeCourses(model, isEng);
	    }

	    public void AddControlForWebinarInv(SimplePageVM model, bool isEng = false) {
		    AddUnlimitWithoutChargeCourses(model, isEng);
	    }

	    private void AddUnlimitWithoutChargeCourses(SimplePageVM model, bool isEng) {
		    var courseTCs = PriceService.CourseWithUnlimite().Except(PriceService.CourseWithUnlimitePrice().Keys);
		    var courses = courseTCs.Select(x => Tuple.Create(
			    CourseService.GetAllCourseNames().GetValueOrDefault(x), 0M)).Where(x => x.Item1 != null).ToList();
		    AddCourseTable(model, courses, false, isEng);
	    }

	    private static void AddCourseTable(SimplePageVM model, List<System.Tuple<Course, decimal>> courses,
			bool showPrice, bool isEng = false) {
		    var courseTitle = isEng ? "Course" : "Курс";
		    var hourTitle = isEng ? "Hours" : "Часы";
		    var priceTitle = isEng ? "Price" : "Цена БО";

		    var table = H.table[H.Head(courseTitle, hourTitle, showPrice ? priceTitle : null),
			    courses.OrderBy(x => x.Item1.GetNameOrEng(isEng))
				.Select(x => H.Row2(H.td[Links.CourseLink(null, x.Item1.UrlName, x.Item1.GetNameOrEng(isEng))].Style("text-align:left;"),
				    (int)x.Item1.BaseHours,
				    showPrice ? x.Item2.MoneyString() : null))].Class("table");
		    model.Controls.Add(
			    new SimplePageVM.Control(table.ToString()));
	    }

	    public void AddControlForAboutDiplom(SimplePageVM model) {
		    var diploms = CourseService.DiplomTracks().ToList();
		    var sections = UniqCourseInSection(GroupVMService.GetSectionCourseTCs(diploms));
		    var data = sections.Select(x => Tuple.Create(x.Key, 
				CourseListVMService.GetAll(new TrackListVM {IsDiplomPage = true, 
					Courses = x.ToList()}).FluentUpdate(y => y.EntityName = x.Key.Name))).ToList();
		    var list = new DiplomProgramListVM {
			    List = data
		    };
			model.Controls.Add(new SimplePageVM.Control(Views.Page.DiplomProgramList, list));
	    }


	    public void AddControlForTrainingPrograms(SimplePageVM model, PageController page) {
		    var list = GetTrainingPrograms(page);
		    model.Controls.Add(new SimplePageVM.Control(Views.Page.TrainingProgramList, list));
	    }

	    private DiplomProgramListVM GetTrainingPrograms(PageController page) {
		    return MethodBase.GetCurrentMethod().CacheDay(() => {
			    var programs = CourseService.GetAllHitTracks()
				    .Select(x => x.Course_TC).Except(CourseTC.HalfTracks.Keys).ToList();
			    var sectionCourseTCs = GroupVMService.GetSectionCourseTCs(programs);
			    var sections = UniqCourseInSection(sectionCourseTCs);
			    var data = sections.Select(x => Tuple.Create(x.Key,
				    CourseListVMService.GetAll(new TrackListVM {
					    IsTrainingProgramsPage = true,
					    Courses = x.ToList()
				    }).FluentUpdate(y => {
					    y.EntityName = x.Key.Name;
					    y.EntityUrl = page.Html.Url().Section().Urls.Details(x.Key.UrlName);
				    }))).ToList();
			    var list = new DiplomProgramListVM {
				    List = data
			    };
			    return list;

		    });
	    }

	    private static IEnumerable<IGrouping<Section, string>> UniqCourseInSection(List<Grouping<Section, string>> sectionCourseTCs) {
		    var sections = sectionCourseTCs
			    .SelectMany(x => x.Select(y => new {course = y, section = x.Key}))
			    .GroupBy(x => x.course, x => x.section)
			    .Select(x => new {course = x.Key, group = x.First()})
			    .GroupBy(x => x.@group, x => x.course, new GenericComparer<Section>(x => x.Section_ID));
		    return sections;
	    }

	    public void AddControlForIntramuralExtramural(SimplePageVM model) {
        	var openClasses = GroupService.GetPlannedAndNotBegin()
        		.Where(x => x.IsIntraExtramural).Take(30).ToList();

			var groups = GroupVMService.GetSectionGroups(openClasses)
				.Select(x => Tuple.Create(x.Key, new NearestGroupsVM(x.ToList()))).ToList();

			model.Controls.Add(
                new SimplePageVM.Control(Views.Shared.Education.NearestGroupListWithSections, groups));

           
        }


        public void AddControlForSingUpWebinar(SimplePageVM model) {
        	var groups = GroupService.GetPlannedAndNotBegin().NotSpecial()
        		.Where(x => x.WebinarExists).Take(30).ToList();
    		
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.NearestGroupList, 
                    new NearestGroupsVM(groups)));
        }
        public void AddControlForMicrosoftSeminars(SimplePageVM model) {
        	var groups = CourseListVMService.GetSeminars(false)
				.GroupSeminars.Where(x => x.Group.IsMicrosoftseminar).ToList();
	        var msSeminars = GroupService.GetPlannedAndNotBegin().Where(
		        x => CourseTC.MsSeminars.Contains(x.Course_TC))
				.Select(x => new GroupSeminar(x)).ToList();
    			
            model.Controls.Add(
                new SimplePageVM.Control(Views.Shared.Education.SeminarList, 
                    groups));
			if (msSeminars.Any()) {
	            model.Controls.Add(
	                new SimplePageVM.Control(Views.Shared.Education.SeminarList, 
	                    msSeminars,"Расписание семинаров First Look"));
			}
        }


        public void AddControlRecruiter2011(SimplePageVM model) {
        	var sections = SectionService.GetAll(x => new[] {370, 371}.Contains(x.Section_ID)).ToList();
            model.Controls.Add(
                new SimplePageVM.Control(Views.Page.Recruiter2011,sections));
        }

        public void AddControlSpecialistTV(SimplePageVM model) {
			VideoService.LoadWith(x => x.VideoCategory);
			var videos = VideoService.GetAll(x => x.IsActive && x.IsNew).OrderByDescending(x => x.VideoID).ToList();
			var newVideos = VideoService.GetAll(x => x.IsActive)
				.OrderByDescending(x => x.VideoID).Take(3).ToList();
        	var videoCategories = VideoCategoryService.GetAll().Where(x => x.ParentId == null).ToList();
        	model.Controls.Add(
                new SimplePageVM.Control(Views.Page.Videos,
					new VideosVM{Videos = videos,
						NewVideos = newVideos,
				Categories = videoCategories}));
        }
        public void AddControlForClassIpCamera(SimplePageVM model) {
        	/*var date = DateTime.Now;
        	var lecture = LectureService.GetAll(x => x.LectureDateBeg <= date && x.LectureDateEnd >= date
				&& x.ClassRoom_TC == "МС2")
        		.FirstOrDefault();
        	var groups = new List<Group>();
			if(lecture != null)*/
			var	groups = GroupService.GetPlannedAndNotBegin().Where(x => x.Discount.HasValue).Take(10);
            model.Controls.Add( new SimplePageVM.Control(PartialViewNames.IpCamera, groups));
        }

        public void AddControlForDiscounts(SimplePageVM model) {
      /*      var actions = MarketingActionService.GetAll().IsActive()
                .Where(a => !a.IsAdvert && !a.IsSecret);
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.Discounts,
                    actions.ToList()));*/
        }

        public void AddControlForReserve(SimplePageVM model) {
            var reserve = MarketingActionService.GetAll().BySysName(
                MarketingActions.Reserve);
            model.Controls.Add(
                new SimplePageVM.Control(PartialViewNames.Reserve,
                   new ReserveVM{Discounts = reserve
                       .Discounts.Where(d => d.IsActive).ToList()} ));
        }

		 public void AddControlForCollectionMcts(SimplePageVM model) {
            model.Controls.Add(
                new SimplePageVM.Control(Views.Page.CollectionMctsForm,
                   new CollectionMctsFormVM()));
        }

        public void AddControlForCareer(SimplePageVM model) {
//            var vacancies = VacancyService.GetAll().IsActive().OrderByDescending(v =>
//                v.IsHot
//                ).OrderByDescending(v => v.PublishDate).Take(CommonConst.MaxHotVacansyCount);
            var vacancies = VacancyService.GetAll().IsActive().Where(v => v.IsHot && !v.IsPartner &&
				v.Type != CenterVacancyType.Probation)
                .OrderByDescending(v => v.PublishDate);
//            if (vacancies.Any(v => v.IsHot))
//                vacancies = vacancies.Where(v => v.IsHot);
            model.RightColumnControls.Add(
                new SimplePageVM.Control(PartialViewNames.VacanciesBlock, vacancies));
        }

        public void AddControlForWebinar(SimplePageVM model) {
            var groups = GroupService.GetPlannedAndNotBegin()
                .Where(g => g.WebinarExists)
				.NotSeminars()
                .Take(CommonConst.WebinarCount);
            model.RightColumnControls.Add(
                new SimplePageVM.Control(PartialViewNames.WebinarsBlock, groups));
        }

        public void AddControlForWebinarResponses(SimplePageVM model) {
            var responses = ResponseServise.GetRandomForWebinar();
            model.Controls.Add(
             new SimplePageVM.Control(Views.Shared.Common.ResponseList, responses) {
             	OnBottom = true
             });
        }
        public void AddControlForIntraExtraResponses(SimplePageVM model) {
            var responses = ResponseServise.GetAll(x => x.IsActive && x.IsIntraExtra)
				.OrderByDescending(x => x.ResponseID)
				.Take(20).ToList();
            model.Controls.Add(
             new SimplePageVM.Control(Views.Shared.Common.ResponseList, responses) {
             	OnBottom = true
             });
        }
        public void AddControlForDiplomResponses(SimplePageVM model) {
            var responses = ResponseServise.GetAll(x => x.IsActive && x.IsDiplom)
				.OrderByDescending(x => x.ResponseID)
				.Take(20).ToList();
            model.Controls.Add(
             new SimplePageVM.Control(Views.Shared.Common.ResponseList, responses) {
             	OnBottom = true
             });
        }

		public void AddControlForTestingCenter(SimplePageVM model) {
			var vendors = VendorService.GetAll(x =>
				Vendor.ForTest.Contains(x.Vendor_ID) && x.IsActive).ToList()
				.OrderBy(x => Vendor.ForTest.IndexOf(x.Vendor_ID)).ToList();
            model.Controls.Add(
             new SimplePageVM.Control(Views.Shared.Catalog.Vendors, vendors) {
             	OnBottom = true
             });
        }

        public void AddControlForPersonalManager(SimplePageVM model) {
	        AddManagers(model, new List<string>(), EmpGroups.Team);
        }
        public void AddControlForOflManager(SimplePageVM model) {
	        AddManagers(model, EmpGroups.Ofl);
        }

	    private void AddManagers(SimplePageVM model, List<string> empGroups, string starWith = null) {
		    var managers =
			    EmployeeService.GetAll();
			managers = starWith != null 
				? managers.Where(e => e.EmpGroup_TC.StartsWith(starWith))
				: managers.Where(e => empGroups.Contains(e.EmpGroup_TC));
			var result = managers
				    .CommonList().ToList()
				    .Select(c => new SubSectionWithNote(c, GetManagerNote(c)))
				    .ToList();

		    model.Controls.Add(
			    new SimplePageVM.Control(PartialViewNames.SubSectionsWithNote,
				    new SubSectionsWithNoteVM {
					    List = result,
					    Title = "Менеджеры"
				    }
				    ));
	    }
    }
}
