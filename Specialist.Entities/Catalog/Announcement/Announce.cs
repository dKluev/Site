using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Entities.Announcement
{
    public class Announce
    {
        public Course Course { get; set; }

        public List<Group> AnnounceGroups { get; set; }

    }
}