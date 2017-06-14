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
            this.Display("����� �����");

            For(x => x.OrderID).Display("�����").ReadOnly();
            For(x => x.Course_TC).Display("����").ReadOnly();
            For(x => x.Count).Display("����������");
            For(x => x.CreateDate).Display("���� ��������").ReadOnly();
            For(x => x.Track_TC).Display("����").ReadOnly();
            For(x => x.MoneyDiscount).Display("������(���)");
            For(x => x.PercentDiscount).Display("������(%)");
            For(x => x.PriceType_TC).Display("��� ����").UIHint(Controls.Text);
            For(x => x.Price).Display("����");
            For(x => x.Group_ID).Display("������").Display(Controls.Text).ReadOnly();
        }
    }
}