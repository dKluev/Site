using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class CourseMD : CmsMetaData<Course>
    {
        public override void Init()
        {
            this.Display("����");
            this.ReadOnly();
            this.DisplayBy(d => d.Course_TC);
//            this.DisplayByName();
            For(x => x.Course_TC).Display("���");
            For(x => x.IsActive).Display("�������");
            For(x => x.IsNew).Display("�����");
            For(x => x.WebPublishSchedule).Display("���");
            this.AddName();
        }
    }
}