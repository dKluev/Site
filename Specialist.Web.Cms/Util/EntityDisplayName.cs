using SimpleUtils.Utils;
using Specialist.Web.Common.Html;

namespace Specialist.Web.Cms.Util
{
    public class EntityDisplayName
    {
        public static string CutLong(string name)
        {
            name = StringUtils.RemoveTags(name);
            if (name.Length < 100)
                return name;
            return name.Substring(0, 100) + "...";
        }
    }
}