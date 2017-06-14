using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Shop;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Helpers;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgNextCoursesView:BaseView<OrgNextCoursesVM> {
		public override object Get() {
			if(!Model.Courses.Any())
				return p["Вариантов нет"];
			return div[AjaxForm(Url.Action<CartController>(c => c.AddCourseListPost(null)))[
				Model.Courses.Select(x => 
					p[H.InputCheckbox("courseTCs",x.Course_TC),x.WebName]),
				SaveButton("Добавить в корзину")
				]];
		}
	}
}