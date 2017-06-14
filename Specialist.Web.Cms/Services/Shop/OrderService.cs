using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using System.Linq;

namespace Specialist.Web.Cms.Services
{
    public class OrderService : Repository<Order>
    {
        public OrderService(IContextProvider contextProvider) : base(contextProvider)
        {
        }

        public override System.Linq.IQueryable<Order> GetAll()
        {
            return base.GetAll().Where(o => o.UserID != null && !o.InPlan);
        }
    }
}