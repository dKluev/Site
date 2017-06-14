using System;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel {
    public class AdviceListVM: IViewModel {

        public PagedList<Advice> Advices { get; set; }

        public string Title {
            get { return "Советы «Специалиста»"; }
        }
    }
}