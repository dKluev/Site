using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Announcement;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Common.Logic;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using SimpleUtils;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Catalog.ViewModel
{
    public class SectionVM: IViewModel
    {
    	private Section _section;
    	public Section Section {
    		get { return _section; }
    		set { _section = value; 
				Description = new TextWithInfoTags(_section.Description);
			}
    	}



    	public List<int> SectionIdContainUserWorks { get; set; }

    	public TextWithInfoTags Description { get; set; }

	    public List<Profession> Professions {
		    get {
			    return EntityWithTags.SelectMany(x => _.List(x.Entity).AddFluent(x.List))
					.OfType<Profession>().Distinct(x => x.Profession_ID).ToList();
		    }
	    } 


        public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>> 
            EntityWithTags { get; set; }
 
        public List<Announce> Announces { get; set; }

    	public string Title {
    		get {
    			return Section.Name;
    		}
    	}

	    public Section Parent { get; set; }

	    public List<Section> SubSections {
		    get { return EntityWithTags.Select(x => x.Entity).OfType<Section>().ToList(); }
		    
	    }
    }
}