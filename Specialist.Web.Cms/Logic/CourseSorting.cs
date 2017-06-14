using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;
using SimpleUtils.Extension;

namespace Specialist.Web.Cms.Logic
{
    public class CourseSorting
    {
        List<Course> _courses;
        private readonly List<string> _changedTCList;
        private readonly List<string> _allTCList;

        public CourseSorting(List<Course> courses,
            List<string> changedTCList,
            List<string> allTCList)
        {
            _courses = courses;
            _changedTCList = changedTCList;
            _allTCList = allTCList;

        
        }

        public void Update()
        {
            var listWithChanaged =
           _allTCList.Select(x => new
           {
               courseTC = x,
               IsChanged =
                   _changedTCList.Any(y => y == x)
           }).ToList();

            string previous = null;
            foreach (var tcWithChanged in listWithChanaged)
            {
                if (tcWithChanged.IsChanged)
                    UpdateSortOrder(tcWithChanged.courseTC, previous,
                        previous != null ? null : listWithChanaged
                            .First(x => !x.IsChanged).courseTC);
                previous = tcWithChanged.courseTC;
            }
        }

        public void UpdateSortOrder(string courseTC, string previousTC, string nextTC)
        {
            var current = _courses.First(c => c.Course_TC == courseTC);
            if (previousTC == null)
            {
                var next = _courses.First(c => c.Course_TC == nextTC);
                SetNewSortOrder(current, next.WebSortOrder);
                return;
            }

            var previous = _courses.First(c => c.Course_TC == previousTC);
            SetNewSortOrder(current, previous.WebSortOrder + 1);
        }

        private void SetNewSortOrder(Course course, int sortOrder)
        {
            var coursesInPlace = _courses.Where(c => c.WebSortOrder == sortOrder);
            if (coursesInPlace.Any())
            {
                var i = 1;
                foreach (var courseInPlace in coursesInPlace)
                {
                    SetNewSortOrder(courseInPlace, sortOrder + i);
                    i++;
                }
            }
            course.WebSortOrder = sortOrder;
        }
    }
}