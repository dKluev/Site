using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using SimpleUtils.Collections;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Utils;
using Specialist.Web.Cms.Root.YandexDirect.Logic;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Cms.ViewModel;
using SimpleUtils.Extension;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Html;

namespace Console {
	public static class DirectApiBannerService {

		public const int MaxBannerCount = 1000;

		public static BannerPhraseInfo AsNewPhrase(BannerPhraseInfo bannerPhraseInfo) {
			bannerPhraseInfo.PhraseID = 0;
			bannerPhraseInfo.ContextPrice = null;
			bannerPhraseInfo.BannerID = null;
			return bannerPhraseInfo;
		}
		public static UpdateDirectVM CreateBanners(List<BannerInfo> banners, int? targetCampaignId) {
			if(!banners.Any())
				return new UpdateDirectVM();
			var bannerIds = banners.Select(x => x.BannerID).ToList();
			var created = CreateBanners(banners, bannerIds, targetCampaignId);
			var updated = UpdateBanners(banners, bannerIds);
			return new UpdateDirectVM {
				Created = created,
				Updated = updated.Item1,
				Archive = updated.Item2,
			};
		}
		public static string GetForecastFolder() {
			return HostingEnvironment.MapPath("~/temp/forecast/");
		}
		public static string GetForecastFile(int campaignId) {
			return GetForecastFolder() + "{0}-{1}.xls"
				.FormatWith(DateTime.Now.ToString("yy-MM-dd-HH-mm"),campaignId);
		}

		private static bool _forecastRun = false;
		static DirectApiService DirectApiService = new DirectApiService();
		static DirectApiService LiveDirectApiService = new DirectApiService(true);
		public static void WriteForecast(int campaingId) {
			if (_forecastRun) return;
			_forecastRun = true;
			var banners = DirectApiService.GetActiveBanners(
				new GetBannersInfo {
					GetPhrases = "WithPrices",
					CampaignIDS = new[] {campaingId}
				}).ToList();
			var phraseInfos = banners.SelectMany(x => x.Phrases)
				.Where(x => x.PremiumMin >= 5).ToList() ;
			var phrases = phraseInfos.Select(x => x.Phrase.ToLower()).Distinct().ToList();
			var forecast = GetForecast(phrases);

			var data =  phraseInfos.Select(pi => {
				var forecastPrice = forecast.GetValueOrDefault(pi.Phrase.ToLower());
				return _.List(
					pi.CampaignID.NotNullString(),
					pi.BannerID.NotNullString(),
					pi.Phrase,
					pi.PremiumMin.NotNullString(),
					forecastPrice.ToString(),
					(pi.PremiumMin - forecastPrice).GetValueOrDefault().ToString("n2"));
			}).ToList();
			var head = H.Head("Компания", 
				"Объявление",
				"Фраза",
				"Цена",
				"Прогноз",
				"Разница" );
			var table = H.table[head, data.Select(x => H.tr[x.Select(y => H.td[y])])].ToString();
			var fileName = GetForecastFile(campaingId);
			File.WriteAllText(fileName, table);

			_forecastRun = false;
		}

		public static Dictionary<string, float?> GetForecast(List<string> phrases) {
			DeleteAllForecasts();
			var rows = ListUtils.GetRows(phrases.Take(500).ToList(),100);
			var forecastIds = rows.Select(x =>
				DirectApiService.CreateNewForecast(
					new NewForecastInfo {Phrases = x.ToArray()})).ToList();
			while (DirectApiService.GetForecastList().All(x => x.StatusForecast != "Done")) {
				Thread.Sleep(30*1000);
			}
			return forecastIds.SelectMany(x => DirectApiService.GetForecast(x).Phrases)
				.ToDictionary(x => x.Phrase, x => x.PremiumMin);
		}

		static void DeleteAllForecasts() {
			var forecastStatusInfos = DirectApiService.GetForecastList();
			foreach (var forecastStatusInfo in forecastStatusInfos) {
				DirectApiService.DeleteForecastReport(forecastStatusInfo.ForecastID);
			}
		}

		public static UpdateDirectVM UpdateCampaign(int campaingId, int? targetCampaignId) {
			var draftId = 9471306;
			var newBannerIds = CreateDraftBanners(campaingId, draftId);
			var forCreate = CheckBanners(campaingId, draftId, newBannerIds);
			return CreateBanners(forCreate, targetCampaignId);
		}

		private static List<BannerInfo> CheckBanners(int campaingId, int draftId, List<long> newBannerIds) {
			var banners = DirectApiService.GetActiveBanners(
				new GetBannersInfo {
					GetPhrases = "WithPrices",
					CampaignIDS = new[] {campaingId}
				}).ToList();
			var newBanners =
				DirectApiService.GetBanners(
					new GetBannersInfo {
						GetPhrases = "WithPrices",
						CampaignIDS = new[] {draftId},
						BannerIDS = newBannerIds.ToArray()
					})
					.ToList();

			var forCreate = new List<BannerInfo>();
			foreach (var bannerInfo in newBanners) {
				var oldId = int.Parse(bannerInfo.Title);
				var oldBanner = banners.First(x => x.BannerID == oldId);
				var phrasesForCreate = new List<string>();
				foreach (var newPhrase in bannerInfo.Phrases) {
					var oldPhrase = oldBanner.Phrases
						.FirstOrDefault(x => x.Phrase == newPhrase.Phrase);
					if (oldPhrase != null && oldPhrase.PremiumMin > newPhrase.PremiumMin) {
						phrasesForCreate.Add(oldPhrase.Phrase);
					}
				}
				if (phrasesForCreate.Any()) {
					forCreate.Add(new BannerInfo {
						BannerID = oldId,
						CampaignID = campaingId,
						Phrases = phrasesForCreate.Select(x => new BannerPhraseInfo {
							Phrase = x
						}).ToArray()
					});
				}
			}
			DirectApiService.CallMethodOne<int>("DeleteBanners",
				new {
					CampaignID = draftId,
					BannerIDS =
						newBannerIds
				});
			return forCreate;
		}

