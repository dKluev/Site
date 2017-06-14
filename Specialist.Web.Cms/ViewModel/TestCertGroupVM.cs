using System.Collections.Generic;
using Specialist.Entities.Tests;

namespace Specialist.Web.Cms.ViewModel {
	public class TestCertGroupVM {
		public decimal GroupId { get; set; }

		public int TestId { get; set; }

		public List<Test> Tests { get; set; }

		public List<UserTest> UserTests { get; set; } 
	}
}