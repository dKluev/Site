using System;
using System.Collections.Generic;
using SimpleUtils.Common;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;

namespace Specialist.Services.Interface
{
    public interface ICourseListVMService
    {
        SeminarListVM GetSeminars(bool isConsultation);
        AllCourseListVM GetAll(object obj);
        bool CheckHasMaxCourseCount(object entity);
        List<ElearningCourse> ElearningCourses(string name);
//    	List<Tuple<Course, DateTime>> GetAnnounces(int sectionId);
	    List<GroupSeminar> ProbWebinars();
    }
}