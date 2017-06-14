using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Common;
using SimpleUtils.Extension;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Core.Interface;
using Specialist.Services.Order;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Core;
using System.Linq;
using System.Linq.Dynamic;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Cms.Core.ViewModel;
using Specialist.Web.Cms.ViewModel;
using Specialist.Web.Common.Extension;
using Specialist.Web.Const;
using Specialist.Web.Util;
using Tuple = SimpleUtils.Common.Tuple;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Mvc.Extensions;

namespace Specialist.Web.Cms.Controllers {
	[CmsAuth(RoleList = Role.OrderManager)]
	public class OrderEntityController : BaseController<Order> {
		const string IsCompleteName = "IsComplete";

		const string FullNameName = "FullName";

		const string UpdateDateName = "UpdateDate";

		public bool IsComplete {
			get;
			set;
		}

		public string FullName {
			get;
			set;
		}

		public DateTime? OrderUpdateDate {
			get;
			set;
		}

		[Dependency]
		public ISpecialistExportService SpecialistExportService {
			get;
			set;
		}

		[Dependency]
		public IRepository<OrderDetail> OrderDetailService { get; set; }

		[Dependency]
		public IRepository2<Source> SourceService { get; set; }

		[Dependency] 
		public IRepository<Course> CourseService { get; set; }

		[Dependency]
		public IRepository<Track> TrackService {
			get;
			set;
		}

		[Dependency]
		public IRepository<Student> StudentService {
			get;
			set;
		}

		[Dependency]
		public IRepository<UserTest> UserTestService {
			get;
			set;
		}

		[Dependency]
		public IRepository<EntityStudySet> EntityStudySetService {
			get;
			set;
		}

		[Dependency]
		public IRepository<CoursePrerequisite> CoursePrerequisiteService {
			get;
			set;
		}
		[Dependency]
		public IRepository<StudentInGroup> StudentInGroupService {
			get;
			set;
		}

		[Dependency]
		public ISiteObjectRelationService SiteObjectRelationService {
			get;
			set;
		}

		[Dependency]
		public IRepository<User> UserService {
			get;
			set;
		}

		[Dependency]
		public IRepository<Test> TestService {
			get;
			set;
		}

		protected override IEnumerable<string> GetFilterQueryStringKeys() {
			var keys = base.GetFilterQueryStringKeys();
			IsComplete = keys.Any(k => k == IsCompleteName);
			if (keys.Contains(FullNameName))
				FullName = Request.QueryString[FullNameName];
			if (keys.Contains(UpdateDateName))
				OrderUpdateDate = Convert.ToDateTime(Request.QueryString[UpdateDateName]);

			return keys.Where(k => k != IsCompleteName
				&& k != FullNameName && k != UpdateDateName);
		}

		protected override IQueryable<Order> AddFilterToList(
			Dictionary<string, object> currentFilterValues) {

			var items = base.AddFilterToList(currentFilterValues);
			items = items.Where(i => !i.InPlan);
			if (IsComplete) {
				currentFilterValues.Add(IsCompleteName, true);
				items = items.Where(i => i.PaymentType_TC != null);
			} else {
				items = items.Where(i => i.Started && i.PaymentType_TC == null);

			}

			if (!FullName.IsEmpty()) {

				currentFilterValues.Add(FullNameName, FullName);
				items = items.Where(o =>
					(o.User.LastName + " " + o.User.FirstName + " " +
						o.User.SecondName).Contains(FullName)
							|| o.User.Email.Contains(FullName));
			}

			if (OrderUpdateDate.HasValue) {
				currentFilterValues.Add(UpdateDateName, OrderUpdateDate);
				var date = OrderUpdateDate.Value;
				items = items.Where(o => o.UpdateDate.Year == date.Year &&
					o.UpdateDate.Month == date.Month && o.UpdateDate.Day == date.Day);
			}


			return items;
		}

		public ActionResult Export(int orderID) {

			try {
				SpecialistExportService.Export(orderID, false, null);

			}
			catch (Exception e) {
				if (e.Message.StartsWith(CommonTexts.FullGroupError)) {
					return Content("Экспорт невозможен. Группа заполнена");
				}
				throw;
			}

			return RedirectBack();
		}

