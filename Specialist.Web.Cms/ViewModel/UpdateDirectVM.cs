using System.Collections.Generic;
using Specialist.Web.Cms.Root.YandexDirect.Logic;

namespace Specialist.Web.Cms.ViewModel {
	public class UpdateDirectVM {
		public List<BannerInfo> Created { get; set; }
		public List<BannerInfo> Updated { get; set; }
		public List<BannerInfo> Archive { get; set; }
		public int? CampaignId { get; set; }
		public int? TargetCampaignId { get; set; }

		public UpdateDirectVM() {
			Created = new List<BannerInfo>();
			Updated = new List<BannerInfo>();
			Archive = new List<BannerInfo>();
		}
	}
}