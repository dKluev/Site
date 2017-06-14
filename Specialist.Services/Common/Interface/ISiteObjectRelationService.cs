using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.UnityInterception;

namespace Specialist.Services.Catalog
{
    public interface ISiteObjectRelationService:IRepository<SiteObjectRelation> {
     //   int GetWeight(object entity);
     /*   TagWithEntity<T> GetTag<T>(T entity);

        [Cached]
        List<TagWithEntity<SiteObject>> GetAllTags();*/

    	IQueryable<SiteObjectRelation> GetByRelation(string relationObjectType,
    		object relationObjectId, Type objectType);

    	IQueryable<SiteObjectRelation> GetByRelation(Type relationType,
    		IEnumerable<object> relationObjectIds, Type objectType);

    	IQueryable<SiteObjectRelation> GetByRelation(Type relationType,
    		object relationObjectId, Type objectType);

    	IQueryable<SiteObjectRelation> GetRelation(Type type,
    		IEnumerable<object> objectIds, Type relationType = null);


	    Dictionary<Tuple<Type,int>,List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>> GetAllMenuTree();
    }
}