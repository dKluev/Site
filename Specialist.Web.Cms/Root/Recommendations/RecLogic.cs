using System;
using System.Collections.Generic;
using Console;
using Specialist.Entities.Context;
using System.Linq;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Cms.Root.Recommendations {
	public class RecLogic {
		public void CreateDB() {
			var courseStudentData = new SpecialistDataContext().StudentInGroups
				.Select(x => new {x.Student_ID, x.Group.Course.ParentCourse_TC}).ToList();
			var courseStudents = courseStudentData.Select(x => new CourseStudent {
				CourseTC = x.ParentCourse_TC,
				StudentId = x.Student_ID
			});
			var context = new RecRepository().GetContext();
			context.AddMany(courseStudents);


		} 


		public void CreateNames() {
			var courses = new SpecialistDataContext().Courses
				.Select(x => new {x.Course_TC, x.Name}).ToList();
			var names = courses.Select(x => new CourseName() {
				CourseTC = x.Course_TC,
				Name = x.Name
			});
			var context = new RecRepository().GetContext();
			context.AddMany(names);


		} 

		public void CreateCoefs() {
			var courseTCs = new SpecialistDataContext().Courses.Where(x => x.IsActive)
				.Select(x => x.ParentCourse_TC).Distinct().ToList();
			var context = new RecRepository().GetContext();
			context.DeleteMany<CourseCoef>(c => true);
			var pairs = courseTCs.SelectMany(x => courseTCs.Select(y => Tuple.Create(x, y)))
				.Where(x => string.Compare(x.Item1,x.Item2) > 0).ToList();
			var i = 0;
			var courseStudents = context.All<CourseStudent>().ToList()
				.GroupByToDictionary(x => x.CourseTC, x => x.StudentId);
			var cache = new List<CourseCoef>();
			foreach (var p in pairs) {
				i++;
				if(i%100 == 0)
					System.Console.WriteLine(i);
				var x1 = courseStudents.GetValueOrDefault(p.Item1);
				var x2 = courseStudents.GetValueOrDefault(p.Item2);
				if(x1 == null || x2 == null)
					continue;
				var a = x1.Count;
				var b = x2.Count;
				var c = x1.Intersect(x2).Count();
				if(c < 10)
					continue;
				var k = c*decimal.One*Math.Max(a,b)/((a + b - c)*Math.Min(a,b));
				var ck = new CourseCoef {CourseTC = p.Item1, CourseTC2 = p.Item2, Coef = k*100,
				Count = c};
				cache.Add(ck);
			}
			context.AddMany(cache);
		}

		public void Delete() {
			var context = new RecRepository().GetContext();
			context.DeleteMany<CourseStudent>(x => x.CourseTC.Contains("Архив"));

		}
	}
}