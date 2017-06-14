using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class UserWorkMD : CmsMetaData<UserWork>
    {
        public override void Init()
        {
            this.Display("Работа слушателя");
            this.TryAddStandartProperties();
	        this.DisplayBy(x => x.FullName);

            For(x => x.FullName).Display("ФИО");
            For(x => x.Section_ID).Display("Направление");
            For(x => x.Course_TC).Display("Курс").UIHint(Controls.Text);
            For(x => x.WorkSectionID).Display("Раздел");
            For(x => x.SmallImage).Display("Иконка");
            For(x => x.Trainer_TC).Display("Препод.").UIHint(Controls.Text);
        }
    }
}