using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using MvcContrib.Attributes;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Web.Const;
using System.Linq;
using Specialist.Web.Common.Util;
using Specialist.Web.Util;

namespace Specialist.Web.Controllers
{
    public class MasterPageController : ExtendedController
    {
        [Dependency]
        public ICityService CityService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        [Dependency]
        public IAnnounceService AnnounceService { get; set; }

        [Dependency]
        public INewsService NewsService { get; set; }

        private void InitDictionaries(CityFilterVM model)
        {
            model.Cities = CityService.GetAll().OrderBy(c => c.SortOrder).ToList();
        }

     /*   [ValidateInput(false)]
        [ChildActionOnly]
        public virtual ActionResult CityFilter(bool main)
        {
            var model = new CityFilterVM();
            model.IsMain = main;
            InitDictionaries(model);
            model.GlobalCityTC = UserSettingsService.CityTC;
            return PartialView(PartialViewNames.CityFilter, model);
        }*/

	    [ValidateInput(false)]
        public virtual ActionResult Footer() {
            return PartialView(PartialViewNames.Footer, null);
        }



     /*   [HttpPost]
        [AjaxOnly]
        public virtual ActionResult CityFilter(string cityTC)
        {
            if(!Request.IsAjaxRequest())
                return null;
            UserSettingsService.CityTC = cityTC;
            return Json("done");
        }*/
    }
}