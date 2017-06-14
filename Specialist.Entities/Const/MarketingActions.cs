using System.Collections.Generic;
using System.Net.Configuration;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const
{
    public static class MarketingActions
    {
        public const string Birthday = "Birthday";

	    public const int Best2016 = 308;

        public const string NewVersionCourse = "NewVersionCourse";

        public const string HotGroup = "HotGroup";

        public const string Reserve = "Reserve";

	    public const int GoldFall = 140;

	    public const int Panasonic = 216;
	    public const int Unlimit = 256;

	    public static string GetPanasonicPromocode(bool isReal) {
		    return isReal ? "PanaSpecialist" : "SPECIALIST";
	    }

	    //    	public static readonly List<int> Microsofts = _.List(16, 27, 35, 36, 41,43,48, 60, 50, 61, 62);
    }
}
