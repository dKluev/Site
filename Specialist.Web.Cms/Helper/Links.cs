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

			var startedOrderMenu = Tree.New(helper.ListLink<Order>("�������������"));
			var completeOrderMenu = Tree.New(HtmlControls
					.Anchor("/OrderEntity/List?IsComplete=True&pageIndex=1",
					"�����������").ToString());

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
				Tree.New("������� �����")
				.AddNode(Tree.New(helper.ListLink<Section>("�����������")))
				.AddNode(Tree.New(helper.ListLink<Vendor>("�������")))
				.AddNode(Tree.New(helper.ListLink<Profession>("���������")))
				.AddNode(Tree.New(helper.ListLink<Product>("��������")))
				.AddNode(Tree.New(helper.ListLink<SiteTerm>("����������")))
				.AddNode(Tree.New(helper.ListLink<Course>("�����"))
					.AddNode(Tree.New(helper.ActionLink<SiteObjectRelationEntityController>(
					c => c.ReverseRelationSorting(typeof(Course).Name, null, null), "����������").ToString()))
					.AddNode(Tree.New(helper.ActionLink<CourseEntityController>(
					c => c.Update(), "����������").ToString()))
					)
				)
			.AddNode(
				Tree.New("�������")
				.AddNode(Tree.New(helper.ListLink<News>("�������")))
				.AddNode(Tree.New(helper.ListLink<Advice>("������")))
				.AddNode(Tree.New(helper.ListLink<Video>("�����"))
					.AddNode(Tree.New(helper.ListLink<VideoCategory>("�������"))))
				.AddNode(Tree.New(helper.ListLink<SuccessStory>("������� ������")))
				.AddNode(Tree.New(helper.ListLink<UsefulInformation>("�������� ����������")))
				.AddNode(Tree.New(helper.ListLink<Guide>("������������"))
				.AddNode(Tree.New(helper.ActionLink<SiteObjectRelationEntityController>(
					c => c.ReverseRelationSorting(typeof(Guide).Name, null, null), "����������").ToString()))
				)
				.AddNode(Tree.New(helper.ListLink<UserWork>("������ ����������"))
				.AddNode(Tree.New(helper.ListLink<UserWorkSection>("�������"))))
				.AddNode(Tree.New(helper.ListLink<Banner>("�������")))
				.AddNode(Tree.New(helper.ListLink<Response>("������"))
					.AddNode(Tree.New(helper.ActionLink<ResponseEntityController>(
					c => c.ExportSearch(null, null, null, null, false), "�����")
					.ToString())))
				.AddNode(Tree.New(helper.ListLink<Organization>("�����������"))
					.AddNode(Tree.New(helper.ListLink<OrgResponse>("������")))
					.AddNode(Tree.New(helper.ListLink<OrgProject>("�������"))))
				)
			.AddNode(
				Tree.New("������������� �����")
				.AddNode(Tree.New(helper.ListLink<MarketingAction>("�����"))))
			 .AddNode(
				Tree.New("��������������").AddNode(Tree.New("����").AddNode(Tree.New(
				helper.ActionLink<EmployeeEntityController>(c => c.TrainersInfo(),
					"�������������").ToString()),
					Tree.New(
					helper.ActionLink<UserEntityController>(c => c.Subscribers(),
					"����������").ToString()),
					Tree.New(
					helper.ActionLink<OrderEntityController>(c => c.Beeline(),
					"������").ToString())
					))
				.AddNode(
	                Tree.New("�����")
		                .AddNode(courseLink("������", c => c.CourseInfo()))
		                .AddNode(courseLink("����� ���c��", c => c.CourseNewVersions()))
		                .AddNode(courseLink("��� ������", c => c.WithoutTag(0)))
		                .AddNode(courseLink("��� �������������", c => c.WithoutGuide()))
		                .AddNode(courseLink("��� ��������", c => c.WithoutDescription()))
		                .AddNode(courseLink("������ 10 �������", c => c.WithoutResponse()))
		                .AddNode(courseLink("��������", c => c.TagsReport(null)))
		                .AddNode(courseLink("��� �������", c => c.WithoutChain())))
				.AddNode(Tree.New(helper.ListLink<Poll>("������")))
				.AddNode(Tree.New(helper.ListLink<Competition>("��������")))
				.AddNode(Tree.New(helper.ListLink<Vacancy>("��������")))
				.AddNode(Tree.New(helper.ListLink<SimplePage>("��������"))
					.AddNode(
						Tree.New(helper.ListLink<SimplePageRelation>("����� �������")))
					 .AddNode(Tree.New(helper.ListLink<PageMeta>("���� ����")))
					 .AddNode(Tree.New(helper.ListLink<HtmlBlock>("Html �����")))
					 )
				.AddNode(Tree.New(helper.ListLink<MailTemplate>("������� �����"))
				.AddNode(Tree.New(helper.ActionLink<MailTemplateEntityController>(
				c => c.MailList(), "��������").ToString())))
				.AddNode(Tree.New("������.������").AddNode(Tree.New(
				helper.ActionLink<HomeController>(c => c.Competitors(null), "����������").ToString()))
				.AddNode(Tree.New(
				helper.ActionLink<HomeController>(c => c.WordStat(), "WordStat").ToString()))
				)
				)
			  .AddNode(
				Tree.New("������")
				.AddNode(startedOrderMenu)
				.AddNode(completeOrderMenu))
			  .AddNode(
				Tree.New("�����")
				.AddNode(Tree.New(helper.ListLink<Test>("�����")))
				.AddNode(Tree.New(helper.ActionLink<OrderEntityController>(c => c.TestCertGroup(null),
					"����������� ������").ToString())
				));
			//                .AddNode(Tree.New(helper.ListLink<OrderDetail>("������ ������"))));
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
