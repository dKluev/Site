using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Interface;
using Specialist.Services.Tests;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Core;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Common.Exceptions;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Views;
using Specialist.Web.Pages;
using Specialist.Web.Root.Tests.Validators;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Util;
using SpecialistTest.Web.Core.Mvc.Extensions;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Controllers.Tests {
	[Auth(RoleList = Role.Trainer | Role.TestAdmin | Role.Admin | Role.TestCorpManager)]
	public class TestEditController : ViewController {
		[Dependency]
		public TestService TestService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public TestModuleService TestModuleService { get; set; }

		[Dependency]
		public IRepository2<TestQuestion> TestQuestionService { get; set; }

		[Dependency]
		public IRepository2<TestModuleSet> TestModuleSetService { get; set; }

		[Dependency]
		public IRepository2<TestAnswer> TestAnswerService { get; set; }

		[Dependency]
		public IEmployeeService EmployeeService { get; set; }

		


		[AjaxOnly]
		public ActionResult GetTests(AjaxGridRequest model) {
			var tests = TestService.GetAll();
			if (!User.IsCompany) {
				tests = tests.Where(x => x.Status == TestStatus.Edit);
			}
			if(User.IsCompany)
				tests = tests.Where(x => x.CompanyId == User.CompanyID);
			else if(User.InRole(Role.Admin)){}
			else if(User.IsEmployee)
				tests = tests.Where(x => x.Author_TC == User.Employee_TC);
			var list = tests.Select(x => new {x.Id, x.Name, x.Status})
				.ToPagedList(model.Page - 1, model.Rows);

			return Json(new GridData(list.PageCount,
				model.Page,
				list.Count,
				list.Select(x => new {x.Id, x.Name, 
					Status = NamedIdCache<TestStatus>.GetName(x.Status) })), JsonRequestBehavior.AllowGet);
		}
		[AjaxOnly]
		public ActionResult GetModuleSets(int testId, AjaxGridRequest model) {
			var list = TestModuleSetService
				.GetAll(x => x.TestId == testId).OrderBy(x => x.Number).Select(x => 
					new {x.Id,x.Number,x.Description}).ToPagedList(model.Page - 1, model.Rows);
			return JsonGet(new GridData(list.PageCount,
				model.Page,
				list.Count,
				list));
		}

		[AjaxOnly]
		public ActionResult GetModules(int testId, AjaxGridRequest model) {
			var list = TestModuleService
				.GetAll(x => x.TestId == testId).Select(x => new {x.Id, x.Name}).ToPagedList(model.Page - 1, model.Rows);

			return JsonGet(new GridData(list.PageCount,
				model.Page,
				list.Count,
				list));
		}

		[AjaxOnly]
		public ActionResult GetModulesAuto(int testId, string term) {
			term = term ?? string.Empty;
			var list = TestModuleService.GetAll(x => x.TestId == testId && x.Name.Contains(term))
				.Select(x => Select2VM.Item(x.Id,x.Name));
			return Json(new Select2VM(list), JsonRequestBehavior.AllowGet);
		}
		[AjaxOnly]
		public ActionResult GetCoursesAuto(string term) {
			term = term ?? string.Empty;
			var list = CourseService.GetAllActiveCourseNames().Where(x => x.Value.ToLower().Contains(term.ToLower())).Take(10)
				.Select(x => Select2VM.Item(x.Key,x.Value));
			return Json(new Select2VM(list), JsonRequestBehavior.AllowGet);
		}
		[AjaxOnly]
		public ActionResult GetEmployeeAuto(string term) {
			term = term ?? string.Empty;
			var list = EmployeeService.AllEmployees().Select(x => x.Value)
				.Where(x => x.FullName.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0 
				&& (x.EmpGroup_TC == EmpGroups.Trainer || x.Employee_TC == Employees.Dinzis)) 
				.Select(x => Select2VM.Item(x.Employee_TC,x.FullName));
			return Json(new Select2VM(list), JsonRequestBehavior.AllowGet);
		}

//		[AjaxOnly]
//		public ActionResult GetQuestions(int testId, AjaxGridRequest model) {
//			var list = TestQuestionService.GetAll(x => x.TestId == testId)
//				.Select(x => new {x.Id, x.Description}).ToPagedList(model.Page - 1, model.Rows);
//
//			return Json(new GridData(list.PageCount,
//				model.Page,
//				list.Count,
//				list), JsonRequestBehavior.AllowGet);
//		}
//
//		[AjaxOnly]
//		public ActionResult GetAnswers(int questionId, AjaxGridRequest model) {
//			var question = TestQuestionService.GetByPK(questionId);
//
//			Expression<Func<TestAnswer, object>> selector = null;
//
//			switch (question.Type) {
//				case TestQuestionType.OneAnswer:
//				case TestQuestionType.ManyAnswers:
//					selector = x => new {
//						x.Id,
//						x.Description,
//						IsRight = x.IsRight.GetValueOrDefault() ? "Да" : "Нет"
//					};
//					break;
//				case TestQuestionType.Comparison:
//					selector = x => new {
//						x.Id,
//						x.Description,
//						ComparableId = x.ComparableAnswer.Description
//					};
//					break;
//				case TestQuestionType.Sort:
//					selector = x => new {
//						x.Id,
//						x.Description,
//						x.Sort
//					};
//					break;
//				default:
//					throw new Exception("TestQuestionType out of range");
//			}
//			var list = TestAnswerService.GetAll(x => x.QuestionId == questionId)
//				.Select(selector).ToPagedList(model.Page - 1, model.Rows);
//			return Json(new GridData(list.PageCount,
//				model.Page,
//				list.Count,
//				list), JsonRequestBehavior.AllowGet);
//		}
//
		[AjaxOnly]
		public ActionResult GetAnswersAuto(int questionId, string term) {
			term = term ?? string.Empty;
			var list = TestAnswerService.GetAll(x => 
				x.QuestionId == questionId 
				&& x.ComparableId == null 
				&& x.Answer == null
				&& x.Description.Contains(term))
				.Select(x => Select2VM.Item(x.Id,x.Description));
			return Json(new Select2VM(list), JsonRequestBehavior.AllowGet);
		}


		public ActionResult List() {
			var ajaxGridVM = new TestListVM() {
				GetListUrl = Url.Action<TestEditController>(c => c.GetTests(null)),
				EditUrl = Url.Action<TestEditController>(c => c.EditTest((int?) null)),
				ViewUrl = Url.Action<TestEditController>(c => c.ViewTest(null)),
				Caption = "Тесты",
				OpenDialogsInPage = true
			};
			AddColumns<Test>(ajaxGridVM, x => x.Name, x => x.Status);
			return BaseView(new PagePart(PartialViewNames.AjaxList, ajaxGridVM));
		}
		public void EditAnswerPermission(int answerId) {
			EditQuestionPermission(TestAnswerService.GetValues(answerId, x=> x.QuestionId));
		}

		public void EditQuestionPermission(int questionId) {
			var testId = TestQuestionService.GetValues(questionId, x => x.TestId);
			EditTestPermission(testId);
		}

		public void EditTestPermission(int testId) {
			var test = TestService.GetValues(testId,x => new {x.Id, x.CompanyId, x.Author_TC, x.Status});
			if(User.InRole(Role.Admin))
				return;
			if (User.InRole(Role.TestCorpManager)) {
				if (test.CompanyId != User.CompanyID) {
					throw new PermissionException();
				}

			}else if (test.Status != TestStatus.Edit) {
				throw new PermissionException();
			}else if(test.Author_TC != User.Employee_TC)
				throw new PermissionException();
		}

		public ActionResult ViewTest(int? id) {
			var test = TestService.GetFullTest(id.Value);
			ViewTestPermission(test);
			var model = new TestReadOnlyVM {Test = test, 
				Modules = TestModuleService.GetForTest(id.Value).ToList(),
				Checker = EmployeeService.AllEmployees().GetValueOrDefault(test.Checker_TC)
			};
			return BaseViewWithModel(new TestReadOnlyView(), model);
		}


		private void ViewTestPermission(Test test) {
			if(User.InRole(Role.TestAdmin))
				return;
			if (User.InRole(Role.TestCorpManager)) {
				if (User.CompanyID != test.CompanyId) {
					throw new PermissionException();
				}
			}else if (test.Author_TC != User.Employee_TC && test.Checker_TC != User.Employee_TC) {
				throw new PermissionException();
			}
		}

		[HttpPost]
		public ActionResult ReturnToEdit(int id) {
			TestService.EnableTracking();
			var test = TestService.GetByPK(id);
			test.Status = TestStatus.Edit;
			MailService.TestAudit(test, Url.TestEdit().ViewTest(id, test.Name).AbsoluteHref(), test.Author_TC, true);
			TestService.SubmitChanges();
			return OkJson();
			
		}

		[HttpPost]
		public ActionResult SendToAudit(int id, string employeeTC) {
			TestService.EnableTracking();
			var test = TestService.GetByPK(id);
			test.Status = TestStatus.Audit;
			if (!employeeTC.IsEmpty()) {
				test.Checker_TC = employeeTC;
			}
			TestService.SubmitChanges();

			MailService.TestAudit(test, Url.TestEdit().ViewTest(id, test.Name).AbsoluteHref(), employeeTC);
			return OkJson();
		}

		[HttpPost]
		public ActionResult SendCompleteTest(int id) {
			var test = TestService.GetByPK(id);
			MailService.TestComplete(test);
			return OkJson();
		}

		public ActionResult EditTest(int? id) {
			var test = new Test();
			AjaxGridVM modulesModel = null;
			AjaxGridVM moduleSetsModel = null;
			AjaxGridVM questionsModel = null;
			var modules = new List<TestModule>();
			var modulePercents = new Dictionary<int, int>();
			if (id.HasValue) {
				TestService.LoadWith(x => x.TestPassRule);
				test = TestService.GetByPK(id);
				EditTestPermission(test.Id);
				modulesModel = new AjaxGridVM {
					GetListUrl = Url.Action<TestEditController>(c => c.GetModules(id.Value, null)),
					EditUrl = Url.Action<TestEditController>(c => c.EditModule(id.Value, null)),
					DeleteUrl = Url.Action<TestEditController>(c => c.DeleteModule(null)),
					Caption = "Модули"
				};
				AddColumns<TestModule>(modulesModel, x => x.Name);
				moduleSetsModel = new AjaxGridVM {
					GetListUrl = Url.Action<TestEditController>(c => c.GetModuleSets(id.Value, null)),
					EditUrl = Url.Action<TestEditController>(c => c.EditModuleSet(id.Value, null)),
					Caption = "План тестирования"
				};
				AddColumns<TestModuleSet>(moduleSetsModel, x => x.Number);
				AddColumns<TestModuleSet>(moduleSetsModel, x => x.Description);
				questionsModel = new AjaxGridVM {
					GetListUrl = Url.Action<TestEditController>(c => c.GetQuestions(id.Value, null)),
					EditUrl = Url.Action<TestEditController>(c => c.EditQuestion(id.Value, null)),
					DeleteUrl = Url.Action<TestEditController>(c => c.DeleteQuestion(null)),
					Caption = "Вопросы"
				};
				AddColumns<TestQuestion>(questionsModel, x => x.Description);
				modulePercents = EntityUtils.GetModulePercents(test.TestPassRule);
				modules = TestModuleService.GetAll(x => x.TestId == id.Value).ToList();


			}
			test.TestPassRule = test.TestPassRule ?? new TestPassRule();

			var testEditVM = new TestEditVM {Test = test,
				CourseName =  CourseService.AllCourseLinks()
				.GetValueOrDefault(test.CourseTCList).GetOrDefault(x => x.WebName),
				Modules = modules, 
				ModulePercents = modulePercents};
			return BaseView(
				new PagePart(new TestEditView().Init(testEditVM, Url)),
				modulesModel == null ? null : new PagePart(PartialViewNames.AjaxList, modulesModel),
				questionsModel == null ? null : new PagePart(PartialViewNames.AjaxList, questionsModel),
				moduleSetsModel == null || test.CompanyId.HasValue ? null : new PagePart(PartialViewNames.AjaxList, moduleSetsModel)
				);
		}

		private void SetUpdateDateAndLastChanger(object entity) {
			var employeeTC = User.Employee_TC
				?? Employees.Specweb;

			EntityUtils.SetUpdateDateAndLastChanger(entity, employeeTC);
		}


		[HttpPost]
		public ActionResult EditTest(TestEditVM model) {
			SetUpdateDateAndLastChanger(model.Test);

			if (!LinqToSqlValidator.Validate(ModelState, model.Test))
				return ErrorJson();
			TestService.EnableTracking();
			if (model.Test.Id == 0) {
				model.Test.Status = TestStatus.Edit;
				model.Test.Author_TC = User.Employee_TC ?? Employees.Specweb;
				model.Test.CompanyId = User.CompanyID;
				EntityUtils.SetModulePercents(model.Test.TestPassRule, model.ModulePercents);
				TestService.InsertAndSubmit(model.Test);
				TestModuleService.EnableTracking();
				TestModuleService.CreateModulesFromCourse(model.Test);
				return UrlJson(Url.Action<TestEditController>(c => c.EditTest(model.Test.Id)));
			}
			TestService.LoadWith(x => x.TestPassRule);
			var test = TestService.GetByPK(model.Test.Id);
			var isNewCourse = test.CourseTCList != model.Test.CourseTCList;
			EditTestPermission(test.Id);
			test.Update(model.Test, 
				x => x.Description, 
				x => x.Name,
				x => x.CourseTCList
				);
			var newTestPassRule = model.Test.TestPassRule;
			test.TestPassRule.UpdateBy(newTestPassRule);
			EntityUtils.SetModulePercents(test.TestPassRule, model.ModulePercents);
			TestService.SubmitChanges();
			if (isNewCourse) {
				TestModuleService.EnableTracking();
				TestModuleService.CreateModulesFromCourse(model.Test);
			}
			return Json("ok");
		}

	

		public ActionResult EditModule(int testId, int? id) {
			var module = new TestModule();
			if (id.HasValue) {
				EditTestPermission(testId);
				module = TestModuleService.GetByPK(id.Value);
			}
			else {
				module.TestId = testId;
			}
			return BaseView(
				new PagePart(new ModuleEditView().Init(module, Url).Get().ToString()));
		}

		[HttpPost]
		public ActionResult EditModule(TestModule model) {
			if (!LinqToSqlValidator.Validate(ModelState, model))
				return ErrorJson();
			EditTestPermission(model.TestId);
			TestModuleService.EnableTracking();
			if (model.Id == 0) {
				TestModuleService.InsertAndSubmit(model);
				return UrlJson(Url.TestEdit().Urls.EditModule(model.TestId, model.Id));
			}
			else {
				var module = TestModuleService.GetByPK(model.Id);
				module.Update(model, x => x.Name);
				TestModuleService.SubmitChanges();
			}
			return Json("ok");
		}

		[HttpPost]
		public ActionResult DeleteModule(int? id) {
			TestModuleService.EnableTracking();
			var module = TestModuleService.GetByPK(id.Value);
			EditTestPermission(module.TestId);
			TestModuleService.DeleteAndSubmit(module);
			return Json("ok");
		}

		public ActionResult EditQuestion(int testId, int? id) {
			EditTestPermission(testId);
			var model = new TestQuestion();
			TestQuestionService.LoadWith(x => x.TestModule);
			AjaxGridVM answersModel = null;
			if (id.HasValue) {
				model = TestQuestionService.GetByPK(id.Value);
				answersModel = new AjaxGridVM {
					GetListUrl = Url.Action<TestEditController>(c => c.GetAnswers(id.Value, null)),
					EditUrl = Url.Action<TestEditController>(c => c.EditAnswer(id.Value, null)),
					DeleteUrl = Url.Action<TestEditController>(c => c.DeleteAnswer(null)),
					Caption = "Ответы"
				};
				AddColumns<TestAnswer>(answersModel, x => x.Description);
			switch (model.Type) {
				case TestQuestionType.OneAnswer:
				case TestQuestionType.ManyAnswers:
					AddColumns<TestAnswer>(answersModel, x => x.IsRight);
					break;
				case TestQuestionType.Comparison:
					AddColumns<TestAnswer>(answersModel, x => x.ComparableId);
					break;
				case TestQuestionType.Sort:
					AddColumns<TestAnswer>(answersModel, x => x.Sort);
					break;
				default:
					throw new Exception("TestQuestionType out of range");
			}
			}
			else {
				model.TestId = testId;
			}
			return BaseView(
				new PagePart(new QuestionEditView().Init(model, Url)),
				answersModel.IfNotNull(x => new PagePart(PartialViewNames.AjaxList, x)));
		}

		short MaxModuleSetNumber(int testId) {
			return TestModuleSetService.GetAll(x => x.TestId == testId)
				.Max(x => (short?) x.Number).GetValueOrDefault();
		}

		public ActionResult EditModuleSet(int testId, int? id) {
			EditTestPermission(testId);
			var model = new ModuleSetVM();
			var set = new TestModuleSet();
			var modulePercents = new Dictionary<int, int>() ;
			set.TestPassRule = new TestPassRule();
			if (id.HasValue) {
				TestModuleSetService.LoadWith(x => x.TestPassRule);
				set = TestModuleSetService.GetByPK(id.Value);
				modulePercents= EntityUtils.GetModulePercents(set.TestPassRule);
			}
			else {
				set.TestId = testId;
				set.Number = (short)(1 + MaxModuleSetNumber(testId));
				
			}
			model.ModulePercents = modulePercents;
			model.ModuleSet = set;
			model.Modules = TestModuleService.GetForTest(testId).ToList();
			return BaseViewWithModel(new ModuleSetEditView(), model);
		}
		[HttpPost]
		public ActionResult EditModuleSet(ModuleSetVM model) {
			var moduleSet = model.ModuleSet;
			EditTestPermission(moduleSet.TestId);
			EntityUtils.SetModulePercents(moduleSet.TestPassRule, model.ModulePercents);
			var validators = new TestValidators(ModelState);
			validators.Validate(moduleSet.TestPassRule);
			validators.Validate(moduleSet);
			if (!ModelState.IsValid)
				return ErrorJson();
			TestModuleSetService.EnableTracking();
			TestModuleSetService.LoadWith(x => x.TestPassRule);
			if (moduleSet.Id == 0) {
				TestModuleSetService.InsertAndSubmit(moduleSet);
				return UrlJson(Url.TestEdit().Urls.EditModuleSet(moduleSet.TestId, moduleSet.Id));
			}
			var oldModel = TestModuleSetService.GetByPK(moduleSet.Id);
			oldModel.Update(moduleSet, x => x.Description, x => x.Number);
			oldModel.TestPassRule.UpdateBy(moduleSet.TestPassRule);
			EntityUtils.SetModulePercents(oldModel.TestPassRule, model.ModulePercents);
			TestModuleSetService.SubmitChanges();
			return OkJson();
		}

		[HttpPost]
		public ActionResult EditQuestion(TestQuestion model) {
			EditTestPermission(model.TestId);
			if (!LinqToSqlValidator.Validate(ModelState, model))
				return ErrorJson();
			TestQuestionService.EnableTracking();
			if (model.Id == 0) {
				TestQuestionService.InsertAndSubmit(model);
				return UrlJson(Url.TestEdit().Urls.EditQuestion(model.TestId, model.Id));
			}
			var oldModel = TestQuestionService.GetByPK(model.Id);
			oldModel.Update(model, x => x.Description, x => x.ModuleId);
			TestQuestionService.SubmitChanges();
			return Json("ok");
		}

		[HttpPost]
		public ActionResult DeleteQuestion(int? id) {
			TestQuestionService.EnableTracking();
			var question = TestQuestionService.GetByPK(id.Value);
			EditTestPermission(question.TestId);
			TestQuestionService.DeleteAndSubmit(question);
			return Json("ok");
		}

		public ActionResult EditAnswer(int questionId, int? id) {
			var model = new TestAnswer();
			EditQuestionPermission(questionId);
			if (id.HasValue) {
				TestAnswerService.LoadWith(x => x.TestQuestion);
				model = TestAnswerService.GetByPK(id.Value);
				if (model.ComparableId.HasValue)
					model.ComparableAnswer = TestAnswerService.GetByPK(model.ComparableId.Value);
			}
			else {
				model.QuestionId = questionId;
				model.TestQuestion = TestQuestionService.GetByPK(questionId);
			}
			return BaseView(
				new PagePart(new AnswerEditView().Init(model, Url)));
		}

		[HttpPost]
		public ActionResult EditAnswer(TestAnswer model) {
			EditQuestionPermission(model.QuestionId);
			if (!LinqToSqlValidator.Validate(ModelState, model))
				return ErrorJson();
			TestAnswerService.EnableTracking();
			if (model.Id == 0) {
				TestAnswerService.InsertAndSubmit(model);
				return UrlJson(Url.TestEdit().Urls.EditAnswer(model.QuestionId, model.Id));
			}
			var oldModel = TestAnswerService.GetByPK(model.Id);
			oldModel.Update(model, x => x.Description, x => x.IsRight, x => x.ComparableId, x => x.Sort);
			TestAnswerService.SubmitChanges();
			return OkJson();
		}

		[HttpPost]
		public ActionResult DeleteAnswer(int? id) {
			TestAnswerService.EnableTracking();
			var answer = TestAnswerService.GetByPK(id.Value);
			EditAnswerPermission(id.Value);
			TestAnswerService.DeleteAndSubmit(answer);
			return OkJson();
		}
		public ActionResult GetAnswerFileControl(int answerId) {
			return Content(TestControls.AnswerFileView(answerId, true).NotNullString());
		}
		public ActionResult UploadAnswerFile(int answerId, string qqfile) {
			EditAnswerPermission(answerId);
			var fileNameWithoutExt = UserImages.GetTestAnswerFileSysWithoutExt(answerId);
			DeleteAnswerFile(answerId);
			var result = SaveTestFile(qqfile, fileNameWithoutExt);
			return Json(result,"text/html");
		}
		[HttpPost]
		public ActionResult DeleteAnswerFile(int answerId) {
			EditAnswerPermission(answerId);
			var file = UserImages.GetTestAnswerFileSys(answerId);
			if(file != null)
				System.IO.File.Delete(file);
			return OkJson();
		}


		public ActionResult GetQuestionFileControl(int questionId) {
			return Content(TestControls.QuestionFileView(questionId, true).NotNullString());
		}
		public ActionResult UploadQuestionFile(int questionId, string qqfile) {
			EditQuestionPermission(questionId);
			var fileNameWithoutExt = UserImages.GetTestQuestionFileSysWithoutExt(questionId);
			DeleteQuestionFile(questionId);
			var result = SaveTestFile(qqfile, fileNameWithoutExt);
			return Json(result, "text/html");
		}
		[HttpPost]
		public ActionResult DeleteQuestionFile(int questionId) {
			EditQuestionPermission(questionId);
			var file = UserImages.GetTestQuestionFileSys(questionId);
			if(file != null)
				System.IO.File.Delete(file);
			return OkJson();
		}

		private object SaveTestFile(string clientFileName, string filename) {
			if(Request.Files.Count > 0)
				clientFileName = Request.Files[0].FileName;
			var extension = Path.GetExtension(clientFileName);
			filename +=  extension;
			if (!Urls.TestFileExts.Any(x => x.EndsWith(extension)))
				return new {Message = "Ext"};
			if (Request.ContentLength > UserImages.MaxTestFileSize.Bytes) {
				return new {Message = "Size"};
			}
			if(Request.Files.Count > 0)
				Request.Files[0].SaveAs(filename);
			else {
				var buffer = new byte[Request.ContentLength];  
			    using (var br = new BinaryReader(Request.InputStream))  
			        br.Read(buffer, 0, buffer.Length);  
				System.IO.File.WriteAllBytes(filename, buffer);
			}
			return new {Message= "ok"};
		}

		[HttpPost]
		public ActionResult Activate(int id, bool activate) {
			TestService.EnableTracking();
			var test = TestService.GetByPK(id);
			test.Status = activate ? TestStatus.Active : TestStatus.Edit;
			TestService.SubmitChanges();
			return OkJson();
		}

		[AjaxOnly]
		public ActionResult GetQuestions(int testId, AjaxGridRequest model) {
			var list = TestQuestionService.GetAll(x => x.TestId == testId)
				.Select(x => new {x.Id, x.Description}).ToPagedList(model.Page - 1, model.Rows);

			list = list.Select(x => new {x.Id, Description = StringUtils.ReplaceGLT(x.Description)})
				.ToPagedList(list);

			return Json(new GridData(list.PageCount,
				model.Page,
				list.Count,
				list), JsonRequestBehavior.AllowGet);
		}
		
		[AjaxOnly]
		public ActionResult GetAnswers(int questionId, AjaxGridRequest model) {
			var question = TestQuestionService.GetByPK(questionId);

			Expression<Func<TestAnswer, object>> selector = null;

			PagedList<object> list;
			switch (question.Type) {
				case TestQuestionType.OneAnswer:
				case TestQuestionType.ManyAnswers:
					var l1 = TestAnswerService.GetAll(x => x.QuestionId == questionId)
						.Select(x => new {
						x.Id,
						x.Description,
						IsRight = x.IsRight.GetValueOrDefault() ? "Да" : "Нет"
					}).ToPagedList(model.Page - 1, model.Rows);

					list = l1.Select(x => new {
						x.Id,
						Description = StringUtils.ReplaceGLT(x.Description),
						x.IsRight
					}).Cast<object>().ToPagedList(l1);
                    
					break;
				case TestQuestionType.Comparison:
					var l2 = TestAnswerService.GetAll(x => x.QuestionId == questionId)
						.Select(x => new {
						x.Id,
						x.Description,
						ComparableId = x.ComparableAnswer.Description
					}).ToPagedList(model.Page - 1, model.Rows);

					list = l2.Select(x => new {
						x.Id,
						Description = StringUtils.ReplaceGLT(x.Description),
						x.ComparableId
					}).Cast<object>().ToPagedList(l2);
					break;
				case TestQuestionType.Sort:
					var l3 = TestAnswerService.GetAll(x => x.QuestionId == questionId)
						.Select(x => new {
						x.Id,
						x.Description,
						x.Sort
					}).ToPagedList(model.Page - 1, model.Rows);
					list = l3.Select(x => new {
						x.Id,
						Description = StringUtils.ReplaceGLT(x.Description),
						x.Sort
					}).Cast<object>().ToPagedList(l3);
					break;
				default:
					throw new Exception("TestQuestionType out of range");
			}
			return Json(new GridData(list.PageCount,
				model.Page,
				list.Count,
				list), JsonRequestBehavior.AllowGet);
		}


	
	}
}
