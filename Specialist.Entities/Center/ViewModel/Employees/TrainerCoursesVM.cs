using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel
{
    public class TrainerCoursesVM: IViewModel
    {
        public List<Tuple<CourseLink,bool>> CourseHasVideos { get; set; }

        public string Title
        {
            get { return "Курсы преподавателя"; }
        }
    }
}