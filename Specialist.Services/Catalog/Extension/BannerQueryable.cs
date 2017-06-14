using System;
using System.Linq;
using Specialist.Entities.Context;

namespace Specialist.Services.Catalog.Extension {
	public static class BannerQueryable {
		public static IQueryable<Banner> IsActiveByDate(this IQueryable<Banner> banners) {
			var now = DateTime.Now;
			return banners
				.Where(
					b => ((b.ActualDate == null || b.ActualDate > now)
						&& (b.DateBegin == null || b.DateBegin < now))
						&& (b.DateBegin != null || b.ActualDate != null)
				);
		}

		public static IQueryable<Banner> IsNotActiveByDate(this IQueryable<Banner> banners) {
			var now = DateTime.Now;
			return banners
				.Where(
					b =>!((b.ActualDate == null || b.ActualDate > now)
						&& (b.DateBegin == null || b.DateBegin < now))
				);
		}
	}
}