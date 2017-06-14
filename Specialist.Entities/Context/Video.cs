using SimpleUtils.Util;

namespace Specialist.Entities.Context {
	public partial class Video {
		public string UrlTitle {
			get { return Linguistics.UrlTranslite(Name); }
		} 
	}
}