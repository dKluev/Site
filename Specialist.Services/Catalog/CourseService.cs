using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using SimpleUtils;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Common.Extension;
using System.Linq.Dynamic;
using Specialist.Services.UnityInterception;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Const;
using Specialist.Services.Catalog.Extension;
using Specialist.Web.Common.Utils;

namespace Specialist.Services
{
    public class CourseService : Repository<Course>, ICourseService
    {
        public CourseService() : base(new ContextProvider()) {}

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public IRepository2<CoursesCourseGiveOutItem> CoursesCourseGiveOutItemService { get; set; }

        [Dependency]
        public IRepository2<Group> GroupService { get; set; }

        [Dependency]
        public IRepository2<CoursesChain> CourseChainService { get; set; }

		[Dependency]
		public ISiteObjectRelationService SiteObjectRelationService { get; set; }


    	public List<string> GetNextCourseTCs(List<string> parentCourseTCs) {
    		var nextCourseTCList = CourseChainService.GetAll()
    			.Where(cc => parentCourseTCs.Contains(cc.Course_TC))
    			.OrderBy(cc => cc.SortOrder)
    			.Select(cc => new {cc.NextCourse_TC, cc.SortOrder})
				.ToList().Distinct(x => x.NextCourse_TC)
				.OrderBy(x => x.SortOrder).Select(x => x.NextCourse_TC).ToList();
    		return nextCourseTCList;
    	}



	    public HashSet<string> OutdatedCourses() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => {
			    var dateBeg = DateTime.Today.AddMonths(-6);
			    var dateEnd = DateTime.Today.AddMonths(6);
			    var courses = GroupService.GetAll(x => x.LectureType_TC == LectureTypes.Planned)
				    .Where(x => x.DateBeg >= dateBeg && x.DateBeg <= dateEnd)
				    .GroupBy(x => x.Course_TC).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
			    return new HashSet<string>(GetAllActiveCourseNames().Keys.Except(courses));
		    });
	    }

        public Course GetByUrlName(string urlName)
        {
            var loadOptions = new DataLoadOptions();

            loadOptions.LoadWith<Course>(c => c.CourseContents);
            loadOptions.LoadWith<Course>(c => c.CoursePrerequisites);
            loadOptions.AssociateWith<Course>(c =>
                                              c.CourseContents.OrderBy(cc => cc.ModuleNumber));

            context.LoadOptions = loadOptions;
            var course = GetAll().
                Where(c => c.IsActive && c.UrlName == urlName)
				.OrderBy(x => x.Course_ID).FirstOrDefault();
            if (course != null)
            {
                course.Prices = PriceService.GetAllPricesForCourse(course.Course_TC, null);
            }
          
            return course;
        }

		public Dictionary<string, CourseLink> AllCourseLinks() {
			return MethodBase.GetCurrentMethod().Cache(() => {
                var all = GetAll().Select(c =>
                    new CourseLink {
                        Name = c.Name,
                        WebName = c.WebName,
                        WebShortName = c.WebShortName,
                        UrlName = c.UrlName,
                        IsTrack = c.IsTrack,
						IsActive = c.IsActive,
						IsNew = c.IsNew,
                        CourseTC = c.Course_TC,
                    }).ToList();

				return all.ToDictionary(x => x.CourseTC, x => x);
			},24);
		} 

        public IQueryable<CourseLink> GetCourseLinkList(IEnumerable<string> tcList, bool unactive = false) {
	        var links = AllCourseLinks();
	        var result = links.GetValues(tcList).Where(x => x != null);
			if(!unactive)
				result = result.Where(x => x.IsActive).ToList();
	        return result.AsQueryable();
        }

	    public List<string> GetSecondCourses(string courseTC) {
		    var parentTC = this.GetValues(courseTC, x => x.ParentCourse_TC);
			var next = GetNextCourseTCs(_.List(parentTC));
	        var notSecondCourseDiscount = NotSecondCourses();
		    return next.Where(x => !notSecondCourseDiscount.Contains(x)).ToList();
	    }

