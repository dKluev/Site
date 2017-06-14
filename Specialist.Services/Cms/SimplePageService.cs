using System;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Cms.Interface;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Cms {
	public class SimplePageService : Repository<SimplePage>, ISimplePageService {
		public SimplePageService(IContextProvider contextProvider) : base(contextProvider) {}

		public SimplePage GetByUrl(string url) {
		/*	var urlSegments = url.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
			SimplePage parentPage = null;
			for (int i = 0; i < urlSegments.Length - 1; i++) {
				var urlName = urlSegments[i];
				if (parentPage == null) {
					var fullUrlName = "/" + url;
					parentPage = GetAll().FirstOrDefault(p => p.UrlName == urlName || p.UrlName == fullUrlName);
				} else {
					parentPage = GetAll().FirstOrDefault(p => p.UrlName == urlName &&
						Equals(p.SimplePageRelations
							.FirstOrDefault(spr => spr.IsMainParent).ParentPage, parentPage));
				}
				if (parentPage == null)
					return null;
			}

			var lastSegment = urlSegments.Last();
			var fullSegment = "/" + lastSegment;
			if (parentPage == null)
				return GetAll().FirstOrDefault(p => p.UrlName == lastSegment
					|| p.UrlName == fullSegment);*/
			/*return GetAll().FirstOrDefault(p => p.UrlName == lastSegment &&
				Equals(p.SimplePageRelations
					.FirstOrDefault(spr => spr.IsMainParent).ParentPage, parentPage));*/
			var fullUrlName = "/" + url;
			return GetAll().FirstOrDefault(p => p.UrlName == fullUrlName);
		}

	
	}
}