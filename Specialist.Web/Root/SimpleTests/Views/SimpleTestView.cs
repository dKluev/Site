using System.Linq;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.SimpleTests.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Htmls = SimpleUtils.FluentHtml.Tags.Htmls;

namespace Specialist.Web.Root.SimpleTests.Views {
	public class SimpleTestView:BaseView<SimpleTest> {
		public override object Get() {
			var test = Model;
			return H.Div("simple-test-body")[
GetScript("/Scripts/Views/SimpleTest/simpletest.js?v=1", "initSimpleTest",
					Url.Action<SimpleTestController>(c => c.Result(test.Id,0))), 
				H.Div("simple-test-desc")[
				test.Description, br,
				Web.Common.Html.Htmls.AddThis(null)],

				test.Questions.Select((q, i) => Question(q, i))
				];
		}

		public TagDiv Question(SimpleTest.Question question, int index) {
			return div[
				h2["Вопрос " + (index + 1)],
				question.Text, br,
				Img(question.Image), br,
				question.Answers.Select((a, i) => p[button[a.Text]
					.Class("question-answer").Data("answer-index", i.ToString())])]
				.Style("display:none;").Class("test-question question-" + index);
		}
	}
}
