using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using SimpleUtils;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.UnityInterception;
using SimpleUtils.Extension;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Utils;

namespace Specialist.Services
{
    public class GroupService: Repository<Group>, IGroupService
    {
        public GroupService() 
            : base(new ContextProvider()) {}

        [Dependency]
        public IDayShiftService DayShiftService { get; set; }


        public NearestGroupSet GetNearestGroups(Course course, string cityTC)
        {
            var courseTC = course.Course_TC;
            var isRestrictedSchedule = course.IsRestrictedSchedule;
        	var groupsForCourse = GetGroupsForCourse(courseTC).ToList();
			
        	var webinarOnlyGroups = WebinarOnly().Where(x => x.Course_TC == courseTC).ToList();
        	groupsForCourse = groupsForCourse.Concat(webinarOnlyGroups)
        		.OrderBy(x => x.DateBeg).ToList();
        	var webinars = groupsForCourse.Where(g => g.WebinarExists).ToList();
        	webinars = webinars.OrderBy(x => x.DateBeg).ToList();
        	var webinarOnly = !groupsForCourse.Any();
			if(webinarOnly) {
				groupsForCourse = webinars.ToList();
				webinars = new List<Group>();
			}
        	var groupSet = GetNearestGroups(groupsForCourse, isRestrictedSchedule);
        	groupSet.Webinars = webinars.Take(CommonConst.NearestGroupCount).ToList();
        	groupSet.WebinarOnly = webinarOnly;
        	groupSet.CourseTC = courseTC;
	        groupSet.HasMoreGroups = groupsForCourse.Count > CommonConst.NearestGroupCount;
        	return groupSet;
        }

       

        public NearestGroupSet GetNearestGroups(Complex complex) {
            
            var groups = GetPlannedAndNotBegin().
                Where(g => g.Complex_TC == complex.Complex_TC).NotSpecial().ToList();
            return GetNearestGroups(groups, false);
        }

        private NearestGroupSet GetNearestGroups(List<Group> groups, 
			bool isRestrictedSchedule) {
            var weekend =
                from gr in groups
                where gr.DaySequence_TC != null
                select gr;

            var dayShifts = (
                from gr in groups
                where gr.DayShift_TC != null
                group gr by gr.DayShift_TC into grouped
                    select EntityWithList.New(
                        grouped.First().DayShift, 
                        grouped.OrderBy(g => g.DateBeg).Take(CommonConst.NearestGroupCount))
                ).ToList();

            if(!isRestrictedSchedule)
                foreach (var dayShift in DayShiftService.GetCurrent())
                    if (dayShifts.All(ds => ds.Entity.DayShift_TC != dayShift.DayShift_TC))
                        dayShifts.Add(EntityWithList.New(dayShift, new List<Group>()));
                


            return 
                new NearestGroupSet
                {
                    DayShiftGroups = dayShifts,
                    Weekend = weekend.Take(CommonConst.NearestGroupCount).ToList()
                };
        }

        public IQueryable<Group> GetAllByTrainer(string employeeTC)
        {
            return GetPlannedAndNotBegin().NotSpecial().Where(g => g.Teacher_TC == employeeTC);
        }
        
        public Tuple<Group, Group> GetNearestGroupAndWebinar(Course course, 
			bool onlyIntraExtra, bool onlyOpenLearning)
        {
	        var groupsForCourse = GetGroupsForCourse(course.CourseTCOrFirst).OrderBy(x => x.DateBeg).ToList();
        	var	group = groupsForCourse.FirstOrDefault();
	        Group secondGroups = null;
	        if (onlyOpenLearning) {
		        secondGroups = groupsForCourse.FirstOrDefault(x => x.IsOpenLearning);
	        } else {
		        var allGroups = groupsForCourse.Concat(WebinarOnly()
					.Where(g => g.Course_TC == course.Course_TC)).OrderBy(x => x.DateBeg).ToList();
		        if (onlyIntraExtra) {
			        secondGroups = allGroups.FirstOrDefault(x => x.IsIntraExtramural);
		        } else {
			        secondGroups = allGroups.FirstOrDefault(x => x.WebinarExists);
		        }
	        }

            return Tuple.Create(group,secondGroups);
        }

