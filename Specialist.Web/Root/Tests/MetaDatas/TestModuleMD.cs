using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestModuleMD : BaseMetaData<TestModule> {
		public override void Init() {
			For(x => x.Name).Display("Название");
		}
	}
}