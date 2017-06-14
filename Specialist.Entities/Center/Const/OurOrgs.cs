using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
    public static class OurOrgs {
        public const string Spec = "����";
        public const string Ru = "��";
        public const string CS = "��";
        public const string Cos = "���";
	    public const string Bt = "��";

	    public const string CKO = "���";

		public static Dictionary<string, string> OrgOrderPostfix = new Dictionary<string, string> {
			{Spec, "�"},
			{Ru, "�"},
			{Cos, "�"},
		}; 

	    public static HashSet<string> ForOrder = _.HashSet(Ru, Spec, Cos); 

    	public const string FullName =
    		"��������������� ������� ���������� ��������������� ����������������� ����������� ������ ������������� �������� ����������� ������-�������� ������ ��� ���� ��.�.�.�������";

    	public const string LegalAddress = "105066, �.������, ��. ������ ��������������, �.35, ���. 64";
		public static class Banks {
			public const byte Cos = 30;
		}


		public static Dictionary<string, int> CertTypes = new Dictionary<string, int>() {
//			{OurOrgs.Spec, 0},
			{OurOrgs.Cos, 1},
			{OurOrgs.Ru, 2}
		};

	    public const string RuName = "��� \"����������.��\"";
		public static Dictionary<string,string> Names = new Dictionary<string, string> {
			{Ru,RuName},
			{Spec,"��� \"����������\""},
			{Cos,"��� ��� \"����������\""},
		}; 
    }
}