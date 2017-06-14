using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity.InterceptionExtension;
using SimpleUtils.FluentAttributes;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Providers;
using Microsoft.Practices.Unity;
using MvcContrib.Unity;
using NLog;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using Specialist.Entities.Catalog;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Passport.MetaData;
using Specialist.Services;
using Specialist.Services.Common;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Site;
using Specialist.Web.Common.Util;
using Specialist.Web.Common.Utils.Tasks;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Common;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Core;
using Specialist.Web.Pages;
using Specialist.Web.Pages.Interfaces;
using Specialist.Web.Root.Tests.MetaDatas;
using Specialist.Web.Validators.Core;
using Logger = Specialist.Services.Utils.Logger;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web
{
    public class MvcApplication : System.Web.HttpApplication, IUnityContainerAccessor
    {
        public static bool IsDebug {
            get {
#if(DEBUG)
                return true;
#endif
                return false;
            }
        }


    	public override string GetVaryByCustomString(HttpContext context, string arg) {
    		
			if(arg == "CityTC") {
				var userSettings = Container.Resolve<UserSettingsService>();
				return userSettings.CityTC ?? "all";
			}
			if(arg == "IsChrome") {
				return RssController.IsChrome().ToString();
			}
			if (CacheManager.CacheKeys.ContainsKey(arg)) {
				return CacheManager.Get(arg).ToString();

			}
    		return base.GetVaryByCustomString(context, arg); 
    	}

		public static IMetaDataProvider MetaDataProvider { get; set; }

		static MvcApplication() {
			MetaDataProvider = new ManyAssemblyMetaDataProvider(typeof (TestMD).Assembly,
				typeof(UserMD).Assembly);
		/*	Config.DescriptionProvider = new MetaDataTypeDescriptionProvider(
							MetaDataProvider);*/

		}


    	public static void RegisterRoutes(RouteCollection routes)
        {
            RoutingRegistrator.Register(routes);

            routes.MapRoute(
            "Home",
            "",
            new { controller = "Home", action = "Index", id = "" }
            );

            routes.MapRoute(
                "Default",
                "{*url}",
                new { controller = "Page", action = "Process", url = "" }
                );
		
        }


        protected void Application_Start() {
        	Htmls.IsNewSite = true;
            MvcHandler.DisableMvcResponseHeader = true; 
            InitializeContainer();
            RegisterRoutes(RouteTable.Routes);

            Config.DescriptionProvider = new MetaDataTypeDescriptionProvider(
                new AssemblyMetaDataProvider(typeof(SpecialistDataContext).Assembly));
        	CommonConst.IsMobile = HttpRuntime.AppDomainAppPath.ToLower().Contains("mobile");
        	CommonConst.IsForTest = HttpRuntime.AppDomainAppPath.ToLower().Contains("fortest");
//            if(!(IsDebug || HttpRuntime.AppDomainAppPath.ToLower().Contains("test"))) 
//				Logger.Info("main start");


//            SqlDependency.Start(GetSpecialistWebConnectionString()); 

        }

        protected void Application_End() {
        }

     
    /*    protected void Application_Stop()
        {
			StopTasks();
//            SqlDependency.Stop(GetSpecialistWebConnectionString());

        }*/

        protected virtual void InitializeContainer()
        {
            if (Container == null)
            {
                Container = new UnityContainer();

                UnityRegistrator.RegisterControls(Container, 
                    typeof(HomeController).Assembly);
                UnityRegistrator.RegisterServices(Container);
            	Container
            		.RegisterType<ISimplePageVMService, SimplePageVMService>()
            		.RegisterInstance(typeof(IMetaDataProvider), MetaDataProvider)
	                .RegisterType<IAnnounceService, AnnounceService>()
	                .RegisterType<ISectionVMService, SectionVMService>()
            		.RegisterType<ICourseVMService, CourseVMService>();
            	Container.Configure<Interception>()
            		.SetDefaultInterceptorFor<CourseVMService>(
            			new VirtualMethodInterceptor())
            		.SetDefaultInterceptorFor<SuperJobService>(
            			new VirtualMethodInterceptor());


                ValidatorRegistrator.Register(Container);

                ControllerBuilder.Current.
                    SetControllerFactory(typeof(UnityControllerFactory));
            }
        }


		void Application_BeginRequest(object sender, EventArgs e) {
			if(CommonConst.IsMobile)
				return;
            var userSettings = new UserSettingsService();
			if(userSettings.CommonSite)
				return;
			var request = HttpContext.Current.Request;
			if(request.UserAgent.IsEmpty())
				return;
			if(request.Url.PathAndQuery.ToLower().StartsWith("/partner/"))
				return;
			if(request["commonsite"] != null) {
				userSettings.CommonSite = true;
				return;
			}
			var maxSize = Math.Max(request.Browser.ScreenPixelsHeight,
				request.Browser.ScreenPixelsWidth);
			if (request.Browser.IsMobileDevice && maxSize < 600) {
				Response.Redirect(CommonConst.MobileRoot + Request.RawUrl);
			}else {
				userSettings.CommonSite = true;
			}
		}

        void Application_Error(object sender, EventArgs e)
        {
            if (IsDebug)
                return;
            var ex = Server.GetLastError().GetBaseException();
            var user = Container.Resolve<IAuthService>().CurrentUser;
		/*	if(ex is HttpRequestValidationException) {
				if(ex.As<HttpRequestValidationException>().Message.Contains("Cookie")) {
				/*	Request.Cookies.Clear();
					Response.Cookies.Clear();
					Response.Redirect("/page/cleancookies");
					return;
				}
			}*/
			if (ex is HttpException) {
				Response.StatusCode = ex.As<HttpException>().GetHttpCode();
	            Logger.Exception(ex, user);
			}
			else if(Response.StatusCode == (int) HttpStatusCode.OK) {
	            Logger.Exception(ex, user);
				Response.StatusCode = (int) HttpStatusCode.NotFound;
			}else {
	            Logger.Exception(ex, user);
			}
			

        }


        /// <summary>
        /// Окончание запроса к приложению
        /// </summary>
        public void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
        
//            var logger = LogManager.GetCurrentClassLogger();
        /*    if(HttpContext.Current.Request.Browser.Type == "IE6") {
                logger.Info("no cache");*/
//                ResponseHeader.SetNoCache();
         /*   }else {
                logger.Info("cache");
            }*/
//            ProcessLastModifiedDate();


            //            if (HttpContext.Current.Response.ContentType != "text/html")
            //                return;
            //            HttpContext.Current.Response.ClearHeaders();
            //            
            //            HttpContext.Current.Response.AddHeader("Last-modified", new DateTime(2009, 01, 01).ToUniversalTime().ToString("r"));
            /* HttpContext.Current.Response.Headers.Add("Last-modified", 
                 new DateTime(2008, 03, 01).ToUniversalTime().ToString("r")); */
            //Работа с HTTP заголовками страницы
            /*  if (HttpContext.Current.Response.StatusCode != 304)
              {
                  if (HttpContext.Current.Handler is ViewPage)
                  {
                      Page oPage = (ViewPage)HttpContext.Current.Handler;
                      SetLastModifiedHeader(oPage);
                  }
              }*/
        }

        private void ProcessLastModifiedDate()
        {
        /*    if (HttpContext.Current.Request.Headers["If-Modified-Since"] != null)
            {
                var mvcHandler = HttpContext.Current.Handler as MvcHandler;
                if (mvcHandler == null)
                    return;
                var controller =
                    ControllerBuilder.Current.GetControllerFactory()
                        .CreateController(mvcHandler.RequestContext, "Course");
                controller.Execute(mvcHandler.RequestContext);
            }*/

          /*  if (HttpContext.Current.Items["Last-modified"] == null)
                return;
            var date = (DateTime)HttpContext.Current.Items["Last-modified"];

            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response
                .AddHeader("Last-modified", date.ToUniversalTime().ToString("r"));
            GetIfModifiedSince(date.ToUniversalTime());*/

        }


        /// <summary>
        /// Установка HTTP заголовков страницы
        /// </summary>
   /*     private void SetLastModifiedHeader(object obj)
        {
            //Заголовок Last-modified
            var oLastModified = DateTime.MinValue;



            this.GetIfModifiedSince(oLastModified);

            HttpContext.Current.Response.AddHeader("Last-modified", oLastModified.ToUniversalTime().ToString("r"));
        }*/

        /// <summary>
        /// Метод позволяет отвечать на запросы с заголовком If-modified-since
   /*     /// </summary>
        public void GetIfModifiedSince(DateTime oLastModified)
        {
            DateTime oHeaderDate;
            if (HttpContext.Current.Request.Headers["If-Modified-Since"] != null)
            {
                if (!DateTime.TryParse(HttpContext.Current.Request.Headers["If-Modified-Since"], out oHeaderDate))
                {
                    return;
                }
                oHeaderDate = oHeaderDate.ToUniversalTime();
            }
            else
            {
                return;
            }

            var oModifiedDate = oLastModified;

            if (oModifiedDate == DateTime.MinValue)
                return;
            if (oModifiedDate.ToString("r") != oHeaderDate.ToString("r"))
                return;

            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.StatusCode = 304;
            HttpContext.Current.Response.AddHeader("Last-modified", oHeaderDate.ToString("r"));
            HttpContext.Current.Response.End();
        }*/



        IUnityContainer IUnityContainerAccessor.Container
        {
            get { return Container; }
        }


        public static IUnityContainer Container
        {
            get;
            private set;
        }



    }
}
