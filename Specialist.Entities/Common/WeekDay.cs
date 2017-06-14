using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Specialist.Entities.Common
{
    public class WeekDay
    {
        public DayOfWeek DayOfWeek { get; set; }

        public WeekDay(DayOfWeek dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }

        public string Name { get { return GetName(DayOfWeek); } }

        public static List<WeekDay> GetAll()
        {
            return Enum.GetValues(typeof (DayOfWeek)).Cast<DayOfWeek>().Select(dow => 
                new WeekDay(dow)).ToList();
        }

        private static string GetName(DayOfWeek dayOfWeek)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)dayOfWeek];
        }
    }
}