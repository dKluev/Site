using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using MvcContrib.ActionResults;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Const;
using Specialist.Entities.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Entities.ViewModel;
using Specialist.Services;
using Specialist.Services.Center.ViewModel;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Logic;
using Specialist.Web.Common.Mvc.Controllers;
using Specialist.Web.Const;
using Specialist.Web.Util;
using System.Linq.Dynamic;
using SimpleUtils;
using SimpleUtils.Reflection;
using Specialist.Services.Common.Extension;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.Common.Extension;
using SimpleUtils.Extension;

namespace Specialist.Web.Controllers
{
    public class TrackController : ExtendedController
    {
        
        [Dependency]
        public ITrackVMService TrackVMService { get; set; }

        [Dependency]
        public ICourseService CourseService { get; set; }

		[HandleNotFound]
        public ActionResult Details(string urlName)
        {
            if (urlName.In("t-tor","t-sks", "torsh", "t-tor48")) {
            	return this.RedirectToAction<CourseController>(c => c.Details(urlName));
            }
			if(Regex.IsMatch(urlName.ToLowerInvariant(), "[à-ÿ]")) {
				var track = CourseService.FirstOrDefault(x => x.Course_TC == urlName);
				if (track == null)
					return null;
				return RedirectToAction<TrackController>(tc => 
					tc.Details(track.UrlName));
			}
			var model = TrackVMService.GetByUrlName(urlName);
			if (model != null && !model.Course.IsTrackBool) {
            	return RedirectToAction<CourseController>(c => c.Details(urlName));
			}
            
            return View(model);
        }

		public ActionResult List(string id) {
			return View(new TrackListVM {
				Course = CourseService.GetByPK(id),
			});
		}

      
    }
}