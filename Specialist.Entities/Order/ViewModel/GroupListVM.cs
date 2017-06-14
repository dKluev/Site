using System.Collections.Generic;
using SimpleUtils.FluentAttributes.Utils;
using SimpleUtils.Util;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context.ViewModel
{
    public class GroupListVM
    {
        public class MonthTab
        {
            public string Name { get; set; }

            public string Id {get;set;}

            public IEnumerable<Group> Groups { get; set; }

            public bool IsActive { get; set; }

            public MonthTab(string name, string id, IEnumerable<Group> groups)
            {
                Name = name;
                Id = id;
                Groups = groups;

            }
        }

    	public bool IsWebinars { get; set; }


    	public bool HidePrice { get { return IsWebinars || OrderDetail.IsTrack; } }
        public List<Group> Groups { get; set; }

        public string CityTC { get; set; }

        public OrderDetail OrderDetail { get; set; }

        public bool IsBusiness { get; set; }

    	public List<PriceView> Prices { get; set; } 

		public decimal? GetPrice(Group group) {
			return PriceUtils.GetPrice(Prices, group, OrderDetail.Order.CustomerType);
		}

        public List<MonthTab> GetTabs
        {
            get
            {
                var tempGroups = 
                    from gr in Groups
                    let date = gr.DateBeg.Value
                    group gr by new {date.Year, date.Month} into grouped
                    select grouped;
                var result = new List<MonthTab>();

                var first  = true;
                var hasActive = false;
                foreach (var grouped in tempGroups.Take(2))
                {
                    var monthTab = new MonthTab(MonthUtil.GetName(grouped.Key.Month),
                        "Month" + grouped.Key.Month, grouped);
                    result.Add(monthTab); 

                    if(!hasActive)
                    {
                        if(!OrderDetail.Group_ID.HasValue)
                        {
                            monthTab.IsActive = first ;
                        }
                        else
                        {
                            monthTab.IsActive = grouped.Any(
                                g => g.Group_ID == OrderDetail.Group_ID);
                        }
                        hasActive = monthTab.IsActive;
                        first = false;
                    }

                }
                var allTab = new MonthTab("Полное расписание", "All", Groups);
                allTab.IsActive = !hasActive;
                result.Add(allTab);
                return result;
            }
        }


        public List<decimal> MorningDayGroupIDs
        {
            get
            {
                return Groups.Where(g => g.DayShift_TC == DayShifts.MorningDay).
                    Select(g => g.Group_ID).ToList();
            }
        }

	    public bool ShowType { get; set; }
    }
}