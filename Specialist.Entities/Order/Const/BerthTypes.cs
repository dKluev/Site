using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context.Const {
	public static class BerthTypes {
		public const string Paid = "���"; 
		public const string Kons = "����"; 
		public const string Plan = "����";
		public const string Nepos = "�����";
		public const string Garant = "�����";
		public const string Unlimit = "��";
		public const string Perevod = "�����";
		public const string Dvp = "���";

		public static HashSet<string> Hide = _.HashSet(Plan, Perevod, Nepos, Garant); 

	//	public const string CertTestPaid = Paid;	

		public const string NotPaid = "����"; //�� ��������

		public static readonly List<string> AllForCert = 
			"���,����,���,��,������,�����,�����,�����,����".Split(',').ToList(); 

		public static readonly List<string> AllPaidForCourses =
			_.List(
				"���",
				"�����",
				"�����",
				"����",
				"����",
				"�����",
				"����",
				"�����",
				Unlimit,
				Dvp,"�����", "�����", "�����", "����", "����");

		public static readonly List<string> AllPaidAndKonsForCourses =
			_.List(Kons).AddFluent(AllPaidForCourses);

		public static readonly List<string> AllPaidForTestCerts =
			_.List("���", "�����", "�����", Dvp);

		public static List<string> PaidReport = _.List("���", "����", "�����", Dvp);


		public static readonly List<string> AllPaid = _.List(
			"����",
			"���",
			"�����",
			"�����",
			"�����",
			"����",
			"����",
			"����",
			"���"
			).AddFluent(AllPaidForCourses);

	}
}
