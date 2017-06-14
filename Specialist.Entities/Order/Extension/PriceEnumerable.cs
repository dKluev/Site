using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Const;
using Specialist.Entities.Context;

namespace Specialist.Entities.Context.Extension
{
    public static class PriceEnumerable
    {
        public static decimal? GetPrice(this IEnumerable<PriceView> prices,
            string priceTypeTC)
        {
            return prices.GetOrDefault(pl =>
               pl.FirstOrDefault(pv => pv.CommonPriceTypeTC == priceTypeTC))
               .GetOrDefault(p => (decimal?)p.Price);

        }

		public static bool HasFullTimePrice(this IEnumerable<PriceView> prices) {
    		var fullTimeTypes = PriceTypes.GetFulltime();
    		return prices.Any(p => fullTimeTypes.Contains(p.CommonPriceTypeTC));
    	}
    }
}