using System.Collections.Generic;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Catalog.Links.Interfaces;

namespace Specialist.Entities.ViewModel {
	public class GroupsWithDiscountVM:NearestGroupsVM {
		public GroupsWithDiscountVM(List<Group> groups, bool hideCourse) : base(groups, hideCourse) {}
		public Course Course { get; set; }

	}
}