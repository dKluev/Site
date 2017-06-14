using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using MvcContrib.ActionResults;
using SimpleUtils.Extension;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Announcement;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Common;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.ViewModel;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Common;
using Specialist.Services.Common.Extension;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Order;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Mvc.Controllers;
using Specialist.Web.Common.Site;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Logic;
using Specialist.Web.Core.Views;
using Specialist.Web.Pages;
using Specialist.Web.Root.Catalog.Views;
using Specialist.Web.Root.Profile.Views;
using Specialist.Web.Util;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Const;
using Specialist.Web.Helpers;
using Specialist.Web.Common.Html;
using Specialist.Entities.Catalog.Links.Interfaces;

namespace Specialist.Web.Controllers {
	public partial class CourseController : ViewController {
		[Dependency]
		public ICourseVMService CourseVMService { get; set; }

		[Dependency]
		public IPriceService PriceService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public IRepository2<UserCourseInfo> UserCourseInfoService { get; set; }

		[Dependency]
		public ICourseListVMService CourseListVMService { get; set; }

		[Dependency]
		public IGroupService GroupService { get; set; }

		[Dependency]
		public IFileVMService FileVMService { get; set; }

		[Dependency]
		public ITrackService TrackService { get; set; }

		[Dependency]
		public IMailService MailService { get; set; }

		[Dependency]
		public ISpecialistExportService SpecialistExportService { get; set; }

		[Dependency]
		public IRepository2<Guide> GuideService { get; set; }

		[Dependency]
		public IRepository2<StudentInGroup> StudentInGroupService { get; set; }

		[Dependency]
		public ISectionVMService SectionVMService { get; set; }

		[Dependency]
		public IVendorVMService VendorVMService { get; set; }

		[Dependency]
		public IAnnounceService AnnounceService { get; set; }

		[Dependency]
		public ISectionService SectionService { get; set; }

		[Dependency]
		public IGroupVMService GroupVMService { get; set; }

		[MobileCache]
		public virtual ActionResult MainCourses() {
			var model = CourseVMService.GetMainCourses();
			return MView(Views.Course.MainCourses, model);
		}

//		[OutputCache(Duration = 60 * 60)]
		public virtual ActionResult MainCoursesSections() {
			var model = CourseVMService.GetMainCourses();
			return View(model);
		}

		[HandleNotFound]
		public ActionResult Print(string urlName) {
			ViewData["ForPrint"] = true;
			return View(PartialViewNames.CourseContents, CourseVMService.GetByUrlName(urlName));
		}


		[HandleNotFound]
		public virtual ActionResult Details(string urlName) {
			if(CommonConst.IsMobile)
				return MView(Views.Course.Details,
					CourseVMService.GetMobileByUrlName(urlName));



			if (urlName.In("tor1","sks1","sks2","tor2")) {
				string urlnew = "t-" + urlName.Substring(0, urlName.Length - 1);
				return this.RedirectToAction(c => c.Details(urlnew));
//				var model = CourseVMService.GetByUrlName(urlnew);
//				return View(model);
			} 
			if(urlName.In("tor16", "tor32")) {
				var urlnew = "t-tor";
				return this.RedirectToAction(c => c.Details(urlnew));
			}
			if(urlName.In("torsh1", "torsh2")) {
				var urlnew = "torsh";
				return this.RedirectToAction(c => c.Details(urlnew));
			}
			else {
			int id;
				if (int.TryParse(urlName, out id)) {
					var course = CourseService.GetAll()
						.FirstOrDefault(c => c.Course_ID == id);
					return RedirectOrNotFound(course);
				}

				if (Regex.IsMatch(urlName.ToLowerInvariant(), "[а-я]")) {
					var course = CourseService.GetAll()
						.FirstOrDefault(c => c.Course_TC == urlName.ToUpperInvariant());
					if (urlName.Equals(
						course.GetOrDefault(x => x.UrlName),
						StringComparison.InvariantCultureIgnoreCase)) {
						return NotFound();
					}
					return RedirectOrNotFound(course);
				}


				var model = CourseVMService.GetByUrlName(urlName);

				if (model != null && model.Course.IsTrackBool && !CourseTC.TorUrls.Contains(model.Course.UrlName)) {
					return RedirectToAction<TrackController>(c => c.Details(model.Course.UrlName),true);
				}
				return View(model);
			}
		}

		private ActionResult RedirectOrNotFound(Course course) {
			if (course == null)
				return NotFound();
			return new RedirectResult(Url.Action<CourseController>(
				c => c.Details(course.UrlName)), true);
		}

