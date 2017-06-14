using Specialist.Entities.Const;
using Specialist.Entities.Education.ViewModel;

namespace Specialist.Entities.Context {
	public partial class StudentInGroupLecture {
		 
            public Attendance Attendance
            {
                get
                {
                    if (Truancy.GetValueOrDefault())
                        return Attendance.Truancy;
                    if (Lateness.HasValue)
                        return Attendance.Lateness;
                    if (Departure.HasValue)
                        return Attendance.Departure;
                    return Attendance.None;
                }
            }
	}
}