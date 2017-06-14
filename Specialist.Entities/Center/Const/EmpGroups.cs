using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const
{
    public static class EmpGroups
    {
        public const string Trainer = "ÏĞÅÏÎÄÛ";

        public static string Team = "ÒÈÌ";

        public const string Dismiss = "ÓÂÎËÅÍ";

	    public static List<string> Ofl = _.List( 
			"ÁÀÇÀ",
		    "ÈÍÔÎ",
		    "ÊÀÑÑÀ",
		    "ÊÎÍÑ",
		    "ĞÊ ÎÔË",
		    "ÑÁ",
		    "ÒÌ");

//	    public const string Svid = "ÑÂÈÄ";

	    public const string Sont = "ÑÎÍÒ";




	    /*  public const string Manager = "ÒÌ";

        public const string Vip = "ÂÈÏ";

        public static List<string> GetAllEmployees()
        {
            return new List<string>{Manager, Vip};
        }*/
    }
}