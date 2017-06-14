using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using Specialist.Web.ViewModel.Orders;

namespace Specialist.Web.Core.Views {
	public class WorkPlaceView:BaseView<WorkPlace> {
		public override object Get() {
			return l( 
				AjaxForm(Url.Action<ProfileController>(x => x.WorkPlacePost(null)))[
				ControlFor(x => x.Name), 
				ControlFor(x => x.Site), 
				ControlFor(x => x.FullName), 
				ControlFor(x => x.Phone), 
				ControlFor(x => x.Email), 
				SaveButton()
				]);
		}
	}
}