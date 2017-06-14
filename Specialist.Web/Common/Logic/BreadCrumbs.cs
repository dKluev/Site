using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using SimpleUtils;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Education.ViewModel;
using Specialist.Entities.Examination.ViewModel;
using Specialist.Entities.Message.ViewModel;
using Specialist.Entities.Order.ViewModel;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Profile.ViewModel.Common;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog;
using Specialist.Services.Cms;
using Specialist.Services.Core;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Center;
using Specialist.Web.Controllers.Shop;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Views;
using Specialist.Web.Helpers;
using SimpleUtils.Reflection;
using Specialist.Services.Common.Extension;
using System.Linq;
using Specialist.Web.Pages;
using Specialist.Web.Root.Center.ViewModels;
using Specialist.Web.Root.Exams.ViewModels;
using Microsoft.Practices.Unity;
using Specialist.Web.Root.Learning.ViewModels;
using Specialist.Web.Root.OrgTests.ViewModels;
using Specialist.Web.Root.PlannedTests.ViewModels;
using Specialist.Web.Root.Profile.ViewModels;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using Specialist.Web.ViewModel.Orders;
using SpecialistTest.Web.Core.Mvc.Extensions;

namespace Specialist.Web.Common.Logic
{
    public class BreadCrumbs
    {
        private readonly MainMenu _mainMenu;
        HtmlHelper _helper = null;
        private string Home;

        [ThreadStatic]
        private static BreadCrumb root;

        public static BreadCrumb GetBreadCrumbPart(HtmlHelper _helper)
        {
            if(root == null)
                root = new BreadCrumb(typeof(MainPageVM), 
                    HtmlControls.Anchor("/", "Главная").ToString()).Add(
                new BreadCrumb(typeof(ProfileVM), 
                    _helper.Profile())
                    .Add(
                    new BreadCrumb(typeof(EditProfileVM), 
                        _helper.ActionLink<ProfileController>(
                            c => c.EditProfile(), "Редактирование профиля").ToString()),
                    new BreadCrumb(typeof(ChangePasswordVM), 
                        _helper.ActionLink<ProfileController>(
                            c => c.ChangePassword(), "Сменить пароль").ToString()),
                    new BreadCrumb(typeof(SubscribesVM)),
                    new BreadCrumb(typeof(StudyTypeStatsVM)),
                    new BreadCrumb(typeof(UploadStudentPhotoVM)),
                    new BreadCrumb(typeof(CertificateListVM)),
                    new BreadCrumb(typeof(RealSpecialistVM)),
                    new BreadCrumb(typeof(WorkPlace)),
                    new BreadCrumb(typeof(GroupPhotosVM)),
                    new BreadCrumb(typeof(ClassmatesVM)),
                    new BreadCrumb(typeof(SeminarListVM)),
                    new BreadCrumb(typeof(SeminarCompleteVM)),
                    new BreadCrumb(typeof(AddSeminarVM)),
                    new BreadCrumb(typeof(PlanTestUserStatsVM)),
                    new BreadCrumb(typeof(PlanTestQuestionStatsVM)),
                    new BreadCrumb(typeof(LearningVM), 
                        _helper.ActionLink<ProfileController>(
                            c => c.Learning(), "Мои курсы").ToString())
                        .Add(
                        new BreadCrumb(typeof(GroupVM)),
						new BreadCrumb(typeof(CoursePlannedTestVM))
                        ),
                    new BreadCrumb(typeof(EditExamQuestionnaireVM)),
                    new BreadCrumb(typeof(ChangeStatusVM)),
                    new BreadCrumb(typeof(FileListVM)),
                    new BreadCrumb(typeof(OrgGroupSearchVM)),
                    new BreadCrumb(typeof(OrgStudentVM)),
                    new BreadCrumb(typeof(OrgFileListVM)),
                    new BreadCrumb(typeof(OrgListVM)),
                    new BreadCrumb(typeof(OrgStatusUpdateVM)),
                    new BreadCrumb(typeof(OrgGroupVM)),
                    new BreadCrumb(typeof(OrgRealSpecialistVM)),
                    new BreadCrumb(typeof(TrainerCoursesVM)),
                    new BreadCrumb(typeof(TrainerGroupsVM)),
                    new BreadCrumb(typeof(EditSuccessStoryVM)),
                    new BreadCrumb(typeof(MyResponses)),
                    new BreadCrumb(typeof(LibraryVM)),
                    new BreadCrumb(typeof(MarketingActionsVM)),
                    new BreadCrumb(typeof(PrivateMessageListVM)),
                    new BreadCrumb(typeof(CompanyTestResultsVM)),
                    new BreadCrumb(typeof(TestEditVM)),
                    new BreadCrumb(typeof(TestReadOnlyVM)),
                    new BreadCrumb(typeof(CompanyTestsVM)),
                    new BreadCrumb(typeof(TestResultVM)),
                    new BreadCrumb(typeof(UserTestsVM), _helper.Url().UserTests().ToString()),
                    new BreadCrumb(typeof(TestCertificatesVM)),
                    new BreadCrumb(typeof(GroupInfosVM), 
						_helper.Url().GroupTests().ToString()).Add(new BreadCrumb(typeof(GroupInfoVM)), 
					new BreadCrumb(typeof(GroupTestResultVM))),
                    new BreadCrumb(typeof(GroupPrepareVM)),
                    new BreadCrumb(typeof(TestListVM)),
                    new BreadCrumb(SimplePages.HostingInfo),
                    new BreadCrumb(typeof(CompetitionsVM),
                        _helper.ActionLink<CenterController>(
                            c => c.Competitions(), "Конкурсы").ToString())
                        .Add(
                        new BreadCrumb(typeof(CompetitionVM))
                        ),
                    new BreadCrumb(typeof(CompanyFileListVM), 
						_helper.Url().CompanyFile().List("Файлы компаний").ToString()).Add(
						new BreadCrumb(typeof(CompanyFileVM))
					),
                    new BreadCrumb(typeof(UserFileListVM), 
                        _helper.ActionLink<FileController>(
                            c => c.List(1), "Материалы преподавателя").ToString())
                        .Add(
                        new BreadCrumb(typeof(UserFileVM))
                        ),
                    new BreadCrumb(typeof(UserExamQuestionnaire), 
                        _helper.ActionLink<ProfileController>(
                            c => c.ExamQuestionnaire(null), "Анкета для сдачи экзаменов").ToString())
                    )
                );
            return root;

        }

