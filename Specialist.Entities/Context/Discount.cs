using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Specialist.Entities.Context
{
    public partial class Discount
    {
        public string Name
        {
            get
            {
                var result = string.Empty;
                if (MarketingAction != null)
                    result += MarketingAction.Name + " - ";
                if (PercentValue.HasValue)
                    result += PercentValue.Value + "%";
                else if (MoneyValue.HasValue)
                    result += MoneyValue.Value + "руб.";
                if(Present != null)
                {
                    if (result != string.Empty)
                        result += " +";
                    result += Present.Name;
                }
                return result;
            }
        }

		private EntityRef<ClabCardColor> _ClabCardColor = default(EntityRef<ClabCardColor>);
        [Association(Storage = "_ClabCardColor", ThisKey = "ClabCardColor_TC",
            OtherKey = "ClabCardColor_TC")]
        public ClabCardColor ClabCardColor {
            get { return _ClabCardColor.Entity; }
            set { _ClabCardColor.Entity = value; }
        } 

        public string DiscountText {
            get {
                var result = string.Empty;
                if (PercentValue.HasValue)
                    result += (IsSummable ? "+" : "") + PercentValue + "% ";

                if (MoneyValue.HasValue)
                    result += MoneyValue.Value + " руб.";
                return result;
            }
        }
    }
}