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
                               {1, "Январь"},
                               {2, "Февраль"},
                               {3, "Март"},
                               {4, "Апрель"},
                               {5, "Май"},
                               {6, "Июнь"},
                               {7, "Июль"},
                               {8, "Август"},
                               {9, "Сентябрь"},
                               {10, "Октябрь"},
                               {11, "Ноябрь"},
                               {12, "Декабрь"}

                           };
      
        static Dictionary<int, string> otherMonthNames =
            new Dictionary<int, string>
                           {
                               {1, "января"},
                               {2, "февраля"},
                               {3, "марта"},
                               {4, "апреля"},
                               {5, "мая"},
                               {6, "июня"},
                               {7, "июля"},
                               {8, "августа"},
                               {9, "сентября"},
                               {10, "октября"},
                               {11, "ноября"},
                               {12, "декабря"}
                           };

        public static Dictionary<DayOfWeek, string> DayNames =
            new Dictionary<DayOfWeek, string>
                           {
                               {DayOfWeek.Monday, "Понедельник"},
                               {DayOfWeek.Tuesday, "Вторник"},
                               {DayOfWeek.Wednesday, "Среда"},
                               {DayOfWeek.Thursday, "Четверг"},
                               {DayOfWeek.Friday, "Пятница"},
                               {DayOfWeek.Saturday, "Суббота"},
                               {DayOfWeek.Sunday, "Воскресенье"},
                           };
      
        public static Dictionary<DayOfWeek, string> DayNamesShort =
            new Dictionary<DayOfWeek, string>
                           {
                               {DayOfWeek.Monday, "Пн"},
                               {DayOfWeek.Tuesday, "Вт"},
                               {DayOfWeek.Wednesday, "Ср"},
                               {DayOfWeek.Thursday, "Чт"},
                               {DayOfWeek.Friday, "Пт"},
                               {DayOfWeek.Saturday, "Сб"},
                               {DayOfWeek.Sunday, "Вс"},
                           };

        public static string GetName(int monthIndex, bool other = false) {
        	return other ? otherMonthNames[monthIndex] : monthNames[monthIndex];
        }
    }
}