using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Catalog.Const {
	public static class AuthorizationTypes {
		public const string Microsoft = "Ã—";
		public const string OneC = "1—";
		public const string Adobe = "¿ƒŒ¡";
		public const string Graphisoft = "√—";
		public const string Cisco = "÷»—";
		public const string Council = "≈— ";
		public const string Exin = "EXIN";
		public const string Pmi = "PMI";
		public const string Kaspersky = " ¿—œ";
		public const string Esk = "≈— ";
		public static HashSet<string> WithoutMOCert = _.HashSet(Exin, Pmi); 
		public static List<string> Expensive = _.List(Microsoft, Cisco, Council);

		public static byte GetSecondCourseDiscount(string tc) {
			var max = (byte)15;
			if (tc == null) {
				return max;
			}
			var d = SecondCourseDiscount.GetValueOrDefault(tc);
			return (d > 0 ? d : max);
		}

		public static Dictionary<string, byte> SecondCourseDiscount = new Dictionary<string, byte> {
			{Esk, 5},
			{Kaspersky,8 },
			{Cisco, 10 },
			{Microsoft, 13}
		};
	}
}