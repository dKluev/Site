using System.Collections.Generic;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class GroupTestResultVM {
		public GroupInfo GroupInfo { get; set; }

		public List<GroupUserTests> GroupUserTestsList { get; set; } 
	}
}