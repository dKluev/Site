using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class GroupTestMD : BaseMetaData<GroupTest> {
		public override void Init() {
			this.DisplayBy(x => x.GroupInfoId);
			this.Display("Группы");
			this.ReadOnly();
			For(x => x.TestId).Display("Тест");
			For(x => x.DateBegin).Display("Дата начала");
			For(x => x.DateEnd).Display("Дата окончания");
			For(x => x.AttemptCount).Display("Количество попыток");
		}
	}
}