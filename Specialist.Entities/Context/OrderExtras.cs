using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Specialist.Entities.Context {
	public partial class OrderExtras {
		
        private EntityRef<Extras> _Extras = default(EntityRef<Extras>);
        [Association(Storage = "_Extras", ThisKey = "Extras_ID",
            OtherKey = "Extras_ID", IsForeignKey = true)]
        public Extras Extras {
            get { return _Extras.Entity; }
            set { _Extras.Entity = value; }
        }
	}
}