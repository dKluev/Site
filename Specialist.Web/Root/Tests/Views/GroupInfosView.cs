using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using System.Linq;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Web.Const;

namespace Specialist.Web.Core.Views {
	public class GroupInfosView : BaseView<GroupInfosVM> {
		public override object Get() {
			return l(
				Model.GroupInfos.Any() ? (object)
				table.Class("defaultTable")[Head("Группа","Дата","Примечание", "Результат"),
					Model.GroupInfos.Select(x => Row(
						User.InRole(Role.TestAdmin) 
						? (object) Url.GroupTest().GroupInfo(x.Id,x.Group_ID) : x.Group_ID,
						x.Group.DateBeg.DefaultString(),
						x.Notes,
						Url.Link<GroupTestController>(c => c.Result(x.Id), "Результат"))
					)] 
					: p["На данный момент ничего нет"]);
		}
	}
}