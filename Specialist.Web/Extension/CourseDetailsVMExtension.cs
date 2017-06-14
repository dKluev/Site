using System.Collections.Generic;
using SimpleUtils.Common;
using SimpleUtils.Util;
using Specialist.Entities;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using System.Linq;

namespace Specialist.Web.Extension
{
    using SectionGroups = Tuple<string, List<Group>>;
    public static class CourseDetailsVMExtension
    {
        public static List<SectionGroups>
            GetAllSectionGroups(this NearestGroupSet nearestGroupSet)
        {
            var result = new List<SectionGroups>();
            result.Add(Tuple.New("sectionall", nearestGroupSet.All));
            result.AddRange(nearestGroupSet.DayShiftGroups
                .Select(ds => Tuple.New("section" + ds.Entity.DayShift_TC, ds.List )));
            result.Add(Tuple.New("sectionweekend", nearestGroupSet.Weekend));
			if(nearestGroupSet.Webinars.Any())
	            result.Add(Tuple.New("sectionwebinars", nearestGroupSet.Webinars));
            return result;
        }
    }
}