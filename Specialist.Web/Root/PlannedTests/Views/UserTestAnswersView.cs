using System.Collections.Generic;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.PlannedTests.ViewModels;
using Specialist.Web.Root.Tests.ViewModels;
using System.Linq;
using SpecialistTest.Web.Core.Mvc.Extensions;

namespace Specialist.Web.Core.Views {
	public class UserTestAnswersView: BaseView<UserTestAnswerVM> {
		public override object Get() {
			return H.div[Model.Questions.Select(x => TestReadOnlyView.QuestionView(x,0,0, true))];
		}
	}
	}