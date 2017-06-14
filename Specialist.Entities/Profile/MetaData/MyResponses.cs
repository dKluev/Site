using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.ViewModel
{
    public class MyResponses: IViewModel
    {
        public PagedList<Response> Responses { get; set; }
        public string Title
        {
            get { return "Мои отзывы"; }
        }
    }
}