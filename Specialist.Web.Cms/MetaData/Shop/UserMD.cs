using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Passport;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class UserMD : CmsMetaData<User>
    {
        public override void Init()
        {
            this.Display("������������");
            this.DisplayBy(x => x.Email);
            For(x => x.Email).Display("E-mail").ReadOnly();
            For(x => x.LastName).Display("�������").ReadOnly();
            For(x => x.FirstName).Display("���").ReadOnly();
            For(x => x.SecondName).Display("��������").ReadOnly();
            For(x => x.Employee_TC).Display("��� ����������");
            For(x => x.IsActive).Display("����������");
        }
    }
}