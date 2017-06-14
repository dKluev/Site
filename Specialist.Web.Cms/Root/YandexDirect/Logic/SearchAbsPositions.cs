using System;

namespace Specialist.Web.Cms.Root.YandexDirect.Logic {
	[Serializable]
	public class SearchAbsPositions {
		public DateTime Date { get; set; }

		public int PhraseId { get; set; }

		public string Sites { get; set; }

		public SearchAbsPositions(DateTime date, int phraseId, string sites) {
			Date = date;
			PhraseId = phraseId;
			Sites = sites;
		}
	}
}