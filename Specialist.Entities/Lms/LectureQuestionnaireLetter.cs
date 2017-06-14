using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Lms {
	public class LectureQuestionnaireLetter {
		public char? Letter { get; set; }

		public string Name { get; set; }
		public LectureQuestionnaireLetter(char? letter, string name) {
			Letter = letter;
			Name = name;
		}

		public static List<LectureQuestionnaireLetter> All = _.List(
			new LectureQuestionnaireLetter('А', "Отлично"),
			new LectureQuestionnaireLetter('Б', "Хорошо"),
			new LectureQuestionnaireLetter('В', "Посредственно"),
			new LectureQuestionnaireLetter('Г', "Плохо"),
			new LectureQuestionnaireLetter(null, "Не оценено")
			);

	}
}