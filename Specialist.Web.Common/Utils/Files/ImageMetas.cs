using System.Collections.Generic;
using System.Reflection;
using System.Web;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Web.Common.Utils.Files {
	public class ImageMetas {
		public Dictionary<string, string> Descs() {
			return MethodBase.GetCurrentMethod().CacheDay(() => new SpecialistWebDataContext().ImageMetas
				.ToDictionary(x => x.Name.ToLower(), x => x.Description));
		}  
	}
}