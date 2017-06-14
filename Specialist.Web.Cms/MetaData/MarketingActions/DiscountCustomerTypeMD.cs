using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.MarketingAction
{
    public class DiscountCustomerTypeMD : CmsMetaData<DiscountCustomerType>
    {
        public override void Init()
        {
            this.Display("Тип обучения");
            this.Deletable();
            For(x => x.Discount_ID).Display("Бонус");
            For(x => x.CustomerType_TC).Display("Тип обучения");
        }
    }
}