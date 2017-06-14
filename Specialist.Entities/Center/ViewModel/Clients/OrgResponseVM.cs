using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.ViewModel;

namespace Specialist.Entities.Center.ViewModel {
    public class OrgResponseVM: IViewModel {

        public OrgResponse Response { get; set; }

        public SimplePage CorporateClients { get; set; }

        public string Title {
            get { return "Отзыв компании: " + Response.Organization.Name; }
        }
    }
}