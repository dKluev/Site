using System.Collections.Generic;
using System.Linq;

namespace Specialist.Web.Root.Lms {
	public class HoursVM {
		public Dictionary<string, double> Courses { get; set; }
		public double Ind { get; set; }
		public double NoMoscow { get; set; }
		public double Spec { get; set; }

		public HoursVM() {
			Courses = new Dictionary<string, double>();
		}

		public double Total {
			get {
				return Courses.Sum(x => x.Value);
			}
		}
		 
	}
}