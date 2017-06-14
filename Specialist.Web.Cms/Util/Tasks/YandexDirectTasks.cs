using System;
using System.Collections.Generic;
using System.Linq;
using Console;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Utils;
using Specialist.Web.Cms.Root.YandexDirect.Logic;
using Specialist.Web.Common.Utils.Tasks;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Specialist.Services.Catalog.Extension;
using Microsoft.Practices.Unity;
using SimpleUtils.Extension;
using Specialist.Entities.Utils;

namespace Specialist.Web.WinService.Tasks {
	public class YandexDirectTasks : TaskWithTimer {
		private const int TitleLimit = 30;
		private const int TextLimit = 66;
		DirectApiService DirectApiService = new DirectApiService(true);

		public override double Minutes {
			get { return 24*60; }
		}

		public override void TimerTick() {
			if (IsTodayDone) return;
			new AdsDirectService().UpdateAllBannersHrefs();
		}

		public void UpdateText() {
			var groupService = Cms.MvcApplication.Container.Resolve<IRepository<Group>>();
			var bannerCourses =  YandexDirectTextUtils.Banners.GroupBy(x => x.Item2)
				.ToDictionary(x => x.Key, x => x.SelectMany(y => y.Item1).Distinct());
			var courses = bannerCourses.SelectMany(x => x.Value).ToArray();
			var groupInfos = groupService.GetAll().PlannedAndNotBegin()
				.Where(x => x.DateBeg >= DateTime.Today 
					&&  courses.Contains(x.Course_TC)).ByCity(Cities.Moscow)
				.GroupBy(x => x.Course_TC).ToDictionary(x => x.Key, gr => {
					var dateTime = gr.Min(x => x.DateBeg);
					if(dateTime > DateTime.Today.AddDays(14))
						dateTime = null;
					return new {
						Discount = gr.Max(x => x.Discount),
						Date = dateTime
					};
				});
			var bannerIds = bannerCourses.Select(x => x.Key).ToArray();
			var banners = DirectApiService.GetActiveBanners(new GetBannersInfo{BannerIDS = bannerIds});
			var bannersForUpdate = new List<BannerInfo>();
			foreach (var banner in banners) {
				if(banner.BannerID == 11780704)
					continue;
				if(banner.CampaignID.In(8095,920306,1688161))
					continue;
				var courseTCs = bannerCourses[banner.BannerID];
				var groupInfo = courseTCs
					.Select(courseTC => groupInfos.GetValueOrDefault(courseTC))
					.Where(x => x != null && (x.Date.HasValue || x.Discount.HasValue))
					.OrderByDescending(x=> x.Discount).ThenBy(x => x.Date).FirstOrDefault();
				if(groupInfo == null) {
					var firstLink = banner.Sitelinks.FirstOrDefault();
					if (firstLink != null) {
						ClearBannerText(firstLink);	
						if (!firstLink.Title.IsEmpty() && firstLink.Title != " ") {
							bannersForUpdate.Add(banner);
						}
					}
					continue;
				}
				if(banner.Sitelinks.Any()) {
					var orderTitle = banner.Sitelinks[0].Title;
					var otherBannerLength = banner.Sitelinks.Skip(1).Sum(x => x.Title.Length);
					var textLimit = TextLimit - otherBannerLength;
					textLimit = Math.Min(textLimit, TitleLimit);
					UpdateText(banner.Sitelinks[0],
						groupInfo.Date, groupInfo.Discount, textLimit);
					if(orderTitle != banner.Sitelinks[0].Title) {
						bannersForUpdate.Add(banner);
						WL(banner.Sitelinks[0].Title);
					}
					
				}

			}
			bannersForUpdate = bannersForUpdate.Where(x => !x.Sitelinks.Any() || x.Sitelinks
				.All(y => y.Title.Length <= TitleLimit)).ToList();
			UpdateBanners(bannersForUpdate);
			log = string.Empty;
		}

		private void UpdateBanners(List<BannerInfo> bannersForUpdate) {
			if(!bannersForUpdate.Any())
				return;
			var count = bannersForUpdate.Count/100 + 1;
			for (int i = 0; i < count; i++) {
				var banners = bannersForUpdate.Skip(100*i).Take(100);
				if(!banners.Any())
					return;
				AddPhrases(banners);
				foreach (var bannerInfo in banners) {
					foreach (var phraseInfo in bannerInfo.Phrases) {
						phraseInfo.ContextPrice = null;
					}
				}
				DirectApiService.CreateOrUpdateBanners(banners.ToArray());
				
			}
		}

		private void AddPhrases(IEnumerable<BannerInfo> banners) {
			var bannerIds = banners.Select(x => x.BannerID).ToArray();
			var phrases = DirectApiService.GetBannerPhrases(bannerIds);
			foreach (var banner in banners) {
				banner.Phrases = phrases.Where(x => x.BannerID == banner.BannerID).ToArray();
			}
		}

