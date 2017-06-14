using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Common.Extension;
using Specialist.Services.Catalog.Extension;
using SimpleUtils;
using Specialist.Services.Interface.Passport;
using Tuple = SimpleUtils.Common.Tuple;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Services
{
    public class CourseListVMService: ICourseListVMService
    {
        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public ITrackService TrackService { get; set; }
        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

		[Dependency]
    	public ISectionService SectionService { get; set; }

		[Dependency]
    	public IRepository2<News> NewsService { get; set; }

        [Dependency]
        public IAuthService AuthService { get; set; }

        public bool CheckHasMaxCourseCount(object entity) {
        	return false; /*SiteObjectService.GetByRelationObject<Course>(entity)
                    .IsActive().Count() > CommonConst.MaxCourseCount;*/
        }

        public List<ElearningCourse> ElearningCourses(string name) {
            var elearningCourses = PriceService.GetElearningCourses();
            var courses = CourseService.GetCourseLinkList(elearningCourses)
                .Where(c => c.Name.Contains(name)).Take(50).ToList()
                .Select(c => new ElearningCourse {
                    CourseLink = c,
                    Prices =
                        PriceService.GetAllPricesForCourse(c.CourseTC, null).ToList()
                }).ToList();
            return courses;
        }

        public AllCourseListVM GetAll(object obj)
        {
            var courses = obj == null 
				? CourseService.AllCoursesForList()
				: CourseService.GetAllFor(obj);
	        var track = obj as Course;
	        var isTrack = track != null;
	        var isTrackDiplom = track != null && courses.FirstOrDefault(x => x.Course_TC == track.Course_TC)
				.GetOrDefault(x => x.IsDiplom);
	        var trackList = obj as TrackListVM;
	        var siteTerm = obj as SiteTerm;
	        var isIntraExtraTerm = siteTerm != null && siteTerm.SiteTerm_ID == Sections.Terms.IntraExtra;
	        var isOpenClassesTerm = siteTerm != null && siteTerm.SiteTerm_ID == Sections.Terms.OpenClasses;
	        var isDiplomPage = trackList != null && trackList.IsDiplomPage;
	        var isTrainingProgramsPage = trackList != null && trackList.IsTrainingProgramsPage;
	        if (isDiplomPage || isTrainingProgramsPage) {
		        courses = courses.Where(x => trackList.Courses.Contains(x.Course_TC)).ToList();
	        }
	        var isIntraExtraTrackPage = (isTrack && CourseService.IntraExtraTracks().Contains(track.Course_TC))
				|| isDiplomPage;
	        var trackFullPrices = TrackService.TrackFullPrices();
            var items =
                courses.Select(c => {
                	var nearestGroupAndWebinar = GroupService.GetNearestGroupAndWebinar(c, 
						isIntraExtraTrackPage || isIntraExtraTerm, isOpenClassesTerm);
	                var coefficient = isTrack
						? null
						: GroupService.GetGroupsForCourse(c.Course_TC).Where(x => !x.IsIntraExtramural)
			                .Select(x => PriceUtils.GetCoefficient(x)).Min();

	                var prices = PriceService.GetAllPricesForCourse(c.Course_TC, null);
	                var trackPrices = c.IsTrackBool
		                ? trackFullPrices.GetValueOrDefault(c.Course_TC)
		                : null;
	                return new CommonCourseListItemVM
                    {
                        Course = c,
						IsIntraExtraTrackPage = isIntraExtraTrackPage,
						IsTrackPage = isTrack,
						TrackFullPrices = trackPrices,
                        NearestGroup = nearestGroupAndWebinar.Item1,
						NearestWebinar = nearestGroupAndWebinar.Item2,
                        Prices = prices,
						PriceCoefficent = coefficient
                    };
                })
                .ToList();
	        var commonCourseListVM = new CommonCourseListVM
                {
                    Items = items,
					IsTrackPage = isTrack,
					IsDiplomPage = isDiplomPage,
					IsTrainingProgramsPage = isTrainingProgramsPage
                };
        	return 
                new AllCourseListVM
                {
                    MainList = courses,
					TrackCounts = CourseService.TrackCourseCounts(),
					TrackLastCourseDiscounts = TrackService.TrackLastCourseDiscounts(),
                    Common  = commonCourseListVM,
					IsTrack = isTrack,
					IsDiplomPage = isDiplomPage,
					IsTrainingProgramsPage = isTrainingProgramsPage,
					IsIntraExtra = isIntraExtraTrackPage,
					IsTrackDiplom = isTrackDiplom,
					HideIntraGroup = isIntraExtraTerm || isOpenClassesTerm,
					IsOpenClasses = isOpenClassesTerm,
					CourseWithUnlimit = PriceService.CourseWithUnlimite(),
					
                } ;
        }

        List<CourseLink> GetCourseForSeminar(Group group) {
            if(group.IsSeminar || group.Notes.IsEmpty())
                return new List<CourseLink>();
            var tcList = group.Notes.Split(',').AsEnumerable();
            return CourseService.GetCourseLinkList(tcList).ToList();
        }

		public List<GroupSeminar> ProbWebinars() {
			var groupQueryable = GroupService.GetAll().NotBegin()
				.Where(g => g.Course_TC == CourseTC.ProbWeb
					&& g.LectureType_TC == LectureTypes.Planned
					&& (g.GroupCalc.NumOfWebinarists < g.MaxNumOfWebinarists.GetValueOrDefault(CommonConst.MaxNumOfWebinarists)))
				.ToList();
			return groupQueryable.Select(x => new GroupSeminar(x)).ToList();

		} 
      

        public SeminarListVM GetSeminars(bool isConsultation) {
	        var model = new SeminarListVM();
        	var groupQueryable = GroupService.GetAll().SeminarsFilter(); 
			groupQueryable = isConsultation 
				? groupQueryable.Where(g => g.Course.CourseCategories
					.Any(cc => cc.Category_TC == Categories.Consultations))
				: groupQueryable.Where(x => x.Course_TC == CourseTC.Seminar);
        	var user = AuthService.CurrentUser;
			if(!isConsultation || user == null || user.IsEmployee) {
				groupQueryable = groupQueryable.Where(g => g.LectureType_TC == LectureTypes.Planned &&
					g.DateBeg < DateTime.Today.AddMonths(6)
					&& (g.GroupCalc.NumOfWebinarists < g.MaxNumOfWebinarists.GetValueOrDefault(CommonConst.MaxNumOfWebinarists)));
				model.ProbWebinars = ProbWebinars();
			} else {
				var studentCourseTCs = new List<string>();
				var student = AuthService.GetCurrentStudent();
				if (student != null) {
					studentCourseTCs = student.EndedGroups.Select(g => g.Course_TC).ToList();
				}
				var consultationTCs = studentCourseTCs.SelectMany(c =>
					CourseService.GetCourseConsultations().GetValueOrDefault(c) ?? new List<string>()).ToList();
				groupQueryable = groupQueryable.Where(g => consultationTCs.Contains(g.Course_TC));
			}

        	var groupSeminars = groupQueryable
                .ToList()
                .Select(g => new GroupSeminar(g, GetCourseForSeminar(g)))
                .ToList();
	        var newIds = groupSeminars.Select(x => x.Group.NewsId ?? 0)
		        .Where(x => x > 0).ToList();
			if (newIds.Any()) {
				var news = NewsService.GetAll(x => newIds.Contains(x.NewsID))
					.ToDictionary(x => x.NewsID, x => x);
				groupSeminars.ForEach(x => 
					x.News = news.GetValueOrDefault(x.Group.NewsId ?? 0));
			}
	        if (!isConsultation) {
		        groupSeminars = groupSeminars.Where(x => !x.Group.Title.IsEmpty()).ToList();
	        }
			model.GroupSeminars = groupSeminars;
            model.Consultation = isConsultation;
	        return model;
        }

		
    
    }
}
