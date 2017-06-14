using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Examination.ViewModel;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Common.Extension;
using Specialist.Services.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Util;

namespace Specialist.Web.Controllers {
	public partial class VendorController : ExtendedController {
		[Dependency]
		public IVendorVMService VendorVMService { get; set; }


		[Dependency]
		public IVendorService VendorService { get; set; }

		[Dependency]
		public IExamService ExamService { get; set; }

		[HandleNotFound]
		public virtual ActionResult Details(string urlName, string urlPart, int? pageIndex) {
			VendorVM model;
			if (urlPart.IsEmpty()) {
				model = VendorVMService.GetBy(urlName, null);
				if(model == null)
					return null;
			}
			else if (urlPart == VendorVM.Tab.Testing) {
				if (!pageIndex.HasValue)
					return RedirectToAction(() => Details(urlName, urlPart, 1));
				var vendor = VendorService.GetAll().ByUrlName(urlName);
				if (vendor == null) {
					return NotFound();
				}
				var exams = new ExamListVM {
					Exams = ExamService.GetAll().Where(e => e.ExamPrice > 0 && e.Available)
						.Where(e => e.Vendor_ID == vendor.Vendor_ID)
						.OrderBy(e => e.Exam_TC)
						.ToPagedList(pageIndex.GetValueOrDefault() - 1),
					Vendor = vendor
				};
				model = new VendorVM(vendor) {
					CurrentTab = 2,
					Exams = exams
				};
			}
			else {
				var vendor = VendorService.GetAll().ByUrlName(urlName);
				if (vendor == null)
					return null;
				model = new VendorVM(vendor) {CurrentTab = 1};
			}

			return View(model);
		}
	}
}