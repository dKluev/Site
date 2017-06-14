using System.Collections.Generic;
using System.Linq;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Catalog.Interface
{
    public interface INewsService:IRepository<News>
    {
        IQueryable<News> GetAllForMain();
        List<News> GetFor(object entity);
    }
}