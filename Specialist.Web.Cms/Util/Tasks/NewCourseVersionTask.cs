using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Services.Catalog;
using Specialist.Services.Common;
using Specialist.Services.Common.Interface;
using Specialist.Services.Core.Interface;
using Specialist.Web.Cms.Services;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils.Tasks;
using SimpleUtils.Collections.Extensions;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
namespace Specialist.Web.WinService.Tasks {
	public class NewCourseVersionTask : TaskWithTimer {
		public override double Minutes {
			get { return 60*3; }
		}

		public override void TimerTick() {
			var hour = DateTime.Now.Hour;
			if(hour.BetweenInclude(9,19)) {
				var courseEntityService = Cms.MvcApplication.Container.Resolve<CourseEntityService>();
				var courses = courseEntityService.GetNewVersionCourses()
					.Where(x => x.Item2);
				if(courses.Any()) {
					var mailService = Cms.MvcApplication.Container.Resolve<IMailService>();
				mailService.Send(new MailAddress("info@specialist.ru"),
					MailService.contmanagers,
					"", "[Новая версия курса]" + courses.Select(x => x.Item1).JoinWith(", "));
					
				}
			}
		}


	}
}