using System.Collections.Generic;
using System.Linq;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Entities.Utils;
using Tab = SimpleUtils.Common.Tuple<string, string, bool>;

namespace Specialist.Entities.Catalog.ViewModel {
	public class MainCoursesVM {
		public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
			RootSections { get; set; }
		public List<EntityWithList<Section, IEntityCommonInfo>>
			Professions { get; set; }
		public List<Vendor> Vendors { get; set; }

		public List<Product> Products { get; set; }

		public List<SiteTerm> SiteTerms { get; set; }



		public List<IGrouping<Section, IEntityCommonInfo>>[] GetSections() {
			var sections = RootSections.Select(x =>
				Grouping.New(x.Entity.As<Section>(),
					x.List)).ToList();
			var result = Rotate(sections);
			return result;
		}
		public List<IGrouping<Section, IEntityCommonInfo>>[] GetProfessions() {
			var sections = Professions.Select(x =>
				Grouping.New(x.Entity,
					x.List)).ToList();
			var result = Rotate(sections);
			return result;
		}

		private static List<IGrouping<Section, IEntityCommonInfo>>[] Rotate(List<Grouping<Section, IEntityCommonInfo>> sections) {
			var result = new[] {
				new List<IGrouping<Section, IEntityCommonInfo>>(),
				new List<IGrouping<Section, IEntityCommonInfo>>(),
				new List<IGrouping<Section, IEntityCommonInfo>>(),
			};

			for (int i = 0; i < sections.Count; i++) {
				result[i%3].Add(sections[i]);
			}
			return result;
		}

		
	}
}