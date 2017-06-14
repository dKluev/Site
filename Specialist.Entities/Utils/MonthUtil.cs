using System;
using System.Collections.Generic;

namespace SimpleUtils.Util
{
    public static class MonthUtil
    {
		public static int GetPrevious() {
			var now = DateTime.Today;
			return new DateTime(now.Year, now.Month, 1).AddMonths(-1).Month;
		}
        static Dictionary<int, string> monthNames =
            new Dictionary<int, string>
                           {
                               {1, "������"},
                               {2, "�������"},
                               {3, "����"},
                               {4, "������"},
                               {5, "���"},
                               {6, "����"},
                               {7, "����"},
                               {8, "������"},
                               {9, "��������"},
                               {10, "�������"},
                               {11, "������"},
                               {12, "�������"}

                           };
      
        static Dictionary<int, string> otherMonthNames =
            new Dictionary<int, string>
                           {
                               {1, "������"},
                               {2, "�������"},
                               {3, "�����"},
                               {4, "������"},
                               {5, "���"},
                               {6, "����"},
                               {7, "����"},
                               {8, "�������"},
                               {9, "��������"},
                               {10, "�������"},
                               {11, "������"},
                               {12, "�������"}
                           };

        public static Dictionary<DayOfWeek, string> DayNames =
            new Dictionary<DayOfWeek, string>
                           {
                               {DayOfWeek.Monday, "�����������"},
                               {DayOfWeek.Tuesday, "�������"},
                               {DayOfWeek.Wednesday, "�����"},
                               {DayOfWeek.Thursday, "�������"},
                               {DayOfWeek.Friday, "�������"},
                               {DayOfWeek.Saturday, "�������"},
                               {DayOfWeek.Sunday, "�����������"},
                           };
      
        public static Dictionary<DayOfWeek, string> DayNamesShort =
            new Dictionary<DayOfWeek, string>
                           {
                               {DayOfWeek.Monday, "��"},
                               {DayOfWeek.Tuesday, "��"},
                               {DayOfWeek.Wednesday, "��"},
                               {DayOfWeek.Thursday, "��"},
                               {DayOfWeek.Friday, "��"},
                               {DayOfWeek.Saturday, "��"},
                               {DayOfWeek.Sunday, "��"},
                           };

        public static string GetName(int monthIndex, bool other = false) {
        	return other ? otherMonthNames[monthIndex] : monthNames[monthIndex];
        }
    }
}