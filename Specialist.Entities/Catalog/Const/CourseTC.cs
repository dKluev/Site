using System.Collections.Generic;
using Specialist.Entities.Utils;
using SimpleUtils.Collections.Extensions;
using System.Linq;

namespace Specialist.Entities.Catalog.Const
{
    public static class CourseTC {

    	public const string Test = "реяр";
        public const string Seminar = "яел";
        public const string Kons = "йнмя";
        public const string WebText = "беареяр";
	    public const string Itil = "хрхк";
	    public const string Dba1 = "даю1";

	    public const string DpCons = "дойнмя";

	    public const string ZaochSpec = "гюнвм_яоеж";

	    public static readonly List<string> SemSrt = _.List(Seminar, Srt);

	    public const string Time1 = "рюил1";

	    public const string Video = "бй";

		public const string Depifr = "дхохтп";

		public const string ProbWeb = "опнабеа";

    	public const string C8b1 = "1я8а1"; 
    	public const string C8b2 = "1я8а2"; 
    	public const string An1 = "юм1"; 

        public const string Prometric = "оплр";

        public const string Srt = "япр";
        public const string Sert = "яепр";

    	public const string Vue = "бсе";

	    public const string ArchiveBlue = "юпухб_цнксашу";

        public const string MosRus = "лня-пся";

    	public const string Tor = "р-рнп";
    	public const string Tor1 = "рнп1";
    	public const string Tor2 = "рнп2";

    	public const string Tor48 = "р-рнп48";
    	public const string Tor16 = "рнп16";
    	public const string Tor32 = "рнп32";

	    public const string Dubli = "дсакх";
	    public const string Deleted = "юпухб_сдюкеммше";
	    public const string Unlimit = "ан";
	    public const string Sen = "яем";

    	public const string Sks = "р-яйя";
    	public const string Sks1 = "яйя1";
    	public const string Sks2 = "яйя2";

    	public const string Torsh = "рнпь";
    	public const string Torsh1 = "рнпь1";
    	public const string Torsh2 = "рнпь2";

	    public const string Podar = "ондюп";

	    public static List<string> TorUrls = _.List("t-tor", "torsh", "t-sks"); 
	    public static HashSet<string> NoSell = _.HashSet("дойнмя"); 

    	public const string ExcelA = "р-щйяекэ-ю";
    	public const string ExcelB = "р-щйяекэ-а";

    	public const string Rukpord = "псйондп1";
    	public const string English = "юмцкщ1-ю";

	    public const string BuhSem = "яел1я83";

	    public const string ProbOz = "опнанг";
	    public const string DpEkz = "дощйг";

	    public const string Erp1c = "1яепо";

	    public static HashSet<string> HideStudentCount = _.HashSet("1я820", "даю1", "ойюд-ю"); 

			public static readonly Dictionary<string, List<string>> HalfTracks = new Dictionary<string, List<string>>{
			{Tor, _.List(Tor1,Tor2)},
			{Tor48, _.List(Tor16,Tor32)},
			{Sks, _.List(Sks1,Sks2)},
			{Torsh, _.List(Torsh1,Torsh2)}};

    	public static readonly List<string> HalfTrackCourses = HalfTracks.SelectMany(x => x.Value).ToList();


	    public static HashSet<string> CiscoPrepay = _.HashSet("хжмд1-ю", "хжмд2-ю");
        public static readonly List<string> Exams =
                new List<string>{MosRus, Prometric, Vue};

	    public static readonly List<string> NotMainPageParents = _.List("айо", "рпй");

    	public static readonly List<string> AllSpecial;
    	public static readonly List<string> AllSpecialWithoutHalf;
    	public static readonly List<string> AllSpecialWithoutHalfAndSeminar;
		static CourseTC() {
			AllSpecialWithoutHalf = Exams.ToList()
				.AddFluent(Seminar)
				.AddFluent(Srt)
				.AddFluent(Kons)
				.AddFluent("ябхд_лн")
				.AddFluent("япр-яхя")
				.AddFluent("япр-рнп")
				.AddFluent("опнабеа")
				.AddFluent(ProbOz);
			AllSpecialWithoutHalfAndSeminar =
				AllSpecialWithoutHalf.Where(x => x != Seminar).ToList();
				
			AllSpecial = AllSpecialWithoutHalf.ToList().AddFluent(HalfTrackCourses);

		}


