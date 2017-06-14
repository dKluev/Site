using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog.Extension;
using Specialist.Services.Common.Extension;
using Specialist.Services.Common.Utils;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Utils;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Group = Specialist.Entities.Context.Group;
using Tuple = SimpleUtils.Common.Tuple;

namespace Specialist.Web.Root.Common.MailList {
	public class MailListService : H {
		[Microsoft.Practices.Unity.Dependency]
		public IRepository<MailTemplate> MailTemplateService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public IRepository<News> NewsService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public Repository<Group> GroupService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public Repository<User> UserService { get; set; }

		[Microsoft.Practices.Unity.Dependency]
		public IGroupVMService GroupVMService { get; set; }

		private MailAddress maillist = new MailAddress("maillist@specialist.ru",
			"Öåíòð Ñïåöèàëèñò", StringUtils.Encoding1251);

		private const string NewsPattern = @"\[News=(.*?)\]";
		private const string GroupsPattern = @"\[Groups=?(.*?)\]";
		private const string BestGroupsPattern = @"\[BestGroups\]";

		public SimpleUtils.Common.Tuple<MailTemplate, MailListType> GetTemplate() {
			var template =
				MailTemplateService.GetAll().BySysName(MailTemplates.MailList);
			ReplaceNewsBlocks(template);
			var dataLoadOptions = new DataLoadOptions();
			dataLoadOptions.LoadWith<Group>(g => g.BranchOffice);
			dataLoadOptions.LoadWith<Group>(g => g.Course);
			dataLoadOptions.LoadWith<Group>(g => g.DayShift);
			dataLoadOptions.LoadWith<BranchOffice>(g => g.City);
			GroupService.Context.LoadOptions = dataLoadOptions;
			ReplaceGroupBlocks(template);
			var bestGroupsBlock = GetGroupsBlock(true,null);
			template.Description = Regex.Replace(template.Description, BestGroupsPattern,
				bestGroupsBlock);
			var mailListType = GetType(template);

			return Tuple.New(template, mailListType);
		}

	

		private MailListType GetType(MailTemplate template) {
			foreach (var type in Enum.GetValues(typeof (MailListType))) {
				var typeTag = "[" + type + "]";
				if (template.Description.Contains(typeTag)) {
					template.Description = template.Description.Remove(typeTag);
					return (MailListType) type;
				}
			}
			return MailListType.None;
		}

		private List<string> courseList = _.List(
"ÝÊÑÅËÜ1",
"ÒÁÓÕ-Ê",
"ÈÖÍÄ1",
"ÝÊÑÅËÜ2",
"ÝÉ×ÒÌË-Á",
"ÔØ1-Ç",
"1Ñ8Á1",
"3ÄÌ1-Ê",
"ÀÊÀÄ20131",
"ÏÏ-Ä",
"ÑÈ",
"Ì6420Â",
"ÁÊÏ24",
"ÊÀÄÐ-Â",
"ÈÒÏÌ-À",
"Ò-ÒÎÐ48",
"ÂÌÀÐÊ-Â",
"ÎÐÈÑ-À",
"ÓÏÐ-Ä",
"ÑÄÑ",
"ÂÎÐÄ1-À"
			);

		IQueryable<Group> GetPlannedGroups() {
			return GroupService.GetAll(x => x.DateBeg > DateTime.Today.AddDays(1)
				&& x.DateBeg <= DateTime.Today.AddDays(30))
				.PlannedAndNotBegin();
		} 
		
		public List<Group> GetForUnsplitCourseTCList(string courseTCs) {
			var groupIds = new HashSet<decimal>(StringUtils.IntListSplit(courseTCs)
				.Select(x => (decimal)x)).ToList();
			var groups = GetPlannedGroups();
			if (groupIds.Any())
				return groups.Where(x => groupIds.Contains(x.Group_ID)).ToList();
			var courseTCList = StringUtils.SafeSplit(courseTCs);
			return GetPlannedGroups()
					.Where(x => courseTCList.Contains(x.Course_TC))
					.ToList();
		} 

