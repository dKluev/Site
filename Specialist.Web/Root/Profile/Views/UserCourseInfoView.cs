using Specialist.Entities.Context;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.Logic;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Root.Profile.Views {
	public class UserCourseInfoView:BaseView<UserCourseInfo> {
		public override object Get() {
			return
				div[h3["Информация по курсу для слушателей"],
					AjaxForm(Url.Action<CourseController>(x => x.UserCourseInfoPost(null)))[
						div.Class("editor-div")[
							HiddenFor(x => x.Course_TC),
							textarea[Model.Description].Name(Model.For(x => x.Description))
								.Id("course-description")],
						SaveButton()
						]
					];
		}
	}
}