using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Web.Cms.Root.YandexDirect.Logic {
	public static class Phrases {
		public static List<string> ForCompetitors = new List<string> {
			"курсы менеджеров по персоналу",
			"курсы управления персоналом",
			"бухгалтерские курсы",
			"курсы бухгалтеров",
			"курсы 1С"
		};

		public static List<string> YandexCompetitors = _.List(
			"бухгалтерские курсы",
			"курсы бухгалтеров",
			"курсы 1с",
			"курсы бухгалтерского учета",
			"курсы 1с бухгалтерия",
			"курсы 1с торговля и склад",
			"курсы 1с предприятие",
			"курсы 1с зарплата и кадры"
			); 

		public static List<string> ForWordStat = _.List(
			"курсы",
			"обучение",
			"курсы бухгалтеров",
			"бухгалтерские курсы",
			"курсы 1с");
	}
}