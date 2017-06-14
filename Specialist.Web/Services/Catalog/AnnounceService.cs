using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Utils;
using Specialist.Entities.Announcement;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using SimpleUtils;
using Specialist.Web.Common.Utils;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Services
{
    public class AnnounceService: IAnnounceService
    {
        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public IGroupVMService GroupVMService { get; set; }

		[Dependency]
        public ISectionService SectionService { get; set; }

		[Dependency]
        public ICourseService CourseService { get; set; }

		[Dependency]
        public CourseVMService CourseVMService { get; set; }

		[Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        public IQueryable<Announce> GetAllFor(object obj)
        {
			var groups = GetHotGroupByRelations(obj);
			if(!groups.Any() && obj is Product) {
				var section = SiteObjectService.GetByRelationObject<Section>(obj).FirstOrDefault();
				if(section != null)
					groups = GetHotGroupByRelations(section);
			}
            return GetAnnounces(groups);
        }

        private IQueryable<Group> GetHotGroupByRelations(object entity) {
            var courseTCList = CourseService.GetCourseTCListFor(entity.GetType(),
				_.List(LinqToSqlUtils.GetPK(entity)));
            return GetHotGroupsForCourses(courseTCList);
        }


        /*public IQueryable<Group> GetHotGroups(Exam exam) {
            var courseTCList = exam.ExamCourses.Select(ec => ec.Course_TC).ToList();
            return GetHotGroupsForCourses(courseTCList);
        }*/

        private IQueryable<Group> GetHotGroupsForCourses(IEnumerable<string> courseTCs) {
        	var cityTC = Cities.Moscow;// UserSettingsService.CityTC;
            return GroupService.GetGroupsForCourses(courseTCs, true)
					.ByCity(cityTC).Where(g => g.Discount.HasValue && g.DateBeg <= DateTime.Today.AddMonths(1)).OrderByDescending(g => g.ShowOnNearestGroups).ThenBy(x => x.DateBeg);
        }

    	private List<Announce> GetAnnouncesForCourses(IEnumerable<string> courseTCs, 
			List<decimal> excludeGroupIds = null) {
    		excludeGroupIds = excludeGroupIds ?? new List<decimal>();
    		var groups = GroupService.GetGroupsForCourses(courseTCs, true, true).ToList();
			if (excludeGroupIds.Any()) {
				groups = groups.Where(x => !excludeGroupIds.Contains(x.Group_ID)).ToList();
			}
    		if(!groups.Any())
	    		groups = GroupService.GetGroupsForCourses(courseTCs, true)
				.Where(g => g.DateBeg < DateTime.Today.AddMonths(3)).ToList();

    		return GetAnnounces(groups.OrderByDescending(x => x.IsBlazing)
				.ThenBy(x => x.IsBlazing ? null : x.DateBeg)
				.ThenByDescending(x => PriceService.CoursePriceIndex()
					.GetValueOrDefault(x.Course_TC)).Take(20).AsQueryable()).ToList();
    	}

       
		public List<Announce> GetAllForMainCached() {
			return CacheUtils.Get(MethodBase.GetCurrentMethod(), () => {
				 var groups = GroupVMService.GetAllForMain();
				return GetAnnounces(groups.AsQueryable()).ToList();
			}
			);
		}

        private IQueryable<Announce> GetAnnounces(IQueryable<Group> groups) {
            return 
                from gr in groups
                group gr by gr.Course into grouped
                select 
                    new Announce
                    {
                        Course = grouped.Key, 
                        AnnounceGroups = grouped.ToList()
                    };
        }

		public List<Announce> GetAllForSection(int sectionId) {

			var courseTCs = CourseVMService.GetCourseTCListForTotalSection(sectionId);
			var result = FilterCourses(sectionId, courseTCs);
			return GetAnnouncesForCourses(result.Item1,result.Item2);
		}

	    private Tuple<List<string>,List<decimal>> FilterCourses(int sectionId, List<string> courseTCs) {
			var notAnnounce = SectionService.NotAnnounce()
				.GetValueOrDefault(sectionId) ?? new List<string>();
		    var groupIds = StringUtils.IntList(notAnnounce).Select(x => (decimal) x).ToList();
		    if (!groupIds.Any()) {
			    courseTCs = courseTCs.Except(notAnnounce).ToList();
		    }
		    return Tuple.Create(courseTCs,groupIds);
	    }


	    public List<Announce> GetHotGroupsForSection(Section section) {
			var sections = _.List(section).AddFluent(
				SectionService.GetChildren(section.Section_ID));
			var announces = new List<Announce>();
			foreach (var subSection in sections) {
				if (announces.Count >= CommonConst.AnnounceCount) break;
				announces.AddRange(GetAllFor(subSection)
					.Take(CommonConst.AnnounceCount - announces.Count));
			}
			return announces;
		}

		public List<Announce> GetAllForEntity(Type type, object pk) {
			var courseTCs = CourseService.GetCourseTCListFor(type, _.List(pk));
			var isMicrosoft = type == typeof (Vendor) && (int) pk == Vendor.Microsoft;
			if (isMicrosoft) {
				var result = FilterCourses(Sections.Network, courseTCs);
				return GetAnnouncesForCourses(result.Item1,result.Item2);
			}
			return GetAnnouncesForCourses(courseTCs);
		}
    }
}