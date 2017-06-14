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
//            this.Display("Бонус");
//            this.DisplayByName();
////            this.TryAddStandartProperties();
//
//            For(x => x.Description).Display("Описание").UIHint(Controls.TextArea);
//            For(x => x.IsActive).Display("Активен");
//            For(x => x.MarketingAction_ID).Display("Акция");
//            For(x => x.PercentValue).Display("Процент");
//            For(x => x.MoneyValue).Display("Сумма");
//            For(x => x.Present_ID).Display("Подарок");
//            For(x => x.IsSummable).Display("Суммировать с остальными бонусами");
//            For(x => x.MaxPercentValue).Display("Максимальный процент");
//            For(x => x.ForGraduate).Display("Только для выпускников");
//            For(x => x.ReserveDateSpan).Display("До старта группы (дни)");
//            For(x => x.DayShift_TC).Display("Время занятий")
//                .ForeignType(typeof(DayShift));
//            For(x => x.GroupDateBegin).Display("Начало групп");
//            For(x => x.GroupDateEnd).Display("Окончание групп");
//            For(x => x.IsWeekend).Display("Выходные").UIHint(Controls.TriCheckBox);
//            For(x => x.ExcludeAuthorizationType).Display("Исключая авторизацию");
//            For(x => x.ClabCardColor_TC).Display("Карта НС").ForeignType(typeof(ClabCardColor));
//            For(x => x.PreviousOrderSum).Display("Сумма предыдущих заказов");
//            
//        }
//    }
//}