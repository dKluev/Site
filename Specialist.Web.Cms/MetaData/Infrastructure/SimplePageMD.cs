using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class SimplePageMD : CmsMetaData<SimplePage>
    {
        public override void Init()
        {
            this.Display("Страница");
            this.DisplayBy(x => x.Title);
            For(x => x.Title).Display("Заголовок");

            this.TryAddStandartProperties();
            For(x => x.LinkText).Display("Текст ссылки");
            For(x => x.WithoutLink).Display("Без ссылки");
            For(x => x.CourseTCList).Display("Курсы").UIHint(CommonConst.CourseTCList);
            For(x => x.UseTabs).Display("Табы");
            For(x => x.UseAltTabs).Display("Синии табы");
            

            For(x => x.SysName);
        }
    }
}