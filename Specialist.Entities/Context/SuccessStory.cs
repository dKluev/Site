using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Context {
    public partial class SuccessStory {

            
        private EntityRef<User> _User = default(EntityRef<User>);
        [Association(Storage = "_User", ThisKey = "UserID",
            OtherKey = "UserID", IsForeignKey = true)]
        public User User {
            get { return _User.Entity; }
            set { _User.Entity = value; }
        }

		   private EntityRef<Profession> _Profession = default(EntityRef<Profession>);
        [Association(Storage = "_Profession", ThisKey = "Profession_ID",
            OtherKey = "Profession_ID", IsForeignKey = true)]
        public Profession Profession {
            get { return _Profession.Entity; }
            set { _Profession.Entity = value; }
        }
    }
}