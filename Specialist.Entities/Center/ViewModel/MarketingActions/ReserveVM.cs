using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context;
using System.Linq;
using SimpleUtils.Extension;

namespace Specialist.Entities.Center.ViewModel {
    public class ReserveVM {
        public List<Discount> Discounts { get; set; }

        public List<byte> GetDayList() {
            return Discounts.Where(d => d.ReserveDateSpan.HasValue)
                .Select(d => d.ReserveDateSpan.Value).Distinct().ToList();
        }

        public byte? GetForType(string customerType, byte days) {
            var discount = Discounts.FirstOrDefault(d => d.DiscountCustomerTypes
                .Any(dct => dct.CustomerType_TC == customerType)
                    && d.ReserveDateSpan == days);
            return discount.GetOrDefault(x => x.PercentValue);
        }
    }
}