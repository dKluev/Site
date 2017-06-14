using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.Links;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Entities.Passport;
using SimpleUtils.Extension;

namespace Specialist.Entities.Profile.ViewModel
{
    public class LearningVM : IViewModel
    {
	    public class GroupList {
            public LearningVM Model { get; set; }
		    public List<Group> Groups { get; set; } 

			public bool ShowInfo { get; set; }

			public string EmptyListText { get; set; }
	    }

	    public GroupList GetGroupList(List<Group> groups, string text, bool showInfo) {
		    return new GroupList {
			    Model = this,
			    Groups = groups,
				EmptyListText = text,
				ShowInfo = showInfo
		    };
	    }
        public Student Student { get; set; }

	    public User User { get; set; }

	    public Employee Manager { get; set; }

	    public List<CourseLink> NextCourses { get; set; } 

	    public List<Group> NextGroups { get; set; } 

        public virtual string Title
        {
            get { return "Мои курсы"; }
        }


	    public decimal? Debt(decimal groupId) {
		    var studentInGroup = Student.StudentInGroups.First(x => x.Group_ID == groupId);
		    if (studentInGroup.BerthType_TC == BerthTypes.Dvp) {
			    return null;
		    }
		    if (studentInGroup.Debt == 100) {
			    return null;
		    }
		    var debt = studentInGroup.Debt;
		    return (debt * studentInGroup.Charge)/100;
	    }

	    public bool IsWebinar(decimal groupId) {
		    var priceTypeTc = Student.StudentInGroups.First(x => x.Group_ID == groupId).PriceType_TC;
		    return priceTypeTc == PriceTypes.IntraExtraWebinar || PriceTypes.IsWebinar(priceTypeTc) ||
				priceTypeTc == PriceTypes.Unlimited;
	    }

	    public List<SigEvent> SigEvents { get; set; }
    }
}
