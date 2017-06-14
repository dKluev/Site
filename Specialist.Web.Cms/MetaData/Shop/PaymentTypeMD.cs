using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class PaymentTypeMD : CmsMetaData<PaymentType>
    {
        public override void Init()
        {
            this.Display("Тип оплаты");
            this.DisplayByName();
            this.ReadOnly();
        }
    }
}