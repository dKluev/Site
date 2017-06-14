using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using SimpleUtils.FluentAttributes.Utils;
using Microsoft.Practices.Unity;
using SimpleUtils.Collections;
using SimpleUtils.Collections.Paging;
using SimpleUtils.Common;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Utils;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Services.Common.Extension;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Catalog;
using Specialist.Services.Interface.Passport;
using Specialist.Services.Utils;
using Specialist.Web.ActionFilters;
using Specialist.Web.Cms.Const;

using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Controls;
using Specialist.Web.Cms.Core.Service;
using Specialist.Web.Cms.Core.ViewModel;
using Specialist.Web.Cms.Core.ViewModel.Interfaces;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.MetaData.Validators;
using Specialist.Web.Cms.Repository;
using System.Linq;
using System.Linq.Dynamic;
using SimpleUtils.Reflection;
using SimpleUtils.Extension;
using SimpleUtils;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Helpers;
using Specialist.Web.Common.Extension;
using EntityUtils = Specialist.Web.Util.EntityUtils;
using SimpleUtils.Collections.Extensions;

namespace Specialist.Web.Cms.Core
{
	[CmsAuth(RoleList = Role.ContentManager)]
    public class BaseController<T>: Controller where T : class, new()
    {
        
        [Dependency]
        public ComboBoxSourceCreator ComboBoxSourceCreator { get; set; }

        [Dependency]
        public DynamicRepository DynamicRepository { get; set; }

        protected IMetaDataProvider MetaDataProvider { get; set; }

        [Dependency]
        public IRepository<SiteObjectType> SiteObjectTypeRepository { get; set; }

        [Dependency]
        public ISiteObjectService SiteObjectService { get; set; }

        [Dependency]
        public IRepository<T> Repository { get; set; }

        [Dependency]
        public IAuthService AuthService { get; set; }

        public BaseMetaData MetaData { get; set;}

		public ICmsValidator<T> Validator { get; set; } 

        public HtmlHelper Html
        {
            get
            {
                return new HtmlHelper(new ViewContext(ControllerContext,
                    new WebFormView(ControllerContext, "web"), ViewData, TempData, new StringWriter()), 
                    new ViewPage());
            }
        }

        public const string UpdateDate = "UpdateDate";

        public void SetUpdateDateAndChanger(T e) {
            var user = AuthService.CurrentUser;
        	var employeeTC = user.Employee_TC;
			if(employeeTC.IsEmpty())
				throw new Exception("employee_tc is null");
        	EntityUtils.SetUpdateDateAndLastChanger(e, employeeTC);
        }


    	[InjectionMethod]
        public virtual void Initialize(IMetaDataProvider metaDataService) 
        {
            MetaDataProvider = metaDataService;
            MetaData = MetaDataProvider.Get(typeof (T));
    		Validator =  (ICmsValidator<T>) AllValidators.List.GetValueOrDefault(typeof (T));            
    	}

    /*    [InjectionMethod]
        public virtual void SetInvoker(IUnityContainer container)
        {
            ActionInvoker = new CustomControllerActionInvoker(container);
        }
*/

        protected void AddExtraControls(IExtraControls model, bool tabFocus)
        {
            foreach (var metaData in MetaDataProviderUtils.GetWhereForeign(MetaDataProvider, MetaData.EntityType))
            {
                /*if(metaData.IsReadOnly())
                    continue;*/

                if(tabFocus)
                    model.ExtraControls.Add(new TabFocusControl(
                        "[" + metaData.DisplayName() + "]", MetaData.EntityType.Name 
                        + Const.Common.ControlPosfix));
                else
                {
                    var foreignProperty = metaData.GetProperties()
                        .Where(p => p.ForeignType() != null &&
                            p.ForeignType() == MetaData.EntityType).First();
                    model.ExtraControls.Add(new FilterLinkControl(
                        "[" + metaData.DisplayName() + "]", metaData.EntityType.Name
                        + Const.Common.ControlPosfix,
                        foreignProperty.Info.Name));
                }

            }

            var siteObjectType = LinqToSqlUtils.GetTableName(MetaData.EntityType);
            if(SiteObjectTypeRepository.GetByPK(siteObjectType) != null)
                model.ExtraControls.Add(new RelationsLinkControl());
        }

