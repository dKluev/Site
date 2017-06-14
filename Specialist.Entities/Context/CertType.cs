using System;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Context {
    public partial class CertType:IEntityCommonInfo {
	    public string name = null;
        public string Name {
            get { return name ?? CertTypeName; }
	        set {
		        name = StringUtils.RemoveTags(value);
		        CertTypeName = value;
	        }
        }

        public int WebSortOrder {
            get { return SortOrder; }
        }
    }
}