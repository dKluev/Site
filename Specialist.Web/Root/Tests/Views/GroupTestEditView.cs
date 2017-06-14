using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using System.Linq;

namespace Specialist.Web.Core.Views {
	public class GroupTestEditView : BaseView<GroupTestEditVM> {
		public override object Get() {
			var names = _.List("Тест");
			if(Model.Modules.Any())
				names.Add("Проценты модулей");
			return l(
				AjaxForm(Url.Action<GroupTestController>(x => x.EditGroupTest(null)))[
				Tabs(names,
				l(
				AutocompleteFor(Model.GroupTest.Test.GetOrDefault(x => x.Name), x => x.GroupTest.TestId, 
				Url.Action<GroupTestController>(c => c.GetTestsAuto(Model.GroupTest.GroupInfoId, null))),
				HiddenFor(x => x.GroupTest.Id),
				ControlFor(x => x.GroupTest.DateBegin),
				ControlFor(x => x.GroupTest.DateEnd),
				ControlFor(x => x.GroupTest.AttemptCount),
				HiddenFor(x => x.GroupTest.GroupInfoId),
				HiddenFor(x => x.GroupTest.TestPassRule.Id), 
				ControlFor(x => x.GroupTest.TestPassRule.Time), 
				ControlFor(x => x.GroupTest.TestPassRule.QuestionCount), 
				ControlFor(x => x.GroupTest.TestPassRule.PassMark), 
				ControlFor(x => x.GroupTest.TestPassRule.AverageMark), 
				ControlFor(x => x.GroupTest.TestPassRule.ExpertMark)
				),
					TestControls.ModulePercentsView(Model.ModulePercents, Model.Modules)
				),SaveButton()
				]);
		}
	}
}