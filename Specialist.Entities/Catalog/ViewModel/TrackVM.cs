using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Entities.ViewModel
{
    public class TrackVM: CourseBaseVM
    {
    	public List<Section> Sections { get; set; }
    }
}