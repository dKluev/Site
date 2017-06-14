using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Context;
using Specialist.Entities.Context.Const;
using Specialist.Services.Common.Extension;
using Specialist.Web.Common.Extension;
using Specialist.Web.Controllers;
using Specialist.Web.Controllers.Center;

namespace Specialist.Services.Common {
	public class SiteMapGenerator {
		public string RootUrl {
			get { return "http://" + HttpContext.Current.Request.Url.Authority; }
		}

		public XDocument Get(UrlHelper urlHelper) {
			var date = DateTime.Today.ToString("yyyy-MM-dd");
			var urls = GetUrls(urlHelper);


			XNamespace xmlNS = "http://www.sitemaps.org/schemas/sitemap/0.9";


			var siteMap =
				new XDocument(
					new XElement(xmlNS + "urlset",
						from url in urls
						select
							new XElement(xmlNS + "url",
								new XElement(xmlNS + "loc", url),
								new XElement(xmlNS + "lastmod", date))
						));
			siteMap.Declaration = new XDeclaration("1.0", "UTF-8", null);
			return siteMap;
		}

		private List<string> GetUrls(UrlHelper urlHelper) {
			var root = RootUrl;
			var context = new SpecialistDataContext();
			var webcontext = new SpecialistWebDataContext();
			var urls = new List<string>();

			urls.AddRange(
				context.Courses.IsActive().Where(x => x.UrlName != null).Where(x => x.IsTrack != true)
					.OrderByDescending(c => c.Course_ID).Select(x => x.UrlName).ToList()
					.Select(x => root + urlHelper.Action<CourseController>(c => c.Details(x)))
				);

			urls.AddRange(
				webcontext.News.IsActive().OrderByDescending(n => n.PublishDate).Take(50)
				.Select(x => new {x.NewsID, x.Title}).ToList()
					.Select(x => root + urlHelper
						.Action<SiteNewsController>(c => c.Details(x.NewsID
						, Linguistics.UrlTranslite(x.Title))))
				);
			urls.AddRange(
				webcontext.Sections.IsActive().Select(x => x.UrlName).ToList()
					.Select(x => root + urlHelper.Action<SectionController>(c => c.Details(x)))
				);
			if (!CommonConst.IsMobile) {
				urls.AddRange(
					context.Vendors.IsActive().Select(x => x.UrlName).ToList()
						.Select(x => root + SimplePages.FullUrls.Vendor + x)
					);


				urls.AddRange(
					context.Professions.IsActive().Select(x => x.UrlName).ToList()
						.Select(x => root + urlHelper.Action<ProfessionController>(c => c.Details(x)))
					);

				urls.AddRange(
					webcontext.Products.IsActive().Select(x => x.UrlName + UrlName.ProductPostfix).ToList()
						.Select(x => root + urlHelper.Action<ProductController>(c => c.Details(x)))
					);


				urls.AddRange(
					webcontext.SiteTerms.IsActive().Select(x => x.UrlName).ToList()
						.Select(x => root + urlHelper.Action<DictionaryController>(c => c.Definition(x)))
					);



				urls.AddRange(
					webcontext.SimplePages.ToList().Select(x => x.Url)
						.Select(x => root + x)
					);

				urls.AddRange(
					context.Certifications.IsActive().Select(x => x.UrlName).ToList()
						.Select(x => root + urlHelper.Action<CertificationController>(
							c => c.Details(x)))
					);
				urls.AddRange(
					context.Exams.Where(x => x.Available).Select(x => x.Exam_TC).ToList()
						.Select(x => root + urlHelper.Action<ExamController>(c => c.Details(x)))
					);
				
			}

			return urls;
		}
	}
}