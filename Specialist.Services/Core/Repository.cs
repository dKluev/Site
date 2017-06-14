using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection.Extensions;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;
using System.Linq.Dynamic;
using SimpleUtils.Reflection;
using SimpleUtils.Extension;
using SimpleUtils;

namespace Specialist.Services.Core
{
    public class Repository<T>: IRepository<T> where T : class
    {
        Type _type =typeof(T);
        protected DataContext context;

        public DataContext Context { get { return context; } set { context = value; } }

        public Repository(IContextProvider contextProvider)
        {
            this.context = contextProvider.Get(_type);
            if(context != null)
                context.Log = new StringWriter();
        }

        public virtual T GetByPK(object pk)
        {
            /*return context.GetTable<T>().Where(LinqToSqlUtil.GetIdPropertyName(_type) + 
                " = @0", id).FirstOrDefault();*/
            var itemParameter = Expression.Parameter(_type, "item");

            var whereExpression = Expression.Lambda<Func<T, bool>>
                (
                    Expression.Equal(
                        Expression.Property(
                            itemParameter,
                            LinqToSqlUtils.GetPKPropertyName(_type)
                        ),
                        Expression.Constant(pk)
                    ),
                    new[] { itemParameter }
                );

            return GetAll().FirstOrDefault(whereExpression);
            
        }

        public virtual IQueryable<T> GetByPK(IEnumerable<object> pkList)
        {
			if (!pkList.Any()) {
				return Enumerable.Empty<T>().AsQueryable();
			}

            var idPropertyName = LinqToSqlUtils.GetPKPropertyName(_type);
            var where = pkList.Select((pk, index) => 
                string.Format(" {0} == @{1} ", idPropertyName, index))
                .JoinWith(" || ");
            
            return GetAll().Where(where, pkList.ToArray());
                
//            return GetAll().Where(LinqToSqlUtil.WhereContains<T>(pkList.AsQueryable()));

        }


        public virtual IQueryable<T> GetAll()
        {
            return context.GetTable<T>();
        }

       


        public virtual void UpdateOrInsert(T obj)
        {
            var id = LinqToSqlUtils.GetPK(obj);
            if(id.Equals(LinqToSqlUtils.GetPKPropertyInfo(_type).PropertyType.Default()))
            {
                this.InsertAndSubmit(obj);
                return;
            }
            var oldOjb = GetByPK(id);
            oldOjb.UpdateByMeta(obj);
            context.SubmitChanges();

        }

      


        public void Insert(T obj)
        {
            context.GetTable<T>().InsertOnSubmit(obj);
        }


        public void Delete(T obj)
        {
            var id = LinqToSqlUtils.GetPK(obj);
            context.GetTable<T>().DeleteOnSubmit(GetByPK(id));
        }

        public void SubmitChanges()
        {
            context.SubmitChanges();
        }
    }
}