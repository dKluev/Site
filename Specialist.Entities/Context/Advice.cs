using System.Data.Linq;
using System.Data.Linq.Mapping;
using SimpleUtils.Utils;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Context {
    public partial class Advice {

		public string GetShortDescription() {
			return ShortDescription ??
				StringUtils.GetShortText(Description);
		}

        private EntityRef<Employee> _Employee = default(EntityRef<Employee>);
        [Association(Storage = "_Employee", ThisKey = "Employee_TC",
            OtherKey = "Employee_TC")]
        public Employee Employee {
            get { return _Employee.Entity; }
            set { _Employee.Entity = value; }
        } 
    }
}