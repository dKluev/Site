using System.Web.Mvc;
using System.Web.Routing;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Mvc.Controllers;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Center;
using Specialist.Web.Controllers.Common;
using Specialist.Web.Controllers.Message;
using Specialist.Web.Controllers.Tests;
using Specialist.Web.Util.Routing;
using SimpleUtils.StrongRouting.Extensions;
using System.Linq;

namespace Specialist.Web.Common.Site
{
    public static class RoutingRegistrator
    {
        public static void Register(RouteCollection routes)
        {
		/*	routes.Ignore("programs/{*pathInfo}");
			routes.Ignore("News/{*pathInfo}");*/
        	//TODO routes.RouteExistingFiles = true
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("robots.txt");
            routes.IgnoreRoute("{*favicon}", new {favicon = @"(.*/)?favicon.ico(/.*)?"});


            routes.Register<CenterController>()
				.For(x => x.UploadJubileeFile(null)).AsQueryString()
				.For(x => x.JubileeFormPost(null)).AsQueryString()
				.For(x => x.ExpressOrder(null)).AsQueryString()
				.For(x => x.CollectionMctsPost(null)).AsQueryString()
				.For(x => x.OrderPaperCatalogPost(null)).AsQueryString()
				.For(x => x.SeminarRegistrationPost(0,null)).AsQueryString()
				.For(x => x.OrgCatalogPost(null)).AsQueryString()
				.For(x => x.MtsEmployeePost(null)).AsQueryString()
				.Done();
            routes.Register<GraduateController>()
                .For(x => x.UploadBest(null,null)).AsQueryString().Done();
            routes.Register<ClientController>()
                .For(x => x.UploadFileForJob(null)).AsQueryString().Done();
            routes.Register<DictionaryController>().Done();
            routes.Register<MessageController>()
				.For(x => x.EditPost(null)).AsQueryString().Done();
            routes.Register<ProfessionController>().Done();
            routes.Register<TrackController>().Done();
            routes.Register<ExamController>().Done();
            routes.Register<CertificationController>()
                  .For(c => c.CertificationNames(null)).AsQueryString()
                  .For(c => c.CertificationList(null)).AsQueryString()
				  .Done();
            routes.Register<VendorController>().Done();

            routes.Register<SimpleRegController>()
                .For(x => x.RegistrationPost(null)).AsQueryString()
                .For(x => x.Registration(null, null)).AsQueryString()
				.Done();
            routes.Register<ProfileController>()
                .For(x => x.Register(null,null,null)).AsQueryString()
                .For(x => x.FacebookLogin(null,null)).AsQueryString()
                .For(x => x.LinkFacebook(null)).AsQueryString()
                .For(x => x.CustomerTypeChoice(null)).AsQueryString()
                .For(x => x.RegisterPost(null)).AsQueryString()
                .For(x => x.RestorePassword((string)null)).AsQueryString()
                .For(x => x.ExamQuestionnaire(null)).AsQueryString()
                .For(x => x.WorkPlacePost(null)).AsQueryString()
                .Done();
            routes.Register<LocationsController>().Done();
            routes.Register<MasterPageController>().Done();
            routes.Register<SectionController>().Done();
            routes.Register<ProductController>().Done();
            routes.Register<InfoController>().Done();

            routes.Register<GroupController>()
                .For(c => c.Search(null)).AsQueryString()
                .For(c => c.SearchSubmit(null)).AsQueryString()
                .For(c => c.ForCourseTCList(null,false,0)).AsQueryString()
                .For(c => c.ForNews(0,0)).AsQueryString()
                .For(c => c.ForCourseTCListWithSort(null,false,0,0)).AsQueryString()
                .For(c => c.HotGroupsWithSort(0)).AsQueryString()
                .For(c => c.List(null, null)).AsQueryString()
                .For(c => c.ListPdf(null)).AsQueryString()
                .Done();

            routes.Register<CourseController>()
                .For(c => c.CourseNames(null)).AsQueryString()
                .For(c => c.Search(null)).AsQueryString()
                .For(c => c.ElearningNames(null)).AsQueryString()
                .For(c => c.CourseListsForSection(0)).AsQueryString()
                .For(c => c.CourseListsForVendor(0)).AsQueryString()
                .For(c => c.UserCourseInfoPost(null)).AsQueryString()
                .For(c => c.MainCourses()).Url("courses/")
                .Done();
            routes.Register<TestEditController>()
                .For(c => c.GetTests(null)).AsQueryString()
                .For(c => c.GetModules(0,null)).AsQueryString()
                .For(c => c.GetModuleSets(0,null)).AsQueryString()
                .For(c => c.GetQuestions(0,null)).AsQueryString()
                .For(c => c.GetAnswers(0,null)).AsQueryString()
                .For(c => c.GetModulesAuto(0,null)).AsQueryString()
                .For(c => c.GetCoursesAuto(null)).AsQueryString()
                .For(c => c.GetEmployeeAuto(null)).AsQueryString()
                .For(c => c.UploadQuestionFile(0,null)).AsQueryString()
                .For(c => c.UploadAnswerFile(0,null)).AsQueryString()
                .For(c => c.GetAnswersAuto(0,null)).AsQueryString()
                .Done();
            routes.Register<TestController>().Done();
            routes.Register<LmsController>()
				.For(x =>x.UpdateLecture(null)).AsQueryString()
				.For(x =>x.LectureQuestionnairePost(null)).AsQueryString()
				.Done();
            routes.Register<BadgeController>().Done();
            routes.Register<SimpleTestController>().Done();
            routes.Register<OrgTestController>().Done();
            routes.Register<OrgProfileController>()
                .For(c => c.GetCoursesAuto(null)).AsQueryString()
                .For(c => c.GetStudentsAuto(null)).AsQueryString()
                .For(c => c.GroupSearchPost(null)).AsQueryString()
                .For(c => c.StatusUpdatePost(null)).AsQueryString()
                .Done();
            routes.Register<GroupTestController>()
                .For(c => c.GetGroupTests(0,null)).AsQueryString()
                .For(c => c.GetTestsAuto(0,null)).AsQueryString()
                .Done();

            routes.Register<TestRunController>()
                .For(c => c.ResultPost(0,null)).AsQueryString()
                .For(c => c.Start(0,null,null)).AsQueryString()
				.Done();
            routes.Register<EmployeeController>()
 //               .For(c => c.Trainer(null)).Url("Trainer/")
                .For(c => c.AboutTrainer(null, SimplePages.Urls.AboutTrainer, null)).Url("trainer/")
                .For(c => c.Manager(null)).Url("manager/")
				.For(c => c.AddResponsePost(null)).AsQueryString()
				.For(c => c.TrainersJson(null)).AsQueryString()
                .Done();

            routes.Register<ClientController>()
                .For(c => c.PrivatePerson(SimplePages.Urls.SuccessStories, null))
                    .Url("center/references/private-persons/")
                .For(c => c.CorporateClients(SimplePages.Urls.Responses, null))
                    .Url("center/references/corporate-clients/")
              /*  .For(c => c.SearchVacancy(SimplePages.Urls.SearchParamsVacancy, null)).Url("client/searchvacancy/")
                .For(c => c.SearchResume(SimplePages.Urls.SearchParamsResume, null)).Url("client/searchresume/")*/
                .Done();
         
			routes.Register<PartnerController>()
				.For(x => x.SberbankCallback(null,null,null,0)).AsQueryString()
				.Done();
            routes.RegisterController("Cart");
            routes.RegisterController("Order");
            routes.RegisterController("Chart");
            routes.RegisterController("EditCart");
            routes.RegisterController("File");
            routes.RegisterController("CompanyFile");
            routes.RegisterController("Home");
            routes.RegisterController("Page");
            routes.RegisterController("Account");
            routes.RegisterController("Rss");

            routes.Register<MobileAppController>().Done();


            routes.MapRoute( "CityDefailt", "Contacts",
               new
               {
                   controller = "Locations",
                   action = "City",
                   urlName = (string)null
               }
               );

            foreach (var newsType in NewsType.All.Where(x => !x.HideFromTabs))
            {
                routes.MapRoute(
                    "News" + newsType.UrlName + "ListDefault",
                    "center/news/" + newsType.UrlName + "/{pageIndex}",
                    new
                    {
                        controller = "SiteNews",
                        action = "List",
                        newsTypeUrl = newsType.UrlName,
                        pageIndex = (int?)null
                    }
                    );

            }

			
            routes.Register<SiteNewsController>()
     //        .For(c => c.List(null, null)).Url("center/news/")
             .For(c => c.Search(null, 0, null)).Url("news/list/")
             .For(c => c.Details(0, null)).Url("news/")
             .Done();


        /*    routes.MapRoute(
                    "NewsListDefault",
                    "center/news",
                    new
                    {
                        controller = "SiteNews",
                        action = "List",
                        newsTypeUrl = (string)null,
                        pageIndex = (int?)null
                    }
                    ); */
         
/*
            routes.MapRouteWithoutMvc(
                "NewsDefault",
                "center.mvc/news/{newsID}/{title}",
                new { controller = "SiteNews", action = "Details", title = string.Empty },
                    new { newsID = @"\d+" }
                );*/



			DecorateRoutes(routes);

        	 routes.MapRoute("SqlRss",
                    "RSSSql/rss.aspx",
                    new
                    {
                        controller = "Rss",
                        action = "Sql"
                    }
                    );

        }

		private static void DecorateRoutes(RouteCollection routeCollection)
{
    for (int i = 0; i < routeCollection.Count; i++)
    {
        routeCollection[i] = new LowercasingRoute(routeCollection[i]);
    }
}

    }
}