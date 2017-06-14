using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;

namespace Specialist.Services.Examination
{
    public class ExamService: Repository<Exam>, IExamService
    {
        public ExamService(IContextProvider contextProvider) : base(contextProvider) {}

        public Exam GetByNumber(string number)
        {
            return context.GetTable<Exam>().FirstOrDefault(e => e.Exam_TC == number);
        }

    }
}