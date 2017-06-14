using System.Collections.Generic;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.ViewModel {
    public class ElearningCourse {
        public CourseLink CourseLink { get; set; }

        public List<PriceView> Prices { get; set; }
    }
}