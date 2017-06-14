using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Profile.ViewModel
{
    public class EditSuccessStoryVM: IViewModel
    {
        public SuccessStory SuccessStory { get; set; }

        public List<UserImageVM> Images { get; set; }

        public List<Profession> Professions { get; set; }

        public string Title
        {
            get { return "Ваша история успеха"; }
        }
    }
}