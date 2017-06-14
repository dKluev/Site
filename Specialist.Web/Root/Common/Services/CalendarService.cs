using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Utils;

namespace Specialist.Web.Root.Common.Services {
	public class CalendarService {
	    public static HashSet<DateTime> DayOffList() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => {
			    var start = DateTime.Today.AddYears(-3);
			    var end = DateTime.Today.AddYears(3);
				var context = new SpecialistDataContext();
			    var workDays = _.List('В', 'П');
			    return new HashSet<DateTime>(context.GetTable<Calendar>().Where(x =>
				    x.DateValue >= start && x.DateValue <= end && workDays.Contains(x.WorkDay))
				    .Select(x => x.DateValue).ToList());
		    });
	    } 

	}
}