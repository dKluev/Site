using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Context;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class OrderDetailMD : CmsMetaData<OrderDetail>
    {
        public override void Init()
        {
            this.DisplayBy(x => x.OrderDetailID);
            this.Display("Заказ курса");

            For(x => x.OrderID).Display("Заказ").ReadOnly();
            For(x => x.Course_TC).Display("Курс").ReadOnly();
            For(x => x.Count).Display("Количество");
            For(x => x.CreateDate).Display("Дата создания").ReadOnly();
            For(x => x.Track_TC).Display("Трек").ReadOnly();
            For(x => x.MoneyDiscount).Display("Скидка(руб)");
            For(x => x.PercentDiscount).Display("Скидка(%)");
            For(x => x.PriceType_TC).Display("Тип цены").UIHint(Controls.Text);
            For(x => x.Price).Display("Цена");
            For(x => x.Group_ID).Display("Группа").Display(Controls.Text).ReadOnly();
        }
    }
}