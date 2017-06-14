using System;
using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Web.Cms.ViewModel {
	public class ReverseRelationSortingVM {
		public string ObjectType { get; set; }

		public List<SiteObjectRelation> Relations { get; set; }

		public string ClassName { get; set; }

		public SiteObject SiteObject { get; set; }

		public ReverseRelationSortingVM() {
			Relations = new List<SiteObjectRelation>();
		}
	}
}