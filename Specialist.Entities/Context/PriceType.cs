using SimpleUtils.Common.Extensions;
using Specialist.Entities.Const;
using SimpleUtils.Extension;

namespace Specialist.Entities.Context
{
    public partial class PriceType
    {
        public string Name
        {
            get
            {
                return PriceTypes.GetShortName(CommonPriceTypeTC);
            }
        }

        public string CommonPriceTypeTC { get { return GetCommonType(); } }

        public string GetCommonType() {
            if (! PriceListType_TC.In(PriceListTypes.Distance, PriceListTypes.Common))
            {
                return PriceType_TC.Remove(0, 1);
            }
            return PriceType_TC;
        }
    }
}