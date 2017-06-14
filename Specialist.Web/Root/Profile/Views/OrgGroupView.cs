using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Controllers;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Helpers;
using SpecialistTest.Web.Core.Mvc.Extensions;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgGroupView:BaseView<OrgGroupVM> {
		public override object Get() {
			return l(h2["Курс: ", Model.Group.Course.Name],
				table.Class("table")[H.Head("Сотрудник", "Обучение в группе", "Анкета", "Продолжение"),
					Model.Students.Select(x => H.Row(x.FullName,
						Url.GroupAttendanceLink(x.Student_ID, Model.Group.Group_ID),
					Url.OrgProfile().Questionnaire(x.Student_ID, Model.Group.Group_ID, "Обучение")
					.OpenInUiDialog(),
					Url.OrgProfile().NextCourseOrder(x.Student_ID, Model.Group.Group_ID,
					"Варианты продолжения").OpenInUiDialog()
						))]);
		}
	}
}