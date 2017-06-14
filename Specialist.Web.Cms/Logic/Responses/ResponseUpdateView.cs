using System.Web.Mvc;
using Specialist.Web.Cms.Controllers;
using Specialist.Web.Cms.Core.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Cms.Logic.Responses {
	public class ResponseUpdateView:H {
		public string FromCourseTC { get; set; }

		public string ToCourseTC { get; set; }


		public object Get(UrlHelper url) {
			return Form(url.Action<CourseEntityController>(c => c.Update()))[
				p[label["Старый код"], 
				InputText(this.For(x => x.FromCourseTC), null)],
				p[label["Новый код"], 
				InputText(this.For(x => x.ToCourseTC), null)],
				Submit("Обновить").Id("send-for-all").Class("confirm-dialog")];
		}
		
	}
}