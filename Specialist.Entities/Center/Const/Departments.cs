using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class Departments {
		public const string Ofl = "��";

		public const string Corp = "����";

		public static List<string> Order = _.List(Ofl, Corp);
	}
}