using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using Specialist.Entities.Examination.Const;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Common.Extension;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;

namespace Specialist.Services.Catalog
{
    public class NewsService: Repository<News>, INewsService
    {
        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        public NewsService(IContextProvider contextProvider) : base(contextProvider) {}

        public IQueryable<News> GetAllForMain()
        {
            return GetAll().Where(n => n.IsActive && n.ForMainPage).OrderByDescending(n => 
                n.PriorityOrder).ThenByDescending(n => n.PublishDate);
        }

        public List<News> GetFor(object entity) {
        	var provider = entity as Provider;
			if(provider != null) {
				var vendorId = Providers.GetVendor(provider.Provider_ID);
				if(vendorId == null)
					return new List<News>();
				entity = new Vendor {Vendor_ID = vendorId.Value};
			}
			var news = SiteObjectService.GetByRelationObject<News>(entity);
	        var showEverywhere = GetActiveNews(this.GetAll(x => x.ShowEverywhere));
	        return GetActiveNews(GetActiveNews(news).Concat(showEverywhere).AsQueryable());
        }

	    private static List<News> GetActiveNews(IQueryable<News> news) {
		    var month5 = DateTime.Today.AddMonths(-3);
		    return news
			    .Where(x => x.PublishDate >= month5 && x.IsActive)
			    .OrderByDescending(n => n.PublishDate)
			    .Take(CommonConst.NewsCount).ToList();
	    }
    }
}