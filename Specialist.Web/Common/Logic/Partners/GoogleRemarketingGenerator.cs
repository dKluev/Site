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
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Html;
using Specialist.Web.Controllers;
using Logger = Specialist.Services.Utils.Logger;

namespace Specialist.Services.Common {
	public class GoogleRemarketingGenerator:XmlShopGenerator {
		 
    	private UrlHelper _url;
        public string Get(UrlHelper urlHelper) {
        	_url = urlHelper;
	        var data = Goods();
			data.Insert(0, _.List(
				"Program ID",
				"Program name",
				"Destination URL",
				"Thumbnail image URL",
				"Image URL",
				"Program description",
				"School name",
				"Contextual keywords"
				));
            return CsvUtil.Render(data, ",");
        }


		public List<List<string>> Goods() {
			var courses = GetCourses().Where(x => 
				x.Prices.Any(y => y.IsMain));

    		var courseDescs = GetCourseDescs();
    		var courseShortDescs = GetCourseShortDescs();
			return courses.Select(x => Good(x, 
					StringUtils.RemoveTags(courseDescs.GetValueOrDefault(x.Course.Course_TC)),
					StringUtils.RemoveTags(courseShortDescs.GetValueOrDefault(x.Course.Course_TC))))
					.Where(x => x != null).ToList();
		}

    	private List<string> Good(CommonCourseListItemVM item, string desc, string shortDesc) {
    		try {
    			if (shortDesc == null)
    				return null;
    			var c = item.Course;
    			var image = Images.Course(c.UrlName).Attribute("src");
    			if (image == null)
    				return null;
    			return _.List(
    				"c" + c.Course_ID,
    				StringUtils.RemoveTags(c.GetShortName()),
					CommonConst.SiteRoot +
						(c.IsTrackBool
							? _url.Action<TrackController>(x => x.Details(c.UrlName))
							: _url.Action<CourseController>(x => x.Details(c.UrlName))),
    				image.Value,
    				image.Value,
    				shortDesc,
    				"«Специалист» при МГТУ им.Баумана",
					"курсы;обучение;вебинар;семинар");
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