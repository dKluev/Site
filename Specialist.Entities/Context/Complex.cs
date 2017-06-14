using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Interface;
using SimpleUtils;
using System.Linq;
using Specialist.Entities.Const;

namespace Specialist.Entities.Context
{
    public partial class Complex : IEntityCommonInfo
    {
        public int WebSortOrder { get { return 0; } }

    }
}