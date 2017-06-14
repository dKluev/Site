using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using Bing;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Education;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Mvc.ActionResults;
using Specialist.Web.Const;
using Specialist.Web.Controllers.Center;
using Specialist.Web.Controllers.Message;
using Specialist.Web.Util;
using Specialist.Entities.Catalog.Links.Interfaces;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Web.Helpers;
using SimpleUtils.Extension;

namespace Specialist.Web.Controllers.Common {
	public class RssController : ExtendedController {
		[Dependency]
		public INewsService NewsService { get; set; }

		[Dependency]
		public IRepository<Advice> AdviceService { get; set; }

		[Dependency]
		public IGroupService GroupService { get; set; }


		[Dependency]
		public LectureService LectureService { get; set; }

		[Dependency]
		public IGroupVMService GroupVMService { get; set; }
		[Dependency]
		public IUserService UserService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }

		[Dependency]
		public IRepository<UserMessage> UserMessageService { get; set; }

		[Dependency]
		public IRepository<MessageSection> MessageSectionService { get; set; }

		public string RootUrl {
			get { return CommonConst.SiteRoot; }
		}


		public static bool IsChrome() {
			if (System.Web.HttpContext.Current.Request.UserAgent != null)
				return System.Web.HttpContext.Current.Request.UserAgent.ToLowerInvariant().Contains("chrome");
			return false;
		}

		[OutputCache(Duration = 60*30, VaryByParam = "sectionID;messageID")]
		public ActionResult Messages(int? sectionID, long? messageID) {
			var sectionIds = sectionID.HasValue ? _.List(sectionID.Value) : new List<int>();
			return ForumMessages(messageID, sectionIds);
		}
		[OutputCache(Duration = 60*30, VaryByParam = "none")]
		public ActionResult ForumMessages() {
			var messages = GetAllSectionsMessages(_.List(1,2,3,4,5));
			return GetFeed("Все сообщения форума", messages);
		}

		private ActionResult ForumMessages(long? messageID, List<int> sectionIds) {
			var messages = (sectionIds.Any()
				? UserMessageService.GetAll().Where(um => sectionIds.Contains(um.MessageSectionID.Value))
				: UserMessageService.GetAll().Where(um => um.ParentMessageID == messageID))
				.OrderByDescending(m => m.CreateDate)
				.Take(CommonConst.MessageCount).ToList();
			var title = string.Empty;
			if (sectionIds.Any()) {
				if (sectionIds.Count == 1) {
					var section = MessageSectionService.GetByPK(sectionIds.First());
					title = section.Name;
				}
				else {
					title = "Сообщения форума";
				}
			}
			else {
				var message = UserMessageService.GetByPK(messageID);
				if (message == null)
					title = "Сообщение не найдено";
			}
			return GetFeed(title, messages);
		}

		private List<SyndicationItem> GetItems(IEnumerable<UserMessage> messages) {
			var items = new List<SyndicationItem>();
			foreach (var message in messages) {
				var content = message.Text;
				if (message.ParentMessageID == null)
					content += "<br/>" + H.Anchor(
						RootUrl + Url.Action<RssController>(c =>
							c.Messages(null, message.UserMessageID)), "rss");
				var uri = RootUrl +
					Url.Action<MessageController>(c =>
						c.Details(message.ParentMessageID.HasValue
							? message.ParentMessageID.Value
							: message.UserMessageID, 1));
				var item =
					new SyndicationItem(GetTitle(message),
						SyndicationContent.CreateHtmlContent(content),
						new Uri(uri),
						message.UserMessageID.ToString(),
						message.CreateDate);
				if (message.ParentMessageID == null)
					item.ElementExtensions.Add(new XElement("comments",
						RootUrl + Url.Action<RssController>(c =>
							c.Messages(null, message.UserMessageID))));
				items.Add(item);
			}
			return items;
		}

		[OutputCache(Duration = 60*30, VaryByParam = "sectionID")]
		public ActionResult AllMessages(int sectionID) {
			var sections = _.List(sectionID);
			var messages = GetAllSectionsMessages(sections);
			var section = MessageSectionService.GetByPK(sectionID);
			return GetFeed(section.Name + ": все сообщения", messages);
		}

