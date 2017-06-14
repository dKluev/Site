using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Utils;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.Context {
    public partial class MarketingAction {
        public Discount FirstDiscount {
            get {
                return Discounts.FirstOrDefault(x => x.IsActive);
            }
        }

		public string GetShortDescription() {
			return ShortDescription ??
				StringUtils.GetFirstParagraph(Description);
		}

        public string DateInterval {
            get {
                if (!DateBegin.HasValue || !DateEnd.HasValue)
                    return null;
                var result = new List<string>();
                if (DateBegin.HasValue && !(DateBegin.Value < DateTime.Today.AddMonths(-1)))
                    result.Add(DateBegin.Value.ToShortDateString());
                if(DateEnd.HasValue)
                    result.Add(DateEnd.Value.ToShortDateString());
                return result.JoinWith(" - ");
            }
        }

    	public const string WithWebinar = "[WithWebinar]";

		public List<string> CourseTCSplitList {
            get { return StringUtils.SafeSplit(CourseTCList); }
        }


        public byte? MaxPercent {
            get {
                return Discounts.Max(d => d.PercentValue);
            }
        }

        public string DiscountInterval {
            get {
                var activeDiscounts = Discounts.Where(d => d.IsActive);
                if(!activeDiscounts.Any())
                    return string.Empty;
                if (activeDiscounts.Count() == 1)
                    return FirstDiscount.DiscountText;
                var discountWithPercent =
                    activeDiscounts.Where(d => d.PercentValue.HasValue);
                var max = discountWithPercent.Max(x => x.PercentValue);
                var min = discountWithPercent.Min(x => x.PercentValue);
                if(min == max)
                    return max.Value.ToString();
                return min.Value + "-" + max.Value + "%";


            }
        }
    }
}