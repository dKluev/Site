using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class OrgResponseMD : CmsMetaData<OrgResponse>
    {
        public override void Init()
        {
            this.Display("Отзыв организации");
            this.TryAddStandartProperties();
            For(x => x.ShortDescription).Display("Аннотация").UIHint(Controls.TextArea);
            For(x => x.OriginalImg).Display("Скан оригинала");
            For(x => x.Authors).Display("Авторы");
            For(x => x.OrganizationID).Display("Организация");
            For(x => x.Employee_TC).Display("Коды препод.");
            For(x => x.Course_TC).Display("Код курса");
        }
    }
}