		private string GetGroupsBlock(bool isBest, string courseTCs) {
			List<Group> groups;
			List<Grouping<Section, Group>> sectionGroups;
			if (!courseTCs.IsEmpty()) {
				groups = GetForUnsplitCourseTCList(courseTCs);
				sectionGroups = _.List(Grouping.New(new Section(), groups));
					
			}else if (isBest) {
				groups = GetPlannedGroups()
					.Where(x => courseList.Contains(x.Course_TC) && x.Discount.HasValue)
					.ToList();
				sectionGroups = _.List(Grouping.New(new Section(), groups));
				
			} else {
			groups = GroupService.GetAll(x => x.DateBeg > DateTime.Today.AddDays(1)
				&& x.DateBeg <= DateTime.Today.AddDays(30) && x.Discount.HasValue)
			                     .PlannedAndNotBegin().ToList();
			sectionGroups = GroupVMService.GetSectionGroups(groups);
				
			}

			return l(
				sectionGroups.OrderBy(x => x.Key.WebSortOrder)
					.Select(x => l(h3[x.Key.Name],
						ul.Style("list-style-type:square;font-size:13px;")[
							GroupsBlock(x)]
						).ToString()).JoinWith("")).ToString();
		}

		private static IEnumerable<TagLi> GroupsBlock(Grouping<Section, Group> groups) {
			var courses = groups.GroupBy(x => x.Course_TC).ToList();
			return courses.Select(c => {
				var course = c.First().Course;
				var maxDiscount = c.Max(x => x.Discount);
				return li.Style("margin:6px 0;")[
					span[Anchor("/course/" + course.UrlName,
						course.GetName()).AbsoluteHref(), "<br/>",
						span.Style("color: rgb(172, 61, 75); font-weight: bold;")[
							c.OrderBy(x => x.DateBeg)
							.Select(x => x.DateBeg.NotNullString("dd.MM")).Distinct().JoinWith(", ")], 
						maxDiscount.IfNotNull(d =>
							span.Style("color: rgb(172, 61, 75); font-weight: bold;")[
								(c.Count() == 1 ? " — ñêèäêà {0}%!" :	" — ñêèäêè äî {0}%!").FormatWith(d)])
						
						]];
			});
		}


		private void ReplaceGroupBlocks(MailTemplate template) {
			foreach (Match match in Regex.Matches(template.Description, GroupsPattern)) {
				var block = GetGroupsBlock(false, match.Groups[1].Value);

				template.Description = template.Description.Replace(match.Groups[0].Value,
					block);

			}
		}

		private void ReplaceNewsBlocks(MailTemplate template) {
			foreach (Match match in Regex.Matches(template.Description, NewsPattern)) {
				var newsIds = match.Groups[1].Value
					.Split(',').Select(s => StringUtils.ParseInt(s).GetValueOrDefault())
					.Where(x => x > 0).ToList();
				var news = NewsService.GetAll(x => newsIds.Contains(x.NewsID)).ToList();
				news = news.OrderBy(x => newsIds.IndexOf(x.NewsID)).ToList();

				var line = tr[td[hr
					.Style("height:1px;border-width:0;color:#999;background-color:#999")]];
				var block =  l(
					ul.Style("list-style-type:square")[
						news.Select(n => li.Style("margin:6px 0; padding:0;")[
							NewsAnchor(n)])
						],
					table.Cellpadding("10px")[news.Select(x =>
						l(line,
							tr[
								td[h3.Style("color:rgb(172, 61, 75);")[
									NewsAnchor(x).Style("color:rgb(172, 61, 75);")],
									p[NewsAnchor(x,Images.NewsSmall(x)
										.FluentUpdate(y => y.SetAttributeValue("hspace", 10))
										.FluentUpdate(y => y.SetAttributeValue("align", "left"))
										.Size(100, 100).ToString()),
										x.ShortDescription, " ",
										i[NewsAnchor(x, "Ïîäðîáíåå")]
										]
									]
								])),
						line
						]
					).ToString();
				template.Description = template.Description.Replace(match.Groups[0].Value,
					block);

			}
		}

		private TagA NewsAnchor(News n, string title = null) {
			title = title ?? n.Title;
			return Anchor("/news/" + n.NewsID +
				"/" + Linguistics.UrlTranslite(n.Title), title).AbsoluteHref();
		}

