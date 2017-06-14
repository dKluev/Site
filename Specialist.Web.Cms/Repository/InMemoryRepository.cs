using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Services.Core.Interface;
using Specialist.Services.Common.Extension;

namespace Specialist.Web.Cms.Repository
{
    public class InMemoryRepository<T>: IRepository<T> where T : class
    {
        IQueryable<T> _source;

        public InMemoryRepository()
        {
            var type = typeof(T);
            Initialize(
                (IEnumerable<T>)type.GetMethod("GetAll").Invoke(null, new object[0]));

        }

        protected void Initialize(IEnumerable<T> source)
        {
            _source = source.AsQueryable();
        }

   


        public T GetByPK(object pk)
        {
            return _source.ByPrimeryKey(pk);
        }

        public IQueryable<T> GetByPK(IEnumerable<object> pkList)
        {
            return pkList.Select(pk => GetByPK(pk)).AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return _source;
        }

        public void UpdateOrInsert(T obj)
        {
            
        }


        public void Insert(T obj)
        {
            
        }

        public void SubmitChanges()
        {
            
        }

        public void Delete(T obj)
        {
            
        }

    }
}