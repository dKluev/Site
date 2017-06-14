using System;
using System.Collections.Generic;

namespace Specialist.Web.Root {
	public class Api {
		public class StudentCourse {
			public string CourseName { get; set; }

			public string City { get; set; }

			public string Duration { get; set; }

			public string FinishDate { get; set; }
		}

		public class Student {

			public string Id { get; set; }

			public List<StudentCourse> Courses { get; set; }
		}
	}
}