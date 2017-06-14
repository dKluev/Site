using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.Unity;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Core;
using Specialist.Entities.Utils;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.UnityInterception;
using Specialist.Web.Common.Utils;
using SimpleUtils.Collections.Extensions;
using Specialist.Services.Common.Extension;

namespace Specialist.Services.Catalog
{
    using SiteObjectWeight = TagWithEntity<SiteObject>;
    public class SiteObjectRelationService :Repository<SiteObjectRelation>, ISiteObjectRelationService
    {
    	public SiteObjectRelationService(IContextProvider contextProvider) : base(contextProvider) {}

		public static readonly List<Type> types = _.List(
					typeof (Section),
					typeof (Vendor),
					typeof (Product),
					typeof (Profession),
					typeof (SiteTerm));

    	private static readonly List<Type> relationTypes = types.Concat(_.List(typeof (Course))).ToList();


		public Dictionary<Tuple<Type,int>, List<SiteObjectRelation>> GetAllForMenu() {
//			return CacheUtils.Get(MethodBase.GetCurrentMethod(), () => {
				var tableNames = types.Select(x => SiteObject.TypeTableNames.GetValueOrDefault(x)) ;
				var relationNames = relationTypes
					.Select(x => SiteObject.TypeTableNames.GetValueOrDefault(x)) ;

				return this.GetAll(x => tableNames.Contains(x.ObjectType)
					&& relationNames.Contains(x.RelationObjectType)
					&& x.Object.IsActive && x.RelationObject.IsActive).ToList()
					.GroupBy(x => new {x.Object_ID, x.ObjectType}).ToDictionary(
					x => Tuple.Create(SiteObject.TableNameTypes[x.Key.ObjectType],(int)x.Key.Object_ID), x => x.ToList());
//			});
		} 

//		public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>> GetMenuTree(object entity) {
//			var context = new SpecialistDataContext();
//			var allForMenu = GetAllForMenu();
//			var relations = allForMenu.GetValueOrDefault(Tuple.Create(entity.GetType(),
//				(int)LinqToSqlUtils.GetPK(entity))) ?? new List<SiteObjectRelation>();
//			if(!relations.Any())
//				return new List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>();
//			var tableNames = types.Select(x => SiteObject.TypeTableNames.GetValueOrDefault(x)) ;
//			var tree = relations.Where(x => tableNames.Contains(x.RelationObjectType)).Select(x => EntityWithList.New(x,
//				allForMenu.GetValueOrDefault(Tuple.Create(SiteObject.TableNameTypes[x.RelationObjectType], (int)x.RelationObject_ID))));
//			var allrelations = tree.SelectMany(x => x.List.Concat(_.List(x.Entity)))
//				.ToList();
//			var entityByType = new Dictionary<Type, Dictionary<object, IEntityCommonInfo>>();
//			entityByType.Add(typeof (Section), 
//				GetEntities<Section>(context, allrelations, x => x.Section_ID));
//			entityByType.Add(typeof (Vendor), 
//				GetEntities<Vendor>(context, allrelations, x => x.Vendor_ID));
//			entityByType.Add(typeof (Product), 
//				GetEntities<Product>(context, allrelations, x => x.Product_ID));
//			entityByType.Add(typeof (SiteTerm), 
//				GetEntities<SiteTerm>(context, allrelations, x => x.SiteTerm_ID));
//			entityByType.Add(typeof (Profession), 
//				GetEntities<Profession>(context, allrelations, x => x.Profession_ID));
//			entityByType.Add(typeof (Course), 
//				GetEntities<Course>(context, allrelations, x => x.Course_TC));
//
//
//			Func<SiteObjectRelation, IEntityCommonInfo> getEntity = x => 
//				entityByType[SiteObject.TableNameTypes[x.RelationObjectType]]
//				[x.RelationObject_ID];
//			var entityWithTags = tree.Select(x => EntityWithList.New(getEntity(x.Entity),
//				x.List.Select(getEntity)/*.OrderBy(e => e.WebSortOrder)*/)).ToList();
//			return entityWithTags;
//		}



		Dictionary<object,IEntityCommonInfo> GetEntities<T>(SpecialistDataContext context, 
			List<SiteObjectRelation> allrelations,
			Func<T,object> idSelector) where T : class, IEntityCommonInfo {
			var ids = GetEntityIds(typeof (T), allrelations);
			if(!ids.Any())
				return new Dictionary<object, IEntityCommonInfo>();
			return Repository2<T>.GetByPKList(context.GetTable<T>(), ids)
				.ToDictionary(idSelector, x => (IEntityCommonInfo) x);

		} 

    	private static List<object> GetEntityIds(Type entityType, 
			List<SiteObjectRelation> allrelations) {
    		var sectionIds = allrelations.Where(x => x.RelationObjectType ==
    			SiteObject.TypeTableNames[entityType]).Select(x => x.RelationObject_ID)
    			.Distinct()
    			.ToList();
    		return sectionIds;
    	}

