using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using System.Linq;
using EntityUtils = Specialist.Web.Util.EntityUtils;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Core.Views {
	public class TestEditView:BaseView<TestEditVM> {

		public object FormGroup(string name) {
			return h3[name];
		}

		public override object Get() {
			var courseControl = Select2For(Model.CourseName,x => x.Test.CourseTCList, 
				Url.Action<TestEditController>(c => c.GetCoursesAuto(null)));
			if (Model.Test.CompanyId.HasValue) {
				courseControl = null;
			}

			return l( 
				AjaxForm(Url.Action<TestEditController>(x => x.EditTest((TestEditVM)null)))[
				Tabs(_.List("Тест", "Условия прохождения", "Проценты модулей"),
					l(
						HiddenFor(x => x.Test.Id), 
						ControlFor(x => x.Test.Name), 
						ControlFor(x => x.Test.Description),
						courseControl 
					),
					l(
						HiddenFor(x => x.Test.TestPassRule.Id), 
						ControlFor(x => x.Test.TestPassRule.Time), 
						ControlFor(x => x.Test.TestPassRule.QuestionCount), 
						ControlFor(x => x.Test.TestPassRule.PassMark), 
						ControlFor(x => x.Test.TestPassRule.AverageMark), 
						ControlFor(x => x.Test.TestPassRule.ExpertMark) 
					),
					l(
					TestControls.ModulePercentsView(Model.ModulePercents, Model.Modules)
					)
				
				),
				SaveButton()
				]);
		}
	}
}