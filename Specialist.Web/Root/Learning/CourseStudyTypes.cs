using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentHtml.Tags;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Web.Common.Html;

namespace Specialist.Entities.Const
{
    public class CourseStudyTypes {
	    public const int Intramural = 1;
	    public const int Webinar = 2;
	    public const int OpenLearning = 3;
	    public const int IntraExtra = 4;

	    public static Dictionary<int, TagA> Links = new Dictionary<int, TagA> {
		    {Intramural, H.Anchor(SimplePages.FullUrls.Fulltime, "Очное")},
		    {Webinar, H.Anchor(SimplePages.FullUrls.Webinar, "Вебинар")},
		    {OpenLearning, H.Anchor(SimplePages.FullUrls.OpenClasses, "Открытое")},
		    {IntraExtra, H.Anchor(SimplePages.FullUrls.IntraExtramural, "Очно-заочное")}
	    };

	    public static string GetLinks(Group group, bool isWebinar) {
		    var types = new List<int>();
		    if (isWebinar) {
			    types.Add(Webinar);
		    }
		    if (group.IsOpenLearning) {
			    types.Add(OpenLearning);
		    }
		    if (group.IsIntraExtramural) {
			    types.Add(IntraExtra);
		    }
		    if (!types.Any()) {
			    types.Add(Intramural);
		    }
		    return types.Select(x => Links[x].ToString()).JoinWith("<br/>");
	    } 
    }
}