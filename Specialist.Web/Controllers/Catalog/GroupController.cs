using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Bing;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentAttributes.Utils;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Filter;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc.Binders;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Core.Views;
using Specialist.Web.Pages;
using Specialist.Web.Root.Learning.ViewModels;
using Specialist.Services.Center.Extension;
using SimpleUtils.Extension;
using Specialist.Services.Common.Extension;
using Specialist.Web.Helpers;
using System.Web.Mvc.Html;
using Specialist.Web.Root.PlannedTests.ViewModels;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Education.ViewModel;
using Specialist.Services;
using Specialist.Services.Interface.Order;
using Specialist.Web.Cms.Root.Socials;
using Specialist.Web.Common.Utils;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Mvc.ActionResults;
using Specialist.Web.Common.Services;
using Specialist.Web.Core.Logic;
using Specialist.Web.Root.Profile.Services;
using TuesPechkin;
using EntityUtils = Specialist.Web.Util.EntityUtils;

namespace Specialist.Web.Controllers
{
    public partial class GroupController : ViewController
    {
		[Dependency]
		public ICartService CartService { get; set; }

		[Dependency]
		public IOrderService OrderService { get; set; }

        [Dependency]
        public IGroupVMService GroupVMService { get; set; }

        [Dependency]
        public AlbumVideoService AlbumVideoService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public ISimplePageService SimplePageService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

		[Dependency]
        public IEmployeeService EmployeeService { get; set; }


        [Dependency]
        public IComplexService ComplexService { get; set; }

        [Dependency]
        public CourseFileVMService CourseFileVMService { get; set; }

        [Dependency]
        public IDayShiftService DayShiftService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IRepository2<Lecture> LectureService { get; set; }

        [Dependency]
        public IRepository2<News> NewsService { get; set; }

        [Dependency]
        public IRepository2<GroupData> GroupDataService { get; set; }

        [Dependency]
        public IRepository2<StudentInGroupLecture> StudentInGroupLectureService { get; set; }

        [Dependency]
        public ISectionService SectionService { get; set; }

        private void InitDictionary(GroupFilter filter)
        {
            filter = filter ?? new GroupFilter();
            filter.DayShifts = DayShiftService.GetAll().ToList();
			 filter.Complexes = ComplexService.List().Select(x => x.Value).Where(c => c.IsPublished).ToList();
			filter.Sections = SectionService.GetSectionsTree();
	        filter.Employees = EmployeeService.GetAllTrainers();

        }

		[HandleNotFound]
        public ActionResult Details(decimal groupID) {
			var groupVM = GroupVMService.GetGroup(groupID);
			if (groupVM != null) {
				groupVM.ShowLibrary = User.GetOrDefault(x => x.IsStudent) 
					&& CourseFileVMService.GetFiles(_.List(Tuple.Create(groupVM.Group.Course_TC, groupVM.Group.Teacher_TC))).Any();
				var vkGroupId = GetVkGroupId(groupID);
				groupVM.VkGroupUrl = GetVkGroupUrl(vkGroupId);
			}
			return MView(Views.Group.Details, groupVM);
		}

		[Authorize]
	    public ActionResult Videos(decimal groupId) {
//			var emails = _.List("ptolochko@specialist.ru", "director@specialist.ru", "dinzis@specialist.ru", "lokhturov@specialist.ru");
//			if (!emails.Contains(User.Email)) {
//				return null;
//			}
			var r = GroupVMService.HideVimeoGroupVideo(groupId);
			var hide = r.Item1;
			var group = r.Item2;
			if (hide) {
				return BaseViewWithTitle("Записи не доступны");
			}
		    var videoIds = AlbumVideoService.GetVideos(group.VimeoAlbumId);
		    var videos = InlineBaseView.New(new GroupVideosVM {Group = group}, 
				z => SiteHtmls.VimeoPlayers(videoIds, group.WbnRecPwd));
		    return BaseViewWithTitle(
				"Записи " + StringUtils.AngleBrackets(group.Course.WebName), 
				new PagePart(videos));
	    }

