using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Message.ViewModel
{
    public class SectionMessageListVM: IViewModel
    {
        public PagedList<UserMessage> Messages { get; set; }

        public MessageSection Section { get; set; }

        public string Title
        {
            get { return Section.Name; }
        }

	    public Dictionary<long, int> MessageCounts { get; set; }
    }
}