using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Catalog {
	public class CoursePrerequisiteService: Repository2<CoursePrerequisite> {
		public CoursePrerequisiteService(IContextProvider contextProvider) : base(contextProvider) {}

		public IQueryable<CoursePrerequisite> GetForCourse(string courseTC) {
			return this.GetAll(x => x.Course_TC == courseTC && x.Test_ID != null)
				.OrderBy(x => x.SortOrder).ThenBy(x => x.Test_ID);
		} 

		public IQueryable<int> GetTestIds(string courseTC) {
			return GetForCourse(courseTC).Select(x => (int)x.Test_ID.Value);
		} 
	}
}