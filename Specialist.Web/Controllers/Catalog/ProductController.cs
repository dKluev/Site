using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Services.Center;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Const;
using Specialist.Web.Util;

namespace Specialist.Web.Controllers
{
    public class ProductController : ExtendedController
    {
        [Dependency]
        public IRepository<Product> ProductService { get; set; }

        [Dependency]
        public IUserWorkService UserWorkService { get; set; }

		[HandleNotFound]
        public virtual ActionResult Details(string urlName)
        { 
			if(!urlName.EndsWith(UrlName.ProductPostfix))
				return this.RedirectToAction<ProductController>(c =>
					c.Details(urlName + UrlName.ProductPostfix),true);
            var product = ProductService.GetAll()
                .ByUrlName(urlName.Replace(UrlName.ProductPostfix, string.Empty));
            return MView(ViewNames.CommonEntityPage, product);
        }

		public ActionResult UserWorks() {
			
			return View(PartialViewNames.UserWorkBlock, 
				UserWorkService.GetAllRandomForBlock().Take(5).ToList());
		}
    }
}