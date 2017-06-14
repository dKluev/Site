using System.Linq;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Utils.Files;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Lms;

namespace Bing.Views {
	public class LectureFilesView: BaseView<LectureFilesVM> {
		 
		public override object Get() {
			var form = Form(Url.Info().Urls.GroupFiles(null))[
				LabeledTextInput("№ Группы", x => x.GroupId),
				SaveButton("Показать")];
			return div[form, Table()];
		}

		TagTable Table() {
			if (!Model.Lectures.Any()) {
				return null;
			}
			return table[Head("№", "Занятие"), Model.Lectures.Select(x => {
				var file = Model.Files.FirstOrDefault(y => x.Lecture_ID == y.Lecture_ID);
				var content = file != null
					? LectureFiles.GetFile(file.Id)
						.Anchor(x.LectureDateBeg.DefaultString()).ToString()
					: x.LectureDateBeg.DefaultString();
				return Row(x.Lecture_ID, content);
			})].Class("table");

		}
	}
}