		private List<UserMessage> GetAllSectionsMessages(List<int> sections) {
			var messages = UserMessageService.GetAll(um => sections.Contains(um.MessageSectionID.Value))
				.Union(UserMessageService.GetAll(um =>
					sections.Contains(um.Parent.MessageSectionID.Value)))
				.OrderByDescending(m => m.CreateDate)
				.Take(CommonConst.MessageCount).ToList();
			return messages;
		}

		[OutputCache(Duration = 60*30, VaryByParam = "none")]
		public ActionResult MessagesNotAnswered() {
			var messages = UserMessageService.GetAll(um => um.ParentMessageID == null
				&& um.MessageSectionID.HasValue 
				&& !um.IsAnswered)
				.OrderByDescending(m => m.CreateDate)
				.Take(50).ToList();
		
			return GetFeed("Не отвеченные сообщения", messages);
		}

		[OutputCache(Duration = 60*5, VaryByParam = "userId")]
		public ActionResult MessagesForUser(int userId) {
			var messages = UserMessageService.GetAll(um => 
				um.CreatorUserID != userId &&
				UserMessageService.GetAll(x => x.CreatorUserID == userId)
				.Select(x => x.ParentMessageID ?? x.UserMessageID)
				.Contains(um.ParentMessageID.Value)
				)
				.OrderByDescending(m => m.CreateDate)
				.Take(CommonConst.MessageCount).ToList();
			return GetFeed("Персональная лента", messages);
		}

		[OutputCache(Duration = 60*5, VaryByParam = "tc")]
		public ActionResult GroupMessages(string tc) {
			var messages = UserMessageService.GetAll(um => 
				um.Parent.Group.Teacher_TC == tc)
				.OrderByDescending(m => m.CreateDate)
				.Take(CommonConst.MessageCount).ToList();
			return GetFeed("Лента преподавателя", messages);
		}

		private ActionResult GetFeed(string title, List<UserMessage> messages) {
			var createDate = messages.Any()
				? messages.First().CreateDate
				: DateTime.Now;
			var feed =
				new SyndicationFeed(title,
					"Новые сообщения",
					Request.Url,
					RootUrl + Request.Url.PathAndQuery,
					createDate);
			feed.Items = GetItems(messages);
			return new RssResult(feed);
		}


		private string GetTitle(UserMessage message) {
			return message.Title ?? message.Parent.GetOrDefault(x => x.Title) ??
				message.CreateDate.ToString("Новое сообщение dd.MM hh:mm");
		}

		[OutputCache(CacheProfile = "Rss")]
		public ActionResult News() {
			var news = NewsService.GetAll().IsActive().OrderByDescending(n => n.PublishDate)
				.Take(CommonConst.NewsCount);
			var feed =
				new SyndicationFeed("Новости www.specialist.ru",
					"Новости www.specialist.ru",
					Request.Url,
					"newsfeed",
					news.First().PublishDate);
			var items = new List<SyndicationItem>();
			foreach (var newsItem in news) {
				var href = RootUrl + Url.Action<SiteNewsController>(c =>
					c.Details(newsItem.NewsID,
						Linguistics.UrlTranslite(newsItem.Title))) + GetRssUtmPart("news");
				var item =
					new SyndicationItem(newsItem.Title,
						newsItem.ShortDescription,
						new Uri(href),
						newsItem.NewsID.ToString(),
						newsItem.PublishDate);
				items.Add(item);
			}

			feed.Items = items;
			return FeedOrChrome(feed);
		}

		[OutputCache(CacheProfile = "Rss")]
		public ActionResult Advices() {
			var advices = AdviceService.GetAll().IsActive()
				.OrderByDescending(n => n.AdviceID)
				.Take(CommonConst.AdviceCount);
			var url = RootUrl +
				Url.Action<RssController>(c => c.Advices());
			var feed =
				new SyndicationFeed("Советы www.specialist.ru",
					"Советы www.specialist.ru",
					new Uri(url),
					"advicesfeed",
					advices.FirstOrDefault()
						.GetOrDefault(x => x.CreateDate));
			var items = new List<SyndicationItem>();
			foreach (var advice in advices) {
				var item =
					new SyndicationItem(advice.Name,
						StringUtils.GetShortText(advice.Description),
						new Uri(RootUrl + Url.Action<CenterController>(c =>
							c.Advice(advice.AdviceID,
								Linguistics.UrlTranslite(advice.Name)))),
						advice.AdviceID.ToString(),
						advice.CreateDate);
				items.Add(item);
			}

			feed.Items = items;
			return FeedOrChrome(feed);
		}


