using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class OrganizationMD : CmsMetaData<Organization>
    {
        public override void Init()
        {
            this.Display("Организация");
            this.DisplayBy(x => x.Name);
            this.AddName();
            For(x => x.LogoImg).Display("Логотип");
        }
    }
}