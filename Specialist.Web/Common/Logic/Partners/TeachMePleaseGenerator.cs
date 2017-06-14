using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using MvcContrib;
using NLog;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Services.Interface;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Services.Common {
	public class TeachMePleaseGenerator:XmlShopGenerator {
		[Dependency]
    	public IPriceService PriceService { get; set; }

		[Dependency]
    	public IRepository2<CourseDirectionsA> CourseDirectionService { get; set; }

		[Dependency]
    	public IRepository2<Lecture> LectureService { get; set; }

		[Dependency]
    	public IGroupService GroupService { get; set; }
		 
    	private UrlHelper _url;
		private Dictionary<string, string> _directions; 
		private Dictionary<decimal, int> _lectureCount; 
        public XDocument Get(UrlHelper urlHelper) {
        	_url = urlHelper;
	        _directions = CourseDirectionService.GetAll().ToDictionary(x => x.CourseDirectionA_TC, x => x.DirectionAName);
	        _lectureCount = LectureService.GetAll(x =>
		        x.Group.DateBeg >= DateTime.Today
			        && Colors.ForSite.Contains(x.Group.Color_TC)
			        && x.Group.LectureType_TC == LectureTypes.Planned)
		        .GroupBy(x => x.Group_ID).Select(x => new {x.Key, Count = x.Count()}).ToList().ToDictionary(x => x.Key, x => x.Count);
            var yaml =
             new XDocument(Programs());
            return yaml;
        }


		public XElement Programs() {
			var courses = GetCourses().Where(x => 
				x.Prices.Any(y => y.IsMain) && !x.Course.IsTrackBool);

    		var courseDescs = GetCourseDescs();
    		var courseShortDescs = GetCourseShortDescs();
			return X("courses",
				courses.Select(x => Course(x, 
					StringUtils.RemoveTags(courseDescs.GetValueOrDefault(x.Course.Course_TC)),
					StringUtils.RemoveTags(courseShortDescs.GetValueOrDefault(x.Course.Course_TC))
					)));
		}




		XElement TimeTable(Group g) {
			var lessonCount = _lectureCount.GetValueOrDefault(g.Group_ID);
			var minutes = (int)((g.Course.BaseHours + g.Course.AdditionalHours)*45);
			var dur = (int)(g.TimeEnd.Value - g.TimeBeg.Value).TotalMinutes;
			return X("timetable",
				X("date-by-agreement", "false"),
				X("time-by-agreement", "false"),
				X("start-date", g.DateBeg.GetValueOrDefault().Date.ToString("yyyy-MM-dd")),
				X("end-date", g.DateEnd.GetValueOrDefault().Date.ToString("yyyy-MM-dd")),
				X("start", g.TimeBeg.GetValueOrDefault().ToShortTimeString()),
				X("duration", dur),
				X("lessons-count", lessonCount == 0 ? Math.Max(1,minutes/dur) : lessonCount)
//				X("weekdays", g.DaySequence_TC.GetOrDefault(DaySequences.GetName) ?? g.DaySequence)
				);
		}

		XElement Address(Group g) {
			var ca = Cities.Complexes.Addresses.GetValueOrDefault(g.Complex_TC);
			if (ca == null) {
				return X("address",
					X("country", "Россия"),
					X("city", "Москва")
					);
			} else {
			return X("address",
				X("country", "Россия"),
				X("city", "Москва"),
				X("street", ca.Street),
				X("house-number", ca.FullHouse),
				X("floor", ca.Floor),
				X("zip-code", ca.ZipCode));
			}
			
		}

		List<XElement> Lesson(Group g) {
			var webinarPrice = PriceService.GetPriceByType(g.Course_TC, PriceTypes.Webinar, null);
			var mainPrice = PriceService.GetPriceByType(g.Course_TC, PriceTypes.PrivatePersonWeekend, null);
			var main = mainPrice > 0 ? LessonElement(g, false, mainPrice) : null;
			var webinar = g.WebinarExists && webinarPrice > 0 ? LessonElement(g, true, webinarPrice) : null;
			return _.List(webinar, main);
		}

		private XElement LessonElement(Group g, bool isWebinar, decimal? price) {
			var tp = isWebinar ? "online" : "offline";
			var postfix = isWebinar ? "00" : "01";
			return X("lesson",
				X("id", g.Group_ID + postfix),
				X("type", tp),
				X("method", isWebinar ? 13 : 21),
				X("price", (int)price),
				X("pay-target", isWebinar ? 3 : 4),
				TimeTable(g),
				isWebinar ? null : Address(g) 
				);
		}

		private XElement Course(CommonCourseListItemVM item, string desc, string shortDesc) {
    		try {
    			if ((desc.IsEmpty() && shortDesc.IsEmpty()) || item.NearestWebinar == null) {
    				return null;
    			}
    			var groups = GroupService.GetGroupsForCourse(item.Course.Course_TC).Take(15).ToList();

    			if (!groups.Any()) {
    				return null;
    			}
    			var c = item.Course;
    			var category = _directions.GetValueOrDefault(c.CourseDirectionA_TC);
    			shortDesc = shortDesc ?? category;
    			var maxLength = 240;
    			var shortDesc160 = shortDesc.Length > maxLength
    				? StringUtils.SafeSubstring(shortDesc, maxLength - 3) + "..."
					: shortDesc;
    			return X("course",
    				X("id", c.Course_ID),
    				X("name", StringUtils.RemoveTags(c.WebName)),
    				X("short-description", category),
    				X("description", desc),
					X("tags", X("tag", category)),
					X("lessons", groups.SelectMany(Lesson))
					);
    		}
    		catch (Exception e) {
    			Logger.Exception(e, "course error " + item.Course.Course_ID + " " + item.Course.Course_TC);
    			return null;
    		}
    	}
/*
    	private XElement GetCategory(Section section) {
    		return X("Category", A("name",section.Name), 
    			section.SubSections.Select(x => GetCategory(section)));
    	}*/
	}
}