using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.ViewModel
{
    public class SeminarCompleteVM : IViewModel {
        public GroupSeminar GroupSeminar { get; set; }

        public SeminarCompleteVM(GroupSeminar groupSeminar) {
            GroupSeminar = groupSeminar;
        }

        public string Title {
            get { return "Заявка принята"; }
        }
    }
}