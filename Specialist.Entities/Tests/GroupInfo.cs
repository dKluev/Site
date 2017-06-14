using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Context;

namespace Specialist.Entities.Tests {
	public partial class GroupInfo {

	    private EntityRef<Group> _Group = default(EntityRef<Group>);
        [Association(Storage = "_Group", ThisKey = "Group_ID",
            OtherKey = "Group_ID", IsForeignKey = true)]
        public Group Group {
            get { return _Group.Entity; }
            set { _Group.Entity = value; }
        }
		 
	}
}