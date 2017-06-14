using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.MarketingAction
{
    public class DiscountCityMD : CmsMetaData<DiscountCity>
    {
        public override void Init()
        {
            this.Display("Город");
            this.Deletable();
            For(x => x.Discount_ID).Display("Бонус");
            For(x => x.City_TC).Display("Город").ForeignType(typeof(City));
        }
    }
}