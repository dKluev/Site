using System;
using System.Collections.Generic;
using System.Net.Configuration;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using System.Linq;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities
{
    public class NearestGroupSet
    {
    	public string CourseTC { get; set; }

	    public bool HasMoreGroups { get; set; }

        public List<EntityWithList<DayShift, Group>> DayShiftGroups { get; set; }

	    public DateTime? EveningMinTimeFrom() {
		    var groups = DayShiftGroups.FirstOrDefault(x => x.Entity.DayShift_TC == DayShifts.Evening)
				.GetOrDefault(x => x.List) ?? new List<Group>();
			return groups.Select(x => x.TimeBeg).Where(x => x.HasValue).Min();
	    }

        public List<Group> Weekend { get; set;}

        public List<PriceView> Prices { get; set;}

        public List<Group> Webinars { get; set;}

    	public bool WebinarOnly { get; set; }

    	public bool IsEmpty {
    		get { return !All.Any(); }
    	}

	    public NearestGroupSet() {
		    Prices = new List<PriceView>();
	    }

	    public static short? HasMorningDiscount(IEnumerable<Group> groups) {
		    return groups.Where(x =>
			    CommonConst.MorningDiscount.Contains(x.Discount.GetValueOrDefault()) 
				&& DayShifts.IsAllMorningDay(x.DayShift_TC)).OrderByDescending(x => x.Discount)
				.FirstOrDefault().GetOrDefault(x => x.Discount);
	    }

	    public List<Group> All
        {
            get
            {
                var result = new List<Group>();
                result.AddRange(Weekend);
                result.AddRange(DayShiftGroups.SelectMany(sd => sd.List));
                return result.Distinct().OrderBy(g => g.DateBeg) 
                    .Take(CommonConst.NearestGroupCount).ToList();
            }
        }
    }
}