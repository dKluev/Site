using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
using FluentValidation.Validators;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Extension;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Tests;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Core;
using Specialist.Web.Common.Extension;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Views;
using Specialist.Web.Pages;
using Specialist.Web.Root.PlannedTests.ViewModels;
using Specialist.Web.Root.Tests.Validators;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Util;
using Group = Specialist.Entities.Context.Group;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Helpers;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Controllers.Tests {
	[Authorize]
	public class GroupTestController : ViewController {
		[Dependency]
		public IUserService UserService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }
		
		[Dependency]
		public IRepository2<Group> GroupService { get; set; }

		[Dependency]
		public IRepository2<GroupInfo> GroupInfoService { get; set; }

		[Dependency]
		public UserTestResultService UserTestResultService { get; set; }

		[Dependency]
		public IRepository2<Test> TestService { get; set; }

		[Dependency]
		public IRepository2<TestPassRule> TestPassRuleService { get; set; }


		[Dependency]
		public IRepository2<GroupTest> GroupTestService { get; set; }
		[Dependency]
		public IRepository2<Student> StudentService { get; set; }

		[Dependency]
		public StudentInGroupService StudentInGroupService { get; set; }

		[Dependency]
		public UserTestService UserTestService { get; set; }

			[Dependency]
		public TestModuleService TestModuleService { get; set; }


        [Dependency]
        public IRepository2<TestModuleSet> TestModuleSetService { get; set; }
        [Dependency]
        public IRepository2<Group> GroupRepository { get; set; }
        [Dependency]
        public IRepository2<User> UserRepository { get; set; }
        [Dependency]
        public IRepository2<TestQuestion> TestQuestionService { get; set; }


		[Auth(RoleList = Role.TestAdmin)]
		public ActionResult Prepare() {
			return BaseViewWithModel(new GroupPrepareView(),new GroupPrepareVM());
		}

		[Auth(RoleList = Role.TestAdmin)]
		[HttpPost]
		public ActionResult Prepare(GroupPrepareVM model) {
			GroupInfoService.EnableTracking();
			var groupId = model.GroupID;
			var groupInfo = GroupInfoService.FirstOrDefault(x => x.Group_ID == groupId);
			User user = null;
			if(groupInfo == null) {
				user = UserService.GetByEmail(model.Email);
				if (user == null)
					ModelState.AddModelError("", "Пользователь не существует");
				else if (!user.IsCompany)
					ModelState.AddModelError("", "Пользователь не компания");
				
			}
			var group = GroupService.GetByPK(groupId);
			if (group == null)
				ModelState.AddModelError("", "Группа не существует");
			else if (group.Course_TC != CourseTC.Test)
				ModelState.AddModelError("", "Не группа тестирования");
			if (!ModelState.IsValid)
				return ErrorJson();
			RegisterStudentUsers(groupId);
			if (!ModelState.IsValid)
				return ErrorJson();
			if(groupInfo == null) {
				groupInfo = new GroupInfo {CompanyId = user.CompanyID.Value, Group_ID = groupId};
				GroupInfoService.InsertAndSubmit(groupInfo);
			}

			return UrlJson(Url.Action<GroupTestController>(x => x.GroupInfo(groupInfo.Id)));
		}

		private void RegisterStudentUsers(decimal groupId) {
			StudentService.LoadWith(c => c.StudentEmails);
			var students = StudentService.GetAll(x => x.StudentInGroups.Any(y => y.Group_ID == groupId)).ToList();
			var emails = students.SelectMany(x => x.StudentEmails.Select(z => z.Email.Trim())).GroupBy(x => x)
				.Where(x => x.Count() > 1).Select(x => x.Key);
			if (emails.Any()) {
				ModelState.AddModelError("", "Дублирующиеся емейлы " + emails.JoinWith(", "));
				return;
			}
			foreach (var student in students) {
				CreateUserByStudent(student);
			}
			if (ModelState.IsValid) {
				UserService.SubmitChanges();
			}
		}

		public void ValidateIsTrue(bool b, string error) {
			if (!b)
				ModelState.AddModelError("", error);
		}

		private void CreateUserByStudent(Student student) {
			new TestValidators(ModelState).Validate(student);
			var emails = student.StudentEmails.Select(x => x.Email.Trim()).ToList();
			var user = UserService.FirstOrDefault(
				x => emails.Contains(x.Email) || x.Student_ID == student.Student_ID);
			if (user != null) {
				user.Student_ID = student.Student_ID;
			}
			else {
				user = new User {
					FirstName = student.FirstName,
					LastName = student.LastName,
					SecondName = student.MiddleName,
					Email = student.StudentEmails.OrderByDescending(x => x.StudentEmail_ID).First().Email.Trim(),
					Password = Membership.GeneratePassword(6, 0),
					Sex = student.Sex == Sex.M,
					Student_ID = student.Student_ID,
					IsActive = true
				};
				UserService.Insert(user);
			}
		}

		[AjaxOnly]
		public ActionResult GetGroupTests(int groupInfoId, AjaxGridRequest model) {
			var list = GroupTestService
				.GetAll(x => x.GroupInfoId == groupInfoId)
				.Select(x => new {x.Id, TestId = x.Test.Name}).ToPagedList(model.Page - 1, model.Rows);

			return JsonGet(new GridData(list.PageCount,
				model.Page,
				list.ItemCount,
				list));
		}

		[Auth(RoleList = Role.CorpManager | Role.TestAdmin)]
		public ActionResult List() {
			GroupInfoService.LoadWith(x => x.Group);
			var groupInfos = GroupInfoService.GetAll(x => x.CompanyId == User.CompanyID && x.IsComplete);
			if(User.InRole(Role.TestAdmin))
				groupInfos = GroupInfoService.GetAll();
			var groups = groupInfos.
				OrderByDescending(x => x.Group.DateBeg).ToList();
			return BaseViewWithModel(new GroupInfosView(), new GroupInfosVM{GroupInfos = groups});
		}


		[Auth(RoleList = Role.CorpManager)]
		public ActionResult Result(int groupInfoId) {
			List<int> userIds;
			var groupInfo = GetGroupInfo(groupInfoId, out userIds);
			UserTestService.LoadWith(x => x.User);
			var groupUserTestsList = groupInfo.GroupTests.Select(x => Create(x, userIds, UserTestService)).ToList();

			return BaseViewWithModel(new GroupTestResultView(), new GroupTestResultVM{GroupInfo = groupInfo,
				GroupUserTestsList = groupUserTestsList });
		}

		private GroupInfo GetGroupInfo(int groupInfoId, out List<int> userIds) {
			GroupInfoService.LoadWith(c => c.Load(x => x.GroupTests).And<GroupTest>(t => t.Test));
			var groupInfo = GroupInfoService.GetByPK(groupInfoId);
			userIds = UserService.GetAll(x => x.Student.StudentInGroups.Any(y => y.Group_ID == groupInfo.Group_ID))
				.Select(x => x.UserID).ToList();
			return groupInfo;
		}

		[Auth(RoleList = Role.CorpManager)]
		public ActionResult DownloadResult(int groupInfoId) {
			List<int> userIds;
			var groupInfo = GetGroupInfo(groupInfoId, out userIds);
			UserTestService.LoadWith(x => x.User, x => x.Test);
			var userTests = groupInfo.GroupTests.Select(gt => UserTestService.GetUserTests(gt)
				.Where(x => userIds.Contains(x.UserId)).ToList()).ToList();
			var data = UserTestResultService.GetResultData(userTests);
			return File(Encoding.GetEncoding(1251).GetBytes(CsvUtil.Render(data)), 
				"text/csv", "GroupResults-{0}.csv".FormatWith(groupInfo.Group_ID));
			
		}

		public GroupUserTests Create(GroupTest groupTest, List<int> userIds, UserTestService userTestService) {
			
			var userTests = userTestService.GetUserTests(groupTest).Where(x => userIds.Contains(x.UserId)).ToList();
			var best =
				userTests.GroupBy(x => x.UserId).Select(x => x.OrderByDescending(y => y.Status).FirstOrDefault())
				.ToList();
			return new GroupUserTests{GroupTest = groupTest, UserTests = best};
		}

		private decimal GetGroupIdForInfo(int groupInfoId) {
			return GroupInfoService.GetAll(x => x.Id == groupInfoId).Select(x => x.Group_ID).First();
		}

	/*	public ActionResult Assign() {
			var studentId = User.Student_ID.GetValueOrDefault();
			var groupTests = StudentInGroupService.GetGroupTests(studentId).ToList();
			return BaseView(new AssignTestsView(), new AssignTestsVM {GroupTests = groupTests});
		}
*/


		[Auth(RoleList = Role.TestAdmin)]
		public ActionResult GroupInfo(int groupInfoId) {
			var groupInfo = GroupInfoService.GetByPK(groupInfoId);
			var model = new GroupInfoVM() {
				DenyAdd = !User.InRole(Role.TestAdmin),
				GetListUrl = Url.Action<GroupTestController>(c => c.GetGroupTests(groupInfoId, null)),
				EditUrl = Url.Action<GroupTestController>(c => c.EditGroupTest(groupInfoId, null)),
				Caption = "Тесты группы " + groupInfo.Group_ID
			};
			if(User.InRole(Role.TestAdmin))
				model.DeleteUrl = Url.Action<GroupTestController>(c => c.DeleteGroupTest(null));
			AddColumns<GroupTest>(model, x => x.TestId);
			return BaseView(new PagePart(PartialViewNames.AjaxList, model), 
				new PagePart(new GroupInfoView{}.Init(groupInfo, Url).Get().ToString()));
		}
		[HttpPost]
		public ActionResult GroupInfoComplete(int groupInfoId) {
			GroupInfoService.EnableTracking();
			var groupInfo = GroupInfoService.GetByPK(groupInfoId);
			groupInfo.IsComplete = true;
			GroupInfoService.SubmitChanges();
			var user = UserService.FirstOrDefault(x => x.CompanyID == groupInfo.CompanyId);
			MailService.NewGroupTest(user);
			return OkJson();
		}
		[HttpPost]
		public ActionResult RegisterUsers(decimal groupId) {
			RegisterStudentUsers(groupId);
			if (!ModelState.IsValid)
				return ErrorJson();
			return OkJson();
		}

		[HttpPost]
		public ActionResult SendGroupTestInfo(int groupInfoId, bool forManager) {
			GroupInfoService.LoadWith(c => c.Load(x => x.GroupTests).And<GroupTest>(x => x.Test));
			var groupInfo = GroupInfoService.GetByPK(groupInfoId);
			var managerEmail = forManager ? UserService.FirstOrDefault(x => 
				x.CompanyID == groupInfo.CompanyId).Email : null;
			var studentIds = 
				StudentInGroupService.GetAll(x => x.Group_ID == groupInfo.Group_ID).Select(x => x.Student_ID).ToList();
			var users = UserService.GetAll(x => studentIds.Contains(x.Student_ID.Value)).ToList();
			var tests = groupInfo.GroupTests.Select(x => Url.TestLink(x.Test).AbsoluteHref()).ToList();
			MailService.GroupTestInfo(users,tests, managerEmail);
			return OkJson();
		}

		[HttpPost]
		[Auth(RoleList = Role.TestAdmin)]
		public ActionResult DeleteGroupTest(int? id) {
			GroupTestService.EnableTracking();
			var groupTest = GroupTestService.GetByPK(id.Value);
			GroupTestService.DeleteAndSubmit(groupTest);
			return OkJson();
		} 

		public ActionResult EditGroupTest(int groupInfoId, int? id) {
			var groupTest = new GroupTest();
			groupTest.TestPassRule = new TestPassRule();
				var modules = new List<TestModule>();
			var modulePercents = new Dictionary<int, int>();
			if (id.HasValue) {
				GroupTestService.LoadWith(x => x.Test, x => x.TestPassRule);
				groupTest = GroupTestService.GetByPK(id.Value);
				modulePercents = EntityUtils.GetModulePercents(groupTest.TestPassRule);
				modules = TestModuleService.GetForTest(groupTest.TestId).ToList();
			}
			else {
				var groupId = GetGroupIdForInfo(groupInfoId);
				var group = GroupService.GetByPK(groupId);
				groupTest.GroupInfoId = groupInfoId;
				groupTest.DateBegin = group.DateBeg ?? DateTime.Now;
				groupTest.DateEnd = group.DateEnd ?? DateTime.Now;

			}
			var model = new GroupTestEditVM {
				GroupTest = groupTest,
				Modules = modules,
				ModulePercents = modulePercents,
			};
			return BaseView(new PagePart(new GroupTestEditView().Init(model,Url)));
		}

		[HttpPost]
		public ActionResult EditGroupInfo(int id, string notes) {
			GroupInfoService.EnableTracking();
			var groupInfo = GroupInfoService.GetByPK(id);
			groupInfo.Notes = StringUtils.SafeSubstring(notes, 500);
			GroupInfoService.SubmitChanges();
			return OkJson();
		}

		[HttpPost]
		public ActionResult EditGroupTest(GroupTestEditVM model) {
			if (!LinqToSqlValidator.Validate(ModelState, model.GroupTest))
				return ErrorJson();
			GroupTestService.EnableTracking();
			var newGroupTest = model.GroupTest;
			if (newGroupTest.Id == 0) {
				var rule =TestPassRuleService.FirstOrDefault(x => x.TestId == newGroupTest.TestId);
				if(newGroupTest.TestPassRule.QuestionCount == 0) {
					newGroupTest.TestPassRule.UpdateBy(rule);
				}
				newGroupTest.TestPassRule.ModulePercents = rule.ModulePercents;
				Validate(newGroupTest);
				if(!ModelState.IsValid) return ErrorJson();
				GroupTestService.InsertAndSubmit(newGroupTest);
				return UrlJson(Url.Action<GroupTestController>(c => c.EditGroupTest(newGroupTest.GroupInfoId,
					newGroupTest.Id)));
			}
			GroupTestService.LoadWith(x => x.TestPassRule);
			var groupTest = GroupTestService.GetByPK(newGroupTest.Id);
			var isNewTest = groupTest.TestId != newGroupTest.TestId;
			if(isNewTest) {
				var rule =TestPassRuleService.FirstOrDefault(x => x.TestId == newGroupTest.TestId);
				model.ModulePercents = EntityUtils.GetModulePercents(rule);
			}
			groupTest.Update(newGroupTest, x => x.TestId,
				x => x.DateBegin, x => x.DateEnd, x => x.AttemptCount);
			groupTest.TestPassRule.UpdateBy(newGroupTest.TestPassRule);
			EntityUtils.SetModulePercents(groupTest.TestPassRule, model.ModulePercents);
			Validate(groupTest);
			if(!ModelState.IsValid) return ErrorJson();
			GroupTestService.SubmitChanges();
			if(isNewTest)
				return UrlJson(Url.GroupTest().Urls.EditGroupTest(groupTest.GroupInfoId, groupTest.Id));
			return UrlJson(Url.Action<GroupTestController>(c => c.EditGroupTest(groupTest.GroupInfoId,
					groupTest.Id)));
		}

		private void Validate(GroupTest groupTest) {
			var rule = groupTest.TestPassRule;
			new TestValidators(ModelState).Validate(rule);
		}

		[AjaxOnly]
		public ActionResult GetTestsAuto(int groupInfoId, string term) {
			term = term ?? string.Empty;
			var list = TestService.GetAll(x => x.CompanyId == null && x.Name.Contains(term) 
				&& x.Status == TestStatus.Active).Select(x => new {id = x.Id, label = x.Name}).Take(20);
			return JsonGet(list);
		}

		[Auth(RoleList = Role.Trainer)]
		public ActionResult PlanTestUserStats(decimal groupId) {
			var testId = GetTestForGroup(groupId);
			var studentIds = GetStudentIds(groupId);
			var users = UserRepository.GetAll(u => studentIds.Contains(u.Student_ID.Value)).ToList();
			var userNames = users.ToDictionary(x => x.UserID, x => x.FullName);
			var userIds = users.Select(x => x.UserID).ToList();
			var userTests = GetUserTests(testId.Value, userIds).ToList().GroupBy(x => x.TestModuleSetId.Value).ToList();
			var setIds = userTests.Select(x => x.Key);
			var sets = TestModuleSetService.GetByPK(setIds.Cast<object>())
				.ToDictionary(x => x.Id, x=> x);
			var setStats = userTests.Select(x =>
				new PlanTestUserStatsVM.ModuleSetStat(sets.GetValueOrDefault(x.Key),
					x.ToList())).ToList();
			var model = new PlanTestUserStatsVM {
				GroupId = groupId,
				ModuleSetStats = setStats,
				UserNames = userNames
			};
			return BaseViewWithModel(new PlanTestUserStatsView(), model);

		}

	    private IQueryable<UserTest> GetUserTests(int testId, List<int> userIds) {
		    return UserTestService.GetAll(x => x.IsBest
			    && x.TestModuleSetId.HasValue
			    && userIds.Contains(x.UserId)
			    && x.TestId == testId);
	    }


		[Auth(RoleList = Role.Trainer)]
	    public ActionResult PlanTestQuestionStats(decimal groupId) {
			var testId = GetTestForGroup(groupId);
			var studentIds = GetStudentIds(groupId);
			var userIds = UserRepository.GetAll(u => studentIds.Contains(u.Student_ID.Value))
				.Select(x => x.UserID).ToList();
			var questionStats = GetUserTests(testId.Value, userIds)
				.SelectMany(x => x.UserTestAnswers).GroupBy(x => x.QuestionId)
				.Select(x => new {x.Key,
					RightCount = x.Count(y => y.IsRight),
					WrongCount = x.Count(y => !y.IsRight)
				}).ToList().Select(x => 
					Tuple.Create(x.Key, 
					x.RightCount*100/(x.RightCount + x.WrongCount))).ToList();
		    var qIds = questionStats.Select(x => x.Item1).ToList();
		    var qNames = TestQuestionService.GetByPK(qIds.Cast<object>())
			    .ToDictionary(x => x.Id, x => x.Description);

			var model = new PlanTestQuestionStatsVM() {
				GroupId = groupId,
				QuestionStats = questionStats,
				QuestionNames = qNames
			};
			return BaseViewWithModel(new PlanTestQuestionStatsView(), model);

		}

	    private int? GetTestForGroup(decimal groupId) {
		    return 490;
		    //return GroupRepository.GetValues(groupId,x => x.Course.TestId);
	    }

	    private IQueryable<UserTest> GetUserTests(int testId) {
		    return UserTestService.GetAll(x => x.IsBest
			    && x.TestModuleSetId.HasValue
			    && x.TestId == testId);
	    }

	    private List<decimal> GetStudentIds(decimal groupId) {
		    var studentIds = StudentInGroupService
			    .GetAll(x => x.Group_ID == groupId).Select(x => x.Student_ID).ToList();
		    return studentIds;
	    }
	}
}