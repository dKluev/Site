using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Extension;
using SimpleUtils.FluentHtml.Tags;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Education.ViewModel;
using Specialist.Entities.Passport;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Const;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Center;
using Htmls = Specialist.Web.Common.Html.Htmls;
using SpecialistTest.Web.Core.Mvc.Extensions;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Common.Const;
using Specialist.Entities.Profile;
using Specialist.Services.Common;
using Specialist.Web.Common.Mvc;
using Specialist.Web.Root.Catalog;

namespace Specialist.Web.Helpers {
	public partial class SiteHtmls : H {
		private HtmlHelper Html { get; set; }

		public UrlHelper Url { get; set; }


		public static string CreditBlock(decimal price) {
			var txt = Htmls.HtmlBlock(HtmlBlocks.CreditCoursePage);
			var paymentTxt = CreditMinPayment(price);
			return txt.GetOrDefault(x => x.Replace("[Payment]", paymentTxt));
		}

		public static string CreditMinPayment(decimal price) {
			var payment = 0.053366m*Math.Max(price, AlphaBankGenerator.MinPrice);
			return "От {0} руб./месяц".FormatWith(payment.MoneyString());
		}
		public static TagDiv VimeoPlayers(List<string> videoIds, string pwd) {
			return H.div[
				H.p[H.b["Пароль: "], pwd],
				videoIds.Select(x => H.div[Htmls2.Vimeo(x)].Style("padding-bottom:10px;"))];
		}
		public static TagDiv Badges(BangesVM badges) {
			return H.div[H.h3.Class("profile_h3")["Мои награды"],
				H.ul[badges.GetBadges().Select(x => {
					var img = (object) H.Img(x.Item1.Url).Title(x.Item2);
					if (!x.Item5.IsEmpty()) {
						img = H.Anchor(x.Item5, img);
					}
					return
						H.li[x.Item3 == null ? img : H.Anchor(x.Item3, img).Class("ga-click fancy-box").Rel(x.Item4)].Style(
							"float:left; margin:0 5px 10px;");
				})]
					.Style("overflow:hidden; list-style:none; padding-left:0;"),
				H.Div("clear")
				].Style("float:left; margin-left:50px;");
		}

		public static TagUl BadgesNew(BangesVM badges) {
			return 
				H.ul.Class("sign")[badges.GetBadges().Select(x => {
					var img = (object) H.Img(x.Item1.Url).Height(86).Title(x.Item2);
					if (!x.Item5.IsEmpty()) {
						img = H.Anchor(x.Item5, img);
					}
					return
						H.li[x.Item3 == null ? img : H.Anchor(x.Item3, img).Class("ga-click fancy-box").Rel(x.Item4)];
				})];
		}

		public static string Banners(List<Banner> banners) {
			var buttons = banners.Count() > 1
				? H.Ul(banners.Select((x,i) => H.span[i + 1].Class(i == 0 ? "active-button" : "")
					)).Id("banner-rotate")
					: null;
			var content = H.div[H.Div("useful_info_container")[
					
				banners.Select((banner,i) => H.Anchor(banner.TargetUrl,
				Images.Banner(banner.Image, banner.Name)
				.Attr(new {
					title = banner.Name
				}).Class("useful_info").Style("margin-left:20px;").ToString()
				).Class("useful-info-" + (i + 1)).Style(i == 0 ? "" : "display:none;")
			)],buttons, H.Div("clear")];
			return content.ToString();
		}

