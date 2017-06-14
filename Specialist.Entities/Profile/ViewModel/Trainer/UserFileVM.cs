using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;

namespace Specialist.Entities.Profile.ViewModel
{
    public class UserFileVM: IViewModel
    {
        public UserFileVM()
        {
            UserFile = new UserFile();
			GroupIds = new List<decimal>();
			CourseTCs = new List<string>();
        }
        public UserFile UserFile { get; set; }

        public UploadFile UploadFile { get; set; }

        [UIHint(Controls.Hidden)]
        public bool IsNew { get; set; }

	    public List<CourseLink> Courses { get; set; }

	    public List<string> CourseTCs { get; set; }

	    public List<decimal> GroupIds { get; set; }

	    public List<Group> Groups { get; set; }

        [UIHint(Controls.File)]
        [DisplayName("Τΰιλ")]
        public string File { get; set; }

        public string Title
        {
            get { return "Τΰιλ"; }
        }
    }
}