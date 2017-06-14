using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.Links {
    public class CourseLink : ICourseLink {
        public string Name { get; set; }
        public string WebName { get; set; }
        public string WebShortName { get; set; }
        public string UrlName { get; set; }
        public bool? IsTrack { get; set; }

		public bool IsActive { get; set; }
		public bool IsNew { get; set; }

        public string CourseTC { get; set; }
    }
}