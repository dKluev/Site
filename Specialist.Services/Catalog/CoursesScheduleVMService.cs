/*using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Services.Interface;
using Specialist.Services.Interface.ViewModel;

namespace Specialist.Services.ViewModel
{
    public class CoursesScheduleVMService: ICoursesScheduleVMService
    {
        [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        public CoursesScheduleVM GetForTrack(string trackTC)
        {
            var courses = CourseService.GetAllForTrack(trackTC);

            return new CoursesScheduleVM
                   {
                       Courses = Create(courses.ToList()),
                       Months = GetMonths()
                   };
        }
        
        public CoursesScheduleVM GetForCertification(int certificationID)
        {
            var courses = CourseService.GetAllForCertification(certificationID);

            return new CoursesScheduleVM
                   {
                       Courses = Create(courses.ToList()),
                       Months = GetMonths()
                   };
        }

        private List<CoursesSheduleItemVM> Create(List<Course> courses)
        {
            var cityTC = UserSettingsService.CityTC;
            var today = DateTime.Today;
            var courseSheduleItems = new List<CoursesSheduleItemVM>();
            foreach (var course in courses)
            {

                var groups = 
                    GroupService.GetGroupsForCourse(course.UrlName, cityTC, today, 
                       new DateTime(today.Year, today.Month + MonthCount, 1)).ToList();
                courseSheduleItems.Add(Create(course, groups));
                

            }
            return courseSheduleItems;
        }

        private List<CoursesScheduleVM.Month> GetMonths()
        {
            var today = DateTime.Today;
            var months = new List<CoursesScheduleVM.Month>();
            for (int i = 0; i < MonthCount; i++)
            {
                var date = today.AddMonths(i);
                months.Add(
                    new CoursesScheduleVM.Month
                    {
                        Name = MonthUtil.GetName(date.Month),
                        DayCount = DateTime.DaysInMonth(date.Year, date.Month)
                    });
            }
            return months;
        }

        private const int MonthCount = 3;

        private CoursesSheduleItemVM Create(Course course, 
                                           List<Group> groups)
        {

            var groupByDayShiftList =
                from gr in groups
                group gr by gr.DayShift_TC into grouped
                                               orderby grouped.Key
                                               select grouped;
            var dayShifts = new List<CoursesSheduleItemVM.DayShift>();
            foreach (var groupByDayShift in groupByDayShiftList)
            {
                var months = new List<CoursesSheduleItemVM.Month>();
                var currentMonth = DateTime.Now.Month;
                var currentYear = DateTime.Now.Year;
                var monthIndex = currentMonth;
                var year = currentYear;
                for (var i = 0; i < MonthCount; i++)
                {
                    var month = new CoursesSheduleItemVM.Month();
                    for (var j = 0; j < 3; j++)
                    {
                        var groupsInRange = new List<Group>();
                        var leftDate = new DateTime(year, monthIndex, j * 10 + 1);

                        var rightDate = j == 2 ?
                                                   new DateTime(year, monthIndex, 1 ).AddMonths(1) : 
                                                                                                       new DateTime(year, monthIndex, (j + 1) * 10);

                        foreach (var group in groupByDayShift)
                        {
                            if(!group.DateBeg.HasValue)
                                continue;
                            if(leftDate <= group.DateBeg.Value 
                               && group.DateBeg < rightDate)
                                groupsInRange.Add(group);
                        }
                        month.Ranges.Add(groupsInRange);

                    }
                    months.Add(month);
                    monthIndex++;
                    if(monthIndex > 12)
                    {
                        monthIndex = 1;
                        year++;
                    }
                }
                dayShifts.Add(
                    new CoursesSheduleItemVM.DayShift
                    {
                        Months = months,
                        Name = groupByDayShift.Key
                    }
                    );
            }
            return
                new CoursesSheduleItemVM
                {
                    Course = course,
                    DayShifts = dayShifts
                };

        }
           
    }

    
}*/