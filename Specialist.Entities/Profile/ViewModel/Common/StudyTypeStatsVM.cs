using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Profile.ViewModel.Common {
	public class StudyTypeStatsVM : IViewModel {
		public int Intra { get; set; }

		public int Webinar { get; set; }

		public int IntraExtra { get; set; }

		public int Open { get; set; }

		public int Unlimit { get; set; }

		public List<List<object>> GetData() {
			return _.List(
				_.List<object>("Очное", Intra, "#196AFF", SimplePages.FullUrls.Fulltime),
				_.List<object>("Вебинар", Webinar, "#FFA500", SimplePages.FullUrls.Webinar),
				_.List<object>("Открытое", Open, "#15C900", SimplePages.FullUrls.OpenClasses),
				_.List<object>("Безлимитное", Unlimit, "#BD19FF", SimplePages.FullUrls.Unlimited),
				_.List<object>("Очно-заочное", IntraExtra, "#FF0023",
					SimplePages.FullUrls.IntraExtramural)).OrderByDescending(x => x[1]).ToList();

		}

		public string Title {
			get { return "Статистика часов обучения по типу"; }
		}
	}
}