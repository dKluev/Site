using System.Data.Linq.Mapping;

namespace Specialist.Entities.Catalog.Links.Interfaces {
    public interface IEmployeeLink {
       
        string Employee_TC { get; set; }
      
        string EmpGroup_TC { get; set; }

        bool SiteVisible { get; set; }

    	bool FinalSiteVisible { get; }

        string FullName { get; }
        bool IsTrainer { get; }
    }
}