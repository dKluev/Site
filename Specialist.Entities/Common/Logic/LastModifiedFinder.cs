using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Reflection;
using SimpleUtils;

namespace Specialist.Entities.Logic
{
    public class LastModifiedFinder
    {
        public const string LastModifiedPropertyName = "LastChangeDate";
        private List<object> used = new List<object>();
        public DateTime? Find(object obj)
        {
           
            if(obj == null)
                return null;
            if(used.Contains(obj))
                return null;
            used.Add(obj);
            var objEnumberable = obj as IEnumerable;
            var result = DateTime.MinValue;
            if(objEnumberable != null)
            {
                if(EntitySetIsDeferred(objEnumberable))
                    return null;
                foreach (var listObj in objEnumberable)
                {
                    var date = Find(listObj);
                    if(date.HasValue && date > result)
                        result = date.Value;
                }
            }
            else
            {
                foreach (var propertyInfo in obj.GetType().GetProperties())
                {
                    var hasLoadedOrAssignedValue = 
                        EntityRefHasLoadedOrAssignedValue(obj, propertyInfo.Name);
                    if (hasLoadedOrAssignedValue.HasValue &&
                        !hasLoadedOrAssignedValue.Value)
                        continue;
                    DateTime? date = null;
                    var value = propertyInfo.GetValue(obj, new object[0]);
                    if (propertyInfo.Name == LastModifiedPropertyName)
                    {
                        date = (DateTime?) value;
                    }
                    else if(propertyInfo.PropertyType.IsClass
                        && propertyInfo.PropertyType != typeof(string))
                    {
                        
                        date = Find(value);
                    }

                    if (date.HasValue && date > result)
                        result = date.Value;

                }
            }

            return result == DateTime.MinValue ? (DateTime?) null : result;
        }

        private bool EntitySetIsDeferred(object obj)
        {
            if (!obj.GetType().IsGenericType)
                return false;
            if (obj.GetType().GetGenericTypeDefinition() != typeof(EntitySet<>))
                return false;
            var isDeferred = (bool)obj.GetType().GetProperty("IsDeferred")
                .GetValue(obj, new object[0]);
            return isDeferred;
        }

        private bool? EntityRefHasLoadedOrAssignedValue(object mainObj, string propertyName)
        {
            var entityRefFieldInfo = mainObj.GetType().GetField("_" + propertyName,
                BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.IgnoreCase | BindingFlags.Instance);
            if (entityRefFieldInfo == null)
                return null;


            if (!entityRefFieldInfo.FieldType.IsGenericType)
                return null;

            if (entityRefFieldInfo.FieldType
                .GetGenericTypeDefinition() != typeof(EntityRef<>))
                return null;
            var obj = entityRefFieldInfo.GetValue(mainObj);
            if(obj == null)
                return null;
            var hasLoadedOrAssignedValue = (bool)entityRefFieldInfo.FieldType
                .GetProperty("HasLoadedOrAssignedValue")
                .GetValue(obj, new object[0]);

            return hasLoadedOrAssignedValue;
        }
       
       
    }
}