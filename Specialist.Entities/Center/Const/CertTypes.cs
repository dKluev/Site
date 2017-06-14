using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Const {
	public static class CertTypes {
		public const string InterName = "������������� ����������";
		public const string Common = "Cert_Common";

		public const string State = "Cert_State";

		public const string Inter = "Cert_Inter";

		public const string Diplom = "Cert_diplom";

		public const string Microsoft = "Cert_Microsoft";
		public const string Buh = "���";
		public const string Retraining = "��������������";

		public const string OldCert = "�";
		public const string U16 = "U16";
		public const string Y16 = "Y16";
		public static HashSet<string> ForCert = _.HashSet(OldCert, U16, Y16);

		public static class Full {
			public static CertType Diplom = new CertType {
				Name =
					"<b>������ � ����������������<br/> �������������� �������������� �������</b>",
				UrlName = CertTypes.Diplom
			};
			public static CertType Inter = new CertType {
				Name =
					"���������� ������ ����������� �������������� �������",
				UrlName = CertTypes.Inter
			};

			public static CertType Common = new CertType {
				Name = "��������� ��������������� ����������� - <strong>�������������</strong>",
				UrlName = CertTypes.Common
			};

			public static CertType Certificate = new CertType {
				Name =
					"��������� ���������������<br/> ����������������� �����������  -<br/> <strong>������������� </strong>",
				UrlName = CertTypes.Buh,
			};

			public static CertType Ms = new CertType {
				Name =
					"������������� ���������� Microsoft",
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
			"����_1���",
			"����_���",
			"����_����",
			"����_�����",
			"����_����",
			"����_�����",
			"����_�����",
			"����_1�",
			"����_1�8��",
			"����_���"
		};
	}
}