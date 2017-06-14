using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Web.Cms.Controllers;
using Specialist.Web.Common.Extension;
using System.Web.Mvc.Html;
using Microsoft.Web.Mvc;
using SimpleUtils;
using System.Linq;
using Specialist.Web.Common.Html;
using Specialist.Web.Helpers;
using Microsoft.Practices.Unity;
using Specialist.Web.Common.Utils;

namespace Specialist.Web.Cms.Helper {
	public static class Links {

		public static string ListLink<T>(this HtmlHelper helper, string name) {
			return helper.ActionLink(name, "List",
				   new {
					   controller = typeof(T).Name + Const.Common.ControlPosfix, pageIndex = 1
				   }).ToString();
		}

		public static Tree<string> MainMenu(this HtmlHelper helper) {
			return MethodBase.GetCurrentMethod().Cache(() => GetMainMenu(helper), 24);
		}

		static Tree<string> GetMainMenu(this HtmlHelper helper) {
			var paymentTypes =
				MvcApplication.Container.Resolve<IRepository<PaymentType>>().GetAll();


			var model = new Tree<string>();

			var startedOrderMenu = Tree.New(helper.ListLink<Order>("Незавершенные"));
			var completeOrderMenu = Tree.New(HtmlControls
					.Anchor("/OrderEntity/List?IsComplete=True&pageIndex=1",
					"Завершенные").ToString());

			foreach (var paymentType in paymentTypes) {
				completeOrderMenu.AddNode(Tree.New(
					HtmlControls
					.Anchor(
					"/OrderEntity/List?PaymentType_TC={0}&IsComplete=True&pageIndex=1"
						.FormatWith(paymentType.PaymentType_TC), paymentType.Name)
					.ToString()));
			}

			Func<string, Expression<Action<CourseEntityController>>, Tree<string>> courseLink =
				(title, selector) => Tree.New(helper.ActionLink(selector, title).ToString());
			model
			.AddNode(
				Tree.New("Объекты сайта")
				.AddNode(Tree.New(helper.ListLink<Section>("Направления")))
				.AddNode(Tree.New(helper.ListLink<Vendor>("Вендоры")))
				.AddNode(Tree.New(helper.ListLink<Profession>("Профессии")))
				.AddNode(Tree.New(helper.ListLink<Product>("Продукты")))
				.AddNode(Tree.New(helper.ListLink<SiteTerm>("Технологии")))
				.AddNode(Tree.New(helper.ListLink<Course>("Курсы"))
					.AddNode(Tree.New(helper.ActionLink<SiteObjectRelationEntityController>(
					c => c.ReverseRelationSorting(typeof(Course).Name, null, null), "Сортировка").ToString()))
					.AddNode(Tree.New(helper.ActionLink<CourseEntityController>(
					c => c.Update(), "Обновление").ToString()))
					)
				)
			.AddNode(
				Tree.New("Контент")
				.AddNode(Tree.New(helper.ListLink<News>("Новости")))
				.AddNode(Tree.New(helper.ListLink<Advice>("Советы")))
				.AddNode(Tree.New(helper.ListLink<Video>("Видео"))
					.AddNode(Tree.New(helper.ListLink<VideoCategory>("Разделы"))))
				.AddNode(Tree.New(helper.ListLink<SuccessStory>("Истории успеха")))
				.AddNode(Tree.New(helper.ListLink<UsefulInformation>("Полезная информация")))
				.AddNode(Tree.New(helper.ListLink<Guide>("Путеводители"))
				.AddNode(Tree.New(helper.ActionLink<SiteObjectRelationEntityController>(
					c => c.ReverseRelationSorting(typeof(Guide).Name, null, null), "Сортировка").ToString()))
				)
				.AddNode(Tree.New(helper.ListLink<UserWork>("Работы слушателей"))
				.AddNode(Tree.New(helper.ListLink<UserWorkSection>("Разделы"))))
				.AddNode(Tree.New(helper.ListLink<Banner>("Баннеры")))
				.AddNode(Tree.New(helper.ListLink<Response>("Отзывы"))
					.AddNode(Tree.New(helper.ActionLink<ResponseEntityController>(
					c => c.ExportSearch(null, null, null, null, false), "Поиск")
					.ToString())))
				.AddNode(Tree.New(helper.ListLink<Organization>("Организации"))
					.AddNode(Tree.New(helper.ListLink<OrgResponse>("Отзывы")))
					.AddNode(Tree.New(helper.ListLink<OrgProject>("Проекты"))))
				)
			.AddNode(
				Tree.New("Маркетинговые акции")
				.AddNode(Tree.New(helper.ListLink<MarketingAction>("Акции"))))
			 .AddNode(
				Tree.New("Инфраструктура").AddNode(Tree.New("Инфо").AddNode(Tree.New(
				helper.ActionLink<EmployeeEntityController>(c => c.TrainersInfo(),
					"Преподаватели").ToString()),
					Tree.New(
					helper.ActionLink<UserEntityController>(c => c.Subscribers(),
					"Подписчики").ToString()),
					Tree.New(
					helper.ActionLink<OrderEntityController>(c => c.Beeline(),
					"Билайн").ToString())
					))
				.AddNode(
	                Tree.New("Курсы")
		                .AddNode(courseLink("Анонсы", c => c.CourseInfo()))
		                .AddNode(courseLink("Новые верcии", c => c.CourseNewVersions()))
		                .AddNode(courseLink("Без связей", c => c.WithoutTag(0)))
		                .AddNode(courseLink("Без путеводителей", c => c.WithoutGuide()))
		                .AddNode(courseLink("Без описания", c => c.WithoutDescription()))
		                .AddNode(courseLink("Меньше 10 отзывов", c => c.WithoutResponse()))
		                .AddNode(courseLink("Привязки", c => c.TagsReport(null)))
		                .AddNode(courseLink("Без цепочек", c => c.WithoutChain())))
				.AddNode(Tree.New(helper.ListLink<Poll>("Опросы")))
				.AddNode(Tree.New(helper.ListLink<Competition>("Конкурсы")))
				.AddNode(Tree.New(helper.ListLink<Vacancy>("Вакансии")))
				.AddNode(Tree.New(helper.ListLink<SimplePage>("Страницы"))
					.AddNode(
						Tree.New(helper.ListLink<SimplePageRelation>("Связи страниц")))
					 .AddNode(Tree.New(helper.ListLink<PageMeta>("Мета теги")))
					 .AddNode(Tree.New(helper.ListLink<HtmlBlock>("Html блоки")))
					 )
				.AddNode(Tree.New(helper.ListLink<MailTemplate>("Шаблоны писем"))
				.AddNode(Tree.New(helper.ActionLink<MailTemplateEntityController>(
				c => c.MailList(), "Рассылка").ToString())))
				.AddNode(Tree.New("Яндекс.Директ").AddNode(Tree.New(
				helper.ActionLink<HomeController>(c => c.Competitors(null), "Конкуренты").ToString()))
				.AddNode(Tree.New(
				helper.ActionLink<HomeController>(c => c.WordStat(), "WordStat").ToString()))
				)
				)
			  .AddNode(
				Tree.New("Заказы")
				.AddNode(startedOrderMenu)
				.AddNode(completeOrderMenu))
			  .AddNode(
				Tree.New("Тесты")
				.AddNode(Tree.New(helper.ListLink<Test>("Тесты")))
				.AddNode(Tree.New(helper.ActionLink<OrderEntityController>(c => c.TestCertGroup(null),
					"Сертификаты группы").ToString())
				));
			//                .AddNode(Tree.New(helper.ListLink<OrderDetail>("Заказы курсов"))));
			return model;
		}


		public static string SiteObjectLink(this HtmlHelper helper, SiteObject siteObject) {
			if (siteObject == null)
				return null;
			var entityType = (new ContextProvider()).GetTypeByTableName(siteObject.Type);
			var linkName = siteObject.Name;
			/*   if(siteObject.Type == LinqToSqlUtils.GetTableName(typeof(Block)))
			   {
				   var context = new SpecialistWebDataContext();
				   var block = context.Blocks.FirstOrDefault(
					   b => b.BlockID == (int) siteObject.ID);
				   return helper.BlockLink(block);

			   }*/
			if (siteObject.Type == LinqToSqlUtils.GetTableName(typeof(Employee))
				&& siteObject.Entity != null) {
				return HtmlControls.Anchor(Const.Common.SiteDomain +
					helper.GetUrlFor(siteObject.Entity),
					siteObject.ObjectType.Name + ":" +
						siteObject.Entity.As<Employee>().FullName).ToString();

			}
			if (linkName.IsEmpty())
				linkName = siteObject.Type;
			return helper.ActionLink(siteObject.ObjectType.Name + ":" + linkName, "Edit",
				entityType.Name + Const.Common.ControlPosfix, new {
					id = siteObject.ID
				}, null).ToString();
		}

	}
}
