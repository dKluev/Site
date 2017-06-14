using System.Collections.Generic;
using System.Linq;
using Specialist.Entities;
using Specialist.Services.Core.Interface;
using Employee=Specialist.Entities.Context.Employee;

namespace Specialist.Services.Interface
{
    public interface IEmployeeService: IRepository<Employee>
    {
        List<Employee> GetAllTrainers();
	    Dictionary<string, Employee> AllEmployees();
    }
}