//	    public string GetSecondCourseTC(string courseTC) {
//		    var parentTC = this.GetValues(courseTC, x => x.ParentCourse_TC);
//			var next = GetNextCourseTCs(_.List(parentTC));
//
//	        var notSecondCourseDiscount = NotSecondCourses();
//		    return next.FirstOrDefault(x => !notSecondCourseDiscount.Contains(x));
//	    }

    	public IQueryable<Course> GetAllForStudent(decimal studentID)
        {
            
            return
                from sig in context.GetTable<StudentInGroup>()
                where sig.Student_ID == studentID
                select sig.Group.Course;

        }

        public List<string> GetCourseNames(string namePart)
        {
            var courseNames =
                from pair in GetAllActiveCourseNames()
                where pair.Value.ToLower().Contains(namePart.ToLower())
                select pair;
            return courseNames.Take(CommonConst.CourseNameCount)
                .Select(c => c.Value).ToList();
        }

        public List<string> GetElearningNames(string namePart) {
            var elearningCourses = PriceService.GetElearningCourses();
            var courseNames =
                from pair in GetAllActiveCourseNames()
                where elearningCourses.Contains(pair.Key) &&
                    pair.Value.ToLower().Contains(namePart.ToLower())
                select pair;
            return courseNames.Take(CommonConst.CourseNameCount)
                .Select(c => c.Value).ToList();
        }

        [Cached]
        public virtual Dictionary<string,string> GetAllActiveCourseNames() {
	        return GetAllCourseNames().Where(x => x.Value.IsActive)
				.ToDictionary(x => x.Key, x => x.Value.WebName ?? string.Empty);
        }

	    [Cached]
        public virtual Dictionary<string,Course> GetAllCourseNames() {
            return GetAll().Where(c => !c.IsTrack.GetValueOrDefault())
                .Select(c => new {c.WebName, c.NameOfficialEn, c.Course_TC, c.BaseHours, c.IsActive, c.UrlName}).OrderBy(c => c.WebName)
                .ToDictionary(c => c.Course_TC, c =>
					new Course{Course_TC = c.Course_TC, IsActive = c.IsActive, 
						WebName = c.WebName, 
						NameOfficialEn = c.NameOfficialEn,
						BaseHours = c.BaseHours,
						UrlName = c.UrlName});
        }

        [Cached]
		public virtual Dictionary<string, List<string>> GetCourseConsultations() {
			var consultations =
				GetAll().Where(c => c.CourseCategories.Any(cc => cc.Category_TC == Categories.Consultations))
					.Select(c => new {c.Course_TC, Categories = c.CourseCategories.Select(x => x.Category_TC)})
					.ToList();
			var coursesWithConsultation = GetAll().Where(c => c.IsFreeConsultationExists)
				.Select(c => new {c.Course_TC, Categories = c.CourseCategories.Select(x => x.Category_TC)})
				.ToList();

			return coursesWithConsultation.ToDictionary(x => x.Course_TC, x => consultations
				.Where(c => c.Categories.Intersect(x.Categories).Any()).Select(y => y.Course_TC).ToList());
		}
		

        #region GetAllFor

        public List<Course> GetAllForTrack(string trackTC, bool includeTrack = false) {
        	var courses = new List<string>(); 
			if(includeTrack)
				courses.Add(trackTC);
        	var courseTCs = GetActiveTrackCourses().GetValueOrDefault(trackTC)
				?? new List<string>();
			courses.AddRange(courseTCs);
			return AllCoursesForList()
            		.Where(c => courses.Contains(c.Course_TC))
            		.OrderBy(c => courses.IndexOf(c.Course_TC)).ToList();
        }

        public List<Course> GetAllTrackForCourse(string courseTC) {
        	var trackTCs = GetActiveTrackCourses().Where(x => x.Value.Contains(courseTC))
				.Select(x => x.Key);
			return AllCoursesForList().Where(c => trackTCs.Contains(c.Course_TC)).ToList();
        }


        public List<Course> GetAllDiplomTracks() {
        	var trackTCs = DiplomTracks();
			return AllCoursesForList().Where(c => trackTCs.Contains(c.Course_TC)).ToList();
        }
		public Dictionary<string, List<string>> GetTrackCourses() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var trackCourses =
					(from track in context.GetTable<Track>()
						group track by track.Track_TC
						into gr
						select new {
							gr.Key,
							Courses = gr.OrderBy(x => x.SortOrder)
								.Select(track => track.Course_TC)
						})
						.ToList();
				return trackCourses.DistinctToDictionary(x => x.Key, x => x.Courses.ToList());
			});
		}

		public Dictionary<string, List<string>> GetActiveTrackCourses() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var trackCourses =
					(from track in context.GetTable<Track>()
						where track.TrackCourse.IsActive
						group track by track.Track_TC
						into gr
						select new {
							gr.Key,
							Courses = gr.OrderBy(x => x.SortOrder)
								.Select(track => track.Course_TC)
						})
						.ToList();
				return trackCourses.DistinctToDictionary(x => x.Key, x => x.Courses.ToList());
			});
		}

	    public HashSet<string> CoursesInDiploms() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => 
				new HashSet<string>(DiplomTracks().SelectMany(x =>
			    GetActiveTrackCourses().GetValueOrDefault(x) ?? new List<string>()).Distinct()));
	    } 

	    public Dictionary<string, int> CourseLongestTrack() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => {
			    var tracks = GetActiveTrackCourses();
			    return tracks.SelectMany(x => x.Value.Select(y => Tuple.Create(y, x.Value.Count)))
				    .GroupBy(x => x.Item1).Select(x => x.OrderByDescending(y => y.Item2).First())
				    .ToDictionary(x => x.Item1, x => x.Item2);
		    });

	    }


		[Cached]
		public virtual Dictionary<string, int> TrackCourseCounts() {
			return GetActiveTrackCourses().ToDictionary(x => x.Key, 
				x => x.Value.Count(y => y != CourseTC.DpCons));
		}

		public bool IsTrack(string courseTC) {
			return GetTrackCourses().ContainsKey(courseTC);
		}

        public IQueryable<Course> GetAllForCertification(decimal certificationID)
        {
            var courses =
                from examCourse in context.GetTable<ExamCourse>()
                where examCourse.Exam.CertificationExams
                    .Any(ce => ce.Certification_ID == certificationID)
                select examCourse.Course;
            return courses.Distinct();

        }

        public IQueryable<Course> GetAllForExam(decimal examID)
        {
            var courses =
                from examCourse in context.GetTable<ExamCourse>()
                where examCourse.Exam_ID == examID
                select examCourse.Course;
            return courses.Distinct();

        }

        #endregion

		[Cached]
		public virtual List<Course> AllCoursesForList() {
			var localContext = new SpecialistDataContext();
			var loadOptions = new DataLoadOptions();
			loadOptions.LoadWith<Course>(c => c.CourseLanguages);
			loadOptions.LoadWith<CourseLanguage>(c => c.Language);
			localContext.LoadOptions = loadOptions;
			var result = localContext.Courses.IsActive().Select(c => new
				{
					c.Course_ID,
					c.Course_TC,
					c.IsTrack,
					c.UrlName,
					c.WebName,
					c.IsNew,
					c.WebPublishSchedule,
					c.Name,
					c.WebShortName,
					c.CourseDirectionA_TC,
					c.BaseHours,
					c.AdditionalHours,
					c.IntraExtramuralHours,
					c.IntraExtramuralHoursOut,
					Languages = c.CourseLanguages.Select(x => x.Language_TC),
					TrackFirstCourseTC = c.TrackCourses.OrderBy(t => t.SortOrder)
					.Select(t => t.Course.Course_TC).FirstOrDefault()
				}).ToList().GroupBy(c => c.UrlName).Select(x => 
					x.OrderBy(y => y.Course_ID).First());

			return result.Select(c =>
			{
				var courseLanguages = new EntitySet<CourseLanguage>();
				courseLanguages.AddRange(c.Languages.Select(ln =>
					new CourseLanguage { Language = new Language { Language_TC = ln } }));
				return new Course
				{
					Course_ID = c.Course_ID,
					Course_TC = c.Course_TC,
					UrlName = c.UrlName,
					IsNew = c.IsNew,
					WebPublishSchedule = c.WebPublishSchedule,
					WebName = c.WebName,
					CourseDirectionA_TC = c.CourseDirectionA_TC,
					Name = c.Name,
					IsTrack = c.IsTrack,
					WebShortName = c.WebShortName,
					BaseHours = c.BaseHours,
					AdditionalHours = c.AdditionalHours,
					IntraExtramuralHours = c.IntraExtramuralHours,
					IntraExtramuralHoursOut = c.IntraExtramuralHoursOut,
					CourseLanguages = courseLanguages,
					TrackFirstCourseTC = c.TrackFirstCourseTC
				};
			}).ToList();

		}

	    public HashSet<string> IntraExtraTracks() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => new HashSet<string>(this.GetAll(x => x.IsTrack.GetValueOrDefault() && x.IntraExtramuralHours > 0)
			    .Select(x => x.Course_TC)));
	    }
	    public HashSet<string> DiplomTracks() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => 
				new HashSet<string>(this.GetAll(x => x.IsTrack.GetValueOrDefault() 
					&& x.IsActive
					&& (x.BaseHours >= CommonConst.DiplomHours || 
					(x.IntraExtramuralHours + x.IntraExtramuralHoursOut >= CommonConst.DiplomHours)))
				    .Select(x => x.Course_TC)));
	    }
	    public HashSet<string> SeminarCourses() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => 
				new HashSet<string>(this.GetAll(x => x.CourseDirection_TC == CourseDirections.Old.Seminar)
					.Select(x => x.Course_TC)));
	    }

		[Cached]
		public virtual Dictionary<int, List<string>> SectionCourses() {
			var result =
				SiteObjectRelationService.GetRelation(typeof (Course), Enumerable.Empty<object>(), typeof (Section))
					.Select(x => new {CourseTC = x.Object_ID.ToString(), SectionId = (int) x.RelationObject_ID})
					.ToList().GroupBy(x => x.SectionId).ToDictionary(x => x.Key, x => x.Select(z => z.CourseTC)
						.ToList());
			return result;
		} 

    	public List<string> GetCourseTCListForSections(IEnumerable<int> sectionIds) {

    		return sectionIds.SelectMany(x => SectionCourses().GetValueOrDefault(x) ?? new List<string>())
    			.ToList();
    	}

		public List<string> GetCourseTCListFor(Type entityType, 
			IEnumerable<object> objectIds) {
    		return SiteObjectRelationService.GetByRelation(
    			entityType, 
    			objectIds, typeof(Course))
    			.Select(x => x.Object_ID.ToString()).ToList();
    	}

    	public List<Course> GetAllFor(object obj)
        {
            IQueryable<Course> result = null;
            obj.Match<Course>(x => result = GetAllForTrack(x.Course_TC, true)
				.AsQueryable());
            obj.Match<Exam>(x => result = GetAllForExam(x.Exam_ID));
            obj.Match<TrackListVM>(x => result = 
				GetCoursesForTrackList(x).AsQueryable());
            if (result == null)
            {
            	var siteObjectRelations = SiteObjectRelationService
            		.GetByRelation(LinqToSqlUtils.GetTableName(obj), 
            			LinqToSqlUtils.GetPK(obj), typeof(Course));
            	var courses =  siteObjectRelations
					.Select(r => new {r.Object_ID, r.RelationOrder}).ToList()
					.Distinct(x => x.Object_ID.ToString())
					.ToDictionary(x => x.Object_ID.ToString(), x => x.RelationOrder);
            	return AllCoursesForList()
            		.Where(c => courses.ContainsKey(c.Course_TC))
//					.OrderBy(c => courses.GetValueOrDefault(c.Course_TC)).ToList();
					.OrderByDescending(x => x.IsTrackBool)
					.ThenByDescending(x => x.IsDiplom)
					.ThenByDescending(x => x.IsHit)
					.ThenBy(c => courses.GetValueOrDefault(c.Course_TC)).ToList();
            }
        	return result.ToList();
        }

	    private List<Course> GetCoursesForTrackList(TrackListVM x) {
		    if (x.IsDiplomPage) {
			    return GetAllDiplomTracks();
		    }
		    if (x.IsTrainingProgramsPage) {
				return GetAllHitTracks();
		    }
		    return GetAllTrackForCourse(x.Course.Course_TC);
	    }

	    public List<Course> GetAllHitTracks() {
			return MethodBase.GetCurrentMethod().Cache(() => 
		    AllCoursesForList().Where(c => c.IsTrackBool 
			    && !c.IsDiplom
			    && c.IsHit).ToList());
	    }


	    public HashSet<string> NotSecondCourses() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => {
			    var courses = this.GetAll(x => x.IsActive && (
				    x.CourseDirectionA_TC == CourseDirections.Sap
					    || (x.CourseDirectionA_TC == CourseDirections.School &&
						    x.AuthorizationType_TC == AuthorizationTypes.OneC)
					    || x.ParentCourse_TC.StartsWith(CourseTC.Erp1c)
				    )
				    ).Select(x => x.Course_TC).ToList();
				courses.AddRange(CourseTC.AllSpecial);
			    return new HashSet<string>(courses);
		    });
	    }


	    public IQueryable<Course> GetStudentCourseWithParent(string courseTC,
            decimal studentID)
        {
            var currentCourse = GetAll().FirstOrDefault(c => c.Course_TC == courseTC);
            var courses =
                from sig in context.GetTable<StudentInGroup>()
                let parentCourseTCList =
                    from course in GetAll()
                    where course.ParentCourse_TC == currentCourse.ParentCourse_TC
                        && course.Course_TC != currentCourse.Course_TC
                    select course.Course_TC

                where sig.Student_ID == studentID
                    && parentCourseTCList.Contains(sig.Group.Course_TC)
                select sig.Group.Course;
            return courses;
        }
    

    }
}