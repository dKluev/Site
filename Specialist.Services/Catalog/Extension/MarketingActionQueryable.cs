using System;
using System.Linq;
using Specialist.Entities.Context;

namespace Specialist.Services.Catalog.Extension {
	public static class MarketingActionQueryable {
		public static IQueryable<MarketingAction> IsActiveByDate(this IQueryable<MarketingAction> banners) {
			var now = DateTime.Today;
			return banners
				.Where(
					b => ((b.DateEnd == null || b.DateEnd >= now)
						&& (b.DateBegin == null || b.DateBegin <= now))
					&& (b.DateBegin != null || b.DateEnd != null)
);
		}

		public static IQueryable<MarketingAction> IsNotActiveByDate(this IQueryable<MarketingAction> banners) {
			var now = DateTime.Today;
			return banners
				.Where(
					b =>!((b.DateEnd == null || b.DateEnd >= now)
						&& (b.DateBegin == null || b.DateBegin <= now))
				);
		}
	}
}