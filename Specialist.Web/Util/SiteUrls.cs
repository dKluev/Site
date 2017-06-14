using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using SimpleUtils;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Web.Util
{
    public static class SiteUrls
    {
	
        public static bool OpenForRobots() {
            var url = HttpContext.Current.Request.Url.AbsolutePath;
            var allowed = new List<string> {
                "/Message/.*"
            };

            var denied = new List<string> {
            };
            return allowed.Any(x => Regex.IsMatch(url, x))
                && !denied.Any(x => Regex.IsMatch(url, x));
        }

       
    }
}