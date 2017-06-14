using System;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Context {
	public partial class Provider: IEntityCommonInfo {
		public string UrlName {
			get { return Provider_ID.ToString(); }
		}

		public int WebSortOrder {
			get { return 0; }
		}
	}
}