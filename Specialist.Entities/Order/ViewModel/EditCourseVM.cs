using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using System.Linq;
using SimpleUtils;

namespace Specialist.Entities.Context.ViewModel
{
    public class EditCourseVM
    {
    	public string CityTC { get; set; }

        public List<City> Cities { get; set; }

        public List<PriceView> Prices { get; set; }

        public string PriceTypeTC { get; set; }

        public bool IsBusiness { get; set; }

        public bool IsTrack {get { return OrderDetail != null && OrderDetail.Track_TC != null; }}

        public bool IsTrackCourse { get; set; }

        public Dictionary<string,char> PrefixList { get; set; }

        public EditCourseVM() {
        	Prices = new List<PriceView>();
        }

        public EditCourseVM(Dictionary<string, char> prefixList) {
            PrefixList = prefixList;
        }

        public GroupListVM GetGroupListVM(string cityTC, bool withWebinars = false, bool onlyIntraExtra = false) {
        	var groups = GetGroups(cityTC);
	        if (!withWebinars) {
		        groups = groups.Where(x => !x.IsWebinarOnly).ToList();
		        groups = groups.Where(x => onlyIntraExtra ? x.IsIntraExtramural : !x.IsIntraExtramural).ToList();
	        } 

        	return
                new GroupListVM
                {
                    CityTC = cityTC,
					IsWebinars = withWebinars,
                    OrderDetail = OrderDetail,
					Prices = Prices,
                    Groups = groups,
                    IsBusiness = IsBusiness,
					ShowType = !(withWebinars || onlyIntraExtra)
                };
        }

    	public GroupListVM GetGroupListVMForWebinar(string cityTC)
        {
            var model = GetGroupListVM(cityTC,true);
            model.Groups = model.Groups.Where(g => g.WebinarExists).ToList();
            return model;
        }

	    public bool HasIntraExtra {
		    get { return Prices.Any(x => PriceTypes.IsIntraExtra(x.PriceType_TC)) 
				&& Groups.Any(x =>x.IsIntraExtramural); }
	    }

        public List<PriceView> DistancePrices
        {
            get
            {
                return Prices.Where(p => PriceTypes.IsDistanceOrWebinar(p.PriceType_TC))
                    .OrderBy(p => p.Price).ToList();   
            }
        }

    	public bool DistanceOnlyWebinar {
    		get { return true; }
    	}

        public List<decimal> MorningDayGroupIDs
        {
            get
            {
	            if (OrderDetail.IsOrg) {
	                return Groups.Where(g => g.DayShift_TC == DayShifts.MorningDay).
	                    Select(g => g.Group_ID).ToList();
	            }
				return new List<decimal>();
            }
        }

        public List<string> IndividualPriceTypeTCList
        {
            get
            {
                return Prices.Where(p => p.IsIndividual).Select(p => p.PriceType_TC)
                    .ToList();
            }
        }

        public List<Group> Groups {get;set;}

        public List<Group> GetGroups(string cityTC)
        {
            return Groups.Where(g => g.BranchOffice.TrueCity_TC == cityTC).ToList();
        }

        public bool HasIndividualPrice()
        {
            return Prices.Any(p => p.IsIndividual);
        }

        public bool HasGroupPrice() {
            return Prices.Any(p => p.IsDay);
        }


        public List<PriceView> GetPrices()
        {
            var query = Prices.Where(p => PriceTypes.Current.Contains(p.PriceType_TC));
	        if (!OrderDetail.IsOrg) {
		        query = query.Where(x => !PriceTypes.IsBusiness(x.PriceType_TC));
	        }
	        return query.ToList();
        }

        public OrderDetail OrderDetail {get;set;}

/*
        public bool IsDistance { get {
            return CityTC == Entities.Const.Cities.Distance;
        } }
*/
    }
}