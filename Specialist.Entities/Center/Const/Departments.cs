using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class Departments {
		public const string Ofl = "тк";

		public const string Corp = "йнпо";

		public static List<string> Order = _.List(Ofl, Corp);
	}
}