using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Interface
{
    public interface ICertificationService : IRepository<Certification>
    {
        List<Certification> GetAllForCourse(string courseTC);
    }
}