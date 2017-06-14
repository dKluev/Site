using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Specialist.Entities.Utils;

namespace Specialist.Services.Core.Interface
{
    public interface IRepository<T>
        where T : class
    {
        T GetByPK(object pk);
        IQueryable<T> GetByPK(IEnumerable<object> pkList);
        IQueryable<T> GetAll();
        void UpdateOrInsert(T obj);
        void Delete(T obj);
        void Insert(T obj);
        void SubmitChanges();
    }

    public static class IRepositoryMixin
    {

		public static IQueryable<T> GetAll<T>(this IRepository<T> repository,
	Expression<Func<T, bool>> predicate)
	where T : class
		{
			return repository.GetAll().Where(predicate);
		}

		public static T FirstOrDefault<T>(this IRepository<T> repository,
			Expression<Func<T, bool>> predicate)
			where T : class
		{
			return repository.GetAll().FirstOrDefault(predicate);
		}


		public static K GetValues<T,K>(this IRepository<T> repository,object id, 
			Expression<Func<T,K>> selector) where T : class {
			return repository.GetByPK(_.List(id)).Select(selector).FirstOrDefault();
		}

        public static void DeleteAndSubmit<T>(this IRepository<T> repository, T obj) 
            where T : class
        {
            repository.Delete(obj);
            repository.SubmitChanges();
        }

        public static void DeleteAndSubmit<T>(this IRepository<T> repository,
            IEnumerable<T> objList) where T : class
        {
            foreach (var obj in objList)
                repository.Delete(obj);
            repository.SubmitChanges();
        }
        public static void InsertAndSubmit<T>(this IRepository<T> repository, T obj) 
            where T : class
        {
            repository.Insert(obj);
            repository.SubmitChanges();
        }
    }
}