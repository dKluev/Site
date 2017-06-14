using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class CompetitionMD: CmsMetaData<Competition>
    {
        public override void Init()
        {
            this.Display("Конкурс");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
            For(x => x.ToClose).Display("Закрывать по вводу итогов");
            For(x => x.Congratulation).Display("Поздравление").UIHint(Controls.TextArea);
            For(x => x.Course_TC).Display("Код курса");
            For(x => x.Discount).Display("Скидка %");
            For(x => x.WinnerID).Display("Победитель").ForeignType(typeof(User)).NotFilter();
//            For(x => x.WinnerName).Display("Победитель вручную");
            For(x => x.Result).Display("Итоги").UIHint(Controls.Html);
            For(x => x.OpenDate).Display("Дата начала");
            For(x => x.CloseDate).Display("Дата окончания");
            For(x => x.CourseTCList).Display("Курсы").UIHint(CommonConst.CourseTCList);
        }
    }
}