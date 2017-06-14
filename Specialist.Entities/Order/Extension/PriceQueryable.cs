using System.Linq;
using Specialist.Entities.Const;
using Specialist.Entities.Context;

namespace Specialist.Services.Order.Extension
{
    public static class PriceQueryable
    {
        public static PriceView GetDefault(this IQueryable<PriceView> prices)
        {
            return prices.OrderByDescending(p => p.Priority).FirstOrDefault();
        }

    }
}