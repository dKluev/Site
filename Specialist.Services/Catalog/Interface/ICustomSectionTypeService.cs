using Specialist.Entities.Context;

namespace Specialist.Services.Interface
{
    public interface ICustomSectionTypeService
    {
        CustomSectionType GetByUrlName(string urlName);
    }
}