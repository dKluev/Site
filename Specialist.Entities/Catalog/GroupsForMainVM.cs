using System.Collections.Generic;
using Specialist.Entities.Announcement;
using Specialist.Entities.Context;


namespace Specialist.Entities.Catalog {
	public class GroupsForMainVM {
		public City City { get; set; }

		public List<Group> Groups { get; set; }
	}
}