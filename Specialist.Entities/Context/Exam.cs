using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Specialist.Entities.Examination.Const;

namespace Specialist.Entities.Context
{
    public partial class Exam
    {


        public List<Certification> Certifications
        {
            get
            {
                return CertificationExams.Select(ce => ce.Certification).ToList();
            }
        }

        public List<Course> Courses {
            get {
                return ExamCourses.Select(ce => ce.Course).ToList();
            }
        }
    }
}