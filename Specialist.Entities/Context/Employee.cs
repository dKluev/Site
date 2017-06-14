using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links.Interfaces;
using Specialist.Entities.Const;
using Specialist.Entities.Passport;
using System.Linq;
using SimpleUtils.Extension;
using Specialist.Entities.Profile.Const;
using Specialist.Entities.Utils;

namespace Specialist.Entities.Context
{
    public partial class Employee: IEmployeeLink, IEntityCommonInfo
    {
		public static string GetFullName(string lastName, string firstName, string middleName) {
			return lastName + " " + firstName + " " + middleName;
		}

	    public string FullName
        {
            get
            {
                return GetFullName(LastName, FirstName, MiddleName);
            }
        }

	    public string LastFirstName
        {
            get { return LastName + " " + FirstName; }
        }

	    public bool IsDismiss { get { return EmpGroup_TC == EmpGroups.Dismiss; } }

    	public bool FinalSiteVisible {
		get { return SiteVisible || Employees.TrainersAlwaysVisible.Contains(Employee_TC); }
    	}

		 public List<EmployeeContact> PublicContacts {
            get {
            	var contacts = _.List(ContactTypes.Specialist.Icq,
            		ContactTypes.Specialist.Skype, ContactTypes.Specialist.Site, ContactTypes.Specialist.Phone);
				if(IsTrainer)
					contacts = _.List(ContactTypes.Specialist.Site);
            	return EmployeeContacts
                    .Where(ec => contacts
					.Contains(ec.ContactType_ID))
                    .ToList();
            }
		 }

		public bool WorkIn(string departmentTC) {
			return Department.Department_TC == departmentTC;
		}

        public List<string> EmailList {
            get { return StringUtils.SafeSplit(EMail, ';'); }
        }

        private const int ShortDescriptionLength = 100;

        public string ShortResume
        {
            get
            {
                if(SiteDescription == null 
                    || this.SiteDescription.Length <= ShortDescriptionLength)
                    return SiteDescription;
                return SiteDescription.Substring(0, 100) + "...";
            }
        }

        public static bool CheckIsTrainer(IEmployeeLink employee) {
            return employee.EmpGroup_TC == EmpGroups.Trainer
                || Employees.SpecialTrainers.Contains(employee.Employee_TC);
        }

        public bool IsTrainer
        {
            get
            {
                return CheckIsTrainer(this);
            }
        }

        public IEnumerable<Certification> Certifications {
            get {
                return EmployeeCertifications.Select(ec => ec.Certification);
            }
        }

        public Department Department
        {
            get
            {
				if(EmpGroup == null)
					return new Department{Department_TC = string.Empty};
                return EmpGroup.DepartmentEmpGroups.FirstOrDefault()
                    .GetOrDefault(x => x.Department);
            }
        }

        private EntityRef<User> _User = default(EntityRef<User>);
        [Association(Storage = "_User", ThisKey = "Employee_TC",
            OtherKey = "Employee_TC")]
        public User User
        {
            get { return _User.Entity; }
            set { _User.Entity = value; }
        }

        public string Name
        {
            get { return FullName; }
        }

        public string Description
        {
            get { return SiteDescription; }
        }

        public string UrlName
        {
            get { return Employee_TC; }
        }

        public string FirstEmail { get {
            return EmailList.FirstOrDefault();
        } }

        public string FirstSpecEmail { get {
            return EmailList.FirstOrDefault(StringUtils.IsSpecEmail);
        } }

        public int WebSortOrder { get { return 0; } }

        private EntitySet<Response> _Responses = default(EntitySet<Response>);
        [Association(Storage = "_Responses", ThisKey = "Employee_TC",
            OtherKey = "Employee_TC", IsForeignKey = true)]
        public EntitySet<Response> Responses
        {
            get { return _Responses; }
        }

    }
}