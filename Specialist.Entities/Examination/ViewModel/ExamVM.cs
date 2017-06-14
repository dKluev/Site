using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;

namespace Specialist.Entities.ViewModel {
    public class ExamVM : IViewModel {
        public Exam Exam { get; set; }

        public List<Group> Groups { get; set; }

        public string Title {
            get { return "Экзамен " + Exam.ExamName; }
        }
    }
}