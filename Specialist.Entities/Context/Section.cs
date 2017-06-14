using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Context
{
    public partial class Section:IForMainPage, IEntityCommonInfo
    {
	    private List<Section> _subSections = new List<Section>();

	    public List<Section> SubSections {
		    get { return _subSections; }
		    set { _subSections = value; }
	    }
    }
}