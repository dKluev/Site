using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Utils;
using Specialist.Entities.Common;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc.ActionResults;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Pages;
using Specialist.Web.Properties;
using Specialist.Web.Root.Learning.Services;
using Specialist.Web.Root.Learning.ViewModels;
using Specialist.Web.Util;
using SpecialistTest.Common.Utils;
using Tuple = SimpleUtils.Common.Tuple;
using Specialist.Entities.Catalog.Links.Interfaces;
using SpecialistTest.Web.Core.Mvc.Extensions;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Lms.Const;
using Specialist.Entities.Profile.ViewModel.Common;
using Specialist.Web.Common.Utils;
using Specialist.Web.Core.Logic;
using Specialist.Web.Helpers;
using Specialist.Web.Root.Catalog;
using Specialist.Web.Root.Graduate.ViewModels;
using Htmls = Specialist.Web.Common.Html.Htmls;

namespace Specialist.Web.Controllers {
	public class GraduateController : ViewController {
		//        private const string Best2010ImageKey = "Best2010ImageKey";


		[Dependency]
		public IUserSettingsService UserSettingsService { get; set; }

		[Dependency]
		public IRepository<Certificate> CertificateService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public ICourseVMService CourseVMService { get; set; }

		[Dependency]
		public IRepository<UserTest> UserTestService { get; set; }

		[Dependency]
		public GroupCertService GroupCertService { get; set; }

		[Dependency]
		public IRepository2<StudentInGroup> StudentInGroupRepository { get; set; }

		[Dependency]
		public ISimplePageService SimplePageService { get; set; }

		public ActionResult DownloadBest(string bestName) {
			var filename = UserImages.GetBestFileSys(bestName, UserSettingsService.SessionID);
			return new DownloadResult("avatar.jpg", filename);
		}

		public ActionResult GroupCert(decimal sigId) {
			var sig = GroupCertService.CreateOrExists(sigId);
			if (!sig.Item1 && sig.Item2 == null) {
				return NotFound();
			}
			var engCertErrorText = GroupCertService.CreateOrExistsEng(sigId, sig.Item2);
			var isEngVendorExists = GroupCertService.IsVendorCertExist(sigId, false);
			var isRuVendorExists = GroupCertService.IsVendorCertExist(sigId, true);
			var courseTC = StudentInGroupRepository.GetValues(sigId, x => x.Group.Course_TC);
			var nextCourseTCs = CourseService.GetNextCourseTCs(_.List(courseTC)).JoinWith(",");
			var course = CourseService.GetAllCourseNames().GetValueOrDefault(courseTC.Trim());
			if (course == null) {
				return NotFound();
			}
			var isCertExists = GroupCertService.IsCertExist(sigId, true);
			return BaseViewWithTitle("Документ об окончании обучения", 
				new PagePart(H.b[Html.CourseLink(course)].ToString()), 
				new PagePart(H.h3["В связи с техническими работами на сайте электронные варианты документов временно недоступны. Приносим извинения за доставленные неудобства"].ToString()), 
				isEngVendorExists ? new PagePart(GetCertView(Images.GroupCertEng(sigId, true,false),sigId,true, 
				true, false)) : null,
				isRuVendorExists ? new PagePart(GetCertView(Images.GroupCertEng(sigId, true,true),sigId,
				true, true, true)) : null,
				new PagePart(engCertErrorText ?? GetCertView(Images.GroupCertEng(sigId, false, false),sigId,true)),
				isCertExists ? new PagePart(GetCertView(Images.GroupCertEng(sigId, false, true),sigId,true,ru: true)) : null,
				new PagePart(
					Html.Action<GroupController>(c => 
					c.ForCourseTCList(nextCourseTCs,false, GroupTitleType.GroupCert)))
				);

		}

		private string GetCertView(TagImg certImg, decimal sigId, bool eng = false, bool vendor = false, 
			bool ru = false) {
			var view = H.div[
				certImg.Style("margin:10px 0;"), H.br,
				Url.Graduate().DownloadCert(sigId, eng, vendor, ru, "Скачать").Class("ui-button"),
				ru && !vendor ? H.div[H.h3["Поделитесь с друзьями!"],
				Htmls.AddThis(Html)] : null
				];
			return view.ToString();
		}

//		private string GetCertVendorView(TagImg certImg, decimal sigId) {
//			var view = H.div[
//				certImg.Style("margin:10px 0;"), H.br,
//				Url.Graduate().DownloadCertVendor(sigId, "Скачать").Class("ui-button")
//				];
//			return view.ToString();
//		}



/*
		public ActionResult SeminarCert(decimal sigId) {
			if (sigId == 0)
				return NotFound();
			var fileSys = UserImages.GetWebinarCertFileSys(sigId);
			if (!System.IO.File.Exists(fileSys)) {
				var sig = GroupCertService.GetFullSig(sigId);
				if (sig.Group.IsSem && sig.Group.IsFinished) {
					using (var image = Image.FromFile(UserImages.GetWebinarCertFileSys(0))) {
						ImageUtils.DrawWebinarCertText(image,
							sig.Student.FullName, sig.Group.Title, sig.Group.DateBeg.Value).Save(fileSys);
					}
				}
				else {
					return NotFound();
				}
			}

			var view = GetCertView(Images.SeminarCert(sigId),sigId);
			return BaseViewWithTitle("Сертификат", new PagePart(view));

		}
*/

