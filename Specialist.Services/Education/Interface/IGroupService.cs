using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Interface
{
    public interface IGroupService: IRepository<Group>
    {
        NearestGroupSet GetNearestGroups(Course courseTC, string cityTC);

        Tuple<Group, Group> GetNearestGroupAndWebinar(Course course, bool onlyIntraExtra, bool onlyOpenLearning);

        IQueryable<Group> GetGroupsForCourse(List<string> courseTCs, string cityTC,
                 DateTime? beginLeftDate, DateTime? beginRightDate, string dayShiftTC, string complexTC, string daySequenceTC);

        IQueryable<Group> GetGroupsForCourse(string courseTC);

        IQueryable<Group> GetGroupsForCourseByPriceType(string courseTC, string priceTypeTC, string cityTC);

        IQueryable<Group> GetAllByTrainer(string employeeTC);

        IQueryable<Group> GetPlannedAndNotBegin();

        NearestGroupSet GetNearestGroups(Complex complex);

    	IQueryable<Group> GetGroupsForCourses(IEnumerable<string> courseTCs, bool onePerCourse = false, bool isAnnounce = false);

    	Dictionary<string, int> GroupCountByTrainers();

    	List<Group> WebinarOnly();
	    List<Group> GetForUnsplitCourseTCList(string courseTCs);
    }
}