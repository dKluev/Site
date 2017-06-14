using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;
using Specialist.Entities.Profile.ViewModel;

namespace Specialist.Entities.Center.ViewModel {
    public class SuccessStoryVM: IViewModel {

	    public CourseLink CourseLink { get; set; }

        public SuccessStory SuccessStory { get; set; }

        public List<UserImageVM> Images { get; set; }

        public string Title {
            get { return "История успеха"; }
        }
    }
}