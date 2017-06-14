using Microsoft.Web.Mvc;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Controllers;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.SimpleTests.ViewModels;

namespace Specialist.Web.Root.SimpleTests.Views {
	public class SimpleTestResultView:BaseView<SimpleTestResultVM> {
		public override object Get() {
			return div[
				h3[Url.SimpleTest().Details(Model.Test.Id, Model.Test.Name)],
				Model.Result.Text, br,
				Htmls.AddThis(null),
				br, 
				 HtmlHelper.Action<GroupController>(c => 
					 c.ForCourseTCList(Model.Result.Courses,false,0))
				];
		}
	}
}