using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Helpers {
	public static class TrackPageHtmls {
		public static string TrackPrice(this CommonCourseListItemVM courseListItemVM, string priceType, decimal? newPrice) {
			var realFullPrice = courseListItemVM.GetTrackFullPrice(priceType);
			var pt = PriceTypes.IsCorp(priceType) ? PriceTypes.Corporate : PriceTypes.Main; 
			var ppPrice = courseListItemVM.GetTrackFullPrice(pt);
			var intraExtraFullPrice = newPrice*((decimal) 1.8);
			var fullPrice = PriceTypes.IsIntraExtra(priceType)
				? (ppPrice > 0 && ppPrice <= intraExtraFullPrice ? 
				OrderDetail.FloorToFifty(((decimal)ppPrice) * (decimal) 0.95) : intraExtraFullPrice)
				: realFullPrice;
		   return Htmls2.OldNewPrice(fullPrice, newPrice);
		} 
	}
}
