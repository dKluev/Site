using System;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;
using FluentValidation; using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
using SimpleUtils.FluentAttributes.Utils;
using Specialist.Entities.Catalog;
using Specialist.Entities.Passport;
using Specialist.Services.Interface.Passport;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Extension;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.ViewModel;
using Specialist.Web.Const;

namespace Specialist.Web.Util {
	[ValidateInput(false)]
	public class ExtendedController : Controller {
		private User _user;

		public new User User {
			get { return _user ?? (_user = AuthService.CurrentUser); }
		}

		public HtmlHelper Html {
			get {
				return new HtmlHelper(new ViewContext(ControllerContext,
					new WebFormView(ControllerContext,"web"), ViewData, TempData, new StringWriter()),
					new ViewPage());
			}
		}

		[Dependency]
		public IAuthService AuthService { get; set; }

		public IUnityContainer UnityContainer { get; set; }

		[InjectionMethod]
		public void SetContainer(IUnityContainer container) {
			UnityContainer = container;
			ActionInvoker = new CustomControllerActionInvoker(container);
		}

		/*protected bool Validate<T>(T model) {
			foreach (var validationResult in new Validator().Validate(model)) {
				foreach (var errorMessage in validationResult.ErrorMessages) {
					ModelState.AddModelError(validationResult.PropertyName,
						errorMessage);
				}
			}
			FluentValidate(model);
			return ModelState.IsValid;
		}
*/
		public bool FluentValidate<T>(T model) {
			if(UnityContainer.IsRegistered(typeof(IValidator<T>))){
				var validator = UnityContainer.Resolve<IValidator<T>>();
				validator.Validate(model).AddToModelState(ModelState, string.Empty);
			}
			return ModelState.IsValid;
		}

		public RedirectToRouteResult RedirectToAction<TController>(
			Expression<Action<TController>> action, bool isPermanent = false) where TController : Controller {
			var routeValuesFromExpression =
				LinkExtension.GetRouteValuesFromExpression(action);
			if(isPermanent)
				return RedirectToRoutePermanent(routeValuesFromExpression);
			return RedirectToRoute(routeValuesFromExpression);
		}


		public RedirectToRouteResult RedirectToAction(
			Expression<Action> action) {
			var routeValuesFromExpression =
				LinkExtension.GetRouteValuesFromExpression(action);
			return RedirectToRoute(routeValuesFromExpression);
		}

		public ActionResult RedirectBack() {
			if (Request.UrlReferrer == null)
				return Redirect("/");
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}


		public void ShowMessage(string text) {
			Session[Htmls.ShowMessageKey] = text;
		}

		public void ShowErrorMessage(string text) {
			Session[Htmls.ShowErrorMessageKey] = text;
		}

		protected ActionResult ErrorView(HttpStatusCode statusCode, 
			string aspxerrorpath = null) {
			aspxerrorpath = aspxerrorpath ?? Request.Url.PathAndQuery;
			Response.StatusCode = (int) statusCode;
			return View(ViewNames.Error, new ErrorVM((int) statusCode, aspxerrorpath));
		}

		protected ActionResult NotFound() {
			return ErrorView(HttpStatusCode.NotFound);
		}

		public ActionResult MView(string viewName, object model = null) {
			if(CommonConst.IsMobile) {
				var mobileView = viewName + ".Mobile";
				var result = ViewEngines.Engines
					.FindView(ControllerContext, mobileView, null);
				if(result.View != null)
					return View(mobileView, model);
			}
			return View(viewName, model);
		}

	}
}