using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;

namespace Specialist.Web.Cms.Services {
	public class CourseEntityService {
		 
		[Dependency]
		public IRepository<Group> GroupService { get; set; }

		[Dependency]
		public IRepository<Course> CourseService { get; set; }

		public List<Tuple<string, bool,string>> GetNewVersionCourses() {
    		var courseSources = CourseService.GetAll().IsActive()
    			.GroupBy(x => x.UrlName)
    			.Where(x => x.Count() > 1).ToList();
    		var bothCourseTCs = courseSources.Select(x => 
				x.OrderByDescending(y => y.Course_ID))
    			.Select(x => new {New = x.ElementAt(0).Course_TC, 
					Old = x.ElementAt(1).Course_TC});
			var courseTCs = bothCourseTCs.Select(x => x.New).ToList();
    		var coursesWithGroup = GroupService.GetAll()
    			.Where(y => y.DateBeg >= DateTime.Today &&
    				courseTCs.Contains(y.Course_TC)).Select(x => x.Course_TC).Distinct().ToList();
    		var courses =
    			bothCourseTCs.Select(x => Tuple.Create(
    				x.New,
    				coursesWithGroup.Contains(x.New),
					x.Old
    				)).ToList();
    		return courses;
    	}
	}
}