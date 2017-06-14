using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Contracts;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using Specialist.Web.Cms.Core.ViewModel;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Repository;
using Specialist.Web.Cms.Util;
using SimpleUtils;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using SimpleUtils.FluentAttributes.Core.Extensions;

namespace Specialist.Web.Cms.Core
{
    public static class SelectListUtil
    {
        public static IMetaDataProvider MetaDataProvider = 
            MvcApplication.MetaDataProvider;
        public static DynamicRepository DynamicRepository = new DynamicRepository(MvcApplication.Container);
        public static List<SelectListItem> GetSourceForFilter(
    PropertyMetaData property, System.Collections.IEnumerable source, Type entityType,
    object filterValue)
        {
            var result = GetSelectedListItems(property, source, filterValue);
            result.Insert(0, _emtpySelectItem);
            return result;
        }

        private static List<SelectListItem> GetSelectedListItems(
            PropertyMetaData property, System.Collections.IEnumerable source, object value)
        {
            var result = GetSelectedListItems(property, source);
            SetSelected(result, new [] {value} );
            return result.OrderBy(sli => sli.Text).ToList();
        }

                private static List<SelectListItem> GetSelectedListItems(
            PropertyMetaData property, System.Collections.IEnumerable source)
        {
            var result = new List<SelectListItem>();
            var relatedPropertyType = property.ForeignType();
            var foreignMetaData = MetaDataProvider.Get(relatedPropertyType);
            var first = true;
            PropertyInfo propertyInfo = null;
            foreach (var o in source)
            {

                object id;
                if(first)
                {
                    propertyInfo = o.GetType().GetProperty(property.Info.Name);
                    first = false; 
                }
                if (propertyInfo != null)
                    id = o.GetValue(property.Info.Name);
                else
                    id = o.GetValue(LinqToSqlUtils.GetPKPropertyName(
                        foreignMetaData.EntityType));
                result.Add(new SelectListItem
                {
                    Text = GetDisplay(o, foreignMetaData).ToString(),
                    Value = id.ToString()
                });
            }

            return result.OrderBy(sli => sli.Text).ToList();
        }


        public static List<SelectListItem> GetSelectedListItems(
          PropertyMetaData property, System.Collections.IEnumerable source, 
            List<object> values)
        {
            var result = GetSelectedListItems(property, source);
            SetSelected(result, values);
            return result.OrderBy(sli => sli.Text).ToList();
        }

        private static void SetSelected(IEnumerable<SelectListItem> list, 
            IEnumerable<object> values)
        {
            foreach (var item in list)
            {
                if (values.Any(o => item.Value == o.NotNullString()))
                    item.Selected = true;
            }
        }

        private static readonly SelectListItem _emtpySelectItem = 
            new SelectListItem { Text = "Нет", Value = string.Empty };
        public static List<SelectListItem> GetSourceForComboBox(
          PropertyMetaData property, object currentValue, IEnumerable source)
        {
            
            var result = GetSelectedListItems(property, source, currentValue);

            if (LinqToSqlUtils.CanBeNull(property.Info))
                result.Insert(0, _emtpySelectItem);
            return result;
        }

        public static object GetDisplay(object item)
        {
            if (item == null)
                return string.Empty;
            return GetDisplay(item, MetaDataProvider.Get(item.GetType()));
        }

        public static object GetDisplay(object item, BaseMetaData metaData)
        {
            if (item == null)
                return string.Empty;
            if (metaData == null)
                if (item.GetType() == typeof(string))
                    return EntityDisplayName.CutLong((string)item);
                else
                    return item;
            var property = metaData.GetDisplayPropertyMetaData();
            if (property == null || property.ForeignType() == null)
                return metaData.DisplayProperty().GetOrDefault(x =>  EntityDisplayName.CutLong(
                    item.GetValue(x.Name).ToString()));

            var obj = property.Info.GetValue(item);
            return GetDisplay(obj);

        }

        public static object GetValueForProperty(PropertyMetaData property, object item, UrlHelper url)
        {
            object value;

            if (property.Control() == Controls.Select)
            {
                var foreignType = property.ForeignType();
                var foreignMetaData = MetaDataProvider.Get(
                    foreignType);
                object foreignValue = null;


                if (property.ForeignInfo() != null)
                    foreignValue = item.GetValue(property.ForeignInfo().Name);
                else {
                    var id = item.GetValue(property.Name);
                    if(id != null)
                        foreignValue = DynamicRepository.GetByPK(foreignType, id);
                }
                Contract.NotNull(new { foreignMetaData }, new { foreignType });
                if (!foreignMetaData.IsReadOnly())
                {

                    value = new ListVM.EntityLink(
                        foreignValue,
                        foreignMetaData, url);
                }
                else
                    value = GetDisplay(foreignValue);
            }
            else
            {
                var metaData = MetaDataProvider.Get(item.GetType());
                if (metaData.DisplayProperty() != null && property.Name == metaData.DisplayProperty().Name)
                    value = new ListVM.EntityLink(item, metaData, url);
                else
                {
                	var propertyValue = property.Info.GetValue(item);
					if(property.GetAttribute<FullLengthStringDisplayAttribute>() != null) {
						value = propertyValue.NotNullString();
					}
					else {
	                	value = GetDisplay(propertyValue);
	                    if (property.Format() != null)
	                        value = value.NotNullString(property.Format());
					}
                }
            }
            return value;
        }

    }
}