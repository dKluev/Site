using System.Collections.Generic;
using Specialist.Web.Cms.Root.YandexDirect.Logic;

namespace Specialist.Web.Cms.ViewModel {
	public class DirectBannerPricesVM {
		public long? BannerId { get; set; } 
		public string Token { get; set; } 
		public int? CompanyId { get; set; } 
		public List<BannerPhraseInfo> Prices { get; set; }
	}
}