	    public static readonly List<string> HideGroups = _.List(
		    "бятепю1",
		    "бятепю2",
		    "бятепю5",
		    "бюпебэч3",
		    "бятепю11",
		    "бюпебэч1",
		    "бятепю12",
		    "пейнбепх");

		public static readonly Dictionary<string, string> WithRealParent = new Dictionary<string, string>{
			{"нпяй11", "нпяй"},
			{"щиврлк-а", "юьрлк"},
			{"бел", "бел24"},
			{"хжмд1", "хмрпн"},
			{"юйюд2010", "юйюд2009"},
			{"ажлям-ю", "ябхв"},
			{"аяжх-ю", "пнср"},
			{"3дл1-г", "3дл8-1"},
			{"дол1-б", "дол"},
			{"айо1", "а2000"},
			{"айо2", "а2000"},
			{"хрюкоб", "хрюкоб"},
			{"хрюкот", "хрюкот"},
			{"хрюкоп", "хрюкоп"},
		};


	    public static List<string> MsVoucher = _.List(
		    "щйяекэ1-ю",
		    "щйяекэ2-ю",
		    "щйяекэ3-б",
		    "юйя1-ю",
		    "юйя2-д",
		    "юксй-ц",
		    "бнпд2-ю",
		    "лйьо131",
		    "оо-е",
		    "лйьо132");


	

    	public static readonly string[] TestTC =  { 
//"р-беад-х",
//"беа-ю",
//"щиврлк-ц",
"ть1-к",
"ть2-к",
"щйяекэ2-б",
"р-ялеопнт-а"
//"яерх1-ю",
//"хжмд1-ю",
//"псйондп1",
    };

    	public static readonly List<string> MsProject = _.List(
			"йкокюм",
"л5927",
"л5928",
"л5929",
"окюмхп",
"пеяок",
"соой-а",
"сопюдл",
"соп-д",
"соя-б"

			);
    	public static readonly List<string> PM = _.List(
    		"л5929",
    		"люпйпсй",
    		"окюмхп",
    		"оло",
    		"пеяок",
    		"плх-б",
    		"яйпюл1",
    		"собпел",
    		"сойюв",
    		"соой-а",
    		"сопюдл",
    		"соп-д",
    		"сопея",
    		"сопнц",
    		"соя-б"
    		);

    	public static readonly List<string> SqlRss =
    		"л10953 л50429 л10774 л10775юб л10776юб л10777 л10751юб л10750юб л6234 л6235 л6236 л50578A л2778-а л6231 л6232 л50400ю л50401ю л10175 л10232 дпсоюк2 юмдпнхд л10266 л10267 л10264 л10265 л10262 л10263 л10135 л10553ю л10554ю бхмдтнм л50466 л6368 юитнм1 юъйя улк-ю юйя1 юйя2-ц юйя3 пмп1-ю пмп2-ю пмп3 пмп4 улк-ю дфяй-ю нопбеа бел-б пмп1-ю пмп2-ю ляйбк нопнц-ю дфб1-ю дфб2-ю дфб3ее дек1 дек2 дек3 нпяй11 нпокяй11 нпя111 нпя112 нпя113 щйяекэ4 щйяекэ5 охрнм1-ю охрнм2 охрнм3 1я821 1я82гюо 1я823 1я820 1я821 1я82гюо 1я823 рон рон3 яем л10778"
    			.Split(' ').ToList();
    	public static readonly List<string> OracleRss =
    		"нпяй11 нпокяй11 нпмюя нпя111 нпя112 нпя113 нпяол нпах1 нпах2 нпах3 опхлю"
    			.Split(' ').ToList();


    	public static readonly List<string> SqlOracleRss =
    		"нпяй11 нпокяй11 нпя111 нпя112 нпя113 нпол нпмюя нпах1 нпах2 нпах3"
    			.Split(' ').Distinct().ToList();

	    public static readonly List<string> MsSeminars = _.List(
		    "л40025",
		    "л40005",
		    "л40026ю",
		    "л40028",
		    "л40029",
		    "л40030ю",
		    "л40031");
    }
}
