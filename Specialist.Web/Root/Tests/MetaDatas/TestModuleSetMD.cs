using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestModuleSetMD : BaseMetaData<TestModuleSet> {
		public override void Init() {
			For(x => x.Number).Display("����� ������������");
			For(x => x.Description).Display("��������").UIHint(Controls.BigTextArea);
		}
	}
}