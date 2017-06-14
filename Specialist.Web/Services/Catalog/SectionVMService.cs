using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections.Extensions;
using Specialist.Entities.Announcement;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Entities.Utils;
using Specialist.Services.Catalog;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Center;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Web.Common.Utils;

namespace Specialist.Services {
	public class SectionVMService : ISectionVMService {
		[Dependency]
		public ISectionService SectionService { get; set; }

		[Dependency]
		public IEntityCommonService EntityCommonService { get; set; }


		[Dependency]
	    public SiteObjectRelationService SiteObjectRelationService { get; set; }

		[Dependency]
		public IUserWorkService UserWorkService { get; set; }

		[Dependency]
		public IResponseService ResponseService { get; set; }

		[Dependency]
		public ICourseService CourseService { get; set; }


		public SectionVM GetBy(string urlName, int? id) {
			var section = urlName.IsEmpty() 
				? SectionService.GetByPK(id)
				: SectionService.GetAll().ByUrlName(urlName);
			if(section == null)
				return null;
			var sectionIdsContainUserWorks = GetSectionIdsContainUserWorks();
			
			var entityWithTags = EntityCommonService.GetEntityWithTags(section);
			AddMicrosoft(section, entityWithTags);
			var parent = SectionService.GetSectionsTree()
				.FirstOrDefault(z => z.SubSections
					.Any(x => x.Section_ID == section.Section_ID));
			return
				new SectionVM {
					Section = section,
					Parent = parent,
					EntityWithTags = entityWithTags,
					SectionIdContainUserWorks = sectionIdsContainUserWorks,
				};
		}

		private List<int> GetSectionIdsContainUserWorks() {
			return MethodBase.GetCurrentMethod().CacheDay(() =>
				UserWorkService.GetAll(x => x.SmallImage != null)
					.Select(x => x.Section_ID).Distinct().ToList());
		}

		private static void AddMicrosoft(Section section, List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>> entityWithTags) {
			if (section.Section_ID == Sections.NetworkBasic) {
				var microsoft = new Vendor {
					Name = "по сетевым технологиям Microsoft",
					UrlName = "microsoft",
					WebSortOrder = 100000,
					IsActive = true
				};
				entityWithTags.Add(EntityWithList.New((IEntityCommonInfo) microsoft,
					new List<IEntityCommonInfo>()));
			}
		}



		public IQueryable<Response> GetResponses(int sectionId) {
			var courseTCList = CourseService.GetCourseTCListForSections(
				_.List(sectionId));
			return 
				ResponseService.GetAll()
				.IsActive()
				.Where(r => courseTCList.Contains(r.Course_TC));
		}

		public List<string> CoursesForInvoice() {
			return MethodBase.GetCurrentMethod().Cache(() => {
				var sectionIds = SectionService.GetTreeWithSubsections()
					.Where(x => Sections.ForInvoice.Contains(x.Section_ID))
					.SelectMany(x => x.SubSections).Select(x => x.Section_ID).ToList();
				return CourseService.GetCourseTCListForSections(sectionIds);
			});
		} 

		public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
				GetSectionWithEntityTree() {
			var mainSections = SectionService.GetAll(x => x.IsMain && x.IsActive)
				.OrderBy(x => x.WebSortOrder).ToList();
			return mainSections.Select(x => EntityWithList.New((IEntityCommonInfo)x,
				SiteObjectRelationService.GetMenuTree(x).Select(e => e.Entity)
				.Where(e => e is Section || e.As<IForMainPage>()
					.GetOrDefault(z => z.ForMainPage)))).ToList();
		}


	}
}