		public int? CurrentUser {
			[MethodImpl(MethodImplOptions.Synchronized)]
			get {
				var file = GetMailListFile();
				if (!File.Exists(file))
					return null;
				return StringUtils.ParseInt(File.ReadAllText(file));
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set {
				var file = GetMailListFile();
				if (value == null && File.Exists(file)) {
					File.Delete(file);
					return;
				}
				var directoryName = Path.GetDirectoryName(file);
				if (!Directory.Exists(directoryName))
					Directory.CreateDirectory(directoryName);
				File.WriteAllText(file, value.ToString());
			}
		}


		private string GetMailListFile() {
			//			return "temp/maillist.txt";
			return HostingEnvironment.MapPath("~/temp/maillist.txt");
		}

		private static string GetIsStoppedFile() {
			//			return "temp/maillist.txt";
			return HostingEnvironment.MapPath("~/temp/isstopped.txt");
		}

		public DateTime? GetLastSendDate() {
			if (File.Exists(GetMailListFile()))
				return File.GetLastWriteTime(GetMailListFile());
			return null;
		}

		public static bool IsStopped {
			[MethodImpl(MethodImplOptions.Synchronized)]
			get { return File.Exists(GetIsStoppedFile()); }
			[MethodImpl(MethodImplOptions.Synchronized)]
			set {
				if(File.Exists(GetIsStoppedFile()) == value)
					return;
				if(value) {
					File.WriteAllText(GetIsStoppedFile(), "isstopped");
				}else {
					File.Delete(GetIsStoppedFile());
				}
			}
		}

		public void StopMailList() {
			IsStopped = true;
		}

		public double? GetSendedPercent() {
			if (CurrentUser == null)
				return null;
			var template =
				MailTemplateService.GetAll().BySysName(MailTemplates.MailList);
			var type = GetType(template);
			var queryable = AddFilter(UserService.GetAll(), type);
			var allCount = queryable.Count();
			var sendedCount = queryable.Where(x => x.UserID <= CurrentUser.GetValueOrDefault()).Count();
			return (sendedCount*100.0)/allCount;
		}

		public void SendForAll() {
			IsStopped = false;
			if (CurrentUser == null)
				CurrentUser = 0;
			var mailTemplate = GetTemplate();
			Task.Factory.StartNew(() => {
				var count = 0;
				while (true) {
					if (IsStopped)
						return;
					if (count++ > 100000)
						return;
					var currentUserId = CurrentUser.GetValueOrDefault();
					using (var context = new PassportDataContext()) {
						var userQuerybale = context.Users.AsQueryable();
						var mailListType = mailTemplate.V2;
						userQuerybale = AddFilter(userQuerybale, mailListType);
						var user = userQuerybale
							.Select(x => new {
								x.UserID,
								x.Email,
								FullName = x.FirstName + " " + x.SecondName
							})
							.OrderBy(x => x.UserID)
							.Where(x => x.UserID > currentUserId).FirstOrDefault();
						if (user == null) {
							CurrentUser = null;
							return;
						}

						using (var client = new SmtpClient()) {
								var time = DateTime.Now;
								Send(client, mailTemplate.V1, user.Email, user.FullName);
								CurrentUser = user.UserID;
								var pause = 3000 - (int) (DateTime.Now - time).TotalMilliseconds;
								if (pause > 0) {
									Thread.Sleep(pause);
								}
						}
					}
				}
			}, TaskCreationOptions.LongRunning).ContinueWith(task => {
				if (task.Exception != null) {
					IsStopped = true;
					Logger.Exception(task.Exception, "maillist " + task.Exception);
				}
			});
		}

		private void Unsubscribe(string email) {
			using (var context = new PassportDataContext()) {
				foreach (var user in context.Users.Where(u => u.Email == email)) {
					user.MailListSubscribed = false;
					user.MailListTypes = (byte) MailListType.None;
				}
				context.SubmitChanges();
			}
		}

		private IQueryable<User> AddFilter(IQueryable<User> userQuerybale, MailListType mailListType) {
			if (mailListType == MailListType.None)
				return userQuerybale.Where(x => x.MailListSubscribed);
			var type = (byte) mailListType;
			return userQuerybale.Where(x => (x.MailListTypes & type) == type);
		}

		private const string MailMaster = "<html><body>{0}</body></thml>";

		public void Send(SmtpClient client, MailTemplate template, string mail, string userName = null) {
			try {
				var body = TemplateEngine.GetText(template.Description, new {userName});
				using (var message =
					new MailMessage(maillist, new MailAddress(mail)) {
						IsBodyHtml = true,
						BodyEncoding = Encoding.UTF8,
						Subject = template.Name,
						Body = MailMaster.FormatWith(body),
					}) {
					client.Send(message);
				}
			}
			catch (FormatException e) {
				Unsubscribe(mail);
				Logger.Exception(e, "mail list format exception " + mail);
			}
			catch (Exception e) {
				Logger.Exception(e, this + " " + mail);
			}
		}


		private List<decimal> groupIds = _.List<decimal>(
			140236,
135918,
141202,
138289,
122793,
131379,
111675,
122641,
140082,
123345,
122435,
117484,
123209,
123477,
137895,
123432,
140650,
140638,
138589,
140637,
141726,
128641,
129303,
135086,
129141,
122566,
136379,
115974,
115475,
130837,
121168,
123365,
123190,
118792,
119738,
141130,
115936,
128747

			);
	}
}