	/*	public virtual ActionResult RandomCourseResponses(string urlName) {
			var course = CourseService.GetAll().ByUrlName(urlName);
			var responses = ResponseService.GetRandomResponsesByCourse(course.Course_TC, 2);
			return View(responses);
		}*/

		[AjaxOnly]
		public virtual ActionResult CourseNames(string query) {
			var names = CourseService.GetCourseNames(query);
			return Json(new {query, suggestions = names}, JsonRequestBehavior.AllowGet);
		}



		[AjaxOnly]
		public virtual ActionResult ElearningNames(string query) {
			var names = CourseService.GetElearningNames(query);
			return Json(new {query, suggestions = names}, JsonRequestBehavior.AllowGet);
		}


		/*    public ActionResult AnnounceForSection(int sectionID)
        {
            var announces = AnnounceService.GetAllForSection(sectionID);
            return PartialView(PartialViewNames.AnnounceList, announces);
        }*/

		/*  public virtual ActionResult ScheduleForTrack(string trackTC)
        {
            var coursesScheduleVM = CoursesScheduleVMService.GetForTrack(trackTC);
            return PartialView(PartialViewNames.CoursesSchedule, coursesScheduleVM);
        }

        public virtual ActionResult ScheduleForCertification(int certificationID)
        {
            var coursesScheduleVM = CoursesScheduleVMService.
                GetForCertification(certificationID);
            return PartialView(PartialViewNames.CoursesSchedule, coursesScheduleVM);
        }*/

		/*
        public ActionResult CourseListFor(object obj)
        {
            return CourseListFor(obj, false);
        }*/

		[AjaxOnly]
		public ActionResult Tracks(string courseTC, bool isSecond) {
			var model = TrackService.GetTrackDiscountForCourse(courseTC);
			if (model.Count == 0)
				return null;
			ViewData["CourseTC"] = courseTC;
			ViewData[DynamicValues.IsSecondTrackBlock] = isSecond;
			return View(ViewNames.CourseTracks, model);
		}

		[AjaxOnly]
		public ActionResult Tags(string courseTC) {
			var model = CourseVMService.GetTags(courseTC);
			if (model.Count == 0)
				return null;
			return View(ViewNames.TagCloud, model);
		}

	/*	[AjaxOnly]
		public ActionResult Statistics(string courseTC) {
			var model = GroupService.GetCourseStatistics(courseTC);
			return View(ViewNames.CourseStatistics, model);
		}*/

		[Auth(RoleList = Role.Trainer)]
		public ActionResult Files(string courseTC) {
			var course = CourseService.GetByPK(courseTC);
			var info = GetUserCourseInfo(courseTC) ?? new UserCourseInfo {
				Course_TC = courseTC,
			};

			var model = new FileListVM {
				Course = course,
				Files = course.CourseFiles.Select(cf => cf.UserFile)
					.Where(cf => cf.UserID == User.UserID).ToList()
			};
			model.UserFiles = FileVMService.GetUserFiles(courseTC);
			return BaseView(new PagePart(ViewNames.Files, model),
				new PagePart(new UserCourseInfoView().Init(info,Url)));
		}

		private UserCourseInfo GetUserCourseInfo(string courseTC) {
			return UserCourseInfoService.FirstOrDefault(x => x.UserID == User.UserID
				&& x.Course_TC == courseTC);
		}

		[Authorize]
		[HttpPost]
		public ActionResult UserCourseInfoPost(UserCourseInfo model) {
			UserCourseInfoService.EnableTracking();
			var info = GetUserCourseInfo(model.Course_TC);
			var isEmpty = model.Description.IsEmpty();
			if (info == null) {
				if (!isEmpty) {
					model.UserID = User.UserID;
					UserCourseInfoService.InsertAndSubmit(model);
				}
			}
			else {
				if (isEmpty) {
					UserCourseInfoService.DeleteAndSubmit(info);
				}
				else {
					info.Update(model, x => x.Description);
					UserCourseInfoService.SubmitChanges();
				}
				
			}

			return OkJson();
		}


		public ActionResult Seminars() {
			var model = CourseListVMService.GetSeminars(false);
			return View(model);
		}

		[Auth(RoleList = Role.Graduate | Role.Trainer)]
		public ActionResult Consultations() {
			var model = CourseListVMService.GetSeminars(true);
			return View(ViewNames.Seminars, model);
		}

		public ActionResult SeminarComplete(decimal groupID) {
			var group = GroupService.GetByPK(groupID);
			var seminar = new GroupSeminar(group);
			var model = new SeminarCompleteVM(seminar);
			return View(model);
		}

