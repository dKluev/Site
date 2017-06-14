using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Core.Views {
	public class GroupPrepareView:BaseView<GroupPrepareVM> {
		public override object Get() {
			return l( 
				AjaxForm(Url.Action<GroupTestController>(x => x.Prepare(null)))[
				ControlFor(x => x.GroupID), 
				ControlFor(x => x.Email), 
				SaveButton("Создать группу")
				]);
		}
	}
}