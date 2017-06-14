using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestModuleSetMD : BaseMetaData<TestModuleSet> {
		public override void Init() {
			For(x => x.Number).Display("Номер тестирования");
			For(x => x.Description).Display("Описание").UIHint(Controls.BigTextArea);
		}
	}
}