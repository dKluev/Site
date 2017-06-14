using System.Linq;
using Specialist.Entities.Tests;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Tests {
	public class UserTestService: Repository2<UserTest> {
		public UserTestService(IContextProvider contextProvider) : base(contextProvider) {}

		public IQueryable<UserTest> GetUserTests(GroupTest test) {
			return this.GetAll(x => x.RunDate >= test.DateBegin
				&& x.RunDate <= test.DateEnd.AddDays(1) && x.TestId == test.TestId);
		}
	}
}