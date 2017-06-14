using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class OrderCustomerTypeMD : CmsMetaData<OrderCustomerType>
    {
        public override void Init()
        {
            this.Display("Тип клиента");
            this.DisplayByName();
            this.ReadOnly();
        }
    }
}