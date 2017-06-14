using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using SimpleUtils.FluentAttributes.Core.Interfaces;

namespace Specialist.Entities.Passport.MetaData
{
    public class CompanyMD: BaseMetaData<Company>
    {
        public override void Init()
        {
            For(x => x.CompanyName).Display("�������� �����������");
            For(x => x.INN).Display("���");
			For(x => x.KPP).Display("���/������");
			For(x => x.LegalAddress).Display("����������� �����");
        }
    }
}