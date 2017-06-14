using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Microsoft.Practices.Unity;
using MvcContrib.Attributes;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Center.ViewModel;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Tests;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using System.Linq.Dynamic;
using Microsoft.Web.Mvc;
using Specialist.Web.Core;
using Specialist.Web.Root.Center.ViewModels;
using Specialist.Web.Util;
using Specialist.Entities.Context.Const;
using Specialist.Services.Common.Extension;
using Specialist.Entities.Passport;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Controllers
{
    public class EmployeeController : ViewController
    {
        [Dependency]
        public IEmployeeVMService EmployeeVMService { get; set; }

        [Dependency]
        public IEmployeeService EmployeeService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public IMailService MailService { get; set; }


        [Dependency]
        public IRepository<SimplePage> SimplePageService { get; set; }

        [Dependency]
        public IRepository2<UserWork> UserWorkService { get; set; }

		[Dependency]
        public IRepository2<EmployeeCertification> EmployeeCertificationService { get; set; }


        [Dependency]
        public IResponseService ResponseService { get; set; }

        [Dependency]
        public IRepository<OrgResponse> OrgResponseService { get; set; }

        [Dependency]
        public IRepository<Advice> AdviceService { get; set; }

        [Dependency]
        public TestService TestService { get; set; }

        [Dependency]
        public IRepository<Video> VideoService { get; set; }

        public ActionResult Trainer(string employeeTC)
        {
			if(employeeTC.IsEmpty())
				return ErrorView(HttpStatusCode.NotFound);

            employeeTC = employeeTC.ToUpper();
            var model = EmployeeVMService.GetEmployee(employeeTC);
			if(model == null)
				return NotFound();			
            return View(ViewNames.Details, model);
        }



		[AjaxOnly]
		public virtual ActionResult TrainersJson(string query) {
			var names = EmployeeService.GetAllTrainers().Where(x =>
				x.FullName.ToLower().Contains(query.ToLower())).Select(x => x.FullName).ToList();
			return Json(new {query, suggestions = names}, JsonRequestBehavior.AllowGet);
		}

        public ActionResult AboutTrainer(string employeeTC, string urlName, int? pageIndex)
        {
			if(employeeTC.IsEmpty())
				return NotFound();
            if (!pageIndex.HasValue 
				&& urlName.In(
					SimplePages.Urls.TrainerOrgResponses, 
					SimplePages.Urls.TrainerResponses, 
					SimplePages.Urls.TrainerTests, 
					SimplePages.Urls.TrainerAdvices, 
					SimplePages.Urls.TrainerWorks, 
					SimplePages.Urls.TrainerVideos))
                return RedirectToAction(() => AboutTrainer(employeeTC, urlName, 1));
            var index = pageIndex.GetValueOrDefault() - 1;

            var model = new AboutTrainerVM();
            model.UrlName = urlName;
            model.Page = GetAboutTrainer();
            employeeTC = employeeTC.ToUpper();
            model.Employee = EmployeeVMService.GetEmployee(employeeTC);
			if(model.Employee == null || !model.Employee.Employee.IsTrainer)
				return Redirect(SimplePages.FullUrls.Trainers);
        	var orgResponses = OrgResponseService.GetAll().IsActive()
        		.Where(r => ("," + r.Employee_TC + ",").Contains("," + employeeTC + ","))
        		.OrderByDescending(o => o.UpdateDate);
			var responses = ResponseService.GetAllForEmployee(employeeTC)
					.OrderByDescending(x => x.Course.IsActive)
            		.ThenByDescending(o => o.Rating).ThenByDescending(x => x.UpdateDate);

        	var advices = AdviceService.GetAll().IsActive()
        		.Where(r => r.Employee_TC == employeeTC)
        		.OrderByDescending(o => o.UpdateDate);
        	var tests = TestService.GetAll().Where(x => x.Status == TestStatus.Active)
        		.Where(r => r.Author_TC == employeeTC)
        		.OrderByDescending(o => o.Id);
        	var videos = VideoService.GetAll().IsActive()
        		.Where(r => r.Employee_TC == employeeTC)
				.OrderByDescending(o => o.VideoID);
        	var userWorks = UserWorkService.GetAll().IsActive()
        		.Where(r => r.Trainer_TC == employeeTC)
				.OrderByDescending(o => o.UserWorkID);
        	model.HasOrgResponses = orgResponses.Any();
        	model.HasResponses = responses.Any();
        	model.HasAdvices = advices.Any();
        	model.HasTests = tests.Any();
	        model.HasVideos = videos.Any();
	        model.HasPortfolio = Images.GetGallaryFiles(model.Employee.Employee, "Portfolio").Any();
	        model.HasWorks = userWorks.Any();

            if (model.IsTrainerResponses) {
            	model.Responses = responses.ToPagedList(index, 20);
            }

        	if (model.IsTrainerOrgResponses) {
            	model.OrgResponses = orgResponses.ToPagedList(index, 10);;
            }

            if (model.IsAdvices) {
            	model.Advices = advices.ToPagedList(index, 10);
            }
            if (model.IsTests) {
            	model.Tests = tests.ToPagedList(index, 10);
            }
            if (model.IsVideos) {
            	model.Videos = videos.ToList();
            }
            if (model.IsWorks) {
            	model.UserWorks = userWorks.ToPagedList(index, 10);
            }
			if(model.IsAboutTrainer) {
				model.Certifications = EmployeeCertificationService.GetAll(
					x => x.EmployeeFK_TC == employeeTC && x.Certification.IsActive)
					.OrderBy(x => x.SortOrder)
					.Select(x => x.Certification).ToList();
			}
        	return View(model);
        }

        private SimplePage GetAboutTrainer()
        {
            return SimplePageService.GetAll().BySysName(
                SimplePages.AboutTrainer);
        }

		[HandleNotFound]
        public ActionResult Manager(string employeeTC)
        {
            employeeTC = employeeTC.ToUpper();
            var model = EmployeeVMService.GetEmployee(employeeTC);
			if(model == null || model.Employee.IsTrainer)
				return Redirect(SimplePages.FullUrls.Trainers);
        	
            return MView(ViewNames.Details, model);
        }

        [Auth(RoleList = Role.Trainer)]
        public ActionResult Groups()
        {
            return View(EmployeeVMService.GetGroups());
        }

        [Auth(RoleList = Role.Trainer)]
        public ActionResult Courses()
        {
            return View(EmployeeVMService.GetCourses());
        }

		[Authorize]
		public ActionResult AddResponse(string employeeTC) {
			var model = new AddResponseVM {EmployeeTC = employeeTC,
			Courses = CourseService.GetAllActiveCourseNames().Select(x => 
				new CourseLink{WebName = x.Value,CourseTC = x.Key}).ToList()};
			return BaseView(Views.Employee.AddResponseForm,model);
		}
		[Authorize]
		public ActionResult AddResponsePost(AddResponseVM model) {
			var message = model.Text + H.br +
				model.CourseTC + H.br +
				model.EmployeeTC + H.br;
			MailService.SendResponse(model.EmployeeTC, message);
			return Content("ok");
				
		}
    }
}
