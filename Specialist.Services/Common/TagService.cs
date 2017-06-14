using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using SimpleUtils.Util;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Web.Cms.Repository;
using SimpleUtils;

namespace Specialist.Services.Catalog
{
//    public class TagService: ITagService
//    {
//        [Dependency]
//        public DynamicRepository DynamicRepository { get; set; }
//
//      
//
//        [Dependency]
//        public IContextProvider ContextProvider { get; set; }
//
//        [Dependency]
//        public ISiteObjectRelationService SiteObjectRelationService { get; set; }
//
//     
//
//       /* public IQueryable<Tag> GetAllFor(Block block)
//        {
//            return
//                from siteObjectTag in context.SiteObjectTags
//                where (int)siteObjectTag.SiteObject.ID == block.BlockID
//                    && siteObjectTag.SiteObject.Type == "tBlocks"
//                select siteObjectTag.Tag;
//        }*/
//        Random _random = new Random();
//
//    /*    public List<TagWithEntity> GetTagsByNamePart(string namePart)
//        {
//            var siteObjects = SiteObjectRelationService.GetAllTags()
//                .Where(t => t.Entity.Type != "tCourses")
//              .Where(t => t.Entity.Name.ToLower().Contains(namePart.ToLower()))
//              .ToList()
//              .Take(30)
//              .Select(t => t.Entity);
//
//            var groupedSiteObjects =
//                from so in siteObjects
//                group so by so.Type into grouped
//                select grouped;
//
//            var result = new List<TagWithEntity>();
//            foreach (var groupedSiteObject in groupedSiteObjects)
//            {
//                var entityType = ContextProvider.GetTypeByTableName(groupedSiteObject.Key);
//                var entities = DynamicRepository.GetByPK(entityType,
//                    groupedSiteObject.Select(so => so.ID).ToArray());
//                foreach (var entity in entities)
//                {
//                    result.Add(new TagWithEntity(entity,
//                    SiteObjectRelationService.GetWeight(entity)));
//                }
//            }
//
//            return result
//                    .Cast<TagWithEntity<object>>().ToList().Normalization().Cast<TagWithEntity>().ToList();
//        }*/
//
//       /* public IQueryable<Tag> GetAllTags(int count)
//        {
//            var result = new List<Tag>();
//            var siteObjects = context.SiteObjects
//                .Where(so => so.IsTag && so.Name.Length < 50).Take(count);
//            foreach (var siteObject in siteObjects)
//            {
//                var entityTypeForObject =
//                    ContextProvider.GetTypeByTableName(siteObject.Type);
//                var entity = DynamicRepository
//                   .GetByPK(entityTypeForObject, siteObject.ID );
//                result.Add(new Tag{Entity = entity, Weight = _random.Next(1,10)});
//            }
//           
//            return result.AsQueryable();
//        }*/
//    }
}