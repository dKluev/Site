using System;
using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Lms.Const {
	public class FinalExamMarks {

		public static Tuple<string, string> Absence = Tuple.Create("НЕЯВ", "Не явился");

		public const string Pass = "ЗЧТ";

		public static List<String> MOCert = _.List(
			"ОТЛ",
			"ХОР",
			"УДВ",
			Pass);

		public static List<Tuple<string, string>> PassList = _.List(Tuple.Create(Pass, "Зачёт"), Tuple.Create("НЕЗАЧ", "Незачёт"), Absence);
		public static List<Tuple<string, string>> MarkList = _.List(
			Tuple.Create("ОТЛ", "Отлично"),
			Tuple.Create("ХОР", "Хорошо"),
			Tuple.Create("УДВ", "Удовлетворительно"),
			Tuple.Create("НЕУД", "Неудовлетворитльно"),
			Absence);
	}
}