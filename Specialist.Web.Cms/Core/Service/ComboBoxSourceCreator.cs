using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Providers.Interfaces;
using Microsoft.Practices.Unity;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using Specialist.Services.Core.Interface;
using Specialist.Web.Cms.MetaData.Utils;
using Specialist.Web.Cms.Repository;
using SimpleUtils.Extension;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.Core.Service
{
    public class ComboBoxSourceCreator
    {


        private IQueryable GetFilterComboBoxSource(PropertyMetaData propertyMetaData,
            BaseMetaData metaData)
        {
            if (propertyMetaData.ForeignInfo() == null)
                return GetComboBoxSource(propertyMetaData, false);
            string selector = GetSelector(propertyMetaData);

            return DynamicRepository.GetAll(metaData.EntityType)
                          .Where(propertyMetaData.Info.Name + " != null")
                          .Select(selector)
                          .Cast<object>().Distinct();
        }

        [Dependency]
        public DynamicRepository DynamicRepository { get; set; }

        [Dependency]
        public IMetaDataProvider MetaDataProvider { get; set; }

        public string GetSelector(PropertyMetaData propertyMetaData) {
            return GetSelector(propertyMetaData, false);
        }

        public string GetSelector(PropertyMetaData propertyMetaData, 
            bool withoutProperty)
        {
            var relatedMeta = MetaDataProvider.Get(propertyMetaData.ForeignType());
            var property = string.Empty;
            if (!withoutProperty && propertyMetaData.ForeignInfo() != null)
                property = propertyMetaData.ForeignInfo().Name + ".";
            var idProperty = property
                             + LinqToSqlUtils.GetPKPropertyName(relatedMeta.EntityType);
            return "new(" + idProperty +
                   " as " + propertyMetaData.Info.Name +
                   ", " + property +
                   relatedMeta.DisplayProperty().Name + ")";
        }

        public IEnumerable GetFilterComboBoxSource(BaseMetaData metaData,
            PropertyMetaData propertyMetaData, object filterValue)
        {
            var source = GetFilterComboBoxSource(propertyMetaData, metaData)
                .Cast<object>().ToList();
            if (filterValue != null)
            {
                AddFilterValueToSource(propertyMetaData, filterValue, source);
            }
            return source;
        }

        private void AddFilterValueToSource(PropertyMetaData propertyMetaData, 
            object filterValue, ICollection<object> source)
        {
            var nameValue = source.Where(o => o.GetValue(propertyMetaData.Info.Name)
                                                  .Equals(filterValue));
            if (nameValue.Count() == 0)
            {
                var foreignType = propertyMetaData.ForeignType();
//                if(propertyMetaData.ForeignInfo() != null)
                   var item = DynamicRepository.GetAll(
                        foreignType)
                        .Where(LinqToSqlUtils
                            .GetPKPropertyName(foreignType) + "= @0", filterValue)
                        .Select(GetSelector(propertyMetaData, true))
                        .Cast<object>().First();
/*                else {
                    item = DynamicRepository.GetByPK( foreignType, filterValue); }*/
            
                source.Add(item);
            }
        }

        public virtual IQueryable GetComboBoxSource(
            PropertyMetaData propertyMetaData)
        {
            return GetComboBoxSource(propertyMetaData, null);
        }


        public virtual IQueryable GetComboBoxSource(
            PropertyMetaData propertyMetaData, string filter)
        {
            return GetComboBoxSource(propertyMetaData, false, filter);
        }

        public virtual IQueryable GetComboBoxSource(
            PropertyMetaData propertyMetaData, bool onlyActive)
        {
            return GetComboBoxSource(propertyMetaData, onlyActive, null);
        }

        public virtual IQueryable GetComboBoxSource(
            PropertyMetaData propertyMetaData, bool onlyActive, string filter)
        {
            var relatedMeta = MetaDataProvider.Get(
                   propertyMetaData.ForeignType());
            var idProperty = LinqToSqlUtils.GetPKPropertyName(relatedMeta.EntityType);
            var selector = "new(" + idProperty +
                           " as " + propertyMetaData.Info.Name +
                           ", " + relatedMeta.DisplayProperty().Name + ")";

            var result =
                DynamicRepository.GetAll( propertyMetaData.ForeignType());
            if (filter != null)
                result = result.Where(filter);
     /*       if(onlyActive)
                foreach (var property in Const.Common.ActiveProperties)
                {
                    if(propertyMetaData.ForeignType.HasProperty(property))
                        result = result.Where(property);
                }*/

            return result.Select(selector);
        }

        public virtual IQueryable GetSource(Type entityType)
        {
            var relatedMeta = MetaDataProvider.Get(entityType);
            var idProperty = LinqToSqlUtils.GetPKPropertyName(relatedMeta.EntityType);
            var selector = "new(" + idProperty + 
                           ", " + relatedMeta.DisplayProperty().Name + ")";
            return DynamicRepository.GetAll(
                        entityType)
                        .Select(selector);
        }

      /*  public Dictionary<string, IEnumerable> GetSourceForComboBoxs(
            Dictionary<string, object> filter)
        {
            var sources = new Dictionary<string, IEnumerable>();
            foreach (var property in _metaData.Properties)
            {
                if (property.ForeignType != null)
                {
                    sources.Add(property.Name, 
                        filter != null
                        ? GetFilterComboBoxSource(property, 
                            filter.GetValueOrDefault(property.Name)) 
                        : GetComboBoxSource(property));
                }

            }
            return sources;
        }*/

    /*    private void AddSourceForComboBoxs(Dictionary<string, IEnumerable> sources,
        Dictionary<string, object> filter, MetaData metaData)
        {
            foreach (var property in metaData.Properties)
            {
                if(property.Control == ControlNames.PropertyGrid)
                    AddSourceForComboBoxs(sources, filter, MetaDataProvider.GetFor());
                if (property.ForeignType != null)
                {
                    sources.Add(property.Info.Name, filter != null ?
                        GetFilterComboBoxSource(property, filter) : GetComboBoxSource(property));
                }

            }
        }*/
    }
}