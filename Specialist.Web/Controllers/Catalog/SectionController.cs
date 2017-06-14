using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Web.Mvc;
using Specialist.Services.Center;
using Specialist.Services.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Common.Html;
using Specialist.Web.Const;
using System.Linq;
using Specialist.Web.Helpers;
using Specialist.Web.Util;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Controllers {
	public class SectionController : ExtendedController {
		[Dependency]
		public ISectionVMService SectionVMService { get; set; }

		[Dependency]
		public IUserWorkService UserWorkService { get; set; }

		[Dependency]
		public ISectionService SectionService { get; set; }

		[Dependency]
		public IAnnounceService AnnounceService { get; set; }
		

		[HandleNotFound]
		public virtual ActionResult Details(string urlName) {
			var model =
				SectionVMService.GetBy(urlName);
			if(model != null) {
				model.Announces = AnnounceService.GetHotGroupsForSection(model.Section);
			}

			return MView(ViewNames.RootSection, model);
		}

		[OutputCache(Duration = 24*60*60, VaryByParam = "none")]
		public ActionResult MainPageSections() {
			var sections = SectionService.AllActiveSections().Where(x => x.ForMainPage)
				.OrderBy(x => x.WebSortOrder).ToList();
			return Content(Htmls2.MarkArrow(sections.Select(x =>
				Html.SectionLink(x))).ToString());
		}

		[AjaxOnly]
		public ActionResult UserWorks(int sectionId) {
			var userWorks = UserWorkService.GetAllRandomForSection(sectionId).Take(4);
			return Content(H.l(Html.Site().UserWorkFour(userWorks)).ToString());
		}

		
		[AjaxOnly]
		public ActionResult Responses(int sectionId) {
			var responses = SectionVMService.GetResponses(sectionId).Take(4);
			return Content(H.l(Html.Site().OpinionTwo(responses)).ToString());
		}

		public ActionResult SectionsResponses() {
			var sections = SectionService.GetSectionsTree();
			return View(sections);
		}

	}
}