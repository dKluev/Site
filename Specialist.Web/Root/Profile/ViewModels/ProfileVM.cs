using System;
using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Context.ViewModel;
using Specialist.Entities.Passport;
using System.Linq;
using SimpleUtils.Extension;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Utils;
using Specialist.Web.Common.Cdn;
using Specialist.Web.Root.Profile.Logic;

namespace Specialist.Entities.Profile
{
    public class ProfileVM: IViewModel
    {
		public bool IsNewProfile { get; set; }
	    public ProfileVM(bool isNewProfile) {
		    IsNewProfile = isNewProfile;
	    }

	    public User User { get; set; }

		public List<Video> Videos { get; set; } 

        public Employee Manager { get; set; }

	    public string EmployeeCompanyName { get; set; }

        public bool HasHosting
        {
            get
            {
                return User.Student.GetOrDefault(s => s.HostingLogin != null);
            }
        }

    	public StudentClabCard Card {
    		get { return User.Student.GetOrDefault(x => x.Card); }
    	}


        public string Title{
            get {
	            if (IsNewProfile) {
		            return null;
	            }
	            var title = User.IsCompany
		            ? "Организация: " + User.Company.CompanyName
		            : "Личный кабинет. "  + User.FirstName + " " + User.LastName ;
				return title;
            }
        }

    	public bool IsBest { get; set; }
    	public bool IsExcelMaster { get; set; }
	    public List<List<ProfileMenu>> Menu { get; set; }
	    public string CartStatus { get; set; }
	    public bool HasUnlimit { get; set; }
        public bool ExistGroup { get; set; }
	}
}