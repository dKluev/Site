using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Filter;
using SimpleUtils;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Util {
	public static class GroupFilterExtension {
		/*  public static List<SelectListItem> GetLeftMonthList(this GroupFilter groupFilter)
		  {
            
			  var months = Month.GetAllMoreThen(DateTime.Now.Month);
			  if(groupFilter.LeftMonth.HasValue)
				  foreach (var month in months)
				  {
					  month.Selected = month.Value == groupFilter.LeftMonth.Value.ToString();
				  }
			  return months;
		  }

		  public static List<SelectListItem> GetRightMonthList(this GroupFilter groupFilter)
		  {

			  var months = Month.GetAllMoreThen(1);
			  if (groupFilter.RightMonth.HasValue)
				  foreach (var month in months)
				  {
					  month.Selected = month.Value == groupFilter.RightMonth.Value.ToString();
				  }
			  return months;
		  }
  */
		public static string GetDayShiftDescription(this GroupFilter groupFilter, string tc) {
			var dayShift = groupFilter.DayShifts.FirstOrDefault(ds => ds.DayShift_TC == tc);
			if (dayShift == null)
				return null;

			if (dayShift.TimeFrom.Hour == 0)
				return dayShift.Name;
			return string.Format("{0} ({1:HH:mm} - {2:HH:mm})", dayShift.Name,
				dayShift.TimeFrom, dayShift.TimeTo);
		}

		public static List<SelectListItem> GetStudyTypes(this GroupFilter groupFilter) {
			return GroupFilter.StudyTypes.Select(x => new SelectListItem {
				Selected = x.Key == groupFilter.StudyTypeId,
				Value = x.Key.ToString(),
				Text = x.Value
			}).ToList();
		}

		public static List<SelectListItem> GetDayShiftList(this GroupFilter groupFilter) {

			var dayShifts = groupFilter.DayShifts;
			var result = new List<SelectListItem>();
			result.Add(
						new SelectListItem {
							Selected = groupFilter.DayShiftTC.IsEmpty(),
							Value = string.Empty,
							Text = "Любое"

						});
			//            if (!groupFilter.DayShiftTC.IsNullOrEmpty())
			foreach (var dayShift in dayShifts) {
				result.Add(
					new SelectListItem {
						Selected = dayShift.DayShift_TC == groupFilter.DayShiftTC,
						Value = dayShift.DayShift_TC,
						Text = groupFilter.GetDayShiftDescription(
							dayShift.DayShift_TC)
					});
			}
			return result;
		}

		public static List<SelectListItem> GetDaySequences(this GroupFilter groupFilter) {

			var dayShifts = groupFilter.DayShifts;
			var result = new List<SelectListItem>();
			result.Add(
						new SelectListItem {
							Value = string.Empty,
							Text = "Все равно"

						});
			result.Add(new SelectListItem {
				Value = DaySequences.Saturday,
				Text = DaySequences.GetName(DaySequences.Saturday)
			});

			result.Add(new SelectListItem {
				Value = DaySequences.Sunday,
				Text = DaySequences.GetName(DaySequences.Sunday)
			});

			foreach (var daySequence in result) {
				daySequence.Selected = groupFilter.DaySequenceTC ==
					daySequence.Value;
			}
			return result;
		}
		public static List<SelectListItem> GetComplexList(this GroupFilter groupFilter) {

			var compelexes = groupFilter.Complexes;
			var result = new List<SelectListItem>();
			result.Add(
						new SelectListItem {
							Selected = groupFilter.DayShiftTC.IsEmpty(),
							Value = string.Empty,
							Text = "Любой"

						});
			foreach (var complex in compelexes) {
				result.Add(
					new SelectListItem {
						Selected = complex.Complex_TC == groupFilter.ComplexTC,
						Value = complex.Complex_TC,
						Text = complex.Name + complex.Metro.GetOrDefault(x => " (" + x + ")"),
					});
			}
			return result;
		}



		public static string GetCurrentDayShiftDescription(this GroupFilter groupFilter) {
			return groupFilter.GetDayShiftDescription(groupFilter.DayShiftTC);
		}

		public static Complex GetCurrentComplex(this GroupFilter groupFilter) {
			return groupFilter.Complexes.FirstOrDefault(c => c.Complex_TC ==
				groupFilter.ComplexTC);
		}

		public static Section GetCurrentSection(this GroupFilter groupFilter) {
			return groupFilter.Sections.Union(groupFilter.Sections.SelectMany(x => x.SubSections))
				.FirstOrDefault(c => c.Section_ID == groupFilter.SectionId.Value)
				;
		}

		public static Employee GetTrainer(this GroupFilter groupFilter) {
			return groupFilter.Employees.FirstOrDefault(c => c.Employee_TC == groupFilter.EmployeeTC);
		}
	}
}