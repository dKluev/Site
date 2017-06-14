using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;

namespace Specialist.Web.Root.Center.ViewModels {
	public class AddResponseVM: IViewModel {

		[DisplayName("Выберите курс который вы проходили")]
		public string CourseTC { get; set; }

		public string EmployeeTC { get; set; }

		public List<CourseLink> Courses { get; set; }

		[DisplayName("Отзыв")]
		[UIHint(Controls.TextArea)]
		public string Text { get; set; }

		public string Title { get { return "Отзыв о преподавателе"; } }
	}
}