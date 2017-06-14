using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Center.ViewModel;
using Specialist.Entities.Context;

namespace Specialist.Services.Center.ViewModel
{
    public interface IEmployeeVMService {
        EmployeeVM GetEmployee(string employeeTC);
        TrainerGroupsVM GetGroups();
        TrainerCoursesVM GetCourses();
	    IQueryable<Group> GetTrainerGroups(string employeeTC);
	    List<string> GetEmployeeCourses(string employeeTC);
    }
}