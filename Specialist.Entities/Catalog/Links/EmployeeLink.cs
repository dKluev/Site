using System;
using Specialist.Entities.Catalog.Links.Interfaces;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Context;

namespace Specialist.Entities.Catalog.Links {
    public class EmployeeLink: IEmployeeLink {
        public string Employee_TC { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string EmpGroup_TC { get; set; }
        public bool SiteVisible { get; set; }
        public bool FinalSiteVisible { get { return SiteVisible; } }

        public string FullName {
            get { return new[] {LastName, FirstName, MiddleName}.JoinWith(" "); }
        }

        public bool IsTrainer {
            get { return Employee.CheckIsTrainer(this); }
           
        }
    }
}