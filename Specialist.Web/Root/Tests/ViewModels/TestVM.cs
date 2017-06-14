using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class TestVM {
		public Test Test { get; set; }

		public bool IsActiveCalc { get; set; }



		public List<Section> Sections { get; set; }

		public List<Test> NextTests { get; set; }

		public List<Test> PrevTests { get; set; }
		public List<Course> Courses { get; set; }
		public List<Tuple<string, int>> TestStats { get; set; } 
	}
}