using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using SimpleUtils.Common;
using SimpleUtils.Reflection;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;

namespace SimpleUtils.Extension
{
    public static class NameValueCollectionExtension
    {

        public static void Update(this NameValueCollection collection, object obj)
        {
            var type = obj.GetType();
            foreach (var key in collection.AllKeys)
            {
                var propertyInfo = type.GetProperty(key);
                if(propertyInfo == null)
                    continue;
                propertyInfo.SetValue(obj, 
                    ObjectUtils.SmartConvert(collection[key], propertyInfo.PropertyType));
            }
        }
        

        public static Dictionary<string, object> ConvertTypeByProperties(this NameValueCollection collection,
            Type type)
        {
            var result = new Dictionary<string, object>();
            foreach (var key in collection.AllKeys)
            {
                if (collection[key] == string.Empty)
                    continue;
                result.Add(key, ObjectUtils.SmartConvert(collection[key],
                    type.GetProperty(key).PropertyType));
            }
            return result;
        }
    }
}