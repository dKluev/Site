using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class CityMD : CmsMetaData<City>
    {
        public override void Init()
        {
            this.DisplayBy(x => x.CityName); 
        }
    }
}