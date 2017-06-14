using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using System.Linq;
using System.Linq.Dynamic;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;
using Specialist.Web;

namespace Specialist.Entities.Center.ViewModel
{
    public class SearchUserVM : IViewModel
    {
        public string YourUserID { get; set; }
        public string YourName { get; set; }
        public string YourPatronymic { get; set; }
        public string YourSurname { get; set; }
        public string YourEmail { get; set; }
        public string Result { get; set; }

        public List<Passport.User> PassportUser { get; set; }

        public SearchUserVM()
        {
            PassportUser = new List<Passport.User>();
        }

        public string Title
        {
            get { return ""; }
        }

    }
}
