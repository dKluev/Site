using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class SiteObjectMD : CmsMetaData<SiteObject>
    {
        public override void Init()
        {
            this.Display("Объект сайта");
            this.DisplayByName();
            this.AddName();
            For(x => x.Type).Display("Type").ForeignType(typeof(SiteObjectType));
        }
    }
}