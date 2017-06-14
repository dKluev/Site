using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestPassRuleMD : BaseMetaData<TestPassRule> {
		public override void Init() {
			For(x => x.Time).Display("�����(���)");
			For(x => x.QuestionCount).Display("���������� ��������");
			For(x => x.PassMark).Display("��������� ����(��)");
			For(x => x.AverageMark).Display("�� ������� �������");
			For(x => x.ExpertMark).Display("�� �������");
		}
	}
}