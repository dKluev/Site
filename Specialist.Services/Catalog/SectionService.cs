using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using SimpleUtils.Utils;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Services.Catalog;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Common.Extension;
using Specialist.Services.UnityInterception;
using SimpleUtils.Common.Extensions;
using Specialist.Web.Common.Utils;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Services
{
    public class SectionService: Repository<Section>, ISectionService
    {
        public SectionService() : base(new ContextProvider()) {}

		[Dependency]
	    public SiteObjectRelationService SiteObjectRelationService { get; set; }

        

		public List<Section> AllActiveSections() {
			var sections = GetSectionsTree();
			var children = sections.SelectMany(s => s.SubSections);
			return sections.Concat(children).Distinct(x => x.Section_ID).ToList();
		}


		public List<Section> GetSectionsTree() {
			return MethodBase.GetCurrentMethod().CacheDay(() => {
				var mainSections = this.GetAll(x => x.IsMain && x.IsActive)
					.OrderBy(x => x.WebSortOrder).ToList();
				foreach (var mainSection in mainSections) {
					mainSection.SubSections = SiteObjectRelationService
						.GetMenuTree(mainSection).Select(e => e.Entity)
						.OfType<Section>().ToList();
				}
				return mainSections;
			});
		}
		public List<Section> GetTreeWithSubsections() {
			return GetSectionsTree().Where(x => x.SubSections.Any()).ToList();
		}

		public List<Section> GetChildren(int sectionId) {
			return GetSectionsTree().FirstOrDefault(x => x.Section_ID == sectionId)
				.GetOrDefault(x => x.SubSections) ?? new List<Section>();
		}

		public Dictionary<int, List<string>> NotAnnounce() {
			return MethodBase.GetCurrentMethod().Cache(() => {
				return GetAll().Where(x => x.IsActive && x.NotAnnounce != null)
					.Select(x => new {x.Section_ID, x.NotAnnounce})
						.ToList().ToDictionary(x => x.Section_ID,
							x => StringUtils.SafeSplit(x.NotAnnounce));
			});
		} 
    
    }
}