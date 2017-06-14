using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Shop;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.SiteEntities
{
    public class VendorMD : CmsMetaData<Vendor>
    {
        public override void Init()
        {
            this.Display("Вендор");
            this.ReadOnly();
            this.DisplayByName();
            this.AddName();
            this.TryAddStandartProperties();
        }
    }
}