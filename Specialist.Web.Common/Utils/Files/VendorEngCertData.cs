using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Configuration;
using MvcContrib;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Common.Utils.Files {
	public class VendorEngCertData : VendorCertBase {

		public const int main = 1;
		public const int mainRu = 20;
		public const int microsoftCert = 3;
		public const int adobeCert = 4;


		public const int postgres = 5;
		public const int graphisoft = 6;
		public const int paragon = 7;
		public const int askon = 10;
		public const int maijin = 11;

		public const double verticalScale = 3.0;
		public static Dictionary<int, double> SpecialScale = new Dictionary<int, double> {
			{microsoftCert,  3500.0/727},
			{paragon, verticalScale},
			{askon, verticalScale},
			{maijin, verticalScale},
		};

		public static HashSet<int> WithOrgs = _.HashSet(
			mainRu,
			askon,
			maijin);

//		public static HashSet<int> All = _.HashSet(
//			microsoftCert,
//			adobeCert);


		public static Dictionary<string,int> CourseRuCertVendor = new Dictionary<string, int> {
			{CourseTC.Dba1, postgres},
			{"СИС2",  paragon},
			{"СИС3", paragon},
			{"АС1", askon},
			{"КОМПИГР", maijin}
		}; 

		public VendorEngCertData(bool hd, int certType, string fullName, string certName, DateTime date, string trainerName) : base(hd, certType, fullName, certName, date, trainerName) {}


		public List<CertTextPosition> GetTexts() {
			if (CertType == microsoftCert) {
				return Ms();
			}
			if (CertType == adobeCert) {
				return Adobe();
			}

			if (CertType == postgres) {
				return Postgress();
			}
			if (CertType == graphisoft) {
				return Graphisoft();
			}
			if (CertType == paragon) {
				return Paragon();
			}
			if (CertType == askon) {
				return Askon();
			}
			if (CertType == maijin) {
				return Maijin();
			}
			if (CertType == main || CertType == mainRu) {
				return Main();
			}


			throw new Exception("certid isn't supported");
		}


		public List<CertTextPosition> Main() {
			var dateText = Date.DefaultString();
			return _.List(
				Text(FullName, 337, 180, 14, "Arial"),
				Text(CertName, 337, 243, 13, "Arial", 400),
				Text(dateText, 380, 410, 8, "Arial", italic:true));
		}


		public List<CertTextPosition> Ms() {
			var dateText = "Computer Training Center at Bauman MSTU " 
				+ Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
			return _.List(
				Text(FullName, 281, 210, 14, "Arial"),
				Text(CertName, 281, 310, 9, "Arial", 280),
				Text(TrainerName, 460, 428, 7),
				Text(dateText, 440, 483, 6));
		}

		public List<CertTextPosition> Adobe() {
			var row3 = 414;
			return _.List(
				Text(FullName, 400, 252, 14, "Arial", bold:true,center:true),
				Text(CertName, 400, 334, 12, "Arial", bold:true,center:true),
				Text(TrainerName, 400, row3, 10,center:true),
				Text("CCT" + StringUtils.AngleBrackets("Specialist"), 158, row3, 10,center:true),
				Text(Date.ToString("dd.MM.yy", CultureInfo.InvariantCulture), 640, row3, 10,center:true));
		}


		public List<CertTextPosition> Postgress() {
			return _.List(
				Text(FullName, 36, 302, 13, "Arial"));
		}

		public List<CertTextPosition> Graphisoft() {
			return _.List(
				Text(FullName, 400, 175, 16, "Arial",bold:true, center:true),
				Text(CertName, 400, 245, 12, "Arial",bold:true, center:true),
				Text(TrainerName, 400, 398, 13, "Arial", center:true)
				);
		}

		public List<CertTextPosition> Askon() {
			var left = 137;
			return _.List(
				Text(FullName, left, 457, 16, "Arial",bold:true),
				Text(CertName, left, 550, 12, "Arial",bold:true),
				Text(TrainerName, left, 810, 12, "Arial", bold:true),
				Text(Date.DefaultString(), left, 870, 12, "Arial", bold:true)
				);
		}


		public List<CertTextPosition> Paragon() {
			return _.List(
				Text("Сертификат", 400, 350, 24, bold:true, center:true),
				Text(FullName, 400, 460, 18, "Arial", center:true),
				Text(CertName, 100, 570, 14, "Arial", center:true,width:600),
				Text("Paragon Software GmbH\nКонстантин Комаров", 460, 800, 13, "Arial")
				);
		}

		public List<CertTextPosition> Maijin() {
			var left = 137;
			return _.List(
				Text(FullName, 400, 509, 20, "Arial",bold:true, center:true),
				Text(CertName, 100, 720, 16, "Arial",bold:true, center:true, width: 600)
				);
		}



	}
}