using Specialist.Entities.Const;
using Specialist.Web.Common.Html;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Helpers;

namespace Specialist.Web.Root.Profile.Views {
	public class OrgRealSpecialistView:BaseView<OrgRealSpecialistVM> {
		public override object Get() {
			if(!Model.Students.Any())
				return p["Сотрудники со статусом Настоящий Специалист отсутствуют"];
			return div[H.table.Class("table")[H.Head("Сотрудник","Статус"),
				Model.Students.Select(x => H.Row(x.FullName,ClabCardColors.GetName(x.StudentCalc
					.StudentClabCard.ClabCardColor_TC)))]];
		}
	}
}