using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Education.ViewModel;
using Specialist.Entities.Filter;
using Specialist.Entities.Utils;
using Specialist.Entities.ViewModel;

namespace Specialist.Services.Interface
{
    public interface IGroupVMService
    {
        GroupListVM GetAllGroups(GroupFilter filter, int pageIndex);
        GroupVM GetGroup(decimal groupID);
//        GroupForumVM GetForum(decimal groupID, int index);
//        int NewForumMessage(GroupForumVM model);
        UserMessage GetOrCreateGroupRootMessage(decimal groupID);
    	List<Grouping<Section, Group>> GetSectionGroups(IEnumerable<Group> groups);
    	List<Grouping<Section, Course>> GetSectionCourses(IEnumerable<Course> courses);
	    List<Group> DiscountGroups();
	    List<Group> GetAllForMain();
	    List<Grouping<Section, string>> GetSectionCourseTCs(IEnumerable<string> courseTCs);
	    Tuple<bool, Group> HideVimeoGroupVideo(decimal groupId);
    }
}