using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;

namespace Specialist.Entities.Utils {
	public static class DateUtils {
		public static DateTime LastMonthDay(DateTime date) {
			return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
		}
		public static DateTime FirstMonthDay(DateTime date) {
			return new DateTime(date.Year, date.Month, 1);
		}

		public static int WeekCount(DateTime date) {
			return WeekIndex(LastMonthDay(date)) - WeekIndex(FirstMonthDay(date)) + 1;
		}

		public static int WeekIndex(DateTime date) {
			var cal = DateTimeFormatInfo.CurrentInfo.Calendar;
			return cal.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
		}
		public static bool IsWeekend(DateTime date) {
			return date.DayOfWeek.In(DayOfWeek.Saturday, DayOfWeek.Sunday);
		}

		public static List<DateTime> Range(DateTime begin, DateTime end) {
			return Enumerable.Range(0, (int) (end - begin).TotalDays)
				.Select(x => begin.AddDays(x)).ToList();
		}
		public static DateTime Min(DateTime date1, DateTime date2) {
			return date1 < date2 ? date1 : date2;
		}

		public static DateTime Max(DateTime date1, DateTime date2) {
			return date1 > date2 ? date1 : date2;
		}

		public static List<List<DateTime>> GetMonthWeeks(DateTime month) {
			var first = FirstMonthDay(month);
			var monday = StartOfWeek(first);
			var weekCount = WeekCount(first);
			return Enumerable.Range(0, weekCount).Select(x =>
				Enumerable.Range(0, 7).Select(y => monday.AddDays(x*7 + y)).ToList()).ToList();
		}
		public static List<List<DateTime>> GetMonthWeeksByTwo(DateTime month) {
			var first = FirstMonthDay(month);
			var monday = StartOfWeek(first);
			var weekCount = WeekCount(first);
			weekCount = weekCount/2 + weekCount%2;
			var dayCount = 14;
			return Enumerable.Range(0, weekCount).Select(x =>
				Enumerable.Range(0, dayCount).Select(y => monday.AddDays(x*dayCount + y)).ToList()).ToList();
		}
		public static DateTime StartOfWeek(DateTime dt) {
			int diff = dt.DayOfWeek - DayOfWeek.Monday;
			if (diff < 0) {
				diff += 7;
			}
			return dt.AddDays(-1 * diff).Date;
		}
		public static DateTime EndOfWeek(DateTime dt) {
			return StartOfWeek(dt).AddDays(6);
		}
		public static List<DateTime> GetDays(DateTime monthDate) {
			var month = monthDate.Month;
			var year = monthDate.Year;
			return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
							 .Select(day => new DateTime(year, month, day))
							 .ToList();
		}

		public static bool IsWorkTime() {
			var now = DateTime.Now;
			return now.Hour >= CommonConst.ConsultantBegin && now.Hour < CommonConst.ConsultantEnd
					&& !now.DayOfWeek.In(DayOfWeek.Saturday, DayOfWeek.Sunday);
		}

	}
}