		//[Authorize]
		public ActionResult AddSeminar(decimal groupID, bool? isIntramural) {
            
            var user = AuthService.CurrentUser;
			if (user == null)
                return RedirectToAction<SimpleRegController>(c => c.Registration(Request.Url.AbsolutePath, "СЕМИНАР"));
            //return RedirectToAction<AccountController>(c => c.LogOn((string) null));

            var group = GroupService.GetByPK(groupID);
			if (group.DateBeg < DateTime.Today) {
				return BaseViewWithTitle("Запись на семинар завершена", new PagePart("Семинар прошел " + group.DateBeg.DefaultString()));
				
			}
			if (!StringUtils.OnlyCyrillicAndSpace(user.FullName)) {
				return BaseViewWithTitle("Запись на семинар", new PagePart("Для записи на семинар, " + 
					Url.Profile().EditProfile("введите ФИО кириллицей")));
			}
			var onlyIntramular = Request.UrlReferrer != null && Request.UrlReferrer.ToString().Contains("test-specpredl");
			if (onlyIntramular) {
				isIntramural = true;
			}
			if (group.HasIntraExtra && !isIntramural.HasValue) {
				return BaseViewWithModel(new AddSeminarView(), 
					new AddSeminarVM{Group = group});
			}

			return AddSeminarWithLink(groupID, isIntramural.GetValueOrDefault());
		}

		[Authorize]
		public ActionResult AddSeminarWithLink(decimal groupID, bool isIntramural) {
			var user = AuthService.CurrentUser;
			var group = GroupService.GetByPK(groupID);
			if((group.IsProbWeb || group.IsSeminar) && !group.IsCareerDay && NeedExport(groupID)) {
				var orderDetail = new OrderDetail {
					Group_ID = groupID,
					PriceType_TC = (isIntramural || !group.WebinarExists)
					? PriceTypes.PrivatePersonWeekend
					: PriceTypes.Webinar ,
					OrderExtras = new EntitySet<OrderExtras>()
				};
				var order = new Order() {
					OrderExams = new EntitySet<OrderExam>(),
					OrderDetails = new EntitySet<OrderDetail> {orderDetail},
					User = user
				};
				try {
					SpecialistExportService.Export(order, true);
				}catch(Exception e) {
					Logger.Exception(e, User);
					return BaseView(new PagePart("Группа заполнена"));
				}
			}
			var seminar = new GroupSeminar(group);
			MailService.SeminarComplete(seminar, Url.ComplexLinkAnchor(group.Complex).AbsoluteHref().ToString());
			return RedirectToAction<CourseController>(c => c.SeminarComplete(groupID));
		}

		bool NeedExport(decimal groupId) {
			if (User == null || !User.IsStudent) {
				return true;
			}

			return !StudentInGroupService.GetAll(x =>
				x.Student_ID == User.Student_ID &&
					x.Group_ID == groupId).Any();

		}


		public virtual ActionResult CourseList(string typeName, object pk,
			string name) {
			var entity = SiteObject.GetEntity(typeName, pk);
			if(entity == null)
				return null;

			var model = CourseListVMService.GetAll(entity);
			model.EntityName = name;
			model.EntityType = entity.GetType();
			return MView(PartialViewNames.AllCourseListPart, model);
		}

		


		public virtual ActionResult CourseListForModel(object model) {
			var courses = CourseListVMService.GetAll(model);
			return View(PartialViewNames.AllCourseListPart, courses);
		}

		public virtual ActionResult CourseListFor(object obj) {
			var model = new AllCourseListVM.Entity(obj);
			return View(PartialViewNames.AllCourseList, model);
		}

		[AjaxOnly]
		[OutputCache(Duration = 60*60, VaryByParam = "sectionId",
			VaryByCustom = "CityTC")]
		public ActionResult CourseListsForSection(int sectionId) {
			var model = SectionVMService.GetBy(null, sectionId);
			return View(PartialViewNames.CourseLists, model.EntityWithTags);
		}

		[AjaxOnly]
		[OutputCache(Duration = 60*60, VaryByParam = "vendorId",
			VaryByCustom = "CityTC")]
		public ActionResult CourseListsForVendor(int vendorId) {
			var model = VendorVMService.GetBy(null, vendorId);
			return View(PartialViewNames.CourseLists, model.EntityWithTags);
		}


		[AjaxOnly]
		public virtual ActionResult ElearningList(string name) {
			var model = CourseListVMService.ElearningCourses(name);
			return View(model);
		}

		[AjaxOnly]
		[OutputCache(Duration = 3600, VaryByCustom = CacheManager.Announce)]
		public ActionResult CoursesForCarousel(int sectionId) {
			var announces = AnnounceService.GetAllForSection(sectionId);
			return GetCarousel(announces);
		}

