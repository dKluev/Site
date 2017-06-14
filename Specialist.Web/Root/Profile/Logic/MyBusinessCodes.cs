using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Web.Root.Profile.Logic {
	public class MyBusinessCodes {
		public const string spec = "spec";
		public const string special = "special";
		public const string specialist = "specialist";

		public static readonly List<string> All = _.List(spec, special, specialist);
	}
}