        public virtual ActionResult List(int pageIndex, OrderColumn orderColumn)
        {
           
            var currentFilterValues = GetCurrentFilterValues();
            var items = AddFilterToList(currentFilterValues);
            if(orderColumn.ColumnName.IsEmpty()) {
                if(MetaData.EntityType.HasProperty(UpdateDate)) {
                    orderColumn = new OrderColumn {
                        IsDesc = true,
                        ColumnName = UpdateDate,
                    };
                    
                }else
                    orderColumn = new OrderColumn {
                        IsDesc = true,
                        ColumnName =
                            LinqToSqlUtils.GetPKPropertyName(MetaData.EntityType)
                    };
            }
                   


            if(!orderColumn.ColumnName.IsEmpty())
            {
                var orderQuery = orderColumn.ColumnName;
                if (orderColumn.IsDesc)
                    orderQuery += " " + Const.Common.Descending;
                items = items.OrderBy(orderQuery );
            }
            items = OnListSelecting(items);
            var tableData = new List<ListVM.Row>();
            var pagedList = items.ToPagedList(pageIndex - 1);
            foreach (var item in pagedList)
            {
                var id = LinqToSqlUtils.GetPK(item);
                var list = GetValuesForRow(item);
                tableData.Add(new ListVM.Row { Id = id, Entity = item, Values = list });
            }

            var listVM = new ListVM(MetaData, pagedList, tableData);
            AddExtraControls(listVM, true);
            listVM.FilterValues = currentFilterValues;
            listVM.OrderColumn = orderColumn;

            ListVMCreated(listVM);

            return View(listVM);
        }

        protected virtual void ListVMCreated(ListVM listVM)
        {
            
        }

        protected virtual IQueryable<T> OnListSelecting(IQueryable<T> entities)
        {
            return entities;
        }

        protected virtual Dictionary<string, object> GetCurrentFilterValues()
        {
            var currentFilterValues = new Dictionary<string, string>();
            foreach (var key in GetFilterQueryStringKeys())
            {
                if(MetaData.GetProperties().Any(p => p.Name == key))
                    currentFilterValues.Add(key, Request.QueryString[key]);
            }

            return DictionaryUtils.ConvertTypeByProperties(currentFilterValues, typeof(T));
        }

        protected virtual IEnumerable<string> GetFilterQueryStringKeys() {
            return Request.QueryString.AllKeys;
        }

        public virtual ActionResult ListControl(int pageIndex, OrderColumn orderColumn)
        {

            var currentFilterValues = GetCurrentFilterValues();
            var items = AddFilterToList(currentFilterValues);

            if (!orderColumn.ColumnName.IsEmpty())
            {
                var orderQuery = orderColumn.ColumnName;
                if (orderColumn.IsDesc)
                    orderQuery += " " + Const.Common.Descending;
                items = items.OrderBy(orderQuery);
            }

            var tableData = new List<ListVM.Row>();
            var pagedList = items.ToPagedList(pageIndex - 1);
            foreach (var item in pagedList)
            {
                var id = LinqToSqlUtils.GetPK(item);
                var list = GetValuesForRow(item);
                tableData.Add(new ListVM.Row { Id = id, Entity = item, Values = list });
            }

            var listVM = new ListVM(MetaData, pagedList, tableData);
            AddExtraControls(listVM, true);
            listVM.FilterValues = currentFilterValues;
            listVM.OrderColumn = orderColumn;

            return View(PartialViewNames.ListControl, listVM);
        }

