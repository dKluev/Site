using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using Specialist.Entities.Catalog;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Const;
using Specialist.Web.Core.Logic;
using Specialist.Web.Pages;
using Specialist.Web.Util;

namespace Specialist.Web.Core {
	public class ViewController : ExtendedController {
		[Dependency]
		public IMetaDataProvider MetaDataProvider { get; set; }

		protected virtual bool IsBootStrap {
			get { return false; }
		}

		public void AddColumns<T>(AjaxGridVM model,
			params Expression<Func<T, object>>[] selectors) {
			var metaData = MetaDataProvider.Get(typeof (T)).GetProperties();
			var columns = new Dictionary<string, AjaxGridVM.Column>();
			foreach (var selector in selectors) {
				var info = ExpressionUtils.GetPropertyInfo(selector);
				var meta = metaData.First(x => x.Info == info);
				columns.Add(meta.DisplayName(), new AjaxGridVM.Column(info.Name));
			}
			model.ColumnTitles.AddRange(columns.Select(x => x.Key));
			model.Columns.AddRange(columns.Select(x => x.Value));
		}

		protected ActionResult ErrorJson() {
			var error = ModelState.Values.SelectMany(x => x.Errors)
				.Select(x => x.ErrorMessage).JoinWith("<br/>");
			var names = ModelState.Where(x => x.Value.Errors.Any())
				.Select(x => x.Key);

			return Json(new FormResponseVM(error, names.ToList()));
		}

		protected ActionResult OkOrErrorJson() {
			if(ModelState.IsValid) return OkJson();
			return ErrorJson();
		}

		protected ActionResult UrlJson(string url) {
			return Json(new FormResponseVM(url));
		}
		protected ActionResult OkJson() {
			return Json("ok");
		}
		protected ActionResult MessageJson(string message) {
			return Json(new {message});
		}

		public ActionResult BaseView(string view, object model) {
			return BaseView(new PagePart(view, model));
		}

		public ActionResult BaseView<T>(BaseView<T> view, T model) {
			return BaseView(new PagePart(view.Init(model,Url).Get().ToString()));
		}

		public ActionResult BaseViewWithModel<T>(BaseView<T> view, T model) {
			view.HtmlHelper = Html; 
			return BaseView(new PagePart(view.Init(model,Url)));
		}

		public ActionResult BaseView(params PagePart[] pageParts) {
			return BaseViewWithTitle(null, pageParts);
		}

		public ActionResult BaseViewWithTitle(string title, params PagePart[] pageParts) {
			var model = new BaseVM {
				Parts = pageParts.Where(x => x != null).ToList(),
				Title = title,
				IsBootStrap = IsBootStrap
			};
			return ViewWithBaseVM(model);
		}

		protected ActionResult ViewWithBaseVM(BaseVM model) {
			if (CommonConst.IsMobile && !model.IsBootStrap)
				return View(Const.Views.Shared.Core.MobileView, model);
			if (Request.IsAjaxRequest())
				return View(PartialViewNames.BaseViewContent, model);
			var viewName = model.IsBootStrap ? Const.Views.Shared.Core.BootStrapView : ViewNames.BaseView;
			return View(viewName, model);
		}

		protected ActionResult JsonGet(object data) {
			return Json(data, JsonRequestBehavior.AllowGet);
		}

	}
}