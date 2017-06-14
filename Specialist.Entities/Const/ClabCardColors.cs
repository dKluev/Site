using System.Collections.Generic;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class ClabCardColors {

		public const string Blue = "A";
		public const string Silver = "S";
		public const string Gold = "G";
		public const string Platinum = "P";
		public const string Diamond = "D";

		public static List<string> WebinarRecord = _.List(Platinum, Diamond); 

		public static readonly Dictionary<string, int> ColorCounts =
			new Dictionary<string, int> {
				{Blue,3},
				{Silver,5},
				{Gold,7},
				{Platinum, 10},
				{Diamond, 15}
			};
		public static readonly Dictionary<string, int> ColorIds =
			new Dictionary<string, int> {
				{Blue,1},
				{Silver,2},
				{Gold,3},
				{Platinum, 4},
				{Diamond, 5}
			};
		public static readonly Dictionary<string, int> ColorDiscounts =
			new Dictionary<string, int> {
				{Blue,7},
				{Silver,10},
				{Gold,15},
				{Platinum, 20},
				{Diamond, 20}
			};

		public static int? ColorCount(string tc) {
			return ColorCounts.GetValueOrDefault(tc);
		}
		public static string NextColor(string tc) {
			switch (tc) {
				case "A":
					return "S";
				case "S":
					return "G";
				case "G":
					return "P";
				case "P":
					return "D";
				default:
					return null;
			}
			
		}

		public static string GetName2(string tc) {
			switch (tc) {
				case "A":
					return "Синяя";
				case "S":
					return "Серебряная";
				case "G":
					return "Золотая";
				case "P":
					return "Платиновая";
				case "D":
					return "Бриллиантовая";
				default:
					return "Безымянный";
			}
		} 

		public static string GetName3(string tc) {
			switch (tc) {
				case "A":
					return "Синию";
				case "S":
					return "Серебро";
				case "G":
					return "Золото";
				case "P":
					return "Платину";
				case "D":
					return "Бриллианты";
				default:
					return "Безымянный";
			}
		} 

		public static string GetName(string tc) {
			switch (tc) {
				case "A":
					return "Синий";
				case "S":
					return "Серебряный";
				case "G":
					return "Золотой";
				case "P":
					return "Платиновый";
				case "D":
					return "Бриллиантовый";
				default:
					return "Безымянный";
			}
		} 
	}
}