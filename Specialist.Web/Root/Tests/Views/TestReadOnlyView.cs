using System;
using System.Collections.Generic;
using SimpleUtils.Common.Enum;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using System.Linq;
using System.Web;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Util;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Utils;
using Specialist.Web.Const;

namespace Specialist.Web.Core.Views {
	public class TestReadOnlyView:BaseView<TestReadOnlyVM> {


		public object Modules() {
			var percents = EntityUtils.GetModulePercents(Model.Test.TestPassRule);
			if(percents.Sum(x => x.Value) == 0)
				return strong["Равномерно распределение по модулям"];
			return div[h3["Модули"], Model.Modules.Select((m,i) => p[strong[m.Name], " - ",
				percents.GetValueOrDefault(m.Id).NotNullString() + "%"])];
		}

		object SendToAuditorForm() {
			var sendToAuditButton = SaveButton("Передать на проверку преподавателю");
			Model.EmployeeTC = Model.Test.Checker_TC;
			return AjaxForm(Url.Action<TestEditController>(c =>
				c.SendToAudit(Model.Test.Id, null)))[
				Select2WithoutLabel(Model.Checker.GetOrDefault(x => x.FullName), x=> x.EmployeeTC, Url.TestEdit().Urls.GetEmployeeAuto(null)),
				sendToAuditButton];
		}
		object ReturnToEditForm() {
			return AjaxButton(Url.Action<TestEditController>(c =>
				c.ReturnToEdit(Model.Test.Id)), "Вернуть автору на редактирование");
		}

		public object TestComplete() {
			var testCompleteButton = SaveButton("Разместить новые тест на сайте");
			return AjaxForm(Url.Action<TestEditController>(c =>
				c.SendCompleteTest(Model.Test.Id)))[testCompleteButton];
		}

		public object AdminPanel() {

			var sendToAuditorForm = SendToAuditorForm();
			var returnToEditForm = ReturnToEditForm();
			var testCompleteForm = TestComplete();
			var status = Status();

			if (!Model.Test.Status.In(TestStatus.Audit)) {
				sendToAuditorForm = null;
				testCompleteForm = null;
			}
			if (Model.Test.Status == TestStatus.Edit) {
				returnToEditForm = null;
			}


			var block = div[
				h2["Администрирование"],
				status,
				sendToAuditorForm,
				hr,
				returnToEditForm,
				testCompleteForm 
				];
			if (!User.InRole(Role.TestAdmin)) {
				block = null;
			}
			return block;

		}

		private TagH3 Status() {
			var status = h3["Статус теста: ", TestStatus.GetName(Model.Test.Status)];
			return status;
		}

		public override object Get() {
			var testQuestions = Model.Test.TestQuestions;
			var count = testQuestions.Count;
			var grQuestions = testQuestions.GroupBy(x => x.ModuleId).ToList();
			var i = 0;

			var sendToAuditButton = AjaxButton(Url.TestEdit().Urls.SendToAudit(Model.Test.Id, null), 
				"Отправить на проверку").As<object>();
			var editLink = h3[Url.TestEdit().EditTest(Model.Test.Id, "Редактировать тест")];
			var activateButton = AjaxButton(Url.TestEdit().Urls.Activate(Model.Test.Id, !Model.Test.IsActive),
				Model.Test.IsActive ? "Дизактивировать" : "Активировать");
			if (User.CompanyID == null) {
				activateButton = null;
			}
			var userIsAuthro = User.Employee_TC == Model.Test.Author_TC;
			if (!userIsAuthro 
				|| Model.Test.Status != TestStatus.Edit 
				|| !Model.Test.TestPassRule.IsCorrect()) {
				sendToAuditButton = userIsAuthro ? Status() : null;
				editLink = null;
			}
			return l(
				AdminPanel(),
				h2["Условия прохождения"],
				Model.Test.TestPassRule.IsCorrect() ? null : JuiHtmls.Error("Ошибка в условии прохождения"),
					TextFor(x => x.Test.TestPassRule.Time), 
						TextFor(x => x.Test.TestPassRule.QuestionCount), 
						TextFor(x => x.Test.TestPassRule.PassMark), 
						TextFor(x => x.Test.TestPassRule.AverageMark), 
						TextFor(x => x.Test.TestPassRule.ExpertMark), 
				Modules(),
				editLink,
				grQuestions.Select(x => div[h2[Model.Modules.FirstOrDefault(m => m.Id == x.Key)
					.GetOrDefault(z => z.Name) ?? "Модуль не определен"],
					x.Select(q =>
					QuestionView(q, count, i++) )
					]),
					sendToAuditButton,activateButton
				);
		}

		public static object TestText(string txt) {
			return H.Raw(StringUtils.ReplaceGLT(txt));
		}

		public static TagDiv QuestionView(TestQuestion x, int count, int i, bool hideCount = false) {
			return div.Style("border-bottom:1px solid;").Class("test-question-id-" + x.Id)[
				h3["Вопрос" + (hideCount ? "" : " {0}/{1}".FormatWith(i + 1, count))],
				p[TestText(x.Description)], 
				TestControls.QuestionFileView(x.Id, false, true),
				div[AnswerPart(x)] ];
		}

		static object AnswerPart(TestQuestion model) {
			switch (model.Type) {
				case TestQuestionType.OneAnswer:
					return l(strong["Выберите один ответ:"], GetAnswerList(model.TestAnswers));
				case TestQuestionType.ManyAnswers:
					return l(strong["Выберите несколько ответов:"], GetAnswerList(model.TestAnswers));
					
				case TestQuestionType.Comparison:
					return l(strong["Сопоставьте ответы:"], ComparisonView(model));
				case TestQuestionType.Sort:
					return l(strong["Отсортируйте в правильном порядке:"], 
						GetAnswerList(model.TestAnswers.OrderBy(x => x.Sort.GetValueOrDefault())));
				default:
					throw new Exception("TestQuestionType out of range");
			}
		}


		private static TagUl GetAnswerList(IEnumerable<TestAnswer> answers) {
			return ul[answers.Select(x => 
				li.Style("margin-bottom:15px;")[x.IsRight.GetValueOrDefault() 
				? (object)div.Style("border:2px solid Green;padding:5px;border-radius: 5px;")[
					TestText(x.Description), TestControls.AnswerFileView(x.Id, false, true)] 
				: div[TestText(x.Description),TestControls.AnswerFileView(x.Id, false, true)]
				
				])];
		}

			public static object ComparisonView(TestQuestion model) {
			var answers1 = model.TestAnswers.Where(x => x.ComparableId.HasValue);
			var answers2 = model.TestAnswers.Where(x => x.ComparableId == null);
				var compare = answers1.Select(x =>
					Tuple.Create(x, answers2.FirstOrDefault(y => y.Id == x.ComparableId.Value)));

				return l(
					table[
						compare.Select(x => tr[
							td[x.Item2.GetOrDefault(a => TestText(a.Description)), br, 
							TestControls.AnswerFileView(x.Item2.Id, false)],
							td[TestText(x.Item1.Description), br, 
							TestControls.AnswerFileView(x.Item1.Id, false)] 
							]) ]);
		}
	}
}
