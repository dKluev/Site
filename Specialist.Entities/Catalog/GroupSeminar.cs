using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.Catalog {
    public class GroupSeminar {
        public Group Group { get; set; }

	    public bool IsMsSeminar { get { return CourseTC.MsSeminars.Contains(Group.Course_TC); } }

        public List<CourseLink> Courses { get; set; }

        public GroupSeminar(Group group) : this(group, new List<CourseLink>()) {}

	    public News News { get; set; }

        public GroupSeminar(Group group, List<CourseLink> courses) {
            Group = group;
            Courses = courses;
        }
    }
}