    	public IQueryable<SiteObjectRelation> GetByRelation(Type relationType,
			object relationObjectId, Type objectType) {
			var relationObjectType = LinqToSqlUtils.GetTableName(relationType);
			return GetByRelation(relationObjectType, relationObjectId, objectType);
		}

    	public IQueryable<SiteObjectRelation> GetByRelation(string relationObjectType, object relationObjectId, Type objectType) {
    		return GetAll().Where(sor => sor.RelationObject_ID
    			.Equals(relationObjectId)
    				&& sor.RelationObjectType == relationObjectType && sor.ObjectType ==
    					LinqToSqlUtils.GetTableName(objectType));
    	}

    	public IQueryable<SiteObjectRelation> GetByRelation(Type relationType,
			IEnumerable<object> relationObjectIds, Type objectType) {
			var relationObjectType = LinqToSqlUtils.GetTableName(relationType);
			return GetAll().Where(sor => relationObjectIds.Contains(sor.RelationObject_ID)
					&& sor.RelationObjectType == relationObjectType && sor.ObjectType ==
						LinqToSqlUtils.GetTableName(objectType));
		}

		public IQueryable<SiteObjectRelation> GetRelation(Type type,
			IEnumerable<object> objectIds, Type relationType = null) {
			var objectType = LinqToSqlUtils.GetTableName(type);
			IQueryable<SiteObjectRelation> query;
			if(objectIds.Any())
			query = GetAll().Where(sor => objectIds.Contains(sor.Object_ID)
				&& sor.ObjectType == objectType);
			else {
				query = GetAll().Where(sor => sor.ObjectType == objectType);
				
			}
			if(relationType != null) {
				var relationObjectType = LinqToSqlUtils.GetTableName(relationType);
				query = query.Where(sor => sor.RelationObjectType == relationObjectType);
			}
			return query;
		}

		public List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>> GetMenuTree(object entity) {
		    var allTree = GetAllMenuTree();
		    return allTree.GetValueOrDefault(Tuple.Create(entity.GetType(),
			    (int) LinqToSqlUtils.GetPK(entity))) ?? 
				new List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>();
	    }

		public Dictionary<Tuple<Type,int>,List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>> GetAllMenuTree() {
			return MethodBase.GetCurrentMethod().Cache(() => {
				#if(DEBUG)
					return new Dictionary<Tuple<Type, int>, List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>>();
				#endif
				var allForMenu = GetAllForMenu();
				var allrelations = allForMenu.SelectMany(x => x.Value).ToList();
				var entityByType = GetAllEntityByType(allrelations);
				var tableNames = types.Select(x => SiteObject.TypeTableNames.GetValueOrDefault(x));
				return allForMenu.ToDictionary(x => x.Key, pair => {
					var relations = pair.Value.OrderBy(x => x.RelationOrder).ToList();
					if (!relations.Any())
						return new List<EntityWithList<IEntityCommonInfo, IEntityCommonInfo>>();

					var tree = relations.Where(x => tableNames.Contains(x.RelationObjectType)).Select(x => EntityWithList.New(x,
						(allForMenu.GetValueOrDefault(
							Tuple.Create(SiteObject.TableNameTypes[x.RelationObjectType],
							(int) x.RelationObject_ID)) ?? 
							new List<SiteObjectRelation>()).OrderBy(z => z.RelationOrder)));

					Func<SiteObjectRelation, IEntityCommonInfo> getEntity = x =>
						entityByType[SiteObject.TableNameTypes[x.RelationObjectType]]
							.GetValueOrDefault(x.RelationObject_ID);
					var entityWithTags = tree.Select(x => EntityWithList.New(getEntity(x.Entity),
						x.List.Select(getEntity).Where(y => y != null))).Where(y => y.Entity != null).ToList();
					return entityWithTags;
				});
			});
		}

	    


	    private Dictionary<Type, Dictionary<object, IEntityCommonInfo>> GetAllEntityByType(List<SiteObjectRelation> allrelations) {
		    
			    var context = new SpecialistDataContext();
			    var entityByType = new Dictionary<Type, Dictionary<object, IEntityCommonInfo>>();
			    entityByType.Add(typeof (Section),
				    GetEntities<Section>(context, allrelations, x => x.Section_ID));
			    entityByType.Add(typeof (Vendor),
				    GetEntities<Vendor>(context, allrelations, x => x.Vendor_ID));
			    entityByType.Add(typeof (Product),
				    GetEntities<Product>(context, allrelations, x => x.Product_ID));
			    entityByType.Add(typeof (SiteTerm),
				    GetEntities<SiteTerm>(context, allrelations, x => x.SiteTerm_ID));
			    entityByType.Add(typeof (Profession),
				    GetEntities<Profession>(context, allrelations, x => x.Profession_ID));
			    entityByType.Add(typeof (Course),
				    GetEntities<Course>(context, allrelations, x => x.Course_TC));
			    return entityByType;
		    
	    }
    }
}