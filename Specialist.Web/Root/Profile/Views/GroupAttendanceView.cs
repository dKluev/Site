using Specialist.Web.Common.Html;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.ViewModels;
using System.Linq;
using Specialist.Web.Const;
using Specialist.Web.Helpers;

namespace Specialist.Web.Root.Profile.Views {
	public class GroupAttendanceView:BaseView<GroupAttendanceVM> {
		public override object Get() {
			var lectureBlock = Model.Lectures.Any()
				? (object) H.div[Images.Attendances(),H.table.Class("table")[H.Head("Лекция","Посещаемость", "Оценка"),
				Model.Lectures.Select((x,i) => H.Row( (i+1),
					Images.Attendance(x.Attendance),x.Mark))]]
				: p["Пока информации нет"];
			var certBlock = Model.HasGroupCert
				? div[
					h3["Сертификат"],
					Images.GroupCertEng(Model.SigId,false,true).Style("margin-bottom:10px;"),
					br,
					Url.Graduate().DownloadCert(Model.SigId, true, false, true, "Скачать").Class("ui-button")
					]
				: null;
			return div[lectureBlock, certBlock];
		}
	}
}