using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Entities.Passport.MetaData
{
    public class UserAddressMD : BaseMetaData<UserAddress>
    {
        public UserAddressMD()
        {
            For(x => x.ForSberbank).UIHint(Controls.Hidden);
            For(x => x.CountryID).Display("������") ;
            For(x => x.City).Display("�����")
                .Example("������: ������");
            For(x => x.Index)
                .Display("�������� ������")
                .Example("������: 123456");
            For(x => x.Address)
                .Display("�������� �����")
                .Example("������: ��. ����������, ��� 16, ������ 2, ��. 11");

        }
    }
}