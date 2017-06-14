using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Specialist.Entities.Context;
using Specialist.Web.Common.Utils;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Root.Common.Services {
	public class PageMetaService {
		public Dictionary<string, PageMeta> GetAllPageMetas() {
			return CacheUtils.Get(MethodBase.GetCurrentMethod(), () => {
				using (var context = new SpecialistWebDataContext()) {
					return context.PageMetas.ToDictionary(x => x.Url.ToLower(),
						x => x);
				}
			});
		}

		public PageMeta GetFor(string url) {
			return GetAllPageMetas().GetValueOrDefault(url);
		}
	}
}