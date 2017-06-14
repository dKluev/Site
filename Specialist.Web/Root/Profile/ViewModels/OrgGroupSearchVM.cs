using System;
using SimpleUtils.Collections.Paging;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgGroupSearchVM:IViewModel {
		public decimal? StudentId { get; set; }

		public bool AutoSearch { get; set; }
		
		public string CourseTC { get; set; }

		public bool Current { get; set; }

		public DateTime? LeftDataBeg { get; set; }

		public int PageIndex { get; set; }

		public DateTime? RightDataBeg { get; set; }

		public PagedList<Group> Groups { get; set; }

		public string Title { get { return "Поиск сотрудников"; } }
	}
}