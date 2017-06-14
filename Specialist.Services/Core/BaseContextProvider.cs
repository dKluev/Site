using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Core
{
    public class BaseContextProvider : IContextProvider
    {
        protected static readonly Dictionary<Type, List<Type>> _contextList;
        static BaseContextProvider()
        {
            _contextList = new Dictionary<Type, List<Type>>();
            var contextTypes = typeof(SpecialistDataContext).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(DataContext)));
            foreach (var contextType in contextTypes)
            {
                var entityTypes = new List<Type>();
                foreach (var info in contextType.GetProperties())
                {
                    if (info.PropertyType.IsGenericType &&
                        info.PropertyType.GetGenericTypeDefinition() == typeof(Table<>))
                    {
                        var entityType = info.PropertyType.GetGenericArguments()[0];
                        entityTypes.Add(entityType);
                    }
                }
                _contextList.Add(contextType, entityTypes);
            }
        }

        public Type GetTypeByTableName(string tableName)
        {
            return _contextList.SelectMany(pair => pair.Value)
                .FirstOrDefault(t => LinqToSqlUtils.GetTableName(t) == tableName);
        }

        public virtual DataContext Get(Type entityType)
        {
            var context = (DataContext)_contextList.FirstOrDefault(
                pair => pair.Value.Contains(entityType)).Key.Create();
            return context;
        }



    }
}