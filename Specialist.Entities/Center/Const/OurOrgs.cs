using System.Collections.Generic;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
    public static class OurOrgs {
        public const string Spec = "СПЕЦ";
        public const string Ru = "РУ";
        public const string CS = "ЦС";
        public const string Cos = "ЦОС";
	    public const string Bt = "БТ";

	    public const string CKO = "ЦКО";

		public static Dictionary<string, string> OrgOrderPostfix = new Dictionary<string, string> {
			{Spec, "С"},
			{Ru, "Р"},
			{Cos, "К"},
		}; 

	    public static HashSet<string> ForOrder = _.HashSet(Ru, Spec, Cos); 

    	public const string FullName =
    		"Образовательное частное учреждение дополнительного профессионального образования «Центр компьютерного обучения «Специалист» Учебно-научного центра при МГТУ им.Н.Э.Баумана»";

    	public const string LegalAddress = "105066, г.Москва, ул. Нижняя Красносельская, д.35, стр. 64";
		public static class Banks {
			public const byte Cos = 30;
		}


		public static Dictionary<string, int> CertTypes = new Dictionary<string, int>() {
//			{OurOrgs.Spec, 0},
			{OurOrgs.Cos, 1},
			{OurOrgs.Ru, 2}
		};

	    public const string RuName = "НОУ \"Специалист.РУ\"";
		public static Dictionary<string,string> Names = new Dictionary<string, string> {
			{Ru,RuName},
			{Spec,"ОЧУ \"Специалист\""},
			{Cos,"ОЧУ ДПО \"Специалист\""},
		}; 
    }
}