		private ContentResult GetCarousel(List<Announce> announces) {
			if (!announces.Any())
				return null;
			return Content(
				CommonSiteHtmls.Carousel(announces.Select(a =>
					a.AnnounceGroups.First()).ToList().GetRows(6)
					.Select(x => Html.Site().CoursesFour(x))).ToString());
		}

	/*	[AjaxOnly]
		public ActionResult CarouselForVendor(int vendorId) {
			var announces = AnnounceService.GetAllForVendor(vendorId);
			return GetCarousel(announces);
		}*/

		[AjaxOnly]
		[OutputCache(Duration = 3600, VaryByCustom = CacheManager.Announce)]
		public ActionResult CarouselForEntity(string typeName, object pk) {
			var type = SiteObject.GetType(typeName);
			if(type == null)
				return null;
			var announces = AnnounceService.GetAllForEntity(type,
				LinqToSqlUtils.CorrectPKType(pk, type));
			return GetCarousel(announces);
		}



		public ActionResult NearestCourses() {
			return View(SectionService.GetSectionsTree());
		}

		public ActionResult WithoutWebinar() {
			var webinarCourseTCs = PriceService.CourseWithWebinar();
			var ppCourseTCs = new HashSet<string>(PriceService.GetAllCurrent()
				.Where(x => x.PriceType_TC == PriceTypes.PrivatePersonWeekend).Select(x => x.Course_TC).Distinct());
			var courses = CourseService.AllCoursesForList().Where(x => !x.IsTrackBool &&
				ppCourseTCs.Contains(x.Course_TC) && !webinarCourseTCs.Contains(x.Course_TC))
				.OrderBy(x => x.GetName())
				.ToList();
			var sectionCourses = GroupVMService.GetSectionCourses(courses);
			return View(CommonVM.New(sectionCourses, "Курсы, не читаемые в режиме вебинар"));
		}

		public ActionResult IsNew() {
			return View(new IsNewCoursesVM());
		}

		[OutputCache(Duration = 60*60, VaryByParam = "none")]
		public ActionResult IsNewBlock() {
			var courses = CourseService.AllCoursesForList().Where(x => 
				x.IsNew).OrderBy(x => x.GetName())
				.ToList();
			var sectionCourses = GroupVMService.GetSectionCourses(courses)
				.OrderBy(x => x.Key.WebSortOrder).ToList();
			return View(PartialViewNames.CoursesBySections, sectionCourses);
		}

		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		public ActionResult WithOpenClasses() {
			var courseTCs = GroupService.GetPlannedAndNotBegin().Where(x => x.IsOpenLearning).Select(x => x.Course_TC);
			var courses = CourseService.AllCoursesForList().Where(x => !x.IsTrackBool &&
				courseTCs.Contains(x.Course_TC))
				.OrderBy(x => x.GetName())
				.ToList();
			var sectionCourses = GroupVMService.GetSectionCourses(courses);
			return View(PartialViewNames.CoursesBySections, sectionCourses);
		}

		[NotMobileRedirect]
		[HandleNotFound]
		public ActionResult Group(decimal id) {
			return View(Views.Course.GroupMobile,
			CourseVMService.GetMobileByGroup(id));
		}
		[NotMobileRedirect]
		public ActionResult Search(string text) {
			if(text.IsEmpty())
				return View(Views.Course.SearchMobile, Tuple.Create(text,
					new List<CourseLink>()));
			var list = CourseService.AllCourseLinks().Where(x => 
				x.Value.IsActive && (x.Value.WebName ?? string.Empty).ToLower().Contains(text.ToLower())).Select(x => x.Value).Take(50).ToList();
			return View(Views.Course.SearchMobile, Tuple.Create(text, list));
		}

		[HandleNotFound]
		public ActionResult Guide(int id) {
			var guide = GuideService.GetByPK(id);
			if (guide == null) {
				return null;
			}
			var mapBlock = H.l(
				H.map[AddTarget(guide.Areas)].Name("map").Id("guide-map"),
				H.JavaScript().Src("/Scripts/Views/Center/guide.js?v=2"));

			var model = new GuideVM {Guide = guide};
			var view = InlineBaseView.New(model, x =>
				H.div[
				Images.Guide(guide.Image).Id("guide-img").Usemap("#map"),
				guide.Areas.IsEmpty() ? null : mapBlock,
				Htmls.Mapster
				]);

			return BaseViewWithModel(view, model);
		}

		string AddTarget(string areas) {
			if (areas.IsEmpty())
				return string.Empty;
			var targetPart = "target='_blank' ";
			return areas.Replace("coords=", targetPart + "coords=");
		}

	}
}
