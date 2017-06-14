using System;

namespace Specialist.Entities.Lms {
	    public class LmsLecture {
		    public decimal Lecture_ID { get; set; }
		    public DateTime LectureDateBeg { get; set; }
		    public DateTime LectureDateEnd { get; set; }
		    public decimal Group_ID { get; set; }
		    public string Course_TC { get; set; }
		    public short Breaks { get; set; }
		    public string ClassRoom_TC { get; set; }
			public DateTime GroupDateBeg { get; set; }
			public string Complex { get; set; }
		    public string Complex_TC { get; set; }
		    public string LectureType_TC { get; set; }
	    }
}