        public IQueryable<Group> GetGroupsForCourse(string courseTC)
        {
			if(courseTC == null)
				return new List<Group>().AsQueryable();
            return
                (GetGroupsForCourses().GetValueOrDefault(courseTC) ?? new List<Group>())
                    .AsQueryable();
        }

        public IQueryable<Group> GetGroupsForCourses(IEnumerable<string> courseTCs, bool onePerCourse = false, bool isAnnounce = false)
        {
			if(onePerCourse) {
	            return courseTCs.Select(s => {
	            	var groupsForCourse = GetGroupsForCourse(s);
					if(isAnnounce)
		            	return groupsForCourse.OrderByDescending(x => x.IsBlazing)
							.FirstOrDefault(x => x.PrintAnnounce);
	            	return groupsForCourse.FirstOrDefault();
	            }).Where(x => x != null).AsQueryable();
			}
            return courseTCs.SelectMany(GetGroupsForCourse).AsQueryable();
        }
        [Cached]
        public virtual Dictionary<string, List<Group>> GetGroupsForCourses() {
        	var groupsForCourses = GetPlannedAndNotBegin().GroupBy(x => x.Course_TC)
        		.ToDictionary(x => x.Key, x => x.ToList());
        	foreach (var halfTrack in CourseTC.HalfTracks) {
        		if(!groupsForCourses.ContainsKey(halfTrack.Key)) {
        			groupsForCourses.Add(halfTrack.Key, 
						groupsForCourses.GetValueOrDefault(halfTrack.Value.First()) ?? new List<Group>());
        		}
        	}
        	return groupsForCourses;
        }

        [Cached]
		public virtual Dictionary<string, int> GroupCountByTrainers() {
        	return GetPlannedAndNotBegin().Where(x => !x.Teacher_TC.IsEmpty())
				.GroupBy(x => x.Teacher_TC)
        		.ToDictionary(x => x.Key, x => x.Count());
		} 

/*
        [Cached]
		public virtual Dictionary<string, Dictionary<string, int>> 
			GroupCountByCourseTrainers() {
        	return GetPlannedAndNotBegin().Where(x => !x.Teacher_TC.IsEmpty())
				.GroupBy(x => x.Course_TC)
        		.ToDictionary(x => x.Key, x => x.GroupBy(y => y.Teacher_TC)
					.ToDictionary(z => z.Key,z => z.Count()));
		} 
*/

    	public IQueryable<Group> GetGroupsForCourseByPriceType(string courseTC, string priceTypeTC, string cityTC)
        {
            var isDistance = PriceTypes.IsDistance(priceTypeTC);
            if(isDistance)
                return new List<Group>().AsQueryable();
            var groups = GetGroupsForCourse(courseTC);
    		if (PriceTypes.IsWebinar(priceTypeTC)) {
    			return groups.Where(x => x.WebinarExists);
    		}
			if (PriceTypes.IsIntraExtra(priceTypeTC)) {
				return groups.Where(x => x.IsIntraExtramural);
			}
            var isBusiness = PriceTypes.IsBusiness(priceTypeTC);
            if(isBusiness)
                groups = groups.Where(g => g.DayShift_TC == DayShifts.MorningDay);
            groups = groups.Where(g => g.BranchOffice.TrueCity_TC == cityTC);
            return groups;
        }



