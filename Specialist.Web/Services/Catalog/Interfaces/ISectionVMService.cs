using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Core;

namespace Specialist.Services.Interface
{
    public interface ISectionVMService
    {
        SectionVM GetBy(string urlName, int? id = null);

      
    	IQueryable<Response> GetResponses(int sectionId);

	    List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
		    GetSectionWithEntityTree();

	    List<string> CoursesForInvoice();
    }
}