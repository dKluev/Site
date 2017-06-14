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
using Specialist.Entities.Catalog;
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
using Specialist.Services.Interface;
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
    public class SocialTask : TaskWithTimer
    {
        public override double Minutes
        {
            get { return 60; }
        }

		public static Task TimerForEach<T>(TimeSpan timeSpan, List<T> items, Action<T> func) {
			return Observable.Timer(TimeSpan.Zero, timeSpan)
				.Take(items.Count).Zip(items, (_, t) => t)
				.ForEachAsync(func);
		}

        public override void TimerTick() {
	        if (DateTime.Now.Hour < 12 )
		        return;
			PostNews();
        }

	    private static void PostTrainers() {
		    var today = DateTime.Today;
		    var month = today.AddDays(30);
		    var twoWeeks = today.AddDays(14);
		    var week = today.AddDays(7);
		    var tomorrow = today.AddDays(1);
		    var unity = Cms.MvcApplication.Container;
		    var userService = unity.Resolve<IRepository2<User>>();
		    var groupService = unity.Resolve<IRepository2<Group>>();
		    var blockService = unity.Resolve<IRepository2<HtmlBlock>>();
		    groupService.LoadWith(x => x.Course);
		    var trainerRole = (short) Role.Trainer;

		    var trainers = userService.GetAll(x => (x.Roles & trainerRole) == trainerRole
													&& x.Employee_TC != null && (x.FbToken != null || x.VkToken != null)
			    ).ToList();
		    var trainerTCs = trainers.Select(x => x.Employee_TC).ToList();
		    var trainerGroups = groupService.GetAll(x =>
			    trainerTCs.Contains(x.Teacher_TC) && x.Course.IsActive &&
				!CourseTC.AllSpecial.Contains(x.Course_TC))
			    .Where(g =>
				    g.DateBeg == month
					|| g.DateBeg == twoWeeks
					|| g.DateBeg == week
					|| g.DateBeg == tomorrow).PlannedAndNotBegin().ToList()
			    .GroupByToDictionary(x => x.Teacher_TC, x => x);
		    var trainersWithGroups = trainers.Select(x => Tuple.Create(x,
			    trainerGroups.GetValueOrDefault(x.Employee_TC))).Where(x => x.Item2 != null)
			    .ToList();

		    var messages = GetTemplates(blockService, HtmlBlocks.FacebookTexts);
		    var megaMessages = GetTemplates(blockService, HtmlBlocks.FacebookMegaTexts);
		    TimerForEach(TimeSpan.FromMinutes(10), trainersWithGroups, twg => {
			    var trainer = twg.Item1;
			    var allGroups = twg.Item2;
			    var megaGroups = allGroups.Where(x => x.MegaGroup_ID != null)
				    .GroupBy(x => x.MegaGroup_ID.Value).Select(x => x.ToList()).ToList();
			    var groups = allGroups.Where(x => x.MegaGroup_ID == null)
				    .Select(x => _.List(x)).Concat(megaGroups).ToList();

			    TimerForEach(TimeSpan.FromMinutes(25), groups, g => {
				    var message = g.First().MegaGroup_ID.HasValue
					    ? megaMessages.Random()
					    : messages.Random();
				    UpdateStatusForGroups(trainer, g, message);
			    });
		    }).Wait();
	    }

	    private static List<string> GetTemplates(IRepository2<HtmlBlock> blockService, int blockId) {
		    return blockService.GetValues(blockId, 
			    x => x.Description).Split(new [] {"</p>"},
				    StringSplitOptions.RemoveEmptyEntries)
			    .Select(x => x.Remove("<p>").Remove("\n").Remove("\r"))
			    .Where(x => x.Contains("[Date]")).ToList();
	    }

	    static void UpdateStatusForGroups(User user, List<Group> groups, string template) {
			try {
				var message = string.Empty;
				if (groups.First().MegaGroup_ID.HasValue) {
					message = UpdateMegaStatus(user, groups, template);

				} else {
					message = UpdateStatus(user, groups.First(), template); 
					CreateEvent(groups.First(), user, message);
				}

			}
			catch(Exception e) {
				Logger.Exception(e,"groups {0} user {1}".FormatWith( 
					groups.Select(x => x.Group_ID.ToString()).JoinWith(", "), user.Email ));
				if (e.InnerException != null) {
					if (e.InnerException is FacebookOAuthException) {
						ClearUser(user.UserID);
					}
					else {
						Logger.Exception(e.InnerException,"inner");
					}
				}
			}

		}

		static void ClearUser(int userId) {
	        var unity = Cms.MvcApplication.Container;

    		var userService = unity.Resolve<IRepository2<User>>();
			userService.EnableTracking();
			var user = userService.GetByPK(userId);
			if (user.FbToken == null) return;

			user.FbToken = null;
			userService.SubmitChanges();
    		var mailService = unity.Resolve<IMailService>();
			mailService.Send(MailService.info,new MailAddress(user.Email), 
				H.Anchor("/profile/socialconnect", "Авторизоваться").AbsoluteHref().ToString(),
				"Необходимо авторизоваться в Facebook");
			
			
		}

	    private static string UpdateMegaStatus(User user, List<Group> groups, 
			string template) {
		    var dateText = GetDateText(groups.First());
		    var courses = "Все курсы, которые я веду, на моей персональной страничке {0} ."
				.FormatWith(GetTrainerUrl(groups.First().Teacher_TC));
		    var message = TemplateEngine.GetText(template,
			    new {
				    Date = dateText,
				    Courses = courses
			    });
		    PostStatusUpdate(user, message);
		    return message;
	    }

	    private static string UpdateStatus(User user, Group g, string template) {
		    var dateText = GetDateText(g);
		    var courseUrl = GetCourseUrl(g);
		    var message = TemplateEngine.GetText(template,
			    new {
				    CourseName = StringUtils.AngleBrackets(g.Course.WebName),
				    Date = dateText,
				    CourseUrl = courseUrl
			    });
		    PostStatusUpdate(user, message);
		    return message;
	    }

	    private static void CreateEvent(Group g, User user, string message) {
		    var month = DateTime.Today.AddDays(30);
		    if (g.DateBeg == month && g.TimeBeg.HasValue) {
			    var eventName = g.Course.GetShortName();
				if (eventName.Length > 75) {
				    eventName = "Группа " + (g.MegaGroup_ID ?? g.Group_ID);
				}
			    PostCreateEvent(user, g.DateBeg.Value
				    .Add(g.TimeBeg.Value.TimeOfDay), eventName, message);
		    }
	    }

	    private static string GetCourseUrl(Group g) {
		    return Links.CourseLinkAnchor(null, g.Course).AbsoluteHref().Attribute("href").Value;
	    }

		 private static string GetTrainerUrl(string employeeTC) {
		    return H.Anchor("/trainer/" + employeeTC.ToLower())
				.AbsoluteHref().Attribute("href").Value;
	    }

	    private static string GetDateText(Group g) {
		    return g.DateBeg.Value.Day + " " +
			    MonthUtil.GetName(g.DateBeg.Value.Month, true);
	    }

	    private static void PostCreateEvent(User user, DateTime startDate, 
			string eventName, string message) {
//			new FacebookService(user).PostCreateEvent(startDate,eventName,message);
	    }

	    private static void PostStatusUpdate(User user, string message) {
//			new FacebookService(user).PostStatusUpdate(message);
			new VkontakteService(user.VkToken).PostStatusUpdate(message);
	    }

	    private const string SpecFBToken =
	"CAACkYkfRHZA0BAAXe5yi2AjY17WUA40e2kDYTWDSzZAIwOSv70rLXFTZB1TDJ582AyVNqhpFphTZAZCep9pGz8IX43DktWvenALvKAVdZAMRxIedvaapPFboYWvBbNLTEzWb6481ZBvlpIVdQOEZASiZBFhZAbc2IouyinCUgq2SZCF39fvQWkqN0aeAUGpyX6eGnQZD";


		string GetImgUrl(string text) {
			var m = Regex.Matches(text, "src=[\"'](.*?)[\"']");
			if (m.Count > 0) {
				return m[0].Groups[1].Value;
			}
			return null;
		}

	    public void PostDiscounts() {
	        var unity = Cms.MvcApplication.Container;
    		var simpleValueService = unity.Resolve<SimpleValueService>();
			simpleValueService.EnableTracking();
    		var groupService = unity.Resolve<IGroupService>();
    		var courseService = unity.Resolve<ICourseService>();
		    if (simpleValueService.LastPostDiscountsDate.AddDays(2) <= DateTime.Today) {
			    var treeDays = DateTime.Today.AddDays(3);
			    var groups = groupService.GetPlannedAndNotBegin().Where(x => x.Discount > 0
					&& x.DateBeg > treeDays).Take(10).ToList();
			    var texts = _.List("Курсы со скидкой на " + DateTime.Today.DefaultString());
			    var groupLines = groups.Select(x => {
				    var course = courseService.AllCourseLinks()[x.Course_TC];
				    var link = Links.CourseLinkAnchor(null, course.UrlName, course.WebName);
				    return "{0} {1} — {2}% скидка".FormatWith(x.DateBeg.ShortString(), 
						link.ToFbLink(), x.Discount);
			    });
				texts.AddRange(groupLines);
				texts.Add(H.Anchor(SimplePages.FullUrls.GroupDiscounts, 
					"Все скидки на " + DateTime.Today.DefaultString()).ToFbLink());
			    var text = texts.JoinWith("\n");
			    Logger.Run(() => new FacebookService(SpecFBToken).PostSpecUpdate(text, null), "postdiscounts");
			    simpleValueService.LastPostDiscountsDate = DateTime.Today;
				simpleValueService.SubmitChanges();
		    }
	    }
		public void PostNews() {

	        var unity = Cms.MvcApplication.Container;
    		var newsService = unity.Resolve<IRepository2<News>>();
    		var simpleValueService = unity.Resolve<SimpleValueService>();
			simpleValueService.EnableTracking();
    		var siteObjectService = unity.Resolve<ISiteObjectRelationService>();
			
			var yesterday = DateTime.Today.AddDays(-1);
			var lastPostedNewsId = simpleValueService.LastPostedNewsId;
			var news = newsService
				.GetAll(x => x.IsActive && x.PublishDate == yesterday && x.NewsID > lastPostedNewsId)
				.OrderBy(x => x.NewsID).FirstOrDefault();
			if (news != null) {
				simpleValueService.LastPostedNewsId = news.NewsID;
				simpleValueService.SubmitChanges();

//				var tags =  siteObjectService.GetRelation(typeof (News),
//					_.List<object>(news.NewsID)).Select(x => x.RelationObject.Name)
//					.Where(x => !x.Contains(" ")).Take(3).ToList()
//					.Select(x => "#" + x).ToList().AddFluent("#курсы #обучение").JoinWith(" ");
				var link = CommonConst.SiteRoot + "/news/" + news.NewsID; 
//				var fbmessage = news.ShortDescription + Environment.NewLine + link + Environment.NewLine + tags;
//				var vkmessage = news.ShortDescription + Environment.NewLine + link;
//				var imgUrl = GetImgUrl(news.Description);
//				imgUrl = imgUrl ?? Images.NewsSmall(news).GetSrc();
				Logger.Run(() => new OdnoklassnikiService().PostNews(link), "socialtask");
//				Logger.Run(() => new FacebookService(SpecFBToken).PostSpecUpdate(fbmessage, imgUrl), "socialtask");
				//Logger.Run(() => new VkontakteService(VkontakteService.Token).PostSpecUpdate(vkmessage, imgUrl),
//					"socialtask");
				
			}

		}
	}
}
