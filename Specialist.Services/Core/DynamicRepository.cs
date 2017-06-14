using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
using Specialist.Services.Core.Interface;

namespace Specialist.Web.Cms.Repository
{
    public class DynamicRepository
    {
        readonly IUnityContainer _container;

        public DynamicRepository(IUnityContainer container)
        {
            _container = container;
        }

        public IQueryable GetAll(Type type)
        {
            var repository = GetRepository(type);
            var getAllMethod = repository.GetType().GetMethod(MethodBase.GetCurrentMethod().Name);

            var items = getAllMethod.Invoke(repository, new object[] { });

            return (IQueryable) items;
        }

        public object GetByPK(Type type, object id)
        {
            var repository = GetRepository(type);

            var getByIdMethod = repository.GetType().GetMethod(
                MethodBase.GetCurrentMethod().Name, new [] {typeof(object)});

            var items = getByIdMethod.Invoke(repository, new [] { id });

            return items;
        }

        public IQueryable GetByPK(Type type, IEnumerable<object> idList)
        {
            var repository = GetRepository(type);

            var getByIdMethod = repository.GetType()
                .GetMethod(MethodBase.GetCurrentMethod().Name, 
                new [] {typeof(IEnumerable<object>)});

            var items = getByIdMethod.Invoke(repository, new[] { idList });

            return (IQueryable)items;
        }


        private object GetRepository(Type type)
        {
            var repositoryType = typeof(IRepository<>).MakeGenericType(new[] { type });
            return _container.Resolve(repositoryType);
        }
    }
}