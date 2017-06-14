using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Tests;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using Specialist.Web.ViewModel.Orders;

namespace Specialist.Web.Core.Views {
	public class ExpressRegisterView:BaseView<ExpressRegisterVM> {
		public override object Get() {
			return l( 
				AjaxForm(Url.Action<OrderController>(x => x.ExpressRegisterPost(null)))[
				ControlFor(x => x.LastName), 
				ControlFor(x => x.FirstName), 
				ControlFor(x => x.SecondName), 
				ControlFor(x => x.Phone), 
				ControlFor(x => x.Email), 
				ControlFor(x => x.PersonalData), 
				SaveButton("ќформить заказ")
				]);
		}
	}
}