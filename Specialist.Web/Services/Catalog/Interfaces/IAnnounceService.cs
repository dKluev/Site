using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Announcement;
using Specialist.Entities.Context;

namespace Specialist.Services.Interface
{
    public interface IAnnounceService
    {
        IQueryable<Announce> GetAllFor(object obj);
    	List<Announce> GetAllForSection(int sectionId);
    	List<Announce> GetAllForEntity(Type type, object pk);
	    List<Announce> GetAllForMainCached();
	    List<Announce> GetHotGroupsForSection(Section section);
    }
}