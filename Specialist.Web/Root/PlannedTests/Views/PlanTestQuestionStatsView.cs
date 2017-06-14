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
	public class PlanTestQuestionStatsView: BaseView<PlanTestQuestionStatsVM> {
		public override object Get() {
			return div[TestControls.StatsLinks(Url, Model.GroupId,false), 
				H.table[H.Head("Вопрос", "Правильно (%)"),
				Model.QuestionStats.OrderBy(x => x.Item2).Select(x =>
					H.Row(Model.QuestionNames[x.Item1], 
					x.Item2
					))]
					.Class("defaultTable")];
		}
	}
	}