using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class GroupTestMD : BaseMetaData<GroupTest> {
		public override void Init() {
			this.DisplayBy(x => x.GroupInfoId);
			this.Display("������");
			this.ReadOnly();
			For(x => x.TestId).Display("����");
			For(x => x.DateBegin).Display("���� ������");
			For(x => x.DateEnd).Display("���� ���������");
			For(x => x.AttemptCount).Display("���������� �������");
		}
	}
}