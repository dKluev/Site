using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class OrgResponseMD : CmsMetaData<OrgResponse>
    {
        public override void Init()
        {
            this.Display("����� �����������");
            this.TryAddStandartProperties();
            For(x => x.ShortDescription).Display("���������").UIHint(Controls.TextArea);
            For(x => x.OriginalImg).Display("���� ���������");
            For(x => x.Authors).Display("������");
            For(x => x.OrganizationID).Display("�����������");
            For(x => x.Employee_TC).Display("���� ������.");
            For(x => x.Course_TC).Display("��� �����");
        }
    }
}