	    public static object GetModel(ViewMasterPage page) {
	    	var baseVM = page.Model as BaseVM;
			if(baseVM != null) {
				return baseVM.MainModel;
			}
		    return page.Model;
	    }
        object _model;
        SimplePageService SimplePageService = 
            new SimplePageService(new ContextProvider());
        public BreadCrumbs(ViewMasterPage page, MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
            _helper = page.Html;
            _model = GetModel(page);

        }

        private void AddLink(List<string> list, object obj)
        {
			if(obj == null)
				return;
            list.Add(_helper.GetLinkFor(obj));
        }

        public List<string> GetBreadCrumbs(object model)
        {
              return new List<string>();
        }

        public List<string> GetBreadCrumbs(EntityCommonVM model)
        {
            var breadcrumb = new List<string>();
            model.Entity.Match<SimplePage>(
                x => breadcrumb.AddRange(GetBreadCrumbs(x)));
            return breadcrumb;
        }


    	public List<string> GetBreadCrumbs(EmployeeVM model) {
    		return GetBreadCrumbs(SimplePageService.GetAll()
    			.SysName(SimplePages.Managers), true);
    	}

    	public List<string> GetBreadCrumbs(JubileeGroupsVM model) {
    		return GetBreadCrumbs(SimplePageService.GetAll()
    			.SysName(SimplePages.SpecialOffers), true);
    	}

    /*	public List<string> GetBreadCrumbs(GroupsWithDiscountVM model) {
            var breadcrumb = new List<string>();
    		AddLink(breadcrumb, model.Course);
    		return breadcrumb;
    	}*/
    	public List<string> GetBreadCrumbs(AboutTrainerVM model) {
    		return GetBreadCrumbs(SimplePageService.GetAll()
    			.SysName(SimplePages.Trainers), true);
    	}

	    public const string Separator = "|";

        public List<string> GetBreadCrumbs(CourseVM model)
        {
            var breadcrumb = new List<string>();
            Action<object> addLink = x => AddLink(breadcrumb, x);
            model.Sections.ForEach(x => {
	            AddLink(breadcrumb, x);
				breadcrumb.Add(Separator);
            });
            model.AuthorizationVendor.NotNull(x => {
	            addLink(x);
				breadcrumb.Add(Separator);
            });
            model.Product.NotNull(x => addLink(x));
            return breadcrumb;
        }