		[OutputCache(CacheProfile = "Rss")]
		public ActionResult HotGroups() {
			var groups = GroupVMService.GetAllForMain();
			var url = RootUrl +
				Url.Action<RssController>(c => c.HotGroups());
			var feed =
				new SyndicationFeed("Курсы со скидками www.specialist.ru",
					"Курсы со скидками www.specialist.ru",
					new Uri(url),
					"hotgroupfeed",
					DateTime.Now);
			var items = new List<SyndicationItem>();
			foreach (var group in groups) {
				var item =
					new SyndicationItem(group.Course.WebName,
						group.DateBeg.DefaultString() +
							" скидка " + group.Discount + "%",
						new Uri(RootUrl + Url.Action<CourseController>(
							c => c.Details(group.Course.UrlName))),
						group.Group_ID.ToString(),
						DateTimeOffset.Now);
				items.Add(item);
			}

			feed.Items = items;
			return FeedOrChrome(feed);
		}


		
		[OutputCache(CacheProfile = "Rss")]
		public ActionResult MsProjectGroups() {
			return GetGroups(CourseTC.MsProject, false);
		}

		[OutputCache(CacheProfile = "Rss")]
		public ActionResult PmGroups() {
			return GetGroups(CourseTC.PM, false);
		}

		[OutputCache(Duration = 60*60)]
		public ActionResult CourseGroups(string tc) {
			if (tc.IsEmpty()) return null;
			
			var courseTC = tc.ToUpper();
			var course = CourseService.GetAllCourseNames().GetValueOrDefault(courseTC);
			if (course == null) return null;
			var courseName = course.WebName;
			var groups = GroupService.GetGroupsForCourses(_.List(courseTC))
				.OrderBy(x => x.DateBeg).Take(10).ToList();
		
			var url = RootUrl +
				Url.Action<RssController>(c => c.CourseGroups(courseTC));
			var title = "Группы по курсу " + courseName;
			var feed =
				new SyndicationFeed(title,
					title,
					new Uri(url),
					"coursegroups" + courseTC,
					DateTime.Now);
			var items = new List<SyndicationItem>();
			var compaign = "coursegroups";
			foreach (var group in groups) {
				var href = Html.CourseLinkAnchor(group.Course).AbsoluteHref().GetHref()
							+ GetRssUtmPart(compaign);
				var item =
					new SyndicationItem(group.Course.WebName,
						"Группа " + group.DateInterval,
						new Uri(href),
						group.Group_ID.ToString(),
						DateTimeOffset.Now);
				items.Add(item);
			}

			feed.Items = items;
			return FeedOrChrome(feed);
		}

		private static string GetRssUtmPart(string compaign) {
			return StringUtils.GetUtmPart("site", "rss", compaign);
		}

		[OutputCache(CacheProfile = "Rss")]
		public ActionResult PmHotGroups() {
			return GetGroups(CourseTC.PM, true);
		}


		private ActionResult GetGroups(List<string> courseTCs, bool hotOnly) {
			var groups = GroupService.GetGroupsForCourses(courseTCs)
				.OrderBy(x => x.DateBeg).Take(10).ToList();
			if (hotOnly)
				groups = groups.Where(x => x.Discount.HasValue).ToList();
			var url = RootUrl +
				Url.Action<RssController>(c => c.HotGroups());
			var feed =
				new SyndicationFeed("Курсы со скидками www.specialist.ru",
					"Курсы со скидками www.specialist.ru",
					new Uri(url),
					"hotgroupfeed",
					DateTime.Now);
			var items = new List<SyndicationItem>();
			foreach (var group in groups) {
				var content = GetContent(@group);
				var item =
					new SyndicationItem(group.Course.WebName +
						(group.Discount.HasValue
							? " скидка " + group.Discount + "%" : ""),
						content,
						new Uri(RootUrl + Url.Action<CourseController>(
							c => c.Details(group.Course.UrlName))),
						group.Group_ID.ToString(),
						DateTimeOffset.Now);
				items.Add(item);
			}

			feed.Items = items;
			return FeedOrChrome(feed);
		}

