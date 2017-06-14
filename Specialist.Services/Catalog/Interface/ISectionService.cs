using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Interface
{
    public interface ISectionService: IRepository<Section>
    {

    	List<Section> AllActiveSections();
	    List<Section> GetSectionsTree();
	    List<Section> GetChildren(int sectionId);
	    List<Section> GetTreeWithSubsections();
	    Dictionary<int, List<string>> NotAnnounce();
    }
}