		protected override void ListVMCreated(Core.ViewModel.ListVM listVM) {
			var property =
				new PropertyMetaData(MetaData.EntityType.GetProperty(IsCompleteName));
			property.SetAttribute(new UIHintAttribute(Controls.CheckBox));
			property.Display("Завершенный");
			listVM.ExtraFilters.Add(property);
			property =
				new PropertyMetaData(MetaData.EntityType.GetProperty(FullNameName));
			property.SetAttribute(new UIHintAttribute(Controls.Text));
			property.Display("ФИО или Email");
			listVM.ExtraFilters.Add(property);

			property =
				new PropertyMetaData(MetaData.EntityType.GetProperty(UpdateDateName));
			property.SetAttribute(new UIHintAttribute(Controls.DatePicker));
			property.Display("Дата");
			listVM.ExtraFilters.Add(property);
			base.ListVMCreated(listVM);
		}

		public ActionResult Beeline() {
			return View();
		}

		public ActionResult BeelineData(DateTime? start, DateTime? end) {
			start = start ?? new DateTime(2010, 2, 1);

			var promocode = "b07f53d";

			end = end.Value.AddDays(1);
			var orderDetails = OrderDetailService.GetAll().Where(od => od.CreateDate > start
				&& od.CreateDate < end && od.Order.PromoCode == promocode
					&& od.Order.CustomerType == OrderCustomerType.PrivatePerson
						&& od.Order.UserID != null);
			var regCount = orderDetails.Select(x => x.Order.UserID).Distinct().Count();
			ViewData["RegCount"] = regCount;

			var orderDetailsForReport = orderDetails.Where(od =>
				od.Order.PaymentType_TC != null).Select(x => new {
					x.Order.User.SecondName,
					x.Order.User.FirstName,
					x.Order.User.LastName,
					x.Course_TC,
					x.Price,
					x.CreateDate
				}).Distinct().ToList().GroupBy(x => new {
					x.SecondName, x.FirstName, x.LastName, x.Course_TC
				}).Select(x => new {
					x.Key.SecondName, x.Key.FirstName, x.Key.LastName, x.Key.Course_TC,
					Price = x.Select(y => y.Price).Max(),
					CreateDate = x.Select(y => y.CreateDate).Max(),
				});
			var model = new JsTableVM {
				Title = "",
				Columns = {{"Фамилия", null},{"Имя", null}, {"Отчество", null}, {"Курс", null}, {"Цена", null},
				{"Дата", null}},
				Rows = orderDetailsForReport.Select(x => _.List<object>(
					x.LastName,
					x.FirstName,
					x.SecondName,
					x.Course_TC,
					x.Price.MoneyString(),
					x.CreateDate.DefaultString()))
			};
			return View(model);
		}

		public ActionResult TestCert(decimal id, bool? eng) {
			var orderDetail = OrderDetailService.GetByPK(id);
			var userTest = orderDetail.UserTest;
			var isEng = orderDetail.Params.Lang == TestCertLang.Eng;
			if (eng.HasValue)
				isEng = eng.Value;


			var certName = EntityUtils.GetTestCertName(isEng, userTest);
			var user = orderDetail.Order.User;
			var userName = isEng ? user.EngFullName : user.FullName;
			return View(Tuple.New(userName, certName, userTest));
		}

		public ActionResult Envelope(int userId) {
			var user = UserService.GetByPK(userId);
			return View(user);
		}

		public ActionResult UserTestCert(int id, string name) {
			var userTest = UserTestService.GetByPK(id);
			var isEng = true;

			var certName = EntityUtils.GetTestCertName(isEng, userTest);
			var user = userTest.User;
			var userName = name ?? (isEng
				? user.EngFullName ?? Linguistics.Translite(user.LastName + " " + user.FirstName, true)
				: user.FullName);
			return View((string) "TestCert", (object) Tuple.New(userName, certName, userTest));
		}

