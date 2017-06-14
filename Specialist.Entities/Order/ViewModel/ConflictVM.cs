using System;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Order.ViewModel {
    public class ConflictVM: IViewModel {
        public string CustomerType { get; set; }
        public string Title {
            get { return "Конфликт"; }
        }
    }
}