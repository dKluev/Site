using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.MarketingAction
{
    public class DiscountGraduateCourseMD : CmsMetaData<DiscountGraduateCourse>
    {
        public override void Init()
        {
            this.Display("����");
            this.Deletable();
            For(x => x.Discount_ID).Display("�����");
            For(x => x.Course_TC).Display("����").UIHint(Controls.Text);
            For(x => x.ForStudent).Display("���������� �����������");
        }
    }
}