        protected virtual IQueryable<T> AddFilterToList(Dictionary<string, object> currentFilterValues) {
            return Repository.GetAll().Filter(currentFilterValues);
        }

        protected virtual List<object> GetValuesForRow(T item)
        {
            var list = new List<object>();
            foreach (var property in MetaData.GetProperties())
            {
                if(property.Control() == Controls.Hidden)
                    continue;
                var value = SelectListUtil.GetValueForProperty(property, item, Url);
                list.Add(value);
            }
            return list;
        }

       

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult List(int pageIndex, FormCollection formCollection)
        {
            var filterValues = formCollection.ConvertTypeByProperties(typeof(T));
            var routeValueDictionary = new RouteValueDictionary(filterValues) { { "pageIndex", 1 } };
        	foreach (var key in routeValueDictionary.Keys.ToList()) {
        		var value = routeValueDictionary[key];
				if(value is DateTime || value is DateTime?)
					routeValueDictionary[key] = ((DateTime?) value).DefaultString();
        	}
            return RedirectToAction("List", routeValueDictionary);
        }

     


        public virtual ActionResult Add()
        {
            var obj = new T();

            var typedValues = Request.QueryString.ConvertTypeByProperties(typeof(T));
            foreach (var pair in typedValues)
            {
                obj.SetValue(pair.Value, pair.Key);
            }


            var editVM = new EditVM(obj, MetaData);
            editVM.FilterValues = GetCurrentFilterValues();
            editVM.QueryString = Request.QueryString.ToString();
            return View("Add", editVM);
        }

		protected virtual void AfterAdded(T entity) {
			
		}

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public virtual ActionResult Add(FormCollection formCollection, string operationType)
        {
            var queryString = formCollection["QueryString"];
            var values = new Dictionary<string, object>();
            if (queryString != null)
                values = HttpUtility.ParseQueryString(queryString)
                    .ConvertTypeByProperties(typeof(T));
            var routeValueDictionary =
                new RouteValueDictionary(values) { { "pageIndex", 1 } };

            var obj = new T();
            UpdateEntity(obj, formCollection);
            OnEntityAdding(obj);
            Validate(obj);
            if (!ModelState.IsValid)
            {
                var editVM = new EditVM(obj, MetaData);
                return View("Add", editVM);
            }
            try
            {
                SetUpdateDateAndChanger(obj);
                Repository.InsertAndSubmit(obj);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);               
                var editVM = new EditVM(obj, MetaData);
                return View("Add", editVM);
            }
            AfterAdded(obj);
            
            if(operationType == OperationType.Apply)
                return RedirectToAction("Edit", new {id = LinqToSqlUtils.GetPK(obj)});
            return RedirectToAction("List", routeValueDictionary);
        }

        private void Validate(T obj) {
	        if (Validator == null) return;
            foreach (var validationResult in Validator.Validate(obj))
            {
                ModelState.AddModelError("", validationResult);
            }
        }

        protected virtual void OnEntityAdding(T entity)
        {
            
        }

        public virtual ActionResult Edit(object id, int? tabFocus)
        {
        	id = Server.UrlDecode(id.ToString());
            id = Convert.ChangeType(id, LinqToSqlUtils.GetPKPropertyInfo(
                MetaData.EntityType).PropertyType);
            var obj = Repository.GetByPK(id);
			if(obj == null)
				return Content("Объект не найден");

            var editVM = new EditVM(obj, MetaData);
            AddExtraControls(editVM, false);

        /*    if(tabFocus.HasValue)
            {
                for (int i = 0; i < editVM.MetaData.ExtraControls.Count; i++)
                {
                    var control = editVM.MetaData.ExtraControls[i];
                    if (control.DisplayName.GetHashCode() == tabFocus)
                        editVM.TabFocus = i + 1;
                }
            }*/
            editVM.SiteUrl =  Html.GetUrlFor(obj);

            SetSiteObject(id, editVM);

            return View("Edit", editVM);
        }



