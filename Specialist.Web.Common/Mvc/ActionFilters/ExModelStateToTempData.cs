using System.Collections.Generic;
using System.Web.Mvc;

namespace Specialist.Web.ActionFilters {
    public class ExModelStateToTempDataAttribute : ActionFilterAttribute {
        // Fields
        public const string TempDataKey = "__MvcContrib_ValidationFailures__";

        // Methods
        private static void CopyModelStateToTempData(ModelStateDictionary modelState, TempDataDictionary tempData) {
            tempData["__MvcContrib_ValidationFailures__"] = modelState;
        }

        private void CopyTempDataToModelState(ModelStateDictionary modelState, TempDataDictionary tempData) {
            if (tempData.ContainsKey("__MvcContrib_ValidationFailures__")) {
                ModelStateDictionary dictionary = tempData["__MvcContrib_ValidationFailures__"] as ModelStateDictionary;
                if (dictionary != null) {
                    foreach (KeyValuePair<string, ModelState> pair in dictionary) {
                        if (modelState.ContainsKey(pair.Key)) {
                            modelState[pair.Key].Value = pair.Value.Value;
                            foreach (ModelError error in pair.Value.Errors) {
                                modelState[pair.Key].Errors.Add(error);
                            }
                            continue;
                        }
                        modelState.Add(pair.Key, pair.Value);
                    }
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            ModelStateDictionary modelState = filterContext.Controller.ViewData.ModelState;
            ControllerBase controller = filterContext.Controller;
            if (filterContext.Result is ViewResultBase) {
                this.CopyTempDataToModelState(controller.ViewData.ModelState, controller.TempData);
            }
            else if (((filterContext.Result is RedirectToRouteResult) || (filterContext.Result is RedirectResult))) {
                CopyModelStateToTempData(controller.ViewData.ModelState, controller.TempData);
            }
        }
    }




}