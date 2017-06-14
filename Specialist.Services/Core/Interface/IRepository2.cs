using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Specialist.Web.Core.Utils;

namespace Specialist.Services.Core.Interface
{
    public interface IRepository2<T>
        where T : class
    {
        T GetByPK(object pk);
        IQueryable<T> GetByPK(IEnumerable<object> pkList);
        IQueryable<T> GetAll();
        void UpdateOrInsert(T obj);
        void Delete(T obj);
        void Insert(T obj);
        void SubmitChanges();
    	void EnableTracking();
    	void LoadWith(Action<DataLoadOptionsBuilder<T>> action);
    	void LoadWith(params Expression<Func<T,object>>[] selectors);
    	K GetValues<K>(object id, Expression<Func<T,K>> selector);
    }

    public static class IRepository2Mixin
    {

		public static IQueryable<T> GetAll<T>(this IRepository2<T> repository,
	Expression<Func<T, bool>> predicate)
	where T : class
		{
			return repository.GetAll().Where(predicate);
		}

		public static T FirstOrDefault<T>(this IRepository2<T> repository,
			Expression<Func<T, bool>> predicate)
			where T : class
		{
			return repository.GetAll().FirstOrDefault(predicate);
		}

		public static K FirstOrDefault<T,K>(this IRepository2<T> repository,
			Expression<Func<T, bool>> predicate, Expression<Func<T,K>> selector)
			where T : class
		{
			return repository.GetAll(predicate).Select(selector).FirstOrDefault();
		}

        public static void DeleteAndSubmit<T>(this IRepository2<T> repository, T obj) 
            where T : class
        {
            repository.Delete(obj);
            repository.SubmitChanges();
        }

        public static void DeleteAndSubmit<T>(this IRepository2<T> repository,
            IEnumerable<T> objList) where T : class
        {
            foreach (var obj in objList)
                repository.Delete(obj);
            repository.SubmitChanges();
        }
        public static void InsertAndSubmit<T>(this IRepository2<T> repository, T obj) 
            where T : class
        {
            repository.Insert(obj);
            repository.SubmitChanges();
        }
    }
}