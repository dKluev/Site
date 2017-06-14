using SimpleUtils;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.Catalog.Links.Interfaces {
    public interface ICourseLink {
        string Name { get; }
        string WebName { get;}
        string WebShortName { get; }
        string UrlName { get; }
        bool? IsTrack { get; set; }
    }

    public static class ICourseLinkMixin {
        public static string GetName(this ICourseLink courseLink) {
            return courseLink.WebName.IsEmpty() 
                ? courseLink.Name 
                : courseLink.WebName;
        }

		public static string GetShortName(this ICourseLink courseLink) {
            return courseLink.WebShortName.IsEmpty() 
                ? courseLink.GetName() 
                : courseLink.WebShortName;
        }

    }
}