		private static List<long> CreateDraftBanners(int campaingId, int draftId) {
			var banners2 = DirectApiService.GetActiveBanners(
				new GetBannersInfo {
					GetPhrases = "WithPrices",
					CampaignIDS = new[] {campaingId}
				}).Where(x => x.Phrases != null && x.Phrases.Any()).ToList();
			foreach (var bannerInfo in banners2) {
				bannerInfo.CampaignID = draftId;
				bannerInfo.Title = bannerInfo.BannerID.ToString();
				bannerInfo.BannerID = 0;
				foreach (var bannerPhraseInfo in bannerInfo.Phrases) {
					AsNewPhrase(bannerPhraseInfo);
				}
			}
			var newBannerIds = DirectApiService.CreateOrUpdateBanners(banners2.ToArray())
				.ToList();
			return newBannerIds;
		}

		private static Tuple<List<BannerInfo>, List<BannerInfo>> UpdateBanners(List<BannerInfo> banners, IEnumerable<long> bannerIds) {
			var newBanners = GetBanners(bannerIds);

			foreach (var newBanner in newBanners) {
				var banner = banners.First(x => x.BannerID == newBanner
					.BannerID);
				var phreses = banner.Phrases.Select(x => x.Phrase).ToList();
				newBanner.Phrases = newBanner.Phrases.Where(x => !phreses.Contains(x.Phrase)) 
					.ToArray();
			}
			var forUpdate = newBanners.Where(x => x.Phrases.Any()).ToArray();
			var forArchiveList = newBanners.Where(x => !x.Phrases.Any());
			var forArchive = forArchiveList.Select(x => x.BannerID).ToArray();
			if(forUpdate.Any()) {
				DirectApiService.CreateOrUpdateBanners(forUpdate);
			}

			if(forArchive.Any()) {
				var compainId = banners.First().CampaignID;
				DirectApiService.CallMethodOne<int>("StopBanners",
					new {CampaignID = compainId, BannerIDS = 
						forArchive});
				DirectApiService.CallMethodOne<int>("ArchiveBanners",
					new {CampaignID = compainId, BannerIDS = 
						forArchive});
			}
			return Tuple.Create(forUpdate.ToList(), forArchiveList.ToList());
		}

		static void CopyTags(Dictionary<long,long> oldNewBannerIds) {
			var oldBannerIds = oldNewBannerIds.Select(x => x.Key).ToArray();
			var bannerTags = LiveDirectApiService.GetBannersTags(new BannersRequestInfo {BannerIDS = oldBannerIds})
				.Where(x => x.TagIDS.Any()).ToList();
			foreach (var bannerTagsInfo in bannerTags) {
				bannerTagsInfo.BannerID = oldNewBannerIds[bannerTagsInfo.BannerID];
			}
			LiveDirectApiService.UpdateBannersTags(bannerTags.ToArray());

		}

		private static List<BannerInfo> CreateBanners(List<BannerInfo> banners, IEnumerable<long> bannerIds, 
			int? targetCampaignId) {
			var newBanners = GetBanners(bannerIds).ToList();

			var oldNewBanners = new Dictionary<long, BannerInfo>();
			foreach (var newBanner in newBanners) {
				var banner = banners.First(x => x.BannerID == newBanner
					.BannerID);
				var phreses = banner.Phrases.Select(x => x.Phrase).ToList();
				newBanner.Phrases = newBanner.Phrases.Where(x => phreses.Contains(x.Phrase))
					.Select(AsNewPhrase).ToArray();
				oldNewBanners.Add(newBanner.BannerID, newBanner);
				newBanner.BannerID = 0;
			}
			if (newBanners.Any()) {
				if (!targetCampaignId.HasValue) {
					var campaignId = newBanners.First().CampaignID;
					var bannerCount = CampaignBannerCount(campaignId);
					if (bannerCount + newBanners.Count > MaxBannerCount) {
						targetCampaignId = CloneCampaign(campaignId);
					}
				}
				if (targetCampaignId.HasValue) {
					newBanners.ForEach((x,i) => x.CampaignID = targetCampaignId.Value);
				}
				var result = DirectApiService.CreateOrUpdateBanners(newBanners.ToArray());
				result.ForEach((x,i) => newBanners[i].BannerID = x);

				if (!targetCampaignId.HasValue) {
					var oldNewBannerIds = oldNewBanners.ToDictionary(x => x.Key, x => x.Value.BannerID);
					CopyTags(oldNewBannerIds);
				}
			}
			return newBanners;
		}

		private static IEnumerable<BannerInfo> GetBanners(IEnumerable<long> bannerIds) {
			return DirectApiService.GetBanners(
				new GetBannersInfo {GetPhrases = "Yes", 
					BannerIDS = bannerIds.ToArray()}).ToList();
		}

		public static int CampaignBannerCount(int campaignId) {
			return DirectApiService.GetBanners(new GetBannersInfo {CampaignIDS = new[] {campaignId}}).Count();
		}

		public static int CloneCampaign(int origCampaignId) {
			var campaign = DirectApiService.GetCampaignsParams(
				new CampaignIDSInfo{CampaignIDS = new []{origCampaignId}}).First();
			campaign.CampaignID = 0;
			campaign.StartDate = null;
			campaign.Name = "[Clone]" + campaign.Name;
			campaign.Strategy.StrategyName = "LowestCostPremium";
			return DirectApiService.CreateOrUpdateCampaign(campaign);
		}
	}
}