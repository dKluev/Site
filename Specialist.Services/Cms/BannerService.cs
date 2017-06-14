using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.SitePart;
using Specialist.Services.Common.Extension;
using SimpleUtils.Collections.Extensions;
using Specialist.Services.Catalog.Extension;

namespace Specialist.Services.SitePart
{
    public class BannerService:IBannerService
    {
        SpecialistWebDataContext context = new SpecialistWebDataContext();

        public List<Banner> GetBanner(string url) {
            var banners = context.Banners
                .Where(b => ("," + b.Pages + ",").Contains("," + url + ","))
                .IsActive()
                .ToList();
            return banners;
        }
		public Banner GetMainPageImageBanner() {
			var banner = context.Banners
				.Where(b => b.Pages == CommonConst.MainPageImage)
				.IsActive()
				.FirstOrDefault();
            return banner;
        }

		public Banner GetSideBanner() {
			var banners = context.Banners
				.Where(b => b.IsSide)
				.IsActive()
				.ToList();
			var banner = banners.Random(b => b.Priority);
            return banner;
        }
    }
}