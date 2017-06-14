using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData
{
    public class SectionMD: CmsMetaData<Section>
    {
        public override void Init()
        {
            this.Display("Направление");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
	        For(x => x.IsMain).Display("Главное");
        	For(x => x.RelationsAsMenu).Display("Меню");
			For(x => x.IsSeparator).Display("Разделитель");
			For(x => x.ForMainPage).Display("На главную");
			For(x => x.DescriptionForTest).UIHint(Controls.Html).Display("Описание[Т]");
            For(x => x.NotAnnounce).Display("Не анонс").UIHint(CommonConst.CourseTCList);
        }
    }
}