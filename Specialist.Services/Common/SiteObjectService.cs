using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface;
using Specialist.Services.Interface.Catalog;

namespace Specialist.Services.Catalog
{
    public class SiteObjectService: ISiteObjectService
    {
        SpecialistDataContext context = new SpecialistDataContext();

        public SpecialistDataContext Context
        {
            get { return context; }
        }

        public SiteObjectService(IContextProvider contextProvider)
        {
            this.context = (SpecialistDataContext) 
                contextProvider.Get(typeof(SiteObject));
            if(context != null)
                context.Log = new StringWriter();
        }
      
        [Dependency]
        public IUserSettingsService UserSettingsService { get; set; }

        public IQueryable<T> GetDoubleRelation<T>(Type objectType, object id) where T : class
        {
            var type = LinqToSqlUtils.GetTableName(objectType);
            var requiredObjectType = LinqToSqlUtils.GetTableName(typeof(T));
            var requiredRelations = GetRequiredRelationsByDoubleLink(
                id, type, requiredObjectType);
            var requiredIds = requiredRelations.Select(sor => sor.Object.ID);
            return  context.GetTable<T>().Where(LinqToSqlUtils.WhereContains<T>(requiredIds));
        }

        public IQueryable<T> GetDoubleRelation<T>(object obj) where T : class
        {
            return GetDoubleRelation<T>(obj.GetType(), LinqToSqlUtils.GetPK(obj));
        }


        public IQueryable<T> GetWithoutRelation<T>() 
            where T : class
        {
            var requiredObjectType = LinqToSqlUtils.GetTableName(typeof(T));
            var objectIdsWithRelations =
                from sor in context.GetTable<SiteObjectRelation>()
                where sor.ObjectType == requiredObjectType
                select sor.Object_ID;
            return context.GetTable<T>()
                .Where(LinqToSqlUtils.WhereContains<T>(objectIdsWithRelations, true ));
        }

        public IQueryable<T> GetRelation<T>(Type objectType, object id) where T : class
        {
            var type = LinqToSqlUtils.GetTableName(objectType);
            var requiredObjectType = LinqToSqlUtils.GetTableName(typeof(T));

            var cityTC = UserSettingsService.CityTC;




            IQueryable<T> objList = null;
            var doubleLinkRelations =
                new List<Type> { typeof(Banner), typeof(Response), typeof(News) };
            var singleLinkRelations =
               new List<Type> {typeof(City) };
            var byRelationObject = 
                new List<Type> {typeof(Course), typeof(Product), typeof(Certification)};
            if (doubleLinkRelations.Contains(typeof(T)))
            {
                IQueryable<SiteObjectRelation> requiredRelations;
                if (cityTC == null)
                    requiredRelations = GetRequiredRelationsByDoubleLink(
                       id, type, requiredObjectType);
                else
                    requiredRelations = GetRequiredRelationsByRelations2WithCity(
                        id, type, requiredObjectType, cityTC);
                /*     if (cityTC != null )
                        requiredRelations = FilterByCity<T>(requiredRelations, 
                            context.GetTable<SiteObjectRelation>(), cityTC);*/
                var requiredIds = requiredRelations.Select(sor => sor.Object.ID);
                objList = context.GetTable<T>().Where(
                    LinqToSqlUtils.WhereContains<T>(requiredIds));
            }
            else if (singleLinkRelations.Contains(typeof(T)))
            {
                objList = GetSingleRelation<T>(objectType, id);
            }
            else if (byRelationObject.Contains(typeof(T)))
            {
                objList = GetByRelationObject<T>(objectType, id);
            }
            /*           if (typeof(T) == )
                           objList = (IQueryable<T>)(
                               from o in context.GetTable<Response>()
                               where context.GetTable<SiteObjectRelation>().Where(
                                   sor => sor.Object_ID == id && sor.ObjectType == type)
                                   .SelectMany(sor => sor.RelationObject.RelationObjectRelations)
                                   .Where(sor => sor.Object.Type == relationObjectType)
                                   .Select(sor => sor.Object.ID).Contains(o.ResponseId)
                               select o);

                      if (typeof(T) == typeof(Certification))
                           objList = (IQueryable<T>)(
                               from o in context.GetTable<Certification>()
                               where context.GetTable<SiteObjectRelation>().Where(
                                   sor => sor.Object_ID == id && sor.ObjectType == type 
                                        && sor.RelationObjectType == requiredObjectType)
                                   .Select(sor => sor.RelationObject_ID).Contains(o.Certification_ID)
                               select o);
             * */

            return objList;
        }

