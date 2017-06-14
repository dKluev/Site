using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Reflection;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Utils;
using Specialist.Web.Root.Graduate.ViewModels;

namespace Specialist.Web.Root.Graduate.Services {
	public class StudentRatingService: Repository2<StudentRating> {
		public StudentRatingService(IContextProvider contextProvider) : base(contextProvider) {}

		public T GetForStudent<T>(decimal studentID, Expression<Func<StudentRating, T>> selector) {
			return this.FirstOrDefault(x => x.Student_ID == studentID 
				&& !x.Org_ID.HasValue
				&& x.CourseDirection_TC.Equals(null), selector);
		}


		public TopStudentListVM GetTopStudents() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var data = GetTopStudents(null);
				return new TopStudentListVM {
					List = data,
				};
			});
		}

		public List<string> GetTopStudents(decimal? orgId) {
			var data = this.GetAll(x => x.CourseDirection_TC.Equals(null)
				&& Equals(x.Org_ID, orgId))
				.OrderBy(x => x.Rating)
				.Select(x => x.Rating + ". " +
					x.Student.LastName + " " + x.Student.FirstName + " " + x.Student.MiddleName)
				.Take(TopStudentListVM.TopCount).ToList();
			return data;
		}
	}
}