using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using System.Linq;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Web.Helpers;

namespace Specialist.Web.Core.Views {
	public class AssignTestsView : BaseView<AssignTestsVM> {
		public override object Get() {
			return l(Title(),
				table.Class("table")[Head("Тест","Дата начала","Дата окончания"),
					Model.GroupTests.Select(x => Row(
						Url.TestLink(x.Test),
						x.DateBegin.DefaultString(),
						x.DateEnd.DefaultString())
					)]);

		}
	}
}