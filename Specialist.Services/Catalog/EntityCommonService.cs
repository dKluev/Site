using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Catalog.ViewModel;
using Specialist.Entities.Common.ViewModel;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Services.Catalog.Interface;
using Specialist.Services.Common.Extension;
using Specialist.Services.Interface.Catalog;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Services.Catalog
{
    public class EntityCommonService: IEntityCommonService
    {
        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        [Dependency]
        public SiteObjectRelationService SiteObjectRelationService { get; set; }


        public SimplePageVM Get(SimplePage page)
        {
            return new SimplePageVM(page)
            {
                EntityWithTags = page.Children.OrderBy(x => x.WebSortOrder).Select(sp =>
                    EntityWithList.New((IEntityCommonInfo)sp,
                        sp.Children.OrderBy(x => x.WebSortOrder).Cast<IEntityCommonInfo>())).ToList(),
            };
        }
        public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
           GetEntityWithTags(object entity) {
        	return SiteObjectRelationService.GetMenuTree(entity).ToList();
        }
    	public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
           GetEntityWithTags2(object entity)
        {
            var entityWithTags
                = new List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>();
			var activeSections = SiteObjectService
                .GetSingleRelation<Section>(entity)
				.IsActive().ToList();
            var activeVendors = SiteObjectService
                .GetSingleRelation<Vendor>(entity)
				.IsActive().ToList();
            var activeProducts = SiteObjectService
                .GetSingleRelation<Product>(entity)
				.IsActive().ToList();
            var activeProfessions = SiteObjectService
                .GetSingleRelation<Profession>(entity)
				.IsActive().ToList();
            var activeSiteTerm = SiteObjectService
                .GetSingleRelation<SiteTerm>(entity)
				.IsActive().ToList();
			 entityWithTags.AddRange(
                GetSubSections(activeSections));
            entityWithTags.AddRange(
                GetSubSections(activeSiteTerm));
            entityWithTags.AddRange(
                GetSubSections(activeVendors));
            entityWithTags.AddRange(
                GetSubSections(activeProfessions));
            entityWithTags.AddRange(
                GetSubSections(activeProducts));
            return entityWithTags;
        }

        public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>
           GetSubSections(IEnumerable<IEntityCommonInfo> entities)
        {

            var subSections = entities.Select(ss =>
                EntityWithList.New(ss,
                        SiteObjectService.GetSingleRelation<Course>(ss)
                        	.IsActive().Cast<IEntityCommonInfo>().ToList()
                        .Concat(SiteObjectService.GetSingleRelation<Product>(ss)
                            .IsActive()
                            .ByWebOrder()
                            .ToList()
                            )
                        .Concat(SiteObjectService.GetSingleRelation<Vendor>(ss)
                            .IsActive()
                            .ByWebOrder()
                            .ToList()
                            )
                        .Concat(SiteObjectService.GetSingleRelation<Profession>(ss)
                            .IsActive()
                            .ByWebOrder()
                            .ToList()
                            )
                        .Concat(SiteObjectService.GetSingleRelation<SiteTerm>(ss)
                            .IsActive()
                            .ByWebOrder()
                            .ToList()
                            )
                    )
                ).ToList();
            return subSections;
        }

       

    }
}