		public ActionResult TestCertGroup(TestCertGroupVM model) {
			var studentIds = StudentInGroupService.GetAll(x => x.Group_ID == model.GroupId)
				.Select(x => x.Student_ID).ToList();
			model.UserTests = new List<UserTest>();
			if (model.GroupId > 0) {
				var userIds = UserService.GetAll(x => x.Student_ID.HasValue &&
					studentIds.Contains(x.Student_ID.Value)).Select(x => x.UserID).ToList();
				var userTests = UserTestService.GetAll(x => x.TestId == model.TestId
					&& userIds.Contains(x.UserId) && UserTestStatus.PassStatuses.Contains(x.Status)).ToList();
				model.UserTests = userTests;
			}
			model.Tests = TestService.GetAll(x => x.Status == TestStatus.Active).ToList()
				.OrderBy(x => x.Name).ToList();

			return View(model);
		}


		public ActionResult SpecialistOrders(string text) {
			if (System.Web.HttpContext.Current.Request.IsPost()) {
				var sigIds = Regex.Split(text, @"[^\d]").Where(x => !x.IsEmpty()).Select(decimal.Parse).ToList();
				var sources = SourceService.GetAll().Select(x => new {x.Source_ID, x.Name})
					.ToDictionary(x => x.Source_ID, x => x.Name);
				var result = sigIds.GetRows(1000).SelectMany(ids => 
				
				OrderDetailService.GetAll(x =>
					ids.Contains(x.StudentInGroup_ID.Value))
							.Select(x => new{x.OrderID, x.StudentInGroup_ID}).ToList())
					.ToDictionary(x => x.StudentInGroup_ID, x => x.OrderID);

				var sigs = sigIds.GetRows(1000).SelectMany(ids =>
					StudentInGroupService.GetAll(x =>
						ids.Contains(x.StudentInGroup_ID))
						.Select(x => new {x.StudentInGroup_ID, x.StudentInGroupSources}).ToList()
					).Select(x => _.List(x.StudentInGroup_ID.ToString(), 
					result.GetValueOrDefault(x.StudentInGroup_ID).NotNullString() ?? 
					x.StudentInGroupSources.Select(y => y.Source_ID).ToList().Select(y => sources[y]).FirstOrDefault()));
					
			return File(StringUtils.Encoding1251.GetBytes(CsvUtil.Render(sigs)), 
				"text/csv", "SpecialistOrders.csv");
			}

			return View();

		}

		public ActionResult PaidOrders(string text) {
			if (System.Web.HttpContext.Current.Request.IsPost()) {
				var orderIds = Regex.Split(text, @"[^\d]").Where(x => !x.IsEmpty()).Select(decimal.Parse).ToList();
				var result = orderIds.GetRows(1000).SelectMany(ids => 
				OrderDetailService.GetAll(x =>
					ids.Contains(x.OrderID.Value)).Where(x =>
						BerthTypes.PaidReport.Contains(
							x.StudentInGroup.BerthType_TC))
							.Select(x => new{x.OrderID, x.StudentInGroup.BerthType_TC}).ToList()
							.GroupBy(x => x.OrderID)
					.Select(x => new {
						x.Key,
						BerthType = x.Select(y => y.BerthType_TC).Max()
					}).ToList()).Select(x => _.List(x.Key.ToString(), x.BerthType)).ToList();
			return File(StringUtils.Encoding1251.GetBytes(CsvUtil.Render(result)), 
				"text/csv", "PaidOrders.csv");
			}

			return View();

		}

