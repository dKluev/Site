using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Web.Common.Utils;
using Specialist.Services.Catalog.Extension;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Services
{
    public class EmployeeService: Repository<Employee>, IEmployeeService
    {
        public EmployeeService(IContextProvider contextProvider) : base(contextProvider) {}

        [Dependency]
        public IRepository<Group> GroupService { get; set; }

        public List<Employee> GetAllTrainers() {
		    return MethodBase.GetCurrentMethod().CacheDay(() => AllEmployees().Select(x => x.Value).Where(e => e.SiteVisible
		        || Employees.TrainersAlwaysVisible.Contains(e.Employee_TC))
		        .Where(e => e.EmpGroup_TC ==
			        EmpGroups.Trainer || Employees.SpecialTrainers.Contains(e.Employee_TC)
		        ).OrderBy(x => x.FullName).ToList());
        }

		public List<string> TrainersWithoutGroup() {
			return CacheUtils.Get(MethodBase.GetCurrentMethod(), () => {
				var visibleEmployees = GetAll().Where(x => x.SiteVisible).Select(x => x.Employee_TC).ToList();
				var trainerWithGroup = GroupService.GetAll().Where(x => x.DateBeg > DateTime.Now.AddDays(-30)
					&& (x.Color_TC == Colors.Pink || x.Color_TC == Colors.Yellow)).Select(x => x.Teacher_TC).Distinct()
					.ToList().AddFluent(Employees.TrainersAlwaysVisible);
				return visibleEmployees.Except(trainerWithGroup).ToList();
			}, 24);
		}

	    public Dictionary<string, Employee> AllEmployees() {
		    return MethodBase.GetCurrentMethod().CacheDay(() =>
			    GetAll().ToDictionary(x => x.Employee_TC, x => x));
	    } 

        public IQueryable<Employee> GetAllByCourseTC(string courseTC)
        {
            return  
                from employee in GetAll()
                where context.GetTable<EmployeesCourse>().Where(ec =>
                    ec.Course_TC == courseTC)
                    .Select(ec => ec.Employee_TC)
                    .Contains(employee.Employee_TC) &&
                    employee.SiteVisible
                select employee;
        }
    }
}