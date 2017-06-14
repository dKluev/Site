using System.Collections.Generic;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.Education.ViewModel
{
    public class ConsultationListVM
    {
        public List<Group> Groups { get; set; }

        public IEnumerable<Group> Seminars
        {
            get
            {
                return Groups.Where(g => g.Course_TC == CourseTC.Seminar);
            }
        }

        public IEnumerable<Group> Consultations
        {
            get
            {
                return Groups.Where(g => g.Course_TC != CourseTC.Seminar);
            }
        }
    }
}