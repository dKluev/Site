using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Utils;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using SimpleUtils.Reflection;
using SimpleUtils.FluentAttributes.Core.Extensions;
using System.Linq;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.MetaData.Utils
{
    public static class BaseMetaDataExtension
    {
        public static PropertyMetaDataBuilder<Te, string> AddName<Te>(this BaseMetaData<Te> metaData)
        {
            var propertyForName = ReflectionUtils.GetPropertyForName(metaData.EntityType);
            return AddProperty<Te, string>(metaData, propertyForName, "Название");
        }

        public static void Deletable<Te>(this BaseMetaData<Te> metaData)
        {
            metaData.SetAttribute(new DeletableAttribute());
        }

        public static PropertyMetaData GetDisplayPropertyMetaData(this BaseMetaData metaData)
        {
            var displayColumnAttribute = metaData.GetAttribute<DisplayColumnAttribute>();
            if (displayColumnAttribute == null)
                return null;
            var propertyName = displayColumnAttribute.DisplayColumn;
            return metaData.GetProperties().FirstOrDefault(x => x.Name == propertyName);
        }

        public static PropertyInfo DisplayProperty(this BaseMetaData metaData)
        {
            var displayColumnAttribute = metaData.GetAttribute<DisplayColumnAttribute>();
            if(displayColumnAttribute == null)
                return null;
            return metaData.EntityType.GetProperty(displayColumnAttribute.DisplayColumn);
            
        }

      /*  public static bool CanEdit(this BaseMetaData metaData)
        {
            return metaData.GetAttribute<ReadOnlyAttribute>() == null;
        }*/

        public static bool CanDelete(this BaseMetaData metaData)
        {
            return metaData.GetAttribute<DeletableAttribute>() != null;
        }


        public static void TryAddStandartProperties<Te>(this BaseMetaData<Te> metaData, bool hideUrlName = false)
        {
            var isActive = "IsActive";
            var description = "Description";
            var urlName = "UrlName";
            var webSortOrder = "WebSortOrder";

			if(!hideUrlName)
	            TryAddProperty<Te, string>(metaData, urlName, "Название в ЧПУ");
            TryAddProperty<Te, string>(metaData, description, "Описание").UIHint(Controls.Html);
            TryAddProperty<Te, bool>(metaData, isActive, "Активен");
            TryAddProperty<Te, bool>(metaData, webSortOrder, "Сорт.");


        }

        public static void TryAddUpdateAndChanger<Te>(this BaseMetaData<Te> metaData) {
 
            var updateDate = "UpdateDate";
            var lastChangerTC = "LastChanger_TC";

            TryAddProperty<Te, bool>(metaData, updateDate, "Дата изм.")
                .Format("dd.MM.yy HH:mm:ss")
                .ReadOnly();
            TryAddProperty<Te, bool>(metaData, lastChangerTC, "Изм.")
                .ReadOnly();

        }


        private static PropertyMetaDataBuilder<Te, Tp> TryAddProperty<Te, Tp>(BaseMetaData metaData,
        string propertyName, string displayName)
        {
            if (!metaData.EntityType.HasProperty(propertyName))
                return new PropertyMetaDataBuilder<Te, Tp>((PropertyInfo)null);
            var propertyInfo = metaData.EntityType.GetPropertyInfoNN(propertyName);
            return AddProperty<Te, Tp>(metaData, propertyInfo, displayName);
        }

        private static PropertyMetaDataBuilder<Te, Tp> AddProperty<Te, Tp>(BaseMetaData metaData,
           string propertyName, string displayName)
        {
            var propertyInfo = metaData.EntityType.GetPropertyInfoNN(propertyName);
            return AddProperty<Te, Tp>(metaData, propertyInfo, displayName);
        }

        private static PropertyMetaDataBuilder<Te, Tp> AddProperty<Te, Tp>(BaseMetaData metaData,
            PropertyInfo propertyForName, string displayName)
        {
            var propertyMetaDataBuilder = new PropertyMetaDataBuilder<Te, Tp>(propertyForName);
            propertyMetaDataBuilder.SetAttribute(new DisplayNameAttribute(displayName));
            metaData.PropertyBuilders.Add(propertyMetaDataBuilder);
            return propertyMetaDataBuilder;
        }
    }
}