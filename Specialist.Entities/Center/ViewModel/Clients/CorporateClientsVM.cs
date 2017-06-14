using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Context.Const;

namespace Specialist.Entities.Center.ViewModel {
    public class CorporateClientsVM : IViewModel {

        public SimplePage Page { get; set; }

        public PagedList<OrgResponse> Responses { get; set; }

        public PagedList<OrgProject> OrgProjects { get; set; }

        public CorporateClientsVM() {
            Responses = new PagedList<OrgResponse>();
            OrgProjects = new PagedList<OrgProject>();
        }


        public bool IsResponses { get {
            return UrlName == SimplePages.Urls.Responses;
        } }

        public bool IsProjects {
            get {
                return UrlName == SimplePages.Urls.Projects;
            }
        }

        public List<SimplePage> Pages { get { return SimplePages.GetCorporateClients(); } }

        public string UrlName { get; set; }


        public string Title {
            get { return "Корпоративные заказчики"; }
        }
    }
}