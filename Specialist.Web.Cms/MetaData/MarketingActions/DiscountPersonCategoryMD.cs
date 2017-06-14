using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.MarketingAction
{
    public class DiscountPersonCategoryMD : CmsMetaData<DiscountPersonCategory>
    {
        public override void Init()
        {
            this.Display("Бонус - Категория человека");
            this.Deletable();
            For(x => x.Discount_ID).Display("Бонус");
            For(x => x.PersonCategory_ID).Display("Категория человека");
        }
    }
}