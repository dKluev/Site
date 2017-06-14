using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using SimpleUtils.Linq.Data.LInq;
using SimpleUtils.LinqToSql;
using SimpleUtils.Reflection;
using SimpleUtils.Util;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Web.Cms.Util.Mock
{
    public class MockRepository<T>: IRepository<T> where T : class
    {
      /*  MockExamSetRepository _es = new MockExamSetRepository();
        IRepository<T> Repository
        {
            get
            {
                if (typeof(T) == typeof(ExamSet))
                    return (IRepository<T>) _es;
                return null;
            }
        }*/
        public const int ItemCount = 100;
        private Type _type = typeof (T);
        public MockRepository()
        {
            if(_source != null)
                return;
            _source = new List<T>();
            for (int i = 0; i < ItemCount; i++)
            {
                _source.Add((T)LinqEntityMockGenerator.GetObject(_type, i));
            }
        }

        protected static List<T> _source;

        private int GetMaxId()
        {
            return _source.Max(x => Convert.ToInt32(LinqToSqlUtils.GetPK(x)));
        }


        public T GetByPK(object pk)
        {
            return _source.Where(x => LinqToSqlUtils.GetPK(x).Equals(pk)).FirstOrDefault();
        }

        public IQueryable<T> GetByPK(IEnumerable<object> pkList)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return _source.AsQueryable();
        }

        public void UpdateOrInsert(T obj)
        {
//            var id = LinqToSqlUtil.GetId(obj);
//            var oldObj = GetByID(id);

        }

        public void Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void InsertAndSubmit(T obj)
        {
            LinqToSqlUtils.SetPK(obj, GetMaxId() + 1);
            _source.Add(obj);
        }

        public void DeleteAndSubmit(T obj)
        {
            var id = LinqToSqlUtils.GetPK(obj);
            var oldObj = GetByPK(id);
            _source.Remove(oldObj);
        }

        public void SubmitChanges()
        {
            throw new NotImplementedException();
        }
    }
}