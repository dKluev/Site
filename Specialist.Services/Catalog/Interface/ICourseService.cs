using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Paging;
using Specialist.Entities;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Interface
{
    public interface ICourseService:IRepository<Course>
    {
        Course GetByUrlName(string urlName);
    	List<string> GetCourseNames(string namePart);
        List<Course> GetAllFor(object obj);
        List<Course> GetAllForTrack(string trackTC, bool includeTrack = false);
        IQueryable<Course> GetAllForCertification(decimal certificationID);
        IQueryable<Course> GetAllForStudent(decimal studentID);

        IQueryable<Course> GetStudentCourseWithParent(string courseTC,
                                                                      decimal studentID);

        IQueryable<CourseLink> GetCourseLinkList(IEnumerable<string> tcList, bool unactive = false);
        List<string> GetElearningNames(string namePart);
    	List<string> GetCourseTCListForSections(IEnumerable<int> sectionIds);

    	List<Course> AllCoursesForList();

    	List<string> GetCourseTCListFor(Type entityType, 
    		IEnumerable<object> objectIds);

    	Dictionary<string, List<string>> GetCourseConsultations();

    	Dictionary<string, List<string>> GetActiveTrackCourses();

    	bool IsTrack(string courseTC);

    	Dictionary<string,string> GetAllActiveCourseNames();

	    Dictionary<string,Course> GetAllCourseNames();

	    Dictionary<string, int> TrackCourseCounts();
	    Dictionary<string, CourseLink> AllCourseLinks();
	    HashSet<string> IntraExtraTracks();
	    Dictionary<string, int> CourseLongestTrack();
	    Dictionary<string, List<string>> GetTrackCourses();
	    HashSet<string> DiplomTracks();
	    HashSet<string> SeminarCourses();
	    HashSet<string> CoursesInDiploms();
	    List<Course> GetAllHitTracks();
	    HashSet<string> OutdatedCourses();
	    HashSet<string> NotSecondCourses();
	    List<string> GetNextCourseTCs(List<string> parentCourseTCs);
	    List<string> GetSecondCourses(string courseTC);
    }
}