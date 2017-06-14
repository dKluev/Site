using System.Collections.Generic;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Entities.ViewModel;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Interface
{
    public interface ICourseVMService
    {
        CourseVM GetByUrlName(string urlName);
        List<TagWithEntity> GetTags(string courseTC);
        MainCoursesVM GetMainCourses();

    	MobileCourseVM GetMobileByUrlName(string urlName);
    	MobileCourseVM GetMobileByGroup(decimal groupId);
	    List<string> GetCourseTCListForTotalSection(int sectionId);
    }
}