using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using MvcContrib.Unity;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Providers;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using Specialist.Entities.Center;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Tests.Consts;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Controllers;
using Specialist.Web.Cms.Core;
using Specialist.Web.Cms.Core.FluentMetaData;
using Specialist.Web.Cms.MetaData;
using Specialist.Web.Cms.MetaData.Shop;
using Specialist.Web.Cms.Repository;
using Specialist.Web.Cms.Services;
using Specialist.Web.Common.Site;
using Specialist.Web.Common.Util;
using Specialist.Web.Common.Utils.Tasks;
using Specialist.Web.Root.Common.MailList;
using Specialist.Web.Root.Tests.MetaDatas;
using Specialist.Web.WinService.Tasks;

namespace Specialist.Web.Cms {
	public class MvcApplication : HttpApplication, IUnityContainerAccessor {
		public static bool IsDebug {
			get {
#if(DEBUG)
				return true;
#endif
				return false;
			}
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("Content/{*pathInfo}");
			routes.IgnoreRoute("Scripts/{*pathInfo}");

			RoutingRegistrator.Register(routes);


			routes.MapRoute(
				"ListDefault",
				"{controller}/List",
				new {controller = "Home", action = "List"},
				new[] {Const.Common.ControllerNamespace}
				);

			routes.MapRoute(
				"CmsDefault",
				"{controller}/{action}/{id}",
				new {controller = "Home", action = "Index", id = ""},
				new[] {Const.Common.ControllerNamespace}
				);

			routes.MapRoute(
				"Home",
				"",
				new {controller = "Home", action = "Index", id = ""},
				new[] {Const.Common.ControllerNamespace}
				);
		}

		public static IMetaDataProvider MetaDataProvider { get; set; }

		static MvcApplication() {
			MetaDataProvider = new ManyAssemblyMetaDataProvider(typeof (SectionMD).Assembly, 
				typeof(TestMD).Assembly);
			Config.DescriptionProvider = new MetaDataTypeDescriptionProvider(
				new AssemblyMetaDataProvider(typeof (UserMD).Assembly));
		}


		protected virtual void InitializeContainer() {
			if (Container == null) {
				Container = new UnityContainer();
				UnityRegistrator.RegisterControls(Container,
					typeof (HomeController).Assembly);
				UnityRegistrator.RegisterServices(Container);
				Container
					.RegisterType<IRepository<SiteObjectType>, SiteObjectTypeRepository>()
					.RegisterType<IRepository<PaymentType>, PaymentTypeRepository>()
					.RegisterType<IRepository<NewsType>, InMemoryRepository<NewsType>>()
					.RegisterType<IRepository<OrderCustomerType>, InMemoryRepository<OrderCustomerType>>()
					.RegisterType<IRepository<TestStatus>, NamedIdRepository<TestStatus>>()
					.RegisterType<IRepository<ResponseRating>, InMemoryRepository<ResponseRating>>()
					.RegisterType<IRepository<MarketingActionType>, InMemoryRepository<MarketingActionType>>()
					.RegisterType<IRepository<RawQuestionnaireType>,
						InMemoryRepository<RawQuestionnaireType>>()
					.RegisterType<IRepository<CenterVacancyType>,
						InMemoryRepository<CenterVacancyType>>()
					.RegisterType<IRepository<Order>, OrderService>()
					.RegisterType<IRepository<OrderDetail>, OrderDetailService>()
					.RegisterInstance(typeof (IMetaDataProvider), MetaDataProvider)
					.RegisterType<IContextProvider, ContextProvider>()
					.RegisterType<IResponseLogicService, ResponseLogicService>();

				Container.Configure<Interception>()
					.SetDefaultInterceptorFor<PaymentTypeRepository>(
						new VirtualMethodInterceptor());


				ControllerBuilder.Current.
					SetControllerFactory(typeof (CmsControllerFactory));
				ControllerBuilder.Current.DefaultNamespaces.Add(Const.Common.ControllerNamespace);
			}
		}

		protected void Application_Start() {
//			FilterProviders.Providers.Add(new AuthFilterProvider());

			InitializeContainer();
			RegisterRoutes(RouteTable.Routes);
			InitializeFluentMetaData();

			if (!IsDebug) {
				StartTasks();
			}
		}

		protected void Application_End() {
			MailListService.IsStopped = true;
			StopTasks();
		}

		private void Application_Error(object sender, EventArgs e) {
			if (IsDebug)
				return;
			var user = Container.Resolve<IAuthService>().CurrentUser;
			var ex = Server.GetLastError().GetBaseException();
			Logger.Exception(ex, user);
		}

		public static readonly List<TaskWithTimer> _tasks = new List<TaskWithTimer> {
			new ResponseExportTask(),
        	new MailUserOrdersTask(),
			new TestCertPaidTask(),
			new EntityActivationTask(),
			new YandexDirectTasks(),
			new NewCourseVersionTask(),
			new GroupPhotosTask(),
			new EveryDayTasks(),
			new SocialTask()
		};

		private void StartTasks() {
			Task.Factory.StartNew(() => {
				foreach (var task in _tasks) {
					task.Start();
				}
			});
		}

		private void StopTasks() {
			foreach (var task in _tasks) {
				task.Stop();
			}
		}


		private void InitializeFluentMetaData() {
			MetaDataAttributeProviders.AdditionalAttributes
				.AddFluent(CmsMetaDataAttributeProviders.ForeignTypeAttribute)
				.AddFluent(CmsMetaDataAttributeProviders.DispayAttribute);
		}

		IUnityContainer IUnityContainerAccessor.Container {
			get { return Container; }
		}


		public static IUnityContainer Container { get; private set; }
	}
}