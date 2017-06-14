using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestQuestionMD : BaseMetaData<TestQuestion> {
		public override void Init() {
			For(x => x.Description).Display("��������").UIHint(Controls.BigTextArea);
			For(x => x.ModuleId).Display("������");
			For(x => x.Type).Display("���");
		}
	}
}