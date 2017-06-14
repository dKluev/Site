using System;
using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Core;

namespace Specialist.Entities.Center.ViewModel {
    public class PrivatePersonVM: IViewModel {

        public PagedList<SuccessStory> SuccessStories { get; set; }

        public PagedList<Response> Responses { get; set; }

        public List<EntityWithList<Section, EntityWithList<Section, UserWorkSection>>>
            UserWorks { get; set;}

        public PrivatePersonVM() {
            Responses = new PagedList<Response>();
            SuccessStories = new PagedList<SuccessStory>();
        }

        public bool IsSuccessStories { get {
            return UrlName == SimplePages.Urls.SuccessStories;
        } }

        public bool IsResponses { get {
            return UrlName == SimplePages.Urls.Responses;
        } }

        public bool IsUserWorks {
            get {
                return UrlName == SimplePages.Urls.Works;
            }
        }

        public List<SimplePage> Pages { get { return SimplePages.GetPrivatePerson(); } }

        public string UrlName { get; set; }


        public string Title {
            get { return Pages.First(x => x.UrlName == UrlName).Name; }
        }
    }
}