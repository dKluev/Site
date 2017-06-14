using System;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;

namespace Specialist.Entities.Center.ViewModel
{
    public class NewsListVM: IViewModel
    {
        public PagedList<News> News { get; set; }

        public NewsType Type { get; set; }
        public virtual string Title {
            get { return Type.Name; }
        }
    }
}