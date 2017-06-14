using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Lms;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Education {
	public class LectureService: Repository2<Lecture> {
		public LectureService(IContextProvider contextProvider) : base(contextProvider) {}

	    public List<LmsLecture> GetLmsLectures(DateTime dateBegin, DateTime dateEnd, string employeeTC) {
		    var lectures = GetLectures(x =>
			    x.Teacher_TC == employeeTC
				&& x.LectureDateBeg >= dateBegin
				&& x.LectureDateBeg <= dateEnd).ToList();
		    return lectures;
	    }


	    public List<LmsLecture> GetLectures(Expression<Func<Lecture, bool>> lectureSelector) {
		    var lmsLectures = this.GetAll(lectureSelector)
			    .Select(x => new LmsLecture {Lecture_ID = x.Lecture_ID, 
				    LectureDateBeg = x.LectureDateBeg, 
				    LectureDateEnd = x.LectureDateEnd, 
				    Group_ID = x.Group_ID, 
				    Course_TC = x.Group.Course_TC, 
				    LectureType_TC = x.Group.LectureType_TC, 
				    Complex_TC = x.Group.Complex_TC, 
				    GroupDateBeg = x.Group.DateBeg.Value,
				    Complex = x.Group.Complex_TC,
				    Breaks = x.Breaks, 
				    ClassRoom_TC = x.ClassRoom_TC})
			    .OrderBy(x => x.LectureDateBeg).ToList();
			lmsLectures.ForEach(x => x.Course_TC = x.Course_TC + Group.GetSpecialPostfix(x.LectureType_TC,x.Complex_TC));
		    return lmsLectures;
	    }
	}
}