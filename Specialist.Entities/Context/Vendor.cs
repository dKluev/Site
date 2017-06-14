using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context
{
    public partial class Vendor: IForMainPage, IEntityCommonInfo
    {
        public const int Microsoft = 1;

	    public static List<int> ForTest = _.List(Microsoft,
			21,
62,
82,
4,
145,
60,
14,
28,
3);
    }
}