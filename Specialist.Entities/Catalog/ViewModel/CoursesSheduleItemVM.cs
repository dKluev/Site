using System;
using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Entities.ViewModel
{
    public class CoursesSheduleItemVM
    {
        public class Month
        {
            /// <summary>
            /// —писки групп разбитые по интервалам мес€ца
            /// </summary>
            public List<List<Group>> Ranges { get; set; }
            
            public Month()
            {
                Ranges = new List<List<Group>>();
            }
        }

        /// <summary>
        /// „асть дн€ проведени€ группы с набором мес€цев
        /// </summary>
        public class DayShift
        {
            public string Name { get; set; }

            public List<Month> Months { get; set; }
        }

        public Course Course { get; set; }

        public List<DayShift> DayShifts { get; set; }

    }
}