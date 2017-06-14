using System;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel {
    public class PollListVM: IViewModel {

        public PagedList<Poll> Polls { get; set; }

        public string Title {
            get { return "Опросы"; }
        }
    }
}