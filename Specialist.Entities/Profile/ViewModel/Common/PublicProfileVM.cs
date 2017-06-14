using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using System.Linq;
using SimpleUtils.Extension;
using Specialist.Entities.Tests;

namespace Specialist.Entities.Profile
{
    public class PublicProfileVM:IViewModel
    {
        public User User { get; set; }

    	public bool IsOwner { get; set; }

        public List<UserContact> Socials { get; set; }

        public SuccessStory SuccessStory { get; set; }

        public List<Competition> Competitions { get; set; }

//        public List<string> BestGraduateTypes { get; set; }

    	public List<Test> Tests { get; set; } 

    	public string Title {
    		get { return "Профиль " + User.FullName; }
    	}

    	public bool IsBest { get; set; }

    	public bool IsExcelMaster { get; set; }
    }
}