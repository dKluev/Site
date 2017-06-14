using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common;
using Specialist.Entities.Context;

namespace Specialist.Web.Root.Center.ViewModels {
	public class VideosVM {
		public List<Video> Videos { get; set; } 

		public List<Video> NewVideos { get; set; } 


		public List<VideoCategory> Categories { get; set; } 
	}
}