		private string log = string.Empty;

		private void WL(object obj) {
			log += obj + Environment.NewLine;
		}

		private void UpdateText(Sitelink link, DateTime? date, 
			short? discount, int textLimit) {
			ClearBannerText(link);
			string autoText;

			if(discount.HasValue) {
				autoText = YandexDirectTextUtils.GetDiscountText(discount.Value, 
						textLimit - link.Title.Length);
				link.Title += autoText;
				return;
			}
			if(date.HasValue) {
				autoText = YandexDirectTextUtils.GetDateText(date.Value, 
					textLimit - link.Title.Length);
				link.Title += autoText;
			}

		}

		private void ClearBannerText(Sitelink sitelink) {
			if(sitelink == null)
				return;
//			banner.Title = YandexDirectTextUtils.RemoveDateAndDiscount(banner.Title);
//			banner.Text = YandexDirectTextUtils.RemoveDateAndDiscount(banner.Text);
			sitelink.Title = 
					YandexDirectTextUtils.RemoveDateAndDiscount(sitelink.Title);
		}

		public void UpdateActiveBannders() {
//			var testSite = 782792;
			var bannersForUpdate = new List<BannerInfo>();
			var bannerForStop = new List<BannerInfo>();
			var compaignIDs = DirectApiService.GetActiveCompaignIDs();
			foreach (var compaigns in compaignIDs.GetRows(10)) {
				if(!compaigns.Any())
					break;
				var banners = DirectApiService.GetActiveBanners(new GetBannersInfo{CampaignIDS = compaigns.ToArray() });
//				result += banners.OrderBy(x => x.CampaignID).Where(x => x.Sitelinks.Any(y => y.Title.Length > 30))
//					.Select(x => x.CampaignID + " " + x.BannerID + " " + 
//						x.Sitelinks.Where(z => z.Title.Length > 30)
//						.Select(z => z.Title).JoinWith("; ")).JoinWith(Environment.NewLine);


				var badBanners = _.List<long>(272867448, 11780704,242879828,242504496,242504529, 242504531, 242504540);
				foreach (var banner in banners) {
					if(badBanners.Contains(banner.BannerID) || 
						banner.Phrases == null || banner.Phrases.Any(x => x.Price.GetValueOrDefault() < 0.01))
						continue;
					var old = banner.Href;
					if(YandexDirectTextUtils.UpdateHref(banner)) {
						WL(old);
						bannersForUpdate.Add(banner);
						WL(banner.Href);
					}
					if (DisableBanner(banner)) {
						bannerForStop.Add(banner);
						WL(banner.Href);
					}
				}
			}
			foreach (var bannerInfo in bannersForUpdate) {
				if(bannerInfo.ContactInfo != null && bannerInfo.ContactInfo.Street == null
					&& bannerInfo.ContactInfo.PointOnMap != null)
					bannerInfo.ContactInfo.PointOnMap = null;
			}

			UpdateBanners(bannersForUpdate);
			StopBanners(bannerForStop);

		
			log = string.Empty;

		}
		
		void StopBanners(List<BannerInfo> banners) {
			if (banners.Any()) {
				if (banners.Count <= 25) {
					foreach (var item in banners.GroupBy(x => x.CampaignID)) {
						DirectApiService.StopBanners(new CampaignBidsInfo {
							CampaignID = item.Key,
							BannerIDS = item.Select(x => x.BannerID).ToArray()
						});
					}
					var body = banners.Select(YandexDirectTextUtils.BannerLink).JoinWith("<br/>");
					var mailService = Cms.MvcApplication.Container.Resolve<IMailService>();
					mailService.Send(MailService.info, MailService.marketers, body, "Объявления без групп",
						MailService.ptolochko);

				}
				else {
					Logger.Exception(new Exception("count = " + banners.Count), "stop banners");
				}

			}
		}

		private HashSet<string> _coursesWithGroup = null;

		HashSet<string> CoursesWithGroup {
			get {
				if (_coursesWithGroup == null) {
					var groupService = Cms.MvcApplication.Container.Resolve<IRepository2<Group>>();
					var halfYear = DateTime.Today.AddMonths(6);
					var courses = groupService.GetAll(x => x.DateBeg >= DateTime.Today
						&& x.DateEnd <= halfYear).Select(x => x.Course.UrlName).Distinct().ToList();
					_coursesWithGroup = new HashSet<string>(courses.Where(x => x != null).Select(x => x.ToLower()));
				}
				return _coursesWithGroup;
			}
		} 
 

		public bool DisableBanner(BannerInfo banner) {
			var urlName = YandexDirectTextUtils.GetUrlName(banner.Href);
			if (CourseTC.TorUrls.Contains(urlName))
				return false;
			if (!urlName.IsEmpty()) {
				return !CoursesWithGroup.Contains(urlName);
			}
			return false;
		}

	}
}
