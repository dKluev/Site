using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Core;

namespace Specialist.Services.Catalog.Interface
{
    public interface IEntityCommonService {
        List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
            GetEntityWithTags(object entity);

        List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
            GetSubSections(IEnumerable<IEntityCommonInfo> entities);

        SimplePageVM Get(SimplePage page);
    }
}