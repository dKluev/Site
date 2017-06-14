using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class NewsMD : CmsMetaData<News>
    {
        public override void Init()
        {
            this.Display("Новость");
            this.DisplayBy(x => x.Title);
            For(x => x.Title).Display("Заголовок");
            For(x => x.ShortDescription).Display("Аннотация").UIHint(Controls.TextArea);
            For(x => x.PublishDate).Display("Дата новости");
            For(x => x.ForMainPage).Display("На главной");
			For(x => x.IsHot).Display("Важная");
            For(x => x.MainPageDateEnd).Display("Дата на главной");
            For(x => x.Type).Display("Раздел").ForeignType(typeof(NewsType));
            For(x => x.SmallImage).Display("Иконка");
            For(x => x.PriorityOrder).Display("Сорт.");
	        For(x => x.ShowEverywhere).Display("Везде");
            For(x => x.CourseTCList).Display("Курсы").UIHint(CommonConst.CourseTCList);
            this.TryAddStandartProperties(true);
 
        }
    }
}