		public string MobileComplexes(IEnumerable<Complex> complexes) {
    		return MHtmls.MainList(complexes.Select(x => Url.Locations().Complex(x.UrlName,
    			H.l(x.Name, H.span.Class("addrfilials")[x.Address]))));
    	}
		public string MobileActions(IEnumerable<MarketingAction> actions) {
    		return MHtmls.MainList(actions.Select(x => Url.Center().MarketingAction(x.UrlName,
    			H.l(span.Class("h2_inline")[x.Name], 
				H.span.Class("p_inline")[H.Raw(StringUtils.GetFirstParagraph(x.Description))]))));
    	}
		public string MobileNews(IEnumerable<News> news) {
    		return MHtmls.MainList(news.Select(x => Url.SiteNews().Details(x.NewsID,null,
    			H.l(span.Class("h2_inline")[x.Name], 
				H.span.Class("p_inline")[x.ShortDescription]))));
    	}
		public string MobileSections(IEnumerable<Section> sections) {
    		return MHtmls.MainList(sections.Select(x => Url.SectionLink(x)));
    	}
		public string MobileCourses(IEnumerable<ICourseLink> courses) {
    		return MHtmls.MainList(courses
				.Select(x => Html.CourseLinkAnchor(x.UrlName, x.GetName())));
    	}
		public TagDiv MobileGroups(List<Group> groups) {
			if(!groups.Any())
				return null;
			return H.div.Id("groups")[
				H.h3["Ближайшие группы:"], groups.Select(g => H.Div("group")[
					Url.Link<CourseController>(c => c.Group(g.Group_ID),
						l(span.Class("date")[g.DateBeg.DefaultString()],
							span.Class("groupName")[g.Course.GetName()], 
							Raw(Htmls2.Discount(g, true))))])
				];
		}
		public TagDiv MobileCourseGroups(List<Group> groups) {
			if(!groups.Any())
				return null;
			return H.div.Id("groups")[
				H.h3["Ближайшие группы:"], groups.Select(g => H.Div("group")[
					Url.Link<CourseController>(c => c.Group(g.Group_ID),
						l(span.Class("coursedate")[g.DateInterval],
							span.Class("coursetime")[g.TimeInterval + ";" + g.DaySequence + 
								(g.IsOpenLearning ? ";Открытое Обучение" : "")], 
							span.Class("courseplace")[
							"УК " + StringUtils.AngleBrackets( 
							g.Complex.GetOrDefault(x => x.Name))], 
							Raw(Htmls2.Discount(g, true))))])
				];
		}

		public TagDiv MobileGroupLectures(List<GroupVM.LectureVM> lectures) {
			if(!lectures.Any())
				return null;
			return H.div.Id("groups")[
				lectures.Select(g => H.Div("group")[
					Url.Locations().Complex(g.Lecture.Group.Complex.UrlName,
						l(span.Class("coursedate")[g.DateTimeInterval],
							span.Class("courseplace")[
							"УК " + StringUtils.AngleBrackets( g.Lecture.Group.Complex.Name)] 
							))])];

		}
		


		public SiteHtmls(HtmlHelper html) {
			Html = html;
			Url = new UrlHelper(html.ViewContext.RequestContext);
		}

		public object Response(Response response) {
			if (response == null)
				return string.Empty;
			Func<string, object, TagP> line =
				(name, value) => p[strong[name], br, value];

			return l(
				Div("opinion_text")[response.Description],
				response.Authors.IfNotNull(x => line("Слушатель:", x)),
				response.Course.IfNotNull(x => line("Курс:",
					Html.CourseLinkShortName(x))),
				response.Employee.If(x => x != null && x.SiteVisible, 
				x => line(x.IsTrainer ? "Преподаватель:" : "Менеджер:", Html.EmployeeEntityLink(x))));
		}

		public TagDiv BestTestUser(List<User> users) {
			if(!users.Any())
				return null;
			return div[h3["Лучшие за " + MonthUtil.GetName(MonthUtil.GetPrevious())], Ul(users.Select(x => 
				Html.Encode(x.FullName)))];
		}


		public TagDiv ThreeColumns(IEnumerable<IGrouping<string, IEntityCommonInfo>>
			entities) {
			var list = entities.ToList().GetColumns(3, 3);
			return ThreeColumns(list);
		}

