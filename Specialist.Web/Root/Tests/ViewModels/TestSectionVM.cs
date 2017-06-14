using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestSectionVM {
		public Section Section { get; set; }

		public List<Test> Tests { get; set; }

	}
}