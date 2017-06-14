using System.Collections.Generic;
using Specialist.Entities.Utils;
using SimpleUtils.Collections.Extensions;
using System.Linq;

namespace Specialist.Entities.Catalog.Const
{
    public static class CourseTC {

    	public const string Test = "����";
        public const string Seminar = "���";
        public const string Kons = "����";
        public const string WebText = "�������";
	    public const string Itil = "����";
	    public const string Dba1 = "���1";

	    public const string DpCons = "������";

	    public const string ZaochSpec = "�����_����";

	    public static readonly List<string> SemSrt = _.List(Seminar, Srt);

	    public const string Time1 = "����1";

	    public const string Video = "��";

		public const string Depifr = "������";

		public const string ProbWeb = "�������";

    	public const string C8b1 = "1�8�1"; 
    	public const string C8b2 = "1�8�2"; 
    	public const string An1 = "��1"; 

        public const string Prometric = "����";

        public const string Srt = "���";
        public const string Sert = "����";

    	public const string Vue = "���";

	    public const string ArchiveBlue = "�����_�������";

        public const string MosRus = "���-���";

    	public const string Tor = "�-���";
    	public const string Tor1 = "���1";
    	public const string Tor2 = "���2";

    	public const string Tor48 = "�-���48";
    	public const string Tor16 = "���16";
    	public const string Tor32 = "���32";

	    public const string Dubli = "�����";
	    public const string Deleted = "�����_���������";
	    public const string Unlimit = "��";
	    public const string Sen = "���";

    	public const string Sks = "�-���";
    	public const string Sks1 = "���1";
    	public const string Sks2 = "���2";

    	public const string Torsh = "����";
    	public const string Torsh1 = "����1";
    	public const string Torsh2 = "����2";

	    public const string Podar = "�����";

	    public static List<string> TorUrls = _.List("t-tor", "torsh", "t-sks"); 
	    public static HashSet<string> NoSell = _.HashSet("������"); 

    	public const string ExcelA = "�-������-�";
    	public const string ExcelB = "�-������-�";

    	public const string Rukpord = "�������1";
    	public const string English = "�����1-�";

	    public const string BuhSem = "���1�83";

	    public const string ProbOz = "������";
	    public const string DpEkz = "�����";

	    public const string Erp1c = "1����";

	    public static HashSet<string> HideStudentCount = _.HashSet("1�820", "���1", "����-�"); 

			public static readonly Dictionary<string, List<string>> HalfTracks = new Dictionary<string, List<string>>{
			{Tor, _.List(Tor1,Tor2)},
			{Tor48, _.List(Tor16,Tor32)},
			{Sks, _.List(Sks1,Sks2)},
			{Torsh, _.List(Torsh1,Torsh2)}};

    	public static readonly List<string> HalfTrackCourses = HalfTracks.SelectMany(x => x.Value).ToList();


	    public static HashSet<string> CiscoPrepay = _.HashSet("����1-�", "����2-�");
        public static readonly List<string> Exams =
                new List<string>{MosRus, Prometric, Vue};

	    public static readonly List<string> NotMainPageParents = _.List("���", "���");

    	public static readonly List<string> AllSpecial;
    	public static readonly List<string> AllSpecialWithoutHalf;
    	public static readonly List<string> AllSpecialWithoutHalfAndSeminar;
		static CourseTC() {
			AllSpecialWithoutHalf = Exams.ToList()
				.AddFluent(Seminar)
				.AddFluent(Srt)
				.AddFluent(Kons)
				.AddFluent("����_��")
				.AddFluent("���-���")
				.AddFluent("���-���")
				.AddFluent("�������")
				.AddFluent(ProbOz);
			AllSpecialWithoutHalfAndSeminar =
				AllSpecialWithoutHalf.Where(x => x != Seminar).ToList();
				
			AllSpecial = AllSpecialWithoutHalf.ToList().AddFluent(HalfTrackCourses);

		}


	    public static readonly List<string> HideGroups = _.List(
		    "������1",
		    "������2",
		    "������5",
		    "�������3",
		    "������11",
		    "�������1",
		    "������12",
		    "��������");

		public static readonly Dictionary<string, string> WithRealParent = new Dictionary<string, string>{
			{"����11", "����"},
			{"������-�", "�����"},
			{"���", "���24"},
			{"����1", "�����"},
			{"����2010", "����2009"},
			{"�����-�", "����"},
			{"����-�", "����"},
			{"3��1-�", "3��8-1"},
			{"���1-�", "���"},
			{"���1", "�2000"},
			{"���2", "�2000"},
			{"������", "������"},
			{"������", "������"},
			{"������", "������"},
		};


	    public static List<string> MsVoucher = _.List(
		    "������1-�",
		    "������2-�",
		    "������3-�",
		    "���1-�",
		    "���2-�",
		    "����-�",
		    "����2-�",
		    "����131",
		    "��-�",
		    "����132");


	

    	public static readonly string[] TestTC =  { 
//"�-����-�",
//"���-�",
//"������-�",
"��1-�",
"��2-�",
"������2-�",
"�-�������-�"
//"����1-�",
//"����1-�",
//"�������1",
    };

    	public static readonly List<string> MsProject = _.List(
			"������",
"�5927",
"�5928",
"�5929",
"������",
"�����",
"����-�",
"������",
"���-�",
"���-�"

			);
    	public static readonly List<string> PM = _.List(
    		"�5929",
    		"�������",
    		"������",
    		"���",
    		"�����",
    		"���-�",
    		"�����1",
    		"������",
    		"�����",
    		"����-�",
    		"������",
    		"���-�",
    		"�����",
    		"�����",
    		"���-�"
    		);

    	public static readonly List<string> SqlRss =
    		"�10953 �50429 �10774 �10775�� �10776�� �10777 �10751�� �10750�� �6234 �6235 �6236 �50578A �2778-� �6231 �6232 �50400� �50401� �10175 �10232 ������2 ������� �10266 �10267 �10264 �10265 �10262 �10263 �10135 �10553� �10554� ������� �50466 �6368 �����1 ���� ���-� ���1 ���2-� ���3 ���1-� ���2-� ���3 ���4 ���-� ����-� ������ ���-� ���1-� ���2-� ����� �����-� ���1-� ���2-� ���3�� ���1 ���2 ���3 ����11 ������11 ���111 ���112 ���113 ������4 ������5 �����1-� �����2 �����3 1�821 1�82��� 1�823 1�820 1�821 1�82��� 1�823 ��� ���3 ��� �10778"
    			.Split(' ').ToList();
    	public static readonly List<string> OracleRss =
    		"����11 ������11 ����� ���111 ���112 ���113 ����� ����1 ����2 ����3 �����"
    			.Split(' ').ToList();


    	public static readonly List<string> SqlOracleRss =
    		"����11 ������11 ���111 ���112 ���113 ���� ����� ����1 ����2 ����3"
    			.Split(' ').Distinct().ToList();

	    public static readonly List<string> MsSeminars = _.List(
		    "�40025",
		    "�40005",
		    "�40026�",
		    "�40028",
		    "�40029",
		    "�40030�",
		    "�40031");
    }
}
