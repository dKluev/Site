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
using Specialist.Web.Const;

namespace Specialist.Web.Core.Views {
	public class PlanTestUserStatsView: BaseView<PlanTestUserStatsVM> {
		TagTable Users(List<UserTest> userTests) {
			return H.table[H.Head("ФИО", "Правильно", "Неправильно", "Статус"),
				userTests.Select(x =>
					H.Row(Url.TestRun().UserTestAnswers(x.Id, Model.UserNames[x.UserId])
					.Class("not-link open-in-uidialog"), 
					x.RightCount, 
					x.WrongCount,
					NamedIdCache<UserTestStatus>.GetName(x.Status)
					))]
					.Class("defaultTable");
			
		}
		public override object Get() {
			return div[
				TestControls.StatsLinks(Url, Model.GroupId,true),
				Model.ModuleSetStats.OrderBy(x => x.Set.Number).Select(x =>
				div[p[strong[x.Set.Number + "."]," ", x.Set.Description],
					Users(x.UserTests)]
				)];
		}
	}
	}