		public ActionResult GroupEnd(string courseTC) {
			courseTC = courseTC ?? "0";
			courseTC = courseTC.ToUpper();
			var fileSys = UserImages.GetGroupEndFileSys(courseTC);
			if (!System.IO.File.Exists(fileSys)) {
				var name = CourseVM.WithCoursePrefix(CourseService.AllCourseLinks()[courseTC].Name);
				using (var image = Image.FromFile(UserImages.GetGroupEndFileSys("0"))) {
					ImageUtils.DrawGroupEndText(image, name).Save(fileSys);
				}

			}

			var view = H.l(
				H.Img(Urls.SysToWeb(fileSys)).Style("margin-bottom:10px;"), H.br,
				Url.Graduate().DownloadGroupEnd(courseTC, "Скачать").Class("ui-button"),
				H.h3["Поделитесь с друзьями!"],
				Htmls.AddThis(Html)).ToString();
			return BaseViewWithTitle("Я молодец!", new PagePart(view));

		}
		public ActionResult DownloadGroupEnd(string courseTC) {
			return new DownloadResult("Certificate.png", UserImages.GetGroupEndFileSys(courseTC));
		}

//		public ActionResult DownloadCertVendor(decimal sigId) {
//			if (sigId <= UserImages.MaxCertSigId) {
//				return NotFound();
//			}
//			var fileSys = UserImages.GetGroupCertVendorFileSys(sigId);
//			if (!System.IO.File.Exists(fileSys)) {
//				return NotFound();
//			}
//			return new DownloadResult("Certificate.png", fileSys);
//		}

		public ActionResult DownloadCert(decimal sigId, bool eng, bool vendor, bool ru) {
			if (sigId <= UserImages.MaxCertSigId) {
				return NotFound();
			}
			var group = StudentInGroupRepository.GetValues(sigId, x => x.Group);
			var hd = true;
			if (eng) {
				var sig = GroupCertService.GetFullSig(sigId);
				GroupCertService.CreateOrExistsEng(sigId, sig, hd);
			}
			var fileSys = group.IsSem 
				? UserImages.GetWebinarCertFileSys(sigId)
				:(eng ? UserImages.GetGroupCertEngFileSys(sigId, hd, vendor, ru) : UserImages.GetGroupCertFileSys(sigId)) ;
			if (!System.IO.File.Exists(fileSys)) {
				return NotFound();
			}

			return new DownloadResult("Certificate.png", fileSys);
		}

		public ActionResult DownloadGraduateCertificate(string name, string fullName) {
			var filename = UserImages.GetGraduateCertificateFileSys(name, fullName);
			return new DownloadResult("certificate.png", filename);
		}

		public ActionResult Webinar2011(string id) {
			if (id.IsEmpty())
				return NotFound();
			var model =
				Tuple.New(SimplePageService.GetAll().BySysName(SimplePages.WebinarStudent2011),
					id, UserImages.Webinar2011);
			var certificateFileSys = UserImages.GetGraduateCertificateFileSys(UserImages.Webinar2011, id);
			if (!System.IO.File.Exists(certificateFileSys)) {
				using (var image = Image.FromFile(UserImages.GetGraduateCertificateFileSys(
					UserImages.Webinar2011, "default"))) {
					ImageUtils.DrawFullNameStringBest(UserImages.Webinar2011, image, id).Save(certificateFileSys);
				}
			}
			return View(ViewNames.BestStudent, CommonVM.New(model, model.V1.Title));
		}


		public ActionResult Best2016(decimal id) {
			var exists = GroupCertService.CreateOrExistsBest2016(id);
			if (!exists) {
				return NotFound();
			}

			var certImg = Images.Best2016(id);
			var view = H.div[
				certImg.Style("margin:10px 0;"), 
				H.br,
				Htmls.AddThis(Html)
				];

			return BaseViewWithTitle(CommonTexts.Best2016,
				new PagePart(view.ToString()));

		}


