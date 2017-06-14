

using System.Web.Mvc;
using System.Web.Routing;
using SimpleUtils.FluentHtml.Tags;

namespace Specialist.Web.Const {
	public static class UrlHelperLinkExtention{

					
				
	//	private static ExtendedControllerLinks _ExtendedControllerLinks = null;
		
		public static ExtendedControllerLinks Extended(this UrlHelper urlHelper) {
		//	if(_ExtendedControllerLinks == null) _ExtendedControllerLinks = new ExtendedControllerLinks(urlHelper);
		//	return _ExtendedControllerLinks;
		return new ExtendedControllerLinks(urlHelper);
		}

		public class ExtendedControllerUrls{
			
			private UrlHelper _url;

			public ExtendedControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckRedirectToAction0(){
				CheckMethod<Specialist.Web.Util.ExtendedController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Extended");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Extended", routeValues);
			}
					void TypeCheckRedirectBack1(){
				CheckMethod<Specialist.Web.Util.ExtendedController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Extended");
								
				return _url.Action("RedirectBack", "Extended", routeValues);
			}
					void TypeCheckMView2(){
				CheckMethod<Specialist.Web.Util.ExtendedController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Extended");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Extended", routeValues);
			}
				

		

		}


			public class ExtendedControllerLinks{
			
			private UrlHelper _url;

			public ExtendedControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static ExtendedControllerUrls _ExtendedControllerUrls = null;
			public ExtendedControllerUrls Urls { 
				get {
				//	if(_ExtendedControllerUrls == null) _ExtendedControllerUrls = new ExtendedControllerUrls(_url);
					//return _ExtendedControllerUrls;
					return new ExtendedControllerUrls(_url);
				}
			}

		
		
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Extended().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Extended().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Extended().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static ViewControllerLinks _ViewControllerLinks = null;
		
		public static ViewControllerLinks View(this UrlHelper urlHelper) {
		//	if(_ViewControllerLinks == null) _ViewControllerLinks = new ViewControllerLinks(urlHelper);
		//	return _ViewControllerLinks;
		return new ViewControllerLinks(urlHelper);
		}

		public class ViewControllerUrls{
			
			private UrlHelper _url;

			public ViewControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckBaseView0(){
				CheckMethod<Specialist.Web.Core.ViewController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "View");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "View", routeValues);
			}
					void TypeCheckBaseView1(){
				CheckMethod<Specialist.Web.Core.ViewController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "View");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "View", routeValues);
			}
					void TypeCheckBaseViewWithTitle2(){
				CheckMethod<Specialist.Web.Core.ViewController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "View");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "View", routeValues);
			}
					void TypeCheckRedirectToAction3(){
				CheckMethod<Specialist.Web.Core.ViewController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "View");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "View", routeValues);
			}
					void TypeCheckRedirectBack4(){
				CheckMethod<Specialist.Web.Core.ViewController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "View");
								
				return _url.Action("RedirectBack", "View", routeValues);
			}
					void TypeCheckMView5(){
				CheckMethod<Specialist.Web.Core.ViewController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "View");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "View", routeValues);
			}
				

		

		}


			public class ViewControllerLinks{
			
			private UrlHelper _url;

			public ViewControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static ViewControllerUrls _ViewControllerUrls = null;
			public ViewControllerUrls Urls { 
				get {
				//	if(_ViewControllerUrls == null) _ViewControllerUrls = new ViewControllerUrls(_url);
					//return _ViewControllerUrls;
					return new ViewControllerUrls(_url);
				}
			}

		
		
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.View().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.View().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.View().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.View().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.View().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.View().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static LocationsControllerLinks _LocationsControllerLinks = null;
		
		public static LocationsControllerLinks Locations(this UrlHelper urlHelper) {
		//	if(_LocationsControllerLinks == null) _LocationsControllerLinks = new LocationsControllerLinks(urlHelper);
		//	return _LocationsControllerLinks;
		return new LocationsControllerLinks(urlHelper);
		}

		public class LocationsControllerUrls{
			
			private UrlHelper _url;

			public LocationsControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckCity0(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.City((System.String)null));
			}
			public string City(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "City");
			//	routeValues.Add("controller", "Locations");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("City", "Locations", routeValues);
			}
					void TypeCheckClassRooms1(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.ClassRooms((System.String)null));
			}
			public string ClassRooms(System.String complexTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ClassRooms");
			//	routeValues.Add("controller", "Locations");
									routeValues.Add("complexTC", complexTC);
								
				return _url.Action("ClassRooms", "Locations", routeValues);
			}
					void TypeCheckComplex2(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.Complex((System.String)null));
			}
			public string Complex(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Complex");
			//	routeValues.Add("controller", "Locations");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Complex", "Locations", routeValues);
			}
					void TypeCheckComplexes3(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.Complexes());
			}
			public string Complexes(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Complexes");
			//	routeValues.Add("controller", "Locations");
								
				return _url.Action("Complexes", "Locations", routeValues);
			}
					void TypeCheckContacts4(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.Contacts());
			}
			public string Contacts(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Contacts");
			//	routeValues.Add("controller", "Locations");
								
				return _url.Action("Contacts", "Locations", routeValues);
			}
					void TypeCheckMetroBlock5(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.MetroBlock());
			}
			public string MetroBlock(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MetroBlock");
			//	routeValues.Add("controller", "Locations");
								
				return _url.Action("MetroBlock", "Locations", routeValues);
			}
					void TypeCheckBaseView6(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Locations");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Locations", routeValues);
			}
					void TypeCheckBaseView7(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Locations");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Locations", routeValues);
			}
					void TypeCheckBaseViewWithTitle8(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Locations");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Locations", routeValues);
			}
					void TypeCheckRedirectToAction9(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Locations");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Locations", routeValues);
			}
					void TypeCheckRedirectBack10(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Locations");
								
				return _url.Action("RedirectBack", "Locations", routeValues);
			}
					void TypeCheckMView11(){
				CheckMethod<Specialist.Web.Controllers.LocationsController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Locations");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Locations", routeValues);
			}
				

		

		}


			public class LocationsControllerLinks{
			
			private UrlHelper _url;

			public LocationsControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static LocationsControllerUrls _LocationsControllerUrls = null;
			public LocationsControllerUrls Urls { 
				get {
				//	if(_LocationsControllerUrls == null) _LocationsControllerUrls = new LocationsControllerUrls(_url);
					//return _LocationsControllerUrls;
					return new LocationsControllerUrls(_url);
				}
			}

		
		
		


			public TagA City(	System.String urlName, object content){
				var localActionUrl = _url.Locations().Urls.City(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ClassRooms(	System.String complexTC, object content){
				var localActionUrl = _url.Locations().Urls.ClassRooms(complexTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Complex(	System.String urlName, object content){
				var localActionUrl = _url.Locations().Urls.Complex(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Complexes(	object content){
				var localActionUrl = _url.Locations().Urls.Complexes();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Contacts(	object content){
				var localActionUrl = _url.Locations().Urls.Contacts();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MetroBlock(	object content){
				var localActionUrl = _url.Locations().Urls.MetroBlock();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Locations().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Locations().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Locations().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Locations().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Locations().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Locations().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static ChartControllerLinks _ChartControllerLinks = null;
		
		public static ChartControllerLinks Chart(this UrlHelper urlHelper) {
		//	if(_ChartControllerLinks == null) _ChartControllerLinks = new ChartControllerLinks(urlHelper);
		//	return _ChartControllerLinks;
		return new ChartControllerLinks(urlHelper);
		}

		public class ChartControllerUrls{
			
			private UrlHelper _url;

			public ChartControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckCaptcha0(){
				CheckMethod<Specialist.Web.Controllers.ChartController>(c => 
					c.Captcha());
			}
			public string Captcha(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Captcha");
			//	routeValues.Add("controller", "Chart");
								
				return _url.Action("Captcha", "Chart", routeValues);
			}
				

		

		}


			public class ChartControllerLinks{
			
			private UrlHelper _url;

			public ChartControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static ChartControllerUrls _ChartControllerUrls = null;
			public ChartControllerUrls Urls { 
				get {
				//	if(_ChartControllerUrls == null) _ChartControllerUrls = new ChartControllerUrls(_url);
					//return _ChartControllerUrls;
					return new ChartControllerUrls(_url);
				}
			}

		
		
		


			public TagA Captcha(	object content){
				var localActionUrl = _url.Chart().Urls.Captcha();
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static DictionaryControllerLinks _DictionaryControllerLinks = null;
		
		public static DictionaryControllerLinks Dictionary(this UrlHelper urlHelper) {
		//	if(_DictionaryControllerLinks == null) _DictionaryControllerLinks = new DictionaryControllerLinks(urlHelper);
		//	return _DictionaryControllerLinks;
		return new DictionaryControllerLinks(urlHelper);
		}

		public class DictionaryControllerUrls{
			
			private UrlHelper _url;

			public DictionaryControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDefinition0(){
				CheckMethod<Specialist.Web.Controllers.DictionaryController>(c => 
					c.Definition((System.String)null));
			}
			public string Definition(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Definition");
			//	routeValues.Add("controller", "Dictionary");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Definition", "Dictionary", routeValues);
			}
					void TypeCheckList1(){
				CheckMethod<Specialist.Web.Controllers.DictionaryController>(c => 
					c.List());
			}
			public string List(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "Dictionary");
								
				return _url.Action("List", "Dictionary", routeValues);
			}
					void TypeCheckRedirectToAction2(){
				CheckMethod<Specialist.Web.Controllers.DictionaryController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Dictionary");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Dictionary", routeValues);
			}
					void TypeCheckRedirectBack3(){
				CheckMethod<Specialist.Web.Controllers.DictionaryController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Dictionary");
								
				return _url.Action("RedirectBack", "Dictionary", routeValues);
			}
					void TypeCheckMView4(){
				CheckMethod<Specialist.Web.Controllers.DictionaryController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Dictionary");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Dictionary", routeValues);
			}
				

		

		}


			public class DictionaryControllerLinks{
			
			private UrlHelper _url;

			public DictionaryControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static DictionaryControllerUrls _DictionaryControllerUrls = null;
			public DictionaryControllerUrls Urls { 
				get {
				//	if(_DictionaryControllerUrls == null) _DictionaryControllerUrls = new DictionaryControllerUrls(_url);
					//return _DictionaryControllerUrls;
					return new DictionaryControllerUrls(_url);
				}
			}

		
		
		


			public TagA Definition(	System.String urlName, object content){
				var localActionUrl = _url.Dictionary().Urls.Definition(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA List(	object content){
				var localActionUrl = _url.Dictionary().Urls.List();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Dictionary().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Dictionary().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Dictionary().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static MasterPageControllerLinks _MasterPageControllerLinks = null;
		
		public static MasterPageControllerLinks MasterPage(this UrlHelper urlHelper) {
		//	if(_MasterPageControllerLinks == null) _MasterPageControllerLinks = new MasterPageControllerLinks(urlHelper);
		//	return _MasterPageControllerLinks;
		return new MasterPageControllerLinks(urlHelper);
		}

		public class MasterPageControllerUrls{
			
			private UrlHelper _url;

			public MasterPageControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckFooter0(){
				CheckMethod<Specialist.Web.Controllers.MasterPageController>(c => 
					c.Footer());
			}
			public string Footer(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Footer");
			//	routeValues.Add("controller", "MasterPage");
								
				return _url.Action("Footer", "MasterPage", routeValues);
			}
					void TypeCheckRedirectToAction1(){
				CheckMethod<Specialist.Web.Controllers.MasterPageController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "MasterPage");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "MasterPage", routeValues);
			}
					void TypeCheckRedirectBack2(){
				CheckMethod<Specialist.Web.Controllers.MasterPageController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "MasterPage");
								
				return _url.Action("RedirectBack", "MasterPage", routeValues);
			}
					void TypeCheckMView3(){
				CheckMethod<Specialist.Web.Controllers.MasterPageController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "MasterPage");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "MasterPage", routeValues);
			}
				

		

		}


			public class MasterPageControllerLinks{
			
			private UrlHelper _url;

			public MasterPageControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static MasterPageControllerUrls _MasterPageControllerUrls = null;
			public MasterPageControllerUrls Urls { 
				get {
				//	if(_MasterPageControllerUrls == null) _MasterPageControllerUrls = new MasterPageControllerUrls(_url);
					//return _MasterPageControllerUrls;
					return new MasterPageControllerUrls(_url);
				}
			}

		
		
		


			public TagA Footer(	object content){
				var localActionUrl = _url.MasterPage().Urls.Footer();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.MasterPage().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.MasterPage().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.MasterPage().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static CompanyFileControllerLinks _CompanyFileControllerLinks = null;
		
		public static CompanyFileControllerLinks CompanyFile(this UrlHelper urlHelper) {
		//	if(_CompanyFileControllerLinks == null) _CompanyFileControllerLinks = new CompanyFileControllerLinks(urlHelper);
		//	return _CompanyFileControllerLinks;
		return new CompanyFileControllerLinks(urlHelper);
		}

		public class CompanyFileControllerUrls{
			
			private UrlHelper _url;

			public CompanyFileControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckList0(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.List());
			}
			public string List(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "CompanyFile");
								
				return _url.Action("List", "CompanyFile", routeValues);
			}
					void TypeCheckAdd1(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.Add());
			}
			public string Add(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Add");
			//	routeValues.Add("controller", "CompanyFile");
								
				return _url.Action("Add", "CompanyFile", routeValues);
			}
					void TypeCheckDelete2(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.Delete(0));
			}
			public string Delete(System.Int32 fileID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Delete");
			//	routeValues.Add("controller", "CompanyFile");
									routeValues.Add("fileID", fileID);
								
				return _url.Action("Delete", "CompanyFile", routeValues);
			}
					void TypeCheckEdit3(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.Edit(0));
			}
			public string Edit(System.Int32 fileID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Edit");
			//	routeValues.Add("controller", "CompanyFile");
									routeValues.Add("fileID", fileID);
								
				return _url.Action("Edit", "CompanyFile", routeValues);
			}
					void TypeCheckEdit4(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.Edit((Specialist.Entities.Profile.ViewModel.CompanyFileVM)null));
			}
			public string Edit(Specialist.Entities.Profile.ViewModel.CompanyFileVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Edit");
			//	routeValues.Add("controller", "CompanyFile");
									routeValues.Add("model", model);
								
				return _url.Action("Edit", "CompanyFile", routeValues);
			}
					void TypeCheckBaseView5(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "CompanyFile");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "CompanyFile", routeValues);
			}
					void TypeCheckBaseView6(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "CompanyFile");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "CompanyFile", routeValues);
			}
					void TypeCheckBaseViewWithTitle7(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "CompanyFile");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "CompanyFile", routeValues);
			}
					void TypeCheckRedirectToAction8(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "CompanyFile");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "CompanyFile", routeValues);
			}
					void TypeCheckRedirectBack9(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "CompanyFile");
								
				return _url.Action("RedirectBack", "CompanyFile", routeValues);
			}
					void TypeCheckMView10(){
				CheckMethod<Specialist.Web.Controllers.CompanyFileController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "CompanyFile");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "CompanyFile", routeValues);
			}
				

		

		}


			public class CompanyFileControllerLinks{
			
			private UrlHelper _url;

			public CompanyFileControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static CompanyFileControllerUrls _CompanyFileControllerUrls = null;
			public CompanyFileControllerUrls Urls { 
				get {
				//	if(_CompanyFileControllerUrls == null) _CompanyFileControllerUrls = new CompanyFileControllerUrls(_url);
					//return _CompanyFileControllerUrls;
					return new CompanyFileControllerUrls(_url);
				}
			}

		
		
		


			public TagA List(	object content){
				var localActionUrl = _url.CompanyFile().Urls.List();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Add(	object content){
				var localActionUrl = _url.CompanyFile().Urls.Add();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Delete(	System.Int32 fileID, object content){
				var localActionUrl = _url.CompanyFile().Urls.Delete(fileID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Edit(	System.Int32 fileID, object content){
				var localActionUrl = _url.CompanyFile().Urls.Edit(fileID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Edit(	Specialist.Entities.Profile.ViewModel.CompanyFileVM model, object content){
				var localActionUrl = _url.CompanyFile().Urls.Edit(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.CompanyFile().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.CompanyFile().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.CompanyFile().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.CompanyFile().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.CompanyFile().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.CompanyFile().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static GraduateControllerLinks _GraduateControllerLinks = null;
		
		public static GraduateControllerLinks Graduate(this UrlHelper urlHelper) {
		//	if(_GraduateControllerLinks == null) _GraduateControllerLinks = new GraduateControllerLinks(urlHelper);
		//	return _GraduateControllerLinks;
		return new GraduateControllerLinks(urlHelper);
		}

		public class GraduateControllerUrls{
			
			private UrlHelper _url;

			public GraduateControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDownloadBest0(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.DownloadBest((System.String)null));
			}
			public string DownloadBest(System.String bestName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadBest");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("bestName", bestName);
								
				return _url.Action("DownloadBest", "Graduate", routeValues);
			}
					void TypeCheckGroupCert1(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.GroupCert(0));
			}
			public string GroupCert(System.Decimal sigId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupCert");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("sigId", sigId);
								
				return _url.Action("GroupCert", "Graduate", routeValues);
			}
					void TypeCheckGroupEnd2(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.GroupEnd((System.String)null));
			}
			public string GroupEnd(System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupEnd");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("GroupEnd", "Graduate", routeValues);
			}
					void TypeCheckDownloadGroupEnd3(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.DownloadGroupEnd((System.String)null));
			}
			public string DownloadGroupEnd(System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadGroupEnd");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("DownloadGroupEnd", "Graduate", routeValues);
			}
					void TypeCheckDownloadCert4(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.DownloadCert(0, false, false, false));
			}
			public string DownloadCert(System.Decimal sigId, System.Boolean eng, System.Boolean vendor, System.Boolean ru){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadCert");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("sigId", sigId);
									routeValues.Add("eng", eng);
									routeValues.Add("vendor", vendor);
									routeValues.Add("ru", ru);
								
				return _url.Action("DownloadCert", "Graduate", routeValues);
			}
					void TypeCheckDownloadGraduateCertificate5(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.DownloadGraduateCertificate((System.String)null, (System.String)null));
			}
			public string DownloadGraduateCertificate(System.String name, System.String fullName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadGraduateCertificate");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("name", name);
									routeValues.Add("fullName", fullName);
								
				return _url.Action("DownloadGraduateCertificate", "Graduate", routeValues);
			}
					void TypeCheckWebinar20116(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.Webinar2011((System.String)null));
			}
			public string Webinar2011(System.String id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Webinar2011");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("id", id);
								
				return _url.Action("Webinar2011", "Graduate", routeValues);
			}
					void TypeCheckBest20167(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.Best2016(0));
			}
			public string Best2016(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Best2016");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("id", id);
								
				return _url.Action("Best2016", "Graduate", routeValues);
			}
					void TypeCheckBest20118(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.Best2011((System.String)null));
			}
			public string Best2011(System.String id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Best2011");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("id", id);
								
				return _url.Action("Best2011", "Graduate", routeValues);
			}
					void TypeCheckBest9(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.Best());
			}
			public string Best(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Best");
			//	routeValues.Add("controller", "Graduate");
								
				return _url.Action("Best", "Graduate", routeValues);
			}
					void TypeCheckRealSpecialist10(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.RealSpecialist());
			}
			public string RealSpecialist(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RealSpecialist");
			//	routeValues.Add("controller", "Graduate");
								
				return _url.Action("RealSpecialist", "Graduate", routeValues);
			}
					void TypeCheckRealAvatar11(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.RealAvatar());
			}
			public string RealAvatar(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RealAvatar");
			//	routeValues.Add("controller", "Graduate");
								
				return _url.Action("RealAvatar", "Graduate", routeValues);
			}
					void TypeCheckBestAvatar12(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.BestAvatar());
			}
			public string BestAvatar(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BestAvatar");
			//	routeValues.Add("controller", "Graduate");
								
				return _url.Action("BestAvatar", "Graduate", routeValues);
			}
					void TypeCheckUploadBest13(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.UploadBest((System.String)null, (System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase>)null));
			}
			public string UploadBest(System.String bestName, System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase> userfile){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UploadBest");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("bestName", bestName);
									routeValues.Add("userfile", userfile);
								
				return _url.Action("UploadBest", "Graduate", routeValues);
			}
					void TypeCheckCertificateValidation14(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.CertificateValidation());
			}
			public string CertificateValidation(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CertificateValidation");
			//	routeValues.Add("controller", "Graduate");
								
				return _url.Action("CertificateValidation", "Graduate", routeValues);
			}
					void TypeCheckGroupCerts15(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.GroupCerts());
			}
			public string GroupCerts(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupCerts");
			//	routeValues.Add("controller", "Graduate");
								
				return _url.Action("GroupCerts", "Graduate", routeValues);
			}
					void TypeCheckCertificateValidation16(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.CertificateValidation((Specialist.Web.Root.Learning.ViewModels.CertificateValidationVM)null));
			}
			public string CertificateValidation(Specialist.Web.Root.Learning.ViewModels.CertificateValidationVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CertificateValidation");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("model", model);
								
				return _url.Action("CertificateValidation", "Graduate", routeValues);
			}
					void TypeCheckBaseView17(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Graduate", routeValues);
			}
					void TypeCheckBaseView18(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Graduate", routeValues);
			}
					void TypeCheckBaseViewWithTitle19(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Graduate", routeValues);
			}
					void TypeCheckRedirectToAction20(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Graduate", routeValues);
			}
					void TypeCheckRedirectBack21(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Graduate");
								
				return _url.Action("RedirectBack", "Graduate", routeValues);
			}
					void TypeCheckMView22(){
				CheckMethod<Specialist.Web.Controllers.GraduateController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Graduate");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Graduate", routeValues);
			}
				

		

		}


			public class GraduateControllerLinks{
			
			private UrlHelper _url;

			public GraduateControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static GraduateControllerUrls _GraduateControllerUrls = null;
			public GraduateControllerUrls Urls { 
				get {
				//	if(_GraduateControllerUrls == null) _GraduateControllerUrls = new GraduateControllerUrls(_url);
					//return _GraduateControllerUrls;
					return new GraduateControllerUrls(_url);
				}
			}

		
		
		


			public TagA DownloadBest(	System.String bestName, object content){
				var localActionUrl = _url.Graduate().Urls.DownloadBest(bestName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupCert(	System.Decimal sigId, object content){
				var localActionUrl = _url.Graduate().Urls.GroupCert(sigId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupEnd(	System.String courseTC, object content){
				var localActionUrl = _url.Graduate().Urls.GroupEnd(courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DownloadGroupEnd(	System.String courseTC, object content){
				var localActionUrl = _url.Graduate().Urls.DownloadGroupEnd(courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DownloadCert(	System.Decimal sigId, System.Boolean eng, System.Boolean vendor, System.Boolean ru, object content){
				var localActionUrl = _url.Graduate().Urls.DownloadCert(sigId, eng, vendor, ru);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DownloadGraduateCertificate(	System.String name, System.String fullName, object content){
				var localActionUrl = _url.Graduate().Urls.DownloadGraduateCertificate(name, fullName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Webinar2011(	System.String id, object content){
				var localActionUrl = _url.Graduate().Urls.Webinar2011(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Best2016(	System.Decimal id, object content){
				var localActionUrl = _url.Graduate().Urls.Best2016(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Best2011(	System.String id, object content){
				var localActionUrl = _url.Graduate().Urls.Best2011(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Best(	object content){
				var localActionUrl = _url.Graduate().Urls.Best();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RealSpecialist(	object content){
				var localActionUrl = _url.Graduate().Urls.RealSpecialist();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RealAvatar(	object content){
				var localActionUrl = _url.Graduate().Urls.RealAvatar();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BestAvatar(	object content){
				var localActionUrl = _url.Graduate().Urls.BestAvatar();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UploadBest(	System.String bestName, System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase> userfile, object content){
				var localActionUrl = _url.Graduate().Urls.UploadBest(bestName, userfile);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CertificateValidation(	object content){
				var localActionUrl = _url.Graduate().Urls.CertificateValidation();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupCerts(	object content){
				var localActionUrl = _url.Graduate().Urls.GroupCerts();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CertificateValidation(	Specialist.Web.Root.Learning.ViewModels.CertificateValidationVM model, object content){
				var localActionUrl = _url.Graduate().Urls.CertificateValidation(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Graduate().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Graduate().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Graduate().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Graduate().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Graduate().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Graduate().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static FileControllerLinks _FileControllerLinks = null;
		
		public static FileControllerLinks File(this UrlHelper urlHelper) {
		//	if(_FileControllerLinks == null) _FileControllerLinks = new FileControllerLinks(urlHelper);
		//	return _FileControllerLinks;
		return new FileControllerLinks(urlHelper);
		}

		public class FileControllerUrls{
			
			private UrlHelper _url;

			public FileControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckList0(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.List((System.Nullable<System.Int32>)null));
			}
			public string List(System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "File");
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("List", "File", routeValues);
			}
					void TypeCheckAdd1(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.Add());
			}
			public string Add(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Add");
			//	routeValues.Add("controller", "File");
								
				return _url.Action("Add", "File", routeValues);
			}
					void TypeCheckAddFileTo2(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.AddFileTo((Specialist.Entities.Profile.ViewModel.FileListVM)null));
			}
			public string AddFileTo(Specialist.Entities.Profile.ViewModel.FileListVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddFileTo");
			//	routeValues.Add("controller", "File");
									routeValues.Add("model", model);
								
				return _url.Action("AddFileTo", "File", routeValues);
			}
					void TypeCheckDeleteFileFrom3(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.DeleteFileFrom(0, (System.String)null, (System.Nullable<System.Decimal>)null));
			}
			public string DeleteFileFrom(System.Int32 fileID, System.String courseTC, System.Nullable<System.Decimal> groupID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteFileFrom");
			//	routeValues.Add("controller", "File");
									routeValues.Add("fileID", fileID);
									routeValues.Add("courseTC", courseTC);
									routeValues.Add("groupID", groupID);
								
				return _url.Action("DeleteFileFrom", "File", routeValues);
			}
					void TypeCheckDelete4(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.Delete(0));
			}
			public string Delete(System.Int32 fileID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Delete");
			//	routeValues.Add("controller", "File");
									routeValues.Add("fileID", fileID);
								
				return _url.Action("Delete", "File", routeValues);
			}
					void TypeCheckEdit5(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.Edit(0));
			}
			public string Edit(System.Int32 fileID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Edit");
			//	routeValues.Add("controller", "File");
									routeValues.Add("fileID", fileID);
								
				return _url.Action("Edit", "File", routeValues);
			}
					void TypeCheckEdit6(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.Edit((Specialist.Entities.Profile.ViewModel.UserFileVM)null));
			}
			public string Edit(Specialist.Entities.Profile.ViewModel.UserFileVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Edit");
			//	routeValues.Add("controller", "File");
									routeValues.Add("model", model);
								
				return _url.Action("Edit", "File", routeValues);
			}
					void TypeCheckAddLectureFile7(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.AddLectureFile(0));
			}
			public string AddLectureFile(System.Decimal lectureId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddLectureFile");
			//	routeValues.Add("controller", "File");
									routeValues.Add("lectureId", lectureId);
								
				return _url.Action("AddLectureFile", "File", routeValues);
			}
					void TypeCheckBaseView8(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "File");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "File", routeValues);
			}
					void TypeCheckBaseView9(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "File");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "File", routeValues);
			}
					void TypeCheckBaseViewWithTitle10(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "File");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "File", routeValues);
			}
					void TypeCheckRedirectToAction11(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "File");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "File", routeValues);
			}
					void TypeCheckRedirectBack12(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "File");
								
				return _url.Action("RedirectBack", "File", routeValues);
			}
					void TypeCheckMView13(){
				CheckMethod<Specialist.Web.Controllers.FileController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "File");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "File", routeValues);
			}
				

		

		}


			public class FileControllerLinks{
			
			private UrlHelper _url;

			public FileControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static FileControllerUrls _FileControllerUrls = null;
			public FileControllerUrls Urls { 
				get {
				//	if(_FileControllerUrls == null) _FileControllerUrls = new FileControllerUrls(_url);
					//return _FileControllerUrls;
					return new FileControllerUrls(_url);
				}
			}

		
		
		


			public TagA List(	System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.File().Urls.List(pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Add(	object content){
				var localActionUrl = _url.File().Urls.Add();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddFileTo(	Specialist.Entities.Profile.ViewModel.FileListVM model, object content){
				var localActionUrl = _url.File().Urls.AddFileTo(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteFileFrom(	System.Int32 fileID, System.String courseTC, System.Nullable<System.Decimal> groupID, object content){
				var localActionUrl = _url.File().Urls.DeleteFileFrom(fileID, courseTC, groupID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Delete(	System.Int32 fileID, object content){
				var localActionUrl = _url.File().Urls.Delete(fileID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Edit(	System.Int32 fileID, object content){
				var localActionUrl = _url.File().Urls.Edit(fileID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Edit(	Specialist.Entities.Profile.ViewModel.UserFileVM model, object content){
				var localActionUrl = _url.File().Urls.Edit(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddLectureFile(	System.Decimal lectureId, object content){
				var localActionUrl = _url.File().Urls.AddLectureFile(lectureId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.File().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.File().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.File().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.File().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.File().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.File().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static OrgProfileControllerLinks _OrgProfileControllerLinks = null;
		
		public static OrgProfileControllerLinks OrgProfile(this UrlHelper urlHelper) {
		//	if(_OrgProfileControllerLinks == null) _OrgProfileControllerLinks = new OrgProfileControllerLinks(urlHelper);
		//	return _OrgProfileControllerLinks;
		return new OrgProfileControllerLinks(urlHelper);
		}

		public class OrgProfileControllerUrls{
			
			private UrlHelper _url;

			public OrgProfileControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckStudent0(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.Student(0));
			}
			public string Student(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Student");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("id", id);
								
				return _url.Action("Student", "OrgProfile", routeValues);
			}
					void TypeCheckGroup1(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.Group(0));
			}
			public string Group(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Group");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("id", id);
								
				return _url.Action("Group", "OrgProfile", routeValues);
			}
					void TypeCheckGroupSearch2(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.GroupSearch(false));
			}
			public string GroupSearch(System.Boolean today){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupSearch");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("today", today);
								
				return _url.Action("GroupSearch", "OrgProfile", routeValues);
			}
					void TypeCheckGroupSearchPost3(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.GroupSearchPost((Specialist.Web.Root.Profile.ViewModels.OrgGroupSearchVM)null));
			}
			public string GroupSearchPost(Specialist.Web.Root.Profile.ViewModels.OrgGroupSearchVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupSearchPost");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("model", model);
								
				return _url.Action("GroupSearchPost", "OrgProfile", routeValues);
			}
					void TypeCheckGetStudentsAuto4(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.GetStudentsAuto((System.String)null));
			}
			public string GetStudentsAuto(System.String term){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetStudentsAuto");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("term", term);
								
				return _url.Action("GetStudentsAuto", "OrgProfile", routeValues);
			}
					void TypeCheckGetCoursesAuto5(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.GetCoursesAuto((System.String)null));
			}
			public string GetCoursesAuto(System.String term){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetCoursesAuto");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("term", term);
								
				return _url.Action("GetCoursesAuto", "OrgProfile", routeValues);
			}
					void TypeCheckStatusUpdate6(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.StatusUpdate());
			}
			public string StatusUpdate(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "StatusUpdate");
			//	routeValues.Add("controller", "OrgProfile");
								
				return _url.Action("StatusUpdate", "OrgProfile", routeValues);
			}
					void TypeCheckStatusUpdatePost7(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.StatusUpdatePost((Specialist.Web.Root.Profile.ViewModels.OrgStatusUpdateVM)null));
			}
			public string StatusUpdatePost(Specialist.Web.Root.Profile.ViewModels.OrgStatusUpdateVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "StatusUpdatePost");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("model", model);
								
				return _url.Action("StatusUpdatePost", "OrgProfile", routeValues);
			}
					void TypeCheckNextCourseOrder8(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.NextCourseOrder(0, 0));
			}
			public string NextCourseOrder(System.Decimal studentId, System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "NextCourseOrder");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("studentId", studentId);
									routeValues.Add("groupId", groupId);
								
				return _url.Action("NextCourseOrder", "OrgProfile", routeValues);
			}
					void TypeCheckQuestionnaire9(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.Questionnaire(0, 0));
			}
			public string Questionnaire(System.Decimal studentId, System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Questionnaire");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("studentId", studentId);
									routeValues.Add("groupId", groupId);
								
				return _url.Action("Questionnaire", "OrgProfile", routeValues);
			}
					void TypeCheckAttendance10(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.Attendance(0, 0));
			}
			public string Attendance(System.Decimal studentId, System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Attendance");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("studentId", studentId);
									routeValues.Add("groupId", groupId);
								
				return _url.Action("Attendance", "OrgProfile", routeValues);
			}
					void TypeCheckRealSpecialist11(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.RealSpecialist());
			}
			public string RealSpecialist(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RealSpecialist");
			//	routeValues.Add("controller", "OrgProfile");
								
				return _url.Action("RealSpecialist", "OrgProfile", routeValues);
			}
					void TypeCheckFiles12(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.Files(0));
			}
			public string Files(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Files");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("id", id);
								
				return _url.Action("Files", "OrgProfile", routeValues);
			}
					void TypeCheckList13(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.List());
			}
			public string List(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "OrgProfile");
								
				return _url.Action("List", "OrgProfile", routeValues);
			}
					void TypeCheckDownloadOrders14(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.DownloadOrders());
			}
			public string DownloadOrders(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadOrders");
			//	routeValues.Add("controller", "OrgProfile");
								
				return _url.Action("DownloadOrders", "OrgProfile", routeValues);
			}
					void TypeCheckBaseView15(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "OrgProfile", routeValues);
			}
					void TypeCheckBaseView16(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "OrgProfile", routeValues);
			}
					void TypeCheckBaseViewWithTitle17(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "OrgProfile", routeValues);
			}
					void TypeCheckRedirectToAction18(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "OrgProfile", routeValues);
			}
					void TypeCheckRedirectBack19(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "OrgProfile");
								
				return _url.Action("RedirectBack", "OrgProfile", routeValues);
			}
					void TypeCheckMView20(){
				CheckMethod<Specialist.Web.Controllers.OrgProfileController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "OrgProfile");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "OrgProfile", routeValues);
			}
				

		

		}


			public class OrgProfileControllerLinks{
			
			private UrlHelper _url;

			public OrgProfileControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static OrgProfileControllerUrls _OrgProfileControllerUrls = null;
			public OrgProfileControllerUrls Urls { 
				get {
				//	if(_OrgProfileControllerUrls == null) _OrgProfileControllerUrls = new OrgProfileControllerUrls(_url);
					//return _OrgProfileControllerUrls;
					return new OrgProfileControllerUrls(_url);
				}
			}

		
		
		


			public TagA Student(	System.Decimal id, object content){
				var localActionUrl = _url.OrgProfile().Urls.Student(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Group(	System.Decimal id, object content){
				var localActionUrl = _url.OrgProfile().Urls.Group(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupSearch(	System.Boolean today, object content){
				var localActionUrl = _url.OrgProfile().Urls.GroupSearch(today);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupSearchPost(	Specialist.Web.Root.Profile.ViewModels.OrgGroupSearchVM model, object content){
				var localActionUrl = _url.OrgProfile().Urls.GroupSearchPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetStudentsAuto(	System.String term, object content){
				var localActionUrl = _url.OrgProfile().Urls.GetStudentsAuto(term);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetCoursesAuto(	System.String term, object content){
				var localActionUrl = _url.OrgProfile().Urls.GetCoursesAuto(term);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA StatusUpdate(	object content){
				var localActionUrl = _url.OrgProfile().Urls.StatusUpdate();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA StatusUpdatePost(	Specialist.Web.Root.Profile.ViewModels.OrgStatusUpdateVM model, object content){
				var localActionUrl = _url.OrgProfile().Urls.StatusUpdatePost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA NextCourseOrder(	System.Decimal studentId, System.Decimal groupId, object content){
				var localActionUrl = _url.OrgProfile().Urls.NextCourseOrder(studentId, groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Questionnaire(	System.Decimal studentId, System.Decimal groupId, object content){
				var localActionUrl = _url.OrgProfile().Urls.Questionnaire(studentId, groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Attendance(	System.Decimal studentId, System.Decimal groupId, object content){
				var localActionUrl = _url.OrgProfile().Urls.Attendance(studentId, groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RealSpecialist(	object content){
				var localActionUrl = _url.OrgProfile().Urls.RealSpecialist();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Files(	System.Int32 id, object content){
				var localActionUrl = _url.OrgProfile().Urls.Files(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA List(	object content){
				var localActionUrl = _url.OrgProfile().Urls.List();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DownloadOrders(	object content){
				var localActionUrl = _url.OrgProfile().Urls.DownloadOrders();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.OrgProfile().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.OrgProfile().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.OrgProfile().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.OrgProfile().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.OrgProfile().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.OrgProfile().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static OrgTestControllerLinks _OrgTestControllerLinks = null;
		
		public static OrgTestControllerLinks OrgTest(this UrlHelper urlHelper) {
		//	if(_OrgTestControllerLinks == null) _OrgTestControllerLinks = new OrgTestControllerLinks(urlHelper);
		//	return _OrgTestControllerLinks;
		return new OrgTestControllerLinks(urlHelper);
		}

		public class OrgTestControllerUrls{
			
			private UrlHelper _url;

			public OrgTestControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckTests0(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.Tests());
			}
			public string Tests(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Tests");
			//	routeValues.Add("controller", "OrgTest");
								
				return _url.Action("Tests", "OrgTest", routeValues);
			}
					void TypeCheckResults1(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.Results(0));
			}
			public string Results(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Results");
			//	routeValues.Add("controller", "OrgTest");
									routeValues.Add("id", id);
								
				return _url.Action("Results", "OrgTest", routeValues);
			}
					void TypeCheckDownloadResults2(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.DownloadResults(0));
			}
			public string DownloadResults(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadResults");
			//	routeValues.Add("controller", "OrgTest");
									routeValues.Add("id", id);
								
				return _url.Action("DownloadResults", "OrgTest", routeValues);
			}
					void TypeCheckBaseView3(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "OrgTest");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "OrgTest", routeValues);
			}
					void TypeCheckBaseView4(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "OrgTest");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "OrgTest", routeValues);
			}
					void TypeCheckBaseViewWithTitle5(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "OrgTest");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "OrgTest", routeValues);
			}
					void TypeCheckRedirectToAction6(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "OrgTest");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "OrgTest", routeValues);
			}
					void TypeCheckRedirectBack7(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "OrgTest");
								
				return _url.Action("RedirectBack", "OrgTest", routeValues);
			}
					void TypeCheckMView8(){
				CheckMethod<Specialist.Web.Controllers.OrgTestController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "OrgTest");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "OrgTest", routeValues);
			}
				

		

		}


			public class OrgTestControllerLinks{
			
			private UrlHelper _url;

			public OrgTestControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static OrgTestControllerUrls _OrgTestControllerUrls = null;
			public OrgTestControllerUrls Urls { 
				get {
				//	if(_OrgTestControllerUrls == null) _OrgTestControllerUrls = new OrgTestControllerUrls(_url);
					//return _OrgTestControllerUrls;
					return new OrgTestControllerUrls(_url);
				}
			}

		
		
		


			public TagA Tests(	object content){
				var localActionUrl = _url.OrgTest().Urls.Tests();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Results(	System.Int32 id, object content){
				var localActionUrl = _url.OrgTest().Urls.Results(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DownloadResults(	System.Int32 id, object content){
				var localActionUrl = _url.OrgTest().Urls.DownloadResults(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.OrgTest().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.OrgTest().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.OrgTest().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.OrgTest().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.OrgTest().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.OrgTest().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static SimpleRegControllerLinks _SimpleRegControllerLinks = null;
		
		public static SimpleRegControllerLinks SimpleReg(this UrlHelper urlHelper) {
		//	if(_SimpleRegControllerLinks == null) _SimpleRegControllerLinks = new SimpleRegControllerLinks(urlHelper);
		//	return _SimpleRegControllerLinks;
		return new SimpleRegControllerLinks(urlHelper);
		}

		public class SimpleRegControllerUrls{
			
			private UrlHelper _url;

			public SimpleRegControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckRegistration0(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.Registration((System.String)null, null));
			}
			public string Registration(System.String returnUrl){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Registration");
			//	routeValues.Add("controller", "SimpleReg");
									routeValues.Add("returnUrl", returnUrl);
								
				return _url.Action("Registration", "SimpleReg", routeValues);
			}
					void TypeCheckRegistrationPost1(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.RegistrationPost((Specialist.Entities.Passport.SimpleRegUser)null));
			}
			public string RegistrationPost(Specialist.Entities.Passport.SimpleRegUser user){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RegistrationPost");
			//	routeValues.Add("controller", "SimpleReg");
									routeValues.Add("user", user);
								
				return _url.Action("RegistrationPost", "SimpleReg", routeValues);
			}
					void TypeCheckEmailConfirm2(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.EmailConfirm((System.String)null));
			}
			public string EmailConfirm(System.String id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EmailConfirm");
			//	routeValues.Add("controller", "SimpleReg");
									routeValues.Add("id", id);
								
				return _url.Action("EmailConfirm", "SimpleReg", routeValues);
			}
					void TypeCheckBaseView3(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "SimpleReg");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "SimpleReg", routeValues);
			}
					void TypeCheckBaseView4(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "SimpleReg");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "SimpleReg", routeValues);
			}
					void TypeCheckBaseViewWithTitle5(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "SimpleReg");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "SimpleReg", routeValues);
			}
					void TypeCheckRedirectToAction6(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "SimpleReg");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "SimpleReg", routeValues);
			}
					void TypeCheckRedirectBack7(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "SimpleReg");
								
				return _url.Action("RedirectBack", "SimpleReg", routeValues);
			}
					void TypeCheckMView8(){
				CheckMethod<Specialist.Web.Controllers.SimpleRegController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "SimpleReg");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "SimpleReg", routeValues);
			}
				

		

		}


			public class SimpleRegControllerLinks{
			
			private UrlHelper _url;

			public SimpleRegControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static SimpleRegControllerUrls _SimpleRegControllerUrls = null;
			public SimpleRegControllerUrls Urls { 
				get {
				//	if(_SimpleRegControllerUrls == null) _SimpleRegControllerUrls = new SimpleRegControllerUrls(_url);
					//return _SimpleRegControllerUrls;
					return new SimpleRegControllerUrls(_url);
				}
			}

		
		
		


			public TagA Registration(	System.String returnUrl, object content){
				var localActionUrl = _url.SimpleReg().Urls.Registration(returnUrl);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RegistrationPost(	Specialist.Entities.Passport.SimpleRegUser user, object content){
				var localActionUrl = _url.SimpleReg().Urls.RegistrationPost(user);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EmailConfirm(	System.String id, object content){
				var localActionUrl = _url.SimpleReg().Urls.EmailConfirm(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.SimpleReg().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.SimpleReg().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.SimpleReg().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.SimpleReg().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.SimpleReg().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.SimpleReg().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static ProfileControllerLinks _ProfileControllerLinks = null;
		
		public static ProfileControllerLinks Profile(this UrlHelper urlHelper) {
		//	if(_ProfileControllerLinks == null) _ProfileControllerLinks = new ProfileControllerLinks(urlHelper);
		//	return _ProfileControllerLinks;
		return new ProfileControllerLinks(urlHelper);
		}

		public class ProfileControllerUrls{
			
			private UrlHelper _url;

			public ProfileControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckExamQuestionnaire0(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ExamQuestionnaire((System.String)null));
			}
			public string ExamQuestionnaire(System.String nextUrl){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ExamQuestionnaire");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("nextUrl", nextUrl);
								
				return _url.Action("ExamQuestionnaire", "Profile", routeValues);
			}
					void TypeCheckExamQuestionnaire1(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ExamQuestionnaire((Specialist.Entities.Passport.UserExamQuestionnaire)null, (System.String)null));
			}
			public string ExamQuestionnaire(Specialist.Entities.Passport.UserExamQuestionnaire model, System.String nextUrl){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ExamQuestionnaire");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
									routeValues.Add("nextUrl", nextUrl);
								
				return _url.Action("ExamQuestionnaire", "Profile", routeValues);
			}
					void TypeCheckResponses2(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Responses((System.Nullable<System.Int32>)null));
			}
			public string Responses(System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Responses");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("Responses", "Profile", routeValues);
			}
					void TypeCheckCustomerTypeChoice3(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.CustomerTypeChoice((System.String)null));
			}
			public string CustomerTypeChoice(System.String nextUrl){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CustomerTypeChoice");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("nextUrl", nextUrl);
								
				return _url.Action("CustomerTypeChoice", "Profile", routeValues);
			}
					void TypeCheckRegister4(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Register((System.String)null, (System.String)null, (System.String)null));
			}
			public string Register(System.String customerType, System.String nextUrl, System.String token){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Register");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("customerType", customerType);
									routeValues.Add("nextUrl", nextUrl);
									routeValues.Add("token", token);
								
				return _url.Action("Register", "Profile", routeValues);
			}
					void TypeCheckRegisterControl5(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.RegisterControl((System.String)null, (System.String)null));
			}
			public string RegisterControl(System.String nextUrl, System.String customerType){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RegisterControl");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("nextUrl", nextUrl);
									routeValues.Add("customerType", customerType);
								
				return _url.Action("RegisterControl", "Profile", routeValues);
			}
					void TypeCheckRegisterPost6(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.RegisterPost((Specialist.Entities.Passport.ViewModel.RegisterVM)null));
			}
			public string RegisterPost(Specialist.Entities.Passport.ViewModel.RegisterVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RegisterPost");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("RegisterPost", "Profile", routeValues);
			}
					void TypeCheckChangePassword7(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ChangePassword());
			}
			public string ChangePassword(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ChangePassword");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("ChangePassword", "Profile", routeValues);
			}
					void TypeCheckChangePassword8(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ChangePassword((Specialist.Entities.Profile.ViewModel.ChangePasswordVM)null));
			}
			public string ChangePassword(Specialist.Entities.Profile.ViewModel.ChangePasswordVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ChangePassword");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("ChangePassword", "Profile", routeValues);
			}
					void TypeCheckRestorePassword9(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.RestorePassword((System.String)null));
			}
			public string RestorePassword(System.String email){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RestorePassword");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("email", email);
								
				return _url.Action("RestorePassword", "Profile", routeValues);
			}
					void TypeCheckRestorePassword10(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.RestorePassword((Specialist.Entities.Profile.MetaData.RestorePasswordVM)null));
			}
			public string RestorePassword(Specialist.Entities.Profile.MetaData.RestorePasswordVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RestorePassword");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("RestorePassword", "Profile", routeValues);
			}
					void TypeCheckHostingInfo11(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.HostingInfo());
			}
			public string HostingInfo(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "HostingInfo");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("HostingInfo", "Profile", routeValues);
			}
					void TypeCheckLearning12(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Learning());
			}
			public string Learning(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Learning");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("Learning", "Profile", routeValues);
			}
					void TypeCheckClassmates13(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Classmates());
			}
			public string Classmates(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Classmates");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("Classmates", "Profile", routeValues);
			}
					void TypeCheckChangeStatus14(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ChangeStatus());
			}
			public string ChangeStatus(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ChangeStatus");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("ChangeStatus", "Profile", routeValues);
			}
					void TypeCheckChangeStatus15(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ChangeStatus((Specialist.Entities.Profile.ViewModel.ChangeStatusVM)null));
			}
			public string ChangeStatus(Specialist.Entities.Profile.ViewModel.ChangeStatusVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ChangeStatus");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("ChangeStatus", "Profile", routeValues);
			}
					void TypeCheckSubscribes16(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Subscribes());
			}
			public string Subscribes(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Subscribes");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("Subscribes", "Profile", routeValues);
			}
					void TypeCheckSubscribes17(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Subscribes((Specialist.Entities.Profile.ViewModel.SubscribesVM)null));
			}
			public string Subscribes(Specialist.Entities.Profile.ViewModel.SubscribesVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Subscribes");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("Subscribes", "Profile", routeValues);
			}
					void TypeCheckLibrary18(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Library());
			}
			public string Library(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Library");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("Library", "Profile", routeValues);
			}
					void TypeCheckTests19(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Tests(0));
			}
			public string Tests(System.Int32 index){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Tests");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("index", index);
								
				return _url.Action("Tests", "Profile", routeValues);
			}
					void TypeCheckSurvey20(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Survey(0));
			}
			public string Survey(System.Int32 sigId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Survey");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("sigId", sigId);
								
				return _url.Action("Survey", "Profile", routeValues);
			}
					void TypeCheckSurvey21(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Survey((Specialist.Entities.Secondary.QuestionAnswer)null));
			}
			public string Survey(Specialist.Entities.Secondary.QuestionAnswer model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Survey");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("Survey", "Profile", routeValues);
			}
					void TypeCheckGroupSurvey22(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.GroupSurvey(0, 0));
			}
			public string GroupSurvey(System.Decimal studentId, System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupSurvey");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("studentId", studentId);
									routeValues.Add("groupId", groupId);
								
				return _url.Action("GroupSurvey", "Profile", routeValues);
			}
					void TypeCheckGroupSurvey23(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.GroupSurvey((Specialist.Entities.Secondary.GroupSurvey)null));
			}
			public string GroupSurvey(Specialist.Entities.Secondary.GroupSurvey model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupSurvey");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("GroupSurvey", "Profile", routeValues);
			}
					void TypeCheckExcelMaster24(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ExcelMaster());
			}
			public string ExcelMaster(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ExcelMaster");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("ExcelMaster", "Profile", routeValues);
			}
					void TypeCheckMyBusiness25(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.MyBusiness());
			}
			public string MyBusiness(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MyBusiness");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("MyBusiness", "Profile", routeValues);
			}
					void TypeCheckMyBusiness26(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.MyBusiness((Specialist.Web.Root.Profile.Logic.MyBusinessUser)null));
			}
			public string MyBusiness(Specialist.Web.Root.Profile.Logic.MyBusinessUser model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MyBusiness");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("MyBusiness", "Profile", routeValues);
			}
					void TypeCheckChangeNameRequest27(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ChangeNameRequest());
			}
			public string ChangeNameRequest(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ChangeNameRequest");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("ChangeNameRequest", "Profile", routeValues);
			}
					void TypeCheckChangeNameRequest28(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ChangeNameRequest((Specialist.Entities.Profile.ViewModel.Common.ChangeNameRequestVM)null));
			}
			public string ChangeNameRequest(Specialist.Entities.Profile.ViewModel.Common.ChangeNameRequestVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ChangeNameRequest");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("ChangeNameRequest", "Profile", routeValues);
			}
					void TypeCheckChangeName29(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ChangeName(0, (System.String)null, (System.String)null, (System.String)null));
			}
			public string ChangeName(System.Int32 userId, System.String firstName, System.String secondName, System.String lastName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ChangeName");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("userId", userId);
									routeValues.Add("firstName", firstName);
									routeValues.Add("secondName", secondName);
									routeValues.Add("lastName", lastName);
								
				return _url.Action("ChangeName", "Profile", routeValues);
			}
					void TypeCheckWorkPlace30(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.WorkPlace());
			}
			public string WorkPlace(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "WorkPlace");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("WorkPlace", "Profile", routeValues);
			}
					void TypeCheckWorkPlacePost31(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.WorkPlacePost((Specialist.Entities.Passport.WorkPlace)null));
			}
			public string WorkPlacePost(Specialist.Entities.Passport.WorkPlace model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "WorkPlacePost");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("WorkPlacePost", "Profile", routeValues);
			}
					void TypeCheckSocialConnect32(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.SocialConnect());
			}
			public string SocialConnect(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SocialConnect");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("SocialConnect", "Profile", routeValues);
			}
					void TypeCheckSocialConnects33(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.SocialConnects());
			}
			public string SocialConnects(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SocialConnects");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("SocialConnects", "Profile", routeValues);
			}
					void TypeCheckLinkFacebook34(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.LinkFacebook((System.String)null));
			}
			public string LinkFacebook(System.String token){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "LinkFacebook");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("token", token);
								
				return _url.Action("LinkFacebook", "Profile", routeValues);
			}
					void TypeCheckFacebookLogin35(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.FacebookLogin((System.String)null, (System.String)null));
			}
			public string FacebookLogin(System.String token, System.String returnUrl){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "FacebookLogin");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("token", token);
									routeValues.Add("returnUrl", returnUrl);
								
				return _url.Action("FacebookLogin", "Profile", routeValues);
			}
					void TypeCheckFaceBookAccessToken36(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.FaceBookAccessToken((System.String)null));
			}
			public string FaceBookAccessToken(System.String token){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "FaceBookAccessToken");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("token", token);
								
				return _url.Action("FaceBookAccessToken", "Profile", routeValues);
			}
					void TypeCheckVKontakteAccessToken37(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.VKontakteAccessToken((System.String)null));
			}
			public string VKontakteAccessToken(System.String tokenUrl){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "VKontakteAccessToken");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("tokenUrl", tokenUrl);
								
				return _url.Action("VKontakteAccessToken", "Profile", routeValues);
			}
					void TypeCheckGroupPhotos38(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.GroupPhotos(0));
			}
			public string GroupPhotos(System.Int32 userId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupPhotos");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("userId", userId);
								
				return _url.Action("GroupPhotos", "Profile", routeValues);
			}
					void TypeCheckSendCoupon39(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.SendCoupon());
			}
			public string SendCoupon(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendCoupon");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("SendCoupon", "Profile", routeValues);
			}
					void TypeCheckTestError40(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.TestError());
			}
			public string TestError(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TestError");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("TestError", "Profile", routeValues);
			}
					void TypeCheckSetRostov41(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.SetRostov());
			}
			public string SetRostov(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SetRostov");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("SetRostov", "Profile", routeValues);
			}
					void TypeCheckUpdateEmployeeCompany42(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.UpdateEmployeeCompany(0));
			}
			public string UpdateEmployeeCompany(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UpdateEmployeeCompany");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("id", id);
								
				return _url.Action("UpdateEmployeeCompany", "Profile", routeValues);
			}
					void TypeCheckStudyTypeStats43(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.StudyTypeStats());
			}
			public string StudyTypeStats(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "StudyTypeStats");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("StudyTypeStats", "Profile", routeValues);
			}
					void TypeCheckTopStudents44(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.TopStudents());
			}
			public string TopStudents(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TopStudents");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("TopStudents", "Profile", routeValues);
			}
					void TypeCheckUploadStudentPhoto45(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.UploadStudentPhoto());
			}
			public string UploadStudentPhoto(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UploadStudentPhoto");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("UploadStudentPhoto", "Profile", routeValues);
			}
					void TypeCheckOkRefreshToken46(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.OkRefreshToken());
			}
			public string OkRefreshToken(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OkRefreshToken");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("OkRefreshToken", "Profile", routeValues);
			}
					void TypeCheckHowsMySsl47(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.HowsMySsl());
			}
			public string HowsMySsl(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "HowsMySsl");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("HowsMySsl", "Profile", routeValues);
			}
					void TypeCheckClearCache48(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ClearCache((System.String)null));
			}
			public string ClearCache(System.String key){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ClearCache");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("key", key);
								
				return _url.Action("ClearCache", "Profile", routeValues);
			}
					void TypeCheckJobAjax49(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.JobAjax());
			}
			public string JobAjax(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "JobAjax");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("JobAjax", "Profile", routeValues);
			}
					void TypeCheckBangesAjaxNew50(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.BangesAjaxNew());
			}
			public string BangesAjaxNew(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BangesAjaxNew");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("BangesAjaxNew", "Profile", routeValues);
			}
					void TypeCheckDetails51(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Details());
			}
			public string Details(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("Details", "Profile", routeValues);
			}
					void TypeCheckPublic52(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.Public(0));
			}
			public string Public(System.Int32 userID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Public");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("userID", userID);
								
				return _url.Action("Public", "Profile", routeValues);
			}
					void TypeCheckProfileAjax53(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.ProfileAjax());
			}
			public string ProfileAjax(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ProfileAjax");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("ProfileAjax", "Profile", routeValues);
			}
					void TypeCheckEditProfile54(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.EditProfile());
			}
			public string EditProfile(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditProfile");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("EditProfile", "Profile", routeValues);
			}
					void TypeCheckEditProfile55(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.EditProfile((Specialist.Entities.Profile.EditProfileVM)null));
			}
			public string EditProfile(Specialist.Entities.Profile.EditProfileVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditProfile");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("EditProfile", "Profile", routeValues);
			}
					void TypeCheckDeletePhoto56(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.DeletePhoto());
			}
			public string DeletePhoto(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeletePhoto");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("DeletePhoto", "Profile", routeValues);
			}
					void TypeCheckDeleteStoryImage57(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.DeleteStoryImage(0));
			}
			public string DeleteStoryImage(System.Int32 index){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteStoryImage");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("index", index);
								
				return _url.Action("DeleteStoryImage", "Profile", routeValues);
			}
					void TypeCheckSuccessStory58(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.SuccessStory());
			}
			public string SuccessStory(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SuccessStory");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("SuccessStory", "Profile", routeValues);
			}
					void TypeCheckSuccessStory59(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.SuccessStory((Specialist.Entities.Profile.ViewModel.EditSuccessStoryVM)null));
			}
			public string SuccessStory(Specialist.Entities.Profile.ViewModel.EditSuccessStoryVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SuccessStory");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("model", model);
								
				return _url.Action("SuccessStory", "Profile", routeValues);
			}
					void TypeCheckRealSpecialist60(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.RealSpecialist());
			}
			public string RealSpecialist(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RealSpecialist");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("RealSpecialist", "Profile", routeValues);
			}
					void TypeCheckBaseView61(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Profile", routeValues);
			}
					void TypeCheckBaseView62(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Profile", routeValues);
			}
					void TypeCheckBaseViewWithTitle63(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Profile", routeValues);
			}
					void TypeCheckRedirectToAction64(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Profile", routeValues);
			}
					void TypeCheckRedirectBack65(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Profile");
								
				return _url.Action("RedirectBack", "Profile", routeValues);
			}
					void TypeCheckMView66(){
				CheckMethod<Specialist.Web.Controllers.ProfileController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Profile");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Profile", routeValues);
			}
				

		

		}


			public class ProfileControllerLinks{
			
			private UrlHelper _url;

			public ProfileControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static ProfileControllerUrls _ProfileControllerUrls = null;
			public ProfileControllerUrls Urls { 
				get {
				//	if(_ProfileControllerUrls == null) _ProfileControllerUrls = new ProfileControllerUrls(_url);
					//return _ProfileControllerUrls;
					return new ProfileControllerUrls(_url);
				}
			}

		
		
		


			public TagA ExamQuestionnaire(	System.String nextUrl, object content){
				var localActionUrl = _url.Profile().Urls.ExamQuestionnaire(nextUrl);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ExamQuestionnaire(	Specialist.Entities.Passport.UserExamQuestionnaire model, System.String nextUrl, object content){
				var localActionUrl = _url.Profile().Urls.ExamQuestionnaire(model, nextUrl);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Responses(	System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Profile().Urls.Responses(pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CustomerTypeChoice(	System.String nextUrl, object content){
				var localActionUrl = _url.Profile().Urls.CustomerTypeChoice(nextUrl);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Register(	System.String customerType, System.String nextUrl, System.String token, object content){
				var localActionUrl = _url.Profile().Urls.Register(customerType, nextUrl, token);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RegisterControl(	System.String nextUrl, System.String customerType, object content){
				var localActionUrl = _url.Profile().Urls.RegisterControl(nextUrl, customerType);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RegisterPost(	Specialist.Entities.Passport.ViewModel.RegisterVM model, object content){
				var localActionUrl = _url.Profile().Urls.RegisterPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ChangePassword(	object content){
				var localActionUrl = _url.Profile().Urls.ChangePassword();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ChangePassword(	Specialist.Entities.Profile.ViewModel.ChangePasswordVM model, object content){
				var localActionUrl = _url.Profile().Urls.ChangePassword(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RestorePassword(	System.String email, object content){
				var localActionUrl = _url.Profile().Urls.RestorePassword(email);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RestorePassword(	Specialist.Entities.Profile.MetaData.RestorePasswordVM model, object content){
				var localActionUrl = _url.Profile().Urls.RestorePassword(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA HostingInfo(	object content){
				var localActionUrl = _url.Profile().Urls.HostingInfo();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Learning(	object content){
				var localActionUrl = _url.Profile().Urls.Learning();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Classmates(	object content){
				var localActionUrl = _url.Profile().Urls.Classmates();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ChangeStatus(	object content){
				var localActionUrl = _url.Profile().Urls.ChangeStatus();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ChangeStatus(	Specialist.Entities.Profile.ViewModel.ChangeStatusVM model, object content){
				var localActionUrl = _url.Profile().Urls.ChangeStatus(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Subscribes(	object content){
				var localActionUrl = _url.Profile().Urls.Subscribes();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Subscribes(	Specialist.Entities.Profile.ViewModel.SubscribesVM model, object content){
				var localActionUrl = _url.Profile().Urls.Subscribes(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Library(	object content){
				var localActionUrl = _url.Profile().Urls.Library();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Tests(	System.Int32 index, object content){
				var localActionUrl = _url.Profile().Urls.Tests(index);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Survey(	System.Int32 sigId, object content){
				var localActionUrl = _url.Profile().Urls.Survey(sigId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Survey(	Specialist.Entities.Secondary.QuestionAnswer model, object content){
				var localActionUrl = _url.Profile().Urls.Survey(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupSurvey(	System.Decimal studentId, System.Decimal groupId, object content){
				var localActionUrl = _url.Profile().Urls.GroupSurvey(studentId, groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupSurvey(	Specialist.Entities.Secondary.GroupSurvey model, object content){
				var localActionUrl = _url.Profile().Urls.GroupSurvey(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ExcelMaster(	object content){
				var localActionUrl = _url.Profile().Urls.ExcelMaster();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MyBusiness(	object content){
				var localActionUrl = _url.Profile().Urls.MyBusiness();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MyBusiness(	Specialist.Web.Root.Profile.Logic.MyBusinessUser model, object content){
				var localActionUrl = _url.Profile().Urls.MyBusiness(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ChangeNameRequest(	object content){
				var localActionUrl = _url.Profile().Urls.ChangeNameRequest();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ChangeNameRequest(	Specialist.Entities.Profile.ViewModel.Common.ChangeNameRequestVM model, object content){
				var localActionUrl = _url.Profile().Urls.ChangeNameRequest(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ChangeName(	System.Int32 userId, System.String firstName, System.String secondName, System.String lastName, object content){
				var localActionUrl = _url.Profile().Urls.ChangeName(userId, firstName, secondName, lastName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA WorkPlace(	object content){
				var localActionUrl = _url.Profile().Urls.WorkPlace();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA WorkPlacePost(	Specialist.Entities.Passport.WorkPlace model, object content){
				var localActionUrl = _url.Profile().Urls.WorkPlacePost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SocialConnect(	object content){
				var localActionUrl = _url.Profile().Urls.SocialConnect();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SocialConnects(	object content){
				var localActionUrl = _url.Profile().Urls.SocialConnects();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA LinkFacebook(	System.String token, object content){
				var localActionUrl = _url.Profile().Urls.LinkFacebook(token);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA FacebookLogin(	System.String token, System.String returnUrl, object content){
				var localActionUrl = _url.Profile().Urls.FacebookLogin(token, returnUrl);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA FaceBookAccessToken(	System.String token, object content){
				var localActionUrl = _url.Profile().Urls.FaceBookAccessToken(token);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA VKontakteAccessToken(	System.String tokenUrl, object content){
				var localActionUrl = _url.Profile().Urls.VKontakteAccessToken(tokenUrl);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupPhotos(	System.Int32 userId, object content){
				var localActionUrl = _url.Profile().Urls.GroupPhotos(userId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendCoupon(	object content){
				var localActionUrl = _url.Profile().Urls.SendCoupon();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TestError(	object content){
				var localActionUrl = _url.Profile().Urls.TestError();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SetRostov(	object content){
				var localActionUrl = _url.Profile().Urls.SetRostov();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UpdateEmployeeCompany(	System.Int32 id, object content){
				var localActionUrl = _url.Profile().Urls.UpdateEmployeeCompany(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA StudyTypeStats(	object content){
				var localActionUrl = _url.Profile().Urls.StudyTypeStats();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TopStudents(	object content){
				var localActionUrl = _url.Profile().Urls.TopStudents();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UploadStudentPhoto(	object content){
				var localActionUrl = _url.Profile().Urls.UploadStudentPhoto();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA OkRefreshToken(	object content){
				var localActionUrl = _url.Profile().Urls.OkRefreshToken();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA HowsMySsl(	object content){
				var localActionUrl = _url.Profile().Urls.HowsMySsl();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ClearCache(	System.String key, object content){
				var localActionUrl = _url.Profile().Urls.ClearCache(key);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA JobAjax(	object content){
				var localActionUrl = _url.Profile().Urls.JobAjax();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BangesAjaxNew(	object content){
				var localActionUrl = _url.Profile().Urls.BangesAjaxNew();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Details(	object content){
				var localActionUrl = _url.Profile().Urls.Details();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Public(	System.Int32 userID, object content){
				var localActionUrl = _url.Profile().Urls.Public(userID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ProfileAjax(	object content){
				var localActionUrl = _url.Profile().Urls.ProfileAjax();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditProfile(	object content){
				var localActionUrl = _url.Profile().Urls.EditProfile();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditProfile(	Specialist.Entities.Profile.EditProfileVM model, object content){
				var localActionUrl = _url.Profile().Urls.EditProfile(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeletePhoto(	object content){
				var localActionUrl = _url.Profile().Urls.DeletePhoto();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteStoryImage(	System.Int32 index, object content){
				var localActionUrl = _url.Profile().Urls.DeleteStoryImage(index);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SuccessStory(	object content){
				var localActionUrl = _url.Profile().Urls.SuccessStory();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SuccessStory(	Specialist.Entities.Profile.ViewModel.EditSuccessStoryVM model, object content){
				var localActionUrl = _url.Profile().Urls.SuccessStory(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RealSpecialist(	object content){
				var localActionUrl = _url.Profile().Urls.RealSpecialist();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Profile().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Profile().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Profile().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Profile().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Profile().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Profile().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static OrderControllerLinks _OrderControllerLinks = null;
		
		public static OrderControllerLinks Order(this UrlHelper urlHelper) {
		//	if(_OrderControllerLinks == null) _OrderControllerLinks = new OrderControllerLinks(urlHelper);
		//	return _OrderControllerLinks;
		return new OrderControllerLinks(urlHelper);
		}

		public class OrderControllerUrls{
			
			private UrlHelper _url;

			public OrderControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckOrgComplete0(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.OrgComplete(0));
			}
			public string OrgComplete(System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OrgComplete");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderID", orderID);
								
				return _url.Action("OrgComplete", "Order", routeValues);
			}
					void TypeCheckUploadPassport1(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.UploadPassport((System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase>)null));
			}
			public string UploadPassport(System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase> userfile){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UploadPassport");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("userfile", userfile);
								
				return _url.Action("UploadPassport", "Order", routeValues);
			}
					void TypeCheckDownloadTestCertificate2(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.DownloadTestCertificate(0, false));
			}
			public string DownloadTestCertificate(System.Decimal orderDetailId, System.Boolean second){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadTestCertificate");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderDetailId", orderDetailId);
									routeValues.Add("second", second);
								
				return _url.Action("DownloadTestCertificate", "Order", routeValues);
			}
					void TypeCheckTestCertificates3(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.TestCertificates());
			}
			public string TestCertificates(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TestCertificates");
			//	routeValues.Add("controller", "Order");
								
				return _url.Action("TestCertificates", "Order", routeValues);
			}
					void TypeCheckIsGAExport4(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.IsGAExport(0));
			}
			public string IsGAExport(System.Decimal orderId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "IsGAExport");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderId", orderId);
								
				return _url.Action("IsGAExport", "Order", routeValues);
			}
					void TypeCheckCredit5(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.Credit(0));
			}
			public string Credit(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Credit");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("id", id);
								
				return _url.Action("Credit", "Order", routeValues);
			}
					void TypeCheckRegister6(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.Register());
			}
			public string Register(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Register");
			//	routeValues.Add("controller", "Order");
								
				return _url.Action("Register", "Order", routeValues);
			}
					void TypeCheckExpressRegister7(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.ExpressRegister());
			}
			public string ExpressRegister(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ExpressRegister");
			//	routeValues.Add("controller", "Order");
								
				return _url.Action("ExpressRegister", "Order", routeValues);
			}
					void TypeCheckExpressRegisterPost8(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.ExpressRegisterPost((Specialist.Web.ViewModel.Orders.ExpressRegisterVM)null));
			}
			public string ExpressRegisterPost(Specialist.Web.ViewModel.Orders.ExpressRegisterVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ExpressRegisterPost");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("model", model);
								
				return _url.Action("ExpressRegisterPost", "Order", routeValues);
			}
					void TypeCheckExpressRegisterComplete9(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.ExpressRegisterComplete(0));
			}
			public string ExpressRegisterComplete(System.Decimal orderId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ExpressRegisterComplete");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderId", orderId);
								
				return _url.Action("ExpressRegisterComplete", "Order", routeValues);
			}
					void TypeCheckSberMerchant10(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.SberMerchant((System.String)null));
			}
			public string SberMerchant(System.String commonOrderId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SberMerchant");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("commonOrderId", commonOrderId);
								
				return _url.Action("SberMerchant", "Order", routeValues);
			}
					void TypeCheckSberbankInfo11(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.SberbankInfo(0));
			}
			public string SberbankInfo(System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SberbankInfo");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderID", orderID);
								
				return _url.Action("SberbankInfo", "Order", routeValues);
			}
					void TypeCheckTestCertInfo12(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.TestCertInfo());
			}
			public string TestCertInfo(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TestCertInfo");
			//	routeValues.Add("controller", "Order");
								
				return _url.Action("TestCertInfo", "Order", routeValues);
			}
					void TypeCheckTestCertInfo13(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.TestCertInfo((Specialist.Web.Root.Tests.ViewModels.TestCertInfoVM)null));
			}
			public string TestCertInfo(Specialist.Web.Root.Tests.ViewModels.TestCertInfoVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TestCertInfo");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("model", model);
								
				return _url.Action("TestCertInfo", "Order", routeValues);
			}
					void TypeCheckCashInfo14(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.CashInfo(0));
			}
			public string CashInfo(System.Decimal orderId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CashInfo");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderId", orderId);
								
				return _url.Action("CashInfo", "Order", routeValues);
			}
					void TypeCheckSberbankInfo15(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.SberbankInfo((Specialist.Entities.Context.ViewModel.SberbankInfoVM)null));
			}
			public string SberbankInfo(Specialist.Entities.Context.ViewModel.SberbankInfoVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SberbankInfo");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("model", model);
								
				return _url.Action("SberbankInfo", "Order", routeValues);
			}
					void TypeCheckConfirm16(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.Confirm(0));
			}
			public string Confirm(System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Confirm");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderID", orderID);
								
				return _url.Action("Confirm", "Order", routeValues);
			}
					void TypeCheckConfirm17(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.Confirm((Specialist.Entities.Order.ViewModel.OrderConfirmVM)null));
			}
			public string Confirm(Specialist.Entities.Order.ViewModel.OrderConfirmVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Confirm");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("model", model);
								
				return _url.Action("Confirm", "Order", routeValues);
			}
					void TypeCheckContract18(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.Contract());
			}
			public string Contract(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Contract");
			//	routeValues.Add("controller", "Order");
								
				return _url.Action("Contract", "Order", routeValues);
			}
					void TypeCheckShowContract19(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.ShowContract());
			}
			public string ShowContract(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ShowContract");
			//	routeValues.Add("controller", "Order");
								
				return _url.Action("ShowContract", "Order", routeValues);
			}
					void TypeCheckPaymentComplete20(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.PaymentComplete());
			}
			public string PaymentComplete(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PaymentComplete");
			//	routeValues.Add("controller", "Order");
								
				return _url.Action("PaymentComplete", "Order", routeValues);
			}
					void TypeCheckSpecial21(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.Special((System.Nullable<System.Decimal>)null));
			}
			public string Special(System.Nullable<System.Decimal> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Special");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("id", id);
								
				return _url.Action("Special", "Order", routeValues);
			}
					void TypeCheckSpecialOrg22(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.SpecialOrg(0));
			}
			public string SpecialOrg(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SpecialOrg");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("id", id);
								
				return _url.Action("SpecialOrg", "Order", routeValues);
			}
					void TypeCheckOrderOrg23(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.OrderOrg(0));
			}
			public string OrderOrg(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OrderOrg");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("id", id);
								
				return _url.Action("OrderOrg", "Order", routeValues);
			}
					void TypeCheckSpecialLink24(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.SpecialLink((System.Nullable<System.Decimal>)null));
			}
			public string SpecialLink(System.Nullable<System.Decimal> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SpecialLink");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("id", id);
								
				return _url.Action("SpecialLink", "Order", routeValues);
			}
					void TypeCheckUpdateReasonsForLearning25(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.UpdateReasonsForLearning((System.Collections.Generic.List<Specialist.Entities.Context.OrderDetail>)null));
			}
			public string UpdateReasonsForLearning(System.Collections.Generic.List<Specialist.Entities.Context.OrderDetail> model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UpdateReasonsForLearning");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("model", model);
								
				return _url.Action("UpdateReasonsForLearning", "Order", routeValues);
			}
					void TypeCheckUpdateEducation26(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.UpdateEducation(0, (System.String)null));
			}
			public string UpdateEducation(System.Decimal orderId, System.String eduDocTypeTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UpdateEducation");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderId", orderId);
									routeValues.Add("eduDocTypeTC", eduDocTypeTC);
								
				return _url.Action("UpdateEducation", "Order", routeValues);
			}
					void TypeCheckPaymentTypeChoice27(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.PaymentTypeChoice(0));
			}
			public string PaymentTypeChoice(System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PaymentTypeChoice");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderID", orderID);
								
				return _url.Action("PaymentTypeChoice", "Order", routeValues);
			}
					void TypeCheckSetPaymentType28(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.SetPaymentType((System.String)null, 0));
			}
			public string SetPaymentType(System.String paymentType, System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SetPaymentType");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("paymentType", paymentType);
									routeValues.Add("orderID", orderID);
								
				return _url.Action("SetPaymentType", "Order", routeValues);
			}
					void TypeCheckReceiptPayment29(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.ReceiptPayment(0));
			}
			public string ReceiptPayment(System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ReceiptPayment");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderID", orderID);
								
				return _url.Action("ReceiptPayment", "Order", routeValues);
			}
					void TypeCheckTerminalPayment30(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.TerminalPayment(0));
			}
			public string TerminalPayment(System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TerminalPayment");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderID", orderID);
								
				return _url.Action("TerminalPayment", "Order", routeValues);
			}
					void TypeCheckReceipt31(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.Receipt(0));
			}
			public string Receipt(System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Receipt");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderID", orderID);
								
				return _url.Action("Receipt", "Order", routeValues);
			}
					void TypeCheckInvoicePayment32(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.InvoicePayment(0));
			}
			public string InvoicePayment(System.Decimal orderID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "InvoicePayment");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("orderID", orderID);
								
				return _url.Action("InvoicePayment", "Order", routeValues);
			}
					void TypeCheckBaseView33(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Order", routeValues);
			}
					void TypeCheckBaseView34(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Order", routeValues);
			}
					void TypeCheckBaseViewWithTitle35(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Order", routeValues);
			}
					void TypeCheckRedirectToAction36(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Order", routeValues);
			}
					void TypeCheckRedirectBack37(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Order");
								
				return _url.Action("RedirectBack", "Order", routeValues);
			}
					void TypeCheckMView38(){
				CheckMethod<Specialist.Web.Controllers.OrderController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Order");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Order", routeValues);
			}
				

		

		}


			public class OrderControllerLinks{
			
			private UrlHelper _url;

			public OrderControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static OrderControllerUrls _OrderControllerUrls = null;
			public OrderControllerUrls Urls { 
				get {
				//	if(_OrderControllerUrls == null) _OrderControllerUrls = new OrderControllerUrls(_url);
					//return _OrderControllerUrls;
					return new OrderControllerUrls(_url);
				}
			}

		
		
		


			public TagA OrgComplete(	System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.OrgComplete(orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UploadPassport(	System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase> userfile, object content){
				var localActionUrl = _url.Order().Urls.UploadPassport(userfile);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DownloadTestCertificate(	System.Decimal orderDetailId, System.Boolean second, object content){
				var localActionUrl = _url.Order().Urls.DownloadTestCertificate(orderDetailId, second);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TestCertificates(	object content){
				var localActionUrl = _url.Order().Urls.TestCertificates();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA IsGAExport(	System.Decimal orderId, object content){
				var localActionUrl = _url.Order().Urls.IsGAExport(orderId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Credit(	System.Decimal id, object content){
				var localActionUrl = _url.Order().Urls.Credit(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Register(	object content){
				var localActionUrl = _url.Order().Urls.Register();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ExpressRegister(	object content){
				var localActionUrl = _url.Order().Urls.ExpressRegister();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ExpressRegisterPost(	Specialist.Web.ViewModel.Orders.ExpressRegisterVM model, object content){
				var localActionUrl = _url.Order().Urls.ExpressRegisterPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ExpressRegisterComplete(	System.Decimal orderId, object content){
				var localActionUrl = _url.Order().Urls.ExpressRegisterComplete(orderId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SberMerchant(	System.String commonOrderId, object content){
				var localActionUrl = _url.Order().Urls.SberMerchant(commonOrderId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SberbankInfo(	System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.SberbankInfo(orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TestCertInfo(	object content){
				var localActionUrl = _url.Order().Urls.TestCertInfo();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TestCertInfo(	Specialist.Web.Root.Tests.ViewModels.TestCertInfoVM model, object content){
				var localActionUrl = _url.Order().Urls.TestCertInfo(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CashInfo(	System.Decimal orderId, object content){
				var localActionUrl = _url.Order().Urls.CashInfo(orderId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SberbankInfo(	Specialist.Entities.Context.ViewModel.SberbankInfoVM model, object content){
				var localActionUrl = _url.Order().Urls.SberbankInfo(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Confirm(	System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.Confirm(orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Confirm(	Specialist.Entities.Order.ViewModel.OrderConfirmVM model, object content){
				var localActionUrl = _url.Order().Urls.Confirm(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Contract(	object content){
				var localActionUrl = _url.Order().Urls.Contract();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ShowContract(	object content){
				var localActionUrl = _url.Order().Urls.ShowContract();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PaymentComplete(	object content){
				var localActionUrl = _url.Order().Urls.PaymentComplete();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Special(	System.Nullable<System.Decimal> id, object content){
				var localActionUrl = _url.Order().Urls.Special(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SpecialOrg(	System.Decimal id, object content){
				var localActionUrl = _url.Order().Urls.SpecialOrg(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA OrderOrg(	System.Decimal id, object content){
				var localActionUrl = _url.Order().Urls.OrderOrg(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SpecialLink(	System.Nullable<System.Decimal> id, object content){
				var localActionUrl = _url.Order().Urls.SpecialLink(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UpdateReasonsForLearning(	System.Collections.Generic.List<Specialist.Entities.Context.OrderDetail> model, object content){
				var localActionUrl = _url.Order().Urls.UpdateReasonsForLearning(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UpdateEducation(	System.Decimal orderId, System.String eduDocTypeTC, object content){
				var localActionUrl = _url.Order().Urls.UpdateEducation(orderId, eduDocTypeTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PaymentTypeChoice(	System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.PaymentTypeChoice(orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SetPaymentType(	System.String paymentType, System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.SetPaymentType(paymentType, orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ReceiptPayment(	System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.ReceiptPayment(orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TerminalPayment(	System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.TerminalPayment(orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Receipt(	System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.Receipt(orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA InvoicePayment(	System.Decimal orderID, object content){
				var localActionUrl = _url.Order().Urls.InvoicePayment(orderID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Order().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Order().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Order().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Order().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Order().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Order().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static CertificationControllerLinks _CertificationControllerLinks = null;
		
		public static CertificationControllerLinks Certification(this UrlHelper urlHelper) {
		//	if(_CertificationControllerLinks == null) _CertificationControllerLinks = new CertificationControllerLinks(urlHelper);
		//	return _CertificationControllerLinks;
		return new CertificationControllerLinks(urlHelper);
		}

		public class CertificationControllerUrls{
			
			private UrlHelper _url;

			public CertificationControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.CertificationController>(c => 
					c.Details((System.String)null));
			}
			public string Details(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Certification");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Details", "Certification", routeValues);
			}
					void TypeCheckCertificationNames1(){
				CheckMethod<Specialist.Web.Controllers.CertificationController>(c => 
					c.CertificationNames((System.String)null));
			}
			public string CertificationNames(System.String query){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CertificationNames");
			//	routeValues.Add("controller", "Certification");
									routeValues.Add("query", query);
								
				return _url.Action("CertificationNames", "Certification", routeValues);
			}
					void TypeCheckCertificationList2(){
				CheckMethod<Specialist.Web.Controllers.CertificationController>(c => 
					c.CertificationList((System.String)null));
			}
			public string CertificationList(System.String name){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CertificationList");
			//	routeValues.Add("controller", "Certification");
									routeValues.Add("name", name);
								
				return _url.Action("CertificationList", "Certification", routeValues);
			}
					void TypeCheckRedirectToAction3(){
				CheckMethod<Specialist.Web.Controllers.CertificationController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Certification");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Certification", routeValues);
			}
					void TypeCheckRedirectBack4(){
				CheckMethod<Specialist.Web.Controllers.CertificationController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Certification");
								
				return _url.Action("RedirectBack", "Certification", routeValues);
			}
					void TypeCheckMView5(){
				CheckMethod<Specialist.Web.Controllers.CertificationController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Certification");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Certification", routeValues);
			}
				

		

		}


			public class CertificationControllerLinks{
			
			private UrlHelper _url;

			public CertificationControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static CertificationControllerUrls _CertificationControllerUrls = null;
			public CertificationControllerUrls Urls { 
				get {
				//	if(_CertificationControllerUrls == null) _CertificationControllerUrls = new CertificationControllerUrls(_url);
					//return _CertificationControllerUrls;
					return new CertificationControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.String urlName, object content){
				var localActionUrl = _url.Certification().Urls.Details(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CertificationNames(	System.String query, object content){
				var localActionUrl = _url.Certification().Urls.CertificationNames(query);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CertificationList(	System.String name, object content){
				var localActionUrl = _url.Certification().Urls.CertificationList(name);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Certification().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Certification().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Certification().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static CourseControllerLinks _CourseControllerLinks = null;
		
		public static CourseControllerLinks Course(this UrlHelper urlHelper) {
		//	if(_CourseControllerLinks == null) _CourseControllerLinks = new CourseControllerLinks(urlHelper);
		//	return _CourseControllerLinks;
		return new CourseControllerLinks(urlHelper);
		}

		public class CourseControllerUrls{
			
			private UrlHelper _url;

			public CourseControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckMainCourses0(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.MainCourses());
			}
			public string MainCourses(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MainCourses");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("MainCourses", "Course", routeValues);
			}
					void TypeCheckMainCoursesSections1(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.MainCoursesSections());
			}
			public string MainCoursesSections(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MainCoursesSections");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("MainCoursesSections", "Course", routeValues);
			}
					void TypeCheckPrint2(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Print((System.String)null));
			}
			public string Print(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Print");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Print", "Course", routeValues);
			}
					void TypeCheckDetails3(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Details((System.String)null));
			}
			public string Details(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Details", "Course", routeValues);
			}
					void TypeCheckCourseNames4(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.CourseNames((System.String)null));
			}
			public string CourseNames(System.String query){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseNames");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("query", query);
								
				return _url.Action("CourseNames", "Course", routeValues);
			}
					void TypeCheckElearningNames5(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.ElearningNames((System.String)null));
			}
			public string ElearningNames(System.String query){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ElearningNames");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("query", query);
								
				return _url.Action("ElearningNames", "Course", routeValues);
			}
					void TypeCheckTracks6(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Tracks((System.String)null, false));
			}
			public string Tracks(System.String courseTC, System.Boolean isSecond){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Tracks");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("courseTC", courseTC);
									routeValues.Add("isSecond", isSecond);
								
				return _url.Action("Tracks", "Course", routeValues);
			}
					void TypeCheckTags7(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Tags((System.String)null));
			}
			public string Tags(System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Tags");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("Tags", "Course", routeValues);
			}
					void TypeCheckFiles8(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Files((System.String)null));
			}
			public string Files(System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Files");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("Files", "Course", routeValues);
			}
					void TypeCheckUserCourseInfoPost9(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.UserCourseInfoPost((Specialist.Entities.Context.UserCourseInfo)null));
			}
			public string UserCourseInfoPost(Specialist.Entities.Context.UserCourseInfo model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UserCourseInfoPost");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("model", model);
								
				return _url.Action("UserCourseInfoPost", "Course", routeValues);
			}
					void TypeCheckSeminars10(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Seminars());
			}
			public string Seminars(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Seminars");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("Seminars", "Course", routeValues);
			}
					void TypeCheckConsultations11(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Consultations());
			}
			public string Consultations(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Consultations");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("Consultations", "Course", routeValues);
			}
					void TypeCheckSeminarComplete12(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.SeminarComplete(0));
			}
			public string SeminarComplete(System.Decimal groupID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SeminarComplete");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("groupID", groupID);
								
				return _url.Action("SeminarComplete", "Course", routeValues);
			}
					void TypeCheckAddSeminar13(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.AddSeminar(0, (System.Nullable<System.Boolean>)null));
			}
			public string AddSeminar(System.Decimal groupID, System.Nullable<System.Boolean> isIntramural){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddSeminar");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("groupID", groupID);
									routeValues.Add("isIntramural", isIntramural);
								
				return _url.Action("AddSeminar", "Course", routeValues);
			}
					void TypeCheckAddSeminarWithLink14(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.AddSeminarWithLink(0, false));
			}
			public string AddSeminarWithLink(System.Decimal groupID, System.Boolean isIntramural){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddSeminarWithLink");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("groupID", groupID);
									routeValues.Add("isIntramural", isIntramural);
								
				return _url.Action("AddSeminarWithLink", "Course", routeValues);
			}
					void TypeCheckCourseList15(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.CourseList((System.String)null, (System.Object)null, (System.String)null));
			}
			public string CourseList(System.String typeName, System.Object pk, System.String name){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseList");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("typeName", typeName);
									routeValues.Add("pk", pk);
									routeValues.Add("name", name);
								
				return _url.Action("CourseList", "Course", routeValues);
			}
					void TypeCheckCourseListForModel16(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.CourseListForModel((System.Object)null));
			}
			public string CourseListForModel(System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseListForModel");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("model", model);
								
				return _url.Action("CourseListForModel", "Course", routeValues);
			}
					void TypeCheckCourseListFor17(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.CourseListFor((System.Object)null));
			}
			public string CourseListFor(System.Object obj){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseListFor");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("obj", obj);
								
				return _url.Action("CourseListFor", "Course", routeValues);
			}
					void TypeCheckCourseListsForSection18(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.CourseListsForSection(0));
			}
			public string CourseListsForSection(System.Int32 sectionId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseListsForSection");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("sectionId", sectionId);
								
				return _url.Action("CourseListsForSection", "Course", routeValues);
			}
					void TypeCheckCourseListsForVendor19(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.CourseListsForVendor(0));
			}
			public string CourseListsForVendor(System.Int32 vendorId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseListsForVendor");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("vendorId", vendorId);
								
				return _url.Action("CourseListsForVendor", "Course", routeValues);
			}
					void TypeCheckElearningList20(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.ElearningList((System.String)null));
			}
			public string ElearningList(System.String name){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ElearningList");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("name", name);
								
				return _url.Action("ElearningList", "Course", routeValues);
			}
					void TypeCheckCoursesForCarousel21(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.CoursesForCarousel(0));
			}
			public string CoursesForCarousel(System.Int32 sectionId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CoursesForCarousel");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("sectionId", sectionId);
								
				return _url.Action("CoursesForCarousel", "Course", routeValues);
			}
					void TypeCheckCarouselForEntity22(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.CarouselForEntity((System.String)null, (System.Object)null));
			}
			public string CarouselForEntity(System.String typeName, System.Object pk){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CarouselForEntity");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("typeName", typeName);
									routeValues.Add("pk", pk);
								
				return _url.Action("CarouselForEntity", "Course", routeValues);
			}
					void TypeCheckNearestCourses23(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.NearestCourses());
			}
			public string NearestCourses(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "NearestCourses");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("NearestCourses", "Course", routeValues);
			}
					void TypeCheckWithoutWebinar24(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.WithoutWebinar());
			}
			public string WithoutWebinar(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "WithoutWebinar");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("WithoutWebinar", "Course", routeValues);
			}
					void TypeCheckIsNew25(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.IsNew());
			}
			public string IsNew(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "IsNew");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("IsNew", "Course", routeValues);
			}
					void TypeCheckIsNewBlock26(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.IsNewBlock());
			}
			public string IsNewBlock(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "IsNewBlock");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("IsNewBlock", "Course", routeValues);
			}
					void TypeCheckWithOpenClasses27(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.WithOpenClasses());
			}
			public string WithOpenClasses(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "WithOpenClasses");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("WithOpenClasses", "Course", routeValues);
			}
					void TypeCheckGroup28(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Group(0));
			}
			public string Group(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Group");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("id", id);
								
				return _url.Action("Group", "Course", routeValues);
			}
					void TypeCheckSearch29(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Search((System.String)null));
			}
			public string Search(System.String text){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Search");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("text", text);
								
				return _url.Action("Search", "Course", routeValues);
			}
					void TypeCheckGuide30(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.Guide(0));
			}
			public string Guide(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Guide");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("id", id);
								
				return _url.Action("Guide", "Course", routeValues);
			}
					void TypeCheckBaseView31(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Course", routeValues);
			}
					void TypeCheckBaseView32(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Course", routeValues);
			}
					void TypeCheckBaseViewWithTitle33(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Course", routeValues);
			}
					void TypeCheckRedirectToAction34(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Course", routeValues);
			}
					void TypeCheckRedirectBack35(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Course");
								
				return _url.Action("RedirectBack", "Course", routeValues);
			}
					void TypeCheckMView36(){
				CheckMethod<Specialist.Web.Controllers.CourseController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Course");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Course", routeValues);
			}
				

		

		}


			public class CourseControllerLinks{
			
			private UrlHelper _url;

			public CourseControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static CourseControllerUrls _CourseControllerUrls = null;
			public CourseControllerUrls Urls { 
				get {
				//	if(_CourseControllerUrls == null) _CourseControllerUrls = new CourseControllerUrls(_url);
					//return _CourseControllerUrls;
					return new CourseControllerUrls(_url);
				}
			}

		
		
		


			public TagA MainCourses(	object content){
				var localActionUrl = _url.Course().Urls.MainCourses();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MainCoursesSections(	object content){
				var localActionUrl = _url.Course().Urls.MainCoursesSections();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Print(	System.String urlName, object content){
				var localActionUrl = _url.Course().Urls.Print(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Details(	System.String urlName, object content){
				var localActionUrl = _url.Course().Urls.Details(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseNames(	System.String query, object content){
				var localActionUrl = _url.Course().Urls.CourseNames(query);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ElearningNames(	System.String query, object content){
				var localActionUrl = _url.Course().Urls.ElearningNames(query);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Tracks(	System.String courseTC, System.Boolean isSecond, object content){
				var localActionUrl = _url.Course().Urls.Tracks(courseTC, isSecond);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Tags(	System.String courseTC, object content){
				var localActionUrl = _url.Course().Urls.Tags(courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Files(	System.String courseTC, object content){
				var localActionUrl = _url.Course().Urls.Files(courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UserCourseInfoPost(	Specialist.Entities.Context.UserCourseInfo model, object content){
				var localActionUrl = _url.Course().Urls.UserCourseInfoPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Seminars(	object content){
				var localActionUrl = _url.Course().Urls.Seminars();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Consultations(	object content){
				var localActionUrl = _url.Course().Urls.Consultations();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SeminarComplete(	System.Decimal groupID, object content){
				var localActionUrl = _url.Course().Urls.SeminarComplete(groupID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddSeminar(	System.Decimal groupID, System.Nullable<System.Boolean> isIntramural, object content){
				var localActionUrl = _url.Course().Urls.AddSeminar(groupID, isIntramural);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddSeminarWithLink(	System.Decimal groupID, System.Boolean isIntramural, object content){
				var localActionUrl = _url.Course().Urls.AddSeminarWithLink(groupID, isIntramural);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseList(	System.String typeName, System.Object pk, System.String name, object content){
				var localActionUrl = _url.Course().Urls.CourseList(typeName, pk, name);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseListForModel(	System.Object model, object content){
				var localActionUrl = _url.Course().Urls.CourseListForModel(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseListFor(	System.Object obj, object content){
				var localActionUrl = _url.Course().Urls.CourseListFor(obj);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseListsForSection(	System.Int32 sectionId, object content){
				var localActionUrl = _url.Course().Urls.CourseListsForSection(sectionId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseListsForVendor(	System.Int32 vendorId, object content){
				var localActionUrl = _url.Course().Urls.CourseListsForVendor(vendorId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ElearningList(	System.String name, object content){
				var localActionUrl = _url.Course().Urls.ElearningList(name);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CoursesForCarousel(	System.Int32 sectionId, object content){
				var localActionUrl = _url.Course().Urls.CoursesForCarousel(sectionId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CarouselForEntity(	System.String typeName, System.Object pk, object content){
				var localActionUrl = _url.Course().Urls.CarouselForEntity(typeName, pk);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA NearestCourses(	object content){
				var localActionUrl = _url.Course().Urls.NearestCourses();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA WithoutWebinar(	object content){
				var localActionUrl = _url.Course().Urls.WithoutWebinar();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA IsNew(	object content){
				var localActionUrl = _url.Course().Urls.IsNew();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA IsNewBlock(	object content){
				var localActionUrl = _url.Course().Urls.IsNewBlock();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA WithOpenClasses(	object content){
				var localActionUrl = _url.Course().Urls.WithOpenClasses();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Group(	System.Decimal id, object content){
				var localActionUrl = _url.Course().Urls.Group(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Search(	System.String text, object content){
				var localActionUrl = _url.Course().Urls.Search(text);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Guide(	System.Int32 id, object content){
				var localActionUrl = _url.Course().Urls.Guide(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Course().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Course().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Course().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Course().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Course().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Course().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static ExamControllerLinks _ExamControllerLinks = null;
		
		public static ExamControllerLinks Exam(this UrlHelper urlHelper) {
		//	if(_ExamControllerLinks == null) _ExamControllerLinks = new ExamControllerLinks(urlHelper);
		//	return _ExamControllerLinks;
		return new ExamControllerLinks(urlHelper);
		}

		public class ExamControllerUrls{
			
			private UrlHelper _url;

			public ExamControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckProvider0(){
				CheckMethod<Specialist.Web.Controllers.ExamController>(c => 
					c.Provider(0));
			}
			public string Provider(System.Int32 providerID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Provider");
			//	routeValues.Add("controller", "Exam");
									routeValues.Add("providerID", providerID);
								
				return _url.Action("Provider", "Exam", routeValues);
			}
					void TypeCheckDetails1(){
				CheckMethod<Specialist.Web.Controllers.ExamController>(c => 
					c.Details((System.String)null));
			}
			public string Details(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Exam");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Details", "Exam", routeValues);
			}
					void TypeCheckSearch2(){
				CheckMethod<Specialist.Web.Controllers.ExamController>(c => 
					c.Search(0, (System.String)null));
			}
			public string Search(System.Int32 vendorId, System.String number){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Search");
			//	routeValues.Add("controller", "Exam");
									routeValues.Add("vendorId", vendorId);
									routeValues.Add("number", number);
								
				return _url.Action("Search", "Exam", routeValues);
			}
					void TypeCheckRedirectToAction3(){
				CheckMethod<Specialist.Web.Controllers.ExamController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Exam");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Exam", routeValues);
			}
					void TypeCheckRedirectBack4(){
				CheckMethod<Specialist.Web.Controllers.ExamController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Exam");
								
				return _url.Action("RedirectBack", "Exam", routeValues);
			}
					void TypeCheckMView5(){
				CheckMethod<Specialist.Web.Controllers.ExamController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Exam");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Exam", routeValues);
			}
				

		

		}


			public class ExamControllerLinks{
			
			private UrlHelper _url;

			public ExamControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static ExamControllerUrls _ExamControllerUrls = null;
			public ExamControllerUrls Urls { 
				get {
				//	if(_ExamControllerUrls == null) _ExamControllerUrls = new ExamControllerUrls(_url);
					//return _ExamControllerUrls;
					return new ExamControllerUrls(_url);
				}
			}

		
		
		


			public TagA Provider(	System.Int32 providerID, object content){
				var localActionUrl = _url.Exam().Urls.Provider(providerID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Details(	System.String urlName, object content){
				var localActionUrl = _url.Exam().Urls.Details(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Search(	System.Int32 vendorId, System.String number, object content){
				var localActionUrl = _url.Exam().Urls.Search(vendorId, number);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Exam().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Exam().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Exam().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static GroupControllerLinks _GroupControllerLinks = null;
		
		public static GroupControllerLinks Group(this UrlHelper urlHelper) {
		//	if(_GroupControllerLinks == null) _GroupControllerLinks = new GroupControllerLinks(urlHelper);
		//	return _GroupControllerLinks;
		return new GroupControllerLinks(urlHelper);
		}

		public class GroupControllerUrls{
			
			private UrlHelper _url;

			public GroupControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.Details(0));
			}
			public string Details(System.Decimal groupID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("groupID", groupID);
								
				return _url.Action("Details", "Group", routeValues);
			}
					void TypeCheckVideos1(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.Videos(0));
			}
			public string Videos(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Videos");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("Videos", "Group", routeValues);
			}
					void TypeCheckSearch2(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.Search((Specialist.Entities.Filter.GroupFilter)null));
			}
			public string Search(Specialist.Entities.Filter.GroupFilter filter){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Search");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("filter", filter);
								
				return _url.Action("Search", "Group", routeValues);
			}
					void TypeCheckSearchSubmit3(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.SearchSubmit((Specialist.Entities.Filter.GroupFilter)null));
			}
			public string SearchSubmit(Specialist.Entities.Filter.GroupFilter filter){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchSubmit");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("filter", filter);
								
				return _url.Action("SearchSubmit", "Group", routeValues);
			}
					void TypeCheckList4(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.List((Specialist.Entities.Filter.GroupFilter)null, (System.Nullable<System.Int32>)null));
			}
			public string List(Specialist.Entities.Filter.GroupFilter filter, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("filter", filter);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("List", "Group", routeValues);
			}
					void TypeCheckListPdf5(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.ListPdf((Specialist.Entities.Filter.GroupFilter)null));
			}
			public string ListPdf(Specialist.Entities.Filter.GroupFilter filter){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ListPdf");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("filter", filter);
								
				return _url.Action("ListPdf", "Group", routeValues);
			}
					void TypeCheckJubileeDiscounts6(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.JubileeDiscounts());
			}
			public string JubileeDiscounts(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "JubileeDiscounts");
			//	routeValues.Add("controller", "Group");
								
				return _url.Action("JubileeDiscounts", "Group", routeValues);
			}
					void TypeCheckWithDiscount7(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.WithDiscount((System.String)null));
			}
			public string WithDiscount(System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "WithDiscount");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("WithDiscount", "Group", routeValues);
			}
					void TypeCheckCalendar8(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.Calendar(0));
			}
			public string Calendar(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Calendar");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("Calendar", "Group", routeValues);
			}
					void TypeCheckCurrentAttendance9(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.CurrentAttendance());
			}
			public string CurrentAttendance(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CurrentAttendance");
			//	routeValues.Add("controller", "Group");
								
				return _url.Action("CurrentAttendance", "Group", routeValues);
			}
					void TypeCheckVkGroup10(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.VkGroup(0, 0, (System.String)null));
			}
			public string VkGroup(System.Decimal groupId, System.Decimal captchaId, System.String captchaKey){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "VkGroup");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("groupId", groupId);
									routeValues.Add("captchaId", captchaId);
									routeValues.Add("captchaKey", captchaKey);
								
				return _url.Action("VkGroup", "Group", routeValues);
			}
					void TypeCheckHotGroupsWithSort11(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.HotGroupsWithSort(0));
			}
			public string HotGroupsWithSort(System.Int32 sortType){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "HotGroupsWithSort");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("sortType", sortType);
								
				return _url.Action("HotGroupsWithSort", "Group", routeValues);
			}
					void TypeCheckForNews12(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.ForNews(0, 0));
			}
			public string ForNews(System.Int32 id, System.Int32 sortType){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ForNews");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("id", id);
									routeValues.Add("sortType", sortType);
								
				return _url.Action("ForNews", "Group", routeValues);
			}
					void TypeCheckForCourseTCListWithSort13(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.ForCourseTCListWithSort((System.String)null, false, 0, 0));
			}
			public string ForCourseTCListWithSort(System.String tcList, System.Boolean addGroups, System.Int32 titleType, System.Int32 sortType){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ForCourseTCListWithSort");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("tcList", tcList);
									routeValues.Add("addGroups", addGroups);
									routeValues.Add("titleType", titleType);
									routeValues.Add("sortType", sortType);
								
				return _url.Action("ForCourseTCListWithSort", "Group", routeValues);
			}
					void TypeCheckForCourseTCList14(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.ForCourseTCList((System.String)null, false, 0));
			}
			public string ForCourseTCList(System.String tcList, System.Boolean addGroups, System.Int32 titleType){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ForCourseTCList");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("tcList", tcList);
									routeValues.Add("addGroups", addGroups);
									routeValues.Add("titleType", titleType);
								
				return _url.Action("ForCourseTCList", "Group", routeValues);
			}
					void TypeCheckBuy15(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.Buy(0));
			}
			public string Buy(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Buy");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("id", id);
								
				return _url.Action("Buy", "Group", routeValues);
			}
					void TypeCheckBaseView16(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Group", routeValues);
			}
					void TypeCheckBaseView17(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Group", routeValues);
			}
					void TypeCheckBaseViewWithTitle18(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Group", routeValues);
			}
					void TypeCheckRedirectToAction19(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Group", routeValues);
			}
					void TypeCheckRedirectBack20(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Group");
								
				return _url.Action("RedirectBack", "Group", routeValues);
			}
					void TypeCheckMView21(){
				CheckMethod<Specialist.Web.Controllers.GroupController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Group");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Group", routeValues);
			}
				

		

		}


			public class GroupControllerLinks{
			
			private UrlHelper _url;

			public GroupControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static GroupControllerUrls _GroupControllerUrls = null;
			public GroupControllerUrls Urls { 
				get {
				//	if(_GroupControllerUrls == null) _GroupControllerUrls = new GroupControllerUrls(_url);
					//return _GroupControllerUrls;
					return new GroupControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.Decimal groupID, object content){
				var localActionUrl = _url.Group().Urls.Details(groupID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Videos(	System.Decimal groupId, object content){
				var localActionUrl = _url.Group().Urls.Videos(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Search(	Specialist.Entities.Filter.GroupFilter filter, object content){
				var localActionUrl = _url.Group().Urls.Search(filter);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchSubmit(	Specialist.Entities.Filter.GroupFilter filter, object content){
				var localActionUrl = _url.Group().Urls.SearchSubmit(filter);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA List(	Specialist.Entities.Filter.GroupFilter filter, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Group().Urls.List(filter, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ListPdf(	Specialist.Entities.Filter.GroupFilter filter, object content){
				var localActionUrl = _url.Group().Urls.ListPdf(filter);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA JubileeDiscounts(	object content){
				var localActionUrl = _url.Group().Urls.JubileeDiscounts();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA WithDiscount(	System.String courseTC, object content){
				var localActionUrl = _url.Group().Urls.WithDiscount(courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Calendar(	System.Decimal groupId, object content){
				var localActionUrl = _url.Group().Urls.Calendar(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CurrentAttendance(	object content){
				var localActionUrl = _url.Group().Urls.CurrentAttendance();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA VkGroup(	System.Decimal groupId, System.Decimal captchaId, System.String captchaKey, object content){
				var localActionUrl = _url.Group().Urls.VkGroup(groupId, captchaId, captchaKey);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA HotGroupsWithSort(	System.Int32 sortType, object content){
				var localActionUrl = _url.Group().Urls.HotGroupsWithSort(sortType);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ForNews(	System.Int32 id, System.Int32 sortType, object content){
				var localActionUrl = _url.Group().Urls.ForNews(id, sortType);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ForCourseTCListWithSort(	System.String tcList, System.Boolean addGroups, System.Int32 titleType, System.Int32 sortType, object content){
				var localActionUrl = _url.Group().Urls.ForCourseTCListWithSort(tcList, addGroups, titleType, sortType);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ForCourseTCList(	System.String tcList, System.Boolean addGroups, System.Int32 titleType, object content){
				var localActionUrl = _url.Group().Urls.ForCourseTCList(tcList, addGroups, titleType);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Buy(	System.Decimal id, object content){
				var localActionUrl = _url.Group().Urls.Buy(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Group().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Group().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Group().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Group().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Group().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Group().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static HomeControllerLinks _HomeControllerLinks = null;
		
		public static HomeControllerLinks Home(this UrlHelper urlHelper) {
		//	if(_HomeControllerLinks == null) _HomeControllerLinks = new HomeControllerLinks(urlHelper);
		//	return _HomeControllerLinks;
		return new HomeControllerLinks(urlHelper);
		}

		public class HomeControllerUrls{
			
			private UrlHelper _url;

			public HomeControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckIndex0(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.Index());
			}
			public string Index(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Index");
			//	routeValues.Add("controller", "Home");
								
				return _url.Action("Index", "Home", routeValues);
			}
					void TypeCheckNews1(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.News());
			}
			public string News(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "News");
			//	routeValues.Add("controller", "Home");
								
				return _url.Action("News", "Home", routeValues);
			}
					void TypeCheckSiteMap2(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.SiteMap());
			}
			public string SiteMap(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SiteMap");
			//	routeValues.Add("controller", "Home");
								
				return _url.Action("SiteMap", "Home", routeValues);
			}
					void TypeCheckMainPageResponseNew3(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.MainPageResponseNew());
			}
			public string MainPageResponseNew(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MainPageResponseNew");
			//	routeValues.Add("controller", "Home");
								
				return _url.Action("MainPageResponseNew", "Home", routeValues);
			}
					void TypeCheckMainPageResponse4(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.MainPageResponse());
			}
			public string MainPageResponse(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MainPageResponse");
			//	routeValues.Add("controller", "Home");
								
				return _url.Action("MainPageResponse", "Home", routeValues);
			}
					void TypeCheckBaseView5(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Home");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Home", routeValues);
			}
					void TypeCheckBaseView6(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Home");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Home", routeValues);
			}
					void TypeCheckBaseViewWithTitle7(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Home");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Home", routeValues);
			}
					void TypeCheckRedirectToAction8(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Home");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Home", routeValues);
			}
					void TypeCheckRedirectBack9(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Home");
								
				return _url.Action("RedirectBack", "Home", routeValues);
			}
					void TypeCheckMView10(){
				CheckMethod<Specialist.Web.Controllers.HomeController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Home");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Home", routeValues);
			}
				

		

		}


			public class HomeControllerLinks{
			
			private UrlHelper _url;

			public HomeControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static HomeControllerUrls _HomeControllerUrls = null;
			public HomeControllerUrls Urls { 
				get {
				//	if(_HomeControllerUrls == null) _HomeControllerUrls = new HomeControllerUrls(_url);
					//return _HomeControllerUrls;
					return new HomeControllerUrls(_url);
				}
			}

		
		
		


			public TagA Index(	object content){
				var localActionUrl = _url.Home().Urls.Index();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA News(	object content){
				var localActionUrl = _url.Home().Urls.News();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SiteMap(	object content){
				var localActionUrl = _url.Home().Urls.SiteMap();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MainPageResponseNew(	object content){
				var localActionUrl = _url.Home().Urls.MainPageResponseNew();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MainPageResponse(	object content){
				var localActionUrl = _url.Home().Urls.MainPageResponse();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Home().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Home().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Home().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Home().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Home().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Home().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static PageControllerLinks _PageControllerLinks = null;
		
		public static PageControllerLinks Page(this UrlHelper urlHelper) {
		//	if(_PageControllerLinks == null) _PageControllerLinks = new PageControllerLinks(urlHelper);
		//	return _PageControllerLinks;
		return new PageControllerLinks(urlHelper);
		}

		public class PageControllerUrls{
			
			private UrlHelper _url;

			public PageControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckVacanciesFor0(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.VacanciesFor());
			}
			public string VacanciesFor(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "VacanciesFor");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("VacanciesFor", "Page", routeValues);
			}
					void TypeCheckNewsFor1(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.NewsFor((System.Object)null));
			}
			public string NewsFor(System.Object obj){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "NewsFor");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("obj", obj);
								
				return _url.Action("NewsFor", "Page", routeValues);
			}
					void TypeCheckNewsForMain2(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.NewsForMain());
			}
			public string NewsForMain(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "NewsForMain");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("NewsForMain", "Page", routeValues);
			}
					void TypeCheckGetPoll3(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.GetPoll((System.Nullable<System.Int32>)null));
			}
			public string GetPoll(System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetPoll");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("id", id);
								
				return _url.Action("GetPoll", "Page", routeValues);
			}
					void TypeCheckPoll4(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.Poll());
			}
			public string Poll(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Poll");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("Poll", "Page", routeValues);
			}
					void TypeCheckPollVote5(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.PollVote(0, (System.String)null, 0));
			}
			public string PollVote(System.Int32 pollOptionID, System.String pollAnswer, System.Int32 pollID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PollVote");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("pollOptionID", pollOptionID);
									routeValues.Add("pollAnswer", pollAnswer);
									routeValues.Add("pollID", pollID);
								
				return _url.Action("PollVote", "Page", routeValues);
			}
					void TypeCheckBanner6(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.Banner());
			}
			public string Banner(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Banner");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("Banner", "Page", routeValues);
			}
					void TypeCheckNotFound7(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.NotFound((System.String)null));
			}
			public string NotFound(System.String aspxerrorpath){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "NotFound");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("aspxerrorpath", aspxerrorpath);
								
				return _url.Action("NotFound", "Page", routeValues);
			}
					void TypeCheckError8(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.Error((System.String)null));
			}
			public string Error(System.String aspxerrorpath){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Error");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("aspxerrorpath", aspxerrorpath);
								
				return _url.Action("Error", "Page", routeValues);
			}
					void TypeCheckAskTimetable9(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.AskTimetable());
			}
			public string AskTimetable(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AskTimetable");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("AskTimetable", "Page", routeValues);
			}
					void TypeCheckStudyTypes10(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.StudyTypes());
			}
			public string StudyTypes(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "StudyTypes");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("StudyTypes", "Page", routeValues);
			}
					void TypeCheckUserWorksFor11(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.UserWorksFor((System.Object)null));
			}
			public string UserWorksFor(System.Object obj){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UserWorksFor");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("obj", obj);
								
				return _url.Action("UserWorksFor", "Page", routeValues);
			}
					void TypeCheckJavaScriptError12(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.JavaScriptError((System.String)null, (System.String)null, (System.String)null));
			}
			public string JavaScriptError(System.String msg, System.String url, System.String line){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "JavaScriptError");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("msg", msg);
									routeValues.Add("url", url);
									routeValues.Add("line", line);
								
				return _url.Action("JavaScriptError", "Page", routeValues);
			}
					void TypeCheckNearestGroupMobile13(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.NearestGroupMobile());
			}
			public string NearestGroupMobile(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "NearestGroupMobile");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("NearestGroupMobile", "Page", routeValues);
			}
					void TypeCheckCityCoupon14(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.CityCoupon());
			}
			public string CityCoupon(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CityCoupon");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("CityCoupon", "Page", routeValues);
			}
					void TypeCheckMainMenu15(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.MainMenu());
			}
			public string MainMenu(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MainMenu");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("MainMenu", "Page", routeValues);
			}
					void TypeCheckSendFormTo16(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.SendFormTo((System.String)null));
			}
			public string SendFormTo(System.String sendformemail){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendFormTo");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("sendformemail", sendformemail);
								
				return _url.Action("SendFormTo", "Page", routeValues);
			}
					void TypeCheckProcess17(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.Process((System.String)null));
			}
			public string Process(System.String url){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Process");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("url", url);
								
				return _url.Action("Process", "Page", routeValues);
			}
					void TypeCheckSendForWebMaster18(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.SendForWebMaster((System.String)null));
			}
			public string SendForWebMaster(System.String title){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendForWebMaster");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("title", title);
								
				return _url.Action("SendForWebMaster", "Page", routeValues);
			}
					void TypeCheckTestResponse19(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.TestResponse((System.String)null));
			}
			public string TestResponse(System.String title){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TestResponse");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("title", title);
								
				return _url.Action("TestResponse", "Page", routeValues);
			}
					void TypeCheckSendInviteManager20(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.SendInviteManager());
			}
			public string SendInviteManager(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendInviteManager");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("SendInviteManager", "Page", routeValues);
			}
					void TypeCheckGetPromocode21(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.GetPromocode((System.String)null));
			}
			public string GetPromocode(System.String title){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetPromocode");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("title", title);
								
				return _url.Action("GetPromocode", "Page", routeValues);
			}
					void TypeCheckCourseIdea22(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.CourseIdea());
			}
			public string CourseIdea(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseIdea");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("CourseIdea", "Page", routeValues);
			}
					void TypeCheckMobileAppReview23(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.MobileAppReview());
			}
			public string MobileAppReview(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MobileAppReview");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("MobileAppReview", "Page", routeValues);
			}
					void TypeCheckCatalogIdea24(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.CatalogIdea());
			}
			public string CatalogIdea(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CatalogIdea");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("CatalogIdea", "Page", routeValues);
			}
					void TypeCheckWebinarSpecial25(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.WebinarSpecial());
			}
			public string WebinarSpecial(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "WebinarSpecial");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("WebinarSpecial", "Page", routeValues);
			}
					void TypeCheckCourseTender26(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.CourseTender());
			}
			public string CourseTender(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseTender");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("CourseTender", "Page", routeValues);
			}
					void TypeCheckEnglishOrder27(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.EnglishOrder(0));
			}
			public string EnglishOrder(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EnglishOrder");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("id", id);
								
				return _url.Action("EnglishOrder", "Page", routeValues);
			}
					void TypeCheckDevIdea28(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.DevIdea());
			}
			public string DevIdea(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DevIdea");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("DevIdea", "Page", routeValues);
			}
					void TypeCheckJobVacancy29(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.JobVacancy());
			}
			public string JobVacancy(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "JobVacancy");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("JobVacancy", "Page", routeValues);
			}
					void TypeCheckJobManager30(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.JobManager());
			}
			public string JobManager(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "JobManager");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("JobManager", "Page", routeValues);
			}
					void TypeCheckCareerDay31(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.CareerDay());
			}
			public string CareerDay(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CareerDay");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("CareerDay", "Page", routeValues);
			}
					void TypeCheckSendForWebMaster32(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.SendForWebMaster((Specialist.Web.Common.ViewModel.SendMessageVM)null));
			}
			public string SendForWebMaster(Specialist.Web.Common.ViewModel.SendMessageVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendForWebMaster");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("model", model);
								
				return _url.Action("SendForWebMaster", "Page", routeValues);
			}
					void TypeCheckSendForManager33(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.SendForManager((System.String)null));
			}
			public string SendForManager(System.String tc){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendForManager");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("tc", tc);
								
				return _url.Action("SendForManager", "Page", routeValues);
			}
					void TypeCheckSendExpressOrder34(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.SendExpressOrder((Specialist.Entities.Common.ViewModel.ExpressOrderVM)null));
			}
			public string SendExpressOrder(Specialist.Entities.Common.ViewModel.ExpressOrderVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendExpressOrder");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("model", model);
								
				return _url.Action("SendExpressOrder", "Page", routeValues);
			}
					void TypeCheckSendQuickRegistration35(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.SendQuickRegistration((Specialist.Web.ViewModel.Orders.QuickRegisterVM)null));
			}
			public string SendQuickRegistration(Specialist.Web.ViewModel.Orders.QuickRegisterVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendQuickRegistration");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("model", model);
								
				return _url.Action("SendQuickRegistration", "Page", routeValues);
			}
					void TypeCheckSendMessage36(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.SendMessage((System.String)null, (System.String)null));
			}
			public string SendMessage(System.String text, System.String url){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendMessage");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("text", text);
									routeValues.Add("url", url);
								
				return _url.Action("SendMessage", "Page", routeValues);
			}
					void TypeCheckTopSearch37(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.TopSearch());
			}
			public string TopSearch(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TopSearch");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("TopSearch", "Page", routeValues);
			}
					void TypeCheckSearch38(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.Search((System.String)null, (System.Nullable<System.Int32>)null));
			}
			public string Search(System.String text, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Search");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("text", text);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("Search", "Page", routeValues);
			}
					void TypeCheckHotGroupsFor39(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.HotGroupsFor((System.Object)null));
			}
			public string HotGroupsFor(System.Object obj){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "HotGroupsFor");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("obj", obj);
								
				return _url.Action("HotGroupsFor", "Page", routeValues);
			}
					void TypeCheckHotGroupsForMain40(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.HotGroupsForMain((System.String)null));
			}
			public string HotGroupsForMain(System.String viewName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "HotGroupsForMain");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("viewName", viewName);
								
				return _url.Action("HotGroupsForMain", "Page", routeValues);
			}
					void TypeCheckVideoFor41(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.VideoFor((System.Object)null));
			}
			public string VideoFor(System.Object obj){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "VideoFor");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("obj", obj);
								
				return _url.Action("VideoFor", "Page", routeValues);
			}
					void TypeCheckGuideFor42(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.GuideFor((System.Object)null));
			}
			public string GuideFor(System.Object obj){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GuideFor");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("obj", obj);
								
				return _url.Action("GuideFor", "Page", routeValues);
			}
					void TypeCheckBaseView43(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Page", routeValues);
			}
					void TypeCheckBaseView44(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Page", routeValues);
			}
					void TypeCheckBaseViewWithTitle45(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Page", routeValues);
			}
					void TypeCheckRedirectToAction46(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Page", routeValues);
			}
					void TypeCheckRedirectBack47(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Page");
								
				return _url.Action("RedirectBack", "Page", routeValues);
			}
					void TypeCheckMView48(){
				CheckMethod<Specialist.Web.Controllers.PageController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Page");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Page", routeValues);
			}
				

		

		}


			public class PageControllerLinks{
			
			private UrlHelper _url;

			public PageControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static PageControllerUrls _PageControllerUrls = null;
			public PageControllerUrls Urls { 
				get {
				//	if(_PageControllerUrls == null) _PageControllerUrls = new PageControllerUrls(_url);
					//return _PageControllerUrls;
					return new PageControllerUrls(_url);
				}
			}

		
		
		


			public TagA VacanciesFor(	object content){
				var localActionUrl = _url.Page().Urls.VacanciesFor();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA NewsFor(	System.Object obj, object content){
				var localActionUrl = _url.Page().Urls.NewsFor(obj);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA NewsForMain(	object content){
				var localActionUrl = _url.Page().Urls.NewsForMain();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetPoll(	System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.Page().Urls.GetPoll(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Poll(	object content){
				var localActionUrl = _url.Page().Urls.Poll();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PollVote(	System.Int32 pollOptionID, System.String pollAnswer, System.Int32 pollID, object content){
				var localActionUrl = _url.Page().Urls.PollVote(pollOptionID, pollAnswer, pollID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Banner(	object content){
				var localActionUrl = _url.Page().Urls.Banner();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA NotFound(	System.String aspxerrorpath, object content){
				var localActionUrl = _url.Page().Urls.NotFound(aspxerrorpath);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Error(	System.String aspxerrorpath, object content){
				var localActionUrl = _url.Page().Urls.Error(aspxerrorpath);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AskTimetable(	object content){
				var localActionUrl = _url.Page().Urls.AskTimetable();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA StudyTypes(	object content){
				var localActionUrl = _url.Page().Urls.StudyTypes();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UserWorksFor(	System.Object obj, object content){
				var localActionUrl = _url.Page().Urls.UserWorksFor(obj);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA JavaScriptError(	System.String msg, System.String url, System.String line, object content){
				var localActionUrl = _url.Page().Urls.JavaScriptError(msg, url, line);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA NearestGroupMobile(	object content){
				var localActionUrl = _url.Page().Urls.NearestGroupMobile();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CityCoupon(	object content){
				var localActionUrl = _url.Page().Urls.CityCoupon();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MainMenu(	object content){
				var localActionUrl = _url.Page().Urls.MainMenu();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendFormTo(	System.String sendformemail, object content){
				var localActionUrl = _url.Page().Urls.SendFormTo(sendformemail);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Process(	System.String url, object content){
				var localActionUrl = _url.Page().Urls.Process(url);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendForWebMaster(	System.String title, object content){
				var localActionUrl = _url.Page().Urls.SendForWebMaster(title);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TestResponse(	System.String title, object content){
				var localActionUrl = _url.Page().Urls.TestResponse(title);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendInviteManager(	object content){
				var localActionUrl = _url.Page().Urls.SendInviteManager();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetPromocode(	System.String title, object content){
				var localActionUrl = _url.Page().Urls.GetPromocode(title);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseIdea(	object content){
				var localActionUrl = _url.Page().Urls.CourseIdea();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MobileAppReview(	object content){
				var localActionUrl = _url.Page().Urls.MobileAppReview();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CatalogIdea(	object content){
				var localActionUrl = _url.Page().Urls.CatalogIdea();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA WebinarSpecial(	object content){
				var localActionUrl = _url.Page().Urls.WebinarSpecial();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseTender(	object content){
				var localActionUrl = _url.Page().Urls.CourseTender();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EnglishOrder(	System.Int32 id, object content){
				var localActionUrl = _url.Page().Urls.EnglishOrder(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DevIdea(	object content){
				var localActionUrl = _url.Page().Urls.DevIdea();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA JobVacancy(	object content){
				var localActionUrl = _url.Page().Urls.JobVacancy();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA JobManager(	object content){
				var localActionUrl = _url.Page().Urls.JobManager();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CareerDay(	object content){
				var localActionUrl = _url.Page().Urls.CareerDay();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendForWebMaster(	Specialist.Web.Common.ViewModel.SendMessageVM model, object content){
				var localActionUrl = _url.Page().Urls.SendForWebMaster(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendForManager(	System.String tc, object content){
				var localActionUrl = _url.Page().Urls.SendForManager(tc);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendExpressOrder(	Specialist.Entities.Common.ViewModel.ExpressOrderVM model, object content){
				var localActionUrl = _url.Page().Urls.SendExpressOrder(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendQuickRegistration(	Specialist.Web.ViewModel.Orders.QuickRegisterVM model, object content){
				var localActionUrl = _url.Page().Urls.SendQuickRegistration(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendMessage(	System.String text, System.String url, object content){
				var localActionUrl = _url.Page().Urls.SendMessage(text, url);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TopSearch(	object content){
				var localActionUrl = _url.Page().Urls.TopSearch();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Search(	System.String text, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Page().Urls.Search(text, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA HotGroupsFor(	System.Object obj, object content){
				var localActionUrl = _url.Page().Urls.HotGroupsFor(obj);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA HotGroupsForMain(	System.String viewName, object content){
				var localActionUrl = _url.Page().Urls.HotGroupsForMain(viewName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA VideoFor(	System.Object obj, object content){
				var localActionUrl = _url.Page().Urls.VideoFor(obj);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GuideFor(	System.Object obj, object content){
				var localActionUrl = _url.Page().Urls.GuideFor(obj);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Page().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Page().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Page().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Page().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Page().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Page().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static ProductControllerLinks _ProductControllerLinks = null;
		
		public static ProductControllerLinks Product(this UrlHelper urlHelper) {
		//	if(_ProductControllerLinks == null) _ProductControllerLinks = new ProductControllerLinks(urlHelper);
		//	return _ProductControllerLinks;
		return new ProductControllerLinks(urlHelper);
		}

		public class ProductControllerUrls{
			
			private UrlHelper _url;

			public ProductControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.ProductController>(c => 
					c.Details((System.String)null));
			}
			public string Details(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Product");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Details", "Product", routeValues);
			}
					void TypeCheckUserWorks1(){
				CheckMethod<Specialist.Web.Controllers.ProductController>(c => 
					c.UserWorks());
			}
			public string UserWorks(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UserWorks");
			//	routeValues.Add("controller", "Product");
								
				return _url.Action("UserWorks", "Product", routeValues);
			}
					void TypeCheckRedirectToAction2(){
				CheckMethod<Specialist.Web.Controllers.ProductController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Product");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Product", routeValues);
			}
					void TypeCheckRedirectBack3(){
				CheckMethod<Specialist.Web.Controllers.ProductController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Product");
								
				return _url.Action("RedirectBack", "Product", routeValues);
			}
					void TypeCheckMView4(){
				CheckMethod<Specialist.Web.Controllers.ProductController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Product");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Product", routeValues);
			}
				

		

		}


			public class ProductControllerLinks{
			
			private UrlHelper _url;

			public ProductControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static ProductControllerUrls _ProductControllerUrls = null;
			public ProductControllerUrls Urls { 
				get {
				//	if(_ProductControllerUrls == null) _ProductControllerUrls = new ProductControllerUrls(_url);
					//return _ProductControllerUrls;
					return new ProductControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.String urlName, object content){
				var localActionUrl = _url.Product().Urls.Details(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UserWorks(	object content){
				var localActionUrl = _url.Product().Urls.UserWorks();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Product().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Product().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Product().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static ProfessionControllerLinks _ProfessionControllerLinks = null;
		
		public static ProfessionControllerLinks Profession(this UrlHelper urlHelper) {
		//	if(_ProfessionControllerLinks == null) _ProfessionControllerLinks = new ProfessionControllerLinks(urlHelper);
		//	return _ProfessionControllerLinks;
		return new ProfessionControllerLinks(urlHelper);
		}

		public class ProfessionControllerUrls{
			
			private UrlHelper _url;

			public ProfessionControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.ProfessionController>(c => 
					c.Details((System.String)null));
			}
			public string Details(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Profession");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Details", "Profession", routeValues);
			}
					void TypeCheckProfessions1(){
				CheckMethod<Specialist.Web.Controllers.ProfessionController>(c => 
					c.Professions((System.String)null));
			}
			public string Professions(System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Professions");
			//	routeValues.Add("controller", "Profession");
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("Professions", "Profession", routeValues);
			}
					void TypeCheckRedirectToAction2(){
				CheckMethod<Specialist.Web.Controllers.ProfessionController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Profession");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Profession", routeValues);
			}
					void TypeCheckRedirectBack3(){
				CheckMethod<Specialist.Web.Controllers.ProfessionController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Profession");
								
				return _url.Action("RedirectBack", "Profession", routeValues);
			}
					void TypeCheckMView4(){
				CheckMethod<Specialist.Web.Controllers.ProfessionController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Profession");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Profession", routeValues);
			}
				

		

		}


			public class ProfessionControllerLinks{
			
			private UrlHelper _url;

			public ProfessionControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static ProfessionControllerUrls _ProfessionControllerUrls = null;
			public ProfessionControllerUrls Urls { 
				get {
				//	if(_ProfessionControllerUrls == null) _ProfessionControllerUrls = new ProfessionControllerUrls(_url);
					//return _ProfessionControllerUrls;
					return new ProfessionControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.String urlName, object content){
				var localActionUrl = _url.Profession().Urls.Details(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Professions(	System.String courseTC, object content){
				var localActionUrl = _url.Profession().Urls.Professions(courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Profession().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Profession().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Profession().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static SectionControllerLinks _SectionControllerLinks = null;
		
		public static SectionControllerLinks Section(this UrlHelper urlHelper) {
		//	if(_SectionControllerLinks == null) _SectionControllerLinks = new SectionControllerLinks(urlHelper);
		//	return _SectionControllerLinks;
		return new SectionControllerLinks(urlHelper);
		}

		public class SectionControllerUrls{
			
			private UrlHelper _url;

			public SectionControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.SectionController>(c => 
					c.Details((System.String)null));
			}
			public string Details(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Section");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Details", "Section", routeValues);
			}
					void TypeCheckMainPageSections1(){
				CheckMethod<Specialist.Web.Controllers.SectionController>(c => 
					c.MainPageSections());
			}
			public string MainPageSections(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MainPageSections");
			//	routeValues.Add("controller", "Section");
								
				return _url.Action("MainPageSections", "Section", routeValues);
			}
					void TypeCheckUserWorks2(){
				CheckMethod<Specialist.Web.Controllers.SectionController>(c => 
					c.UserWorks(0));
			}
			public string UserWorks(System.Int32 sectionId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UserWorks");
			//	routeValues.Add("controller", "Section");
									routeValues.Add("sectionId", sectionId);
								
				return _url.Action("UserWorks", "Section", routeValues);
			}
					void TypeCheckResponses3(){
				CheckMethod<Specialist.Web.Controllers.SectionController>(c => 
					c.Responses(0));
			}
			public string Responses(System.Int32 sectionId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Responses");
			//	routeValues.Add("controller", "Section");
									routeValues.Add("sectionId", sectionId);
								
				return _url.Action("Responses", "Section", routeValues);
			}
					void TypeCheckSectionsResponses4(){
				CheckMethod<Specialist.Web.Controllers.SectionController>(c => 
					c.SectionsResponses());
			}
			public string SectionsResponses(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SectionsResponses");
			//	routeValues.Add("controller", "Section");
								
				return _url.Action("SectionsResponses", "Section", routeValues);
			}
					void TypeCheckRedirectToAction5(){
				CheckMethod<Specialist.Web.Controllers.SectionController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Section");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Section", routeValues);
			}
					void TypeCheckRedirectBack6(){
				CheckMethod<Specialist.Web.Controllers.SectionController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Section");
								
				return _url.Action("RedirectBack", "Section", routeValues);
			}
					void TypeCheckMView7(){
				CheckMethod<Specialist.Web.Controllers.SectionController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Section");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Section", routeValues);
			}
				

		

		}


			public class SectionControllerLinks{
			
			private UrlHelper _url;

			public SectionControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static SectionControllerUrls _SectionControllerUrls = null;
			public SectionControllerUrls Urls { 
				get {
				//	if(_SectionControllerUrls == null) _SectionControllerUrls = new SectionControllerUrls(_url);
					//return _SectionControllerUrls;
					return new SectionControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.String urlName, object content){
				var localActionUrl = _url.Section().Urls.Details(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MainPageSections(	object content){
				var localActionUrl = _url.Section().Urls.MainPageSections();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UserWorks(	System.Int32 sectionId, object content){
				var localActionUrl = _url.Section().Urls.UserWorks(sectionId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Responses(	System.Int32 sectionId, object content){
				var localActionUrl = _url.Section().Urls.Responses(sectionId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SectionsResponses(	object content){
				var localActionUrl = _url.Section().Urls.SectionsResponses();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Section().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Section().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Section().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static TrackControllerLinks _TrackControllerLinks = null;
		
		public static TrackControllerLinks Track(this UrlHelper urlHelper) {
		//	if(_TrackControllerLinks == null) _TrackControllerLinks = new TrackControllerLinks(urlHelper);
		//	return _TrackControllerLinks;
		return new TrackControllerLinks(urlHelper);
		}

		public class TrackControllerUrls{
			
			private UrlHelper _url;

			public TrackControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.TrackController>(c => 
					c.Details((System.String)null));
			}
			public string Details(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Track");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Details", "Track", routeValues);
			}
					void TypeCheckList1(){
				CheckMethod<Specialist.Web.Controllers.TrackController>(c => 
					c.List((System.String)null));
			}
			public string List(System.String id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "Track");
									routeValues.Add("id", id);
								
				return _url.Action("List", "Track", routeValues);
			}
					void TypeCheckRedirectToAction2(){
				CheckMethod<Specialist.Web.Controllers.TrackController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Track");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Track", routeValues);
			}
					void TypeCheckRedirectBack3(){
				CheckMethod<Specialist.Web.Controllers.TrackController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Track");
								
				return _url.Action("RedirectBack", "Track", routeValues);
			}
					void TypeCheckMView4(){
				CheckMethod<Specialist.Web.Controllers.TrackController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Track");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Track", routeValues);
			}
				

		

		}


			public class TrackControllerLinks{
			
			private UrlHelper _url;

			public TrackControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static TrackControllerUrls _TrackControllerUrls = null;
			public TrackControllerUrls Urls { 
				get {
				//	if(_TrackControllerUrls == null) _TrackControllerUrls = new TrackControllerUrls(_url);
					//return _TrackControllerUrls;
					return new TrackControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.String urlName, object content){
				var localActionUrl = _url.Track().Urls.Details(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA List(	System.String id, object content){
				var localActionUrl = _url.Track().Urls.List(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Track().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Track().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Track().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static EmployeeControllerLinks _EmployeeControllerLinks = null;
		
		public static EmployeeControllerLinks Employee(this UrlHelper urlHelper) {
		//	if(_EmployeeControllerLinks == null) _EmployeeControllerLinks = new EmployeeControllerLinks(urlHelper);
		//	return _EmployeeControllerLinks;
		return new EmployeeControllerLinks(urlHelper);
		}

		public class EmployeeControllerUrls{
			
			private UrlHelper _url;

			public EmployeeControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckTrainer0(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.Trainer((System.String)null));
			}
			public string Trainer(System.String employeeTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Trainer");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("employeeTC", employeeTC);
								
				return _url.Action("Trainer", "Employee", routeValues);
			}
					void TypeCheckTrainersJson1(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.TrainersJson((System.String)null));
			}
			public string TrainersJson(System.String query){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TrainersJson");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("query", query);
								
				return _url.Action("TrainersJson", "Employee", routeValues);
			}
					void TypeCheckAboutTrainer2(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.AboutTrainer((System.String)null, (System.String)null, (System.Nullable<System.Int32>)null));
			}
			public string AboutTrainer(System.String employeeTC, System.String urlName, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AboutTrainer");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("employeeTC", employeeTC);
									routeValues.Add("urlName", urlName);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("AboutTrainer", "Employee", routeValues);
			}
					void TypeCheckManager3(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.Manager((System.String)null));
			}
			public string Manager(System.String employeeTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Manager");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("employeeTC", employeeTC);
								
				return _url.Action("Manager", "Employee", routeValues);
			}
					void TypeCheckGroups4(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.Groups());
			}
			public string Groups(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Groups");
			//	routeValues.Add("controller", "Employee");
								
				return _url.Action("Groups", "Employee", routeValues);
			}
					void TypeCheckCourses5(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.Courses());
			}
			public string Courses(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Courses");
			//	routeValues.Add("controller", "Employee");
								
				return _url.Action("Courses", "Employee", routeValues);
			}
					void TypeCheckAddResponse6(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.AddResponse((System.String)null));
			}
			public string AddResponse(System.String employeeTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddResponse");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("employeeTC", employeeTC);
								
				return _url.Action("AddResponse", "Employee", routeValues);
			}
					void TypeCheckAddResponsePost7(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.AddResponsePost((Specialist.Web.Root.Center.ViewModels.AddResponseVM)null));
			}
			public string AddResponsePost(Specialist.Web.Root.Center.ViewModels.AddResponseVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddResponsePost");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("model", model);
								
				return _url.Action("AddResponsePost", "Employee", routeValues);
			}
					void TypeCheckBaseView8(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Employee", routeValues);
			}
					void TypeCheckBaseView9(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Employee", routeValues);
			}
					void TypeCheckBaseViewWithTitle10(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Employee", routeValues);
			}
					void TypeCheckRedirectToAction11(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Employee", routeValues);
			}
					void TypeCheckRedirectBack12(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Employee");
								
				return _url.Action("RedirectBack", "Employee", routeValues);
			}
					void TypeCheckMView13(){
				CheckMethod<Specialist.Web.Controllers.EmployeeController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Employee");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Employee", routeValues);
			}
				

		

		}


			public class EmployeeControllerLinks{
			
			private UrlHelper _url;

			public EmployeeControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static EmployeeControllerUrls _EmployeeControllerUrls = null;
			public EmployeeControllerUrls Urls { 
				get {
				//	if(_EmployeeControllerUrls == null) _EmployeeControllerUrls = new EmployeeControllerUrls(_url);
					//return _EmployeeControllerUrls;
					return new EmployeeControllerUrls(_url);
				}
			}

		
		
		


			public TagA Trainer(	System.String employeeTC, object content){
				var localActionUrl = _url.Employee().Urls.Trainer(employeeTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TrainersJson(	System.String query, object content){
				var localActionUrl = _url.Employee().Urls.TrainersJson(query);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AboutTrainer(	System.String employeeTC, System.String urlName, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Employee().Urls.AboutTrainer(employeeTC, urlName, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Manager(	System.String employeeTC, object content){
				var localActionUrl = _url.Employee().Urls.Manager(employeeTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Groups(	object content){
				var localActionUrl = _url.Employee().Urls.Groups();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Courses(	object content){
				var localActionUrl = _url.Employee().Urls.Courses();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddResponse(	System.String employeeTC, object content){
				var localActionUrl = _url.Employee().Urls.AddResponse(employeeTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddResponsePost(	Specialist.Web.Root.Center.ViewModels.AddResponseVM model, object content){
				var localActionUrl = _url.Employee().Urls.AddResponsePost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Employee().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Employee().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Employee().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Employee().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Employee().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Employee().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static VendorControllerLinks _VendorControllerLinks = null;
		
		public static VendorControllerLinks Vendor(this UrlHelper urlHelper) {
		//	if(_VendorControllerLinks == null) _VendorControllerLinks = new VendorControllerLinks(urlHelper);
		//	return _VendorControllerLinks;
		return new VendorControllerLinks(urlHelper);
		}

		public class VendorControllerUrls{
			
			private UrlHelper _url;

			public VendorControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.VendorController>(c => 
					c.Details((System.String)null, (System.String)null, (System.Nullable<System.Int32>)null));
			}
			public string Details(System.String urlName, System.String urlPart, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Vendor");
									routeValues.Add("urlName", urlName);
									routeValues.Add("urlPart", urlPart);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("Details", "Vendor", routeValues);
			}
					void TypeCheckRedirectToAction1(){
				CheckMethod<Specialist.Web.Controllers.VendorController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Vendor");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Vendor", routeValues);
			}
					void TypeCheckRedirectBack2(){
				CheckMethod<Specialist.Web.Controllers.VendorController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Vendor");
								
				return _url.Action("RedirectBack", "Vendor", routeValues);
			}
					void TypeCheckMView3(){
				CheckMethod<Specialist.Web.Controllers.VendorController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Vendor");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Vendor", routeValues);
			}
				

		

		}


			public class VendorControllerLinks{
			
			private UrlHelper _url;

			public VendorControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static VendorControllerUrls _VendorControllerUrls = null;
			public VendorControllerUrls Urls { 
				get {
				//	if(_VendorControllerUrls == null) _VendorControllerUrls = new VendorControllerUrls(_url);
					//return _VendorControllerUrls;
					return new VendorControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.String urlName, System.String urlPart, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Vendor().Urls.Details(urlName, urlPart, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Vendor().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Vendor().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Vendor().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static GroupTestControllerLinks _GroupTestControllerLinks = null;
		
		public static GroupTestControllerLinks GroupTest(this UrlHelper urlHelper) {
		//	if(_GroupTestControllerLinks == null) _GroupTestControllerLinks = new GroupTestControllerLinks(urlHelper);
		//	return _GroupTestControllerLinks;
		return new GroupTestControllerLinks(urlHelper);
		}

		public class GroupTestControllerUrls{
			
			private UrlHelper _url;

			public GroupTestControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckPrepare0(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.Prepare());
			}
			public string Prepare(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Prepare");
			//	routeValues.Add("controller", "GroupTest");
								
				return _url.Action("Prepare", "GroupTest", routeValues);
			}
					void TypeCheckPrepare1(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.Prepare((Specialist.Web.Root.Tests.ViewModels.GroupPrepareVM)null));
			}
			public string Prepare(Specialist.Web.Root.Tests.ViewModels.GroupPrepareVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Prepare");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("model", model);
								
				return _url.Action("Prepare", "GroupTest", routeValues);
			}
					void TypeCheckGetGroupTests2(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.GetGroupTests(0, (Specialist.Web.Pages.AjaxGridRequest)null));
			}
			public string GetGroupTests(System.Int32 groupInfoId, Specialist.Web.Pages.AjaxGridRequest model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetGroupTests");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupInfoId", groupInfoId);
									routeValues.Add("model", model);
								
				return _url.Action("GetGroupTests", "GroupTest", routeValues);
			}
					void TypeCheckList3(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.List());
			}
			public string List(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "GroupTest");
								
				return _url.Action("List", "GroupTest", routeValues);
			}
					void TypeCheckResult4(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.Result(0));
			}
			public string Result(System.Int32 groupInfoId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Result");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupInfoId", groupInfoId);
								
				return _url.Action("Result", "GroupTest", routeValues);
			}
					void TypeCheckDownloadResult5(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.DownloadResult(0));
			}
			public string DownloadResult(System.Int32 groupInfoId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadResult");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupInfoId", groupInfoId);
								
				return _url.Action("DownloadResult", "GroupTest", routeValues);
			}
					void TypeCheckGroupInfo6(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.GroupInfo(0));
			}
			public string GroupInfo(System.Int32 groupInfoId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupInfo");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupInfoId", groupInfoId);
								
				return _url.Action("GroupInfo", "GroupTest", routeValues);
			}
					void TypeCheckGroupInfoComplete7(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.GroupInfoComplete(0));
			}
			public string GroupInfoComplete(System.Int32 groupInfoId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupInfoComplete");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupInfoId", groupInfoId);
								
				return _url.Action("GroupInfoComplete", "GroupTest", routeValues);
			}
					void TypeCheckRegisterUsers8(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.RegisterUsers(0));
			}
			public string RegisterUsers(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RegisterUsers");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("RegisterUsers", "GroupTest", routeValues);
			}
					void TypeCheckSendGroupTestInfo9(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.SendGroupTestInfo(0, false));
			}
			public string SendGroupTestInfo(System.Int32 groupInfoId, System.Boolean forManager){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendGroupTestInfo");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupInfoId", groupInfoId);
									routeValues.Add("forManager", forManager);
								
				return _url.Action("SendGroupTestInfo", "GroupTest", routeValues);
			}
					void TypeCheckDeleteGroupTest10(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.DeleteGroupTest((System.Nullable<System.Int32>)null));
			}
			public string DeleteGroupTest(System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteGroupTest");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("id", id);
								
				return _url.Action("DeleteGroupTest", "GroupTest", routeValues);
			}
					void TypeCheckEditGroupTest11(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.EditGroupTest(0, (System.Nullable<System.Int32>)null));
			}
			public string EditGroupTest(System.Int32 groupInfoId, System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditGroupTest");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupInfoId", groupInfoId);
									routeValues.Add("id", id);
								
				return _url.Action("EditGroupTest", "GroupTest", routeValues);
			}
					void TypeCheckEditGroupInfo12(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.EditGroupInfo(0, (System.String)null));
			}
			public string EditGroupInfo(System.Int32 id, System.String notes){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditGroupInfo");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("id", id);
									routeValues.Add("notes", notes);
								
				return _url.Action("EditGroupInfo", "GroupTest", routeValues);
			}
					void TypeCheckEditGroupTest13(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.EditGroupTest((Specialist.Web.Root.Tests.ViewModels.GroupTestEditVM)null));
			}
			public string EditGroupTest(Specialist.Web.Root.Tests.ViewModels.GroupTestEditVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditGroupTest");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("model", model);
								
				return _url.Action("EditGroupTest", "GroupTest", routeValues);
			}
					void TypeCheckGetTestsAuto14(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.GetTestsAuto(0, (System.String)null));
			}
			public string GetTestsAuto(System.Int32 groupInfoId, System.String term){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetTestsAuto");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupInfoId", groupInfoId);
									routeValues.Add("term", term);
								
				return _url.Action("GetTestsAuto", "GroupTest", routeValues);
			}
					void TypeCheckPlanTestUserStats15(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.PlanTestUserStats(0));
			}
			public string PlanTestUserStats(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PlanTestUserStats");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("PlanTestUserStats", "GroupTest", routeValues);
			}
					void TypeCheckPlanTestQuestionStats16(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.PlanTestQuestionStats(0));
			}
			public string PlanTestQuestionStats(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PlanTestQuestionStats");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("PlanTestQuestionStats", "GroupTest", routeValues);
			}
					void TypeCheckBaseView17(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "GroupTest", routeValues);
			}
					void TypeCheckBaseView18(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "GroupTest", routeValues);
			}
					void TypeCheckBaseViewWithTitle19(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "GroupTest", routeValues);
			}
					void TypeCheckRedirectToAction20(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "GroupTest", routeValues);
			}
					void TypeCheckRedirectBack21(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "GroupTest");
								
				return _url.Action("RedirectBack", "GroupTest", routeValues);
			}
					void TypeCheckMView22(){
				CheckMethod<Specialist.Web.Controllers.Tests.GroupTestController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "GroupTest");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "GroupTest", routeValues);
			}
				

		

		}


			public class GroupTestControllerLinks{
			
			private UrlHelper _url;

			public GroupTestControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static GroupTestControllerUrls _GroupTestControllerUrls = null;
			public GroupTestControllerUrls Urls { 
				get {
				//	if(_GroupTestControllerUrls == null) _GroupTestControllerUrls = new GroupTestControllerUrls(_url);
					//return _GroupTestControllerUrls;
					return new GroupTestControllerUrls(_url);
				}
			}

		
		
		


			public TagA Prepare(	object content){
				var localActionUrl = _url.GroupTest().Urls.Prepare();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Prepare(	Specialist.Web.Root.Tests.ViewModels.GroupPrepareVM model, object content){
				var localActionUrl = _url.GroupTest().Urls.Prepare(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetGroupTests(	System.Int32 groupInfoId, Specialist.Web.Pages.AjaxGridRequest model, object content){
				var localActionUrl = _url.GroupTest().Urls.GetGroupTests(groupInfoId, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA List(	object content){
				var localActionUrl = _url.GroupTest().Urls.List();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Result(	System.Int32 groupInfoId, object content){
				var localActionUrl = _url.GroupTest().Urls.Result(groupInfoId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DownloadResult(	System.Int32 groupInfoId, object content){
				var localActionUrl = _url.GroupTest().Urls.DownloadResult(groupInfoId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupInfo(	System.Int32 groupInfoId, object content){
				var localActionUrl = _url.GroupTest().Urls.GroupInfo(groupInfoId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupInfoComplete(	System.Int32 groupInfoId, object content){
				var localActionUrl = _url.GroupTest().Urls.GroupInfoComplete(groupInfoId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RegisterUsers(	System.Decimal groupId, object content){
				var localActionUrl = _url.GroupTest().Urls.RegisterUsers(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendGroupTestInfo(	System.Int32 groupInfoId, System.Boolean forManager, object content){
				var localActionUrl = _url.GroupTest().Urls.SendGroupTestInfo(groupInfoId, forManager);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteGroupTest(	System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.GroupTest().Urls.DeleteGroupTest(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditGroupTest(	System.Int32 groupInfoId, System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.GroupTest().Urls.EditGroupTest(groupInfoId, id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditGroupInfo(	System.Int32 id, System.String notes, object content){
				var localActionUrl = _url.GroupTest().Urls.EditGroupInfo(id, notes);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditGroupTest(	Specialist.Web.Root.Tests.ViewModels.GroupTestEditVM model, object content){
				var localActionUrl = _url.GroupTest().Urls.EditGroupTest(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetTestsAuto(	System.Int32 groupInfoId, System.String term, object content){
				var localActionUrl = _url.GroupTest().Urls.GetTestsAuto(groupInfoId, term);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PlanTestUserStats(	System.Decimal groupId, object content){
				var localActionUrl = _url.GroupTest().Urls.PlanTestUserStats(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PlanTestQuestionStats(	System.Decimal groupId, object content){
				var localActionUrl = _url.GroupTest().Urls.PlanTestQuestionStats(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.GroupTest().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.GroupTest().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.GroupTest().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.GroupTest().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.GroupTest().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.GroupTest().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static SimpleTestControllerLinks _SimpleTestControllerLinks = null;
		
		public static SimpleTestControllerLinks SimpleTest(this UrlHelper urlHelper) {
		//	if(_SimpleTestControllerLinks == null) _SimpleTestControllerLinks = new SimpleTestControllerLinks(urlHelper);
		//	return _SimpleTestControllerLinks;
		return new SimpleTestControllerLinks(urlHelper);
		}

		public class SimpleTestControllerUrls{
			
			private UrlHelper _url;

			public SimpleTestControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.Tests.SimpleTestController>(c => 
					c.Details(0));
			}
			public string Details(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "SimpleTest");
									routeValues.Add("id", id);
								
				return _url.Action("Details", "SimpleTest", routeValues);
			}
					void TypeCheckResult1(){
				CheckMethod<Specialist.Web.Controllers.Tests.SimpleTestController>(c => 
					c.Result(0, 0));
			}
			public string Result(System.Int32 id, System.Int32 resultId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Result");
			//	routeValues.Add("controller", "SimpleTest");
									routeValues.Add("id", id);
									routeValues.Add("resultId", resultId);
								
				return _url.Action("Result", "SimpleTest", routeValues);
			}
					void TypeCheckBaseView2(){
				CheckMethod<Specialist.Web.Controllers.Tests.SimpleTestController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "SimpleTest");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "SimpleTest", routeValues);
			}
					void TypeCheckBaseView3(){
				CheckMethod<Specialist.Web.Controllers.Tests.SimpleTestController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "SimpleTest");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "SimpleTest", routeValues);
			}
					void TypeCheckBaseViewWithTitle4(){
				CheckMethod<Specialist.Web.Controllers.Tests.SimpleTestController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "SimpleTest");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "SimpleTest", routeValues);
			}
					void TypeCheckRedirectToAction5(){
				CheckMethod<Specialist.Web.Controllers.Tests.SimpleTestController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "SimpleTest");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "SimpleTest", routeValues);
			}
					void TypeCheckRedirectBack6(){
				CheckMethod<Specialist.Web.Controllers.Tests.SimpleTestController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "SimpleTest");
								
				return _url.Action("RedirectBack", "SimpleTest", routeValues);
			}
					void TypeCheckMView7(){
				CheckMethod<Specialist.Web.Controllers.Tests.SimpleTestController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "SimpleTest");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "SimpleTest", routeValues);
			}
				

		

		}


			public class SimpleTestControllerLinks{
			
			private UrlHelper _url;

			public SimpleTestControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static SimpleTestControllerUrls _SimpleTestControllerUrls = null;
			public SimpleTestControllerUrls Urls { 
				get {
				//	if(_SimpleTestControllerUrls == null) _SimpleTestControllerUrls = new SimpleTestControllerUrls(_url);
					//return _SimpleTestControllerUrls;
					return new SimpleTestControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.Int32 id, object content){
				var localActionUrl = _url.SimpleTest().Urls.Details(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Result(	System.Int32 id, System.Int32 resultId, object content){
				var localActionUrl = _url.SimpleTest().Urls.Result(id, resultId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.SimpleTest().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.SimpleTest().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.SimpleTest().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.SimpleTest().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.SimpleTest().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.SimpleTest().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static TestControllerLinks _TestControllerLinks = null;
		
		public static TestControllerLinks Test(this UrlHelper urlHelper) {
		//	if(_TestControllerLinks == null) _TestControllerLinks = new TestControllerLinks(urlHelper);
		//	return _TestControllerLinks;
		return new TestControllerLinks(urlHelper);
		}

		public class TestControllerUrls{
			
			private UrlHelper _url;

			public TestControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.Details(0));
			}
			public string Details(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("id", id);
								
				return _url.Action("Details", "Test", routeValues);
			}
					void TypeCheckPrerequisite1(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.Prerequisite(0));
			}
			public string Prerequisite(System.Decimal courseId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Prerequisite");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("courseId", courseId);
								
				return _url.Action("Prerequisite", "Test", routeValues);
			}
					void TypeCheckCoursePlanned2(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.CoursePlanned((System.String)null));
			}
			public string CoursePlanned(System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CoursePlanned");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("CoursePlanned", "Test", routeValues);
			}
					void TypeCheckSection3(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.Section((System.String)null));
			}
			public string Section(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Section");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Section", "Test", routeValues);
			}
					void TypeCheckPrerequisites4(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.Prerequisites());
			}
			public string Prerequisites(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Prerequisites");
			//	routeValues.Add("controller", "Test");
								
				return _url.Action("Prerequisites", "Test", routeValues);
			}
					void TypeCheckBest5(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.Best((System.Nullable<System.Int32>)null));
			}
			public string Best(System.Nullable<System.Int32> sectionId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Best");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("sectionId", sectionId);
								
				return _url.Action("Best", "Test", routeValues);
			}
					void TypeCheckBaseView6(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Test", routeValues);
			}
					void TypeCheckBaseView7(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Test", routeValues);
			}
					void TypeCheckBaseViewWithTitle8(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Test", routeValues);
			}
					void TypeCheckRedirectToAction9(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Test", routeValues);
			}
					void TypeCheckRedirectBack10(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Test");
								
				return _url.Action("RedirectBack", "Test", routeValues);
			}
					void TypeCheckMView11(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Test");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Test", routeValues);
			}
				

		

		}


			public class TestControllerLinks{
			
			private UrlHelper _url;

			public TestControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static TestControllerUrls _TestControllerUrls = null;
			public TestControllerUrls Urls { 
				get {
				//	if(_TestControllerUrls == null) _TestControllerUrls = new TestControllerUrls(_url);
					//return _TestControllerUrls;
					return new TestControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.Int32 id, object content){
				var localActionUrl = _url.Test().Urls.Details(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Prerequisite(	System.Decimal courseId, object content){
				var localActionUrl = _url.Test().Urls.Prerequisite(courseId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CoursePlanned(	System.String courseTC, object content){
				var localActionUrl = _url.Test().Urls.CoursePlanned(courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Section(	System.String urlName, object content){
				var localActionUrl = _url.Test().Urls.Section(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Prerequisites(	object content){
				var localActionUrl = _url.Test().Urls.Prerequisites();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Best(	System.Nullable<System.Int32> sectionId, object content){
				var localActionUrl = _url.Test().Urls.Best(sectionId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Test().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Test().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Test().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Test().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Test().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Test().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static TestEditControllerLinks _TestEditControllerLinks = null;
		
		public static TestEditControllerLinks TestEdit(this UrlHelper urlHelper) {
		//	if(_TestEditControllerLinks == null) _TestEditControllerLinks = new TestEditControllerLinks(urlHelper);
		//	return _TestEditControllerLinks;
		return new TestEditControllerLinks(urlHelper);
		}

		public class TestEditControllerUrls{
			
			private UrlHelper _url;

			public TestEditControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckGetTests0(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetTests((Specialist.Web.Pages.AjaxGridRequest)null));
			}
			public string GetTests(Specialist.Web.Pages.AjaxGridRequest model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetTests");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("model", model);
								
				return _url.Action("GetTests", "TestEdit", routeValues);
			}
					void TypeCheckGetModuleSets1(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetModuleSets(0, (Specialist.Web.Pages.AjaxGridRequest)null));
			}
			public string GetModuleSets(System.Int32 testId, Specialist.Web.Pages.AjaxGridRequest model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetModuleSets");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("testId", testId);
									routeValues.Add("model", model);
								
				return _url.Action("GetModuleSets", "TestEdit", routeValues);
			}
					void TypeCheckGetModules2(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetModules(0, (Specialist.Web.Pages.AjaxGridRequest)null));
			}
			public string GetModules(System.Int32 testId, Specialist.Web.Pages.AjaxGridRequest model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetModules");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("testId", testId);
									routeValues.Add("model", model);
								
				return _url.Action("GetModules", "TestEdit", routeValues);
			}
					void TypeCheckGetModulesAuto3(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetModulesAuto(0, (System.String)null));
			}
			public string GetModulesAuto(System.Int32 testId, System.String term){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetModulesAuto");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("testId", testId);
									routeValues.Add("term", term);
								
				return _url.Action("GetModulesAuto", "TestEdit", routeValues);
			}
					void TypeCheckGetCoursesAuto4(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetCoursesAuto((System.String)null));
			}
			public string GetCoursesAuto(System.String term){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetCoursesAuto");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("term", term);
								
				return _url.Action("GetCoursesAuto", "TestEdit", routeValues);
			}
					void TypeCheckGetEmployeeAuto5(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetEmployeeAuto((System.String)null));
			}
			public string GetEmployeeAuto(System.String term){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetEmployeeAuto");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("term", term);
								
				return _url.Action("GetEmployeeAuto", "TestEdit", routeValues);
			}
					void TypeCheckGetAnswersAuto6(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetAnswersAuto(0, (System.String)null));
			}
			public string GetAnswersAuto(System.Int32 questionId, System.String term){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetAnswersAuto");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("questionId", questionId);
									routeValues.Add("term", term);
								
				return _url.Action("GetAnswersAuto", "TestEdit", routeValues);
			}
					void TypeCheckList7(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.List());
			}
			public string List(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "TestEdit");
								
				return _url.Action("List", "TestEdit", routeValues);
			}
					void TypeCheckViewTest8(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.ViewTest((System.Nullable<System.Int32>)null));
			}
			public string ViewTest(System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ViewTest");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
								
				return _url.Action("ViewTest", "TestEdit", routeValues);
			}
					void TypeCheckReturnToEdit9(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.ReturnToEdit(0));
			}
			public string ReturnToEdit(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ReturnToEdit");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
								
				return _url.Action("ReturnToEdit", "TestEdit", routeValues);
			}
					void TypeCheckSendToAudit10(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.SendToAudit(0, (System.String)null));
			}
			public string SendToAudit(System.Int32 id, System.String employeeTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendToAudit");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
									routeValues.Add("employeeTC", employeeTC);
								
				return _url.Action("SendToAudit", "TestEdit", routeValues);
			}
					void TypeCheckSendCompleteTest11(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.SendCompleteTest(0));
			}
			public string SendCompleteTest(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SendCompleteTest");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
								
				return _url.Action("SendCompleteTest", "TestEdit", routeValues);
			}
					void TypeCheckEditTest12(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditTest((System.Nullable<System.Int32>)null));
			}
			public string EditTest(System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditTest");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
								
				return _url.Action("EditTest", "TestEdit", routeValues);
			}
					void TypeCheckEditTest13(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditTest((Specialist.Web.Root.Tests.ViewModels.TestEditVM)null));
			}
			public string EditTest(Specialist.Web.Root.Tests.ViewModels.TestEditVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditTest");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("model", model);
								
				return _url.Action("EditTest", "TestEdit", routeValues);
			}
					void TypeCheckEditModule14(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditModule(0, (System.Nullable<System.Int32>)null));
			}
			public string EditModule(System.Int32 testId, System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditModule");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("testId", testId);
									routeValues.Add("id", id);
								
				return _url.Action("EditModule", "TestEdit", routeValues);
			}
					void TypeCheckEditModule15(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditModule((Specialist.Entities.Tests.TestModule)null));
			}
			public string EditModule(Specialist.Entities.Tests.TestModule model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditModule");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("model", model);
								
				return _url.Action("EditModule", "TestEdit", routeValues);
			}
					void TypeCheckDeleteModule16(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.DeleteModule((System.Nullable<System.Int32>)null));
			}
			public string DeleteModule(System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteModule");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
								
				return _url.Action("DeleteModule", "TestEdit", routeValues);
			}
					void TypeCheckEditQuestion17(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditQuestion(0, (System.Nullable<System.Int32>)null));
			}
			public string EditQuestion(System.Int32 testId, System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditQuestion");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("testId", testId);
									routeValues.Add("id", id);
								
				return _url.Action("EditQuestion", "TestEdit", routeValues);
			}
					void TypeCheckEditModuleSet18(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditModuleSet(0, (System.Nullable<System.Int32>)null));
			}
			public string EditModuleSet(System.Int32 testId, System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditModuleSet");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("testId", testId);
									routeValues.Add("id", id);
								
				return _url.Action("EditModuleSet", "TestEdit", routeValues);
			}
					void TypeCheckEditModuleSet19(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditModuleSet((Specialist.Web.Root.Tests.ViewModels.ModuleSetVM)null));
			}
			public string EditModuleSet(Specialist.Web.Root.Tests.ViewModels.ModuleSetVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditModuleSet");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("model", model);
								
				return _url.Action("EditModuleSet", "TestEdit", routeValues);
			}
					void TypeCheckEditQuestion20(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditQuestion((Specialist.Entities.Tests.TestQuestion)null));
			}
			public string EditQuestion(Specialist.Entities.Tests.TestQuestion model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditQuestion");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("model", model);
								
				return _url.Action("EditQuestion", "TestEdit", routeValues);
			}
					void TypeCheckDeleteQuestion21(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.DeleteQuestion((System.Nullable<System.Int32>)null));
			}
			public string DeleteQuestion(System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteQuestion");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
								
				return _url.Action("DeleteQuestion", "TestEdit", routeValues);
			}
					void TypeCheckEditAnswer22(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditAnswer(0, (System.Nullable<System.Int32>)null));
			}
			public string EditAnswer(System.Int32 questionId, System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditAnswer");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("questionId", questionId);
									routeValues.Add("id", id);
								
				return _url.Action("EditAnswer", "TestEdit", routeValues);
			}
					void TypeCheckEditAnswer23(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.EditAnswer((Specialist.Entities.Tests.TestAnswer)null));
			}
			public string EditAnswer(Specialist.Entities.Tests.TestAnswer model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditAnswer");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("model", model);
								
				return _url.Action("EditAnswer", "TestEdit", routeValues);
			}
					void TypeCheckDeleteAnswer24(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.DeleteAnswer((System.Nullable<System.Int32>)null));
			}
			public string DeleteAnswer(System.Nullable<System.Int32> id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteAnswer");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
								
				return _url.Action("DeleteAnswer", "TestEdit", routeValues);
			}
					void TypeCheckGetAnswerFileControl25(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetAnswerFileControl(0));
			}
			public string GetAnswerFileControl(System.Int32 answerId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetAnswerFileControl");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("answerId", answerId);
								
				return _url.Action("GetAnswerFileControl", "TestEdit", routeValues);
			}
					void TypeCheckUploadAnswerFile26(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.UploadAnswerFile(0, (System.String)null));
			}
			public string UploadAnswerFile(System.Int32 answerId, System.String qqfile){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UploadAnswerFile");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("answerId", answerId);
									routeValues.Add("qqfile", qqfile);
								
				return _url.Action("UploadAnswerFile", "TestEdit", routeValues);
			}
					void TypeCheckDeleteAnswerFile27(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.DeleteAnswerFile(0));
			}
			public string DeleteAnswerFile(System.Int32 answerId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteAnswerFile");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("answerId", answerId);
								
				return _url.Action("DeleteAnswerFile", "TestEdit", routeValues);
			}
					void TypeCheckGetQuestionFileControl28(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetQuestionFileControl(0));
			}
			public string GetQuestionFileControl(System.Int32 questionId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetQuestionFileControl");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("questionId", questionId);
								
				return _url.Action("GetQuestionFileControl", "TestEdit", routeValues);
			}
					void TypeCheckUploadQuestionFile29(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.UploadQuestionFile(0, (System.String)null));
			}
			public string UploadQuestionFile(System.Int32 questionId, System.String qqfile){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UploadQuestionFile");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("questionId", questionId);
									routeValues.Add("qqfile", qqfile);
								
				return _url.Action("UploadQuestionFile", "TestEdit", routeValues);
			}
					void TypeCheckDeleteQuestionFile30(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.DeleteQuestionFile(0));
			}
			public string DeleteQuestionFile(System.Int32 questionId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteQuestionFile");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("questionId", questionId);
								
				return _url.Action("DeleteQuestionFile", "TestEdit", routeValues);
			}
					void TypeCheckActivate31(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.Activate(0, false));
			}
			public string Activate(System.Int32 id, System.Boolean activate){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Activate");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("id", id);
									routeValues.Add("activate", activate);
								
				return _url.Action("Activate", "TestEdit", routeValues);
			}
					void TypeCheckGetQuestions32(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetQuestions(0, (Specialist.Web.Pages.AjaxGridRequest)null));
			}
			public string GetQuestions(System.Int32 testId, Specialist.Web.Pages.AjaxGridRequest model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetQuestions");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("testId", testId);
									routeValues.Add("model", model);
								
				return _url.Action("GetQuestions", "TestEdit", routeValues);
			}
					void TypeCheckGetAnswers33(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.GetAnswers(0, (Specialist.Web.Pages.AjaxGridRequest)null));
			}
			public string GetAnswers(System.Int32 questionId, Specialist.Web.Pages.AjaxGridRequest model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetAnswers");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("questionId", questionId);
									routeValues.Add("model", model);
								
				return _url.Action("GetAnswers", "TestEdit", routeValues);
			}
					void TypeCheckBaseView34(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "TestEdit", routeValues);
			}
					void TypeCheckBaseView35(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "TestEdit", routeValues);
			}
					void TypeCheckBaseViewWithTitle36(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "TestEdit", routeValues);
			}
					void TypeCheckRedirectToAction37(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "TestEdit", routeValues);
			}
					void TypeCheckRedirectBack38(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "TestEdit");
								
				return _url.Action("RedirectBack", "TestEdit", routeValues);
			}
					void TypeCheckMView39(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestEditController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "TestEdit");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "TestEdit", routeValues);
			}
				

		

		}


			public class TestEditControllerLinks{
			
			private UrlHelper _url;

			public TestEditControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static TestEditControllerUrls _TestEditControllerUrls = null;
			public TestEditControllerUrls Urls { 
				get {
				//	if(_TestEditControllerUrls == null) _TestEditControllerUrls = new TestEditControllerUrls(_url);
					//return _TestEditControllerUrls;
					return new TestEditControllerUrls(_url);
				}
			}

		
		
		


			public TagA GetTests(	Specialist.Web.Pages.AjaxGridRequest model, object content){
				var localActionUrl = _url.TestEdit().Urls.GetTests(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetModuleSets(	System.Int32 testId, Specialist.Web.Pages.AjaxGridRequest model, object content){
				var localActionUrl = _url.TestEdit().Urls.GetModuleSets(testId, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetModules(	System.Int32 testId, Specialist.Web.Pages.AjaxGridRequest model, object content){
				var localActionUrl = _url.TestEdit().Urls.GetModules(testId, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetModulesAuto(	System.Int32 testId, System.String term, object content){
				var localActionUrl = _url.TestEdit().Urls.GetModulesAuto(testId, term);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetCoursesAuto(	System.String term, object content){
				var localActionUrl = _url.TestEdit().Urls.GetCoursesAuto(term);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetEmployeeAuto(	System.String term, object content){
				var localActionUrl = _url.TestEdit().Urls.GetEmployeeAuto(term);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetAnswersAuto(	System.Int32 questionId, System.String term, object content){
				var localActionUrl = _url.TestEdit().Urls.GetAnswersAuto(questionId, term);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA List(	object content){
				var localActionUrl = _url.TestEdit().Urls.List();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ViewTest(	System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.ViewTest(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ReturnToEdit(	System.Int32 id, object content){
				var localActionUrl = _url.TestEdit().Urls.ReturnToEdit(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendToAudit(	System.Int32 id, System.String employeeTC, object content){
				var localActionUrl = _url.TestEdit().Urls.SendToAudit(id, employeeTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SendCompleteTest(	System.Int32 id, object content){
				var localActionUrl = _url.TestEdit().Urls.SendCompleteTest(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditTest(	System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.EditTest(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditTest(	Specialist.Web.Root.Tests.ViewModels.TestEditVM model, object content){
				var localActionUrl = _url.TestEdit().Urls.EditTest(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditModule(	System.Int32 testId, System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.EditModule(testId, id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditModule(	Specialist.Entities.Tests.TestModule model, object content){
				var localActionUrl = _url.TestEdit().Urls.EditModule(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteModule(	System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.DeleteModule(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditQuestion(	System.Int32 testId, System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.EditQuestion(testId, id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditModuleSet(	System.Int32 testId, System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.EditModuleSet(testId, id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditModuleSet(	Specialist.Web.Root.Tests.ViewModels.ModuleSetVM model, object content){
				var localActionUrl = _url.TestEdit().Urls.EditModuleSet(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditQuestion(	Specialist.Entities.Tests.TestQuestion model, object content){
				var localActionUrl = _url.TestEdit().Urls.EditQuestion(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteQuestion(	System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.DeleteQuestion(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditAnswer(	System.Int32 questionId, System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.EditAnswer(questionId, id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditAnswer(	Specialist.Entities.Tests.TestAnswer model, object content){
				var localActionUrl = _url.TestEdit().Urls.EditAnswer(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteAnswer(	System.Nullable<System.Int32> id, object content){
				var localActionUrl = _url.TestEdit().Urls.DeleteAnswer(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetAnswerFileControl(	System.Int32 answerId, object content){
				var localActionUrl = _url.TestEdit().Urls.GetAnswerFileControl(answerId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UploadAnswerFile(	System.Int32 answerId, System.String qqfile, object content){
				var localActionUrl = _url.TestEdit().Urls.UploadAnswerFile(answerId, qqfile);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteAnswerFile(	System.Int32 answerId, object content){
				var localActionUrl = _url.TestEdit().Urls.DeleteAnswerFile(answerId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetQuestionFileControl(	System.Int32 questionId, object content){
				var localActionUrl = _url.TestEdit().Urls.GetQuestionFileControl(questionId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UploadQuestionFile(	System.Int32 questionId, System.String qqfile, object content){
				var localActionUrl = _url.TestEdit().Urls.UploadQuestionFile(questionId, qqfile);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteQuestionFile(	System.Int32 questionId, object content){
				var localActionUrl = _url.TestEdit().Urls.DeleteQuestionFile(questionId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Activate(	System.Int32 id, System.Boolean activate, object content){
				var localActionUrl = _url.TestEdit().Urls.Activate(id, activate);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetQuestions(	System.Int32 testId, Specialist.Web.Pages.AjaxGridRequest model, object content){
				var localActionUrl = _url.TestEdit().Urls.GetQuestions(testId, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetAnswers(	System.Int32 questionId, Specialist.Web.Pages.AjaxGridRequest model, object content){
				var localActionUrl = _url.TestEdit().Urls.GetAnswers(questionId, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.TestEdit().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.TestEdit().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.TestEdit().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.TestEdit().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.TestEdit().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.TestEdit().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static TestRunControllerLinks _TestRunControllerLinks = null;
		
		public static TestRunControllerLinks TestRun(this UrlHelper urlHelper) {
		//	if(_TestRunControllerLinks == null) _TestRunControllerLinks = new TestRunControllerLinks(urlHelper);
		//	return _TestRunControllerLinks;
		return new TestRunControllerLinks(urlHelper);
		}

		public class TestRunControllerUrls{
			
			private UrlHelper _url;

			public TestRunControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckDetails0(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.Details(0));
			}
			public string Details(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("id", id);
								
				return _url.Action("Details", "TestRun", routeValues);
			}
					void TypeCheckPrerequisite1(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.Prerequisite(0, (System.String)null));
			}
			public string Prerequisite(System.Int32 id, System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Prerequisite");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("id", id);
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("Prerequisite", "TestRun", routeValues);
			}
					void TypeCheckCoursePlanned2(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.CoursePlanned(0, 0));
			}
			public string CoursePlanned(System.Int32 id, System.Int32 moduleSetId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CoursePlanned");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("id", id);
									routeValues.Add("moduleSetId", moduleSetId);
								
				return _url.Action("CoursePlanned", "TestRun", routeValues);
			}
					void TypeCheckStart3(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.Start(0, (System.String)null, (System.Nullable<System.Int32>)null));
			}
			public string Start(System.Int32 id, System.String courseTC, System.Nullable<System.Int32> moduleSetId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Start");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("id", id);
									routeValues.Add("courseTC", courseTC);
									routeValues.Add("moduleSetId", moduleSetId);
								
				return _url.Action("Start", "TestRun", routeValues);
			}
					void TypeCheckResult4(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.Result(0));
			}
			public string Result(System.Int32 userTestId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Result");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("userTestId", userTestId);
								
				return _url.Action("Result", "TestRun", routeValues);
			}
					void TypeCheckResultPost5(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.ResultPost(0, (Specialist.Web.Root.Tests.ViewModels.TestResultData)null));
			}
			public string ResultPost(System.Int32 userTestId, Specialist.Web.Root.Tests.ViewModels.TestResultData model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ResultPost");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("userTestId", userTestId);
									routeValues.Add("model", model);
								
				return _url.Action("ResultPost", "TestRun", routeValues);
			}
					void TypeCheckGetTestTime6(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.GetTestTime(0));
			}
			public string GetTestTime(System.Int32 userTestId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GetTestTime");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("userTestId", userTestId);
								
				return _url.Action("GetTestTime", "TestRun", routeValues);
			}
					void TypeCheckUserTestAnswers7(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.UserTestAnswers(0));
			}
			public string UserTestAnswers(System.Int32 userTestId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UserTestAnswers");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("userTestId", userTestId);
								
				return _url.Action("UserTestAnswers", "TestRun", routeValues);
			}
					void TypeCheckBaseView8(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "TestRun", routeValues);
			}
					void TypeCheckBaseView9(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "TestRun", routeValues);
			}
					void TypeCheckBaseViewWithTitle10(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "TestRun", routeValues);
			}
					void TypeCheckRedirectToAction11(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "TestRun", routeValues);
			}
					void TypeCheckRedirectBack12(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "TestRun");
								
				return _url.Action("RedirectBack", "TestRun", routeValues);
			}
					void TypeCheckMView13(){
				CheckMethod<Specialist.Web.Controllers.Tests.TestRunController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "TestRun");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "TestRun", routeValues);
			}
				

		

		}


			public class TestRunControllerLinks{
			
			private UrlHelper _url;

			public TestRunControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static TestRunControllerUrls _TestRunControllerUrls = null;
			public TestRunControllerUrls Urls { 
				get {
				//	if(_TestRunControllerUrls == null) _TestRunControllerUrls = new TestRunControllerUrls(_url);
					//return _TestRunControllerUrls;
					return new TestRunControllerUrls(_url);
				}
			}

		
		
		


			public TagA Details(	System.Int32 id, object content){
				var localActionUrl = _url.TestRun().Urls.Details(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Prerequisite(	System.Int32 id, System.String courseTC, object content){
				var localActionUrl = _url.TestRun().Urls.Prerequisite(id, courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CoursePlanned(	System.Int32 id, System.Int32 moduleSetId, object content){
				var localActionUrl = _url.TestRun().Urls.CoursePlanned(id, moduleSetId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Start(	System.Int32 id, System.String courseTC, System.Nullable<System.Int32> moduleSetId, object content){
				var localActionUrl = _url.TestRun().Urls.Start(id, courseTC, moduleSetId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Result(	System.Int32 userTestId, object content){
				var localActionUrl = _url.TestRun().Urls.Result(userTestId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ResultPost(	System.Int32 userTestId, Specialist.Web.Root.Tests.ViewModels.TestResultData model, object content){
				var localActionUrl = _url.TestRun().Urls.ResultPost(userTestId, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GetTestTime(	System.Int32 userTestId, object content){
				var localActionUrl = _url.TestRun().Urls.GetTestTime(userTestId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UserTestAnswers(	System.Int32 userTestId, object content){
				var localActionUrl = _url.TestRun().Urls.UserTestAnswers(userTestId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.TestRun().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.TestRun().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.TestRun().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.TestRun().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.TestRun().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.TestRun().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static CartControllerLinks _CartControllerLinks = null;
		
		public static CartControllerLinks Cart(this UrlHelper urlHelper) {
		//	if(_CartControllerLinks == null) _CartControllerLinks = new CartControllerLinks(urlHelper);
		//	return _CartControllerLinks;
		return new CartControllerLinks(urlHelper);
		}

		public class CartControllerUrls{
			
			private UrlHelper _url;

			public CartControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckRedirectToDetails0(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.RedirectToDetails());
			}
			public string RedirectToDetails(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToDetails");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("RedirectToDetails", "Cart", routeValues);
			}
					void TypeCheckBackOrDetails1(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.BackOrDetails());
			}
			public string BackOrDetails(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BackOrDetails");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("BackOrDetails", "Cart", routeValues);
			}
					void TypeCheckDetails2(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.Details());
			}
			public string Details(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("Details", "Cart", routeValues);
			}
					void TypeCheckAddGroup3(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.AddGroup(0));
			}
			public string AddGroup(System.Decimal groupID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddGroup");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("groupID", groupID);
								
				return _url.Action("AddGroup", "Cart", routeValues);
			}
					void TypeCheckAddCourse4(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.AddCourse((System.String)null, (System.String)null));
			}
			public string AddCourse(System.String courseTC, System.String priceTypeTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddCourse");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("courseTC", courseTC);
									routeValues.Add("priceTypeTC", priceTypeTC);
								
				return _url.Action("AddCourse", "Cart", routeValues);
			}
					void TypeCheckAddWithSecondCourse5(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.AddWithSecondCourse((System.String)null, (System.String)null));
			}
			public string AddWithSecondCourse(System.String courseTC, System.String secondCourseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddWithSecondCourse");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("courseTC", courseTC);
									routeValues.Add("secondCourseTC", secondCourseTC);
								
				return _url.Action("AddWithSecondCourse", "Cart", routeValues);
			}
					void TypeCheckAddCourseWithSocialLink6(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.AddCourseWithSocialLink());
			}
			public string AddCourseWithSocialLink(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddCourseWithSocialLink");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("AddCourseWithSocialLink", "Cart", routeValues);
			}
					void TypeCheckAddCourseWithSocialLinkPost7(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.AddCourseWithSocialLinkPost((System.String)null));
			}
			public string AddCourseWithSocialLinkPost(System.String socialurl){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddCourseWithSocialLinkPost");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("socialurl", socialurl);
								
				return _url.Action("AddCourseWithSocialLinkPost", "Cart", routeValues);
			}
					void TypeCheckAddCourseListPost8(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.AddCourseListPost((System.Collections.Generic.List<System.String>)null));
			}
			public string AddCourseListPost(System.Collections.Generic.List<System.String> courseTCs){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddCourseListPost");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("courseTCs", courseTCs);
								
				return _url.Action("AddCourseListPost", "Cart", routeValues);
			}
					void TypeCheckAddTestCert9(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.AddTestCert(0));
			}
			public string AddTestCert(System.Int32 userTestId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddTestCert");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("userTestId", userTestId);
								
				return _url.Action("AddTestCert", "Cart", routeValues);
			}
					void TypeCheckState10(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.State());
			}
			public string State(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "State");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("State", "Cart", routeValues);
			}
					void TypeCheckStateNew11(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.StateNew());
			}
			public string StateNew(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "StateNew");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("StateNew", "Cart", routeValues);
			}
					void TypeCheckDeleteCourse12(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.DeleteCourse(0));
			}
			public string DeleteCourse(System.Decimal orderDetailID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteCourse");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("orderDetailID", orderDetailID);
								
				return _url.Action("DeleteCourse", "Cart", routeValues);
			}
					void TypeCheckDeleteTrack13(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.DeleteTrack((System.String)null));
			}
			public string DeleteTrack(System.String trackTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteTrack");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("trackTC", trackTC);
								
				return _url.Action("DeleteTrack", "Cart", routeValues);
			}
					void TypeCheckClear14(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.Clear());
			}
			public string Clear(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Clear");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("Clear", "Cart", routeValues);
			}
					void TypeCheckAddExam15(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.AddExam(0));
			}
			public string AddExam(System.Decimal examID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddExam");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("examID", examID);
								
				return _url.Action("AddExam", "Cart", routeValues);
			}
					void TypeCheckOrderUnlimit16(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.OrderUnlimit());
			}
			public string OrderUnlimit(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OrderUnlimit");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("OrderUnlimit", "Cart", routeValues);
			}
					void TypeCheckDeleteExam17(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.DeleteExam(0));
			}
			public string DeleteExam(System.Decimal examID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DeleteExam");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("examID", examID);
								
				return _url.Action("DeleteExam", "Cart", routeValues);
			}
					void TypeCheckBaseView18(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Cart", routeValues);
			}
					void TypeCheckBaseView19(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Cart", routeValues);
			}
					void TypeCheckBaseViewWithTitle20(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Cart", routeValues);
			}
					void TypeCheckRedirectToAction21(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Cart", routeValues);
			}
					void TypeCheckRedirectBack22(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Cart");
								
				return _url.Action("RedirectBack", "Cart", routeValues);
			}
					void TypeCheckMView23(){
				CheckMethod<Specialist.Web.Controllers.Shop.CartController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Cart");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Cart", routeValues);
			}
				

		

		}


			public class CartControllerLinks{
			
			private UrlHelper _url;

			public CartControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static CartControllerUrls _CartControllerUrls = null;
			public CartControllerUrls Urls { 
				get {
				//	if(_CartControllerUrls == null) _CartControllerUrls = new CartControllerUrls(_url);
					//return _CartControllerUrls;
					return new CartControllerUrls(_url);
				}
			}

		
		
		


			public TagA RedirectToDetails(	object content){
				var localActionUrl = _url.Cart().Urls.RedirectToDetails();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BackOrDetails(	object content){
				var localActionUrl = _url.Cart().Urls.BackOrDetails();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Details(	object content){
				var localActionUrl = _url.Cart().Urls.Details();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddGroup(	System.Decimal groupID, object content){
				var localActionUrl = _url.Cart().Urls.AddGroup(groupID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddCourse(	System.String courseTC, System.String priceTypeTC, object content){
				var localActionUrl = _url.Cart().Urls.AddCourse(courseTC, priceTypeTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddWithSecondCourse(	System.String courseTC, System.String secondCourseTC, object content){
				var localActionUrl = _url.Cart().Urls.AddWithSecondCourse(courseTC, secondCourseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddCourseWithSocialLink(	object content){
				var localActionUrl = _url.Cart().Urls.AddCourseWithSocialLink();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddCourseWithSocialLinkPost(	System.String socialurl, object content){
				var localActionUrl = _url.Cart().Urls.AddCourseWithSocialLinkPost(socialurl);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddCourseListPost(	System.Collections.Generic.List<System.String> courseTCs, object content){
				var localActionUrl = _url.Cart().Urls.AddCourseListPost(courseTCs);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddTestCert(	System.Int32 userTestId, object content){
				var localActionUrl = _url.Cart().Urls.AddTestCert(userTestId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA State(	object content){
				var localActionUrl = _url.Cart().Urls.State();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA StateNew(	object content){
				var localActionUrl = _url.Cart().Urls.StateNew();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteCourse(	System.Decimal orderDetailID, object content){
				var localActionUrl = _url.Cart().Urls.DeleteCourse(orderDetailID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteTrack(	System.String trackTC, object content){
				var localActionUrl = _url.Cart().Urls.DeleteTrack(trackTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Clear(	object content){
				var localActionUrl = _url.Cart().Urls.Clear();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddExam(	System.Decimal examID, object content){
				var localActionUrl = _url.Cart().Urls.AddExam(examID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA OrderUnlimit(	object content){
				var localActionUrl = _url.Cart().Urls.OrderUnlimit();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DeleteExam(	System.Decimal examID, object content){
				var localActionUrl = _url.Cart().Urls.DeleteExam(examID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Cart().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Cart().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Cart().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Cart().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Cart().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Cart().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static EditCartControllerLinks _EditCartControllerLinks = null;
		
		public static EditCartControllerLinks EditCart(this UrlHelper urlHelper) {
		//	if(_EditCartControllerLinks == null) _EditCartControllerLinks = new EditCartControllerLinks(urlHelper);
		//	return _EditCartControllerLinks;
		return new EditCartControllerLinks(urlHelper);
		}

		public class EditCartControllerUrls{
			
			private UrlHelper _url;

			public EditCartControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckRedirectToDetails0(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.RedirectToDetails());
			}
			public string RedirectToDetails(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToDetails");
			//	routeValues.Add("controller", "EditCart");
								
				return _url.Action("RedirectToDetails", "EditCart", routeValues);
			}
					void TypeCheckEdit1(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.Edit((Specialist.Entities.Context.ViewModel.EditCartVM)null));
			}
			public string Edit(Specialist.Entities.Context.ViewModel.EditCartVM editCartVM){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Edit");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("editCartVM", editCartVM);
								
				return _url.Action("Edit", "EditCart", routeValues);
			}
					void TypeCheckToggleExam2(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.ToggleExam(0));
			}
			public string ToggleExam(System.Decimal examID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ToggleExam");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("examID", examID);
								
				return _url.Action("ToggleExam", "EditCart", routeValues);
			}
					void TypeCheckToggleCourse3(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.ToggleCourse(0));
			}
			public string ToggleCourse(System.Decimal orderDetailID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ToggleCourse");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("orderDetailID", orderDetailID);
								
				return _url.Action("ToggleCourse", "EditCart", routeValues);
			}
					void TypeCheckToggleTrack4(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.ToggleTrack((System.String)null));
			}
			public string ToggleTrack(System.String trackTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ToggleTrack");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("trackTC", trackTC);
								
				return _url.Action("ToggleTrack", "EditCart", routeValues);
			}
					void TypeCheckToggleWebinar5(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.ToggleWebinar(0));
			}
			public string ToggleWebinar(System.Decimal orderdetailId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ToggleWebinar");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("orderdetailId", orderdetailId);
								
				return _url.Action("ToggleWebinar", "EditCart", routeValues);
			}
					void TypeCheckUpdateOrder6(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.UpdateOrder((System.String)null));
			}
			public string UpdateOrder(System.String customerType){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UpdateOrder");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("customerType", customerType);
								
				return _url.Action("UpdateOrder", "EditCart", routeValues);
			}
					void TypeCheckEditCourse7(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditCourse(0));
			}
			public string EditCourse(System.Decimal orderDetailID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditCourse");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("orderDetailID", orderDetailID);
								
				return _url.Action("EditCourse", "EditCart", routeValues);
			}
					void TypeCheckEditCourse8(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditCourse((Specialist.Entities.Context.ViewModel.EditCourseVM)null));
			}
			public string EditCourse(Specialist.Entities.Context.ViewModel.EditCourseVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditCourse");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("model", model);
								
				return _url.Action("EditCourse", "EditCart", routeValues);
			}
					void TypeCheckEditExam9(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditExam(0));
			}
			public string EditExam(System.Decimal examID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditExam");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("examID", examID);
								
				return _url.Action("EditExam", "EditCart", routeValues);
			}
					void TypeCheckEditExam10(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditExam((Specialist.Entities.Context.ViewModel.EditExamVM)null));
			}
			public string EditExam(Specialist.Entities.Context.ViewModel.EditExamVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditExam");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("model", model);
								
				return _url.Action("EditExam", "EditCart", routeValues);
			}
					void TypeCheckEditTestCert11(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditTestCert(0));
			}
			public string EditTestCert(System.Int32 userTestId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditTestCert");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("userTestId", userTestId);
								
				return _url.Action("EditTestCert", "EditCart", routeValues);
			}
					void TypeCheckEditTestCert12(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditTestCert(0, 0, 0));
			}
			public string EditTestCert(System.Int32 orderDetailId, System.Byte type, System.Byte lang){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditTestCert");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("orderDetailId", orderDetailId);
									routeValues.Add("type", type);
									routeValues.Add("lang", lang);
								
				return _url.Action("EditTestCert", "EditCart", routeValues);
			}
					void TypeCheckEditTrack13(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditTrack((System.String)null));
			}
			public string EditTrack(System.String trackTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditTrack");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("trackTC", trackTC);
								
				return _url.Action("EditTrack", "EditCart", routeValues);
			}
					void TypeCheckSelectGroups14(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.SelectGroups((System.Collections.Generic.List<Specialist.Entities.Context.OrderDetail>)null));
			}
			public string SelectGroups(System.Collections.Generic.List<Specialist.Entities.Context.OrderDetail> orderDetails){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SelectGroups");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("orderDetails", orderDetails);
								
				return _url.Action("SelectGroups", "EditCart", routeValues);
			}
					void TypeCheckUpdateFavoriteTrainer15(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.UpdateFavoriteTrainer((System.String)null));
			}
			public string UpdateFavoriteTrainer(System.String fullName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UpdateFavoriteTrainer");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("fullName", fullName);
								
				return _url.Action("UpdateFavoriteTrainer", "EditCart", routeValues);
			}
					void TypeCheckUpdatePromocode16(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.UpdatePromocode((System.String)null));
			}
			public string UpdatePromocode(System.String promocode){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UpdatePromocode");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("promocode", promocode);
								
				return _url.Action("UpdatePromocode", "EditCart", routeValues);
			}
					void TypeCheckEditExtras17(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditExtras(0));
			}
			public string EditExtras(System.Decimal orderDetailID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditExtras");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("orderDetailID", orderDetailID);
								
				return _url.Action("EditExtras", "EditCart", routeValues);
			}
					void TypeCheckEditExtras18(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditExtras((Specialist.Web.ViewModel.Orders.ExtrasesVM)null));
			}
			public string EditExtras(Specialist.Web.ViewModel.Orders.ExtrasesVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditExtras");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("model", model);
								
				return _url.Action("EditExtras", "EditCart", routeValues);
			}
					void TypeCheckEditSeatNumber19(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditSeatNumber(0));
			}
			public string EditSeatNumber(System.Decimal orderDetailID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditSeatNumber");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("orderDetailID", orderDetailID);
								
				return _url.Action("EditSeatNumber", "EditCart", routeValues);
			}
					void TypeCheckEditSeatNumber20(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.EditSeatNumber((Specialist.Web.ViewModel.Orders.EditSeatNumberVM)null));
			}
			public string EditSeatNumber(Specialist.Web.ViewModel.Orders.EditSeatNumberVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditSeatNumber");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("model", model);
								
				return _url.Action("EditSeatNumber", "EditCart", routeValues);
			}
					void TypeCheckBaseView21(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "EditCart", routeValues);
			}
					void TypeCheckBaseView22(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "EditCart", routeValues);
			}
					void TypeCheckBaseViewWithTitle23(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "EditCart", routeValues);
			}
					void TypeCheckRedirectToAction24(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "EditCart", routeValues);
			}
					void TypeCheckRedirectBack25(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "EditCart");
								
				return _url.Action("RedirectBack", "EditCart", routeValues);
			}
					void TypeCheckMView26(){
				CheckMethod<Specialist.Web.Controllers.Shop.EditCartController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "EditCart");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "EditCart", routeValues);
			}
				

		

		}


			public class EditCartControllerLinks{
			
			private UrlHelper _url;

			public EditCartControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static EditCartControllerUrls _EditCartControllerUrls = null;
			public EditCartControllerUrls Urls { 
				get {
				//	if(_EditCartControllerUrls == null) _EditCartControllerUrls = new EditCartControllerUrls(_url);
					//return _EditCartControllerUrls;
					return new EditCartControllerUrls(_url);
				}
			}

		
		
		


			public TagA RedirectToDetails(	object content){
				var localActionUrl = _url.EditCart().Urls.RedirectToDetails();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Edit(	Specialist.Entities.Context.ViewModel.EditCartVM editCartVM, object content){
				var localActionUrl = _url.EditCart().Urls.Edit(editCartVM);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ToggleExam(	System.Decimal examID, object content){
				var localActionUrl = _url.EditCart().Urls.ToggleExam(examID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ToggleCourse(	System.Decimal orderDetailID, object content){
				var localActionUrl = _url.EditCart().Urls.ToggleCourse(orderDetailID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ToggleTrack(	System.String trackTC, object content){
				var localActionUrl = _url.EditCart().Urls.ToggleTrack(trackTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ToggleWebinar(	System.Decimal orderdetailId, object content){
				var localActionUrl = _url.EditCart().Urls.ToggleWebinar(orderdetailId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UpdateOrder(	System.String customerType, object content){
				var localActionUrl = _url.EditCart().Urls.UpdateOrder(customerType);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditCourse(	System.Decimal orderDetailID, object content){
				var localActionUrl = _url.EditCart().Urls.EditCourse(orderDetailID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditCourse(	Specialist.Entities.Context.ViewModel.EditCourseVM model, object content){
				var localActionUrl = _url.EditCart().Urls.EditCourse(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditExam(	System.Decimal examID, object content){
				var localActionUrl = _url.EditCart().Urls.EditExam(examID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditExam(	Specialist.Entities.Context.ViewModel.EditExamVM model, object content){
				var localActionUrl = _url.EditCart().Urls.EditExam(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditTestCert(	System.Int32 userTestId, object content){
				var localActionUrl = _url.EditCart().Urls.EditTestCert(userTestId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditTestCert(	System.Int32 orderDetailId, System.Byte type, System.Byte lang, object content){
				var localActionUrl = _url.EditCart().Urls.EditTestCert(orderDetailId, type, lang);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditTrack(	System.String trackTC, object content){
				var localActionUrl = _url.EditCart().Urls.EditTrack(trackTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SelectGroups(	System.Collections.Generic.List<Specialist.Entities.Context.OrderDetail> orderDetails, object content){
				var localActionUrl = _url.EditCart().Urls.SelectGroups(orderDetails);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UpdateFavoriteTrainer(	System.String fullName, object content){
				var localActionUrl = _url.EditCart().Urls.UpdateFavoriteTrainer(fullName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UpdatePromocode(	System.String promocode, object content){
				var localActionUrl = _url.EditCart().Urls.UpdatePromocode(promocode);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditExtras(	System.Decimal orderDetailID, object content){
				var localActionUrl = _url.EditCart().Urls.EditExtras(orderDetailID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditExtras(	Specialist.Web.ViewModel.Orders.ExtrasesVM model, object content){
				var localActionUrl = _url.EditCart().Urls.EditExtras(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditSeatNumber(	System.Decimal orderDetailID, object content){
				var localActionUrl = _url.EditCart().Urls.EditSeatNumber(orderDetailID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditSeatNumber(	Specialist.Web.ViewModel.Orders.EditSeatNumberVM model, object content){
				var localActionUrl = _url.EditCart().Urls.EditSeatNumber(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.EditCart().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.EditCart().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.EditCart().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.EditCart().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.EditCart().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.EditCart().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static MessageControllerLinks _MessageControllerLinks = null;
		
		public static MessageControllerLinks Message(this UrlHelper urlHelper) {
		//	if(_MessageControllerLinks == null) _MessageControllerLinks = new MessageControllerLinks(urlHelper);
		//	return _MessageControllerLinks;
		return new MessageControllerLinks(urlHelper);
		}

		public class MessageControllerUrls{
			
			private UrlHelper _url;

			public MessageControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckForum0(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.Forum());
			}
			public string Forum(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Forum");
			//	routeValues.Add("controller", "Message");
								
				return _url.Action("Forum", "Message", routeValues);
			}
					void TypeCheckSection1(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.Section((System.Nullable<System.Int32>)null, (System.Nullable<System.Int32>)null));
			}
			public string Section(System.Nullable<System.Int32> sectionID, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Section");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("sectionID", sectionID);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("Section", "Message", routeValues);
			}
					void TypeCheckDetails2(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.Details(0, (System.Nullable<System.Int32>)null));
			}
			public string Details(System.Int64 messageID, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("messageID", messageID);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("Details", "Message", routeValues);
			}
					void TypeCheckGroup3(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.Group(0));
			}
			public string Group(System.Decimal groupID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Group");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("groupID", groupID);
								
				return _url.Action("Group", "Message", routeValues);
			}
					void TypeCheckAddAnswer4(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.AddAnswer(0));
			}
			public string AddAnswer(System.Int64 messageID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddAnswer");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("messageID", messageID);
								
				return _url.Action("AddAnswer", "Message", routeValues);
			}
					void TypeCheckAddAnswer5(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.AddAnswer((Specialist.Entities.Message.ViewModel.AddAnswerVM)null));
			}
			public string AddAnswer(Specialist.Entities.Message.ViewModel.AddAnswerVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddAnswer");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("model", model);
								
				return _url.Action("AddAnswer", "Message", routeValues);
			}
					void TypeCheckAddMessage6(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.AddMessage(0));
			}
			public string AddMessage(System.Int32 sectionID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddMessage");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("sectionID", sectionID);
								
				return _url.Action("AddMessage", "Message", routeValues);
			}
					void TypeCheckEdit7(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.Edit(0));
			}
			public string Edit(System.Int64 messageId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Edit");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("messageId", messageId);
								
				return _url.Action("Edit", "Message", routeValues);
			}
					void TypeCheckEditPost8(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.EditPost((Specialist.Entities.Message.ViewModel.EditMessageVM)null));
			}
			public string EditPost(Specialist.Entities.Message.ViewModel.EditMessageVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "EditPost");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("model", model);
								
				return _url.Action("EditPost", "Message", routeValues);
			}
					void TypeCheckAddMessage9(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.AddMessage((Specialist.Entities.Message.ViewModel.EditMessageVM)null));
			}
			public string AddMessage(Specialist.Entities.Message.ViewModel.EditMessageVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AddMessage");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("model", model);
								
				return _url.Action("AddMessage", "Message", routeValues);
			}
					void TypeCheckDelete10(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.Delete(0));
			}
			public string Delete(System.Int64 messageID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Delete");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("messageID", messageID);
								
				return _url.Action("Delete", "Message", routeValues);
			}
					void TypeCheckAnsweredToggle11(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.AnsweredToggle(0));
			}
			public string AnsweredToggle(System.Int64 messageID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AnsweredToggle");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("messageID", messageID);
								
				return _url.Action("AnsweredToggle", "Message", routeValues);
			}
					void TypeCheckPrivateList12(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.PrivateList(0, (System.Nullable<System.Int32>)null));
			}
			public string PrivateList(System.Int32 receiverID, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PrivateList");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("receiverID", receiverID);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("PrivateList", "Message", routeValues);
			}
					void TypeCheckPrivateList13(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.PrivateList((Specialist.Entities.Message.ViewModel.PrivateMessageListVM)null));
			}
			public string PrivateList(Specialist.Entities.Message.ViewModel.PrivateMessageListVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PrivateList");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("model", model);
								
				return _url.Action("PrivateList", "Message", routeValues);
			}
					void TypeCheckBaseView14(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Message", routeValues);
			}
					void TypeCheckBaseView15(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Message", routeValues);
			}
					void TypeCheckBaseViewWithTitle16(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Message", routeValues);
			}
					void TypeCheckRedirectToAction17(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Message", routeValues);
			}
					void TypeCheckRedirectBack18(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Message");
								
				return _url.Action("RedirectBack", "Message", routeValues);
			}
					void TypeCheckMView19(){
				CheckMethod<Specialist.Web.Controllers.Message.MessageController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Message");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Message", routeValues);
			}
				

		

		}


			public class MessageControllerLinks{
			
			private UrlHelper _url;

			public MessageControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static MessageControllerUrls _MessageControllerUrls = null;
			public MessageControllerUrls Urls { 
				get {
				//	if(_MessageControllerUrls == null) _MessageControllerUrls = new MessageControllerUrls(_url);
					//return _MessageControllerUrls;
					return new MessageControllerUrls(_url);
				}
			}

		
		
		


			public TagA Forum(	object content){
				var localActionUrl = _url.Message().Urls.Forum();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Section(	System.Nullable<System.Int32> sectionID, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Message().Urls.Section(sectionID, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Details(	System.Int64 messageID, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Message().Urls.Details(messageID, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Group(	System.Decimal groupID, object content){
				var localActionUrl = _url.Message().Urls.Group(groupID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddAnswer(	System.Int64 messageID, object content){
				var localActionUrl = _url.Message().Urls.AddAnswer(messageID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddAnswer(	Specialist.Entities.Message.ViewModel.AddAnswerVM model, object content){
				var localActionUrl = _url.Message().Urls.AddAnswer(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddMessage(	System.Int32 sectionID, object content){
				var localActionUrl = _url.Message().Urls.AddMessage(sectionID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Edit(	System.Int64 messageId, object content){
				var localActionUrl = _url.Message().Urls.Edit(messageId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA EditPost(	Specialist.Entities.Message.ViewModel.EditMessageVM model, object content){
				var localActionUrl = _url.Message().Urls.EditPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AddMessage(	Specialist.Entities.Message.ViewModel.EditMessageVM model, object content){
				var localActionUrl = _url.Message().Urls.AddMessage(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Delete(	System.Int64 messageID, object content){
				var localActionUrl = _url.Message().Urls.Delete(messageID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AnsweredToggle(	System.Int64 messageID, object content){
				var localActionUrl = _url.Message().Urls.AnsweredToggle(messageID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PrivateList(	System.Int32 receiverID, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Message().Urls.PrivateList(receiverID, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PrivateList(	Specialist.Entities.Message.ViewModel.PrivateMessageListVM model, object content){
				var localActionUrl = _url.Message().Urls.PrivateList(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Message().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Message().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Message().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Message().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Message().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Message().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static MobileAppControllerLinks _MobileAppControllerLinks = null;
		
		public static MobileAppControllerLinks MobileApp(this UrlHelper urlHelper) {
		//	if(_MobileAppControllerLinks == null) _MobileAppControllerLinks = new MobileAppControllerLinks(urlHelper);
		//	return _MobileAppControllerLinks;
		return new MobileAppControllerLinks(urlHelper);
		}

		public class MobileAppControllerUrls{
			
			private UrlHelper _url;

			public MobileAppControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckSections0(){
				CheckMethod<Specialist.Web.Controllers.Common.MobileAppController>(c => 
					c.Sections());
			}
			public string Sections(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Sections");
			//	routeValues.Add("controller", "MobileApp");
								
				return _url.Action("Sections", "MobileApp", routeValues);
			}
				

		

		}


			public class MobileAppControllerLinks{
			
			private UrlHelper _url;

			public MobileAppControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static MobileAppControllerUrls _MobileAppControllerUrls = null;
			public MobileAppControllerUrls Urls { 
				get {
				//	if(_MobileAppControllerUrls == null) _MobileAppControllerUrls = new MobileAppControllerUrls(_url);
					//return _MobileAppControllerUrls;
					return new MobileAppControllerUrls(_url);
				}
			}

		
		
		


			public TagA Sections(	object content){
				var localActionUrl = _url.MobileApp().Urls.Sections();
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static BadgeControllerLinks _BadgeControllerLinks = null;
		
		public static BadgeControllerLinks Badge(this UrlHelper urlHelper) {
		//	if(_BadgeControllerLinks == null) _BadgeControllerLinks = new BadgeControllerLinks(urlHelper);
		//	return _BadgeControllerLinks;
		return new BadgeControllerLinks(urlHelper);
		}

		public class BadgeControllerUrls{
			
			private UrlHelper _url;

			public BadgeControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckIssuer0(){
				CheckMethod<Specialist.Web.Controllers.Common.BadgeController>(c => 
					c.Issuer());
			}
			public string Issuer(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Issuer");
			//	routeValues.Add("controller", "Badge");
								
				return _url.Action("Issuer", "Badge", routeValues);
			}
					void TypeCheckRealSpecialist1(){
				CheckMethod<Specialist.Web.Controllers.Common.BadgeController>(c => 
					c.RealSpecialist((System.String)null));
			}
			public string RealSpecialist(System.String tc){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RealSpecialist");
			//	routeValues.Add("controller", "Badge");
									routeValues.Add("tc", tc);
								
				return _url.Action("RealSpecialist", "Badge", routeValues);
			}
					void TypeCheckUserRealSpecialist2(){
				CheckMethod<Specialist.Web.Controllers.Common.BadgeController>(c => 
					c.UserRealSpecialist(0));
			}
			public string UserRealSpecialist(System.Int32 userId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UserRealSpecialist");
			//	routeValues.Add("controller", "Badge");
									routeValues.Add("userId", userId);
								
				return _url.Action("UserRealSpecialist", "Badge", routeValues);
			}
				

		

		}


			public class BadgeControllerLinks{
			
			private UrlHelper _url;

			public BadgeControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static BadgeControllerUrls _BadgeControllerUrls = null;
			public BadgeControllerUrls Urls { 
				get {
				//	if(_BadgeControllerUrls == null) _BadgeControllerUrls = new BadgeControllerUrls(_url);
					//return _BadgeControllerUrls;
					return new BadgeControllerUrls(_url);
				}
			}

		
		
		


			public TagA Issuer(	object content){
				var localActionUrl = _url.Badge().Urls.Issuer();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RealSpecialist(	System.String tc, object content){
				var localActionUrl = _url.Badge().Urls.RealSpecialist(tc);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UserRealSpecialist(	System.Int32 userId, object content){
				var localActionUrl = _url.Badge().Urls.UserRealSpecialist(userId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static LmsControllerLinks _LmsControllerLinks = null;
		
		public static LmsControllerLinks Lms(this UrlHelper urlHelper) {
		//	if(_LmsControllerLinks == null) _LmsControllerLinks = new LmsControllerLinks(urlHelper);
		//	return _LmsControllerLinks;
		return new LmsControllerLinks(urlHelper);
		}

		public class LmsControllerUrls{
			
			private UrlHelper _url;

			public LmsControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckLectureQuestionnaire0(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.LectureQuestionnaire(0));
			}
			public string LectureQuestionnaire(System.Decimal lectureId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "LectureQuestionnaire");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("lectureId", lectureId);
								
				return _url.Action("LectureQuestionnaire", "Lms", routeValues);
			}
					void TypeCheckLectureQuestionnairePost1(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.LectureQuestionnairePost((Specialist.Web.Root.Lms.LectureQuestionnaireVM)null));
			}
			public string LectureQuestionnairePost(Specialist.Web.Root.Lms.LectureQuestionnaireVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "LectureQuestionnairePost");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("model", model);
								
				return _url.Action("LectureQuestionnairePost", "Lms", routeValues);
			}
					void TypeCheckQuestionnaire2(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.Questionnaire(0));
			}
			public string Questionnaire(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Questionnaire");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("Questionnaire", "Lms", routeValues);
			}
					void TypeCheckWebinarLauncher3(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.WebinarLauncher((System.Nullable<System.Decimal>)null));
			}
			public string WebinarLauncher(System.Nullable<System.Decimal> lectureId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "WebinarLauncher");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("lectureId", lectureId);
								
				return _url.Action("WebinarLauncher", "Lms", routeValues);
			}
					void TypeCheckCurator4(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.Curator());
			}
			public string Curator(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Curator");
			//	routeValues.Add("controller", "Lms");
								
				return _url.Action("Curator", "Lms", routeValues);
			}
					void TypeCheckStudentPhoto5(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.StudentPhoto(0));
			}
			public string StudentPhoto(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "StudentPhoto");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("id", id);
								
				return _url.Action("StudentPhoto", "Lms", routeValues);
			}
					void TypeCheckGroupTestResults6(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.GroupTestResults(0));
			}
			public string GroupTestResults(System.Decimal id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupTestResults");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("id", id);
								
				return _url.Action("GroupTestResults", "Lms", routeValues);
			}
					void TypeCheckCourseVideos7(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.CourseVideos((System.String)null));
			}
			public string CourseVideos(System.String courseTC){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseVideos");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("courseTC", courseTC);
								
				return _url.Action("CourseVideos", "Lms", routeValues);
			}
					void TypeCheckDetails8(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.Details());
			}
			public string Details(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "Lms");
								
				return _url.Action("Details", "Lms", routeValues);
			}
					void TypeCheckGroups9(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.Groups());
			}
			public string Groups(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Groups");
			//	routeValues.Add("controller", "Lms");
								
				return _url.Action("Groups", "Lms", routeValues);
			}
					void TypeCheckCourses10(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.Courses());
			}
			public string Courses(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Courses");
			//	routeValues.Add("controller", "Lms");
								
				return _url.Action("Courses", "Lms", routeValues);
			}
					void TypeCheckGroupStudents11(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.GroupStudents((System.Nullable<System.Decimal>)null));
			}
			public string GroupStudents(System.Nullable<System.Decimal> gId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupStudents");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("gId", gId);
								
				return _url.Action("GroupStudents", "Lms", routeValues);
			}
					void TypeCheckStudentSearch12(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.StudentSearch((System.String)null));
			}
			public string StudentSearch(System.String name){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "StudentSearch");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("name", name);
								
				return _url.Action("StudentSearch", "Lms", routeValues);
			}
					void TypeCheckStudent13(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.Student(0));
			}
			public string Student(System.Decimal studentId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Student");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("studentId", studentId);
								
				return _url.Action("Student", "Lms", routeValues);
			}
					void TypeCheckTimeSheet14(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.TimeSheet((System.String)null));
			}
			public string TimeSheet(System.String d){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TimeSheet");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("d", d);
								
				return _url.Action("TimeSheet", "Lms", routeValues);
			}
					void TypeCheckGroup15(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.Group(0));
			}
			public string Group(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Group");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("Group", "Lms", routeValues);
			}
					void TypeCheckGroupInfo16(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.GroupInfo(0));
			}
			public string GroupInfo(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupInfo");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("GroupInfo", "Lms", routeValues);
			}
					void TypeCheckLecture17(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.Lecture((System.Nullable<System.Decimal>)null));
			}
			public string Lecture(System.Nullable<System.Decimal> lectureId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Lecture");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("lectureId", lectureId);
								
				return _url.Action("Lecture", "Lms", routeValues);
			}
					void TypeCheckUpdateLecture18(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.UpdateLecture((Specialist.Web.Root.Lms.LectureEditVM)null));
			}
			public string UpdateLecture(Specialist.Web.Root.Lms.LectureEditVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UpdateLecture");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("model", model);
								
				return _url.Action("UpdateLecture", "Lms", routeValues);
			}
					void TypeCheckBaseView19(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Lms", routeValues);
			}
					void TypeCheckBaseView20(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Lms", routeValues);
			}
					void TypeCheckBaseViewWithTitle21(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Lms", routeValues);
			}
					void TypeCheckRedirectToAction22(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Lms", routeValues);
			}
					void TypeCheckRedirectBack23(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Lms");
								
				return _url.Action("RedirectBack", "Lms", routeValues);
			}
					void TypeCheckMView24(){
				CheckMethod<Specialist.Web.Controllers.Common.LmsController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Lms");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Lms", routeValues);
			}
				

		

		}


			public class LmsControllerLinks{
			
			private UrlHelper _url;

			public LmsControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static LmsControllerUrls _LmsControllerUrls = null;
			public LmsControllerUrls Urls { 
				get {
				//	if(_LmsControllerUrls == null) _LmsControllerUrls = new LmsControllerUrls(_url);
					//return _LmsControllerUrls;
					return new LmsControllerUrls(_url);
				}
			}

		
		
		


			public TagA LectureQuestionnaire(	System.Decimal lectureId, object content){
				var localActionUrl = _url.Lms().Urls.LectureQuestionnaire(lectureId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA LectureQuestionnairePost(	Specialist.Web.Root.Lms.LectureQuestionnaireVM model, object content){
				var localActionUrl = _url.Lms().Urls.LectureQuestionnairePost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Questionnaire(	System.Decimal groupId, object content){
				var localActionUrl = _url.Lms().Urls.Questionnaire(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA WebinarLauncher(	System.Nullable<System.Decimal> lectureId, object content){
				var localActionUrl = _url.Lms().Urls.WebinarLauncher(lectureId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Curator(	object content){
				var localActionUrl = _url.Lms().Urls.Curator();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA StudentPhoto(	System.Decimal id, object content){
				var localActionUrl = _url.Lms().Urls.StudentPhoto(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupTestResults(	System.Decimal id, object content){
				var localActionUrl = _url.Lms().Urls.GroupTestResults(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseVideos(	System.String courseTC, object content){
				var localActionUrl = _url.Lms().Urls.CourseVideos(courseTC);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Details(	object content){
				var localActionUrl = _url.Lms().Urls.Details();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Groups(	object content){
				var localActionUrl = _url.Lms().Urls.Groups();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Courses(	object content){
				var localActionUrl = _url.Lms().Urls.Courses();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupStudents(	System.Nullable<System.Decimal> gId, object content){
				var localActionUrl = _url.Lms().Urls.GroupStudents(gId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA StudentSearch(	System.String name, object content){
				var localActionUrl = _url.Lms().Urls.StudentSearch(name);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Student(	System.Decimal studentId, object content){
				var localActionUrl = _url.Lms().Urls.Student(studentId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TimeSheet(	System.String d, object content){
				var localActionUrl = _url.Lms().Urls.TimeSheet(d);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Group(	System.Decimal groupId, object content){
				var localActionUrl = _url.Lms().Urls.Group(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupInfo(	System.Decimal groupId, object content){
				var localActionUrl = _url.Lms().Urls.GroupInfo(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Lecture(	System.Nullable<System.Decimal> lectureId, object content){
				var localActionUrl = _url.Lms().Urls.Lecture(lectureId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UpdateLecture(	Specialist.Web.Root.Lms.LectureEditVM model, object content){
				var localActionUrl = _url.Lms().Urls.UpdateLecture(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Lms().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Lms().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Lms().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Lms().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Lms().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Lms().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static InfoControllerLinks _InfoControllerLinks = null;
		
		public static InfoControllerLinks Info(this UrlHelper urlHelper) {
		//	if(_InfoControllerLinks == null) _InfoControllerLinks = new InfoControllerLinks(urlHelper);
		//	return _InfoControllerLinks;
		return new InfoControllerLinks(urlHelper);
		}

		public class InfoControllerUrls{
			
			private UrlHelper _url;

			public InfoControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckCityInfoBlock0(){
				CheckMethod<Specialist.Web.Controllers.Common.InfoController>(c => 
					c.CityInfoBlock(0));
			}
			public string CityInfoBlock(System.Int32 id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CityInfoBlock");
			//	routeValues.Add("controller", "Info");
									routeValues.Add("id", id);
								
				return _url.Action("CityInfoBlock", "Info", routeValues);
			}
					void TypeCheckGroupFiles1(){
				CheckMethod<Specialist.Web.Controllers.Common.InfoController>(c => 
					c.GroupFiles((System.Nullable<System.Decimal>)null));
			}
			public string GroupFiles(System.Nullable<System.Decimal> groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupFiles");
			//	routeValues.Add("controller", "Info");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("GroupFiles", "Info", routeValues);
			}
					void TypeCheckBaseView2(){
				CheckMethod<Specialist.Web.Controllers.Common.InfoController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Info");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Info", routeValues);
			}
					void TypeCheckBaseView3(){
				CheckMethod<Specialist.Web.Controllers.Common.InfoController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Info");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Info", routeValues);
			}
					void TypeCheckBaseViewWithTitle4(){
				CheckMethod<Specialist.Web.Controllers.Common.InfoController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Info");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Info", routeValues);
			}
					void TypeCheckRedirectToAction5(){
				CheckMethod<Specialist.Web.Controllers.Common.InfoController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Info");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Info", routeValues);
			}
					void TypeCheckRedirectBack6(){
				CheckMethod<Specialist.Web.Controllers.Common.InfoController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Info");
								
				return _url.Action("RedirectBack", "Info", routeValues);
			}
					void TypeCheckMView7(){
				CheckMethod<Specialist.Web.Controllers.Common.InfoController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Info");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Info", routeValues);
			}
				

		

		}


			public class InfoControllerLinks{
			
			private UrlHelper _url;

			public InfoControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static InfoControllerUrls _InfoControllerUrls = null;
			public InfoControllerUrls Urls { 
				get {
				//	if(_InfoControllerUrls == null) _InfoControllerUrls = new InfoControllerUrls(_url);
					//return _InfoControllerUrls;
					return new InfoControllerUrls(_url);
				}
			}

		
		
		


			public TagA CityInfoBlock(	System.Int32 id, object content){
				var localActionUrl = _url.Info().Urls.CityInfoBlock(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupFiles(	System.Nullable<System.Decimal> groupId, object content){
				var localActionUrl = _url.Info().Urls.GroupFiles(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Info().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Info().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Info().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Info().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Info().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Info().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static PartnerControllerLinks _PartnerControllerLinks = null;
		
		public static PartnerControllerLinks Partner(this UrlHelper urlHelper) {
		//	if(_PartnerControllerLinks == null) _PartnerControllerLinks = new PartnerControllerLinks(urlHelper);
		//	return _PartnerControllerLinks;
		return new PartnerControllerLinks(urlHelper);
		}

		public class PartnerControllerUrls{
			
			private UrlHelper _url;

			public PartnerControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckSberbankCallback0(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.SberbankCallback((System.String)null, (System.String)null, (System.String)null, 0));
			}
			public string SberbankCallback(System.String mdOrder, System.String orderNumber, System.String operation, System.Int32 status){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SberbankCallback");
			//	routeValues.Add("controller", "Partner");
									routeValues.Add("mdOrder", mdOrder);
									routeValues.Add("orderNumber", orderNumber);
									routeValues.Add("operation", operation);
									routeValues.Add("status", status);
								
				return _url.Action("SberbankCallback", "Partner", routeValues);
			}
					void TypeCheckYandexMarket1(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.YandexMarket());
			}
			public string YandexMarket(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "YandexMarket");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("YandexMarket", "Partner", routeValues);
			}
					void TypeCheckGoogleMerchant2(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.GoogleMerchant());
			}
			public string GoogleMerchant(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GoogleMerchant");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("GoogleMerchant", "Partner", routeValues);
			}
					void TypeCheckUchebaRu3(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.UchebaRu());
			}
			public string UchebaRu(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UchebaRu");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("UchebaRu", "Partner", routeValues);
			}
					void TypeCheckTeachMePlease4(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.TeachMePlease());
			}
			public string TeachMePlease(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TeachMePlease");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("TeachMePlease", "Partner", routeValues);
			}
					void TypeCheckX5RetailGroup5(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.X5RetailGroup((System.String)null));
			}
			public string X5RetailGroup(System.String id){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "X5RetailGroup");
			//	routeValues.Add("controller", "Partner");
									routeValues.Add("id", id);
								
				return _url.Action("X5RetailGroup", "Partner", routeValues);
			}
					void TypeCheckGoogleRemarketing6(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.GoogleRemarketing());
			}
			public string GoogleRemarketing(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GoogleRemarketing");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("GoogleRemarketing", "Partner", routeValues);
			}
					void TypeCheckSverhMarket7(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.SverhMarket());
			}
			public string SverhMarket(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SverhMarket");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("SverhMarket", "Partner", routeValues);
			}
					void TypeCheckSuperJob8(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.SuperJob(0));
			}
			public string SuperJob(System.Int32 categoryId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SuperJob");
			//	routeValues.Add("controller", "Partner");
									routeValues.Add("categoryId", categoryId);
								
				return _url.Action("SuperJob", "Partner", routeValues);
			}
					void TypeCheckDwg9(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.Dwg());
			}
			public string Dwg(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Dwg");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("Dwg", "Partner", routeValues);
			}
					void TypeCheckDwgCourses10(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.DwgCourses());
			}
			public string DwgCourses(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DwgCourses");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("DwgCourses", "Partner", routeValues);
			}
					void TypeCheckRedirectToAction11(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Partner");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Partner", routeValues);
			}
					void TypeCheckRedirectBack12(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Partner");
								
				return _url.Action("RedirectBack", "Partner", routeValues);
			}
					void TypeCheckMView13(){
				CheckMethod<Specialist.Web.Controllers.Common.PartnerController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Partner");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Partner", routeValues);
			}
				

		

		}


			public class PartnerControllerLinks{
			
			private UrlHelper _url;

			public PartnerControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static PartnerControllerUrls _PartnerControllerUrls = null;
			public PartnerControllerUrls Urls { 
				get {
				//	if(_PartnerControllerUrls == null) _PartnerControllerUrls = new PartnerControllerUrls(_url);
					//return _PartnerControllerUrls;
					return new PartnerControllerUrls(_url);
				}
			}

		
		
		


			public TagA SberbankCallback(	System.String mdOrder, System.String orderNumber, System.String operation, System.Int32 status, object content){
				var localActionUrl = _url.Partner().Urls.SberbankCallback(mdOrder, orderNumber, operation, status);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA YandexMarket(	object content){
				var localActionUrl = _url.Partner().Urls.YandexMarket();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GoogleMerchant(	object content){
				var localActionUrl = _url.Partner().Urls.GoogleMerchant();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UchebaRu(	object content){
				var localActionUrl = _url.Partner().Urls.UchebaRu();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TeachMePlease(	object content){
				var localActionUrl = _url.Partner().Urls.TeachMePlease();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA X5RetailGroup(	System.String id, object content){
				var localActionUrl = _url.Partner().Urls.X5RetailGroup(id);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GoogleRemarketing(	object content){
				var localActionUrl = _url.Partner().Urls.GoogleRemarketing();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SverhMarket(	object content){
				var localActionUrl = _url.Partner().Urls.SverhMarket();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SuperJob(	System.Int32 categoryId, object content){
				var localActionUrl = _url.Partner().Urls.SuperJob(categoryId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Dwg(	object content){
				var localActionUrl = _url.Partner().Urls.Dwg();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DwgCourses(	object content){
				var localActionUrl = _url.Partner().Urls.DwgCourses();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Partner().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Partner().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Partner().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static RssControllerLinks _RssControllerLinks = null;
		
		public static RssControllerLinks Rss(this UrlHelper urlHelper) {
		//	if(_RssControllerLinks == null) _RssControllerLinks = new RssControllerLinks(urlHelper);
		//	return _RssControllerLinks;
		return new RssControllerLinks(urlHelper);
		}

		public class RssControllerUrls{
			
			private UrlHelper _url;

			public RssControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckMessages0(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.Messages((System.Nullable<System.Int32>)null, (System.Nullable<System.Int64>)null));
			}
			public string Messages(System.Nullable<System.Int32> sectionID, System.Nullable<System.Int64> messageID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Messages");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("sectionID", sectionID);
									routeValues.Add("messageID", messageID);
								
				return _url.Action("Messages", "Rss", routeValues);
			}
					void TypeCheckForumMessages1(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.ForumMessages());
			}
			public string ForumMessages(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ForumMessages");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("ForumMessages", "Rss", routeValues);
			}
					void TypeCheckAllMessages2(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.AllMessages(0));
			}
			public string AllMessages(System.Int32 sectionID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AllMessages");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("sectionID", sectionID);
								
				return _url.Action("AllMessages", "Rss", routeValues);
			}
					void TypeCheckMessagesNotAnswered3(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.MessagesNotAnswered());
			}
			public string MessagesNotAnswered(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MessagesNotAnswered");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("MessagesNotAnswered", "Rss", routeValues);
			}
					void TypeCheckMessagesForUser4(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.MessagesForUser(0));
			}
			public string MessagesForUser(System.Int32 userId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MessagesForUser");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("userId", userId);
								
				return _url.Action("MessagesForUser", "Rss", routeValues);
			}
					void TypeCheckGroupMessages5(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.GroupMessages((System.String)null));
			}
			public string GroupMessages(System.String tc){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "GroupMessages");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("tc", tc);
								
				return _url.Action("GroupMessages", "Rss", routeValues);
			}
					void TypeCheckNews6(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.News());
			}
			public string News(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "News");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("News", "Rss", routeValues);
			}
					void TypeCheckAdvices7(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.Advices());
			}
			public string Advices(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Advices");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("Advices", "Rss", routeValues);
			}
					void TypeCheckHotGroups8(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.HotGroups());
			}
			public string HotGroups(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "HotGroups");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("HotGroups", "Rss", routeValues);
			}
					void TypeCheckMsProjectGroups9(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.MsProjectGroups());
			}
			public string MsProjectGroups(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MsProjectGroups");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("MsProjectGroups", "Rss", routeValues);
			}
					void TypeCheckPmGroups10(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.PmGroups());
			}
			public string PmGroups(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PmGroups");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("PmGroups", "Rss", routeValues);
			}
					void TypeCheckCourseGroups11(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.CourseGroups((System.String)null));
			}
			public string CourseGroups(System.String tc){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseGroups");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("tc", tc);
								
				return _url.Action("CourseGroups", "Rss", routeValues);
			}
					void TypeCheckPmHotGroups12(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.PmHotGroups());
			}
			public string PmHotGroups(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PmHotGroups");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("PmHotGroups", "Rss", routeValues);
			}
					void TypeCheckSql13(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.Sql());
			}
			public string Sql(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Sql");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("Sql", "Rss", routeValues);
			}
					void TypeCheckSqlOracle14(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.SqlOracle());
			}
			public string SqlOracle(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SqlOracle");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("SqlOracle", "Rss", routeValues);
			}
					void TypeCheckOracle15(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.Oracle());
			}
			public string Oracle(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Oracle");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("Oracle", "Rss", routeValues);
			}
					void TypeCheckTimeSheetCalendar16(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.TimeSheetCalendar((System.String)null));
			}
			public string TimeSheetCalendar(System.String tc){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TimeSheetCalendar");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("tc", tc);
								
				return _url.Action("TimeSheetCalendar", "Rss", routeValues);
			}
					void TypeCheckFeedOrChrome17(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.FeedOrChrome((System.ServiceModel.Syndication.SyndicationFeed)null));
			}
			public string FeedOrChrome(System.ServiceModel.Syndication.SyndicationFeed feed){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "FeedOrChrome");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("feed", feed);
								
				return _url.Action("FeedOrChrome", "Rss", routeValues);
			}
					void TypeCheckRedirectToAction18(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Rss", routeValues);
			}
					void TypeCheckRedirectBack19(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Rss");
								
				return _url.Action("RedirectBack", "Rss", routeValues);
			}
					void TypeCheckMView20(){
				CheckMethod<Specialist.Web.Controllers.Common.RssController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Rss");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Rss", routeValues);
			}
				

		

		}


			public class RssControllerLinks{
			
			private UrlHelper _url;

			public RssControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static RssControllerUrls _RssControllerUrls = null;
			public RssControllerUrls Urls { 
				get {
				//	if(_RssControllerUrls == null) _RssControllerUrls = new RssControllerUrls(_url);
					//return _RssControllerUrls;
					return new RssControllerUrls(_url);
				}
			}

		
		
		


			public TagA Messages(	System.Nullable<System.Int32> sectionID, System.Nullable<System.Int64> messageID, object content){
				var localActionUrl = _url.Rss().Urls.Messages(sectionID, messageID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ForumMessages(	object content){
				var localActionUrl = _url.Rss().Urls.ForumMessages();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AllMessages(	System.Int32 sectionID, object content){
				var localActionUrl = _url.Rss().Urls.AllMessages(sectionID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MessagesNotAnswered(	object content){
				var localActionUrl = _url.Rss().Urls.MessagesNotAnswered();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MessagesForUser(	System.Int32 userId, object content){
				var localActionUrl = _url.Rss().Urls.MessagesForUser(userId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA GroupMessages(	System.String tc, object content){
				var localActionUrl = _url.Rss().Urls.GroupMessages(tc);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA News(	object content){
				var localActionUrl = _url.Rss().Urls.News();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Advices(	object content){
				var localActionUrl = _url.Rss().Urls.Advices();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA HotGroups(	object content){
				var localActionUrl = _url.Rss().Urls.HotGroups();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MsProjectGroups(	object content){
				var localActionUrl = _url.Rss().Urls.MsProjectGroups();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PmGroups(	object content){
				var localActionUrl = _url.Rss().Urls.PmGroups();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseGroups(	System.String tc, object content){
				var localActionUrl = _url.Rss().Urls.CourseGroups(tc);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA PmHotGroups(	object content){
				var localActionUrl = _url.Rss().Urls.PmHotGroups();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Sql(	object content){
				var localActionUrl = _url.Rss().Urls.Sql();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SqlOracle(	object content){
				var localActionUrl = _url.Rss().Urls.SqlOracle();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Oracle(	object content){
				var localActionUrl = _url.Rss().Urls.Oracle();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TimeSheetCalendar(	System.String tc, object content){
				var localActionUrl = _url.Rss().Urls.TimeSheetCalendar(tc);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA FeedOrChrome(	System.ServiceModel.Syndication.SyndicationFeed feed, object content){
				var localActionUrl = _url.Rss().Urls.FeedOrChrome(feed);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Rss().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Rss().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Rss().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static CenterControllerLinks _CenterControllerLinks = null;
		
		public static CenterControllerLinks Center(this UrlHelper urlHelper) {
		//	if(_CenterControllerLinks == null) _CenterControllerLinks = new CenterControllerLinks(urlHelper);
		//	return _CenterControllerLinks;
		return new CenterControllerLinks(urlHelper);
		}

		public class CenterControllerUrls{
			
			private UrlHelper _url;

			public CenterControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckCollectionMctsPost0(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.CollectionMctsPost((Specialist.Web.Root.Center.ViewModels.CollectionMctsFormVM)null));
			}
			public string CollectionMctsPost(Specialist.Web.Root.Center.ViewModels.CollectionMctsFormVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CollectionMctsPost");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("model", model);
								
				return _url.Action("CollectionMctsPost", "Center", routeValues);
			}
					void TypeCheckUploadJubileeFile1(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.UploadJubileeFile((System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase>)null));
			}
			public string UploadJubileeFile(System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase> userfile){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UploadJubileeFile");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("userfile", userfile);
								
				return _url.Action("UploadJubileeFile", "Center", routeValues);
			}
					void TypeCheckActionsBlock2(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.ActionsBlock());
			}
			public string ActionsBlock(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ActionsBlock");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("ActionsBlock", "Center", routeValues);
			}
					void TypeCheckVideo3(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Video(0, (System.String)null));
			}
			public string Video(System.Int32 id, System.String title){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Video");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("id", id);
									routeValues.Add("title", title);
								
				return _url.Action("Video", "Center", routeValues);
			}
					void TypeCheckVideoCategory4(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.VideoCategory(0));
			}
			public string VideoCategory(System.Int32 categoryId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "VideoCategory");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("categoryId", categoryId);
								
				return _url.Action("VideoCategory", "Center", routeValues);
			}
					void TypeCheckAbout5(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.About());
			}
			public string About(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "About");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("About", "Center", routeValues);
			}
					void TypeCheckNewsAndActions6(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.NewsAndActions());
			}
			public string NewsAndActions(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "NewsAndActions");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("NewsAndActions", "Center", routeValues);
			}
					void TypeCheckInfo7(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Info());
			}
			public string Info(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Info");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("Info", "Center", routeValues);
			}
					void TypeCheckActions8(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Actions());
			}
			public string Actions(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Actions");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("Actions", "Center", routeValues);
			}
					void TypeCheckExpressOrder9(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.ExpressOrder((Specialist.Entities.Common.ViewModel.ExpressOrderVM)null));
			}
			public string ExpressOrder(Specialist.Entities.Common.ViewModel.ExpressOrderVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ExpressOrder");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("model", model);
								
				return _url.Action("ExpressOrder", "Center", routeValues);
			}
					void TypeCheckDownloadRegister10(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.DownloadRegister((System.String)null));
			}
			public string DownloadRegister(System.String file){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "DownloadRegister");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("file", file);
								
				return _url.Action("DownloadRegister", "Center", routeValues);
			}
					void TypeCheckVacancy11(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Vacancy(0));
			}
			public string Vacancy(System.Int32 vacancyID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Vacancy");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("vacancyID", vacancyID);
								
				return _url.Action("Vacancy", "Center", routeValues);
			}
					void TypeCheckAdvice12(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Advice(0, (System.String)null));
			}
			public string Advice(System.Int32 adviceID, System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Advice");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("adviceID", adviceID);
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Advice", "Center", routeValues);
			}
					void TypeCheckAdviceBlock13(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.AdviceBlock());
			}
			public string AdviceBlock(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "AdviceBlock");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("AdviceBlock", "Center", routeValues);
			}
					void TypeCheckAdvices14(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Advices((System.Nullable<System.Int32>)null));
			}
			public string Advices(System.Nullable<System.Int32> index){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Advices");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("index", index);
								
				return _url.Action("Advices", "Center", routeValues);
			}
					void TypeCheckPolls15(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Polls((System.Nullable<System.Int32>)null));
			}
			public string Polls(System.Nullable<System.Int32> index){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Polls");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("index", index);
								
				return _url.Action("Polls", "Center", routeValues);
			}
					void TypeCheckVacancies16(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Vacancies((System.Nullable<System.Int32>)null));
			}
			public string Vacancies(System.Nullable<System.Int32> index){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Vacancies");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("index", index);
								
				return _url.Action("Vacancies", "Center", routeValues);
			}
					void TypeCheckVacanciesForTeacher17(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.VacanciesForTeacher((System.Nullable<System.Int32>)null));
			}
			public string VacanciesForTeacher(System.Nullable<System.Int32> index){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "VacanciesForTeacher");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("index", index);
								
				return _url.Action("VacanciesForTeacher", "Center", routeValues);
			}
					void TypeCheckVacanciesForEmployee18(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.VacanciesForEmployee((System.Nullable<System.Int32>)null));
			}
			public string VacanciesForEmployee(System.Nullable<System.Int32> index){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "VacanciesForEmployee");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("index", index);
								
				return _url.Action("VacanciesForEmployee", "Center", routeValues);
			}
					void TypeCheckMarketingAction19(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.MarketingAction((System.String)null));
			}
			public string MarketingAction(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MarketingAction");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("MarketingAction", "Center", routeValues);
			}
					void TypeCheckMarketingActions20(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.MarketingActions());
			}
			public string MarketingActions(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MarketingActions");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("MarketingActions", "Center", routeValues);
			}
					void TypeCheckUsefulInformation21(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.UsefulInformation((System.String)null));
			}
			public string UsefulInformation(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UsefulInformation");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("UsefulInformation", "Center", routeValues);
			}
					void TypeCheckCertificate22(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Certificate((System.String)null));
			}
			public string Certificate(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Certificate");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("Certificate", "Center", routeValues);
			}
					void TypeCheckSkype23(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Skype());
			}
			public string Skype(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Skype");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("Skype", "Center", routeValues);
			}
					void TypeCheckCourseTrainers24(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.CourseTrainers((System.String)null));
			}
			public string CourseTrainers(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseTrainers");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("CourseTrainers", "Center", routeValues);
			}
					void TypeCheckSectionResponses25(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.SectionResponses((System.String)null));
			}
			public string SectionResponses(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SectionResponses");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("SectionResponses", "Center", routeValues);
			}
					void TypeCheckCourseResponses26(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.CourseResponses((System.String)null, (System.Nullable<System.Int32>)null));
			}
			public string CourseResponses(System.String urlName, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CourseResponses");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("urlName", urlName);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("CourseResponses", "Center", routeValues);
			}
					void TypeCheckSectionTrainers27(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.SectionTrainers((System.String)null));
			}
			public string SectionTrainers(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SectionTrainers");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("SectionTrainers", "Center", routeValues);
			}
					void TypeCheckCertificationTrainers28(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.CertificationTrainers((System.String)null));
			}
			public string CertificationTrainers(System.String urlName){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CertificationTrainers");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("urlName", urlName);
								
				return _url.Action("CertificationTrainers", "Center", routeValues);
			}
					void TypeCheckTrainerSections29(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.TrainerSections());
			}
			public string TrainerSections(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TrainerSections");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("TrainerSections", "Center", routeValues);
			}
					void TypeCheckTrainerCertifications30(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.TrainerCertifications());
			}
			public string TrainerCertifications(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "TrainerCertifications");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("TrainerCertifications", "Center", routeValues);
			}
					void TypeCheckCompetitions31(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Competitions());
			}
			public string Competitions(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Competitions");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("Competitions", "Center", routeValues);
			}
					void TypeCheckCompetition32(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Competition(0));
			}
			public string Competition(System.Int32 competitionID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Competition");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("competitionID", competitionID);
								
				return _url.Action("Competition", "Center", routeValues);
			}
					void TypeCheckCompetition33(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.Competition((Specialist.Entities.Profile.ViewModel.CompetitionVM)null));
			}
			public string Competition(Specialist.Entities.Profile.ViewModel.CompetitionVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Competition");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("model", model);
								
				return _url.Action("Competition", "Center", routeValues);
			}
					void TypeCheckJubileeForm34(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.JubileeForm());
			}
			public string JubileeForm(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "JubileeForm");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("JubileeForm", "Center", routeValues);
			}
					void TypeCheckJubileeFormPost35(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.JubileeFormPost((Specialist.Web.Root.Center.ViewModels.JubileeFormVM)null));
			}
			public string JubileeFormPost(Specialist.Web.Root.Center.ViewModels.JubileeFormVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "JubileeFormPost");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("model", model);
								
				return _url.Action("JubileeFormPost", "Center", routeValues);
			}
					void TypeCheckSeminarRegistration36(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.SeminarRegistration(0));
			}
			public string SeminarRegistration(System.Decimal groupId){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SeminarRegistration");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("groupId", groupId);
								
				return _url.Action("SeminarRegistration", "Center", routeValues);
			}
					void TypeCheckMtsEmployee37(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.MtsEmployee());
			}
			public string MtsEmployee(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MtsEmployee");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("MtsEmployee", "Center", routeValues);
			}
					void TypeCheckMtsEmployeePost38(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.MtsEmployeePost((Specialist.Web.Root.Center.ViewModels.MtsEmployeeFormVM)null));
			}
			public string MtsEmployeePost(Specialist.Web.Root.Center.ViewModels.MtsEmployeeFormVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MtsEmployeePost");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("model", model);
								
				return _url.Action("MtsEmployeePost", "Center", routeValues);
			}
					void TypeCheckOrgCatalog39(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.OrgCatalog());
			}
			public string OrgCatalog(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OrgCatalog");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("OrgCatalog", "Center", routeValues);
			}
					void TypeCheckOrgCatalogPost40(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.OrgCatalogPost((Specialist.Web.Root.Center.ViewModels.OrgCatalogFormVM)null));
			}
			public string OrgCatalogPost(Specialist.Web.Root.Center.ViewModels.OrgCatalogFormVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OrgCatalogPost");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("model", model);
								
				return _url.Action("OrgCatalogPost", "Center", routeValues);
			}
					void TypeCheckOrderPaperCatalog41(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.OrderPaperCatalog());
			}
			public string OrderPaperCatalog(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OrderPaperCatalog");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("OrderPaperCatalog", "Center", routeValues);
			}
					void TypeCheckOrderPaperCatalogPost42(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.OrderPaperCatalogPost((Specialist.Web.Root.Center.ViewModels.PaperCatalogFormVM)null));
			}
			public string OrderPaperCatalogPost(Specialist.Web.Root.Center.ViewModels.PaperCatalogFormVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OrderPaperCatalogPost");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("model", model);
								
				return _url.Action("OrderPaperCatalogPost", "Center", routeValues);
			}
					void TypeCheckSeminarRegistrationPost43(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.SeminarRegistrationPost(0, (Specialist.Web.Root.Center.ViewModels.SeminarRegisterFormVM)null));
			}
			public string SeminarRegistrationPost(System.Decimal groupId, Specialist.Web.Root.Center.ViewModels.SeminarRegisterFormVM model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SeminarRegistrationPost");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("groupId", groupId);
									routeValues.Add("model", model);
								
				return _url.Action("SeminarRegistrationPost", "Center", routeValues);
			}
					void TypeCheckBaseView44(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Center", routeValues);
			}
					void TypeCheckBaseView45(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Center", routeValues);
			}
					void TypeCheckBaseViewWithTitle46(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Center", routeValues);
			}
					void TypeCheckRedirectToAction47(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Center", routeValues);
			}
					void TypeCheckRedirectBack48(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Center");
								
				return _url.Action("RedirectBack", "Center", routeValues);
			}
					void TypeCheckMView49(){
				CheckMethod<Specialist.Web.Controllers.Center.CenterController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Center");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Center", routeValues);
			}
				

		

		}


			public class CenterControllerLinks{
			
			private UrlHelper _url;

			public CenterControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static CenterControllerUrls _CenterControllerUrls = null;
			public CenterControllerUrls Urls { 
				get {
				//	if(_CenterControllerUrls == null) _CenterControllerUrls = new CenterControllerUrls(_url);
					//return _CenterControllerUrls;
					return new CenterControllerUrls(_url);
				}
			}

		
		
		


			public TagA CollectionMctsPost(	Specialist.Web.Root.Center.ViewModels.CollectionMctsFormVM model, object content){
				var localActionUrl = _url.Center().Urls.CollectionMctsPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UploadJubileeFile(	System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase> userfile, object content){
				var localActionUrl = _url.Center().Urls.UploadJubileeFile(userfile);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ActionsBlock(	object content){
				var localActionUrl = _url.Center().Urls.ActionsBlock();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Video(	System.Int32 id, System.String title, object content){
				var localActionUrl = _url.Center().Urls.Video(id, title);
				return  Htmls.a.Href(localActionUrl)[content];
			}

            /*public TagA RegLink(System.Int32 id, System.String title, object content)
            {
                var localActionUrl = _url.Center().Urls.Video(id, title);
                return Htmls.a.Href(localActionUrl)[content];
            }*/


            public TagA VideoCategory(	System.Int32 categoryId, object content){
				var localActionUrl = _url.Center().Urls.VideoCategory(categoryId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA About(	object content){
				var localActionUrl = _url.Center().Urls.About();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA NewsAndActions(	object content){
				var localActionUrl = _url.Center().Urls.NewsAndActions();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Info(	object content){
				var localActionUrl = _url.Center().Urls.Info();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Actions(	object content){
				var localActionUrl = _url.Center().Urls.Actions();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ExpressOrder(	Specialist.Entities.Common.ViewModel.ExpressOrderVM model, object content){
				var localActionUrl = _url.Center().Urls.ExpressOrder(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA DownloadRegister(	System.String file, object content){
				var localActionUrl = _url.Center().Urls.DownloadRegister(file);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Vacancy(	System.Int32 vacancyID, object content){
				var localActionUrl = _url.Center().Urls.Vacancy(vacancyID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Advice(	System.Int32 adviceID, System.String urlName, object content){
				var localActionUrl = _url.Center().Urls.Advice(adviceID, urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA AdviceBlock(	object content){
				var localActionUrl = _url.Center().Urls.AdviceBlock();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Advices(	System.Nullable<System.Int32> index, object content){
				var localActionUrl = _url.Center().Urls.Advices(index);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Polls(	System.Nullable<System.Int32> index, object content){
				var localActionUrl = _url.Center().Urls.Polls(index);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Vacancies(	System.Nullable<System.Int32> index, object content){
				var localActionUrl = _url.Center().Urls.Vacancies(index);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA VacanciesForTeacher(	System.Nullable<System.Int32> index, object content){
				var localActionUrl = _url.Center().Urls.VacanciesForTeacher(index);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA VacanciesForEmployee(	System.Nullable<System.Int32> index, object content){
				var localActionUrl = _url.Center().Urls.VacanciesForEmployee(index);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MarketingAction(	System.String urlName, object content){
				var localActionUrl = _url.Center().Urls.MarketingAction(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MarketingActions(	object content){
				var localActionUrl = _url.Center().Urls.MarketingActions();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UsefulInformation(	System.String urlName, object content){
				var localActionUrl = _url.Center().Urls.UsefulInformation(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Certificate(	System.String urlName, object content){
				var localActionUrl = _url.Center().Urls.Certificate(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Skype(	object content){
				var localActionUrl = _url.Center().Urls.Skype();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseTrainers(	System.String urlName, object content){
				var localActionUrl = _url.Center().Urls.CourseTrainers(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SectionResponses(	System.String urlName, object content){
				var localActionUrl = _url.Center().Urls.SectionResponses(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CourseResponses(	System.String urlName, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Center().Urls.CourseResponses(urlName, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SectionTrainers(	System.String urlName, object content){
				var localActionUrl = _url.Center().Urls.SectionTrainers(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CertificationTrainers(	System.String urlName, object content){
				var localActionUrl = _url.Center().Urls.CertificationTrainers(urlName);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TrainerSections(	object content){
				var localActionUrl = _url.Center().Urls.TrainerSections();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA TrainerCertifications(	object content){
				var localActionUrl = _url.Center().Urls.TrainerCertifications();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Competitions(	object content){
				var localActionUrl = _url.Center().Urls.Competitions();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Competition(	System.Int32 competitionID, object content){
				var localActionUrl = _url.Center().Urls.Competition(competitionID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Competition(	Specialist.Entities.Profile.ViewModel.CompetitionVM model, object content){
				var localActionUrl = _url.Center().Urls.Competition(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA JubileeForm(	object content){
				var localActionUrl = _url.Center().Urls.JubileeForm();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA JubileeFormPost(	Specialist.Web.Root.Center.ViewModels.JubileeFormVM model, object content){
				var localActionUrl = _url.Center().Urls.JubileeFormPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SeminarRegistration(	System.Decimal groupId, object content){
				var localActionUrl = _url.Center().Urls.SeminarRegistration(groupId);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MtsEmployee(	object content){
				var localActionUrl = _url.Center().Urls.MtsEmployee();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MtsEmployeePost(	Specialist.Web.Root.Center.ViewModels.MtsEmployeeFormVM model, object content){
				var localActionUrl = _url.Center().Urls.MtsEmployeePost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA OrgCatalog(	object content){
				var localActionUrl = _url.Center().Urls.OrgCatalog();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA OrgCatalogPost(	Specialist.Web.Root.Center.ViewModels.OrgCatalogFormVM model, object content){
				var localActionUrl = _url.Center().Urls.OrgCatalogPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA OrderPaperCatalog(	object content){
				var localActionUrl = _url.Center().Urls.OrderPaperCatalog();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA OrderPaperCatalogPost(	Specialist.Web.Root.Center.ViewModels.PaperCatalogFormVM model, object content){
				var localActionUrl = _url.Center().Urls.OrderPaperCatalogPost(model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SeminarRegistrationPost(	System.Decimal groupId, Specialist.Web.Root.Center.ViewModels.SeminarRegisterFormVM model, object content){
				var localActionUrl = _url.Center().Urls.SeminarRegistrationPost(groupId, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Center().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Center().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Center().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Center().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Center().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Center().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static ClientControllerLinks _ClientControllerLinks = null;
		
		public static ClientControllerLinks Client(this UrlHelper urlHelper) {
		//	if(_ClientControllerLinks == null) _ClientControllerLinks = new ClientControllerLinks(urlHelper);
		//	return _ClientControllerLinks;
		return new ClientControllerLinks(urlHelper);
		}

		public class ClientControllerUrls{
			
			private UrlHelper _url;

			public ClientControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckPrivatePerson0(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.PrivatePerson((System.String)null, (System.Nullable<System.Int32>)null));
			}
			public string PrivatePerson(System.String urlName, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "PrivatePerson");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("urlName", urlName);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("PrivatePerson", "Client", routeValues);
			}
					void TypeCheckCorporateClients1(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.CorporateClients((System.String)null, (System.Nullable<System.Int32>)null));
			}
			public string CorporateClients(System.String urlName, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CorporateClients");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("urlName", urlName);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("CorporateClients", "Client", routeValues);
			}
					void TypeCheckSuccessStory2(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SuccessStory(0));
			}
			public string SuccessStory(System.Int32 storyID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SuccessStory");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("storyID", storyID);
								
				return _url.Action("SuccessStory", "Client", routeValues);
			}
					void TypeCheckOrgResponse3(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.OrgResponse(0));
			}
			public string OrgResponse(System.Int32 responseID){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "OrgResponse");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("responseID", responseID);
								
				return _url.Action("OrgResponse", "Client", routeValues);
			}
					void TypeCheckUserWorks4(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.UserWorks(0, 0, (System.Nullable<System.Int32>)null));
			}
			public string UserWorks(System.Int32 sectionID, System.Int32 workSectionID, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UserWorks");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("sectionID", sectionID);
									routeValues.Add("workSectionID", workSectionID);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("UserWorks", "Client", routeValues);
			}
					void TypeCheckChooseSectionResponses5(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.ChooseSectionResponses());
			}
			public string ChooseSectionResponses(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "ChooseSectionResponses");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("ChooseSectionResponses", "Client", routeValues);
			}
					void TypeCheckUploadFileForJob6(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.UploadFileForJob((System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase>)null));
			}
			public string UploadFileForJob(System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase> userfile){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "UploadFileForJob");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("userfile", userfile);
								
				return _url.Action("UploadFileForJob", "Client", routeValues);
			}
					void TypeCheckResume7(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.Resume((Specialist.Entities.Center.ViewModel.ResumeVM)null));
			}
			public string Resume(Specialist.Entities.Center.ViewModel.ResumeVM fillResume){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Resume");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("fillResume", fillResume);
								
				return _url.Action("Resume", "Client", routeValues);
			}
					void TypeCheckVacancies8(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.Vacancies((Specialist.Entities.Center.ViewModel.VacanciesVM)null));
			}
			public string Vacancies(Specialist.Entities.Center.ViewModel.VacanciesVM fillVacancy){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Vacancies");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("fillVacancy", fillVacancy);
								
				return _url.Action("Vacancies", "Client", routeValues);
			}
					void TypeCheckLetter9(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.Letter());
			}
			public string Letter(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Letter");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("Letter", "Client", routeValues);
			}
					void TypeCheckLetter10(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.Letter((Specialist.Entities.Center.ViewModel.LetterVM)null));
			}
			public string Letter(Specialist.Entities.Center.ViewModel.LetterVM fillLetter){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Letter");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("fillLetter", fillLetter);
								
				return _url.Action("Letter", "Client", routeValues);
			}
					void TypeCheckRequestForWebinar11(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.RequestForWebinar());
			}
			public string RequestForWebinar(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RequestForWebinar");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("RequestForWebinar", "Client", routeValues);
			}
					void TypeCheckRequestForWebinar12(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.RequestForWebinar((Specialist.Entities.Center.ViewModel.RequestForWebinarVM)null));
			}
			public string RequestForWebinar(Specialist.Entities.Center.ViewModel.RequestForWebinarVM fillRequestForWebinar){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RequestForWebinar");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("fillRequestForWebinar", fillRequestForWebinar);
								
				return _url.Action("RequestForWebinar", "Client", routeValues);
			}
					void TypeCheckSearchParamsVacancy13(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SearchParamsVacancy());
			}
			public string SearchParamsVacancy(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchParamsVacancy");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("SearchParamsVacancy", "Client", routeValues);
			}
					void TypeCheckSearchParamsVacancy14(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SearchParamsVacancy((Specialist.Entities.Center.ViewModel.SearchVacancyVM)null));
			}
			public string SearchParamsVacancy(Specialist.Entities.Center.ViewModel.SearchVacancyVM fillSearchVacancy){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchParamsVacancy");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("fillSearchVacancy", fillSearchVacancy);
								
				return _url.Action("SearchParamsVacancy", "Client", routeValues);
			}
					void TypeCheckSearchResultVacancy15(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SearchResultVacancy());
			}
			public string SearchResultVacancy(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchResultVacancy");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("SearchResultVacancy", "Client", routeValues);
			}
					void TypeCheckSearchParamsResume16(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SearchParamsResume());
			}
			public string SearchParamsResume(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchParamsResume");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("SearchParamsResume", "Client", routeValues);
			}
					void TypeCheckSearchParamsResume17(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SearchParamsResume((Specialist.Entities.Center.ViewModel.SearchResumeVM)null));
			}
			public string SearchParamsResume(Specialist.Entities.Center.ViewModel.SearchResumeVM fillSearchResume){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchParamsResume");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("fillSearchResume", fillSearchResume);
								
				return _url.Action("SearchParamsResume", "Client", routeValues);
			}
					void TypeCheckSearchResultResume18(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SearchResultResume());
			}
			public string SearchResultResume(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchResultResume");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("SearchResultResume", "Client", routeValues);
			}
					void TypeCheckLetterSent19(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.LetterSent());
			}
			public string LetterSent(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "LetterSent");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("LetterSent", "Client", routeValues);
			}
					void TypeCheckSearchUser20(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SearchUser());
			}
			public string SearchUser(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchUser");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("SearchUser", "Client", routeValues);
			}
					void TypeCheckSearchUser21(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.SearchUser((Specialist.Entities.Center.ViewModel.SearchUserVM)null));
			}
			public string SearchUser(Specialist.Entities.Center.ViewModel.SearchUserVM fillSearchUser){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "SearchUser");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("fillSearchUser", fillSearchUser);
								
				return _url.Action("SearchUser", "Client", routeValues);
			}
					void TypeCheckCheckRegCoupon22(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.CheckRegCoupon((System.String)null, (System.String)null));
			}
			public string CheckRegCoupon(System.String code, System.String email){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "CheckRegCoupon");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("code", code);
									routeValues.Add("email", email);
								
				return _url.Action("CheckRegCoupon", "Client", routeValues);
			}
					void TypeCheckBaseView23(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "Client", routeValues);
			}
					void TypeCheckBaseView24(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "Client", routeValues);
			}
					void TypeCheckBaseViewWithTitle25(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "Client", routeValues);
			}
					void TypeCheckRedirectToAction26(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "Client", routeValues);
			}
					void TypeCheckRedirectBack27(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "Client");
								
				return _url.Action("RedirectBack", "Client", routeValues);
			}
					void TypeCheckMView28(){
				CheckMethod<Specialist.Web.Controllers.Center.ClientController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "Client");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "Client", routeValues);
			}
				

		

		}


			public class ClientControllerLinks{
			
			private UrlHelper _url;

			public ClientControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static ClientControllerUrls _ClientControllerUrls = null;
			public ClientControllerUrls Urls { 
				get {
				//	if(_ClientControllerUrls == null) _ClientControllerUrls = new ClientControllerUrls(_url);
					//return _ClientControllerUrls;
					return new ClientControllerUrls(_url);
				}
			}

		
		
		


			public TagA PrivatePerson(	System.String urlName, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Client().Urls.PrivatePerson(urlName, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CorporateClients(	System.String urlName, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Client().Urls.CorporateClients(urlName, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SuccessStory(	System.Int32 storyID, object content){
				var localActionUrl = _url.Client().Urls.SuccessStory(storyID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA OrgResponse(	System.Int32 responseID, object content){
				var localActionUrl = _url.Client().Urls.OrgResponse(responseID);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UserWorks(	System.Int32 sectionID, System.Int32 workSectionID, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.Client().Urls.UserWorks(sectionID, workSectionID, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA ChooseSectionResponses(	object content){
				var localActionUrl = _url.Client().Urls.ChooseSectionResponses();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA UploadFileForJob(	System.Collections.Generic.IEnumerable<System.Web.HttpPostedFileBase> userfile, object content){
				var localActionUrl = _url.Client().Urls.UploadFileForJob(userfile);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Resume(	Specialist.Entities.Center.ViewModel.ResumeVM fillResume, object content){
				var localActionUrl = _url.Client().Urls.Resume(fillResume);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Vacancies(	Specialist.Entities.Center.ViewModel.VacanciesVM fillVacancy, object content){
				var localActionUrl = _url.Client().Urls.Vacancies(fillVacancy);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Letter(	object content){
				var localActionUrl = _url.Client().Urls.Letter();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Letter(	Specialist.Entities.Center.ViewModel.LetterVM fillLetter, object content){
				var localActionUrl = _url.Client().Urls.Letter(fillLetter);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RequestForWebinar(	object content){
				var localActionUrl = _url.Client().Urls.RequestForWebinar();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RequestForWebinar(	Specialist.Entities.Center.ViewModel.RequestForWebinarVM fillRequestForWebinar, object content){
				var localActionUrl = _url.Client().Urls.RequestForWebinar(fillRequestForWebinar);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchParamsVacancy(	object content){
				var localActionUrl = _url.Client().Urls.SearchParamsVacancy();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchParamsVacancy(	Specialist.Entities.Center.ViewModel.SearchVacancyVM fillSearchVacancy, object content){
				var localActionUrl = _url.Client().Urls.SearchParamsVacancy(fillSearchVacancy);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchResultVacancy(	object content){
				var localActionUrl = _url.Client().Urls.SearchResultVacancy();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchParamsResume(	object content){
				var localActionUrl = _url.Client().Urls.SearchParamsResume();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchParamsResume(	Specialist.Entities.Center.ViewModel.SearchResumeVM fillSearchResume, object content){
				var localActionUrl = _url.Client().Urls.SearchParamsResume(fillSearchResume);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchResultResume(	object content){
				var localActionUrl = _url.Client().Urls.SearchResultResume();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA LetterSent(	object content){
				var localActionUrl = _url.Client().Urls.LetterSent();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchUser(	object content){
				var localActionUrl = _url.Client().Urls.SearchUser();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA SearchUser(	Specialist.Entities.Center.ViewModel.SearchUserVM fillSearchUser, object content){
				var localActionUrl = _url.Client().Urls.SearchUser(fillSearchUser);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA CheckRegCoupon(	System.String code, System.String email, object content){
				var localActionUrl = _url.Client().Urls.CheckRegCoupon(code, email);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.Client().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Client().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.Client().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.Client().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.Client().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.Client().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
				
				
	//	private static SiteNewsControllerLinks _SiteNewsControllerLinks = null;
		
		public static SiteNewsControllerLinks SiteNews(this UrlHelper urlHelper) {
		//	if(_SiteNewsControllerLinks == null) _SiteNewsControllerLinks = new SiteNewsControllerLinks(urlHelper);
		//	return _SiteNewsControllerLinks;
		return new SiteNewsControllerLinks(urlHelper);
		}

		public class SiteNewsControllerUrls{
			
			private UrlHelper _url;

			public SiteNewsControllerUrls(UrlHelper url) {
				_url = url;
			}
			
			void CheckMethod<TController>(System.Linq.Expressions.Expression<System.Action<TController>> action) where TController : Controller {}

		
		
					void TypeCheckList0(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.List((System.String)null, (System.Nullable<System.Int32>)null));
			}
			public string List(System.String newsTypeUrl, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "List");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("newsTypeUrl", newsTypeUrl);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("List", "SiteNews", routeValues);
			}
					void TypeCheckSearch1(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.Search((System.String)null, 0, (System.Nullable<System.Int32>)null));
			}
			public string Search(System.String typeName, System.Int32 id, System.Nullable<System.Int32> pageIndex){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Search");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("typeName", typeName);
									routeValues.Add("id", id);
									routeValues.Add("pageIndex", pageIndex);
								
				return _url.Action("Search", "SiteNews", routeValues);
			}
					void TypeCheckDetails2(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.Details((System.Nullable<System.Int32>)null, (System.String)null));
			}
			public string Details(System.Nullable<System.Int32> newsID, System.String title){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "Details");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("newsID", newsID);
									routeValues.Add("title", title);
								
				return _url.Action("Details", "SiteNews", routeValues);
			}
					void TypeCheckAll3(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.All((System.Nullable<System.Int32>)null));
			}
			public string All(System.Nullable<System.Int32> year){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "All");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("year", year);
								
				return _url.Action("All", "SiteNews", routeValues);
			}
					void TypeCheckBaseView4(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.BaseView((System.String)null, (System.Object)null));
			}
			public string BaseView(System.String view, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("view", view);
									routeValues.Add("model", model);
								
				return _url.Action("BaseView", "SiteNews", routeValues);
			}
					void TypeCheckBaseView5(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.BaseView((Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseView(Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseView");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseView", "SiteNews", routeValues);
			}
					void TypeCheckBaseViewWithTitle6(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.BaseViewWithTitle((System.String)null, (Specialist.Web.Pages.PagePart[])null));
			}
			public string BaseViewWithTitle(System.String title, Specialist.Web.Pages.PagePart[] pageParts){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "BaseViewWithTitle");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("title", title);
									routeValues.Add("pageParts", pageParts);
								
				return _url.Action("BaseViewWithTitle", "SiteNews", routeValues);
			}
					void TypeCheckRedirectToAction7(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.RedirectToAction((System.Linq.Expressions.Expression<System.Action>)null));
			}
			public string RedirectToAction(System.Linq.Expressions.Expression<System.Action> action){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectToAction");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("action", action);
								
				return _url.Action("RedirectToAction", "SiteNews", routeValues);
			}
					void TypeCheckRedirectBack8(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.RedirectBack());
			}
			public string RedirectBack(){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "RedirectBack");
			//	routeValues.Add("controller", "SiteNews");
								
				return _url.Action("RedirectBack", "SiteNews", routeValues);
			}
					void TypeCheckMView9(){
				CheckMethod<Specialist.Web.Controllers.Center.SiteNewsController>(c => 
					c.MView((System.String)null, (System.Object)null));
			}
			public string MView(System.String viewName, System.Object model){
				var routeValues = new RouteValueDictionary();
			//	routeValues.Add("action", "MView");
			//	routeValues.Add("controller", "SiteNews");
									routeValues.Add("viewName", viewName);
									routeValues.Add("model", model);
								
				return _url.Action("MView", "SiteNews", routeValues);
			}
				

		

		}


			public class SiteNewsControllerLinks{
			
			private UrlHelper _url;

			public SiteNewsControllerLinks(UrlHelper url) {
				_url = url;
			}

			//private static SiteNewsControllerUrls _SiteNewsControllerUrls = null;
			public SiteNewsControllerUrls Urls { 
				get {
				//	if(_SiteNewsControllerUrls == null) _SiteNewsControllerUrls = new SiteNewsControllerUrls(_url);
					//return _SiteNewsControllerUrls;
					return new SiteNewsControllerUrls(_url);
				}
			}

		
		
		


			public TagA List(	System.String newsTypeUrl, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.SiteNews().Urls.List(newsTypeUrl, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Search(	System.String typeName, System.Int32 id, System.Nullable<System.Int32> pageIndex, object content){
				var localActionUrl = _url.SiteNews().Urls.Search(typeName, id, pageIndex);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA Details(	System.Nullable<System.Int32> newsID, System.String title, object content){
				var localActionUrl = _url.SiteNews().Urls.Details(newsID, title);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA All(	System.Nullable<System.Int32> year, object content){
				var localActionUrl = _url.SiteNews().Urls.All(year);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	System.String view, System.Object model, object content){
				var localActionUrl = _url.SiteNews().Urls.BaseView(view, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseView(	Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.SiteNews().Urls.BaseView(pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA BaseViewWithTitle(	System.String title, Specialist.Web.Pages.PagePart[] pageParts, object content){
				var localActionUrl = _url.SiteNews().Urls.BaseViewWithTitle(title, pageParts);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectToAction(	System.Linq.Expressions.Expression<System.Action> action, object content){
				var localActionUrl = _url.SiteNews().Urls.RedirectToAction(action);
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA RedirectBack(	object content){
				var localActionUrl = _url.SiteNews().Urls.RedirectBack();
				return  Htmls.a.Href(localActionUrl)[content];
			}
		


			public TagA MView(	System.String viewName, System.Object model, object content){
				var localActionUrl = _url.SiteNews().Urls.MView(viewName, model);
				return  Htmls.a.Href(localActionUrl)[content];
			}
				
		

		}
	

	}

}