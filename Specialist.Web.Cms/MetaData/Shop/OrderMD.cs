using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.Core.FluentMetaData;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class OrderMD : BaseMetaData<Order>
    {
        public override void Init()
        {
            this.DisplayBy(x => x.OrderID);
            this.ReadOnly();
            this.NotAdd();
            this.Display("Заказ");

            For(x => x.UpdateDate).Display("Дата").Format("dd.MM.yy HH:mm:ss").ReadOnly();
            For(x => x.UserID).Display("Пользователь").ReadOnly();
            For(x => x.CustomerType).Display("Тип клиента").ForeignType(typeof(OrderCustomerType));
            For(x => x.PaymentType_TC).Display("Тип оплаты")
                .ForeignType(typeof (PaymentType));
        }
    }
}