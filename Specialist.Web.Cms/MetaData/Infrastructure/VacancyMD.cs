using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class VacancyMD: CmsMetaData<Vacancy>
    {
        public override void Init()
        {
            this.Deletable();
            this.Display("Вакансия");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();

            For(x => x.Type).Display("Тип").ForeignType(typeof(CenterVacancyType));
            For(x => x.PublishDate).Display("Дата публикации");
            For(x => x.IsHot).Display("Горячая вакансия");
            For(x => x.IsPartner).Display("Партнера");
            For(x => x.CourseTCList).Display("Курсы").UIHint(CommonConst.CourseTCList);
            For(x => x.Logo).Display("Лого");
        }
    }
}