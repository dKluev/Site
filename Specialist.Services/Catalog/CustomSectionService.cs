using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Interface;

namespace Specialist.Services
{
    public class CustomSectionService : ICustomSectionService
    {
        public CustomSection GetByUrlNames(string typeUrlName, string sectionUrlName)
        {
            var context = new SpecialistWebDataContext();

            var customSections =
                from customSection in context.CustomSections
                where customSection.CustomSectionType.UrlName == typeUrlName &&
                    customSection.UrlName == sectionUrlName
                select customSection;
            return customSections.FirstOrDefault();
        }
    }
}