        private void SetSiteObject(object id, EditVM editVM)
        {
            var siteObjectType = LinqToSqlUtils.GetTableName(MetaData.EntityType);
            editVM.SiteObject = SiteObjectService.GetBy(siteObjectType, id);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public virtual ActionResult Edit(object id, FormCollection formCollection,
            string operationType)
        {
			id = Server.UrlDecode(id.ToString());
            id = CorrectIdType(id);
            var obj = Repository.GetByPK(id);
			BeforeUpdate(obj);
            UpdateEntity(obj, formCollection);
			AfterUpdate(obj);
            // Validate(obj); TODO: validate
            if (!ModelState.IsValid)
            {
                return GetEditView(obj);
            }

            try
            {
                BeforeEditSubmit(obj);
                SetUpdateDateAndChanger(obj);
                Repository.SubmitChanges();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return GetEditView(obj);
            }
            if (Request.IsAjaxRequest())
                return null;
            if (operationType == OperationType.Apply)
                return RedirectToAction("Edit", new { id });
            return RedirectToAction("List", new { pageIndex = 1 });
        }

        protected virtual void BeforeEditSubmit(T entity)
        {
            
        }

        protected object CorrectIdType(object id)
        {
            return LinqToSqlUtils.CorrectPKType(id, typeof(T));
        }


        private ActionResult GetEditView(T obj)
        {
            var editVM = new EditVM(obj, MetaData);
            return View("Edit", editVM);
        }

        public ActionResult RedirectBack()
        {
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Delete(object id)
        {
			id = Server.UrlDecode(id.ToString());
            var obj = Repository.GetByPK(CorrectIdType(id));

            try
            {
                Repository.DeleteAndSubmit(obj);
            }
            catch(Exception e)
            {
                ModelState.AddModelError("_FORM", e);
            }
            if (Request.IsAjaxRequest())
                return null;
            return RedirectBack();
        }

        public virtual ActionResult FilterComboBox(string propertyName, object currentValue)
        {
           var propertyNameWithoutPrefix = RemovePrefix(propertyName);
            var propertyMetaData = MetaData.GetProperties()
                .First(p => p.Name == propertyNameWithoutPrefix);
            var model = new ComboBoxVM();
            model.PropertyName = propertyName;
            var filterSource = ComboBoxSourceCreator
                .GetFilterComboBoxSource(MetaData, propertyMetaData, currentValue);
            model.Source = SelectListUtil.GetSourceForFilter(propertyMetaData,
                            filterSource, MetaData.EntityType, currentValue);
            return PartialView(Const.Common.FolderControls + Controls.Select, model);

        }

        [ValidateInput(false)]
        public virtual ActionResult Select(string propertyName, object currentValue, 
            object id)
        {
            if(id == string.Empty)
                id = null;
            var propertyNameWithoutPrefix = RemovePrefix(propertyName);
            var propertyMetaData = MetaData.GetProperties()
                .First(p => p.Name == propertyNameWithoutPrefix);
            var model = new ComboBoxVM();
            model.PropertyName = propertyName;
            currentValue = ObjectUtils.SmartConvert(currentValue.NotNullString(),
                propertyMetaData.Type);
            var comboBoxSource = ComboBoxSourceCreator.GetComboBoxSource(propertyMetaData,
                GetComboBoxFilter(propertyName, id));
            model.Source = SelectListUtil.GetSourceForComboBox(propertyMetaData, 
                currentValue, comboBoxSource);
            return PartialView(Const.Common.FolderControls + Controls.Select, model);

        }

        protected virtual string GetComboBoxFilter(string propertyName, object id) {
            return null;
        }

        public virtual ActionResult CheckBoxList(string propertyName, object id)
        {
            id = CorrectIdType(id);
            var propertyNameWithoutPrefix = RemovePrefix(propertyName);
            var propertyMetaData = MetaData.GetProperties()
                .First(p => p.Name == propertyNameWithoutPrefix);
            var model = new ComboBoxVM();
            model.PropertyName = propertyName;
            var entity = Repository.GetByPK(id);
            var m2MEntities = entity.GetValue(propertyNameWithoutPrefix) as IEnumerable;
            var values = new List<object>();
            var otherM2MType = LinqToSqlUtils.GetOtherM2MEntityType(propertyMetaData.Info);
            var otherM2MPropertyInfo = LinqToSqlUtils.GetOtherM2MPropertyInfo(
                propertyMetaData.Info);
            foreach (var m2MEntity in m2MEntities)
            {
                values.Add(otherM2MPropertyInfo.GetValue(m2MEntity));
            }
            var source = ComboBoxSourceCreator.GetSource(otherM2MType);
            model.Source = SelectListUtil.GetSelectedListItems(propertyMetaData,
                source, values);
            return PartialView(Const.Common.FolderControls + 
                Controls.CheckBoxList, model);

        }

        private string RemovePrefix(string propertyName)
        {
            var lastDotIndex = propertyName.LastIndexOf('.');
            if (lastDotIndex >= 0)
                return propertyName.Substring(lastDotIndex + 1);
            return propertyName;

        }


		public const string ShowMessageKey = "ShowMessageKey";

		public void ShowMessage(string text) {
			Session[ShowMessageKey] = text;
		}

		protected virtual void BeforeUpdate(T obj) {
			
		}

		protected virtual void AfterUpdate(T obj) {
			
		}

        protected void UpdateEntity(object obj, NameValueCollection formCollection)
        {
            var metaData = MetaDataProvider.Get(obj.GetType());

            if(metaData == null)
                return;
            foreach (var property in metaData.GetProperties())
            {
                try
                {
                    if (property.Control() == Controls.PropertyGrid)
                    {
                        var collectionForComplexProperty = new NameValueCollection();
                        foreach (var tempKey in formCollection.AllKeys)
                        {
                            var prefix = property.Name + ".";
                            if (tempKey.StartsWith(prefix))
                                collectionForComplexProperty.Add(
                                    tempKey.Remove(0, prefix.Length), formCollection[tempKey]);
                        }
                        UpdateEntity(obj.GetValue(property.Name),
                            collectionForComplexProperty);
                        continue;
                    }
                    object value = null;
              /*      if(property.IsManyToMany)
                    {
                        var m2mEntities = property.Info.GetValue(obj);
                        m2mEntities.InvokeMethod("Clear");
                        var idList = formCollection[property.Info.Name].Split(',');
                        var setType = property.Type.GetGenericArguments().First();
                        var idPropertyName = LinqToSqlUtil.GetIdPropertyName(typeof(T));
                        var entityID = LinqToSqlUtil.GetId(obj);
                        var m2MPropertyInfo = 
                            LinqToSqlUtil.GetOtherM2MPropertyInfo(property.Info);
                        foreach (var id in idList)
                        {
                            var correctId = LinqToSqlUtil.CorrectIdType(id,
                                property.ForeignType);
                            var m2mEntity = setType.Create();
                            m2MPropertyInfo.SetValue(m2mEntity, correctId);
                            m2mEntity.SetValue(entityID, idPropertyName);

                            m2mEntities.InvokeMethod("Add", m2mEntity);

                        }
                        continue;
                    }*/
                    var key = property.Info.Name;
                    var valueType = obj.GetType().GetProperty(key).PropertyType;
                    if (valueType == typeof(bool))
                    {
                        bool b;
                        if (bool.TryParse(formCollection[key], out b))
                            value = b;
                    }
                    else if (formCollection[key] == string.Empty
                        && property.Control() == Controls.Select) { }
                    else if (formCollection[key] == null)
                        continue;
                    else
                        value = ObjectUtils.SmartConvert(formCollection[key], valueType);
                    obj.SetValue(value, key);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(property.Name, ex.Message);
                }
           
            }
        }
    }
}