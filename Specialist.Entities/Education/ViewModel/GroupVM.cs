using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Profile.ViewModel;
using System.Linq;
using SimpleUtils;
using Specialist.Entities.Tests;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Education.ViewModel {

	public class GroupVM : IViewModel {

		public class ProgressVM {
			public int CourseCurrentHours { get; set; }
			public int CourseTotalHours { get; set; }

			public int Course {
				get { return CourseTotalHours > 0 ? (100*CourseCurrentHours)/CourseTotalHours : 0; }
			}
			public int Track { get; set; }
			public string TrackName { get; set; }
		}

		public class LectureVM {
			public Lecture Lecture {
				get;
				set;
			}

			public string WebinarUrl {
				get {
					return Lecture.WebinarURL;
				}
			}

			public List<CourseContent> Contents {
				get;
				set;
			}

			public StudentInGroupLecture StudentLecture {
				get;
				set;
			}

			public string DateTimeInterval {
				get {
					return Lecture.LectureDateBeg.Date.DefaultString() + " " +
						Lecture.LectureDateBeg.ToShortTimeString() + " - " +
						Lecture.LectureDateEnd.ToShortTimeString();
				}
			}

			public Attendance Attendance {
				get {
					if (StudentLecture == null)
						return Attendance.None;
					return StudentLecture.Attendance;
				}
			}
		}

		public Group Group {
			get;
			set;
		}

		public List<Test> Tests { get; set; }
		public List<Test> TestsAfterComplete { get; set; }

		public List<LectureVM> Lectures {
			get;
			set;
		}

		public List<UserMessage> LastMessages {
			get;
			set;
		}

		public bool ShowWebinarUrl {
			get {
				return StudentInGroup != null &&
					BerthTypes.AllPaidForCourses.Contains(StudentInGroup.BerthType_TC)
					&& PriceTypes.IsWebinar(StudentInGroup.PriceType_TC);
			}
		}

		public int GetNextEmptyContentLectureCount(LectureVM lectureVM) {
			if (lectureVM.Contents.IsEmpty())
				return 0;
			return Lectures.SkipWhile(l => l != lectureVM).Skip(1)
				.TakeWhile(l => l.Contents.IsEmpty()).Count();
		}

		public FileListVM FileList {
			get;
			set;
		}

		public string Title {
			get
			{
				if (Group.IsDpCons) {
					return Group.Title;
					
				}else if (Group.IsSem) {
					return "Семинар: {0}".FormatWith(Group.Title);
				}
				else {
					return "Группа по курсу: " + Group.Course.WebName;
				}
			}
		}

		public StudentInGroup StudentInGroup { get; set; }

		public static bool CheckShowVideoAllConditions(bool hideForUnlimit, Group group, StudentInGroup sig) {
			return !hideForUnlimit && (!group.WebinarRecordURL.IsEmpty()
				&& sig != null
				&& BerthTypes.AllPaidForCourses.Contains(sig.BerthType_TC));
		}

		public bool ShowVideoAllConditions {
			get
			{
				return CanAccessVideo && CheckShowVideoAllConditions(HideForUnlimit, Group, StudentInGroup);
			}
		}

		public bool ShowLibrary {
			get;
			set;
		}

		public string TrainerCourseInfo { get; set; }
		public bool IsCertExists { get; set; }

		public bool ShowCert {
			get {
				var byExists = Group.IsSem || IsCertExists;
				var firstHalfTrack = CourseTC.HalfTracks.Any(x => x.Value.First() == Group.Course_TC);
				return StudentInGroup != null
						&& Group.IsFinished
						&& BerthTypes.AllForCert.Contains(StudentInGroup.BerthType_TC)
						&& byExists
						&& !firstHalfTrack;
			}
		}

		public User User { get; set; }
		public ProgressVM Progress { get; set; }
		public string VkGroupUrl { get; set; }
		public bool HideForUnlimit { get; set; }
		public bool HideVideo { get; set; }
		public bool IsUnlimit { get; set; }
		public bool CanAccessVideo { get; set; }
	}
}