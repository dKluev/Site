using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Interface
{
    public interface IExamService: IRepository<Exam>
    {
        Exam GetByNumber(string number);
    }
}