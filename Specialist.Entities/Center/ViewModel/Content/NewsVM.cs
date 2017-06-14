using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel
{
    public class NewsVM: IViewModel
    {
        public News News { get; set; }

        public string Title {
            get { return News.Title; }
        }

	    public Group Seminar { get; set; }

    }
}