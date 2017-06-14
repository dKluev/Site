using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Context {
	public partial class UserFile {
		 
        private EntityRef<User> _User = default(EntityRef<User>);
        [Association(Storage = "_User", ThisKey = "UserID",
            OtherKey = "UserID", IsForeignKey = true)]
        public User User
        {
            get { return _User.Entity; }
            set { _User.Entity = value; }
        }
	}
}