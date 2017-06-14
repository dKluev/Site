using System;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Common.Extension;
using Specialist.Services.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Util;

namespace Console {
	public class GroupTimetableGenerator:H {
		public static int GetWeekNumber(DateTime dtPassed) {
			CultureInfo ciCurr = CultureInfo.CurrentCulture;

			int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFullWeek,
				DayOfWeek.Monday);

			return weekNum;
		}

		public string Get() {
						var context = new SpecialistDataContext();
			var groups = context.Groups.PlannedAndNotBegin()
				.Where(g => g.DateBeg > new DateTime(2011,1,1))
				.Select(g => new {g.Course_TC, Date = g.DateBeg.Value, g.Course.Name}).ToList();
			var orgs = _.List(PriceTypes.Corporate, PriceTypes.CorporateBusinessClass);
			var prices = context.PriceViews.Where(x => orgs.Contains(x.PriceType_TC)
				 && x.Track_TC == null).Select(x =>
					new {x.Course_TC, x.PriceType_TC, x.Price}).ToList();
			var courses = prices.Select(x => x.Course_TC).Distinct();
			Func<string, string, string> getPrice = (courseTC, priceTypeTC) =>
				prices.FirstOrDefault(p => p.Course_TC == courseTC
					&& p.PriceType_TC == priceTypeTC).GetOrDefault(x => (decimal?)x.Price)
					.NotNullString("n0");

			groups =
				(from g in groups
				group g by new {g.Course_TC, Week = GetWeekNumber(g.Date)}
				into gr
				select gr.First()).ToList();
			groups = groups.Where(g => courses.Contains(g.Course_TC)).ToList();
			var resultGroups = groups.GroupBy(x => x.Course_TC)
				.Select(x => new {
					Course_TC = x.Key,
					x.First().Name,
					Dates = x.OrderBy(z => z.Date).Select(z => z.Date.ToString("dd.MM")).JoinWith(" ")
				})
				.ToList();


			var webcontext = new SpecialistWebDataContext();
			var rootSections = webcontext.Sections.Where(s => s.IsMain)
				.ByWebOrder().IsActive().ToList();
			var unity = new UnityContainer();
			UnityRegistrator.RegisterServices(unity);
			var courseService = unity.Resolve<ICourseService>();
    		var sectionWithCourses = 
    			rootSections.Select(s => new {Section = s, CourseTCs =
    				courseService.GetCourseTCListForSections(
    					_.List(s.Section_ID))});
			/*var allSectionCourses = sectionWithCourses
				.SelectMany(x => x.CourseTCs).Distinct().ToList();
			var notInList = resultGroups.Where(g => !allSectionCourses.Contains(g.Course_TC)).ToList();*/
    		var groupBySections =  sectionWithCourses.Select(s =>
    			Grouping.New(s.Section,
    				resultGroups.Where(c => s.CourseTCs.Contains(c.Course_TC))))
    			.Where(s => s.Any()).ToList();


			return table[tr[
				th["Название курса"],
				th["Цена ОРГ"],
				th["Цена ОРГБ"],
				th["Расписание"]],
				groupBySections.Select(s => H.l(tr[td[s.Key.Name].Colspan(4)],
				s.OrderBy(x => x.Name).Select(g => tr[td[g.Name],
					td[getPrice(g.Course_TC, PriceTypes.Corporate)],
					td[getPrice(g.Course_TC, PriceTypes.CorporateBusinessClass)],
					td[g.Dates]] ) ))].ToString();

		}
		
	}
}