        public IQueryable<T> GetByRelationObject<T>(object obj) 
            where T : class
        {
            return GetByRelationObject<T>(obj.GetType(), LinqToSqlUtils.GetPK(obj));
        }

        public IQueryable<T> GetByRelationObject<T>(Type objectType, object id) 
            where T : class
        {
            var requiredObjectType = LinqToSqlUtils.GetTableName(typeof(T));
            var type = LinqToSqlUtils.GetTableName(objectType);

            var requiredIds = GetRequiredObjectIdsByRelationObject(id, type,
                requiredObjectType);
            var objList =
                context.GetTable<T>().Where(LinqToSqlUtils.WhereContains<T>(requiredIds));
            return objList;
        }

        public IQueryable<T> GetSingleRelation<T>(object obj) 
            where T : class
        {
            return GetSingleRelation<T>(obj.GetType(), LinqToSqlUtils.GetPK(obj));
        }

        private IQueryable<T> GetSingleRelation<T>(Type entityType, object id)
           where T : class
        {
            var type = LinqToSqlUtils.GetTableName(entityType);

            return GetSingleRelation<T>(type, id);
        }

        public IQueryable<T> GetSingleRelation<T>(string objectType, object id) 
            where T : class
        {
            var requiredObjectType = LinqToSqlUtils.GetTableName(typeof(T));
            var requiredIds = GetRequiredObjectIdsByObject(id, objectType,
                requiredObjectType);
            var objList =
                context.GetTable<T>().Where(LinqToSqlUtils.WhereContains<T>(requiredIds));

            return objList;
        }


        public IQueryable<T> GetRelation<T>(object obj) where T : class
       {
           var id = LinqToSqlUtils.GetPK(obj);
           return GetRelation<T>(obj.GetType(), id);
       }

        private IQueryable<SiteObjectRelation> GetRequiredRelationsByDoubleLink(
            object id, string type, string requiredObjectType)
        {
            var relations = context.GetTable<SiteObjectRelation>().Where(
                sor => sor.Object_ID == id && sor.ObjectType == type)
                .SelectMany(sor => sor.RelationObject.RelationObjectRelations)
                .Where(sor => sor.Object.Type == requiredObjectType);
            return relations;
        }

        private IQueryable<SiteObjectRelation> GetRequiredRelationsByRelations2WithCity(
            object id, string type, string requiredObjectType, string cityTC)
        {
            var cityObjectType = LinqToSqlUtils.GetTableName(typeof(City));
            var relations =
                GetRequiredRelationsByDoubleLink(id, type, requiredObjectType);
          /*      context.GetTable<SiteObjectRelation>().Where(
                sor => sor.Object_ID == id && sor.ObjectType == type)
                .SelectMany(sor => sor.RelationObject.RelationObjectRelations)
                .Where(sor => sor.Object.Type == requiredObjectType);*/
            var result =
                    from o in relations
                    let cityTCList =
                       from sor in context.GetTable<SiteObjectRelation>()
                       where sor.ObjectType == requiredObjectType
                           && o.Object_ID == sor.Object_ID
                           && sor.RelationObjectType == cityObjectType
                       select sor.RelationObject_ID
                    where cityTCList.Count() == 0 || cityTCList.Contains(cityTC)
                    select o;
            return result;
        }


        private IQueryable<object> GetRequiredObjectIdsByObject(object id, string type,
            string requiredObjectType)
        {
            return context.GetTable<SiteObjectRelation>().Where(
                      sor => sor.Object_ID == id && sor.ObjectType == type
                           && sor.RelationObjectType == requiredObjectType)
                      .Select(sor => sor.RelationObject_ID);
        }

        private IQueryable<object> GetRequiredObjectIdsByRelationObject(
            object id, string type, string requiredObjectType)
        {
            return context.GetTable<SiteObjectRelation>().Where(
                      sor => sor.RelationObject_ID == id && sor.RelationObjectType == type
                           && sor.ObjectType == requiredObjectType)
                      .Select(sor => sor.Object_ID);
        }


         public SiteObject GetBy(string type, object id)
         {
             var siteObject = context.GetTable<SiteObject>()
                 .FirstOrDefault(so => so.ID.Equals(id) &&
                 so.Type == type);
      

             return siteObject;
         }

    }
}