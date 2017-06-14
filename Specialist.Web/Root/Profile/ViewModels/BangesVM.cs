using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context.Const;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Const;
using Specialist.Web.Root.Profile.Logic;

namespace Specialist.Entities.Profile {
	public class BangesVM {
		public decimal LearningHours { get; set; }

		public int? TopBadge { get; set; }

		public bool HasUnlimit { get; set; }
		public string Best2016Url { get; set; }

		public List<Tuple<CdnFile, string, string, string, string>> GetBadges() {
			var badges = new List<Tuple<CdnFile, string, string, string, string>>();
			badges.AddRange(
				HoursBadge.AllHoursBadge.Select(x => {
					var isActive = LearningHours >= x.Item1;
					return Tuple.Create(
						Cdns.ImageBadgeHours(x.Item1 + (isActive ? "" : "-gray") + ".png"),
						x.Item2, isActive ? Cdns.ImageBadgeHours(x.Item1 + "-b.png").Url : null, "HourBadge", (string) null);
				}));
			if (TopBadge.HasValue) {
				badges.Add(Tuple.Create(Cdns.ImageBadgeTop(TopBadge + ".jpg"),
					"Топ {0}% слушателей".FormatWith(TopBadge),
					Cdns.ImageBadgeTop(TopBadge + "-b.png").Url, "TopBadge", (string) null));
			}
			badges.Add(Tuple.Create(Cdns.ImageBadgeUnlimit("unlimit{0}.png"
				.FormatWith(HasUnlimit ? "" : "-gray")),
				"Безлимитное обучение", (string) null, "TopBadge", SimplePages.FullUrls.Unlimited));
			if (Best2016Url != null) {
				badges.Add(Tuple.Create(Cdns.ImageBadgeBest("2016.jpg"),
					CommonTexts.Best2016, (string) null, "Best2016Badge", Best2016Url));
			}
			return badges;
		}
	}
}