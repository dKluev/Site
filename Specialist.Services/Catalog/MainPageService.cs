using System;
using System.Globalization;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using System.Linq;
using Specialist.Services.Interface.SitePart;
using Specialist.Services.UnityInterception;
using Specialist.Services.Common.Extension;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;

namespace Specialist.Services.Catalog
{
    public class MainPageService : IMainPageService
    {
        [Dependency]
        public IVendorService VendorService { get; set; }

        [Dependency]
        public INewsService NewsService { get; set; }

        [Dependency]
        public IRepository<Profession> ProfessionService { get; set; }

        [Dependency]
        public IRepository<Product> ProductService { get; set; }

        [Dependency]
        public IRepository<SiteTerm> SiteTermService { get; set; }

        [Dependency]
        public IBannerService BannerService { get; set; }

        [Dependency]
        public IRepository<MarketingAction> MarketingActionService { get; set; }

        [Dependency]
        public IRepository<CertType> CertTypeService { get; set; }

		[Dependency]
        public IRepository2<Video> VideoService { get; set; }

        [Dependency]
        public ISectionService SectionService { get; set; }

		 [Dependency]
        public ISimplePageService SimplePageService { get; set; }

		[Cached]
        public virtual MainPageVM Get()
        {
            var sections =
                SectionService.GetAll().IsActive().Where(s => s.ForMainPage)
				.ByWebOrder().ToList();


            var professions = ProfessionService.GetAll().IsActive()
                .Where(p => p.ForMainPage).ToList()
				.OrderBy(x => StringUtils.IsBasicLetter(x.Name.First())).ThenBy(x => x.Name).ToList();
            var vendors = VendorService.GetAll().IsActive()
                .Where(p => p.ForMainPage).ByWebOrder().ToList();
            var products = ProductService.GetAll().IsActive()
                .Where(p => p.ForMainPage).ToList().OrderBy(x => x.Name).ToList();
            var siteterms = SiteTermService.GetAll().IsActive()
                .Where(p => p.ForMainPage).ToList().OrderBy(x => x.Name).ToList();
			VideoService.LoadWith(x => x.VideoCategory);
			var videos = VideoService.GetAll(x => 
				x.CategoryId == VideoCategories.CoursePresentation)
				.OrderByDescending(x => x.UpdateDate).Take(3).ToList();
            return 
                new MainPageVM
                {
                    Professions = professions,
                    Vendors = vendors,
                    Sections = sections,
                    Products = products,
					Banners = BannerService.GetBanner(CommonConst.SiteRoot + "/")
						.ShufflePriority(x => x.Priority),
					News =  NewsService.GetAllForMain().ToList(),
					SiteTerms = siteterms,
					Videos = videos,
                    Documents = CertTypeService
						.GetAll(c => CertTypes.ForMain.Contains(c.UrlName)).ToList(),
					About = SimplePageService.GetAll().BySysName(SimplePages.MainPage)
                };
        }
    }
}
