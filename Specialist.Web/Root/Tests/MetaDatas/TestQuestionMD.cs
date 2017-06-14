using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestQuestionMD : BaseMetaData<TestQuestion> {
		public override void Init() {
			For(x => x.Description).Display("Описание").UIHint(Controls.BigTextArea);
			For(x => x.ModuleId).Display("Модуль");
			For(x => x.Type).Display("Тип");
		}
	}
}