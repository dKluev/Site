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
            this.Display("�����������");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
	        For(x => x.IsMain).Display("�������");
        	For(x => x.RelationsAsMenu).Display("����");
			For(x => x.IsSeparator).Display("�����������");
			For(x => x.ForMainPage).Display("�� �������");
			For(x => x.DescriptionForTest).UIHint(Controls.Html).Display("��������[�]");
            For(x => x.NotAnnounce).Display("�� �����").UIHint(CommonConst.CourseTCList);
        }
    }
}