using System.Collections.Generic;
using System.IO;
using Specialist.Web.Cms.Controllers;
using Specialist.Web.Common.Mvc.Controllers;

namespace Specialist.Web.Cms.Const
{
    public class Common
    {
        public const string PageIndex = "pageIndex";

        public const string FolderControls = "Controls/";

        public const string OrderColumn = "orderColumn";
        
        public const string Descending = "descending";

        public const string IsDesc = "isDesc";

        public static string ControllerNamespace = typeof(HomeController).Namespace;

        public static string CommonControllerNamespace = typeof(AccountController).Namespace;

        public static string[] ActiveProperties = new[] { "IsActive", "PublishInSite" };

        public const string SiteDomain = "http://www.specialist.ru";

        public const string ControlPosfix = "Entity";



      
    }
}