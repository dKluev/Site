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
            this.Display("Тест");
			this.DisplayByName();
            this.NotAdd();
			For(x => x.Name).Display("Название");
			For(x => x.Description).Display("Описание").UIHint(Controls.Html);
			For(x => x.Status).Display("Статус").ForeignType(typeof(TestStatus));
			For(x => x.Author_TC).Display("Код автора").UIHint(Controls.Text);
			For(x => x.Checker_TC).Display("Код проверяющего").UIHint(Controls.Text);
			For(x => x.IsNew).Display("Новый");
			For(x => x.Certified).Display("Сертификат");
			For(x => x.ProductName).Display("Продукт");
			For(x => x.ProductNameEng).Display("Продукт(Анг)");
            For(x => x.CourseTCList).Display("Курс").UIHint(CommonConst.CourseTCList);
			For(x => x.NoRestriction).Display("Без ограничения");
			For(x => x.TestIdList).Display("Тесты").UIHint(Controls.Text);
			this.TryAddUpdateAndChanger();
		}
	}
}
