using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Lms;

namespace Specialist.Web.Root.Lms {
	public class TimesheetVM: IViewModel {
		public List<LmsLecture> Lectures { get; set; }

		public DateTime DateBegin { get; set; }

		public string Title {
			get { return "Табель"; }
		}

		public List<List<DateTime>> Weeks { get; set; }
		public List<EmployeesAbsence> Absences { get; set; }
		public Dictionary<decimal, GroupsCalc> Groups { get; set; }
		public string Notes { get; set; }
		public Dictionary<decimal, string> GroupDates { get; set; }
		public HashSet<DateTime> DayOffList { get; set; }
	}
}