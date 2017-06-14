using Microsoft.SqlServer.Server;

namespace Specialist.Entities.Education {
	public class CourseFileVM {
		public string Name { get; set; }
 
		public string Url { get; set; }

		public string CourseTC { get; set; }

		public string UserName { get; set; }

		public string EmployeeTC { get; set; }


		public CourseFileVM(string name, string url, string courseTc, string userName, string employeeTc) {
			Name = name;
			Url = url;
			CourseTC = courseTc;
			UserName = userName;
			EmployeeTC = employeeTc;
		}
	}
}