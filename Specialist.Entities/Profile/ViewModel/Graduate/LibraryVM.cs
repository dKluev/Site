using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Education;

namespace Specialist.Entities.Profile.ViewModel {
    public class LibraryVM: IViewModel {

        public string Title {
            get { return "Электронная библиотека учебных материалов"; }
        }

	    public List<Tuple<Course, List<CourseFileVM>>> Files { get; set; }
    }
}