		public TagDiv ThreeColumns(
			IEnumerable<IEnumerable<IGrouping<string, IEntityCommonInfo>>> list) {
			return div[
				list.Select(x => Div("tab_3column")[
					x.Select(y => Div("link_block")[h3[y.Key],
						Ul(y.Select(z => {
							var mainPage = z.As<IForMainPage>()
								.GetOrDefault(k => k.ForMainPage);
							var link = Html.GetLinkWithoutCoursesPrefixFor(z);
							return mainPage ? strong[link].ToString() : link;
						}) )])])];
		}

		public TagDiv CoursesFour(List<Group> groups) {
			return Div("courses_4")[
				table.Cellpadding("5px")[
				groups.GetRows(2).Select(CourseFourTr)
				 ]];

		}

		private TagTr CourseFourTr(IEnumerable<Group> groups) {
			if(groups.IsEmpty())
				return null;
			return tr[
				groups.Select(g => {
					var courseLink = Html.CourseLinkAnchor(g.Course);
					courseLink.Href(courseLink.GetHref() + "?src=anons");
					return td.Style("width:50%").Valign("top")[
						Images.Course(g.Course.UrlName)
							.Width(100).FloatLeft().ToString(),
						courseLink,
						br,
						span.Class("date")[g.DateBeg.DefaultString() + " " + Htmls2.Discount(g, true)],
						p.Style("font-size: 11px;")[
							g.Course.AnnounceDescription]
						];
				})
				];
		}

		public TagDiv UserWorkFour(IEnumerable<UserWork> works) {
			return Div("work_4")[
				works.Select(w => Div("next_work")[
					Div("next_work_in")[
						Html.UserWorkLink(w),
						Graduate(w.FullName)]])
				];
		}

		public TagDiv OpinionTwo(IEnumerable<Response> responses) {
			return Div("opinions_2")[
				responses.Select(r => Div("next_opinion")[
					Div("next_opinion_in")[
						Div("text")[r.Description],
						Graduate(r.Authors)]])
				];
		}

		public static TagDiv Graduate(string name) {
			return div.Style("text-align:left")[span[strong.Class("text_red")["Слушатель:"], br,
				name]];
		}
		
		public static string Announces() {
			return h2.Class("h2_block announces-header").Style("display:none;")["Анонсы ближайших курсов"] +
       		Div("nearest-courses")[Images.Common("indicator.gif").ToString()].ToString()
			+br + br; 
		}


		public string NearestGroups(List<Group> groups, int titleType = 0, bool hideDiscount = false) {
			if(!groups.Any())
				return null;
			var title = GroupTitleType.Get(titleType);

			return br + (titleType >= 0 ? h3[title].ToString() : null) +
				Html.Partial(Views.Shared.Education.NearestGroupList,
					new NearestGroupsVM(groups) {
						HideDiscount = hideDiscount
					});
		}

		public TagUl Professions(List<Profession> professions) {
			if (!professions.Any())
				return null;
			return Ul(professions.Select(x => l(Html.ProfessionLink(x), 
				span["з/п от {0} руб.".FormatWith(strong[x.Salary.MoneyString()])].Class("zp")).ToString()))
				.Class("square_blue");
		}

		public TagDiv UserWorks(IEnumerable<UserWork> works) {
			return div[works.Select(x => div[
				b["Автор работы: "].Class("fio"), 
				span[x.FullName].Style("color:#0C4D84;"),
				x.Trainer != null && x.Trainer.SiteVisible ? 
				span[br,b["Преподаватель: "], Html.EmployeeLink(x.Trainer)] : null,
				x.Description
				])];
		}

		public TagDiv SuccessStories(List<SuccessStory> list) {
			return div[list.Select(x => {
				var author = x.Author.IsEmpty() ? null : 
					span[strong["Автор истории: "].Class("text_red"), x.Author,br];
				var prof = x.Profession == null ? null : span[strong["Профессия: "].Class("text_red"),
					Html.ProfessionLink(x.Profession)];
				return Div("block_comment")[
					p[author,prof].Class("comment_info"),
					p[Raw(StringUtils.GetShortText(x.Description)), br, Url.Client()
						.SuccessStory(x.SuccessStoryID, "читать далее")].Class("opinion")];
			})];
		}

	}
}