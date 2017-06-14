using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq.Expressions;
using SimpleUtils.Collections.Extensions;
using SimpleUtils.Common;
using SimpleUtils.Reflection;
using System.Linq;
using SimpleUtils.Reflection.Extensions;

namespace SimpleUtils.Extension
{
    public static class GenericExtension
    {
		public static bool IsNull<T>(this T obj) where T : class {
			return obj == null;
		}

		public static bool IsNull<T>(this T? obj) where T : struct {
			return obj == null;
		}
		public static T FluentUpdate<T>(this T obj, Action<T> action) {
			action(obj);
			return obj;
		}

		public static K If<T,K>(this T obj, bool condition, Func<T, K> func)
			where K:class
		{
			return condition ? func(obj) : null;
		}

		public static K If<T,K>(this T obj, Func<T,bool> condition, Func<T, K> func)
			where K:class
		{
			return condition(obj) ? func(obj) : null;
		}

    	public static Tr IfNotNull<Te, Tr>(this Te? obj, Func<Te?, Tr> func)
    		where Te:struct {
			if (!obj.HasValue)
				return default(Tr);
			return func(obj.Value);
		}

		public static Tr IfNotNull<Te, Tr>(this Te obj, Func<Te, Tr> func) where Te : class
		{
			if (obj == null)
				return default(Tr);
			return func(obj);
		}


		public static Te IfNull<Te>(this Te obj, Func<Te> func) where Te : class
		{
			if (obj != null)
				return obj;
			return func();
		}

		public static Tr IfAny<Te, Tr>(this Te obj, Func<Te, Tr> func)
			where Te : IEnumerable<object>
		{
			if (obj == null || !obj.Any())
			{
				return default(Tr);
			}
			return func(obj);
		}

		public static object IfAnyEach<Te>(this IEnumerable<Te> obj, object title,
			Func<Te, object> func)
		{
			if (obj == null || !obj.Any())
			{
				return null;
			}
			return new List<object> { title }.AddFluent(obj.Select(func));
		}



        public static void Update<T>(this T item, T source, 
            params Expression<Func<T, object>>[] properties)
        {
            foreach (var func in properties)
            {
                var propertyName = ExpressionUtils.GetPropertyName(func);
                item.SetValue(source.GetValue(propertyName), propertyName);
            }
        }

        public static void UpdateByMeta<T>(this T item, T source,
            params Expression<Func<T, object>>[] exept)
        {
            var exeptProperties = exept.Select(e =>
                ExpressionUtils.GetPropertyName(e)).ToList();
            foreach (var property in item.GetType().GetProperties())
            {
                if(exeptProperties.Contains(property.Name))
                    continue;
                var isMapping =
                    property.HasAttribute(typeof(ColumnAttribute));
                if (property.CanWrite && isMapping)
                    property.SetValue(item, property.GetValue(source));
            }
        }

    }
}