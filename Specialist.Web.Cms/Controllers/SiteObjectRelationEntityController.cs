using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Services.Catalog;
using Specialist.Services.Core.Interface;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Const;
using Specialist.Web.Cms.Core;
using Specialist.Web.Cms.Core.ViewModel;
using Specialist.Web.Cms.Services;
using Specialist.Web.Cms.ViewModel;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Extension;

namespace Specialist.Web.Cms.Controllers {
	public class SiteObjectRelationEntityController : BaseController<SiteObjectRelation> {
		[Dependency]
		public IRepository<SiteObject> SORepository { get; set; }

		[Dependency]
		public IRepository<SiteObjectRelation> SiteObjectRelationRepository { get; set; }

		[Dependency]
		public IContextProvider ContextProvider { get; set; }

		[Dependency]
		public EntityService EntityService { get; set; }

		[Dependency]
		public ISiteObjectRelationService SORelationService { get; set; }

		[Auth(RoleList = Role.AnyContManager)]
		public ActionResult Objects(string q, int? limit) {
			if (q.EndsWith(" "))
				q = q.RemoveLast();
			else
				q += "%";
			var names = SORepository.GetAll().Where(so =>
				SqlMethods.Like(so.Name, q)).Take(limit ?? 10)
				.ToList().Select(so => new {
					id = so.ID,
					type = so.Type,
					name = so.Name
				});
			return Json(names, JsonRequestBehavior.AllowGet);
		}

