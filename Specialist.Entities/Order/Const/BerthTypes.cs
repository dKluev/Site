using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context.Const {
	public static class BerthTypes {
		public const string Paid = "нок"; 
		public const string Kons = "йнмя"; 
		public const string Plan = "окюм";
		public const string Nepos = "меоня";
		public const string Garant = "цйоеп";
		public const string Unlimit = "ан";
		public const string Perevod = "моепб";
		public const string Dvp = "дбо";

		public static HashSet<string> Hide = _.HashSet(Plan, Perevod, Nepos, Garant); 

	//	public const string CertTestPaid = Paid;	

		public const string NotPaid = "мнок"; //мЕ НОКЮВЕМН

		public static readonly List<string> AllForCert = 
			"нок,оняр,дбо,ан,анкюир,ондюп,ондйк,тхплю,бюсв".Split(',').ToList(); 

		public static readonly List<string> AllPaidForCourses =
			_.List(
				"нок",
				"ондюп",
				"днонн",
				"оняр",
				"аюпр",
				"внокп",
				"внок",
				"цйонд",
				Unlimit,
				Dvp,"тхплю", "ондйк", "цоднц", "бюсв", "вдбо");

		public static readonly List<string> AllPaidAndKonsForCourses =
			_.List(Kons).AddFluent(AllPaidForCourses);

		public static readonly List<string> AllPaidForTestCerts =
			_.List("нок", "цйонд", "ондюп", Dvp);

		public static List<string> PaidReport = _.List("нок", "внок", "внокп", Dvp);


		public static readonly List<string> AllPaid = _.List(
			"внок",
			"пег",
			"днцнм",
			"днонн",
			"ондюп",
			"йнмя",
			"йпед",
			"оняр",
			"цяя"
			).AddFluent(AllPaidForCourses);

	}
}
