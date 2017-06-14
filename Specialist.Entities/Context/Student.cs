using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context
{
    public partial class Student
    {
         public string FullName
        {
            get
            {
                return LastName + " " + FirstName + " " + MiddleName;
            }
        }


        public string FullNameEn
        {
            get
            {
	            if (LastNameEn.IsEmpty() || FirstNameEn.IsEmpty()) {
		            return null;
	            }
	            return FirstNameEn + " " + LastNameEn;
            }
        }

	    public bool IsRealSpecialist {
		    get { return StudentCalc.GetOrDefault(x => x.ÂestClabCard_ID).HasValue; }
	    }
 
    	public StudentClabCard Card {
    		get {
				if(StudentCalc == null)
					return null;
/*
				if(StudentCalc.StudentClabCard == null) {
					var total = StudentCalc.ClabCardScore;
					var color = ClabCardColors.ColorCounts.
						OrderByDescending(x => x.Value)
						.FirstOrDefault(x => total >= x.Value).Key;
					if(color == null)
						return null;
					return new StudentClabCard{ClabCardColor_TC = 
					color};
				}
*/
					
    			return StudentCalc.GetOrDefault(c => c.StudentClabCard);
    		}
    	}
		 public IEnumerable<Group> CourseGroups
        {
            get
            {
                return GetPaidGroups()
                    .Where(sig =>
						
						!CourseTC.AllSpecialWithoutHalfAndSeminar.Contains(sig.Group.Course_TC)).Select(sig => sig.Group);
            }
        }

    	public IOrderedEnumerable<StudentInGroup> GetPaidGroups() {
    		return StudentInGroups
    			.Where(sig => sig.Group != null
					&& sig.Group.Course_TC != CourseTC.ZaochSpec
    				&& BerthTypes.AllPaid.Contains(sig.BerthType_TC))
    			.OrderByDescending(sig => sig.Group.DateBeg);
    	}

		public List<Group> GetAllCourseGroups() {
    		return StudentInGroups
    			.Where(sig => sig.Group != null && !sig.Group.IsExam
				&& !BerthTypes.Hide.Contains(sig.BerthType_TC) && sig.Group.Course_TC != CourseTC.ZaochSpec).Select(x => x.Group)
    			.OrderByDescending(g => g.DateBeg).ToList();
    	}

    	public List<Group> CurrentGroups
        {
            get
            {
                return CourseGroups.Where(g => g.DateEnd >= DateTime.Today
					&& g.DateBeg <= DateTime.Today).ToList();
            }
        }

		public List<Group> ComingGroups
        {
            get
            {
                return GetAllCourseGroups().Where(g => g.DateBeg > DateTime.Today 
					|| (g.IsLightBlue && !g.Course_TC.Contains(CourseTC.ArchiveBlue)))
					.Where(g => !g.IsNotVisible).OrderBy(x => x.DateBeg).ToList();
            }
        }

        public List<Group> EndedGroups
        {
            get
            {
                return CourseGroups.Where(g => g.IsFinished).ToList();
            }
        }

        public IEnumerable<StudentInGroup> Exams
        {
            get
            {
                return GetPaidGroups()
                    .Where(sig => sig.Group.IsExam);
            }
        }
    }
}