using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using SimpleUtils.Collections;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Tests;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Exceptions;
using Specialist.Web.Common.Mvc.Controllers;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Views;
using Specialist.Web.Pages;
using Specialist.Web.Root.PlannedTests.ViewModels;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Util;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Helpers;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Services.Education.Interface;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Controllers.Tests {
	public class TestRunController: ViewController {

		[Dependency]
		public TestService TestService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public ICourseVMService CourseVMService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public IStudentService StudentService { get; set; }


		[Dependency]
		public IRepository2<User> UserService { get; set; }

		[Dependency]
		public CoursePrerequisiteService CoursePrerequisiteService { get; set; }


		[Dependency]
		public ISiteObjectRelationService SiteObjectRelationService { get; set; }

		[Dependency]
		public StudentInGroupService StudentInGroupService { get; set; }

		[Dependency]
		public IRepository2<TestQuestion> TestQuestionService { get; set; }

		[Dependency]
		public IRepository2<TestModuleSet> TestModuleSetService { get; set; }

		[Dependency]
		public IRepository2<UserTestAnswer> UserTestAnswerService { get; set; }


		[Dependency]
		public TestModuleService TestModuleService { get; set; }

		[Dependency]
		public UserTestService UserTestService { get; set; }

		ActionResult GetDetailsView(int id, string courseTC, int? moduleSetId) {
			var test = TestService.GetByPK(id);

			if (courseTC != null && CoursePrerequisiteService.GetTestIds(
					courseTC).All(x => x != id)) {
				return NotFound();
			}

			if(!StudentInGroupService.CalcTestIsActive(test)) {
				return NotFound();
			}
			return BaseView(Views.TestRun.Details, new TestRunDetailsVM {
				Test = test,
				CourseName =  DictionaryUtils.GetValueNotDefaultKey(
				CourseService.GetAllActiveCourseNames(),courseTC),
				ModuleSetId = moduleSetId,
				CourseTC = courseTC,
			});
		}

		[HandleNotFound]
		[Authorize]
		public ActionResult Details(int id) {
			return GetDetailsView(id, null, null);
		}

		public ActionResult Prerequisite(int id, string courseTC) {
			return GetDetailsView(id, courseTC, null);
		}

		public ActionResult CoursePlanned(int id,int moduleSetId) {
			return GetDetailsView(id, null,moduleSetId);
		}

		[AjaxOnly]
		public ActionResult Start(int id, string courseTC,int? moduleSetId) {
			var test = TestService.GetFullTest(id);
			var groupTest = StudentInGroupService.GetUserGroupTest(id);
			if(groupTest != null && groupTest.AttemptCount.HasValue) {
				var count = UserTestService.GetUserTests(groupTest)
					.Count(x => x.UserId == GetUserId(false));
				if(count >= groupTest.AttemptCount.Value)
					return BaseView(new PagePart("Вы использовали все попытки для сдачи теста")) ;
			}
			var userTests = GetCurrentTests();
			var userTest = userTests.FirstOrDefault(x => x.TestId == id 
				&& x.UserId == GetUserId(courseTC != null));
			if(userTest == null) {
				var rule = test.TestPassRule;
				if(groupTest != null)
					rule = groupTest.TestPassRule;
				if(moduleSetId.HasValue) {
					rule = TestModuleSetService.GetValues(moduleSetId.Value,
						x => x.TestPassRule);
				}
				var showAnswers = false;
//				if (User.GetOrDefault(x => x.Student_ID) > 0) {
//					var paidCourses = StudentService.GetPaidCourseTCs(User.Student_ID.Value);
//					showAnswers = test.CourseTCSplitList.Intersect(paidCourses).Any();
//				}
				userTest = SaveUserTest(id, test, rule, courseTC, moduleSetId, showAnswers);
			}

			var model = new TestRunVM {Test = test, UserTest = userTest};
			if(userTest.IsPrerequisite) {
				var otherPretests = CoursePrerequisiteService.GetTestIds(
					courseTC).Skip(1).ToList();
				if(otherPretests.Any()) {
					model.OtherPreTestQuestions = GetQuestions(otherPretests).ToList();
				}
			}

			if (test.TestIds.Any()) {
				model.OtherPreTestQuestions = GetQuestions(test.TestIds).ToList();
			}

			return BaseView(new TestRunView(), model);
		}

		private IQueryable<TestQuestion> GetQuestions(List<int> testIds) {
			TestQuestionService.LoadWith(x => x.TestAnswers);
			return TestQuestionService.GetAll(
				x => testIds.Contains(x.TestId));
		}


		[HandleNotFound]
		public ActionResult Result(int userTestId) {
			UserTestService
				.LoadWith(c => c.Load(x => x.Test, x => x.TestPassRule));
			var userTest = UserTestService.GetByPK(userTestId);
			if(userTest == null)
				return null;
			var isEnglishTest = TestRecomendations.IsEnglishTest(userTest.TestId);
			var courseTCs = RecomendCourseTCs(userTest);

			var courseTC = userTest.Test.CourseTCSplitList.FirstOrDefault();
			var isTrack = !courseTC.IsEmpty() && CourseService.IsTrack(courseTC);


			var courses = CourseService.GetCourseLinkList(courseTCs).ToList();
			var modules = TestModuleService.GetForTest(userTest.TestId).ToList();
			var courseName = DictionaryUtils.GetValueNotDefaultKey(
				CourseService.GetAllActiveCourseNames(),userTest.Course_TC);
			var model = new TestResultVM {UserTest = userTest, 
				RecCourses = courses,
				IsOwned = User != null && User.UserID == userTest.UserId,
				IsPrivatePerson = User != null && !User.IsCompany,
				Modules = modules,
				IsTrack = isTrack,
				Stats = EntityUtils.GetStats(userTest),
				IsEnglish = isEnglishTest,
				CourseName = courseName
			};
			return BaseView(new PagePart(PartialViewNames.Result, model ));
		}

		private List<string> RecomendCourseTCs(UserTest userTest) {
			var courseTCs = new List<string>();
			var isEnglishTest = TestRecomendations.IsEnglishTest(userTest.TestId);
			if (userTest.IsPrerequisite && !isEnglishTest) {
				if (userTest.IsPass) {
					courseTCs.Add(userTest.Course_TC);
				}
				else {
					courseTCs.AddRange(
						EntityUtils.GetCoursePreCourses(CoursePrerequisiteService, userTest.Course_TC).Select(x => x.RequiredCourse_TC));
				}
			}
			else {
				var recomendations = TestRecomendations.Tests.GetValueOrDefault(userTest.TestId);
				if (recomendations == null) {
					courseTCs = SiteObjectRelationService.GetRelation(typeof (Test),
						_.List<object>(userTest.TestId), typeof (Course)).Select(x => x.RelationObject_ID)
						.Cast<string>().ToList();
					if (courseTCs.Any() && userTest.IsPass) {
						var parentCourseTCs = CourseService.GetAll(x => courseTCs.Contains(x.Course_TC))
							.Select(x => x.ParentCourse_TC).ToList();
						courseTCs = CourseService.GetNextCourseTCs(parentCourseTCs);
					}
				}
				else {
					courseTCs =
						recomendations.First(x => x.Key <= userTest.RightCount.GetValueOrDefault()).Value;
				}
				
			}
			return courseTCs;
		}

		[HttpPost]
		[AjaxOnly]
		public ActionResult ResultPost(int userTestId, TestResultData model) {
			GetCurrentTests().RemoveAll(x => x.Id == userTestId);

			UserTestService.EnableTracking();
			UserTestService.LoadWith(x => x.TestPassRule, x => x.Test);
			var userTest = UserTestService.GetByPK(userTestId);
			
			UserTestResultPermission(userTest);

			var testName = TestService.GetValues(userTest.TestId, x => new{ x.Name, x.CompanyId});

			var allTestIds = GetAllTestIds(userTest);


			var allQuestionIds = model.Data.Select(x => x.QuestionId).ToList();
			var questions = GetQuestions(allTestIds).Where(x => 
				allQuestionIds.Contains(x.Id)).ToList();

			var answers = ProcessAnswers(userTest, model, questions);

			SetTestStatus(userTest, answers);
			SendTestResult(testName.Name, userTest, testName.CompanyId);

			ModuleStats(userTest, answers, questions, allTestIds);

			CheckIsBest(userTest);

			UserTestService.SubmitChanges();
			return Json("ok");
		}

		private List<int> GetAllTestIds(UserTest userTest) {
			if (userTest.Test.TestIds.Any()) {
				return userTest.Test.TestIds;
			}
			if (userTest.IsPrerequisite) {
				return CoursePrerequisiteService.GetTestIds(
					userTest.Course_TC).ToList();
			}
			return _.List(userTest.TestId);
		}

		private void CheckIsBest(UserTest userTest) {
			if (!userTest.IsPrerequisite) {
				var oldBest = UserTestService.FirstOrDefault(x =>
					x.TestId == userTest.TestId 
					&& object.Equals(x.TestModuleSetId, userTest.TestModuleSetId)
					&& x.UserId == userTest.UserId && x.IsBest);
				if(oldBest == null) {
					userTest.IsBest = true;
				} else if(oldBest.RightCount <= userTest.RightCount) {
						oldBest.IsBest = false;	
						userTest.IsBest = true;
				}
			}
		}

		private void SendTestResult(string testName, UserTest userTest, int? companyId) {
			var courseName =  DictionaryUtils.GetValueNotDefaultKey(
				CourseService.GetAllActiveCourseNames(),userTest.Course_TC);
			var userTestLink = Url.UserTestLink(userTest, courseName ?? testName).AbsoluteHref();
			if (companyId.HasValue) {
				var email = UserService.GetAll(x => x.CompanyID == companyId).Select(x => x.Email).First();
				MailService.Send(Services.Common.MailService.info, new MailAddress(email), 
					userTestLink.ToString(), "Результат тестирования " + User.FullName);

			}
			if (!userTest.IsPass || !userTest.NormalTest) return;


			var courseTC = RecomendCourseTCs(userTest).ToList();
			if (courseTC.Any()) {
				var courseLink = CourseService.GetCourseLinkList(courseTC);
				if (courseLink.Any()) {
					MailService.TestResult(AuthService.CurrentUser,
						userTestLink, 
						courseLink.Select(cl => 
						Html.CourseLinkAnchor(cl.UrlName, cl.GetName()).AbsoluteHref()).ToList(), userTest);
				}
			}
			
		}

		private static void ModuleStats(UserTest userTest, Dictionary<int, bool> answers, 
			List<TestQuestion> questions, List<int> allTestIds) {
			if(allTestIds.Count > 1)
				return;
			var modules = answers.Select(x =>
				new {
					questions.First(q => q.Id == x.Key).ModuleId,
					x.Value
				})
				.GroupBy(x => x.ModuleId.GetValueOrDefault())
				.Where(x => x.Key != 0)
				.ToDictionary(x => x.Key, x =>
					new UserTestStats.RightWrong {R = x.Count(y => y.Value), W = x.Count(y => !y.Value)});
			var stats = new UserTestStats();

			DictionaryUtils.Add(stats, modules);
			EntityUtils.SetStats(userTest, stats);
		}

		private void SetTestStatus(UserTest userTest, Dictionary<int,bool> answers) {
			userTest.RightCount = (short) answers.Count(x => x.Value);
			userTest.WrongCount = (short) answers.Count(x => !x.Value);
			var points = userTest.RightCount;
			var testPassRule = userTest.TestPassRule;
			if (points >= testPassRule.PassMark)
				userTest.Status = UserTestStatus.Pass;
			if (testPassRule.AverageMark.HasValue && points >= testPassRule.AverageMark)
				userTest.Status = UserTestStatus.Average;
			if (testPassRule.ExpertMark.HasValue && points >= testPassRule.ExpertMark)
				userTest.Status = UserTestStatus.Expert;
			SetAndCheckTime(testPassRule, userTest);
		}

		private Dictionary<int,bool> ProcessAnswers(UserTest userTest, TestResultData model, 
			List<TestQuestion> questions) {
			var userTestId = userTest.Id;
			var answers = new Dictionary<int,bool>();
			var answerList = new List<UserTestAnswer>();
			if(model.Data == null) {
				Logger.Exception(new Exception("model.Data == null"), User);
			}
			foreach (var questionAnswer in model.Data) {
				var question = questions.First(x => x.Id == questionAnswer.QuestionId);
				var isRight = IsRight(question, questionAnswer);
				answers.Add(questionAnswer.QuestionId,isRight);
				var answer = new UserTestAnswer {
					QuestionId = questionAnswer.QuestionId,
					IsRight = isRight,
					UserTestId = userTestId,
					Answer = questionAnswer.GetText(),
				};
				answerList.Add(answer);
			}

			if(userTest.IsCoursePlanned || userTest.ShowAnswers) {
				UserTestAnswerService.EnableTracking();
				foreach (var userTestAnswer in answerList.Where(x => !x.Answer.IsEmpty())) {
					UserTestAnswerService.Insert(userTestAnswer);
				}
				UserTestAnswerService.SubmitChanges();
			}

			return answers;
		}

		private int GetUserId(bool isPrerequisite) {
			if(User == null && isPrerequisite)
				return 977115;
			return User.UserID;
		}

		private void UserTestResultPermission(UserTest userTest) {
			if (GetUserId(userTest.IsPrerequisite) != userTest.UserId) {
				throw new PermissionException(GetUserId(userTest.IsPrerequisite) + " != " + userTest.UserId);
			}
		}

		private void SetAndCheckTime(TestPassRule testPassRule, UserTest userTest) {
			var time = GetRestSeconds(userTest);
			var ruleTimeInSeconds = testPassRule.Time*60;
			var resultTime = ruleTimeInSeconds - time;
			if (resultTime > 30000)
				resultTime = 30000;
			if (resultTime > ruleTimeInSeconds + 5*60) {
				userTest.Status = UserTestStatus.Fail;
			}
			userTest.Time = (short) resultTime;
		}

		private UserTest GetUserTest(UserTestService userTestService, int userTestId) {
			var userTest = GetCurrentTests().FirstOrDefault(x => x.Id == userTestId);
			if(userTest == null) {
				userTest = userTestService.GetByPK(userTestId);
//				if (userTest.UserId != GetUserId(userTest.IsPrerequisite))
//					throw new Exception("userTest.UserId != User.UserID");
				GetCurrentTests().Add(userTest);
			//	Logger.Exception(new Exception("Session without userTest"), User);
			}
			return userTest;
		}

		private bool IsRight(TestQuestion question, TestResultData.QuestionAnswer answer) {
			switch (question.Type) {
				case TestQuestionType.OneAnswer:
					var rightAnswerId = question.TestAnswers.FirstOrDefault(x => x.IsRight.GetValueOrDefault())
						.GetOrDefault(x => x.Id);
					if(rightAnswerId == 0) {
						LogQuestionError(question, "rightAnswers is null");
					}
					return answer.OneAnswer == rightAnswerId;
				case TestQuestionType.ManyAnswers:
					var rightAnswerIds = question.TestAnswers.Where(x => x.IsRight.GetValueOrDefault())
						.Select(x => x.Id).ToList();
					if(!rightAnswerIds.Any())
						LogQuestionError(question, "rightAnswers is null");
					return answer.ManyAnswers.SequenceEqual(rightAnswerIds);
				case TestQuestionType.Comparison:
					return answer.Comparison.Left.Zip(answer.Comparison.Right,
						(l, r) => question.TestAnswers.First(x => x.Id == l).ComparableId == r).All(x => x);
				case TestQuestionType.Sort:
					var sortedAnswerIds = question.TestAnswers.OrderBy(x => x.Sort)
						.Select(x => x.Id);
					return answer.Sort.SequenceEqual(sortedAnswerIds);
				default:
					LogQuestionError(question, "type out of range");
					return false;
			}

			
		}

		private void LogQuestionError(TestQuestion question, string message) {
			Logger.Exception(new Exception(string.Format("question {0} {1}", question.Id, message)), User);
		}

		private List<UserTest> GetCurrentTests() {
			Session[CurrentTestsSessionKey] = Session[CurrentTestsSessionKey] ?? new List<UserTest>();
			return (Session[CurrentTestsSessionKey] as List<UserTest>) ?? new List<UserTest>();
		}

		public ActionResult GetTestTime(int userTestId) {
				UserTestService.LoadWith(x => x.TestPassRule);
				var userTest = GetUserTest(UserTestService, userTestId);
			var restSeconds = GetRestSeconds(userTest);
			return JsonGet(restSeconds);
		}

		private short GetRestSeconds(UserTest userTest) {
			var now = DateTime.Now;
			short restSeconds = 0;
			if(userTest != null)
				restSeconds = (short) 
					((userTest.TestPassRule.Time*60) - (int) (now - userTest.RunDate).TotalSeconds);
			if(restSeconds < 0)
				restSeconds = 0;
			return restSeconds;
		}

		private const string CurrentTestsSessionKey = "CurrentTestsSessionKey";

		private UserTest SaveUserTest(int id, Test test, TestPassRule testPassRule,
			string courseTC, int? moduleSetId, bool showAnswers) {
			var userTest = 
				new UserTest {RunDate = DateTime.Now, 
					Status = UserTestStatus.Fail, 
					TestId = id, 
					TestPassRuleId = testPassRule.Id,
					Course_TC = courseTC,
					TestModuleSetId = moduleSetId,
					ShowAnswers = showAnswers
				};
			userTest.UserId = GetUserId(userTest.IsPrerequisite);
			UserTestService.EnableTracking();
			UserTestService.InsertAndSubmit(userTest);
			userTest.TestPassRule = testPassRule;

			GetCurrentTests().Add(userTest);
			return userTest;
		}

		[Authorize]
		[HandleNotFound]
	    public ActionResult UserTestAnswers(int userTestId) {
			UserTestService.LoadWith(x => x.Test);
			var userTest = UserTestService.GetByPK(userTestId);
			if (!userTest.ShowAnswers 
				|| userTest.UserId != User.UserID) {
				return null;
			}
			var qIds = UserTestAnswerService.GetAll(x => x.UserTestId == userTestId && !x.IsRight)
				.Select(x => x.QuestionId).ToList();
			TestQuestionService.LoadWith(x => x.TestAnswers);
			var model = new UserTestAnswerVM {
				Questions = TestQuestionService.GetAll(x => qIds.Contains(x.Id)).ToList(),
				UserTest = userTest
			};
		    return BaseViewWithModel(new UserTestAnswersView(), model);
	    }
	}
}