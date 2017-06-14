using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Tests.Consts;
using Specialist.Services.Core.Interface;
using Specialist.Services.Common.Extension;

namespace Specialist.Web.Cms.Repository
{
    public class NamedIdRepository<T>: IRepository<T> where T :BaseNamedId<T>, new() {
    
        public T GetByPK(object pk) {
        	return NamedIdCache<T>.Dict[Convert.ToInt32(pk)];
        }

        public IQueryable<T> GetByPK(IEnumerable<object> pkList)
        {
            return pkList.Select(GetByPK).AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return NamedIdCache<T>.List.AsQueryable();
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