using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Tests.Consts {
	public static class TestRecomendations {

		public const int EnglishTestWithLevels = 599;

		public static List<Tuple<int,int, string, List<string>>> EnglishLevels = 
			_.List(
				Tuple.Create(0, 3, "Beginner", _.List("АН1")),
				Tuple.Create(4, 6, "Elementary", _.List("АН2", "АН3")),
				Tuple.Create(7, 13, "Pre-Intermediate", _.List("АН4", "АН5")),
				Tuple.Create(14, 23, "Intermediate", _.List("АН6", "АН7")),
				Tuple.Create(24, 28, "Upper-intermediate", _.List("АН8", "АН9")),
				Tuple.Create(29, int.MaxValue, "Advanced", _.List("АН10"))
			);

		public static string GetLevel(short score) {
			return EnglishLevels.First(x => x.Item2 >= score).Item3;
		}

		private static List<string> level5 = _.List("ТОЕФЛ-А");
		private static List<string> level4 = _.List("АНГЛИ1-А","АНГИНТ1","АНГ1");
		private static List<string> level3 = _.List("АНГЛПИ1-А");
		private static List<string> level2 = _.List("АНГЛЭ1-А");
		private static List<string> level1 = _.List("АНГЛЭ0");

		public const int oldTest = 452;
		public const int newTest = 599;

		private static HashSet<int> EnglishTests = new HashSet<int>(_.List(oldTest, newTest));

		public static bool IsEnglishTest(int testId) {
			return EnglishTests.Contains(testId);
		}

		public static readonly Dictionary<int, Dictionary<int, List<string>>> Tests =

			_.Dict(oldTest,
				_.Dict(26, level5)
					.AddFluent(18, level4)
					.AddFluent(12, level3)
					.AddFluent(6, level2)
					.AddFluent(0, level1)

				).AddFluent(newTest,
				EnglishLevels.AsEnumerable().Reverse().ToDictionary(x => x.Item1, x => x.Item4)
				);

		/*.AddFluent(330, 
			_.Dict(41, level5)
			.AddFluent(33, level4)
			.AddFluent(16, level3)
			.AddFluent(7, level2)
			.AddFluent(0, level1)
			)*/

		/*
		{
	
		}  
*/
	}
}