		public ActionResult SiteObjectEdit(string typeName, object id) {
			var entityType = typeof (SpecialistDataContext).Assembly.GetTypes()
				.Where(t => t.FullName == typeName).First();
			var type = LinqToSqlUtils.GetTableName(entityType);
			var siteObject = SiteObjectService
				.GetBy(type, LinqToSqlUtils.CorrectPKType(id, entityType));
			if (siteObject == null)
				return Redirect(Request.UrlReferrer.AbsoluteUri);
			return View(ViewNames.EditRelationList, new EditVM(siteObject,
				MetaDataProvider.Get(typeof (SiteObject))));
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public virtual ActionResult DeleteAll(string tableName, object id) {
			var entityType = ContextProvider.GetTypeByTableName(tableName);
			var relations = SiteObjectRelationRepository.GetAll(x => x.ObjectType == tableName && x.Object_ID ==
				LinqToSqlUtils.CorrectPKType(id, entityType));
			foreach (var relation in relations) {
				SiteObjectRelationRepository.Delete(relation);
			}
			SiteObjectRelationRepository.SubmitChanges();
			return null;
		}

		public ActionResult RelationList(string type, object id, bool? linkCreator) {
			var entityType = ContextProvider.GetTypeByTableName(type);
			var siteObject = SiteObjectService
				.GetBy(type, LinqToSqlUtils.CorrectPKType(id, entityType));
			foreach (var objectRelation in siteObject.ObjectRelations) {
				var entityTypeForObject =
					ContextProvider.GetTypeByTableName(objectRelation.RelationObjectType);
				if (objectRelation.RelationObject == null)
					continue;
				objectRelation.RelationObject.Entity = DynamicRepository
					.GetByPK(entityTypeForObject,
						LinqToSqlUtils.CorrectPKType(
							objectRelation.RelationObject_ID, entityTypeForObject));
			}
			var model = new RelationListVM {
				EntityType = entityType,
				SiteObject = new SiteObject{ID = id,Type = type},
				ForLinkCreator = linkCreator.GetValueOrDefault(),
			};

			if (model.Sortable) {
				model.Relations = siteObject.ObjectRelations.OrderBy(x => x.RelationOrder).ToList();
			}
			else {
				model.Relations = siteObject.ObjectRelations
					.OrderBy(x => x.RelationObject.GetOrDefault(y => y.Name)).ToList();
			}
			return PartialView(PartialViewNames.RelationListControl, model);
		}

		public ActionResult MainEntityList(string type, object id) {
			var entities = EntityService.GetMainEntites();
			return this.Content(
				H.Form(Url.Action<SiteObjectRelationEntityController>(c => c.MainEntityList(type,id,null)))
				.Id("add-entities-form")[
				H.Ul(entities.Select(x => H.span[
				H.InputCheckbox("entities", LinqToSqlUtils.GetTableName(x) + ";" + LinqToSqlUtils.GetPK(x)), x.Name]))
				.Class("default-ul"),
				H.button["Добавить"]]
				.ToString());
		}

		[HttpPost]
		public ActionResult MainEntityList(string type, object id, List<string> entities) {
			if(entities != null) {
					var objectType = ContextProvider.GetTypeByTableName(type);
					var objectId = LinqToSqlUtils.CorrectPKType(id, objectType);
				var oldRelations = SiteObjectRelationRepository
					.GetAll(x => x.Object_ID == objectId && x.ObjectType == type).ToList();
				foreach (var entity in entities) {
					var parts = entity.Split(';');
					var tableName = parts[0];
					var entityType = ContextProvider.GetTypeByTableName(tableName);
					var entityId = LinqToSqlUtils.CorrectPKType(parts[1], entityType);
					var relation = new SiteObjectRelation {
						Object_ID = objectId,
						ObjectType = type,
						RelationObject_ID = entityId,
						RelationObjectType = tableName
					};
					if(!oldRelations.Any(x => x.RelationObject_ID.Equals(relation.RelationObject_ID) 
						&& x.RelationObjectType == relation.RelationObjectType))
						SiteObjectRelationRepository.Insert(relation);
				}
				SiteObjectRelationRepository.SubmitChanges();
			}
			return Json("ok");
		}

		public override ActionResult Add(FormCollection formCollection, string operationType) {
			var obj = new SiteObjectRelation();
			UpdateEntity(obj, formCollection);
			var objectType = ContextProvider.GetTypeByTableName(obj.ObjectType);
			obj.Object_ID = LinqToSqlUtils.CorrectPKType(obj.Object_ID, objectType);
			var relationObjectType = ContextProvider.GetTypeByTableName(
				obj.RelationObjectType);
			obj.RelationObject_ID = LinqToSqlUtils.CorrectPKType(obj.RelationObject_ID, relationObjectType);
			Repository.InsertAndSubmit(obj);
			return Redirect(Request.UrlReferrer.AbsoluteUri);
		}

		[Auth(RoleList = Role.AnyContManager)]
		public ActionResult UpdateRelationSorting(List<long> relationIds)
		{
			var relations = SiteObjectRelationRepository.GetAll(
				x => relationIds.Contains(x.SiteObjectRelation_ID)).ToList();
			for (short i = 0; i < relationIds.Count; i++) {
				var relationId = relationIds[i];
				var relation = relations.FirstOrDefault(x => x.
					SiteObjectRelation_ID == relationId);
				if (relation != null)
					relation.RelationOrder = i;
			}

			SiteObjectRelationRepository.SubmitChanges();

			return Json("ok");
		}

		[Auth(RoleList = Role.AnyContManager)]
		public ActionResult ReverseRelationSorting(string className, string RelationObject_ID,
	 string RelationObjectType) {
			var type = SiteObjectType.GetAll().First(x => x.ClassName == className);
			var model = new ReverseRelationSortingVM();
			model.ObjectType = type.SysName;
			model.ClassName = className;
			if (RelationObject_ID == null)
				return View(model);


			var objId =
				LinqToSqlUtils.CorrectPKType(RelationObject_ID,  ContextProvider.GetTypeByTableName(
				  RelationObjectType));
			var relationObjectType = ContextProvider.GetTypeByTableName(
                    RelationObjectType);
            var relationObjectID = 
                    LinqToSqlUtils.CorrectPKType(RelationObject_ID, relationObjectType);
                
			model.SiteObject = SORepository.GetAll().First(so =>
                    so.ID.Equals(relationObjectID) 
                    && so.Type == RelationObjectType);

			model.Relations = GetCourseRelations(type.Type, RelationObjectType, (int)objId);
			


			return View(model);
		}

		private List<SiteObjectRelation> GetCourseRelations(Type type, string objType, int objId) {
    		return SORelationService.GetByRelation(objType, objId, type)
				.OrderBy(x => x.RelationOrder)
    			.ToList();
    	}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionName("ReverseRelationSorting")]
		[Auth(RoleList = Role.AnyContManager)]
		public ActionResult ReverseRelationSortingPost(string className, 
			string RelationObject_ID,
			string RelationObjectType)
		{
			return RedirectToAction("ReverseRelationSorting", new
			{
				className,
				RelationObject_ID,
				RelationObjectType
			});
		}

	}
}