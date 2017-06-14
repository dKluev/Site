using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Services.Interface
{
    public interface ICustomSectionService
    {
        CustomSection GetByUrlNames(string typeUrlName, string sectionUrlName);
    }
}