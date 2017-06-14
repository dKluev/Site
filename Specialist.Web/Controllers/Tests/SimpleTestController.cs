using System.Web.Mvc;
using SimpleUtils.Collections.Extensions;
using Specialist.Web.ActionFilters;
using Specialist.Web.Core;
using Specialist.Web.Root.SimpleTests.Logic;
using Specialist.Web.Root.SimpleTests.ViewModels;
using Specialist.Web.Root.SimpleTests.Views;

namespace Specialist.Web.Controllers.Tests {
	public class SimpleTestController:ViewController {

		[HandleNotFound]
		public ActionResult Details(int id) {
			var test = new SimpleTestParser().All().GetValueOrDefault(id);
			if (test == null) return null;
			return BaseViewWithModel(new SimpleTestView(), test);
		}

		[HandleNotFound]
		public ActionResult Result(int id, int resultId) {
			var test = new SimpleTestParser().All().GetValueOrDefault(id);
			if (test == null) return null;
			var model = new SimpleTestResultVM {
				Test = test,
				Result = test.Results[resultId - 1]
			};
			return BaseViewWithModel(new SimpleTestResultView(), model);

		}

		 
	}
}