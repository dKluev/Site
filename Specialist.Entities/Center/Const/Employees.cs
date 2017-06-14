using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Microsoft.Linq.Translations.Auto;

namespace Specialist.Entities.Const {
	public static class Employees {

		public static string MainManager {get {
			return GetKarpovich();
		}
		}

		public static string GetMainManager(bool isOrg) {
			return isOrg ? Glazunova : MainManager;
		}

		public const string KarpovichTC = "ËÀĞÀ";
		public const string LovkovTC = "ËÄÀ";
		public const string TestCertDefault = "ÊĞÓÌ";
		public const string Glazunova = "ÃËÀ";
//		public const string Taranenko = "ÒÀĞÅ";		
		public const string NikulchaTC = "ÍÊË";
		public const string Dinzis = "ÄÄŞ";

//		public const string NikulchaTC = "ÍÊË";
		public const string ZuevaTC = "ŞÕÌÀ";

		public const string Site = "ÑÀÉÒ";

		public static string TestCert = TestCertDefault;
 
		static Employees() {
			try {
				TestCert = new SpecialistDataContext().Employees.Where(x => 
				x.EmpGroup_TC == EmpGroups.Sont).Select(x => x.Employee_TC)
					.FirstOrDefault() ?? TestCertDefault;
			}
			catch {}
		}


		public static List<string> AllManagers = _.List(MainManager, GetLovkov());

		public static DateTime startDate {
			get {
				return new DateTime(2017, 04, 03);
			}
		} 
		public static string TodayManger(DateTime date) {
			var interval = (int) (date - startDate).TotalDays;
			var index = interval%3;
			if (index == 0) {
				return GetKarpovichTC(date);
			}
			if (index == 1) {
				return GetLovkovTC(date);
			}
			if (index == 2) {
				return GetNikulchaTC(date);
			}
			return GetKarpovichTC(date);
		}

		public static string GetNikulchaTC(DateTime date) {
			return NikulchaTC;
		}

		public static string GetKarpovichTC(DateTime date) {
			return KarpovichTC;
		}

		public static string GetLovkovTC(DateTime date) {
			if (date >= new DateTime(2016, 8, 13)
				&& date <= new DateTime(2016, 8, 27))
				return KarpovichTC;
			return LovkovTC;
//			var interval = (int) (date - startDate).TotalDays;
//			var isNikulcha = (interval/2) % 2 != 0;
//			return isNikulcha ? NikulchaTC : ZuevaTC;
		}

		public static string GetLovkov() {
//			if (DateTime.Today >= new DateTime(2016, 7, 2)
//				&& DateTime.Today <= new DateTime(2016, 7, 15))
//				return GetManagerForLovkov();
			return TodayManger(DateTime.Today);
		}


		public static string GetKarpovich() {
//			if (DateTime.Today >= new DateTime(2016, 7, 2)
//				&& DateTime.Today <= new DateTime(2016, 7, 15))
//				return GetManagerForLovkov();
			return TodayManger(DateTime.Today);
		}


		public static string GetTaranenko() {
			return GetLovkov();
		}


		public const string Specweb = "SPECWEB";

		public static readonly List<string> Skype = new List<string> {
				"ØÅÍ",
				"ÁÑÀ",
				"ÊÎÍÈ",
				"ÊÓË",
				"ÌÈÒÈÍÀ",
				"ÍÊË",
			};
		
		public static readonly List<string> TrainersAlwaysVisible = new List<string> {
//				"ÑÂÅØ",
//				"ÀÍÄ",
//				"ØÀÏ"
			};


		public static readonly List<string> SpecialTrainers = new List<string> {
				"ÃÓÄ",
				"ÌÏÑ",
				"ÄÄŞ",
				"ŞÑÈ"
			};

		public const string Rud = "ĞÓÈ";

	}
}
