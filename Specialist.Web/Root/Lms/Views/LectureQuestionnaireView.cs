using System.Linq;
using Microsoft.SqlServer.Server;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Reflection.Extensions;
using Specialist.Entities.Lms;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;

namespace Specialist.Web.Root.Lms.Views {
	public class LectureQuestionnaireView: BaseView<LectureQuestionnaireVM> {

		public TagDiv Marks(string name, char? value) {
			return div[LectureQuestionnaireLetter.All.Select(x => 
				label[InputRadio(name, x.Letter.ToString()).SetChecked(x.Letter == value), x.Name]
				.Class("radio-inline"))];
		}

		public TagDiv MarkBlock(string text, string name, bool isGroup = false) {
			var prefix = isGroup ? "Group." : "Lecture.";
			var entity = isGroup ? (object)Model.Group : Model.Lecture;
			var markName = name + "Letter";
			var commentName  = name + "Comment";
			var mark = (char?)entity.GetValue(markName);
			var comment = entity.GetValue(commentName).NotNullString();
			return div[label[text], Marks(prefix + markName,mark), 
				textarea.Name(prefix + commentName).Rows(1).Class("form-control")[comment]].Class("form-group");

		}

		private string classRoom = 
			"Как бы Вы оценили своевременность и качество подготовки помещения класса к занятию (чистота, доска, проветривание)?";

		private string equipment =
			"Как бы Вы оценили своевременность и качество подготовки имеющейся в классе техники к занятию (компьютеры, проектор, работа Интернета и т.д.)?";

		private string feeding = "Как бы Вы оценили своевременность и качество питания (обеды, кофе-брейки)?";

		private string administration =
			"Как бы Вы оценили быстроту решения возникающих в процессе обучения проблем администрацией?";

		private string documents =
			"Как бы Вы оценили подготовку администрацией документов для слушателей (свидетельства, сертификаты, счета-фактуры)?";

		private string schedule = "Как бы Вы оценили качество составления расписания (удобство для Вас и для слушателей)?";

		public override object Get() {

			var form = AjaxForm(Url.Lms().Urls.LectureQuestionnairePost(null))[
				HiddenFor(x => x.Lecture.LectureQuestionnaire_ID),
				HiddenFor(x => x.Lecture.Lecture_ID),
				HiddenFor(x => x.Group.Group_ID),
				HiddenFor(x => x.Group.GroupQuestionnaire_ID),
				MarkBlock(classRoom, "ClassRoom"),
				MarkBlock(equipment, "Equipment"),
				MarkBlock(feeding, "Feeding"),
				MarkBlock(administration, "Administration",true),
				MarkBlock(documents, "Documents",true),
				MarkBlock(schedule, "Schedule",true),
				Model.HasPermission ? BootHtmls.SubmitButton("Сохранить") : null];
			return div[form].ToString();

		}
	}
}