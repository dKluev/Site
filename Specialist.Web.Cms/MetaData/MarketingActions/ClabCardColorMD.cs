using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Entities.Passport;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class ClabCardColorMD : CmsMetaData<ClabCardColor>
    {
        public override void Init()
        {
            this.Display("Карта НС");
            this.DisplayByName();
            this.ReadOnly();
        }
    }
}