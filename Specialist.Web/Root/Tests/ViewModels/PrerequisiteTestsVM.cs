using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Tests;
using Specialist.Entities.Utils;

namespace Specialist.Web.Root.Tests.ViewModels {
	public class PrerequisiteTestsVM: IViewModel {
		public List<Grouping<Section, Course>> Courses { get; set; }

		public string Title {
			get { return "Бесплатные тесты предварительной подготовки"; }
		}
	}
}