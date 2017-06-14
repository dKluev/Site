using System.Collections.Generic;

namespace Specialist.Entities.ViewModel
{
    public class CoursesScheduleVM
    {
        public class Month
        {
            public string Name { get; set; }

            /// <summary>
            /// Количество дней в месяце
            /// </summary>
            public int DayCount { get; set; }
        }

        public List<Month> Months { get; set; }
        public List<CoursesSheduleItemVM> Courses { get; set; }

    }
}