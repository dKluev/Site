using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Lms;

namespace Specialist.Web.Root.Lms {
	public class LectureQuestionnaireVM: IViewModel {
		public bool HasPermission { get; set; }

		public PiLectureQuestionnaire Lecture { get; set; }
		public PiGroupQuestionnaire Group { get; set; }

		public string Title {
			get {
				return "Анкета преподавателя " + Lecture.Lecture_ID;
			}
		}
	}
}