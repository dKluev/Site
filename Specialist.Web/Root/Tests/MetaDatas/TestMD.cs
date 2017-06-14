using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Web.Cms.Core.FluentMetaData;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Root.Tests.MetaDatas {
	public class TestMD : BaseMetaData<Test> {
		public override void Init() {
            this.Display("����");
			this.DisplayByName();
            this.NotAdd();
			For(x => x.Name).Display("��������");
			For(x => x.Description).Display("��������").UIHint(Controls.Html);
			For(x => x.Status).Display("������").ForeignType(typeof(TestStatus));
			For(x => x.Author_TC).Display("��� ������").UIHint(Controls.Text);
			For(x => x.Checker_TC).Display("��� ������������").UIHint(Controls.Text);
			For(x => x.IsNew).Display("�����");
			For(x => x.Certified).Display("����������");
			For(x => x.ProductName).Display("�������");
			For(x => x.ProductNameEng).Display("�������(���)");
            For(x => x.CourseTCList).Display("����").UIHint(CommonConst.CourseTCList);
			For(x => x.NoRestriction).Display("��� �����������");
			For(x => x.TestIdList).Display("�����").UIHint(Controls.Text);
			this.TryAddUpdateAndChanger();
		}
	}
}
