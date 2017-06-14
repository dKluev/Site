using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Center.ViewModels {
	public class VideoCategoryVM:IViewModel {
		public List<Video> Videos { get; set; }

		public VideoCategory Category { get; set; }

		public string Title { get { return Category.Name; } }
	}
}