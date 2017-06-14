using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.MarketingActions
{
    public class MarketingActionMD : CmsMetaData<Specialist.Entities.Context.MarketingAction>
    {
        public override void Init()
        {
            this.Display("Маркетинговая акция");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
            For(x => x.ShortDescription).Display("Аннотация").UIHint(Controls.Html);
            For(x => x.DateBegin).Display("Дата начала");
            For(x => x.DateEnd).Display("Дата окончания");
            For(x => x.Type).Display("Тип").ForeignType(typeof(MarketingActionType));
            For(x => x.IsAdvert).Display("Рекламная");
			For(x => x.IsOrg).Display("Орг.");
			For(x => x.IsSecret).Display("Секрет");
            For(x => x.CourseTCList).Display("Курсы").UIHint(CommonConst.CourseTCList);
            For(x => x.ShowOnCoursePages).Display("Страница курсов");
            For(x => x.IsSpecialOffer).Display("Спец.");
        }
    }
}