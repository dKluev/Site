using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestPassRuleMD : BaseMetaData<TestPassRule> {
		public override void Init() {
			For(x => x.Time).Display("Время(мин)");
			For(x => x.QuestionCount).Display("Количество вопросов");
			For(x => x.PassMark).Display("Проходной балл(ПБ)");
			For(x => x.AverageMark).Display("ПБ средний уровень");
			For(x => x.ExpertMark).Display("ПБ эксперт");
		}
	}
}