using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.ViewModel
{
    public class CompanyFileVM: IViewModel
    {
	    public User User { get; set; }
        public CompanyFileVM()
        {
			CompanyFile = new CompanyFile();
        }
        public CompanyFile CompanyFile { get; set; }

        public UploadFile UploadFile { get; set; }

	    public List<Company> Companies { get; set; }

        [UIHint(Controls.Hidden)]
        public bool IsNew { get; set; }

        [UIHint(Controls.File)]
        [DisplayName("Τΰιλ")]
        public string File { get; set; }

        public string Title
        {
            get { return "Τΰιλ"; }
        }
    }
}