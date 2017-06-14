using System;
using System.Web;
using Specialist.Entities.Logic;

namespace Specialist.Web.Util
{
    public static class ResponseHeader
    {
        public static void SetLastModifiedDate(object obj)
        {
            var lastModifiedDate = (new LastModifiedFinder()).Find(obj);
            if(lastModifiedDate.HasValue)
            {
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Items["Last-modified"] =
                    lastModifiedDate.Value;
//                HttpContext.Current.Response.AddHeader("Last-modified", 
//                    lastModifiedDate.Value.ToUniversalTime().ToString("r"));
            }
        }

        public static void SetNoCache() {
//            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        /*    HttpContext.Current.Response.Cache.SetExpires(
                );*/
//             HttpContext.Current.Response.AddHeader("Expires" ,
//                 new DateTime(2005, 1, 1).ToUniversalTime().ToString("r"));
//            HttpContext.Current.Response.Cache.SetNoStore();
        }
    }
}