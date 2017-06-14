using System;
using System.Collections.Generic;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using SimpleUtils.Collections.Extensions;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Util;

namespace Specialist.Web.Core.Views {
	public class TestRunView:BaseView<TestRunVM> {


		public override object Get() {
			var questions = GetQuestions();

			var count = questions.Count();
			return l( 
				div.Id("test-engine-content").Style("display:none;")[
				span["Осталось времени - ", span.Id("time-control")]
					.Class("ui-state-default ui-corner-all").Style("padding:5px;"),br,
				div[questions.Select((x,i) => QuestionView(x,i,count))]
				.Class("ui-corner-all ui-widget-content").Style("padding:10px;margin-top:5px;"),
				div.Class("ui-widget-content ui-corner-all").Style("margin-top:5px;")
				["Перейти к вопросу по номеру. Желтым выделены отмеченные Вами вопросы",
				br,div.Id("mark-questions").Style("margin-top:5px;")
				].Style("padding:5px;")
				,
				div.Id("test-navigation").Style("margin:5px;"),
				GetScript("/Scripts/Views/TestRun/testengine.js?v=18", "initTestEngine",
					Url.Action<TestRunController>(c => c.GetTestTime(Model.UserTest.Id)), 
					Url.Action<TestRunController>(c => c.ResultPost(Model.UserTest.Id,null)),
					Url.Action<TestRunController>(c => c.Result(Model.UserTest.Id))
					)
				]);
		}

		private List<TestQuestion> GetQuestions() {
			var questions = new List<TestQuestion>();
			var modulePercents = EntityUtils.GetModulePercents(Model.UserTest.TestPassRule);
			var questionCount = Model.UserTest.TestPassRule.QuestionCount;
			var testQuestions = Model.Test.TestQuestions.Shuffle();
			if(Model.OtherPreTestQuestions.Any()) {
				testQuestions = testQuestions.Concat(Model.OtherPreTestQuestions)
					.Shuffle();
			}
			if(modulePercents.Count == 0 || Model.UserTest.IsPrerequisite)
				return testQuestions.Take(questionCount).ToList();
			if (modulePercents.All(x => x.Value == 0)) {
				var modules = testQuestions.Select(x => x.ModuleId.GetValueOrDefault()).Where(x => x != 0)
					.Distinct().ToList();
				var average = 100/modules.Count;
				modulePercents = modules.ToDictionary(x => x, y => average);
			}
			var counts = modulePercents.ToDictionary(x => x.Key, x => (questionCount*x.Value)/100);
			foreach (var moduleCount in counts) {
				questions.AddRange(testQuestions
					.Where(x => x.ModuleId == moduleCount.Key).Take(moduleCount.Value));
			}

			if (questionCount > questions.Count) {
				var ids = new HashSet<int>(questions.Select(x => x.Id));
				questions.AddRange(
					testQuestions.Where(x => !ids.Contains(x.Id)).Take(questionCount - questions.Count));
			}
			return questions;
		}

		public object QuestionView(TestQuestion model, int index, int count) {
			return div
				.Class(GetQuestionClass(model) + " test-question entity-id-" + model.Id)[
				h2["Вопрос {0} из {1}".FormatWith(index + 1, count)],
				button.Class("mark-button")["Отметить"].Title("Отметьте вопрос, что бы потом к нему вернуться"),
				p[TestReadOnlyView.TestText(model.Description), div.Style("margin:5px;")[TestControls.QuestionFileView(model.Id, false)]],
				AnswerPart(model)].Style("display:none;");

		}

		public string GetQuestionClass(TestQuestion model) {
			switch (model.Type) {
				case TestQuestionType.OneAnswer:
					return "question-type-one-answer";
				case TestQuestionType.ManyAnswers:
					return "question-type-many-answers";
				case TestQuestionType.Comparison:
					return "question-type-comparison";
				case TestQuestionType.Sort:
					return "question-type-sort";
				default:
					throw new Exception("TestQuestionType out of range");
			}
			
		}

		public object AnswerPart(TestQuestion model) {
			switch (model.Type) {
				case TestQuestionType.OneAnswer:
					return l(h3["Выберите один ответ:"], GetAnswerList(model));
				case TestQuestionType.ManyAnswers:
					return l(h3["Выберите несколько ответов:"], GetAnswerList(model));
					
				case TestQuestionType.Comparison:
					return l(h3["Сопоставте ответы:"], ComparisonView(model));
				case TestQuestionType.Sort:
					return l(h3["Отсортируйте в правильном порядке:"], GetAnswerList(model.TestAnswers.Shuffle())
						.AddClass("sortable"));
				default:
					throw new Exception("TestQuestionType out of range");
			}
		}

		public object ComparisonView(TestQuestion model) {
			var answers1 = model.TestAnswers.Where(x => x.ComparableId.HasValue)
				.Shuffle().ToList();
			var answers2 = model.TestAnswers.Where(x => x.ComparableId == null)
				.Shuffle().ToList();

			return l(
				table[tr[
				td[ol[answers1.Select(x => li[GetAnswerText(x)])]],
				td[ol.Style("list-style-type:upper-alpha;")[answers2.Select(x => li[GetAnswerText(x)])]]]],
				table.Class("question-type-comparison")[tr[
				td.Class("left-answers")[GetAnswerList(answers1, (x, i) => (i+1).ToString()).AddClass("sortable")], 
				td[ul.Class("test-answers")[answers1.Select(x => li[
					span.Class("ui-icon ui-icon-grip-solid-horizontal")])]],
				td.Class("right-answers")[GetAnswerList(answers2, (x, i) => ((char)(i + 65)).ToString()).AddClass("sortable")]]]
				);
		}


		object GetAnswerText(TestAnswer answer) {
			var file = TestControls.AnswerFileView(answer.Id, false);
			var desc = TestReadOnlyView.TestText(answer.Description);
			if(file == null)
				return desc;
			return H.l(desc, br, TestControls.AnswerFileView(answer.Id, false));
		}


		private TagUl GetAnswerList(IEnumerable<TestAnswer> answers,
			Func<TestAnswer, int, object> getText = null) {
			if(getText == null)
				getText = (x, i) => GetAnswerText(x);

			return ul.Class("test-answers")[answers.Select((x,i) =>
				li.Class("test-answer ui-state-default entity-id-" + x.Id)[getText(x,i)])];
		}

		private TagUl GetAnswerList(TestQuestion model) {
			return GetAnswerList(model.TestAnswers);
		}
	}
}