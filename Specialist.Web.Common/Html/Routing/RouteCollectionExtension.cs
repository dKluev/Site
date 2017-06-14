using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleUtils;

namespace Specialist.Web.Util.Routing
{
  
    public static class RouteCollectionExtension
    {
        /// <summary>
        /// Register as /value1/1/value2/2
        /// </summary>
        public static void RegisterUrlNamedParameters(this RouteCollection routes, 
            string urlPrefix, string controller, string action, 
            RoutingParameter[] parameters)
        {
            foreach (var parameterCombination in
              Combinatorial.GetAllSubset(parameters.Length))
            {
                var url = urlPrefix;
                var routeValues = new RouteValueDictionary
                                   {
                                       {"controller", controller},
                                       {"action", action}
                                   };

                foreach (var i in parameterCombination)
                {
                    var parameter = parameters[i];
                    url += string.Format("/{0}/{{{1}}}",
                                         parameter.NameInUrl,
                                         parameter.NameInAction);
                }

                foreach (var parameter in parameters)
                {
                    routeValues.Add(parameter.NameInAction, parameter.DefaultValue);
                }


                routes.Add(Guid.NewGuid().ToString(), new Route(url,
                                     routeValues, new MvcRouteHandler()));
            }
        }

       
    }
}