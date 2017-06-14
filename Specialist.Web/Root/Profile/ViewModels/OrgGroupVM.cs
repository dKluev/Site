using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using SimpleUtils.Common.Extensions;

namespace Specialist.Web.Root.Profile.ViewModels {
	public class OrgGroupVM : IViewModel {
		public Group Group {
			get;
			set;
		}

		public List<Student> Students {
			get;
			set;
		}
		public string Title {
			get { return Group.GroupNumberTitle; }
		}
	}
}