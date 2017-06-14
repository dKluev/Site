using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Education.ViewModel;
using Specialist.Entities.Filter;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Common;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using SimpleUtils;
using System.Linq;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Interface.Passport;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Services.Catalog.Extension;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Passport;
using Specialist.Services.Tests;
using Specialist.Web.Common.Utils;
using Certificate = Specialist.Entities.Context.Certificate;

namespace Specialist.Services
{
    public class GroupVMService: IGroupVMService
    {
        [Dependency]
        public IGroupService GroupService { get; set; }

		[Dependency]
		public IRepository2<UserCourseInfo> UserCourseInfoService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public TestService TestService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public IRepository<UserMessage> UserMessageService { get; set; }

        [Dependency]
        public IAuthService AuthService { get; set; }

		 [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        [Dependency]
        public IRepository<StudentInGroup> StudentInGroupService { get; set; }

        [Dependency]
        public IFileVMService FileVMService { get; set; }

		[Dependency]
        public ISectionService SectionService { get; set; }

		[Dependency]
        public IRepository2<Certificate> CertificateService { get; set; }

        public GroupListVM GetAllGroups(GroupFilter filter, int pageIndex)
        {
           
            var cityTC = UserSettingsService.CityTC;
            var leftBeginDate = filter.BeginDate;
            var rightBeginDate = filter.EndDate;
        	var courseTCs = new List<string>();
			if(!filter.CourseTC.IsEmpty())
				courseTCs.Add(filter.CourseTC);
			else {
				if(filter.SectionId.HasValue) {
					var mainSection =
						SectionService.GetSectionsTree()
							.FirstOrDefault(x => x.Section_ID 
								== filter.SectionId.Value);
					var sectionIds = new List<int>();
					if(mainSection != null && mainSection.SubSections.Any())
						sectionIds = mainSection.SubSections.Select(x => x.Section_ID).ToList();
					else
						sectionIds.Add(filter.SectionId.Value);
					courseTCs.AddRange(CourseService.GetCourseTCListForSections(sectionIds));
				}
			}
            var groupQuery = GroupService.GetGroupsForCourse(courseTCs, 
                cityTC, leftBeginDate, rightBeginDate, filter.DayShiftTC, filter.ComplexTC,
				filter.DaySequenceTC);
	        switch (filter.StudyTypeId) {
				case GroupFilter.StudyType.Intramural:
					groupQuery = groupQuery.Where(x => !x.ForWebinarOnly);
			        break;
				case GroupFilter.StudyType.Webinar:
					groupQuery = groupQuery.Where(x => x.WebinarExists);
			        break;
				case GroupFilter.StudyType.OpenLearning:
					groupQuery = groupQuery.Where(x => x.MegaGroup_ID != null);
			        break;
				case GroupFilter.StudyType.IntraExtra:
					groupQuery = groupQuery.Where(x => x.IsIntraExtramural);
			        break;
	        }
	        if (!filter.EmployeeTC.IsEmpty())
		        groupQuery = groupQuery.Where(x => x.Teacher_TC == filter.EmployeeTC);
	        groupQuery = groupQuery.NotSpecial();
            var groups = groupQuery.ToPagedList(pageIndex);
            var result =
              new GroupListVM {
                  Groups = groups,
                  Filter = filter,
              };
            if(!filter.CourseTC.IsEmpty()) {
                var course = CourseService.GetByPK(filter.CourseTC);
                filter.CourseName = course.GetOrDefault(x => x.Name);
                result.Course = course;
            }

          
            return result;
        }

        List<CourseContent> GetContent(Course course, int offset, int hours)
        {
            var contents = course.CourseContents.OrderBy(x => x.ModuleNumber);
            var result = new List<CourseContent>();
            if(contents.Any(c => c.NumOfLessons == 0))
                return result ;
			decimal lecturePass = 0;
            foreach (var content in contents)
            {
                if (lecturePass >= offset && lecturePass < offset + hours)
                    result.Add(content);
                lecturePass += content.Hours;
            }
            return result;
        }

		int GetLectureHours(Lecture lecture) {
			return (int) (((lecture.LectureDateEnd - lecture.LectureDateBeg).TotalMinutes
				- lecture.Breaks)/45);
		}

        public GroupVM GetGroup(decimal groupID)
        {
            var group = GroupService.GetByPK(groupID);
	        if (group == null)
		        return null;
            var user = AuthService.CurrentUser;
            var sglList = new List<StudentInGroupLecture>();
	        StudentInGroup studentInGroup = null;
            if(user.GetOrDefault(x => x.IsStudent)) {
            	studentInGroup = GetSig(groupID);
				if(studentInGroup != null)
	            	sglList = GetSqlList(studentInGroup);
            }



	        var lectureOffset = 0;
	        var lecturesGroup = group.IsOpenLearning
		        ? GroupService.GetByPK(group.MegaGroup_ID.Value) : group;
	        var lectures = lecturesGroup.Lectures.OrderBy(l => l.LectureDateBeg).ToList();
	        var lecturesVM = lectures.Select(l => {
                	var lectureHours = GetLectureHours(l);
                	lectureOffset += lectureHours;
                	return new GroupVM.LectureVM
                	{
                		Lecture = l,
                		Contents = GetContent(group.Course, lectureOffset - lectureHours,
						lectureHours),
                		StudentLecture = sglList.FirstOrDefault(
                			sgl => sgl.Lecture_ID == l.Lecture_ID)
                	};
                }).ToList();

	        var groupFiles = @group.GroupFiles.Select(g => g.UserFile).ToList();
	        var groupUserFiles = groupFiles.Select(x => x.UserFileID);
	        var fileList = new FileListVM{Group = group,    
                Files = groupFiles,
                UserFiles = FileVMService.GetUserFiles(null)
					.Where(x => !groupUserFiles.Contains( x.UserFileID)).ToList()

            };
        	var tests = SiteObjectService.GetByRelationObject<Test>(group.Course).ToList();
	        var testAfterComplete = new List<Test>();
	        if (studentInGroup != null) {

		        var isBegin = group.DateBeg.GetValueOrDefault() + group.TimeBeg.GetValueOrDefault().TimeOfDay <= DateTime.Now;
		        if (group.Course_TC == CourseTC.DpCons && isBegin) {
			        testAfterComplete = TestService.CourseTests()
				        .GetValueOrDefault(studentInGroup.Track_TC) ?? new List<Test>();
			        
		        }else {  
			        testAfterComplete = TestService.CourseTests()
				        .GetValueOrDefault(@group.Course_TC) ?? new List<Test>();
			        
		        }
	        }
	        var trainerUserId = group.Teacher.GetOrDefault(x => 
				x.User.GetOrDefault(y => y.UserID));
	        var trainerCourseInfo = UserCourseInfoService.FirstOrDefault(x =>
		        x.UserID == trainerUserId && x.Course_TC == group.Course_TC)
		        .GetOrDefault(x => x.Description);
	        var sigId = studentInGroup.GetOrDefault(x => x.StudentInGroup_ID);
	        var IsCertExists = CertificateService.GetAll(x =>
		        x.StudentInGroup_ID == sigId).Any();
	        var isUnlimit = studentInGroup.GetOrDefault(x => x.IsUnlimit);
	        var r = GetHideVideo(studentInGroup, sglList, group);
	        var hideVideo = r.Item1;
	        var hideForUnlimit = r.Item2;
	        var canAccessVideo = r.Item3;


	        return new GroupVM
            {
                Group = group,
				TrainerCourseInfo = trainerCourseInfo,
                Lectures = lecturesVM,
                LastMessages = GetLastMessages(groupID),
                FileList = fileList,
				CanAccessVideo = canAccessVideo,
				Tests = tests,
				TestsAfterComplete = testAfterComplete,
				StudentInGroup = studentInGroup,
				IsCertExists = IsCertExists,
				User = user,
				HideForUnlimit = hideForUnlimit,
				HideVideo = hideVideo,
				IsUnlimit = isUnlimit,
				Progress = GetProgress(lectures, group, studentInGroup)
            };
        }

	    private StudentInGroup GetSig(decimal groupID) {
		    var user = AuthService.CurrentUser;
		    return StudentInGroupService.GetAll()
			    .FirstOrDefault(x => BerthTypes.AllPaidAndKonsForCourses.Contains(x.BerthType_TC) && x.Student_ID
				    == user.Student_ID && x.Group_ID == groupID);
	    }

	    private static List<StudentInGroupLecture> GetSqlList(StudentInGroup studentInGroup) {
		    return studentInGroup.StudentInGroupLectures.OrderBy(x => x.Lecture.LectureDateBeg).ToList();
	    }

	    private static Tuple<bool, bool, bool> GetHideVideo(StudentInGroup studentInGroup, List<StudentInGroupLecture> sglList, Group group) {
//		    return Tuple.Create(false, false);
	        var isUnlimit = studentInGroup.GetOrDefault(x => x.IsUnlimit);
	        var hideForUnlimit = isUnlimit;
	        var hideVideo = true;
	        var isIntraExtra = group.IsIntraExtramural;
		    var canAccessVideo = false;
		    if (studentInGroup != null
			    && sglList.Any()
			    && (isIntraExtra
				    || studentInGroup.Group.IsOpenLearning
				    || isUnlimit
				    || PriceTypes.IsWebinar(studentInGroup.PriceType_TC))) {
			    canAccessVideo = true;
			    var firstLecture = sglList.First();
			    if (isIntraExtra && sglList.Count == 1) {
				    hideVideo = false;
			    }
			    else if (firstLecture.Lecture.LectureDateBeg <= DateTime.Now) {
				    var firstLectureIsVisited = !firstLecture.Truancy.GetValueOrDefault();
				    if (isUnlimit) {
					    hideForUnlimit = !(firstLecture.IsRecognized.GetValueOrDefault()
						    && firstLectureIsVisited);
					    hideVideo = hideForUnlimit;
				    }
				    else if (isIntraExtra) {
					    hideVideo = !firstLectureIsVisited;
				    }
				    else {
					    hideVideo = false;
				    }
			    }
		    }
		    return Tuple.Create(hideVideo, hideForUnlimit,canAccessVideo);
	    }

	    public Tuple<bool,Group> HideVimeoGroupVideo(decimal groupId) {
		    var user = AuthService.CurrentUser;
            var group = GroupService.GetByPK(groupId);
		    if (user.Employee_TC == group.Teacher_TC) {
			    return Tuple.Create(false, group);
		    }
		    var sig = GetSig(groupId);
		    if (sig == null) {
			    return Tuple.Create(true, (Group)null);
		    }
		    var sglList = GetSqlList(sig);
	        var r = GetHideVideo(sig, sglList, group);
	        var hideVideo = r.Item1;
	        var hideForUnlimit = r.Item2;
		    var hide = hideVideo || !group.HasVimeo || !GroupVM.CheckShowVideoAllConditions(hideForUnlimit, group, sig);
		    return Tuple.Create(hide, group);
	    }

	    private GroupVM.ProgressVM GetProgress(List<Lecture> lectures, Group @group, StudentInGroup studentInGroup) {
		    if (studentInGroup == null || @group.IsDpCons) {
			    return null;
		    }
		    var courseCurrentHours = 0.0;
		    var courseTotalHours = 0.0;
		    var trackProgress = 0;
		    if (lectures.Any()) {
			    var totalCount = lectures.Count;
			    courseCurrentHours = lectures.Where(x => x.LectureDateEnd <= DateTime.Now)
					.Sum(x => x.Hours);
			    courseTotalHours = lectures.Sum(x => x.Hours);
			    var courseTc = @group.Course_TC;
			    if (studentInGroup.Track_TC != null) {
				    var courses = CourseService.GetTrackCourses().GetValueOrDefault(studentInGroup.Track_TC);
				    if (courses != null) {
					    var index = courses.IndexOf(courseTc);
					    if (index >= 0) {
						    trackProgress = (100*index + (int)(100 * courseCurrentHours/courseTotalHours))/courses.Count;
					    }
				    }
			    }
		    }
		    var ahCoef = 4.0/3;
		    return new GroupVM.ProgressVM {
			    CourseTotalHours = (int)(courseTotalHours * ahCoef),
			    CourseCurrentHours = (int)(courseCurrentHours * ahCoef),
				Track = trackProgress,
				TrackName = studentInGroup.Track_TC.GetOrDefault(x => 
					CourseService.AllCourseLinks()[x.Trim()].WebName)
		    }; 
	    }

	    private List<UserMessage> GetLastMessages(decimal groupID) {
		    var lastMessages = new List<UserMessage>();
		    var rootMessage = GetGroupRootMessage(groupID);
		    if (rootMessage != null)
			    lastMessages = UserMessageService.GetAll(x =>
				    x.ParentMessageID == rootMessage.UserMessageID).OrderByDescending(x =>
					    x.CreateDate).Take(5).ToList();
		    return lastMessages;
	    }

	    private UserMessage GetGroupRootMessage(decimal groupID)
        {
            return UserMessageService.GetAll()
                .FirstOrDefault(x => x.GroupID == groupID);
        }


        public UserMessage GetOrCreateGroupRootMessage(decimal groupID) {
            var rootMessage = GetGroupRootMessage(groupID);
            if (rootMessage == null) {
                var text = GetGroupTitle(groupID);
                rootMessage = new UserMessage {
                    CreatorUserID = CommonConst.AdminUserID,
                    Text = text,
                    Title = text,
                    GroupID = groupID,
                };
                UserMessageService.InsertAndSubmit(rootMessage);
            }
            return rootMessage;
        }

	    private string GetGroupTitle(decimal groupID) {
		    var courseName = GroupService.GetByPK(groupID).Title;
		    return "Выпускники курса " + StringUtils.AngleBrackets(courseName);
	    }


	    public List<Grouping<Section, Group>> GetSectionGroups(IEnumerable<Group> groups) {
    		var sectionWithCourses = GetSectionWithCourses();
    		return sectionWithCourses.Select(s =>
    			Grouping.New(s.Key,
    				groups.Where(c => s.Contains(c.Course_TC))))
    			.Where(s => s.Any()).ToList();
    	}

    	public List<Grouping<Section, Course>> GetSectionCourses(IEnumerable<Course> courses) {
    		var sectionWithCourses = GetSectionWithCourses();
    		return sectionWithCourses.Select(s =>
    			Grouping.New(s.Key,
    				courses.Where(c => s.Contains(c.Course_TC))))
    			.Where(s => s.Any()).ToList();
    	}
    	public List<Grouping<Section, string>> GetSectionCourseTCs(IEnumerable<string> courseTCs) {
    		var sectionWithCourses = GetSectionWithCourses();
    		return sectionWithCourses.Select(s =>
    			Grouping.New(s.Key,
    				courseTCs.Where(s.Contains)))
    			.Where(s => s.Any()).ToList();
    	}

		public List<Group> DiscountGroups() {
			return MethodBase.GetCurrentMethod().Cache(() => {
				var special = _.List<decimal>(140447, 149505, 172845, 159670);
				var maxDate = DateTime.Today.AddDays(8);
				var groups = GroupService.GetPlannedAndNotBegin()
					.Where(g => g.Discount > 0 && (g.GroupCalc.NumOfStudents < 8 
						|| special.Contains(g.Group_ID)) &&
						g.DateBeg <= maxDate).NotSpecial().ToList();
				var courses = groups.Select(x => x.Course_TC).Distinct().ToList();
			var courseIndexes = courses.OrderByDescending(x => 
				PriceService.CoursePriceIndex().GetValueOrDefault(x)).ToList();
				return groups.OrderBy(x => x.DateBeg)
					.ThenBy(x => courseIndexes.IndexOf(x.Course_TC)).ToList();
			});

		}

        public List<Group> GetAllForMain() {
//	        var outdate = CourseService.OutdatedCourses();
	        var groups = DiscountGroups().Where(x => 
				!CourseTC.NotMainPageParents.Contains(x.Course.ParentCourse_TC)
//				&& !outdate.Contains(x.Course_TC) 
				&& !x.MegaGroup_ID.HasValue).Take(CommonConst.GroupForMainCount).ToList();
            return groups;
        }

    	private IEnumerable<Grouping<Section, string>> GetSectionWithCourses() {
    		var rootSections = 
    			SectionService.GetSectionsTree();
    		return rootSections.Select(s => Grouping.New(s,
    			CourseService.GetCourseTCListForSections(_.List(s.Section_ID)
				.AddFluent(s.SubSections.Select(x => x.Section_ID)))));
    	}
    }
}