		private string GetContent(Group @group) {
			var course = group.Course;
			var location = 
				_.List(StringUtils.AngleBrackets(
				Url.ComplexLinkAnchor(group.Complex).AbsoluteHref().ToString()),
				group.Complex.Address, Images.Common("metro.gif").ToString(),
				group.Complex.Metro).JoinWith(" ");
			var content = H.div[
				Images.Main("logo-small.gif").Style("float:left;padding:5px;"),
				H.b[course.GetName()],
				H.br,
				StringUtils.RemoveTags(course.Description),
				H.br,
				H.b["По окончании курса Вы будете уметь:"],
				H.br,
				course.OnComplete,
				H.br,
				H.b["Продолжительность курса - {0} ак. ч."
					.FormatWith( course.BaseHours.ToIntString())],
				H.br,
				H.b["Дата начала курса: "], group.DateBeg.DefaultString(),
				H.br,
				H.b["Место проведения: "], location,
				H.br,
				H.b["График занятий: "], group.DaySequence + " ",
				group.DayShift.GetOrDefault(x => x.Name) + " " + group.TimeInterval,
				H.br,
				H.b["Ссылка на курс: "],
				H.Anchor("/course/" + course.UrlName, course.GetName()).AbsoluteHref()
				].ToString();
			return content;
		}

	/*	[OutputCache(CacheProfile = "Rss")]
		public ActionResult Sql() {
			var courseTCs = CourseService.GetCourseTCListFor(typeof (Product), new object[] {EntityIds.Products.SqlServer});
			var groups = 
				GetGroups(courseTCs).OrderBy(g => g.DateBeg).Take(5);
			var url = RootUrl +
				Url.Action<RssController>(c => c.Sql());
			var feed =
				new SyndicationFeed("Курсы SQL",
					"Ближайщие курсы по SQL",
					new Uri(url),
					"sqlgroupsfeed",
					DateTime.Now);
			XNamespace ns = "http://www.specialist.ru/ns/rss/courses-start-info";
			SetNamespace(feed, "spec",
				ns);
			var src = "?src=sql";
			var items = new List<SyndicationItem>();
			foreach (var group in groups) {
				var item =
					new SyndicationItem(group.Course.WebName,
						"Дата начала: {0}. Преподаватель: {1}. Город: {2}."
							.FormatWith(group.DateBeg.DefaultString(),
								group.Teacher.GetOrDefault(t => t.FullName),
								group.BranchOffice.City.Name),
						new Uri(RootUrl + Url.Action<CourseController>(
							c => c.Details(group.Course.UrlName)) + src),
						group.Group_ID.ToString(),
						DateTimeOffset.Now);
				item.ElementExtensions.Add(new XElement(ns + "teacher",
					H.Anchor(RootUrl + Url.Action<EmployeeController>(c =>
						c.AboutTrainer(group.Teacher_TC.ToLower(), null, null)) + src,
						group.Teacher.FullName)));
				item.ElementExtensions.Add(new XElement(ns + "courseDate",
					group.DateBeg.Value.DefaultString()));
				items.Add(item);
			}

			feed.Items = items;
			return FeedOrChrome(feed);
		}*/

	/*	private Dictionary<int, int> statuses = new Dictionary<int, int> {
			{397,1},
			{376,2},
			{196,3},
			{256,4},
			{397,1},
		};
			*/

		[OutputCache(CacheProfile = "Rss")]
		public ActionResult Sql() {
			var courseTCs = CourseTC.SqlRss;

			var count = 5;
			var groups = GetGroups(courseTCs.Shuffle()).OrderBy(x => x.IsIntraExtramural)
				.Take(count).OrderBy(g => g.DateBeg).ToList();

			var url = RootUrl + Url.Action<RssController>(c => c.Sql());
			var name = "SQL";
			var feed = GroupsToFeed(url, groups, name, "sqlru");

			return FeedOrChrome(feed);
		}

