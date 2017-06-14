using System.Collections.Generic;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Catalog.ViewModel {
    public class NearestGroupsVM {
        public List<Group> Groups { get; set; }

	    public List<PriceView> Prices { get; set; } 

        public bool HideCourse { get; set; }

        public bool ShowAllDiscountLink { get; set; }

        public bool HideTrainer { get; set; }

        public bool HideComplex { get; set; }

    	public bool HideCart { get; set; }

    	public int ColNumber { get; set; }

	    public bool WebinarTab { get; set; }

		public bool HideDiscount { get; set; }

    	public bool ShowDiscount {
    		get { return Groups.Any(x => x.Discount.HasValue) &&!HideDiscount; }
    	}

    	public bool IsTest { get; set; }

    	public bool HideRows { get; set; }

		public string PdfUrl { get; set; }

    	public bool ShowPrice { get {
    		return Prices.Any(); } }

		public decimal? GetPrice(Group group) {
			return PriceUtils.GetPrice(Prices, group, OrderCustomerType.PrivatePerson);
		}

        public NearestGroupsVM(List<Group> groups, bool hideCourse = false) {
            Groups = groups;
            HideCourse = hideCourse;
			Prices = new List<PriceView>();
        }
    }
}