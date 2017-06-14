using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Core;

namespace Specialist.Entities.Common.ViewModel
{
    public class EntityWithSubEntityVM : 
        List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
    {
        
    }
}