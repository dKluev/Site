using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Context {
    public partial class Competition {

        private EntityRef<User> _Winner = default(EntityRef<User>);

        [Association(Storage = "_Winner", ThisKey = "WinnerID",
            OtherKey = "UserID")]
        public User Winner {
            get { return _Winner.Entity; }
            set { _Winner.Entity = value; }
        }


        private EntityRef<Course> _Course = default(EntityRef<Course>);

        [Association(Storage = "_Course", ThisKey = "Course_TC",
            OtherKey = "Course_TC")]
        public Course Course {
            get { return _Course.Entity; }
            set { _Course.Entity = value; }
        }

    }
}