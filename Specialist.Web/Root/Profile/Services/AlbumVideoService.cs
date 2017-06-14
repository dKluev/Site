using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Utils;

namespace Specialist.Web.Root.Profile.Services {
	public class AlbumVideoService : Repository2<AlbumVideo> {
		public AlbumVideoService(IContextProvider contextProvider) : base(contextProvider) {}


		[Dependency]
		public VimeoService VimeoService { get; set; }


		public List<string> GetVideos(string albumId) {
			var id = long.Parse(albumId);
			var videos = this.GetAll(x => x.AlbumId == id).Select(x => x.Videos).FirstOrDefault();
			if (videos == null) {
				Logger.Error("GetVideos " + albumId);
				return VimeoService.GetVideos(albumId);
			} 
			return StringUtils.SafeSplit(videos);
		}

		public void AddVimeoAlbum(long albumId) {
			if (this.GetAll(x => x.AlbumId == albumId).Any()) {
				Logger.Error("AddVimeoAlbum " + albumId);
				return;
			}

			var videoIds = VimeoService.GetVideos(albumId.ToString());
			var album = new AlbumVideo {
				AlbumId = albumId,
				Videos = videoIds.JoinWith(",")
			};
			this.InsertAndSubmit(album);


		}
	}
}