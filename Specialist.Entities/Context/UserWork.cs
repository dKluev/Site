using System.Data.Linq;
using System.Data.Linq.Mapping;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Context {
    public partial class UserWork {
        private EntityRef<Employee> _Trainer = default(EntityRef<Employee>);
        [Association(Storage = "_Trainer", ThisKey = "Trainer_TC",
            OtherKey = "Employee_TC")]
        public Employee Trainer {
            get { return _Trainer.Entity; }
            set { _Trainer.Entity = value; }
        } 
    }
}