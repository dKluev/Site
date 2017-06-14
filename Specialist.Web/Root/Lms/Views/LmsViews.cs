using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Lms;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;

using bh = Specialist.Web.Common.Html.BootHtmls;
namespace Specialist.Web.Root.Lms.Views {
	public static class LmsViews {
		public static object NoData() {
			return bh.Info("Данных пока нет");
		}

		public static object UrlWithPassword(string url, string lgn, string pwd) {
			return H.p[H.b["Логин: "], lgn, H.br, H.b["Пароль: "], pwd, H.br,
				H.b["Ссылка: "], H.Anchor(url)];
		}

		public static object SupportInfo
		{
			get {
				return bh.Warning(
				@"В первую очередь по всем вопросам обращайтесь к <b>администратору и инженерам комплекса.</b> <br/>
		Дополнительно:<br/>
		Техническая и организационная подержка в центральном офисе у менеджеров по вебинарам: <b>{0}</b>, <b>{1}</b> <br/>
		Техническая поддержка по выходным: <b>{2}</b>".FormatWith("+7 (495) 780-48-48 доб. 221", H.Email("webinarmanager@specialist.ru"), H.Email("tag-engineers@specialist.ru")));	
			}
		} 
		public static object NotesView(List<LmsStudentNote> note) {
			if (note == null || !note.Any()) {
				return null;
			}
		    return bh.Table( 
			    note.Select(n => H.Row(H.td[n.Notes].Style("width:100%;"), 
				    H.Raw(n.LastChangeDate.DefaultWithTimeString().Replace(" ", "&nbsp;")), n.LastChanger_TC)));
	    }
	}
}