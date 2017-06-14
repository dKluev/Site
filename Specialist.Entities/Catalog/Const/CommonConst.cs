using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Catalog
{
    public static class CommonConst {

	    public const int MaxNumOfWebinarists = 999;
	    public const string HideDiscount = "[HideDiscount]";

		public const string MainPageImage = "[MainPageImage]";

	    public const int DiplomHours = 250;

	    public const int LongCourseHours = 16;

	    public const decimal FirstPaymentPercent = (decimal) 0.25;

    	public static bool IsMobile = false;

	    public static bool IsForTest = false;

		public const string SiteRoot = "http://www.specialist.ru";
		public const string MobileRoot = "http://m.specialist.ru";
		public const string PureRoot = "www.specialist.ru";

	    public static string CurrentRoot {
		    get {
			    return IsMobile ? MobileRoot : SiteRoot;
		    }
	    }

        public const int NearestGroupCount = 15;

        public const int MessageCount = 10;

        public const int AnnounceCount = 5;

        public const int WebinarCount = 10;

        public const int NewsCount = 5;

        public const int AdminUserID = 301790;

        public const int MaxHotVacansyCount = 12;

        public const int CourseNameCount = 50;

        public const int AdviceCount = 1;

        public const int GroupForMainCount = 10;

    	public const int IpCameraBegin = 10;
    	public const int IpCameraEnd = 21;

	    public const int GoldFallDiscount = 20;


	    public static HashSet<int> MorningDiscount = _.HashSet(10,15,20);


    	public const int ConsultantBegin = 10;
    	public const int ConsultantEnd = 18;

    	public const string EmailForSite = "info@specialist.ru";

        public const string CourseTCList = "CourseTCList";
    }
}