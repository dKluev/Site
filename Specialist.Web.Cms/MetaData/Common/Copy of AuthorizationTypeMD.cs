using FluentMetaData.Core;
using Specialist.Entities.Context;
using FluentMetaData.Core.Extensions;
using Specialist.Web.Cms.MetaData.Utils;

namespace Specialist.Web.Cms.MetaData.Shop
{
    public class CityMD : BaseMetaData<City>
    {
        public override void Init()
        {
            this.DisplayBy(x => x.CityName); 
        }
    }
}