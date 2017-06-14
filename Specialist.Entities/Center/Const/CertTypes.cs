using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class CertTypes {
		public const string InterName = "Международный сертификат";
		public const string Common = "Cert_Common";

		public const string State = "Cert_State";

		public const string Inter = "Cert_Inter";

		public const string Diplom = "Cert_diplom";

		public const string Microsoft = "Cert_Microsoft";
		public const string Buh = "БУХ";
		public const string Retraining = "ПЕРЕПОДГОТОВКА";

		public const string OldCert = "О";
		public const string U16 = "U16";
		public const string Y16 = "Y16";
		public static HashSet<string> ForCert = _.HashSet(OldCert, U16, Y16);

		public static class Full {
			public static CertType Diplom = new CertType {
				Name =
					"<b>Диплом о профессиональной<br/> переподготовке установленного образца</b>",
				UrlName = CertTypes.Diplom
			};
			public static CertType Inter = new CertType {
				Name =
					"Сертификат Центра «Специалист» международного образца",
				UrlName = CertTypes.Inter
			};

			public static CertType Common = new CertType {
				Name = "Программа дополнительного образования - <strong>Свидетельство</strong>",
				UrlName = CertTypes.Common
			};

			public static CertType Certificate = new CertType {
				Name =
					"Программа дополнительного<br/> профессионального образования  -<br/> <strong>Удостоверение </strong>",
				UrlName = CertTypes.Buh,
			};

			public static CertType Ms = new CertType {
				Name =
					"Международный сертификат Microsoft",
				UrlName = CertTypes.Microsoft
			};

		}


		public static readonly string[] ForMain = new[] {
			"Cert_Autodesk",
			"Cert_Cisco",
			Common,
			Microsoft
		};

		public static readonly string[] Vertical = new[] {
			"СЕРТ_1СБИ",
			"СЕРТ_АСТ",
			"СЕРТ_АДОБ",
			"СЕРТ_АСКОН",
			"СЕРТ_КАСП",
			"СЕРТ_КВАРК",
			"СЕРТ_СБИЛД",
			"СЕРТ_1С",
			"СЕРТ_1С8УТ",
			"СЕРТ_ЭДУ"
		};
	}
}