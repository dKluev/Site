using System;
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
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Services.Common {
	public class GoogleMerchantGenerator:XmlShopGenerator {
		 
    	private UrlHelper _url;
        XNamespace g = "http://base.google.com/ns/1.0";
        public XDocument Get(UrlHelper urlHelper) {
        	_url = urlHelper;
            var yaml =
             new XDocument(
             X("rss", 
			 A("version","2.0"),
			 new XAttribute(XNamespace.Xmlns + "g", g.ToString()),
					 Goods()
				 )
             );
            return yaml;
        }


		public XElement Goods() {
			var courses = GetCourses().Where(x => 
				x.Prices.Any(y => y.IsMain));

    		var courseDescs = GetCourseDescs();
    		var courseShortDescs = GetCourseShortDescs();
			return X("channel",
				 X("title","Специалист"),
				 X("link","http://www.specialist.ru/"),
				 X("description", "Центр «Специалист» при МГТУ им. Н.Э.Баумана"),
				courses.Select(x => Good(x, 
					StringUtils.RemoveTags(courseDescs.GetValueOrDefault(x.Course.Course_TC)),
					StringUtils.RemoveTags(courseShortDescs.GetValueOrDefault(x.Course.Course_TC))
					)));
		}



		string GetShortName(Course c) {
			var max = 150;
			if (c.WebName.Length > max) {
				if (c.WebShortName != null) return StringUtils.SafeSubstring(c.WebName, max);
			}
			return StringUtils.SafeSubstring(c.WebName,max);
		}
    	private XElement Good(CommonCourseListItemVM item, string desc, string shortDesc) {
    		try {
    			if (shortDesc == null)
    				return null;
    			var c = item.Course;
    			var image = Images.Course(c.UrlName).Attribute("src");
    			if (image == null)
    				return null;
    			return X("item",
    				X("title", GetShortName(c)),
    				X("description", desc),
					X("link", CommonConst.SiteRoot +
						(c.IsTrackBool
							? _url.Action<TrackController>(x => x.Details(c.UrlName))
							: _url.Action<CourseController>(x => x.Details(c.UrlName))) + 
							StringUtils.GetUtmPart("google","cpc","merchant")),
    				X(g + "id", "c" + c.Course_ID),
    				X(g + "image_link", image.Value),
    				X(g + "availability", "in stock"),
    				X(g + "product_type", "Курс"),
    				X(g + "price", (int) item.Prices.First(x => x.IsMain).Price + " RUB"),
    				X(g + "condition", "new"));
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