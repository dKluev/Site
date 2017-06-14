using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.ViewModel
{
    public class TrackDiscount
    {
        public Course Track { get; set; }

        public decimal DiscountPrice { get; set; }

        public decimal Price { get; set; }

	    public decimal Saving {
		    get {
			    return Price - DiscountPrice;
		    }
	    }

    }
}