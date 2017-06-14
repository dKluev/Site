using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Console;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Context;
using Specialist.Services.Catalog;
using Specialist.Services.Core.Interface;
using Specialist.Web.Cms.Logic;
using Specialist.Web.Cms.Root.Recommendations;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using SubSonic.Repository;

namespace Specialist.Web.Cms.Controllers {
	public class PublicController : Controller {
		[Dependency]
		public Specialist.Services.Core.Interface.IRepository<Order> OrderService { get; set; }

		[Dependency]
		public IRepository2<Course> CourseService { get; set; }

		[Dependency]
		public IRepository2<Section> SectionService { get; set; }

		[Dependency]
		public ISiteObjectRelationService SiteObjectRelationService { get; set; }

		[Dependency]
		public YandexAuthService YandexAuthService { get; set; }

		[Dependency]
		public IRepository2<CoursePrerequisite> CoursePrerequisiteService { get; set; }

		[Dependency]
		public PaperCatalogData PaperCatalogData { get; set; }

		[Dependency]
		public Specialist.Services.Core.Interface.IRepository<OrderDetail> OrderDetailService { get; set; }
		public ActionResult OrdersByDirections(string date) {
			var datetime = date.IsEmpty() 
				? DateTime.Today.AddDays(-14) 
				: DateTime.ParseExact(date, "dd.MM.yyyy", null);
			var orders = OrderService.GetAll(x => x.UpdateDate >= datetime
				&& x.UpdateDate < DateTime.Today && x.OrderDetails.Any()
					&& x.Started).Select(x => new {x.PaymentType_TC, x.UpdateDate,
						x.OrderDetails.First().Course.CourseDirectionA_TC}).ToList().Select(
						x => new {x.PaymentType_TC, x.UpdateDate.Date, x.CourseDirectionA_TC})
						.OrderByDescending(x => x.Date);

			var gr = orders.GroupBy(x => new {x.Date, x.CourseDirectionA_TC});


			var table = H.table[
				H.Head("Дата", "Направление", "Н", "З"),
				gr.Select(x => {
					var count = x.Count(y => y.PaymentType_TC != null);
					return H.Row(
						x.Key.Date.DefaultString(),
						x.Key.CourseDirectionA_TC,
						x.Count() - count, count);
				})];
			return File(new UTF8Encoding().GetBytes(table.ToString()),
				"application/ms-excel", "ordersbydirections{0}.xls"
				.FormatWith(DateTime.Today.AddDays(-1).DefaultString()));
		}

		public ActionResult Orders() {
			var orders = OrderService.GetAll(x => x.UpdateDate >= new DateTime(2011, 6, 1)
				&& x.UpdateDate < DateTime.Today
					&& x.Started).Select(x => new {x.PaymentType_TC, x.UpdateDate}).ToList().Select(
						x => new {x.PaymentType_TC, x.UpdateDate.Date}).OrderByDescending(x => x.Date);

			var gr = orders.GroupBy(x => x.Date);


			var table = H.table[
				H.Head("Дата", "Н", "З"),
				gr.Select(x => {
					var count = x.Count(y => y.PaymentType_TC != null);
					return H.Row(
						x.Key.DefaultString(),
						x.Count() - count, count);
				})];
			return File(new UTF8Encoding().GetBytes(table.ToString()),
				"application/ms-excel", "orders{0}.xls".FormatWith(DateTime.Today.AddDays(-1).DefaultString()));
		}

		public ActionResult TestCertOrders(string date) {
			var endDate = date.IsEmpty() 
				? DateTime.Today 
				: DateTime.ParseExact(date, "dd.MM.yyyy", null).AddDays(1);
			var startDate = endDate.AddDays(-7);
			var orders = OrderDetailService.GetAll(x => x.CreateDate >= startDate
				&& x.CreateDate <= endDate
					&& x.Order.PaymentType_TC != null && x.UserTestId != null)
				.Select(x => new {x.UserTest.Test.Name}).ToList();

			var gr = orders.GroupBy(x => x.Name);

			var table = H.table[
				H.Head("Тест", "К"),
				gr.Select(x => H.Row( x.Key, x.Count()))];
			return File(new UTF8Encoding().GetBytes(table.ToString()),
				"application/ms-excel", "testcertorders{0}.xls"
				.FormatWith(endDate.AddDays(-1).DefaultString()));
		}




