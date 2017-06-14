using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Interface;
using SimpleUtils.Extension;

namespace Specialist.Entities.Context
{
    public partial class Certification : IEntityCommonInfo
    {
        public IEnumerable<Exam> Exams
        {
            get
            {
                return CertificationExams.Select(ce => ce.Exam);
            }
        }

        public int WebSortOrder { get { return 0; } }

    }
}