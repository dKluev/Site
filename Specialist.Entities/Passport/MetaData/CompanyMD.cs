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
            For(x => x.CompanyName).Display("Название организации");
            For(x => x.INN).Display("ИНН");
			For(x => x.KPP).Display("КПП/ОГРНИП");
			For(x => x.LegalAddress).Display("Юридический адрес");
        }
    }
}