using System;

namespace Specialist.Entities.Lms {
	    public class LmsStudentNote {
		    public string Notes { get; set; }
		    public string LastChanger_TC { get; set; }
			public decimal Student_ID { get; set; }
		    public DateTime LastChangeDate { get; set; }
		    public decimal StudentInGroup_ID { get; set; }
	    }
}