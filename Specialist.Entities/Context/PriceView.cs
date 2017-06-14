using Specialist.Entities.Const;
using System.Linq;

namespace Specialist.Entities.Context
{
    public partial class PriceView
    {
        public string CommonPriceTypeTC
        {
            get
            {
                if (PriceType != null)
                {
                    return PriceType.CommonPriceTypeTC;
                }
                return PriceType_TC;
            }
        }

    	public string FixedPriceType {
		    get {
				if(PriceType_TC == PriceTypes.WebinarOrg)
					return PriceType_TC + "ц";
		    	return PriceType_TC;
		    }
    	}

        private bool IsNotDayPrice
        {
            get
            {
                return PriceType_TC.EndsWith("а");
            }
          
        }

        private bool IsEveningPrice
        {
            get
            {
                return CommonPriceTypeTC.StartsWith("вкб");
            }
        }

        private bool IsMorningPrice(PriceView price)
        {
            return CommonPriceTypeTC == "вк" || CommonPriceTypeTC == "вка";
        }

        public bool IsDistance
        {
            get { return PriceTypes.IsDistance(PriceType_TC); }
        }

        public bool IsWebinar
        {
            get { return PriceTypes.Webinars.Contains(PriceType_TC); }
        }
        public bool IsIndividual
        {
            get { return PriceTypes.IsIndividual(PriceType_TC); }
        }

        public bool IsWeekend
        {
            get { return PriceTypes.IsWeekend(PriceType_TC); }
        }


        public bool IsDay
        {
            get { return PriceTypes.IsDay(PriceType_TC); }
        }

        public bool IsMain
        {
            get { return PriceTypes.IsMain(PriceType_TC); }
        }

        public byte Priority
        {
            get
            {
                if (PriceTypes.IsBusiness(PriceType_TC))
                    return 1;
                if (IsWeekend) return 10;
                if (IsDay) return 9;
                if (IsDistance) return 8;
	            if (PriceTypes.IsIntraExtra(PriceType_TC)) return 0;
                return 1;
            }
        }
    }
}