        public List<string> GetBreadCrumbs(TrackVM model)
        {
            var breadcrumb = new List<string>();
            model.Sections.ForEach(x => AddLink(breadcrumb, x));
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(Video model)
        {
            var breadcrumb = new List<string>();
			breadcrumb.Add(_helper.Url().Videos().ToString());
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(VideoCategoryVM model)
        {
            var breadcrumb = new List<string>();
			breadcrumb.Add(_helper.Url().Videos().ToString());
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(GroupVideosVM model)
        {
            var breadcrumb = new List<string>();
			breadcrumb.Add(_helper.Profile());
			breadcrumb.Add(_helper.Url().Group().Details(model.Group.Group_ID, "Страница группы").ToString());
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(TestVM model)
        {
            var breadcrumb = new List<string>();
			breadcrumb.Add(_helper.Url().Tests().ToString());
            model.Sections.ForEach(x => breadcrumb.Add(_helper.Url().TestSectionLink(x).ToString()));
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(TestRunDetailsVM model)
        {
            var breadcrumb = new List<string>();
			if(model.CourseTC == null) {
				breadcrumb.Add(_helper.Url().Tests().ToString());
				breadcrumb.Add(_helper.Url().TestLink(model.Test).ToString());
			}
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(TestSectionVM model)
        {
            var breadcrumb = new List<string>();
			breadcrumb.Add(_helper.Url().Tests().ToString());
            return breadcrumb;
        }
/*
        public List<string> GetBreadCrumbs(BaseVM model) {
        	var innerModel = model.Parts.Select(x => x.Model).FirstOrDefault();
			if(innerModel == null)
				return new List<string>();
            return GetBreadCrumbs((dynamic)innerModel);
        }
*/

        public List<string> GetBreadCrumbs(ExamVM model)
        {
            var breadcrumb = new List<string>();
            if (model.Exam.Vendor != null)
            {
                var vendor = model.Exam.Vendor;
                breadcrumb.Add(_helper.VendorLink(vendor));
                breadcrumb.Add(_helper.VendorExamLink(vendor));
            }
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(ExamSearchVM model)
        {
            var breadcrumb = new List<string>();
            var vendor = model.Vendor;
            breadcrumb.Add(_helper.VendorLink(vendor));
            breadcrumb.Add(_helper.VendorExamLink(vendor));
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(CertificationVM model) {
            var breadcrumb = new List<string>();
            if (model.Certification.Vendor != null) {
                var vendor = model.Certification.Vendor;
                breadcrumb.Add(_helper.VendorLink(vendor));
				if (!vendor.CertificationDescription.IsEmpty()) {
					breadcrumb.Add(Separator);
					var url = _helper.Url().Link<VendorController>(c =>
						c.Details(vendor.UrlName, VendorVM.Tab.Certifications, null), 
						"Сертификации " + vendor.Name).ToString();
					breadcrumb.Add(url);

				}
            }
            return breadcrumb;
        }


        public List<string> GetBreadCrumbs(SectionVM model)
        {
            var breadcrumb = new List<string>();
            if (model.Parent != null)
            {
                AddLink(breadcrumb, model.Parent);
            }
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(ComplexVM model)
        {
            var breadcrumb = GetBreadCrumbs(new CityVM{City =
			model.Complex.BranchOffice.City
			});
            
			AddLink(breadcrumb, model.Complex.BranchOffice.City);
			
            return breadcrumb;
        }

		public List<string> GetBreadCrumbs(CityVM model)
		{
			var breadcrumb = new List<string>();
			breadcrumb.Add(HtmlControls.Anchor(MainMenu.Urls.Locations, 
				"Классы и филиалы").ToString());
			return breadcrumb;
		}

        public List<string> GetBreadCrumbs(SimplePage model)
        {
            return GetBreadCrumbs(model, false, false);
        }

        public List<string> GetBreadCrumbs(SimplePage model, bool includeIt) {
            return GetBreadCrumbs(model, includeIt, false);
        }

        public List<string> GetBreadCrumbs(SimplePage model, bool includeIt,
            bool withoutRoot)
        {
            var breadcrumb = new List<string>();
	        if (model == null) {
		        return breadcrumb;
	        }
            if(model.SysName != null)
            {
                var path = GetBreadCrumbPart().GetPath(model.SysName);
                if(path != null)
                    return path.Select(bc => bc.Link).Reverse().ToList();
            }
            var parent = model.MainParent;
            var parents = new List<SimplePage>();
            while (parent != null)
            {
                parents.Add(parent);
                parent = parent.MainParent;
            }
            parents.Reverse();
            if (includeIt || withoutRoot)
                parents = parents.Skip(1).ToList();
            foreach (var page in parents.Where(p => !p.WithoutLink))
            {
                AddLink(breadcrumb, page);
            }
            if(includeIt && !model.WithoutLink)
                AddLink(breadcrumb, model);
            return breadcrumb;
        }
        
        private BreadCrumb GetBreadCrumbPart() {
            return GetBreadCrumbPart(_helper);
        }

        public List<string> GetBreadCrumbs(NewsVM news)
        {
            var breadcrumb = new List<string>();
	        var type = news.News.NewsType;
	        var url = _helper.Url().SiteNews().List(type.UrlName, null, type.Name);
            breadcrumb.Add(url.ToString());
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(AllNewsVM news)
        {
            var breadcrumb = new List<string>();
            breadcrumb.Add(_helper.ActionLink<SiteNewsController>(
                c => c.List(NewsType.Main, null), "Новости")
                .ToString());
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(RelationNewsVM news)
        {
            var breadcrumb = new List<string>();
            breadcrumb.Add(_helper.News().ToString());
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(AdviceVM model)
        {
            var breadcrumb = new List<string>();
            breadcrumb.Add(_helper.Advices().ToString());
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(VacancyListVM model) {
            return GetBreadCrumbs(model.Career, true);
        }

        public List<string> GetBreadCrumbs(MarketingActionVM model) {
            return GetBreadCrumbs(model.MarketingActions, true);
        }


        public List<string> GetBreadCrumbs(CorporateClientsVM model) {
            return GetBreadCrumbs(model.Page, false, true);
        }

        public List<string> GetBreadCrumbs(OrgResponseVM model) {
            return GetBreadCrumbs(model.CorporateClients, true);
        }

        public List<string> GetBreadCrumbs(UserWorksVM model) {
            var breadcrumb = new List<string>();
			breadcrumb.Add(_helper.ActionLink<ClientController>(c => 
				c.PrivatePerson(SimplePages.Urls.Works, null), 
				"Работы выпускников").ToString());
            if(model.WorkSection != null)
                breadcrumb.Add(_helper.UserWorks(model.Section));
            return breadcrumb;
        }

        public List<string> GetBreadCrumbs(SuccessStoryVM model) {
            var breadcrumb = new List<string>();
			breadcrumb.Add(_helper.ActionLink<ClientController>(c => 
				c.PrivatePerson(SimplePages.Urls.SuccessStories, null), 
				"Истории успеха").ToString());
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(VacancyVM model) {
            var breadcrumb = GetBreadCrumbs(model.Career, true);
            breadcrumb.Add(_helper.ActionLink<CenterController>(c =>
                c.Vacancies(null), "Вакансии").ToString());
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(ExtrasesVM model) {
	        return _.List(_helper.Card().ToString());
        }

        public List<string> GetBreadCrumbs(EditCartVM model) {
	        return _.List(_helper.Card().ToString());
        }
        public List<string> GetBreadCrumbs(OrgOrderCompleteVM model) {
	        return _.List(_helper.Card().ToString());
        }

        public List<string> GetBreadCrumbs(ExamListVM model) {
            return new List<string>{_helper.VendorLink(model.Vendor)};
        }

        public List<string> GetBreadCrumbs(Product model) {
			var breadcrumb = new List<string>();
        	var service = MvcApplication.Container.Resolve<ISiteObjectService>();
			AddLink(breadcrumb, 
				service.GetByRelationObject<Section>(model).FirstOrDefault());
			AddLink(breadcrumb, 
				service.GetByRelationObject<Vendor>(model).FirstOrDefault());
			AddLink(breadcrumb, 
				service.GetByRelationObject<Product>(model).FirstOrDefault());
            return breadcrumb;
        }
        public List<string> GetBreadCrumbs(SiteTerm model) {
			var breadcrumb = new List<string>();
        	var service = MvcApplication.Container.Resolve<ISiteObjectService>();
			AddLink(breadcrumb, 
				service.GetByRelationObject<Section>(model).FirstOrDefault());
            return breadcrumb;
        }

        public List<string> Get()
        {
            if(_helper == null)
                return null;
            var breadCrumbPart = GetBreadCrumbPart();
			if(_model != null) {
				var path = breadCrumbPart.GetPath(_model.GetType());
				if (path != null)
					return path.Select(bc => bc.Link).Reverse().ToList();
            	
			}
            var breadcrumb = new List<string>();
            Home = HtmlControls.Anchor("/", "Главная").ToString();

         


          

            var model = _model;
            var pageSysName = string.Empty;
            var isSimplePage = false;
            model.Match<EntityCommonVM>(x =>
                x.Entity.Match<SimplePage>(y => {
                    pageSysName = y.SysName;
                    isSimplePage = true;
                }));
            if (_mainMenu != null && _mainMenu.PageSysName != pageSysName
                && !isSimplePage)
                breadcrumb.Add(HtmlControls.Anchor(_mainMenu.Url, _mainMenu.Name)
                    .ToString());
			if(model == null)
				return breadcrumb;
            var breadCrumbsbyDispatch = GetBreadCrumbs((dynamic)model) as List<string>;
            if(breadCrumbsbyDispatch.IsEmpty())
            {
                var forumBreadCrumbs = new ForumBreadCrumbs(_helper);
                breadCrumbsbyDispatch = forumBreadCrumbs.GetBreadCrumbs((dynamic)model);
            }


            breadcrumb.AddRange(breadCrumbsbyDispatch);
            if(!(_model is MainPageVM) && breadcrumb.All(s => !s.Contains(@"""/""")  ))
                breadcrumb.Insert(0, Home);
            return breadcrumb;
           
        }
    }
}