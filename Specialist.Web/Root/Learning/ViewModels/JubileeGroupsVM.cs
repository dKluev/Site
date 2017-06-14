using System.Collections.Generic;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Learning.ViewModels {
	public class JubileeGroupsVM:NearestGroupsVM {
		public JubileeGroupsVM(List<Group> groups, bool hideCourse) : base(groups, hideCourse) {}
	}
}