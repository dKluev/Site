using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using SpecialistTest.Web.Core.Mvc;

namespace Specialist.Web.Core.Views {
	public class QuestionEditView:BaseView<TestQuestion> {
		public override object Get() {
			var typeControl = H.l(LabelFor(x => x.Type), SelectFor(x => x.Type, 
				NamedIdCache<TestQuestionType>.List, x=> x.Name, x => x.Id));
			if(Model.Id > 0) {
				typeControl = H.l(LabelFor(x => x.Type), div.Class("editor-div")[
					TestQuestionType.GetName(Model.Type)], HiddenFor(x => x.Type));
			}
			return l(
				Tabs(_.List("Вопрос", "Файл"),
				AjaxForm(Url.Action<TestEditController>(x => x.EditQuestion(null)))[
				HiddenFor(x => x.Id), 
				HiddenFor(x => x.TestId), 
				ControlFor(x => x.Description), 
				typeControl, 
				Select2For(Model.TestModule.GetOrDefault(x => x.Name),x => x.ModuleId, 
					Url.Action<TestEditController>(c => c.GetModulesAuto(Model.TestId,null))), 
				SaveButton()
				],TestControls.TestFileUpload(TestControls.QuestionFileView(Model.Id, true), 
				Url.Action<TestEditController>(c => c.UploadQuestionFile(Model.Id,null)),
					Url.Action<TestEditController>(c => c.DeleteQuestionFile(Model.Id)), 
					Url.Action<TestEditController>(c => c.GetQuestionFileControl(Model.Id)),
					Model.Id == 0	
				)));
		}

	}
}