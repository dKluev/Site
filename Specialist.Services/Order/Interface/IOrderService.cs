using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Interface.Order
{
    public interface IOrderService: IRepository<Entities.Context.Order>
    {
        Entities.Context.Order GetCurrentOrder(bool inPlan = false);
        void UpdateSessionOrderUser();
        Entities.Context.Order GetOrCreateCurrentOrder();
        Entities.Context.Order GetOrCreatePlanOrder();
        Entities.Context.Order GetByPKAndUserID(decimal orderID, int userID);
        string GetPriceTypeForGroup(Group group, bool isBusiness, string customerType);
	    Employee GetUserManagerTC(User user);
        bool ExistGroup(User user);
        Entities.Context.Order GetByCommonId(string commonOrderId);
	    Entities.Context.Order CreateCurrentOrder(bool inPlan);
    }
}
