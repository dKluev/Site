using System.Web.Mvc;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using Specialist.Entities.ViewModel;
using Specialist.Services;
using Specialist.Services.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using Specialist.Services.Common.Extension;
using Specialist.Web.Util;

namespace Specialist.Web.Controllers
{
    public class CertificationController : ExtendedController
    {
        [Dependency]
        public ICertificationcVMService CertificationcVMService { get; set; }

        [Dependency]
        public ICertificationService CertificationService { get; set; }

        public virtual ActionResult Details(string urlName)
        {
            var model = CertificationcVMService.GetByUrlName(urlName);
			if(model == null)
				return NotFound();
			if (model.IsMicrosoft) {
				ViewData[Htmls.AdditionalStyle] = "new-certification";
			}
			
            return View(model);
        }

		[AjaxOnly]
        public virtual ActionResult CertificationNames(string query)
        {
            var names = CertificationService.GetAll().IsActive().Where(c => c.Name.Contains(query))
                .Take(10)
                .Select(c => c.Name);
            return Json(new { query, suggestions = names }, JsonRequestBehavior.AllowGet);
        }
        

		[AjaxOnly]
        public virtual ActionResult CertificationList(string name)
        {

            var model = CertificationService.GetAll().IsActive().Where(c => c.Name.Contains(name))
                .Take(10).ToList();
            return View(model);
        }
    }
}