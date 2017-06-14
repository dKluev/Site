using System.Web.Mvc;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Web.Const;

namespace Specialist.Web.Root.Lms {
	public static class LmsLinks {
		public static TagA WebinarLauncher(UrlHelper url, decimal? lectureId, string courseTC) {
			return url.Lms().WebinarLauncher(lectureId, "Файл .ahk для запуска вебинара " + courseTC);
		}
		 
	}
}