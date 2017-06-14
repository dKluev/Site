using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using SimpleUtils.FluentAttributes.Core.Extensions;
using SimpleUtils.FluentAttributes.Core.Interfaces;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;

namespace Specialist.Web.Cms.Core.FluentMetaData.Attributes
{
    public static class AdditionalAttributesExtension
    {
        public static Type ForeignType(this IAdditionalAttributes attributes)
        {
            return attributes.GetAttribute<ForeignTypeAttribute>().GetOrDefault(x => x.ForeignType);
        }

        public static string Control(this IAdditionalAttributes attributes)
        {
            return attributes.GetAttribute<UIHintAttribute>().UIHint;
        }

        public static bool NotFilter(this IAdditionalAttributes attributes) {
            return attributes.GetAttribute<NotFilterAttribute>() != null;
        }


        public static string Format(this IAdditionalAttributes attributes)
        {
            return attributes.GetAttribute<DisplayFormatAttribute>().GetOrDefault(x => x.DataFormatString);
        }

        public static string DisplayName(this IAdditionalAttributes attributes)
        {
            return attributes.GetAttribute<DisplayNameAttribute>().DisplayName;
        }

        /*public static string DisplayName(this IAdditionalAttributes attributes, string )
        {
            return attributes.GetAttribute<DisplayNameAttribute>().DisplayName;
        }*/

        public static PropertyInfo ForeignInfo(this IAdditionalAttributes attributes)
        {
            return attributes.GetAttribute<ForeignTypeAttribute>().GetOrDefault(x => x.ForeignInfo);
        }

        public static bool IsReadOnly(this IAdditionalAttributes attributes)
        {
            return attributes.GetAttribute<ReadOnlyAttribute>() != null;
        }

        public static IAdditionalAttributes ReadOnly(this IAdditionalAttributes attributes)
        {
            attributes.SetAttribute(new ReadOnlyAttribute(true));
            return attributes;
        }

        public static bool IsNotAdd(this IAdditionalAttributes attributes) {
            return attributes.GetAttribute<NotAddAttribute>() != null;
        }

        public static void NotAdd(this IAdditionalAttributes attributes) {
            attributes.SetAttribute(new NotAddAttribute());
        }

    }
}