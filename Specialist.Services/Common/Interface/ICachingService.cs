using System.Linq;
using System.Reflection;
using System.Web.Caching;

namespace Specialist.Services.Interface
{
    public interface ICachingService
    {
        Cache Cache { get; }
        void SetCache(string cacheKey, MethodBase methodBase);
    }
}