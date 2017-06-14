using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests.Consts;

namespace Specialist.Entities.Tests {
	public partial class UserTest {
		 
		public bool IsPass {
			get { return UserTestStatus.PassStatuses.Contains(Status); }
		}

		public bool IsPrerequisite {
			get { return Course_TC != null; }
		}
		public bool IsCoursePlanned {
			get { return TestModuleSetId.HasValue; }
		}

		public bool NormalTest {
			get { return !IsPrerequisite && !IsCoursePlanned; }
		}
	    private EntityRef<User> _User = default(EntityRef<User>);
        [Association(Storage = "_User", ThisKey = "UserId",
            OtherKey = "UserID", IsForeignKey = true)]
        public User User {
            get { return _User.Entity; }
            set { _User.Entity = value; }
        }
	}
}