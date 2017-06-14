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
            /// ������ ����� �������� �� ���������� ������
            /// </summary>
            public List<List<Group>> Ranges { get; set; }
            
            public Month()
            {
                Ranges = new List<List<Group>>();
            }
        }

        /// <summary>
        /// ����� ��� ���������� ������ � ������� �������
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