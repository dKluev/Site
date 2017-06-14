using System;
using System.Data.Linq.Mapping;
using System.Linq;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.Reflection;
using SimpleUtils.Reflection.Extensions;

namespace SimpleUtils.LinqToSql
{
    public class LinqEntityMockGenerator
    {
        public static object GetObject(Type type, object id)
        {

            var constructor = type.GetConstructors()[0];
            var obj = constructor.Invoke(null);
            var publicPIs = type.GetProperties();
            var associationPIs = publicPIs.Where(pi => pi.HasAttribute(typeof(AssociationAttribute)));
            var thisKeys = associationPIs.Select(pi => pi.GetAttribute<AssociationAttribute>().ThisKey);
            var idPropertyname = LinqToSqlUtils.GetPKPropertyName(type);
            foreach (var propertyInfo in publicPIs)
            {
                object value = null;
                if (propertyInfo.Name == idPropertyname)
                    value = Convert.ChangeType(id, propertyInfo.PropertyType);
                else if (thisKeys.Contains(propertyInfo.Name))
                    continue;
                else if (propertyInfo.PropertyType == typeof(string))
                    value = propertyInfo.Name + id;

                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, value, new object[0]);

            }

            var foreignKeyPIs = associationPIs.Where(pi => pi.GetAttribute<AssociationAttribute>().IsForeignKey);

            foreach (var propertyInfo in foreignKeyPIs)
            {
                if (propertyInfo.PropertyType == type)
                    continue;
                var idPI = LinqToSqlUtils.GetPKPropertyInfo(propertyInfo.PropertyType);
                if (propertyInfo.CanWrite)
                    propertyInfo.SetValue(obj, GetObject(propertyInfo.PropertyType, 
                        GetDefaultId(idPI.PropertyType)), new object[0]);

            }

            return obj;
        }

        private static object GetDefaultId(Type type)
        {
            if (type == typeof(string))
                return "1";
            if(type == typeof(int))
                return 1;
            throw new Exception("Not support");
        }
    }
}