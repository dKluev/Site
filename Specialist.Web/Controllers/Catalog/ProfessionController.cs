using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.ActionFilters;
using Specialist.Web.Const;
using System.Linq.Dynamic;
using Specialist.Services.Common.Extension;
using Specialist.Web.Util;

namespace Specialist.Web.Controllers
{
    public partial class ProfessionController : ExtendedController
    {
        [Dependency]
        public IRepository<Profession> ProfessionService { get; set; }

        [Dependency]
        public IRepository2<Course> CourseService { get; set; }
        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

		[HandleNotFound]
        public virtual ActionResult Details(string urlName)
        {
           
            var model = ProfessionService.GetAll().ByUrlName(urlName);
            return MView(ViewNames.CommonEntityPage, model);
        }

        public ActionResult Professions(string courseTC)
        {
            var model = SiteObjectService.GetSingleRelation<Profession>(
				new Course{Course_TC = courseTC}).ToList();
            if (model.Count() == 0)
                return null;
        	ViewBag.CourseName = CourseService.GetValues(courseTC, x => x.WebName);
            return View(model);
        }


    }
}