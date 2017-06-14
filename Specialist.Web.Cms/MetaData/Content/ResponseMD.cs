using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Center;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.Content
{
    public class ResponseMD : CmsMetaData<Response>
    {
        public override void Init()
        {
            this.Display("Отзыв");
            this.Deletable();
            this.DisplayBy(x => x.Description);
            For(x => x.Description).Display("Текст").UIHint(Controls.Html);
            For(x => x.Rating).Display("Рейтинг").ForeignType(typeof(ResponseRating));
            For(x => x.Authors).Display("Автор");
            For(x => x.Course_TC).Display("Код курса").UIHint(Controls.Text);
            For(x => x.Employee_TC).Display("Код преподавателя").UIHint(Controls.Text);
            For(x => x.IsActive).Display("Активен");
            For(x => x.IsWebinar).Display("Вебинар");
            For(x => x.IsIntraExtra).Display("ОЗ");
            For(x => x.IsDiplom).Display("ДП");
            For(x => x.Type).Display("Тип").ForeignType(typeof(RawQuestionnaireType));
            For(x => x.Complex_TC).Display("Комплекс").ForeignType(typeof(Complex));
        }
    }
}