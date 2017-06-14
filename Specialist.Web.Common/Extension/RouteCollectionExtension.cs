using System.Web.Mvc;
using System.Web.Routing;

namespace Specialist.Web.Common.Extension
{
    public static class RouteCollectionExtension
    {

     /*   public static void RegisterController<T>(
          this System.Web.Routing.RouteCollection routeCollection )
        {
            var contollerName = typeof(T).Name.Remove("Controller");
            RegisterController(routeCollection, contollerName);
        }*/

        public static void RegisterController(this RouteCollection routeCollection, 
            string contollerName)
        {
            routeCollection.MapRoute(
                contollerName + "Defailt",
                contollerName + "/{action}",
                new
                {
                    controller = contollerName,
                }
                );
        }

     /*   public static void RegisterActions<Tc>(this RouteCollection routes,
          params Expression<Func<Tc, ActionResult>>[] actions) where Tc : IController
        {

            var controllerName = typeof(Tc).Name.Remove("Controller");
            foreach (var action in actions)
            {
                Register(action, routes, controllerName);
            }
        }

        private static void Register<Tc>(Expression<Func<Tc, ActionResult>> action, RouteCollection routes, string controllerName) where Tc : IController
        {
            Register<Tc>(action, routes, controllerName, null);
        }

        private static void Register<Tc>(Expression<Func<Tc, ActionResult>> action, RouteCollection routes, string controllerName, string urlWithoutParameters) where Tc : IController
        {

            var methodCall = action.Body.As<MethodCallExpression>();
            var methodInfo = methodCall.Method;
            var typedControllerAction = new ControllerAction<Tc>(action);
            var url = typedControllerAction.DefaultUrl;
            if(!urlWithoutParameters.IsEmpty())
                url = urlWithoutParameters + typedControllerAction.ParametersPart;
            routes.Add(controllerName + methodInfo.Name, 
                new Route(url, 
                    typedControllerAction.DefaultValues, 
                    new MvcRouteHandler()));
        }

        public static void RegisterDefault<Tc>(this RouteCollection routes,
            Expression<Func<Tc, ActionResult>> action) where Tc : IController
        {

            var controllerName = typeof(Tc).Name.Remove("Controller");
            Register(action, routes, controllerName, controllerName + "/");
        }

        public static void RegisterWithoutController<Tc>(this RouteCollection routes,
    Expression<Func<Tc, ActionResult>> action) where Tc : IController
        {

            var controllerName = typeof(Tc).Name.Remove("Controller");
            var methodCall = action.Body.As<MethodCallExpression>();
            Register(action, routes, controllerName, methodCall.Method.Name + "/");
        }


        public static void RegisterAction<Tc>(this RouteCollection routes,
          Expression<Func<Tc, ActionResult>> action, string urlWithoutParameters) where Tc : IController
        {
            var controllerName = typeof(Tc).Name.Remove("Controller");
            Register(action, routes, controllerName, urlWithoutParameters);
        }*/
    }
}