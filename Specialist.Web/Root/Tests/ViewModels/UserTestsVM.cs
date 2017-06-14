using System.Collections.Generic;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class UserTestsVM {
		public PagedList<UserTest> List { get; set; }

		public User User { get; set; }

		public Dictionary<int,int> TestTryCounts { get; set; }

		public UserTestsVM(PagedList<UserTest> list) {
			List = list;
		}
	}
}