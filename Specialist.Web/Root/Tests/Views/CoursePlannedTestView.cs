using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests.Consts;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using System.Linq;
using SpecialistTest.Web.Core.Mvc.Extensions;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Helpers;

namespace Specialist.Web.Core.Views {
	public class CoursePlannedTestView: BaseView<CoursePlannedTestVM> {
		TagA GetStatus(int setId) {
			var userTest = Model.Statuses.GetValueOrDefault(setId);
			if(userTest == null)
				return null;
			return Url.UserTestLink(userTest,
				NamedIdCache<UserTestStatus>.GetName(userTest.Status))
				.Class("not-link open-in-uidialog");
		}
		public override object Get() {
			return H.table[H.Head("Номер", "Описание тестирования", "Лучший результат", ""),
				Model.ModuleSets.Select(x =>
					H.Row(x.Number, x.Description, GetStatus(x.Id),
						Url.Link<TestRunController>(c =>
							c.CoursePlanned(x.TestId, x.Id),
							Images.Button("teststart").ToString())))].Class("table");
		}
	}
}