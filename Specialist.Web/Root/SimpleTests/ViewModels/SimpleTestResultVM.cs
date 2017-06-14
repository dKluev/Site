using Specialist.Entities.Catalog.Interface;
using Specialist.Web.Root.SimpleTests.Logic;

namespace Specialist.Web.Root.SimpleTests.ViewModels {
	public class SimpleTestResultVM:IViewModel {

		public SimpleTest.Result Result { get; set; }

		public SimpleTest Test { get; set; }

		public string Title { get { return "Результат тестирования"; } }
	}
}