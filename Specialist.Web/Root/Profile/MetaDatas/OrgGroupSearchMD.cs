using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Root.Profile.ViewModels;

namespace Specialist.Web.Root.Profile.MetaDatas {
	public class OrgGroupSearchMD : BaseMetaData<OrgGroupSearchVM> {
		public override void Init() {
			For(x => x.LeftDataBeg).Display("Дата c");
			For(x => x.RightDataBeg).Display("Дата по");
			For(x => x.CourseTC).Display("Курс");
			For(x => x.StudentId).Display("Сотрудник");
			For(x => x.Current).Display("Текущие");
		}
	}
}