using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class CertificationMD : CmsMetaData<Certification>
    {
        public override void Init()
        {
            this.Display("Сертификация");
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
        }
    }
}