		public ActionResult Recommendations(RecommendationsVM model) {
			var courses = new List<Course>();
			var completeCourseTC = new List<string>();
			var testCourseTC = new List<string>();
			foreach (var key in ModelState.Keys) {
				ModelState[key].Errors.Clear();
			}
			if (System.Web.HttpContext.Current.Request.IsPost()) {
				var user = UserService.GetAll(x => x.Email == model.Email).FirstOrDefault();
				if (user == null) {
					var student = StudentService.FirstOrDefault(x =>
						x.StudentEmails.Any(z => z.Email == model.Email));
					if (student != null)
						user = new User {
							Student = student
						};
				}
				if (user == null) {
					this.ModelState.AddModelError("", "Слушатель не существует");
				} else {
					if ((model.ProductId == 0 && model.ProfessionId == 0)
						|| (model.ProductId > 0 && model.ProfessionId > 0)) {
						this.ModelState.AddModelError("", "Выберите профессию или продукт");
					} else {
						var entityCourseTCList = EntityStudySetService.GetByPK(
							Math.Max(model.ProductId, model.ProfessionId)).CourseTCList;
						completeCourseTC = GetCompleteCourses(user);

						courses = GetCourses(entityCourseTCList, completeCourseTC);
						if (user.UserID > 0) {
							var testIds = UserTestService.GetAll(x => x.UserId == user.UserID
								&& UserTestStatus.PassStatuses.Contains(x.Status))
								.Select(x => x.TestId).Distinct().ToList();
							testCourseTC = SiteObjectRelationService.GetRelation(typeof(Test),
								testIds.Cast<object>(), typeof(Course))
								.Select(x => x.RelationObject_ID).ToList().Cast<string>()
								.ToList();
						}
					}
				}

			} else {
				model = new RecommendationsVM();
			}
			var sets = EntityStudySetService.GetAll().ToList();
			model.Professions = sets.Where(x => x.Type == 1).OrderBy(x => x.EntityName).ToList();
			model.Products = sets.Where(x => x.Type == 2).OrderBy(x => x.EntityName).ToList();
			model.Courses = courses;
			model.TestCourseTCs = testCourseTC;
			model.CompleteCourseTCs = completeCourseTC;
			return View(model);
		}

		private List<Course> GetCourses(string entityCourseTCList, List<string> completeCourseTC) {
			List<Course> courses;
			var courseTCs = StringUtils.SafeSplit(entityCourseTCList);
			var unStudyCourseTCs = courseTCs
				.Where(x => !completeCourseTC.Contains(x)).ToList();
			var prerequsisite = CoursePrerequisiteService.GetAll(x =>
				unStudyCourseTCs.Contains(x.Course_TC)
					&& x.RequiredCourse_TC != null && x.RequiredCourse.IsActive)
				.Select(x => new {
					x.Course_TC,
					x.RequiredCourse_TC
				}).ToList().GroupByToDictionary(x => x.Course_TC,
					x => x.RequiredCourse_TC);
			courseTCs = courseTCs.SelectMany(x =>
				_.List(x).AddFluent(prerequsisite.GetValueOrDefault(x)))
				.ToList();
			courseTCs = courseTCs.Distinct().ToList();
			courses = CourseService.GetAll(x => courseTCs.Contains(x.Course_TC))
				.ToList();
			return courses;
		}

		[HttpPost]
		public ActionResult GetTracks(List<string> courseTCs) {
			var result = new List<string>();
			if (courseTCs.Any()) {
				var trackCourses =
					TrackService.GetAll(x =>
						x.TrackCourse.IsActive && courseTCs.Contains(x.Course_TC))
						.Select(x => new {
							x.Track_TC, x.Course_TC
						})
						.ToList().GroupByToDictionary(x => x.Track_TC, x => x.Course_TC);
				result = trackCourses.Where(x => x.Value.All(courseTCs.Contains))
				   .Select(x => x.Key).ToList();

			}

			return Json(result);
		}

		private List<string> GetCompleteCourses(User user) {
			var completeCourseTC = new List<string>();
			var student = user.Student;
			if (student != null)
				completeCourseTC =
					student.GetPaidGroups().Select(x => x.Group.Course_TC).ToList();
			var parentTCs = CourseService.GetAll(x =>
				completeCourseTC.Contains(x.ParentCourse_TC)).Select(x => x.ParentCourse_TC)
				.ToList();
			return CourseService.GetAll(x => parentTCs.Contains(x.ParentCourse_TC)
				&& x.IsActive).Select(x => x.Course_TC).ToList()
				.AddFluent(completeCourseTC);
		}
	}
}