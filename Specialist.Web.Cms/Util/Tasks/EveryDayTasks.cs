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
using SimpleUtils.Extension;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;
using Specialist.Entities.Tests.Consts;
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
using Specialist.Entities.Lms;
using Specialist.Services.Order;
using Specialist.Web.Root.Profile.Services;
using Group = Specialist.Entities.Context.Group;

namespace Specialist.Web.WinService.Tasks
{
    public class EveryDayTasks : TaskWithTimer
    {
        public override double Minutes
        {
            get { return 60; }
        }


        public override void TimerTick() {
	        if (IsTodayDone)
		        return;
			UpdateBirthDates();
	        UpdateTestCalc();
			ImportVimeoAlbums();
        }


	    public void ImportVimeoAlbums() {
		    var unity = Cms.MvcApplication.Container;
		    var albumVideoService = unity.Resolve<AlbumVideoService>();
		    var albumIds = albumVideoService.GetAll().Select(x => x.AlbumId).ToList()
			    .Select(x => "https://vimeo.com/album/" + x).ToList();
		    var employeeCourseService = unity.Resolve<IRepository2<EmployeesCourse>>();
		    var lastAlbums = employeeCourseService.GetAll(x => 
			x.BroadcastingURL.Contains("vimeo") && !albumIds.Contains(x.BroadcastingURL))
			    .OrderByDescending(x => x.LastChangeDate).Select(x => x.BroadcastingURL)
			    .ToList().Select(x => long.Parse(StringUtils.GetVimeoAlbumId(x)))
				.Take(10).ToList();
		    foreach (var newAlbumId in lastAlbums) {
		        albumVideoService = unity.Resolve<AlbumVideoService>();
				albumVideoService.EnableTracking();
			    albumVideoService.AddVimeoAlbum(newAlbumId);
		    }

	    }

	    public void ExportOrders() {
		    var startDate = DateTime.Today.AddDays(-1);
		    var unity = Cms.MvcApplication.Container;
		    var exportSerivice = unity.Resolve<ISpecialistExportService>();
		    var orderService = unity.Resolve<IRepository2<Order>>();
		    var orderids = orderService.GetAll(x => x.UpdateDate >= startDate
			    && !Equals(x.PaymentType_TC, null) 
				&& Equals(x.User.CompanyID, null)
				&& !(x.OrderDetails.Any(y => y.StudentInGroup_ID.HasValue) || x.OrderExams.Any(y => y.StudentInGroup_ID.HasValue)))
			    .Select(x => x.OrderID).Distinct().ToList();
		    foreach (var orderid in orderids) {
			    exportSerivice.Export(orderid, true, null);
		    }
	    }

	    public void UpdateBirthDates() {
		    var unity = Cms.MvcApplication.Container;
		    var studentService = unity.Resolve<IRepository2<PiStudent>>();
			studentService.EnableTracking();
		    var userService = unity.Resolve<IRepository2<User>>();
		    var studentBirth = userService.GetAll(x =>
			    x.BirthDate.HasValue && x.Student_ID.HasValue && !x.Student.BirthDate.HasValue)
			    .OrderByDescending(x => x.UserID)
			    .Select(x => new {BirthDate = x.BirthDate.Value, StudentId = x.Student_ID.Value})
			    .Take(100).ToList().DistinctToDictionary(x => x.StudentId, x => x.BirthDate);
		    var studentIds = studentBirth.Select(x => x.Key).ToList();
		    var students = studentService.GetAll(x => studentIds.Contains(x.Student_ID)).ToList();
			students.ForEach(s => s.BirthDate = studentBirth[s.Student_ID]);
			studentService.SubmitChanges();
	    }

	    private static void UpdateTestCalc() {
		    var unity = Cms.MvcApplication.Container;
		    var testCalcService = unity.Resolve<IRepository<TestCalc>>();
		    var UserTestService = unity.Resolve<IRepository2<UserTest>>();

		    var tryCounts = UserTestService.GetAll()
			    .GroupBy(x => x.TestId)
			    .Select(x => new {x.Key, Count = x.Count()})
			    .ToDictionary(x => x.Key, x => x.Count);
		    var passCounts = UserTestService.GetAll(x => UserTestStatus.PassStatuses.Contains(x.Status))
			    .GroupBy(x => x.TestId)
			    .Select(x => new {x.Key, Count = x.Count()})
			    .ToDictionary(x => x.Key, x => x.Count);
		    var userCounts = UserTestService.GetAll().Select(x => new {x.TestId, x.UserId}).Distinct()
			    .GroupBy(x => x.TestId)
			    .Select(x => new {x.Key, Count = x.Count()})
			    .ToDictionary(x => x.Key, x => x.Count);
		    var all = testCalcService.GetAll().ToDictionary(x => x.TestId, x => x);
		    var testCalcs = tryCounts.Select(x => new TestCalc() {
			    PassCount = passCounts.GetValueOrDefault(x.Key),
			    TryCount = x.Value,
			    UserCount = userCounts.GetValueOrDefault(x.Key),
			    TestId = x.Key
		    });
		    foreach (var testCalc in testCalcs) {
			    var old = all.GetValueOrDefault(testCalc.TestId);
			    if (old != null) {
				    old.Update(testCalc, x => x.PassCount, x => x.TryCount, x => x.UserCount);
			    }
			    else {
				    testCalcService.Insert(testCalc);
			    }
		    }
		    testCalcService.SubmitChanges();
	    }
    }
}
