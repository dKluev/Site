using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public class TeacherRatings {

		public static string GetLetter(double r) {
			return list.First(x => x.Item1 <= r).Item2;
		}

		public static List<Tuple<double, string>> list = _.List(
			Tuple.Create(4.75, "А"),
			Tuple.Create(4.25, "АБ"),
			Tuple.Create(3.75, "Б"),
			Tuple.Create(3.25, "БВ"),
			Tuple.Create(2.75, "В"),
			Tuple.Create(2.25, "ВГ"),
			Tuple.Create(1.75, "Г"),
			Tuple.Create(1.25, "ГД"),
			Tuple.Create(0.0, "Д")
			);
	}
}