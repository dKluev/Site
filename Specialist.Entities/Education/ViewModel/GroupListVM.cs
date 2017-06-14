using System;
using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Filter;
using Specialist.Entities.Utils;

namespace Specialist.Entities.ViewModel
{
    public class GroupListVM: IViewModel
    {
        public PagedList<Group> Groups { get; set; }

        public GroupFilter Filter { get; set; }

        public Course Course { get; set; }

    	public List<Grouping<Section, Group>> SectionGroups { get; set; }


        public string Title {
            get { return "Расписание"; }
        }
    }
}