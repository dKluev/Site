using System;
using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Order.Const {
	public class EducDocTypes {
		public const string High = "В";
		public const string Secondary = "СП";
		public const string Learning = "С";



		public static List<Tuple<string, string>> All = _.List(
			Tuple.Create("", "Ни одно из перечисленного"),
			Tuple.Create(High, "Высшее образование"),
			Tuple.Create(Secondary, "Cреднее профессиональное образование"),
			Tuple.Create(Learning, "В данное время учусь на высшее или среднее профессиональное образование")
		); 

	}
}