	    public virtual ActionResult Search(GroupFilter filter)
        {
            filter = filter ?? new GroupFilter();
            InitDictionary(filter);
           
            filter.CourseName = filter.CourseTC.IsEmpty() ? string.Empty :
                CourseService.GetByPK(filter.CourseTC).GetOrDefault(x => x.Name);
        	
		

            return View(ViewNames.GroupSearch, filter);
        }
       
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult SearchSubmit([ModelBinder(typeof(DateTimeBinder))] GroupFilter filter)
        {
            if(filter.DayShiftTC == string.Empty)
                filter.DayShiftTC = null;
            if(filter.DaySequenceTC == string.Empty)
                filter.DaySequenceTC = null;

	        filter.CourseTC = CourseService.GetAllActiveCourseNames()
		        .Where(x => x.Value == filter.CourseName).Select(x => x.Key)
				.FirstOrDefault();
				
           
            filter.CourseName = null;
            filter.DayShifts = null;
            filter.Complexes = null;
        	filter.Sections = null;
	        filter.Employees = null;

            return RedirectToAction<GroupController>(
                x => x.List(filter, 1));
       
        }
 
        public virtual ActionResult List(GroupFilter filter, int? pageIndex)
        {
            pageIndex = pageIndex ?? 1;
            var model = GroupVMService
               .GetAllGroups(filter, pageIndex.Value - 1);
            InitDictionary(model.Filter);
			if(filter.ForPrint.GetValueOrDefault()) {
				if(filter.SectionId.IsNull()) {
					var groups = model.Groups.Source;
					if (filter.ForPdf.GetValueOrDefault()) {
						groups = groups.Take(500);
					}
					var sectionGroups = GroupVMService.GetSectionGroups(groups.ToList());
					model.SectionGroups = sectionGroups;
				}
				
				return View(ViewNames.ListForPrint, model);
			}
            return View(ViewNames.GroupList, model);
        }

		public virtual ActionResult ListPdf(GroupFilter filter) {
			filter.ForPrint = true;
			filter.ForPdf = true;
			var x = this.Html.Action("List",filter);
			var result = PdfCreator.Create(x.ToString());
			DownloadResult.AddContentDisposition(this.Response, "расписание.pdf");
			return File(result, "application/pdf");

		}

		public ActionResult JubileeDiscounts() {
			var description = 
				SimplePageService.GetAll().BySysName(SimplePages.JubileeGroups).GetOrDefault(x => x.Description);
			var groups = GroupService.GetPlannedAndNotBegin().Where(x => x.Discount == 20).ToList();
			return BaseView(new PagePart(description), new PagePart(
				Views.Shared.Education.NearestGroupList, new JubileeGroupsVM(groups, false)));
		}

		public ActionResult WithDiscount(string courseTC) {
			var groups = GroupService.GetGroupsForCourse(courseTC)
				.Where(x => x.Discount.HasValue && !x.IsOpenLearning).ToList();

			var prices = PriceService.GetAllPricesForCourse(courseTC,null);

			var course = CourseService.GetByPK(courseTC);
			var actionBlock = new PagePart(string.Empty);
			if(groups.Any(x => x.Discount == CommonConst.GoldFallDiscount)) {
				actionBlock = new PagePart(
					Htmls.HtmlBlock(HtmlBlocks.GoldFallLarge));
			}
				
			return BaseView(
				new PagePart(H.h3[Html.CourseLink(course)].ToString()),
				actionBlock,
				new PagePart(
				Views.Shared.Education.NearestGroupList, 
				new GroupsWithDiscountVM(groups, true){Course = course,
					Prices = prices}));
		}

		[HttpPost]
		public ActionResult Calendar(decimal groupId) {
			var group = GroupService.GetByPK(groupId);
			if (group == null)
				return null;
			var lectures = LectureService.GetAll(x => x.Group_ID == groupId).ToList();
			if (!lectures.Any())
				lectures.Add(new Lecture {
					LectureDateBeg = group.DateBeg.Value,
					LectureDateEnd = group.DateEnd.Value
				});
			var result = EntityUtils.GetCalendar(lectures.Select(x => 
				new CalData(x.LectureDateBeg, x.LectureDateEnd, 
					group.Complex.GetOrDefault(z => z.Address), group.Title)).ToList());
			var encoding = new System.Text.UTF8Encoding();
			return File(encoding.GetBytes(result), "text/calendar",
				"group" + groupId + ".ics");

		}
		
