using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Entities.Utils {
	public class PriceUtils {

/*
		public static Dictionary<string,double> DayShiftCoefficients = 
 			new Dictionary<string, double> {
 				{DayShifts.Day,0.8},
 				{DayShifts.Morning,(double) CommonConst.MorningPrice},
 				{DayShifts.MorningDay,(double) CommonConst.MorningPrice},
 				{DayShifts.Evening,0.95},
 			};
*/

		public static Tuple<decimal?, decimal?> OnePrice(decimal? price) {
			return Tuple.Create(price, price);
		}
		public static decimal? GetCoefficient(Group g) {
			if (g.Discount.HasValue) {
				return (decimal)((100.0 - g.Discount)/100.0);
			}
			return null;

			/*
			if(!g.DaySequence_TC.IsEmpty())
				return null;
			if (g.DayShift_TC.IsEmpty())
				return null;
			var coef = DayShiftCoefficients.GetValueOrDefault(g.DayShift_TC);
			if(coef > 0)
				return (decimal) coef;
			return null;
*/
		}

		public static Tuple<decimal?, decimal?> GetPriceWithCoefficient(decimal? price, 
			bool isTrack, decimal coefficient) {
			if (price.HasValue && !isTrack) {
				var newPrice = (decimal?) OrderDetail.FloorToFifty(price.Value*coefficient);
				return Tuple.Create(newPrice, newPrice);
			}
			return OnePrice(price);
		}

		public static decimal? GetPrice(List<PriceView> prices, Group group, 
			string customerType) {
			var priceType = PriceTypes.GetForGroup(group, 
				false, customerType);
			var price = prices.FirstOrDefault(x => x.PriceType_TC == priceType
				&& x.Course_TC == group.Course_TC);
			if(price == null) return null;
			return price.Price;
		}
	}
}