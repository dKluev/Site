using System;
using System.Collections.Generic;

namespace Specialist.Web.Cms.Root.YandexDirect.Logic {
	[Serializable]
	public class SeCompetitor {

		public DateTime Date { get; set; }

		public int PhraseId { get; set; }

		public List<string> Direct { get; set; }

		public List<string> YandexSearch { get; set; }

		public List<string> AdWords { get; set; }

		public List<string> GoogleSearch { get; set; }

		public SeCompetitor(DateTime date, int phraseId, List<string> direct, List<string> yandexSearch, List<string> adWords, List<string> googleSearch) {
			Date = date;
			PhraseId = phraseId;
			Direct = direct;
			YandexSearch = yandexSearch;
			AdWords = adWords;
			GoogleSearch = googleSearch;
		}
	}
}