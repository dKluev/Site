using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using NLog;
using SimpleUtils.Utils;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.ViewModel;
using Specialist.Services.Interface;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Html;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Services.Common {
	public class SverhMarketGenerator:XmlShopGenerator {
		 
    	private UrlHelper _url;
        public XDocument Get(UrlHelper urlHelper) {
        	_url = urlHelper;
            var yaml =
             new XDocument(
             X("root", 
					 Categories(),
					 Goods()
				 )
             );
            return yaml;
        }

		public const string rootCategory = "Курсы";

		public XElement Categories() {
			return X("Categories",
				X("Category", A("name", rootCategory)));
		}
		public XElement Goods() {
			var courses = GetCourses().Where(x => 
				x.Prices.Any(y => y.IsMain));

    		var courseDescs = GetCourseDescs();
    		var courseShortDescs = GetCourseShortDescs();
			return X("Goods",
				courses.Select(x => Good(x, 
					StringUtils.RemoveTags(courseDescs.GetValueOrDefault(x.Course.Course_TC)),
					StringUtils.RemoveTags(courseShortDescs.GetValueOrDefault(x.Course.Course_TC))
					)));
		}


    	private XElement Good(CommonCourseListItemVM item, string desc, string shortDesc) {
    		try {
    			if (shortDesc == null)
    				return null;
    			var f = "False";
    			var t = "True";
    			var c = item.Course;
    			var image = Images.Course(c.UrlName).Attribute("src");
    			if (image == null)
    				return null;
    			return X("Good",
    				A("article", "c" + c.Course_ID),
    				A("name", c.WebName),
    				A("cargoRegistered", f),
    				A("isVirtual", t),
    				A("img", image.Value),

    				A("cost", (int) item.Prices.First(x => x.IsMain).Price),
    				A("shortDescription", shortDesc),
    				A("description", desc),
    				A("categories", rootCategory));
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