		[OutputCache(CacheProfile = "Rss")]
		public ActionResult SqlOracle() {
			var courseTCs = CourseTC.SqlOracleRss;

			var count = 2;
			var groups = GetGroups(courseTCs.Shuffle())
				.OrderBy(g => g.DateBeg).Take(count).ToList();

			var url = RootUrl + Url.Action<RssController>(c => c.SqlOracle());
			var name = "Oracle";

			var feed = GroupsToFeed(url, groups, name, "oracle");
			var links =
				_.List(LinkToFeed("/vendor/oracle?utm_source=sqlru&utm_medium=forum&utm_campaign=oracle ",
					"Курсы Oracle! Лучший учебный центр России!"), 
					LinkToFeed("/vendor/oracle?utm_source=sqlru&utm_medium=forum&utm_campaign=oracle ", 
					"ОЧЕНЬ привлекательные цены на курсы Oracle – от 26 тыс.руб.!!!"));
			feed.Items = links.Concat(feed.Items);


			return FeedOrChrome(feed);
		}



		[OutputCache(CacheProfile = "Rss")]
		public ActionResult Oracle() {
			var courseTCs = CourseTC.OracleRss;

			var count = 5;
			var groups = GetGroups(courseTCs.Shuffle())
				.Take(count).OrderBy(g => g.DateBeg).ToList();

			var url = RootUrl + Url.Action<RssController>(c => c.Sql());
			var name = "Oracle";
			var feed = GroupsToFeed(url, groups, name, "sqlru");

			return FeedOrChrome(feed);
		}

		SyndicationItem LinkToFeed(string url, string name) {
			
			return new SyndicationItem(name,
						name,
						new Uri(RootUrl + url));
		}
		private SyndicationFeed GroupsToFeed(string url, List<Group> groups, string name, string utm) {
			var feed =
				new SyndicationFeed("Учебный Центр «Специалист» при МГТУ им. Н.Э. Баумана",
					"Ближайщие курсы " + name,
					new Uri(url),
					name.ToLower() + "groupsfeed",
					DateTime.Now);
			XNamespace ns = "http://www.specialist.ru/ns/rss/courses-start-info";
			SetNamespace(feed, "spec", ns);
			var src = "?utm_source=sqlru&utm_medium=forum&utm_campaign=" + utm;
			var items = new List<SyndicationItem>();
			foreach (var group in groups) {
				var status = string.Empty;
				var desc = "Начало: {0}. Преподаватель: {1}{2}.".FormatWith(
					@group.DateBeg.DefaultString(),
					@group.Teacher.FullName,
					status);
				if (@group.Discount > 0) {
					desc += "Скидка: {0}%. ".FormatWith(@group.Discount);
				}
				var groupUrl = Url.Action<CourseController>(c => c.Details(@group.Course.UrlName));
				var item =
					new SyndicationItem(@group.Title,
						desc,
						new Uri(RootUrl + groupUrl + src));
				item.ElementExtensions.Add(new XElement(ns + "teacher",
					H.Anchor(RootUrl + Url.Action<EmployeeController>(c =>
						c.AboutTrainer(@group.Teacher_TC.ToLower(), null, null)) + src,
						@group.Teacher.FullName)));
				item.ElementExtensions.Add(new XElement(ns + "courseDate",
					@group.DateBeg.Value.DefaultString()));
				items.Add(item);
			}
			feed.Items = items;
			return feed;
		}

		private IEnumerable<Group> GetGroups(IEnumerable<string> courseTCs) {
			return courseTCs 
				.SelectMany(tc => GroupService.GetGroupsForCourse(tc))
				.Where(g => g.DateBeg.HasValue && g.Teacher != null)
				.Distinct(g => g.Course_TC);
		}


		public static void SetNamespace(SyndicationFeed feed, string prefix, XNamespace nsUri) {
			feed.AttributeExtensions.Add(new XmlQualifiedName(prefix,
				"http://www.w3.org/2000/xmlns/"), nsUri.ToString());
		}


	    public ActionResult TimeSheetCalendar(string tc) {
			var dateBegin = DateUtils.FirstMonthDay(DateTime.Today);
			var dateEnd = DateUtils.LastMonthDay(dateBegin.AddMonths(1));
			var lectures = LectureService.GetLmsLectures(dateBegin, dateEnd, tc.ToUpper());
		    var data = lectures.Select(x => new CalData(x.LectureDateBeg, x.LectureDateEnd,
			    x.ClassRoom_TC, x.Course_TC)).ToList();
		    var result = EntityUtils.GetCalendar(data);

			return Content(result,"text/calendar");

	    }


		public ActionResult FeedOrChrome(SyndicationFeed feed) {
			if (IsChrome())
				return View(ViewNames.Chrome);
			return new RssResult(feed);
		}
	}
}