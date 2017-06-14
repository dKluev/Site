using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;

namespace Specialist.Services.Interface.Catalog
{
    public interface ISiteObjectService
    {
        SiteObject GetBy(string type, object id);
        IQueryable<T> GetRelation<T>(object obj) where T : class;
        IQueryable<T> GetDoubleRelation<T>(object obj) where T : class;

        IQueryable<T> GetWithoutRelation<T>() 
            where T : class;

        IQueryable<T> GetByRelationObject<T>(object obj) 
            where T : class;

        IQueryable<T> GetByRelationObject<T>(Type objectType, object id) 
            where T : class;

        IQueryable<T> GetSingleRelation<T>(object obj) 
            where T : class;

        IQueryable<T> GetSingleRelation<T>(string objectType, object id) 
            where T : class;

    }
}