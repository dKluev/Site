using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.MarketingAction
{
    public class DiscountComplexMD : CmsMetaData<DiscountComplex>
    {
        public override void Init()
        {
            this.Display("Комплекс");
            this.Deletable();
            For(x => x.Discount_ID).Display("Бонус");
            For(x => x.Complex_TC).Display("Комплекс").ForeignType(typeof(Complex));

        }
    }
}