		[Cached]
        public virtual List<Group> WebinarOnly() {
                var localContext = new SpecialistDataContext();
                var loadOptions = GetDataLoadOptions();
        	localContext.LoadOptions = loadOptions;
			var groups = localContext.Groups.OrderBy(g => g.DateBeg)
				.WebinarOnly()
				.NotSeminars()
				.ToList();
			foreach (var @group in groups) {
				@group.IsWebinarOnly = true;
			}
			UpdateIntraExtraDateBegin(groups);
			return groups;
		}


#if(!DEBUG)
        [Cached]
#endif
        public virtual IQueryable<Group> GetPlannedAndNotBegin()
        {
/*#if(DEBUG)
            return new List<Group>().AsQueryable();
#endif*/
         /*   using()
            {*/
	        var groups = new List<Group>();
                var localContext = new SpecialistDataContext();
#if(!DEBUG)
                var loadOptions = GetDataLoadOptions();
        	localContext.LoadOptions = loadOptions;
        	groups = localContext.Groups.OrderBy(g => g.DateBeg)
				.PlannedAndNotBegin().NotSeminars().ToList();
        	foreach (var gr in groups) {
				gr.IsWebinarOnly = false;
        		if (gr.Group_ID == 176681) {
        			gr.WebinarExists = false;
        		}
        	}
#endif
#if(DEBUG)
            groups = localContext.Groups.OrderBy(g => g.DateBeg)
                .Where(g => CourseTC.TestTC.Contains(g.Course_TC))
                .PlannedAndNotBegin()
				.NotSeminars()
                .ToList();
#endif
//            }
        
			UpdateIntraExtraDateBegin(groups);
        	return groups.AsQueryable();
        }

    	private static DataLoadOptions GetDataLoadOptions() {
    		var loadOptions = new DataLoadOptions();
    		loadOptions.LoadWith<Group>(g => g.DayShift);
			loadOptions.LoadWith<Group>(g => g.GroupCalc);
    		loadOptions.LoadWith<Group>(g => g.BranchOffice);
    		loadOptions.LoadWith<Group>(g => g.Teacher);
    		loadOptions.LoadWith<Group>(g => g.Complex);
    		loadOptions.LoadWith<Group>(g => g.Course);
    		loadOptions.LoadWith<BranchOffice>(bo => bo.City);
    		return loadOptions;
    	}


        public IQueryable<Group> GetGroupsForCourse(List<string> courseTCs, string cityTC,
         DateTime? beginLeftDate, DateTime? beginRightDate, string dayShiftTC, 
            string complexTC, string daySequenceTC) {
        	var groups = courseTCs.Any()
        		? GetGroupsForCourses(courseTCs)
        		: GetPlannedAndNotBegin();
            
          
            groups =
             from gr in groups
             where cityTC == null || gr.BranchOffice.TrueCity_TC == cityTC
             select gr;

            if (!dayShiftTC.IsEmpty())
                groups = groups.Where(g => g.DayShift_TC == dayShiftTC);
            if (!daySequenceTC.IsEmpty())
                groups = groups.Where(g => g.DaySequence_TC == daySequenceTC);
            if (!complexTC.IsEmpty())
                groups = groups.Where(g => g.Complex_TC == complexTC);

	        groups = groups.ByDateBegin(beginLeftDate, beginRightDate);


            return groups.OrderBy(g => g.DateBeg);
        }

		public List<Group> GetForUnsplitCourseTCList(string courseTCs) {
			courseTCs = courseTCs.GetOrDefault(x => x.ToUpper());
			var groupIds = new HashSet<decimal>(StringUtils.IntListSplit(courseTCs)
				.Select(x => (decimal)x));
			if (groupIds.Any())
				return GetPlannedAndNotBegin().Where(x => groupIds.Contains(x.Group_ID))
					.Take(100).ToList();
			var courseTCList = StringUtils.SafeSplit(courseTCs);
			return courseTCList.Select(tc => GetGroupsForCourse(tc)
                    .FirstOrDefault()).Where(g => g != null).Take(100).ToList();
		}

	    public Group GetProbOz() {
		    var dateEnd = DateTime.Today.AddDays(2);
		    var group = this.FirstOrDefault(x => x.Course_TC == CourseTC.ProbOz
			    && DateTime.Today <= x.DateBeg && x.DateBeg <= dateEnd);
		    return group;
	    }

	    public void UpdateIntraExtraDateBegin(List<Group> groups) {
		    var probGroup = GetProbOz();
		    if (probGroup != null) {
			    var dateBeg = DateUtils.StartOfWeek(DateTime.Today).AddDays(7);
			    var dateEnd = dateBeg.AddDays(6);
			    foreach (var group in groups.Where(x => x.IsIntraExtramural 
					&&  dateBeg <= x.DateBeg && x.DateBeg <= dateEnd)) {
				    group.DateBeg = probGroup.DateBeg;
				    group.ProbOz = probGroup;
			    }
		    }
	    }
    }
}
