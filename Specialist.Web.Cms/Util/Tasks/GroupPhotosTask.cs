using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using Facebook;
using Microsoft.Practices.Unity;
using NLog;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Services.Common.Utils;
using Specialist.Services.Core.Interface;
using Specialist.Web.Cms;
using Specialist.Web.Cms.Root.Socials;
using Specialist.Web.Cms.Services;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Tasks;
using Specialist.Web.Const;
using Specialist.Web.Helpers;
using Logger = Specialist.Services.Utils.Logger;
using System.Linq;
using Specialist.Services.Catalog.Extension;
using Specialist.Web.Common.Extension;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Catalog.Links.Interfaces;
using Group = Specialist.Entities.Context.Group;

namespace Specialist.Web.WinService.Tasks
{
    public class GroupPhotosTask : TaskWithTimer
    {
        public override double Minutes
        {
            get { return 60; }
        }


        public override void TimerTick() {
	        if (IsTodayDone)
		        return;

	        var unity = Cms.MvcApplication.Container;
    		var sigService = unity.Resolve<IRepository2<StudentInGroup>>();
    		var userService = unity.Resolve<IRepository2<User>>();
    		var mailService = unity.Resolve<IMailService>();
	        var groups = Images.GetGroupsPhoto();
	        var yesterday = DateTime.Today.AddDays(-1);
	        var newGroups = groups.Where(x => x.Value.Item2.Date == yesterday)
		        .Select(x => x.Key).ToList();
	        foreach (var groupId in newGroups) {
		        var studentIds = sigService.GetAll(x => x.Group_ID == groupId
					&& BerthTypes.AllPaidForCourses.Contains(x.BerthType_TC)).Select(x => x.Student_ID).ToList();
		        var users = userService.GetAll(x => studentIds.Contains(x.Student_ID.Value)).ToList();
		        foreach (var user in users) {
			        var link = SimpleLinks.GroupPhotos(user.UserID)
				        .AbsoluteHref();
			        link.Href(link.Attribute("href").Value +
				        "?utm_source=auto-site&utm_medium=mail&utm_campaign=photo");
					mailService.NewGroupPhoto(user, link.ToString());
		        }
	        }
        }
	}
}
