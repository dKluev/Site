using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using System.Linq;

namespace Specialist.Web.Cms.Services
{
    public class OrderDetailService : Repository<OrderDetail>
    {
        public OrderDetailService(IContextProvider contextProvider) : base(contextProvider)
        {
        }

        public override System.Linq.IQueryable<OrderDetail> GetAll()
        {
            return base.GetAll().Where(od => od.Order.UserID != null && !od.Order.InPlan);
        }
    }
}