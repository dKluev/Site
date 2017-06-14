using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Entities.Context.ViewModel
{
    public class EditExamVM
    {
        public List<Group> Groups { get; set; }

        public OrderExam OrderExam { get; set; }
    }
}