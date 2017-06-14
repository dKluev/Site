using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Infrastructure
{
    public class MailTemplateMD: CmsMetaData<MailTemplate>
    {
        public override void Init()
        {
            this.Display("Шаблон письма");
            this.NotAdd();
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
        }
    }
}