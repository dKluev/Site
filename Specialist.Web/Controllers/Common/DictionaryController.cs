using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Reflection.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.Common.Extension;
using System.Linq;
using Specialist.Services.Interface;
using SimpleUtils.Reflection;
using Specialist.Web.ActionFilters;
using Specialist.Web.Const;
using Specialist.Web.Util;

namespace Specialist.Web.Controllers
{
    public partial class DictionaryController : ExtendedController
    {
        [Dependency]
        public IRepository<SiteTerm> SiteTermService { get; set; }

        [Dependency]
        public IRepository<Product> ProductService { get; set; }

        [Dependency]
        public IRepository<Profession> ProfessionService { get; set; }

        [Dependency]
        public IVendorService VendorService { get; set; }

		[Dependency]
		public ICourseListVMService CourseListVMService { get; set; }

		[HandleNotFound]
        public virtual ActionResult Definition(string urlName)
        {
            var model = SiteTermService.GetAll().ByUrlName(urlName);
			if(model == null)
				return null;
			if(model.SiteTerm_ID == Sections.Terms.Webinar)
				ViewBag.Seminars = CourseListVMService.ProbWebinars();
            return MView(ViewNames.CommonEntityPage, model);
        }

        public virtual ActionResult List()
        {
            var result = new List<object>();
            result.AddRange(SiteTermService.GetAll().IsActive().Cast<object>());
            result.AddRange(ProductService.GetAll().IsActive().Cast<object>());
            result.AddRange(ProfessionService.GetAll().IsActive().Cast<object>());
            result.AddRange(VendorService.GetAll().IsActive().Cast<object>());
            return View(result.OrderBy(o => o.GetValue("Name")));
        }
    }
}