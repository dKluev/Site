using System;
using System.ComponentModel;
using SimpleUtils.FluentAttributes.Const;
using SimpleUtils.FluentAttributes.Core;
using SimpleUtils.FluentAttributes.Core.Extensions;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using Specialist.Web.Cms.Core.FluentMetaData.Attributes;

namespace Specialist.Web.Cms.Core.FluentMetaData
{
    public static class PropertyMetaDataBuilderExtension
    {
        public static PropertyMetaDataBuilder<Te, Tp> ForeignType<Te, Tp>(
        this PropertyMetaDataBuilder<Te, Tp> builder, Type foreignType)
        {
            builder.SetAttribute(new ForeignTypeAttribute(foreignType));
            builder.UIHint(Controls.Select);
            return builder;
        }

        public static PropertyMetaDataBuilder<Te, Tp> NotFilter<Te, Tp>(
       this PropertyMetaDataBuilder<Te, Tp> builder) {
            builder.SetAttribute(new NotFilterAttribute());
            return builder;
        }

        public static bool IsReadOnly(this PropertyMetaData propertyMetaData)
        {
            return propertyMetaData.GetAttribute<ReadOnlyAttribute>() != null
                || !propertyMetaData.Info.CanWrite;
        }
    }
}