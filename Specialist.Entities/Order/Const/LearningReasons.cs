using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Order.Const {
	public class LearningReasons {
		public const string Professional = "Профессиональная переподготовка";
		public const string Qualification = "Повышение квалификации";
		public const string Comprehensive = "Общеобразовательная программа";
		public const string Testing = "Тестирование";
		public const string Exam = "Экзамен";

		public static HashSet<string> HasEducation = _.HashSet(Qualification, Professional);
	}
}