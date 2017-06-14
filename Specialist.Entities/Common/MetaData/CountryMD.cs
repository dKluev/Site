using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Core;
using Specialist.Entities.Context;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Entities.Common.MetaData
{
    public class CountryMD : BaseMetaData<Country>
    {
        public CountryMD()
        {
            this.DisplayByName();
        }
    }
}