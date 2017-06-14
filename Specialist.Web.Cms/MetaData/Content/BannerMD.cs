using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class BannerMD : CmsMetaData<Banner>
    {
        public override void Init()
        {
            this.Display("Баннер");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();

            For(x => x.Image).Display("Картинка");
            For(x => x.TargetUrl).Display("Целевая страница");
            For(x => x.Pages).Display("Страницы размещения").UIHint(Controls.TextArea)
				.SetAttribute(new FullLengthStringDisplayAttribute());


            For(x => x.DateBegin).Display("Дата начала");
            For(x => x.ActualDate).Display("Дата окончания");
            For(x => x.Priority).Display("Приоритет");
            For(x => x.IsSide).Display("Боковой");

        }
    }
}