		public ActionResult CourseRecommendations(string id) {
			var model = new List<FullCourseCoef>();
			if (!id.IsEmpty()) {
				var parentTC = CourseService.GetValues(id, x => x.ParentCourse_TC);
				var context = new RecRepository().GetContext();
				var coefs = GetCoefs(context, parentTC);
				if (coefs.Any()) {
					var preCourses = CoursePrerequisiteService.GetAll(x => x.Course_TC == id)
						.Select(x => x.RequiredCourse.ParentCourse_TC).ToList();
					coefs = coefs.Where(x => !preCourses.Contains(x.Item1)).ToList();
					
					
					model = GetCourses(coefs);
				}
			}
			return View(model);
		}

		private List<FullCourseCoef> GetCourses(List<Tuple<string, decimal>> coefs) {
			var courseTCs = coefs.Select(x => x.Item1).ToList();
			var coefByCourse = coefs.ToDictionary(x => x.Item1.ToUpper(), x => x.Item2);
			var courses = CourseService.GetAll(x => x.IsActive 
				&& courseTCs.Contains(x.ParentCourse_TC) && x.IsTrack != true).ToList();
			return courses.Select(x => new FullCourseCoef(x, coefByCourse[x.ParentCourse_TC.ToUpper()]))
				.OrderByDescending(x => x.Coef).ToList();
		}

		public ActionResult SectionRecommendations(int? id) {
			var model = new SectionRecVM{};
			model.Sections = SectionService.GetAll(x => x.IsActive && !x.IsMain && !x.RelationsAsMenu).ToList();
			if (id.HasValue) {
				var coefs = Section(id.Value);
				model.Coefs = GetCourses(coefs[0].ToList());
				if(coefs.Count > 1)
					model.ExcludeCoefs = GetCourses(coefs[1].ToList()).OrderBy(x => x.Coef).ToList();
			}
			return View(model);
		}

		public ActionResult CatalogData() {
			var data = PaperCatalogData.GetCsv();
			return File(StringUtils.Encoding1251.GetBytes(data), "text/csv", "data.csv");
		}

		private static List<Tuple<string, decimal>> GetCoefs(SimpleRepository context, string parentTC) {
			var coefs = context.Find<CourseCoef>(x => x.CourseTC == parentTC)
				.Select(x => Tuple.Create(x.CourseTC2, x.Coef))
				.Concat(context.Find<CourseCoef>(x => x.CourseTC2 == parentTC)
					.Select(x => Tuple.Create(x.CourseTC, x.Coef))).ToList();
			return coefs;
		}

		List<IGrouping<bool, Tuple<string, decimal>>> Section(int sectionId) {
			var courseTCs = SiteObjectRelationService
				.GetByRelation(typeof(Section), sectionId, typeof(Course)).Select(x => x.Object_ID.ToString())
				.ToList();
			var parents = CourseService.GetAll(x => courseTCs.Contains(x.Course_TC) 
				&& x.IsTrack != true)
				.Select(x => x.ParentCourse_TC).Distinct().ToList();
			var context = new RecRepository().GetContext();
			var coefs = parents.SelectMany(x => GetCoefs(context, x))
				.GroupBy(x => x.Item1, x => x.Item2).Select(x => Tuple.Create(x.Key, x.Sum()/x.Count()));
			var zero = parents.Except(coefs.Select(x => x.Item1)).Select(x => Tuple.Create(x, decimal.Zero));
			coefs = coefs.Concat(zero);
			var coefs2 = coefs
				.GroupBy(x => parents.Contains(x.Item1));
			return coefs2.ToList();
		}

		public ActionResult YandexAuth(string code) {
			var token = YandexAuthService.GetToken(code);
			//Session[YandexAuthService.SessionKey] = token;
			return Content(token);
		}

	}
}