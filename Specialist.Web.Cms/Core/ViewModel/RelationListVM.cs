using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Services.Catalog;

namespace Specialist.Web.Cms.Core.ViewModel
{
    public class RelationListVM
    {
        public List<SiteObjectRelation> Relations { get; set; }

        public bool ForLinkCreator { get; set; }

    	public Type EntityType { get; set; }

	    public bool Sortable {
		    get { return SiteObjectRelationService.types.Contains(EntityType); }
	    }

		public SiteObject SiteObject { get; set; }
    }
}