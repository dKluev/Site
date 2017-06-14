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
using System.Linq;

namespace Specialist.Web.Core.Views {
	public class ModuleSetEditView:BaseView<ModuleSetVM> {

		object Modules() {
			return TestControls.ModulePercentsView(Model.ModulePercents, Model.Modules);
		}

		public override object Get() {
			return l(
				AjaxForm(Url.Action<TestEditController>(x => x.EditModuleSet(null)))[
					HiddenFor(x => Model.ModuleSet.Id),
					HiddenFor(x => Model.ModuleSet.TestId),
					HiddenFor(x => Model.ModuleSet.TestPassRule.Id), 
					ControlFor(x => Model.ModuleSet.Number),
					ControlFor(x => Model.ModuleSet.Description),
					Modules(),
					ControlFor(x => Model.ModuleSet.TestPassRule.Time),
					ControlFor(x => Model.ModuleSet.TestPassRule.QuestionCount),
					ControlFor(x => Model.ModuleSet.TestPassRule.PassMark),
					SaveButton()]);
		}

	}
}