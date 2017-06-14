using System;
using System.Reflection;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Tests;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Tests.ViewModels;
using Specialist.Web.Common.Extension;
using Specialist.Web.ViewModel.Orders;

namespace Specialist.Web.Core.Views {
	public class AddSeminarView:BaseView<AddSeminarVM> {
		public override object Get() {
			return l( 
				Form(Url.Action<CourseController>(x => x.AddSeminar(Model.Group.Group_ID, false)))[
				SaveButton("Дистанционно (вебинар)").Class("ui-button")
				],
				Form(Url.Action<CourseController>(x => 
					x.AddSeminar(Model.Group.Group_ID, true)))[
				SaveButton("Очно (необходимо приехать на место проведения)")
				.Class("ui-button")
				]
			);
		}
	}
}