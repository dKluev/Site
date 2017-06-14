using System.Web;
using System.Web.Routing;

namespace Specialist.Web.Common.Site {
	public class LowercasingRoute : RouteBase
{
    public LowercasingRoute(RouteBase routeToWrap)
    {
        _inner = routeToWrap;
    }

    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
        return _inner.GetRouteData(httpContext);
    }

    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
    {
        var result = _inner.GetVirtualPath(requestContext, values);
        if (result != null && result.VirtualPath != null && !result.VirtualPath.Contains("?"))
        {
            result.VirtualPath = 
                result.VirtualPath.ToLowerInvariant();
        }
        return result;
    }
    RouteBase _inner;
}
}