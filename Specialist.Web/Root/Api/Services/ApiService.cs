using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Services.Education;
using Specialist.Services.Core.Interface;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Root.Services {
	public class ApiService {

		[Dependency]
		public IRepository<StudentInGroup> StudentInGroupService { get; set; }

		public Api.Student GetStudentCourses(decimal studentId) {
			var studentCourses = StudentInGroupService
				.GetAll(x => x.Student_ID == studentId)
				.Where(x => x.Group != null 
					&& BerthTypes.AllPaidForCourses.Contains(x.BerthType_TC)).ToList()
				.Where(x => !x.Group.IsExam)
				.Select(sig => new Api.StudentCourse {
					City = sig.Group.BranchOffice.GetOrDefault(x => x.City)
						.GetOrDefault(x => x.Name),
					CourseName = sig.Group.Course.Name,
					Duration = sig.Group.DateInterval,
					FinishDate = sig.Group.GetOrDefault(x => x.DateEnd.Value).DefaultString()
				});
			return new Api.Student {Id = ((int)studentId).ToString(),
			Courses = studentCourses.ToList()};
		}
	}
}