using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Util;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.Const;

namespace Specialist.Entities.Center.ViewModel
{
    public class EmployeeVM: IViewModel
    {
        public Employee Employee { get; set; }

        public List<Group> Groups { get; set; }

	    public List<OrgResponse> OrgResponses { get; set; }

       

        public string Title {
            get { return Employee.FullName; }
        }
    }
}