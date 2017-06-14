using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Core.Views {
	public class AnswerEditView:BaseView<TestAnswer> {
		public override object Get() {
			object answerControl = null;
			switch (Model.TestQuestion.Type) {
				case TestQuestionType.OneAnswer: 	
				case TestQuestionType.ManyAnswers:
					answerControl = ControlFor(x => x.IsRight);
					break;
				case TestQuestionType.Comparison:
				answerControl = Select2For(Model.ComparableAnswer.GetOrDefault(x => x.Description),
					x => x.ComparableId, Url.Action<TestEditController>(c => c.GetAnswersAuto(Model.TestQuestion.Id, null)));
					break;
				case TestQuestionType.Sort:
					answerControl = ControlFor(x => x.Sort);
					break;
				default:
					throw new Exception("TestQuestionType out of range");
			}
			return l(
				Tabs(_.List("Ответ", "Файл"),
				AjaxForm(Url.Action<TestEditController>(x => x.EditAnswer(null)))[
				HiddenFor(x => x.Id), 
				HiddenFor(x => x.QuestionId), 
				ControlFor(x => x.Description), 
				answerControl,
				SaveButton()
				], 
				TestControls.TestFileUpload(TestControls.AnswerFileView(Model.Id, true), 
				Url.Action<TestEditController>(c => c.UploadAnswerFile(Model.Id,null)),
					Url.Action<TestEditController>(c => c.DeleteAnswerFile(Model.Id)), 
					Url.Action<TestEditController>(c => c.GetAnswerFileControl(Model.Id)),
					Model.Id == 0	
				))
				
				);
		}
	}
}