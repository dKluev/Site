//using SimpleUtils.FluentAttributes.Const;
//using SimpleUtils.FluentAttributes.Core;
//using Specialist.Entities.Context;
//using SimpleUtils.FluentAttributes.Core.Extensions;
//using Specialist.Web.Cms.MetaData.Utils;
//using Specialist.Web.Cms.Core.FluentMetaData;
//
//namespace Specialist.Web.Cms.MetaData.MarketingAction
//{
//    public class DiscountMD : CmsMetaData<Discount>
//    {
//        public override void Init()
//        {
//            this.Display("�����");
//            this.DisplayByName();
////            this.TryAddStandartProperties();
//
//            For(x => x.Description).Display("��������").UIHint(Controls.TextArea);
//            For(x => x.IsActive).Display("�������");
//            For(x => x.MarketingAction_ID).Display("�����");
//            For(x => x.PercentValue).Display("�������");
//            For(x => x.MoneyValue).Display("�����");
//            For(x => x.Present_ID).Display("�������");
//            For(x => x.IsSummable).Display("����������� � ���������� ��������");
//            For(x => x.MaxPercentValue).Display("������������ �������");
//            For(x => x.ForGraduate).Display("������ ��� �����������");
//            For(x => x.ReserveDateSpan).Display("�� ������ ������ (���)");
//            For(x => x.DayShift_TC).Display("����� �������")
//                .ForeignType(typeof(DayShift));
//            For(x => x.GroupDateBegin).Display("������ �����");
//            For(x => x.GroupDateEnd).Display("��������� �����");
//            For(x => x.IsWeekend).Display("��������").UIHint(Controls.TriCheckBox);
//            For(x => x.ExcludeAuthorizationType).Display("�������� �����������");
//            For(x => x.ClabCardColor_TC).Display("����� ��").ForeignType(typeof(ClabCardColor));
//            For(x => x.PreviousOrderSum).Display("����� ���������� �������");
//            
//        }
//    }
//}