		public ActionResult Best2011(string id) {
			if (id.IsEmpty())
				return NotFound();
			var model =
				Tuple.New(SimplePageService.GetAll().BySysName(SimplePages.BestStudent2011),
					id, UserImages.Best2011);
			var certificateFileSys = UserImages.GetGraduateCertificateFileSys(UserImages.Best2011, id);
			if (!System.IO.File.Exists(certificateFileSys)) {
				using (var image = Image.FromFile(UserImages.GetGraduateCertificateFileSys(
					UserImages.Best2011, "default"))) {
					ImageUtils.DrawFullNameStringBest(UserImages.Best2011, image, id).Save(certificateFileSys);
				}
			}
			return View(ViewNames.BestStudent, CommonVM.New(model, model.V1.Title));
		}

		[Auth(RoleList = Role.Graduate)]
		public ActionResult Best() {
			var imageName = UserImages.BestGraduate;
			var student = AuthService.GetCurrentStudent();
			if (student == null) {
				return BaseView(new PagePart("Вы не являетесь выпускником"));
			}
			var fullName = student.FullName;
			return GetBestResult(fullName, imageName, "Лучший выпускник");
		}
		[Auth(RoleList = Role.Graduate)]
		public ActionResult RealSpecialist() {
			var imageName = UserImages.RealSpecialist;
			var student = AuthService.GetCurrentStudent();
			var fullName = student.FullName;
			var cardColor = student.Card.ClabCardColor_TC;
			return GetBestResult(fullName, imageName, "Настоящий специалист", cardColor);
		}

		private ActionResult GetBestResult(string fullName, string imageName,
			string name,
			string postfix = null) {
			var simplePage = new SimplePage {
				Title = "Свидетельство " +
					StringUtils.AngleBrackets(name)
			};
			var model =
				Tuple.New(simplePage,
					fullName, imageName);
			var certificateFileSys = UserImages.GetGraduateCertificateFileSys(imageName, fullName);
			if (!System.IO.File.Exists(certificateFileSys)) {
				using (var image = Image.FromFile(UserImages.GetGraduateCertificateFileSys(
					imageName, "default" + postfix))) {
					ImageUtils.DrawFullNameStringBest(imageName, image, fullName).Save(certificateFileSys);
				}
			}
			return View(ViewNames.BestStudent, CommonVM.New(model, model.V1.Title));
		}

		[Auth(RoleList = Role.Graduate)]
		public ActionResult RealAvatar() {
			var student = AuthService.GetCurrentStudent();
			var cardColor = student.Card.ClabCardColor_TC;

			var bestName = "Card" + cardColor;
			var filename = UserImages.GetBestFileSys(bestName,
				UserSettingsService.SessionID);

			return View(Views.Graduate.Avatar, CommonVM.New(
				Tuple.New(UserSettingsService.SessionID, System.IO.File.Exists(filename), bestName),
				"Создание аватара"));
		}
		public ActionResult BestAvatar() {
			var bestName = "BestGraduate";
			var filename = UserImages.GetBestFileSys(bestName, UserSettingsService.SessionID);

			return View(Views.Graduate.Avatar, CommonVM.New(
				Tuple.New(UserSettingsService.SessionID, System.IO.File.Exists(filename), bestName),
				"Создание аватара"));
		}

//		public ActionResult Avatar(string bestName) {
//			var filename = UserImages.GetBestFileSys(bestName, UserSettingsService.SessionID);
//
//			return View(CommonVM.New(
//				Tuple.New(UserSettingsService.SessionID, System.IO.File.Exists(filename), bestName),
//				"Создание аватара"));
//		}

		public ActionResult UploadBest(string bestName, IEnumerable<HttpPostedFileBase> userfile) {
			var filename = UserImages.GetBestFileSys(bestName, UserSettingsService.SessionID);

			if (SaveFile(userfile, filename)) {
				Image result;
				using (var bestImg = Image.FromFile(filename)) {
					var icon = (Bitmap)
						Resources.ResourceManager
							.GetObject(StringUtils.UppercaseFirst(bestName) + "Icon", Resources.Culture);

					result = UserImages.PutOver(bestImg, icon);
				}
				var qualityParam = new EncoderParameter(Encoder.Quality, 95L);

				var encoderParams = new EncoderParameters(1);
				encoderParams.Param[0] = qualityParam;

				result.Save(filename, UserImages.GetEncoderInfo("image/jpeg"),
					encoderParams);

				return Content("ok");
			}
			return Content("error");
		}

		private static bool SaveFile(IEnumerable<HttpPostedFileBase> userfile, string filename) {
			var file = userfile.First();
			if (file.FileName.EndsWith(Urls.PhotoExt)
				&& file.ContentLength < UserImages.MaxBestSize.Bytes) {
				var directory = Path.GetDirectoryName(filename);
				if (!Directory.Exists(directory))
					Directory.CreateDirectory(directory);
				UserImages.SaveFileWithResize(file, filename, 800);
				return true;
			}
			return false;
		}

