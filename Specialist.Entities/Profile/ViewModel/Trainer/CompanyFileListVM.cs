using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.ViewModel
{
    public class CompanyFileListVM: IViewModel
    {
        public List<CompanyFile> Files { get; set; }
	    public User User { get; set; }

        public string Title
        {
            get { return "Файлы компаний"; }
        }
    }
}