using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class DaySequences {
		public const string Saturday = "СБ"; //Суббота
		public const string Sunday = "ВС"; //Воскресенье
		public const string Highday = "ПР";
		
		public static string GetName(string tc) {
			switch(tc)
			{
				case Saturday:
					return "Суббота";
				case Sunday:
					return "Воскресенье";
				case Highday:
					return "Праздничные выходные дни";
				default:
					return string.Empty;

			}
		}

		public static readonly List<string> GetAll =
			_.List(Saturday, Sunday, Highday);

		
	}
}