using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Lms.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Services.Passport;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Files;
using SpecialistTest.Common.Utils;

namespace Specialist.Web.Root.Learning.Services {
	public class GroupCertService {

		[Dependency]
		public IRepository<Certificate> CertificateService { get; set; }


		[Dependency]
		public IRepository2<StudentInGroup> StudentInGroupRepository { get; set; }

		[Dependency]
		public IRepository2<User> UserService { get; set; }

		[Dependency]
		public IRepository2<Student> StudentService { get; set; }

		[Dependency]
		public AuthService AuthService { get; set; }

		public StudentInGroup GetFullSig(decimal sigId) {
			StudentInGroupRepository.LoadWith(b => b.Load(x => x.Group, x => x.Student).And<Group>(x => x.Course, x => x.Teacher));
			var sig = StudentInGroupRepository.GetByPK(sigId);
			return sig;
		}


		public void DeleteGroupCertsEng(List<decimal> sigIds) {
			foreach (var sigId in sigIds) {
				var fileSys = UserImages.GetGroupCertFileSys(sigId);
				if (File.Exists(fileSys)) {
					File.Delete(fileSys);
				}

			}
			
		}

//		public bool IsVendorCertExist(decimal sigId) {
//			return System.IO.File.Exists(UserImages.GetGroupCertVendorFileSys(sigId));
//		}
		public bool IsVendorCertExist(decimal sigId, bool ru) {
			return System.IO.File.Exists(UserImages.GetGroupCertEngFileSys(sigId, false, true, ru));
		}

		public bool IsCertExist(decimal sigId, bool ru) {
			return System.IO.File.Exists(UserImages.GetGroupCertEngFileSys(sigId, false, false, ru));
		}

		public bool CreateOrExistsBest2016(decimal studentId) {
			if (!Students.Best2016.Contains(studentId)) {
				return false;
			}
			var fileSys = UserImages.GetBest2016FileSys(studentId);
			if (!System.IO.File.Exists(fileSys)) {
				var student = StudentService.GetValues(studentId, x => new {x.LastName, x.MiddleName, x.FirstName});
				if (student == null) return false;
				using (var image = Image.FromFile(UserImages.GetBest2016FileSys(0))) {
					ImageUtils.Best2016(image, _.List(student.LastName, student.FirstName, student.MiddleName).JoinWith(" ")).Save(
						fileSys);
				}
			}
			return true;
			
		}


