using System.Data.Linq;
using System.Data.Linq.Mapping;
using SimpleUtils.Common.Extensions;

namespace Specialist.Entities.Context
{
    public partial class Response
    {
    	public bool IsMan {
    		get {
    			if(Authors.IsEmpty())
    				return false;
				if(Authors.EndsWith("ич"))
					return true;
    			return false;
    		}
    	}

        private EntityRef<Course> _Course = default(EntityRef<Course>);
        [Association(Storage = "_Course", ThisKey = "Course_TC",
            OtherKey = "Course_TC", IsForeignKey = true)]
        public Course Course
        {
            get { return _Course.Entity; }
        }

        private EntityRef<Employee> _Employee = default(EntityRef<Employee>);
        [Association(Storage = "_Employee", ThisKey = "Employee_TC",
            OtherKey = "Employee_TC", IsForeignKey = true)] 
        public Employee Employee
        {
            get { return _Employee.Entity; }
        } 
    }
}