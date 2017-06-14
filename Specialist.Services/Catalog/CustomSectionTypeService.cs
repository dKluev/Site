using System.Data.Linq;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Interface;

namespace Specialist.Services
{
    public class CustomSectionTypeService: ICustomSectionTypeService
    {
        public CustomSectionType GetByUrlName(string urlName)
        {
            var context = new SpecialistWebDataContext();
            var loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<CustomSectionType>(cst => cst.CustomSections);
            context.LoadOptions = loadOptions;
            var sectionTypes =
                from sectionType in context.CustomSectionTypes
                where sectionType.UrlName == urlName
                select sectionType;
            return sectionTypes.FirstOrDefault();
        }
    }
}