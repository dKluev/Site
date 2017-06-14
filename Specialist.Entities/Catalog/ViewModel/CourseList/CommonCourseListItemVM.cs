using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.FluentAttributes.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using GenericExtension = SimpleUtils.Common.Extensions.GenericExtension;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.ViewModel
{
    public class CommonCourseListItemVM: CourseListItem
    {
		public bool IsIntraExtraTrackPage { get; set; }
    	public Group NearestWebinar { get; set; }


		public decimal? GetTrackFullPrice(string priceType) {
			if (TrackFullPrices == null)
				return null;
			return TrackFullPrices.GetValueOrDefault(priceType);
		}
	    public Dictionary<string, decimal?> TrackFullPrices { get; set; }

	    public Tuple<decimal?, decimal?> DistancePrice {
		    get {
			    var price = GetPriceByType(GetDistancePriceType());
			    if (PriceCoefficent == null)
					return PriceUtils.OnePrice(price);
			    return PriceUtils.GetPriceWithCoefficient(price, Course.IsTrackBool,
				    PriceCoefficent.Value);
		    }
	    }


	    public decimal? DistanceOrgPrice { get {
        	return GetPriceByType(GetDistanceOrgPriceType());
        }
        }


	    public string GetDistancePriceType() {
		    return IsIntraExtraTrackPage ? PriceTypes.IntraExtra : PriceTypes.Webinar;
	    }

	    public string GetDistanceOrgPriceType() {
		    return IsIntraExtraTrackPage ? PriceTypes.IntraExtraOrg : PriceTypes.WebinarOrg;
	    }


    	private decimal? GetPriceByType(string priceTypeTC) {
    		return Prices.FirstOrDefault(
    			p => p.PriceType_TC == priceTypeTC)
    			.GetOrDefault(p => (decimal?)p.Price);
    	}


        public Tuple<decimal?, decimal?> MinFulltimePrice
        {
            get
            {
				var price = Prices.Where(
                	p => PriceTypes.FulltimePerson.Contains(p.CommonPriceTypeTC) && p.Price > 0)
                	.AsQueryable().SelectMin(p => p.Price)
					.GetOrDefault(p => (decimal?)p.Price);
				if(PriceCoefficent == null || IsTrackPage)
					return PriceUtils.OnePrice(price);
				return PriceUtils.GetPriceWithCoefficient(price, Course.IsTrackBool,
					PriceCoefficent.Value);


            }
        }

        public decimal? MinOrgPrice
        {
            get
            {
                return Prices.Where(
                	p => PriceTypes.GetFulltimeOrg().Contains(p.CommonPriceTypeTC) && p.Price > 0)
                	.AsQueryable().SelectMin(p => p.Price)
					.GetOrDefault(p => (decimal?)p.Price);
            }
        }

    	public bool HasNearestGroupOrWebinar	 {
    		get {
    			return 
    				(NearestGroup != null || NearestWebinar != null);
    		}
    	}

	    public bool IsTrackPage { get; set; }
    }
}