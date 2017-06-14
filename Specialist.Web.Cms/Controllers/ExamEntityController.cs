using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Examination;
using Specialist.Web.Cms.Core;
using Specialist.Web.Cms.Core.ViewModel;

namespace Specialist.Web.Cms.Controllers {
	public class ExamEntityController : BaseController<Exam> {
		[Dependency]
		public ExamImportService ExamImportService { get; set; }


		[HttpPost]
		public ActionResult StartImport() {
			Task.Factory.StartNew(() => ExamImportService.Import());
			return null;
		}
		[HttpPost]
		public ActionResult UpdatePrices() {
			Task.Factory.StartNew(() => {
			ExamImportService.WriteStatus("Обновление начато", null, 0, 0);
			ExamImportService.UpdateExamPricesFromProviderPrices(true);
			ExamImportService.IsComplete = false;
			ExamImportService.IsStarted = false;
			ExamImportService.IsCheckPrice = false;
			ExamImportService.WriteStatus("Обновление завершено", null, 0, 0);
			}
				);
			return null;
		}

		public ActionResult Status() {
			if (ExamImportService.IsComplete && !ExamImportService.IsCheckPrice)
				return Content("done");
			var status = ExamImportService.Status ?? new Tuple<string, int>(null,0);
			return Json(new {percent = status.Item2, text = status.Item1});
		}
	}
}