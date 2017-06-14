using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;

namespace Specialist.Services
{
    public class CertificationService: Repository<Certification>, ICertificationService
    {
        public CertificationService(IContextProvider contextProvider) 
            : base(contextProvider) {}

        public List<Certification> GetAllForCourse(string courseTC)
        {
            var certifications = 
                from certificationExam in context.GetTable<CertificationExam>()
                where context.GetTable<Exam>()
                    .Where(e => e.ExamCourses.Any(ec => ec.Course_TC == courseTC))
                    .Select(e => e.Exam_ID).Contains(certificationExam.Exam_ID)
                select certificationExam.Certification;

            return certifications.Distinct().ToList();
        }

    }
}