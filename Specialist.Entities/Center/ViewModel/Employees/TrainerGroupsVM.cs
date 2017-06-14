using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Center.ViewModel
{
    public class TrainerGroupsVM: IViewModel
    {
        public List<Group> Groups { get; set; }

	    public User User { get; set; }

        public string Title
        {
            get { return "Группы преподавателя"; }
        }

	    public List<Group> StartedGroups { get; set; }
	    public List<Group> SeminarGroups { get; set; }
	    public List<Group> EndedGroups { get; set; }
    }
}