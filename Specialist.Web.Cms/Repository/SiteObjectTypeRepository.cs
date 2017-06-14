using System;
using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Web.Cms.Repository
{
    public class SiteObjectTypeRepository: IRepository<SiteObjectType>
    {
        readonly List<SiteObjectType> _source = SiteObjectType.GetAll();
        public SiteObjectTypeRepository()
        {
        }

        public SiteObjectType GetByPK(object pk)
        {
            return _source.FirstOrDefault(t => t.SysName == (string) pk);
        }

        public IQueryable<SiteObjectType> GetByPK(IEnumerable<object> pkList)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SiteObjectType> GetAll()
        {
            return _source.AsQueryable();
        }

        public void UpdateOrInsert(SiteObjectType obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(SiteObjectType obj)
        {
            throw new NotImplementedException();
        }

        public void Insert(SiteObjectType obj)
        {
            throw new NotImplementedException();
        }

        public void InsertAndSubmit(SiteObjectType obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteAndSubmit(SiteObjectType obj)
        {
            throw new NotImplementedException();
        }

        public void SubmitChanges()
        {
            throw new NotImplementedException();
        }
    }
}