using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Entities.Catalog;
using Specialist.Entities.Center;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Common;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Utils;
using Specialist.Web.Const;
using Specialist.Web.Core;
using Specialist.Web.Util;
using XmlResult = Specialist.Web.Common.Mvc.ActionResults.XmlResult;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Interface.Order;
using Specialist.Web.Cms.Root.Socials;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Controllers
{
    public partial class HomeController : ViewController
    {
        [Dependency]
        public IMainPageService MainPageService { get; set; }
        
        [Dependency]
        public INewsService NewsService { get; set; }

        [Dependency]
        public IResponseService ResponseService { get; set; }

        [Dependency]
    	public ISiteObjectRelationService SiteObjectRelationService { get; set; }

        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        [Dependency]
        public ISectionVMService SectionVMService { get; set; }

        [Dependency]
        public IGroupService GroupService { get; set; }

        [Dependency]
        public IEmployeeService EmployeeService { get; set; }

        [Dependency]
        public IOrderService OrderService { get; set; }

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public SimpleValueService SimpleValueService { get; set; }

        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }
		[Dependency]
        public IRepository2<Video> VideoService { get; set; }

	    public const string IsMainPage = "IsMainPage";
		[MobileCache(Duration = 24*60*60)]
        public virtual ActionResult Index() {
        	var model = MainPageService.Get();
			model.IsSecond = Htmls.IsSecond;
			ViewData[IsMainPage] = true;
			OdnoklassnikiRefreshToken();
			SetAdmitad();
        	return MView(Views.Home.Index, model);
        }

	    public void SetAdmitad() {
		    
		    var admitad = Request.Params["admitad_uid"];
		    if (admitad == null) return;
		    UserSettingsService.AdmitadId = admitad;
	    }

	    public void OdnoklassnikiRefreshToken() {
		    var code = Request.Params["code"];
		    if (code == null) return;
		    if (User == null || !User.InRole(Role.Admin)) return;
			SimpleValueService.EnableTracking();
		    var token = OdnoklassnikiUtl.GetRefreshToken(code);
		    SimpleValueService.OkRefreshToken = token;
			SimpleValueService.SubmitChanges();
	    }

    	public ActionResult News()
        {
            var news = NewsService.GetAllForMain().ToList();
            return View(news);
        }

        public ActionResult SiteMap() {
            HttpContext.Response.AddHeader("Last-modified", 
                DateTime.Now.ToUniversalTime().ToString("r"));
			var siteMap = CacheUtils.Get(MethodBase.GetCurrentMethod(), () => 
            XmlResult.ToStringWithDeclaration(new SiteMapGenerator().Get(Url)));
            
            return new XmlResult(siteMap);

        }

		[AjaxOnly]
		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		public ActionResult MainPageResponseNew()
		{
			var courseTableName = LinqToSqlUtils.GetTableName(typeof(Course));
			var courseTCs = GroupService.GetPlannedAndNotBegin().Where(g => g.Discount > 0)
				.Take(CommonConst.GroupForMainCount)
				.Select(x => x.Course_TC).Distinct().ToList();
			var courseTCsForResponse = CacheUtils.Get("courseTCsForMainResponses",
				() => SiteObjectRelationService.GetRelation(typeof (Course), courseTCs)
					.SelectMany(x => x.RelationObject.RelationObjectRelations.Where(sor => sor.ObjectType ==
						courseTableName).Select(y => y.Object_ID)).Select(x => x.ToString()).Distinct().ToList());
			var response = ResponseService.GetRandomForMainPage(courseTCsForResponse);
			var orgResponse = ResponseService.GetOrgRandomResponseForMainPage();
			return PartialView(Tuple.Create(response, orgResponse));
		}
       

		[AjaxOnly]
		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		public ActionResult MainPageResponse()
		{
			var courseTableName = LinqToSqlUtils.GetTableName(typeof(Course));
			var courseTCs = GroupService.GetPlannedAndNotBegin().Where(g => g.Discount > 0)
				.Take(CommonConst.GroupForMainCount)
				.Select(x => x.Course_TC).Distinct().ToList();
			var courseTCsForResponse = CacheUtils.Get("courseTCsForMainResponses",
				() => SiteObjectRelationService.GetRelation(typeof (Course), courseTCs)
					.SelectMany(x => x.RelationObject.RelationObjectRelations.Where(sor => sor.ObjectType ==
						courseTableName).Select(y => y.Object_ID)).Select(x => x.ToString()).Distinct().ToList());
			var response = ResponseService.GetRandomForMainPage(courseTCsForResponse);
			var orgResponse = ResponseService.GetOrgRandomResponseForMainPage();
			return PartialView(Tuple.Create(response, orgResponse));
		}
    }
}