		public ActionResult CertificateValidation() {
			return View(new CertificateValidationVM());
		}


//		public Certificate CertificateFromSig(StudentInGroup sig) {
//			return new Certificate {
//				DateEnd = sig.Group.DateEnd,
//				CourseFullName = sig.Group.Course.NameOfficial ?? sig.Group.Course.Name,
//
//
//			};
//		}

		[Authorize]
	    public ActionResult GroupCerts() {
			StudentInGroupRepository.LoadWith(b => b.Load(x => x.Group).And<Group>(x => x.Course));
			var sigIds = CertificateService.GetAll(x => x.StudentInGroup.Student_ID ==
				User.Student_ID.GetValueOrDefault()).Select(x => x.StudentInGroup_ID).ToList();
		    var sigs = StudentInGroupRepository
				.GetAll(sig => 
					sig.Student_ID == User.Student_ID 
					&& (
					  sig.CertGiven 
					  || FinalExamMarks.MOCert.Contains(sig.FinalExamMark_TC) 
					  || sigIds.Contains(sig.StudentInGroup_ID))
                    && (sig.Group.Course_TC == CourseTC.Itil || 
						!AuthorizationTypes.WithoutMOCert.ToList().Contains(sig.Group.Course.AuthorizationType_TC ?? ""))
					&& BerthTypes.AllForCert.Contains(sig.BerthType_TC) 
					&& sig.Group.DateEnd <= DateTime.Today
					)
					.OrderByDescending(x => x.Group.DateEnd)
				.ToList();
//      var types = CertTypes.ForCert.ToList();
//			var certs = CertificateService.GetAll(x => sigIds.Contains(x.StudentInGroup_ID)
//				&& types.Contains(x.CertType_TC)).OrderByDescending(x => x.DateEnd).ToList();

		    var model = new CertificateListVM() {
			    List = sigs
		    };
			var enterEngName = string.IsNullOrWhiteSpace(User.EngFullName)
				? H.Div("attention2")[
					H.p["Для отображения  сертификата международного образца необходимо  зайти в  раздел {0} и  заполнить  поле  «Полное имя на английском языке». После этого в разделе «Мои сертификаты» появится Ваш сертификат  международного образца.".FormatWith(Url.Profile().EditProfile("«Редактировать профиль»"))]]
				: null;
			var view = InlineBaseView.New(model, x =>
				x.Model.List.Any() 
					? (object) H.div[enterEngName, 
					H.table[
						H.Head( 
						"Дата окончания обучения", 
						"Название курса",
						"Сертификат"
						), 
					   x.Model.List.Select(c => H.Row(
						   c.Group.DateEnd.DefaultString(), 
						   c.Group.Course.NameOfficial ?? c.Group.Course.Name,
						   x.Url.Graduate().GroupCert(c.StudentInGroup_ID, "Открыть")
						   ))].Class("table")]
					: H.b["Вы еще не закончили ни один курс"]);

			return BaseViewWithModel(view, model);
	    }

		[HttpPost]
		public ActionResult CertificateValidation(CertificateValidationVM model) {
			var fullName = string.Empty;
			var number = string.Empty;
			var desc = string.Empty;
			var date = string.Empty;
			var userTestId = StringUtils.ParseInt(model.Number);
			var fio = model.FullName;
			var exists = false;
			if (userTestId.HasValue) {
				var userTest = UserTestService.FirstOrDefault(x => x.Id == userTestId);
				if (userTest != null && userTest.IsPass &&
					(string.Equals(userTest.User.FullName, fio, StringComparison.InvariantCultureIgnoreCase)
						|| string.Equals(userTest.User.EngFullName, fio, StringComparison.InvariantCultureIgnoreCase))) {
					exists = true;
					fullName = userTest.User.FullName;
					number = userTest.Id.ToString();
					desc = "Тест: " + userTest.Test.Name;
					date = "Дата: " + userTest.RunDate.DefaultString();
				}
			} else {
				var certificate = CertificateService.FirstOrDefault(x => x.FullNumber == model.Number);
				if (certificate != null &&
					(string.Equals(certificate.StudentInGroup.Student.FullName,
						fio, StringComparison.InvariantCultureIgnoreCase)
						)) {
					exists = true;
					fullName = certificate.StudentInGroup.Student.FullName;
					number = certificate.FullNumber;
					desc = "Курс: " + certificate.CourseFullName;
					date = "Обучение: " + certificate.DateBeg.DefaultString() +
						" - " + certificate.DateEnd.DefaultString();
				}
			}
			if (!exists) {
				return Content("Сертификата с данными параметрами не существует.");
			}

			var message = _.List(("Сертификат № " + number).Tag("strong"),
				"ФИО: " + fullName, desc, date);
			return Content(message.JoinWith("<br/>"));
		}



	}
}
