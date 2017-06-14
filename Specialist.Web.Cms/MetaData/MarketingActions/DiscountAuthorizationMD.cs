using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.MarketingAction
{
    public class DiscountAuthorizationMD : CmsMetaData<DiscountAuthorizationType>
    {
        public override void Init()
        {
            this.Display("Авторизация");
            this.Deletable();
            For(x => x.Discount_ID).Display("Бонус");
            For(x => x.AuthorizationType_TC).Display("Авторизация").ForeignType(
                typeof(AuthorizationType));
        }
    }
}