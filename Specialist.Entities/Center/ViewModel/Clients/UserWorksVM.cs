using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.ViewModel;

namespace Specialist.Entities.Center.ViewModel {
    public class UserWorksVM : IViewModel {

        public Section Section { get; set; }

        public UserWorkSection WorkSection { get; set; }

        public PagedList<UserWork> UserWorks { get; set; }

        public string Title {
            get {
                return "Работы слушателей: " + (WorkSection != null
                    ? WorkSection.Name
                    : Section.Name);
            }
        }
    }
}