	    public ActionResult CurrentAttendance() {
		    if (User == null) {
			    return null;
		    }
		    if (User.Student_ID != null) {
			    var groupId = StudentInGroupLectureService.GetAll(x =>
				    x.Lecture.LectureDateBeg <= DateTime.Now
					    && x.Lecture.LectureDateEnd >= DateTime.Now
					    && x.StudentInGroup.Student_ID == User.Student_ID
					    && (x.Truancy.GetValueOrDefault() || x.Lateness > 0 || x.Departure > 0)
					    && BerthTypes.AllPaidForCourses.Contains(x.StudentInGroup.BerthType_TC))
				    .Select(x => x.StudentInGroup.Group_ID).FirstOrDefault();
			    if (groupId > 0) {
				    return Content(Url.Group().Details(groupId, 
						Images.Main("AttendanceFail.png")).Style("float:right").ToString());
			    }
		    }
		    return null;
	    }

		static readonly object _vkLock = new object();

		[Auth]
	    public ActionResult VkGroup(decimal groupId, decimal captchaId, string captchaKey) {
			GroupDataService.EnableTracking();

		    lock (_vkLock) {
			    var vkId = GetVkGroupId(groupId);
			    if (!vkId.HasValue) {
				    try {
					    vkId = new VkontakteService(VkontakteService.GroupsToken)
						    .CreateGroup("Группа обучения №" + groupId, captchaId, captchaKey);
				    }
				    catch (CaptchaException ex) {
					    var view = H.div[
						    H.Img(ex.Url),
						    H.Form("")[
								    H.Hidden("captchaId", ex.Id),
								    H.Hidden("groupId", groupId),
								    H.InputText("captchaKey", ""),
									H.Submit("Отправить")
							    ]];
					    return BaseViewWithTitle("Введите текст с картинки", new PagePart(view.ToString()));

				    }
					GroupDataService.InsertAndSubmit(new GroupData {
						VkGroupId = vkId,
						Group_ID = groupId
					});
			    }
			    return Redirect(GetVkGroupUrl(vkId));
			    
		    }
	    }

	    private static string GetVkGroupUrl(long? vkId) {
		    return vkId.HasValue ? "http://vk.com/club" + vkId : null;
	    }

	    private long? GetVkGroupId(decimal groupId) {
		    return GroupDataService.GetAll(x => x.Group_ID == groupId).Select(x => x.VkGroupId).FirstOrDefault();
	    }

	    public ActionResult HotGroupsWithSort(int sortType) {
		    
            var groups = GroupVMService.DiscountGroups();
            
			groups = GroupListSortTypes.Sort(sortType, PriceService.CoursePriceIndex(), groups);
            return View(PartialViewNames.NearestGroupList, 
                    new NearestGroupsVM(groups) {
	                    ShowAllDiscountLink = true
                    });
	    }

	    public ActionResult ForNews(int id, int sortType) {
		    var tcList = NewsService.GetValues(id, x => x.CourseTCList);
		    return ForCourseTCListWithSort(tcList, false, -1, sortType);
	    } 

		public ActionResult ForCourseTCListWithSort(string tcList, 
			bool addGroups, int titleType, int sortType) {
			var hideDiscount = !tcList.IsEmpty() && tcList.StartsWith(CommonConst.HideDiscount);
			if (hideDiscount) {
				tcList = tcList.Remove(CommonConst.HideDiscount);
			}

			var groups = GroupService.GetForUnsplitCourseTCList(tcList);

			if (!groups.Any() && addGroups) {
				groups = GroupService.GetPlannedAndNotBegin().NotSpecial().Take(30).ToList();
			}
			if (groups.Any()) {
				groups = GroupListSortTypes.Sort(sortType, PriceService.CoursePriceIndex(), groups);
				return  Content(Html.Site().NearestGroups(groups,titleType,hideDiscount));
			}
			return Content("");
		}

	    [ChildActionOnly]
		public ActionResult ForCourseTCList(string tcList, bool addGroups, int titleType) {
			return ForCourseTCListWithSort(tcList, addGroups, titleType, 0);
		}

		[Authorize]
		public ActionResult Buy(decimal id) {
			if (User.IsCompany) {
				return BaseViewWithTitle("Заказ юридическим лицом", 
					new PagePart("Для заказа свяжитесь с " + H.Anchor(SimplePages.FullUrls.CorpManagers, "корпоративным отделом")));
			}
			var gr = GroupService.GetByPK(id);
			if (gr != null && PriceService.DopUslCourses().Contains(gr.Course_TC)) {
				OrderService.CreateCurrentOrder(false);
				CartService.AddGroup(id);
				return RedirectToAction<OrderController>(x => x.Register());
			}
			return null;
		}

    }
}