		public string CreateOrExistsEng(decimal sigId, StudentInGroup sig, bool hd = false) {
			var fileSys = UserImages.GetGroupCertEngFileSys(sigId, hd, false,false);
			if (!System.IO.File.Exists(fileSys)) {

				sig = sig ?? GetFullSig(sigId);
				if (sig == null) {
					return "";
				}
				var finalMark = (sig.CertGiven || FinalExamMarks.MOCert.Contains(sig.FinalExamMark_TC));
				var berthType = BerthTypes.AllForCert.Contains(sig.BerthType_TC);
				var authType = (sig.Group.Course_TC == CourseTC.Itil ||
					!AuthorizationTypes.WithoutMOCert.Contains(sig.Group.Course.AuthorizationType_TC));
				var engName = !sig.Group.Course.NameOfficialEn.IsEmpty();
				var certExists = finalMark
					&& sig.Group.IsFinished
					&& berthType
					&& authType
					&& engName;
				if (!certExists) {
					var errorTxt = "";
					if (AuthService.CurrentUser != null && AuthService.CurrentUser.IsEmployee) {
						var errors = new List<string>();
						if(!finalMark) errors.Add("Не проставлено поле аттестации в карточке заказа");
						if(!sig.Group.IsFinished) errors.Add("Группа завершится " + sig.Group.DateEnd.DefaultString());
						if(!berthType) errors.Add("Тип оплаты " + sig.BerthType_TC);
						if(!authType) errors.Add("Авторизация " + sig.Group.Course.AuthorizationType_TC);
						if(!engName) errors.Add("Отсутствует название курса на английском " + sig.Group.Course.Course_TC);
						errorTxt = errors.JoinWith("<br/>");
					}
					return "Сертификат недоступен<br/>" + errorTxt;
				}
				string fullName;
				if (sig.Student.FullNameEn.IsEmpty()) {
					var userIsStudent = AuthService.CurrentUser
						.GetOrDefault(x => x.Student_ID == sig.Student_ID);
					if (userIsStudent) {
						fullName = AuthService.CurrentUser.EngFullName;
					} else {
						fullName = UserService.GetAll(x => x.Student_ID == sig.Student_ID)
							.OrderByDescending(x => x.UserID).Select(x => x.EngFullName).FirstOrDefault();
					}

				} else {
					fullName = sig.Student.FullNameEn;
				}


				GenerateRuCert(sig, hd, VendorEngCertData.mainRu);
				var ruCertId = VendorEngCertData.CourseRuCertVendor.GetValueOrDefault(sig.Group.Course.ParentCourse_TC);

				if (ruCertId > 0) {
					GenerateRuCert(sig, hd, ruCertId);
				}




				if (String.IsNullOrWhiteSpace(fullName)) {
					var isOwner = AuthService.CurrentUser != null && AuthService.CurrentUser.Student_ID == sig.Student_ID;
					return (isOwner ? "Сертификат недоступен, отсутствует информация о Фамилии/Имени на английском языке" : "");
				}

				var certType = GetEngCertType(sig.OurOrg_TC);
				using (var image = Image.FromFile(UserImages.GetGroupCertEngFileSys(certType, hd, false,false))) {
					var data = GetVendorCertData(sig, hd, VendorEngCertData.main, fullName);

					ImageUtils.RenderVendorEngCertTexts(image, data).Save(fileSys);
//						ImageUtils.DrawGroupCertEngTextOld(image,
//							fullName, sig.Group.Course.NameOfficialEn,
//							sig.Group.DateEnd.GetValueOrDefault().ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture), hd).Save(fileSys);
						
					
				}
				var vendorCerType = GetEngCertVendroType(sig);
				if (vendorCerType > 0) {

					using (var image = Image.FromFile(UserImages.GetGroupCertEngFileSys(vendorCerType, hd, true, false))) {
						var data = GetVendorCertData(sig, hd, vendorCerType, fullName);
						fileSys = UserImages.GetGroupCertEngFileSys(sigId, hd, true, false);
						ImageUtils.RenderVendorEngCertTexts(image, data).Save(fileSys);
					}
				}

			}
			return null;


		}

		private static void GenerateRuCert(StudentInGroup sig, bool hd, int ruCertId) {
			var sigId = sig.StudentInGroup_ID;
			var data = GetVendorRuCertData(sig, hd, ruCertId);
			var certTemp = VendorEngCertData.WithOrgs.Contains(ruCertId)
				? ruCertId*10 + OurOrgs.CertTypes.GetValueOrDefault(sig.OurOrg_TC)
				: ruCertId;
			var vendor = ruCertId != VendorEngCertData.mainRu;
			var ru = true;
			using (var image = Image.FromFile(UserImages.GetGroupCertEngFileSys(certTemp, hd, vendor,ru))) {
				var file = UserImages.GetGroupCertEngFileSys(sigId, hd, vendor,ru);
				ImageUtils.RenderVendorEngCertTexts(image, data).Save(file);
			}
		}

		private static VendorEngCertData GetVendorRuCertData(StudentInGroup sig, bool hd, int ruCertId) {
			var data = new VendorEngCertData(
				hd,
				ruCertId,
				sig.Student.FullName,
				sig.Group.Course.NameOfficial,
				sig.Group.DateEnd.GetValueOrDefault(),
				sig.Group.Teacher.GetOrDefault(x => x.FullName));
			return data;
		}

