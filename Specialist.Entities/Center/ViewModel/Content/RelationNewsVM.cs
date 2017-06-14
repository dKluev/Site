using System;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;

namespace Specialist.Entities.Center.ViewModel
{
    public class RelationNewsVM: NewsListVM
    {
    	public SiteObject SiteObject { get; set; }

        public override string Title {
            get { return "Новости обучения " + SiteObject.Name; }
        }
    }
}