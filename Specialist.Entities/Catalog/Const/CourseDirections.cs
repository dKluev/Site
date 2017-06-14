using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Catalog.Const {
	public class CourseDirections {
		public const string School = "ШК_СТ";
		public const string English = "АНГЛ";
		public const string Office = "ОФИС";
		public const string Internet = "ИНТ";
		public const string BaseComp = "БКП";
		public const string Sap = "САП";

		public static List<string> Motorina = _.List(School, Office, Internet, BaseComp);

		public class Old {
		    public const string Seminar = "СЕМИНАР";
		}

	}
}