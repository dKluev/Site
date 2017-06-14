using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Extension;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Passport;

namespace Specialist.Web.ActionFilters
{
    public class CustomControllerActionInvoker: ControllerActionInvoker
    {
        private readonly IUnityContainer _container;

        public CustomControllerActionInvoker(IUnityContainer container)
        {
            _container = container;
        }

        protected override FilterInfo GetFilters(ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
            foreach (var filter in filters.ActionFilters)
            {
                if(!filter.GetType().IsSubclassOf(typeof(FilterAttribute)))
                    continue;
                _container.BuildUp(filter.GetType(), filter);
            }
            foreach (var filter in filters.AuthorizationFilters)
            {
                if(!filter.GetType().IsSubclassOf(typeof(FilterAttribute)))
                    continue;
                _container.BuildUp(filter.GetType(), filter);
            }
            return filters;
        }
    }
}