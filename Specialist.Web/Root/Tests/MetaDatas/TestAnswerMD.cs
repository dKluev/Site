using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestAnswerMD : BaseMetaData<TestAnswer> {
		public override void Init() {
			For(x => x.Description).Display("Описание").UIHint(Controls.TextArea);
			For(x => x.ComparableId).Display("Ответ");
			For(x => x.IsRight).Display("Правильный");
			For(x => x.Sort).Display("Сортировка");
		}
	}
}