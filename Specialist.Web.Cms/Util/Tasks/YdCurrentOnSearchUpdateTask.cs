using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Console;
using Newtonsoft.Json.Linq;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Tasks;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using Microsoft.Practices.Unity;

namespace Specialist.Web.WinService.Tasks {
	public class YdCurrentOnSearchUpdateTask : TaskWithTimer {
		DirectApiService DirectApiService = new DirectApiService();
		public override double Minutes {
			get { return 15; }
		}

	

		public override void TimerTick() {
			var service = Cms.MvcApplication.Container.Resolve<IRepository<YdBannerPhrase>>();
			var oldPhrases = service.GetAll().ToList();

			var bannerIds = oldPhrases.Select(x => x.BannerID).Distinct().ToList();
			var commonPharses = new List<Tuple<YdBannerPhrase, double, double, double>>();
			var newPhrases = 
				DirectApiService.GetBannerPhrasesFilter(bannerIds).AsJEnumerable()
				.Select(x => new YdBannerPhrase{
					PhraseID = x["PhraseID"].Value<int>(), 
					CampaignID = x["CampaignID"].Value<int>(), 
				    Phrase = x["Phrase"].Value<string>(),
				CurrentOnSearch = x["CurrentOnSearch"].Value<double>(),
				BannerID = x["BannerID"].Value<int>()
				});
			foreach (var newPhrase in newPhrases) {
				var oldPhrase = oldPhrases.FirstOrDefault(x => x.PhraseID == newPhrase.PhraseID &&
					x.BannerID == newPhrase.BannerID);
				if(oldPhrase != null) {
					var difference = newPhrase.CurrentOnSearch - oldPhrase.CurrentOnSearch;
					var data = Tuple.Create(oldPhrase, 
						oldPhrase.CurrentOnSearch, newPhrase.CurrentOnSearch, difference);
					commonPharses.Add(data);
					if(CheckForNotability(data))
						oldPhrase.CurrentOnSearch = newPhrase.CurrentOnSearch;
				}

				
			}
			service.SubmitChanges();
			var pharsesForMail = commonPharses.Where(CheckForNotability).ToList();
			if(pharsesForMail.Any()) {
				string mailBody;
				var inc = pharsesForMail.Where(x => x.Item4 > 0);
				mailBody = GetMailBody(inc, true);
				var desc = pharsesForMail.Where(x => x.Item4 < 0);
				mailBody += GetMailBody(desc, false);
				var mailService = Cms.MvcApplication.Container.Resolve<IMailService>();
				mailService.Send(new MailAddress("context@specialist.ru"), new MailAddress("motorina@specialist.ru"), 
					mailBody, "Фразы [+{0}][-{1}]".FormatWith(inc.Count(), desc.Count()));
			}
		}

		private bool CheckForNotability(Tuple<YdBannerPhrase, double, double, double> x) {
			return Math.Abs(x.Item4) >= 0.5;
		}

		private string directBannerUrl = "http://direct.yandex.ru/registered/main.pl?cmd=editBanner&bid={0}&cid={1}";
		private string yandexUrl = "http://yandex.ru/yandsearch?text=";

		private string GetMailBody(IEnumerable<Tuple<YdBannerPhrase, double, double, double>> pharsesForMail, bool increase) {
			string mailBody = null;
			if(pharsesForMail.Any()) {
				mailBody = (H.strong["[{0}]"].ToString() + H.br + pharsesForMail.OrderByDescending(x => x.Item4)
					.Select(x => {
						var pharseLink = H.Anchor(
							directBannerUrl.FormatWith(x.Item1.BannerID, x.Item1.CampaignID),
							x.Item1.Phrase);
						return pharseLink + ": " + x.Item2 + " -> " + x.Item3 + " [" + x.Item4 + "] " +
							H.Anchor(yandexUrl + x.Item1.Phrase, "yandex");
					})
					.JoinWith(H.br.ToString())).FormatWith(increase ? "+" : "-") + H.br;
			}
			return mailBody;
		}
	}
}