using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Core.Views {
	public class ModuleEditView:BaseView<TestModule> {
		public override object Get() {
			return l(Title(), 
				AjaxForm(Url.Action<TestEditController>(x => x.EditModule(null)))[
				HiddenFor(x => x.Id), 
				HiddenFor(x => x.TestId), 
				ControlFor(x => x.Name), 
				SaveButton()
				]);
		}
	}
}