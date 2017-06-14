using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.FluentAttributes.Utils;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Tests;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Utils;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Views;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Services.Common.Extension;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using EntityUtils = Specialist.Web.Util.EntityUtils;

namespace Specialist.Web.Controllers.Tests {
	public class TestController:ViewController {

		[Dependency]
		public ISectionService SectionService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public IGroupVMService GroupVmService { get; set; }
		[Dependency]
		public IRepository2<TestCalc> TestCalcService { get; set; }


		[Dependency]
		public StudentInGroupService StudentInGroupService { get; set; }

		[Dependency]
		public TestService TestService { get; set; }

		[Dependency]
		public IRepository2<UserTest> UserTestService { get; set; }

		[Dependency]
		public IRepository2<TestModuleSet> TestModuleSetService { get; set; }

		[Dependency]
		public IRepository2<User> UserService { get; set; }

		[Dependency]
		public CoursePrerequisiteService CoursePrerequisiteService { get; set; }

		[Dependency]
		public ISiteObjectService SiteObjectService { get; set; }


		[HandleNotFound]
		public ActionResult Details(int id) {
			if (id != 667 && User != null && User.IsRestricted) {
				return Redirect("/");
			}
			TestService.LoadWith(x => x.Author);
			var test = TestService.GetByPK(id);
			if(test == null)
				return null;
			var isActiveCalc = StudentInGroupService.CalcTestIsActive(test);
			if (!isActiveCalc && (test.Status == TestStatus.Archive || test.CompanyId.HasValue)) {
				return null;
			}
			var testStat = TestCalcService.GetByPK(id) ?? new TestCalc();
			var testStats = _.List(
				Tuple.Create("Пользователей сдавало", testStat.UserCount),
				Tuple.Create("Всего попыток", testStat.TryCount),
				Tuple.Create("Сданных тестов", testStat.PassCount)
				);
			var model = new TestVM {
				Test = test,
				NextTests = SiteObjectService.GetSingleRelation<Test>(test)
				.ToList(),
				IsActiveCalc =  isActiveCalc,
				PrevTests = SiteObjectService.GetByRelationObject<Test>(test).ToList() ,
				Courses = SiteObjectService.GetSingleRelation<Course>(test).ToList(),
				Sections = SiteObjectService.GetSingleRelation<Section>(test).ToList(),
				TestStats = testStats
			};
			return BaseView(PartialViewNames.Details, model);
		}

		[HandleNotFound]
		public ActionResult Prerequisite(decimal courseId) {
			var course = CourseService.FirstOrDefault(x => x.Course_ID == courseId);
			var courseTC = course.Course_TC;
			var coursePrerequisite = CoursePrerequisiteService.GetForCourse(courseTC).FirstOrDefault();
			if (coursePrerequisite == null)
				return null;
			TestService.LoadWith(x => x.Author);
			var test = TestService.GetByPK(coursePrerequisite.Test_ID);
			var preCourses = EntityUtils.GetCoursePreCourses(CoursePrerequisiteService, courseTC)
				.Select(x => x.RequiredCourse).ToList();
			var model = new PrerequisiteTestVM {
				Test = test,
				CoursePrerequisite = coursePrerequisite,
				Course = course,
				PrerequisiteCourses = preCourses
			};

			return BaseView(Views.Test.Prerequisite, model);
		}
		[Authorize]
		public ActionResult CoursePlanned(string courseTC) {
			var course = CourseService.GetByPK(courseTC);
			var testId = course.TestId;
			
			var moduleSets = TestModuleSetService.GetAll(x => x.TestId == testId)
				.OrderBy(x => x.Number).ToList();
			var moduleStatuses = UserTestService.GetAll(x =>
				x.IsBest && x.TestId == testId && x.TestModuleSetId.HasValue).ToList()
				.GroupBy(x => x.TestModuleSetId.Value)
				.ToDictionary(x => x.Key, x => x.First());

			var model = new CoursePlannedTestVM {
				ModuleSets = moduleSets,
				Statuses = moduleStatuses,
				Course = course,
			};


			return BaseViewWithModel(new CoursePlannedTestView(), model);
		}

		[HandleNotFound]
		public ActionResult Section(string urlName) {

			var section = SectionService.GetAll().ByUrlName(urlName);
			if(section == null)
				return null;
			var tests = SiteObjectService.GetByRelationObject<Test>(section)
				.Where(x => x.CompanyId == null && x.Status == TestStatus.Active)
				.OrderByDescending(x => x.Id).ToList();
//			if(urlName == "english" && 
//				!tests.Select(x => x.Id).SequenceEqual(TestRecomendations.Tests.Select(x => x.Key))) {
//				Logger.Exception(new Exception("english section tests recomendations"), User);
//			}
			var model = new TestSectionVM {
				Section = section,
				Tests = tests,
			};
			return BaseView(PartialViewNames.Section, model);
		}

		public ActionResult Prerequisites() {
			var courseTCs = CoursePrerequisiteService.GetAll(x => x.Test_ID != null
				&& x.Course.IsActive).Select(x =>
					x.Course_TC).ToList();
	

			var courses = CourseService.GetAll()
				.Where(x => courseTCs.Contains(x.Course_TC)).ToList();
			var sectionCourses = GroupVmService.GetSectionCourses(courses).ToList();

			return BaseView(Views.Test.Prerequisites, 
				new PrerequisiteTestsVM{Courses = sectionCourses });
		}
	
		[ChildActionOnly]
		public ActionResult Best(int? sectionId) {
			var testIds = new List<int>();
			if(sectionId.HasValue) {
				var testIdList = 
					TestService.GetTestSectionRelations().Where(x => x.RelationObject_ID.Equals(sectionId.Value))
					.Select(x => x.Object_ID).ToList();
				testIds = testIdList.Cast<int>().ToList();
			}
			var users = GetUsers(testIds);

			var newTests = TestService.GetAll(x => x.IsNew && x.Status == TestStatus.Active);
			if(testIds.Any())
				newTests = newTests.Where(x => testIds.Contains(x.Id));
			return View(new TestBestVM {
				Users = users,
				NewTests = newTests.OrderByDescending(x => x.Id).ToList()
			});
		}

		private List<User> GetUsers(List<int> testIds) {
			var now = DateTime.Today;
			var start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
			var end = new DateTime(start.Year, start.Month, DateTime.DaysInMonth(start.Year, start.Month));

			var userTests = UserTestService.GetAll(x => x.RunDate >= start 
				&& x.RunDate <= end
					&& UserTestStatus.PassStatuses.Contains(x.Status));
			if(testIds.Any()) {
				userTests = userTests.Where(x => testIds.Contains(x.TestId));
			}
			var bestUserIds = userTests.Where(x => x.User.LastName.ToLower() != "qwe")
				.Select(x => new {x.UserId, x.TestId}).Distinct().GroupBy(x => x.UserId)
				.OrderByDescending(x => x.Count()).Select(x => x.Key).Take(10).ToList();
			return UserService.GetAll(x => bestUserIds.Contains(x.UserID)).ToList();
		}

	}
}