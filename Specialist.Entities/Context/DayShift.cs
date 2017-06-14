using System;
using Specialist.Entities.Const;

namespace Specialist.Entities.Context
{
    public partial class DayShift
    {
        public byte SortOrder
        {
            get
            {
                switch (DayShift_TC)
                {
                    case DayShifts.Morning: return 1;
                    case DayShifts.Day: return 2;
                    case DayShifts.MorningDay: return 3;
                    default: return 4;
                }
            }
        }

        public string GetTimeInterval(DateTime? eveningTime) {
	        var timeFrom = DayShift_TC == DayShifts.Evening && eveningTime.HasValue ? eveningTime.Value : TimeFrom;
			return timeFrom.ToShortTimeString() + "-" + TimeTo.ToShortTimeString();
        }
        
    }
}