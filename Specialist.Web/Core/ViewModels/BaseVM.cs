using System.Collections.Generic;
using System.Linq;

namespace Specialist.Web.Pages {
	public class BaseVM {
		public List<PagePart> Parts { get; set; }
		public string Title { get; set; }
		public bool IsBootStrap { get; set; }

		public List<PagePart> RightSide { get; set; }

		public object MainModel {
			get { return Parts.Where(x => x.Model != null).Select(x => x.Model).FirstOrDefault(); }
		}

		public BaseVM() {
			RightSide = new List<PagePart>();
			Parts = new List<PagePart>();
		}
	}
}