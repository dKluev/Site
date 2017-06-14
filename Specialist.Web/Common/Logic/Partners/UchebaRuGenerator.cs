using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
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
using Specialist.Services.Core.Interface;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Services.Common {
	public class UchebaRuGenerator:XmlShopGenerator {
		[Dependency]
    	public IRepository2<CourseDirectionsA> CourseDirectionService { get; set; }
		 
    	private UrlHelper _url;
        public XDocument Get(UrlHelper urlHelper) {
        	_url = urlHelper;
            var yaml =
             new XDocument(Programs());
            return yaml;
        }


		public XElement Programs() {
			var courses = GetCourses().Where(x => 
				x.Prices.Any(y => y.IsMain) && !x.Course.IsTrackBool);

    		var courseDescs = GetCourseDescs();
    		var courseShortDescs = GetCourseShortDescs();
			var directions = CourseDirectionService.GetAll().ToDictionary(x => x.CourseDirectionA_TC, x => x.DirectionAName);
			return X("programs",
				courses.Select(x => Program(directions, x, 
					StringUtils.RemoveTags(courseDescs.GetValueOrDefault(x.Course.Course_TC)),
					StringUtils.RemoveTags(courseShortDescs.GetValueOrDefault(x.Course.Course_TC))
					)));
		}

		private XElement Form(Course c, decimal? price, Group start, bool isWebinar) {
			if (!price.HasValue || start == null) {
				return null;
			}
			var days = (start.DateEnd - start.DateBeg).Value.Days;
			var hours = (int) (c.BaseHourCalc + c.AdditionalHours); 
			return X("form",
				X("type", isWebinar ? "дистанционная" : "очная"),
				X("duration", 
					X("length", days),
					X("lengthType", "день"),
					X("hours", hours)
				),
				X("commerce", X("price", (int)price), X("currency", "RUB")),
				X("calendar", X("date", X("start", start.DateBeg.GetValueOrDefault().ToString("yyyy-MM-dd"))))
				);
		}

		private XElement Program(Dictionary<string, string> directions, CommonCourseListItemVM item, string desc, string shortDesc) {
    		try {
    			if (shortDesc == null || !item.HasNearestGroupOrWebinar) {
    				return null;
    			}
    			var c = item.Course;
    			var url = CommonConst.SiteRoot +
    				(c.IsTrackBool
    					? _url.Action<TrackController>(x => x.Details(c.UrlName))
    					: _url.Action<CourseController>(x => x.Details(c.UrlName)));
    			var type = c.IsSchool ? "подготовительные курсы" : "повышение квалификации";
    			var audiences = c.IsSchool ? "школьники" : "взрослые";
    			return X("program",
    				A("id", c.Course_ID),
    				X("name", StringUtils.RemoveTags(c.WebName)),
    				X("description", StringUtils.SafeSubstring(shortDesc, 800)),
    				X("link", url),
    				c.CourseDirectionA_TC == null 
					? null 
					: X("rubrics", X("rubric", A("id", c.CourseDirectionA_TC), directions[c.CourseDirectionA_TC])),
    				X("type", type),
    				X("audiences", X("audience", audiences)),
    				X("forms",
    					Form(c, item.GetPrice(PriceTypes.Webinar), item.NearestWebinar, true),
    					Form(c, item.GetPrice(PriceTypes.Main), item.NearestGroup, false)
    					));
    		}
    		catch (Exception e) {
    			Logger.Exception(e, item.Course.Course_ID + " " + item.Course.Course_TC);
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