		private static VendorEngCertData GetVendorCertData(StudentInGroup sig, bool hd, int vendorCerType, string fullName) {
			var employee = sig.Group.Teacher;
			var empName = employee != null 
				? employee.FirstNameEN + " " + employee.LastNameEN
				: null;
			var data = new VendorEngCertData(
				hd,
				vendorCerType,
				fullName,
				sig.Group.Course.NameOfficialEn,
				sig.Group.DateEnd.GetValueOrDefault(),
				empName);

				
			return data;
		}


		private int GetEngCertVendroType(StudentInGroup sig) {
			if (sig.Group.Course.IsMs) {
				return VendorEngCertData.microsoftCert;
			}
			if (sig.Group.Course.AuthorizationType_TC == AuthorizationTypes.Adobe) {
				return VendorEngCertData.adobeCert;
			}
			if (sig.Group.Course.AuthorizationType_TC == AuthorizationTypes.Graphisoft) {
				return VendorEngCertData.graphisoft;
			}

			return 0;
		}

		private int GetEngCertType(string ourOrgTC) {
			return OurOrgs.CertTypes.GetValueOrDefault(ourOrgTC);
		}

		private static bool CheckTemplateFile(decimal sigId) {
			return sigId <= UserImages.MaxCertSigId;
		}

		public Tuple<bool, StudentInGroup> CreateOrExists(decimal sigId) {
			if (CheckTemplateFile(sigId)) {
				return Tuple.Create(false, (StudentInGroup)null);
			}
			var fileSys = UserImages.GetGroupCertFileSys(sigId);
			StudentInGroup sig = null;
			if (!System.IO.File.Exists(fileSys)) {
				sig = GetFullSig(sigId);
				if (sig == null) {
					return Tuple.Create(false, sig);
				}

				if (BerthTypes.AllForCert.Contains(sig.BerthType_TC) && sig.Group.IsFinished) {
					var cert = CertificateService.GetAll(x => x.StudentInGroup_ID == sigId)
						.OrderByDescending(x => x.ForPrint).ThenByDescending(x => x.Certificate_ID)
						.FirstOrDefault();
					if (cert == null) {
						return Tuple.Create(false, sig);
					}
					Func<DateTime, string> format = x => "{0} {1} {2} г.".FormatWith(x.Day,
						MonthUtil.GetName(x.Month, true), x.Year);
					var orgName = OurOrgs.Names.GetValueOrDefault(cert.OurOrg_TC) ?? OurOrgs.RuName;
					var certTemplate = GetCertTemplate(cert);
					if (certTemplate < 0) {
						return Tuple.Create(false, sig);
					}
					using (var image = Image.FromFile(UserImages.GetGroupCertFileSys(certTemplate))) {
						if (cert.CertType_TC == CertTypes.OldCert) {
							var date = "c {0} по {1}".FormatWith(format(cert.DateBeg.GetValueOrDefault()),
								format(cert.DateEnd.GetValueOrDefault()));
							ImageUtils.DrawGroupCertText(image,
								sig.Student.FullName,
								cert.CourseFullName,
								cert.Hours,
								date, cert.FullNumber, sig.Student.Sex, orgName).Save(fileSys);

						} else {

							var date = format(cert.DateEnd.GetValueOrDefault());
							ImageUtils.DrawGroupCertText2016(image,
								sig.Student.FullName,
								cert.CourseFullName,
								cert.Hours,
								date, cert.FullNumber, sig.Student.Sex, orgName, cert.CertType_TC == CertTypes.Y16).Save(fileSys);
						}
					}



				} else {
					return Tuple.Create(false, sig);
				}
			}
			return Tuple.Create(true, sig);
		}

		private static decimal GetCertTemplate(Certificate cert) {
			if (CertTypes.ForCert.Contains(cert.CertType_TC)) {
				var offset = cert.CertType_TC == CertTypes.OldCert ? 0 : 10;
				return offset + OurOrgs.CertTypes.GetValueOrDefault(cert.OurOrg_TC);
			}
			return -1;

		}
	}
}
