using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Util;
using SimpleUtils.FluentAttributes.Core.Extensions;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;
using SimpleUtils.Extension;

namespace Specialist.Web.Cms.Core.FluentMetaData
{
    public static class CmsMetaDataAttributeProviders
    {
        public static List<Attribute> ForeignTypeAttribute(PropertyMetaData propertyMetaData)
        {
            var result = new List<Attribute>();
            var foreignProperty = LinqToSqlUtils.GetForeignProperty(propertyMetaData.Info.DeclaringType,
                                                                   propertyMetaData.Name);
            if (foreignProperty != null)
            {
                propertyMetaData.SetAttribute(new ForeignTypeAttribute(foreignProperty));
                propertyMetaData.SetAttribute(new UIHintAttribute(Controls.Select));
            }

            return result;
        }

        public static List<Attribute> DispayAttribute(PropertyMetaData propertyMetaData) {
            var result = new List<Attribute>();
            if (propertyMetaData.Info.PropertyType.In(typeof(DateTime), typeof(DateTime?)))
                result.Add(new DisplayFormatAttribute {
                    DataFormatString = "dd.MM.yy"
                });
            return result;
        }

    }
}