using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class EntityStudySetMD : CmsMetaData<EntityStudySet>
    {
        public override void Init()
        {
            this.Display("�������� ��������");
        	this.DisplayBy(x => x.EntityName);
			this.NotAdd();
            For(x => x.EntityName).Display("��������").ReadOnly();
            For(x => x.CourseTCList).Display("���� ������")
				.UIHint(CommonConst.CourseTCList).SetAttribute(new FullLengthStringDisplayAttribute());
        }
    }
}