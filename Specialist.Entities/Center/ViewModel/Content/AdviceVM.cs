using System;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel {
    public class AdviceVM: IViewModel {

        public Advice Advice { get; set; }

        public string Title {
            get { return Advice.Name; }
        }
    }
}