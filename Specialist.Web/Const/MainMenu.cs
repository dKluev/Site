using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Examination.ViewModel;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;

namespace Specialist.Web.Const
{
    public class MainMenu
    {
		public static int ActionsCount { get; set; }
        public static class Urls
        {
            public const string MainCourses = "/courses";
            public const string Center = "/center";
            public const string SpecialOffers = "/special-offers";
            public const string Locations = "/locations";
            public const string Contacts = "/contacts";
            public const string Job = "/job";
            public const string Forum = "/message/forum";
            public const string RealSpecialist = "/special-offers/real-specialist";
        }

    	public bool IsHightlight {
    		get { return Url == Urls.RealSpecialist; }
    	}

        public List<Type> ModelVMList { get; set; }

        public List<Func<object, bool>> Predicates { get; set; }

	    public List<MainMenu> SubMenu { get; set; }

	    public bool IsActions {
		    get { return Url == Urls.SpecialOffers; }
	    }

	    public MainMenu(string name, string url, string pageSysName, List<MainMenu> submenu = null)
        {
			SubMenu = submenu;
            ModelVMList = new List<Type>();
            Predicates = new List<Func<object, bool>>();
            Name = name;
            Url = url;
            PageSysName = pageSysName;
        }

        public MainMenu AddModelTypes(params Type[] modelVM)
        {
            ModelVMList.AddRange(modelVM);
            return this;
        }

        public MainMenu AddPredicates(params Func<object, bool>[] predicates)
        {
            Predicates.AddRange(predicates);
            return this;
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string PageSysName { get; set; }

//        public bool IsSelected { get; set; }

        public bool HasModel(object model)
        {
            if(model == null)
                return false;
            var selected = this.ModelVMList.Contains(model.GetType());

            model.Match<EntityCommonVM>(
                ec =>
                {
                    selected = false;
                    if(PageSysName != null)
                        ec.Entity.Match<SimplePage>(
                            sp => selected = sp.SysName == PageSysName ||
                                sp.RootMainParent
                                    .GetOrDefault(x => x.SysName) == PageSysName);
                });
            if (!selected)
            {
                selected = Predicates.Aggregate(false, (r, p) => r || p(model));
            }
            return selected;
        }

		static List<MainMenu> _all = 
                new List<MainMenu>
                {
                    new MainMenu("Курсы", Urls.MainCourses, SimplePages.Courses)
                    .AddModelTypes( 
                        typeof(MainCoursesVM), 
                        typeof(SectionVM), 
                        typeof(IsNewCoursesVM), 
                        typeof(Profession), 
                        typeof(Product), 
                        typeof(SiteTerm), 
                        typeof(ExamVM), 
                        typeof(ExamListVM), 
                        typeof(CertificationVM), 
                        typeof(TrackVM), 
                        typeof(TrackListVM), 
                        typeof(CourseVM))
                    .AddPredicates(x =>
                    {
                        var selected = false;
                        x.Match<EntityCommonVM>(
                            ec => selected = ! (ec.Entity is SimplePage));
                        return selected;
                    }),
                    new MainMenu("Скидки и акции", 
                        Urls.SpecialOffers, SimplePages.SpecialOffers)
                        .AddModelTypes(typeof(MarketingActionVM)),
                    
						new MainMenu("Обучение онлайн", null, null, _.List(
                        new MainMenu("Безлимитное обучение", SimplePages.FullUrls.Unlimited, null),
                        new MainMenu("Открытое обучение", SimplePages.FullUrls.OpenClasses, null),
                        new MainMenu("Очно-заочное обучение",
							SimplePages.FullUrls.IntraExtramural, null),
                        new MainMenu("Вебинары", SimplePages.FullUrls.Webinar, null)
							)),
						new MainMenu("Дипломные программы", null, null, _.List(
	                        new MainMenu("Дипломные программы", SimplePages.FullUrls.Diplom, null),
	                        new MainMenu("Программы повышения квалификации", SimplePages.FullUrls.TrainingPrograms, null)
							)),
						new MainMenu("Тестирование", SimplePages.FullUrls.Testing, null),
                        new MainMenu("Компаниям", SimplePages.FullUrls.ForCompanies, null),


                    new MainMenu("О Центре", 
                        Urls.Center, SimplePages.Center)
                    .AddModelTypes(
						typeof(NewsVM), 
						typeof(NewsListVM),
						typeof(AllNewsVM),
						typeof(AdviceVM), 
						typeof(AdviceListVM), 
						typeof(RelationNewsVM),
                        typeof(EmployeeVM), 
                        typeof(AboutTrainerVM), 
                        typeof(VacancyListVM),
                        typeof(PrivatePersonVM),
                        typeof(SuccessStoryVM),
                        typeof(UserWorksVM),
                        typeof(CorporateClientsVM),
                        typeof(OrgResponseVM),
                        typeof(VacancyVM)),

						new MainMenu("Контакты", Urls.Contacts, SimplePages.Locations),
                };
        public static List<MainMenu> GetAll() {
	        return _all;
        }


    }
}
