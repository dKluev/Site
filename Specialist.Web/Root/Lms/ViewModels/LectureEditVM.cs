using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Education;
using Specialist.Entities.Lms;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Root.Lms {
	public class LectureEditVM: IViewModel {
		public List<PiStudentInGroupLecture> Students { get; set; }
		public bool WebinarExists { get; set; }
		public Ratings Rating { get;set; }

		public class Ratings {
			public string Current { get; set; }
			public string Max { get; set; }
		}

		public class StudentInfo {
			public String FullName { get; set; }
			public String Email { get; set; }
			public String PriceTypeTC { get; set; }
			public String CourseTC { get; set; }
			public decimal StudentID { get; set; }
			public string FinalExamMark_TC { get; set; }
			public bool HasPhoto { get; set; }
			public string BerthTypeTC { get; set; }

			public StudentInfo(List<string> fullName, string email, string priceTypeTc, string courseTc, decimal studentId, string finalExamMark_TC, bool hasPhoto, string berthTypeTc) {
				FullName = fullName.Select(x => ClearnHtml(x)).JoinWith("&nbsp;");
				Email = email;
				PriceTypeTC = priceTypeTc;
				CourseTC = courseTc;
				StudentID = studentId;
				FinalExamMark_TC = finalExamMark_TC;
				HasPhoto = hasPhoto;
				BerthTypeTC = berthTypeTc;
			}

			private static string ClearnHtml(string x) {
				return StringUtils.OnlyLetters(x);
			}

			public bool IsWebinar {
				get { return PriceTypes.IsWebinar(PriceTypeTC) || IsUnlimit; }
			}
			public bool IsUnlimit {
				get { return BerthTypeTC == BerthTypes.Unlimit || PriceTypeTC == PriceTypes.Unlimited; }
			}
		}
		public Dictionary<decimal, StudentInfo> StudentInfos { get; set; }
		

		public string Title {
			get {
				return "Занятие {0} {1} {2} {3} а.ч. {4}"
					.FormatWith(Group.Course_TC + 
					Group.GetSpecialPostfix(Group.LectureType_TC,Group.Complex_TC), 
					Lecture.LectureDateBeg.Date.DefaultString(), 
					Lecture.TimeInterval, 
					(int)Group.Hours,
					Group.Complex_TC
					);
			} 
		}

		public Lecture Lecture { get; set; }
		public LectureEditStatus LectureEditStatus { get; set; }
		public bool LastLecture { get; set; }
		public bool FirstLecture { get; set; }

		public bool ShowUnlimit {
			get {
				return LastLecture || FirstLecture; 
			}
		}
		public Group Group { get; set; }
		public LectureFile LectureFile { get; set; }
		public List<CourseFileVM> SpecFiles { get; set; }

		public bool IsDpCons {
			get {
				return Group != null && Group.Course_TC == CourseTC.DpCons; 
			}
		}
	}
}