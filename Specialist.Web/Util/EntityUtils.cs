using System;
using System.Collections.Generic;
using Bing;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;
using Newtonsoft.Json;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Common.Extensions;
using System.Linq;
using SimpleUtils.Utils;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Lms;
using Specialist.Entities.Tests.Consts;
using Specialist.Services.Common.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Root.Tests.ViewModels;

namespace Specialist.Web.Util {
	public static class EntityUtils {
		public static Dictionary<int, int> GetModulePercents(TestPassRule testPassRule) {
			if(testPassRule.ModulePercents.IsEmpty())
				return new Dictionary<int, int>();
			var percents = JsonConvert.DeserializeObject<Dictionary<int, int>>(testPassRule.ModulePercents);
			return percents;
		}

		public static HashSet<string> GetCourseTCs(MarketingAction action, HashSet<string> withWebinar) {
			if(action.CourseTCList == MarketingAction.WithWebinar)
				return withWebinar;
			return new HashSet<string>(action.CourseTCSplitList);
		} 

		public static void SetModulePercents(TestPassRule testPassRule, Dictionary<int, int> percents) {
			percents = percents ?? new Dictionary<int, int>();
			testPassRule.ModulePercents = JsonConvert.SerializeObject(percents);
		
		}
		public static UserTestStats GetStats(UserTest userTest) {
			if(userTest.Stats.IsEmpty())
				return new UserTestStats();
			return JsonConvert.DeserializeObject<UserTestStats>(userTest.Stats);
		}
		public static void SetStats(UserTest testPassRule, UserTestStats stats) {
			testPassRule.Stats = JsonConvert.SerializeObject(stats ?? new UserTestStats());
		}


		public static void TitleNames(PiStudent s) {
			s.FirstName = s.FirstName.ToTitleCase();
			s.MiddleName = s.MiddleName.ToTitleCase();
			s.LastName = s.LastName.ToTitleCase();
		}

		public static string GetTestCertName(bool isEng, UserTest userTest) {
			string passName = "{0}. Пользователь.";
			string averageName = "{0}. Опытный пользователь.";
			string expertName = "{0}. Специалист.";
			var names = new Dictionary<string, string> {
				{passName, "{0} User"},
				{averageName, "{0} Advanced User"},
				{expertName, "{0} Specialist"},
			};
			string template;
			if(!userTest.TestPassRule.AverageMark.HasValue) {
				template = expertName;
			}else {
				if(userTest.Status == UserTestStatus.Pass)
					template = passName;
				else if(userTest.Status == UserTestStatus.Average)
					template = averageName;
				else if(userTest.Status == UserTestStatus.Expert)
					template = expertName;
				else 
					throw new Exception("user test status is not pass");
			}
			var productName = userTest.Test.ProductName;
			if(isEng) {
				template = names[template];
				productName = userTest.Test.ProductNameEng;
			}
			return template.FormatWith(productName 
				?? userTest.Test.ProductName 
					?? userTest.Test.ProductNameEng
						?? userTest.Test.Name);
		}

		public static IQueryable<CoursePrerequisite> GetCoursePreCourses(IRepository2<CoursePrerequisite> coursePrerequisiteService, string courseTC) {
			return coursePrerequisiteService.GetAll(x => x.Course_TC == courseTC && x.RequiredCourse_TC != null);
		}


		public static void SetUpdateDateAndLastChanger(object entity, string employeeTC) {
			if(entity == null)
				return;
    		var entityType = entity.GetType();
    		var updateDate = entityType.GetProperty("UpdateDate");
    		var lastChanger = entityType.GetProperty("LastChanger_TC");
    		if(updateDate != null)
    			updateDate.SetValue(entity, DateTime.Now);
    		if(lastChanger != null) {
    			lastChanger.SetValue(entity, (object)employeeTC);
    		}
    	}

	    public static double GetLectureHours(DateTime begin, DateTime end, short breaks) {
		    return ((end - begin).TotalMinutes - breaks)/45;
	    }

		public static void UpdateLastChangerAndDate(dynamic entity, string employeeTC) {
			entity.LastChanger_TC = employeeTC;
			entity.LastChangeDate = DateTime.Now;
		}

		public static int RoundDiscount(decimal x) {
			return (int) Math.Round(x);
		}


	    public static string GetCalendar(List<CalData> lectures) {
		    var result = string.Empty;
		    using (var iCal = new iCalendar()) {
			    iCal.AddLocalTimeZone();
			    foreach (var calData in lectures) {
				    var evt = iCal.Create<Event>();
				    evt.Start = new iCalDateTime(calData.Begin);
				    evt.End = new iCalDateTime(calData.End);
				    evt.Location = calData.Location;
				    evt.Summary = calData.Title;
			    }
			    var serializer = new iCalendarSerializer();
			    result = serializer.SerializeToString(iCal);
			    result = result.Replace(
				    "PRODID:-//ddaysoftware.com//NONSGML DDay.iCal 1.0//EN\r\n",
				    "");
		    }
		    return result;
	    }

		public static string GetPromocode(MarketingAction action, Student student) {
			if (action.MarketingAction_ID == MarketingActions.Panasonic) {
				var promocode = MarketingActions.GetPanasonicPromocode(student.GetOrDefault(x => x.IsRealSpecialist));
				return H.p["Промокод: " + promocode].ToString();

			}
			return null;
		}

	}
}