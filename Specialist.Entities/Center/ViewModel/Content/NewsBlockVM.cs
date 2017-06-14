using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel {
	public class NewsBlockVM {
		public object Entity { get; set; }

		public List<News> News { get; set; }
	}
}