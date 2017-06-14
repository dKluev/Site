using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Services.Interface.Order
{
    public interface IDiscountService
    {
     /*   Discount GetMaxDiscountForCourse(string courseTC);
        Discount GetMaxDiscountForGroup(decimal groupID);*/
        List<Discount> GetDiscountsFor(OrderDetail orderDetail);
        List<Discount> GetDiscountsFor(OrderDetail orderDetail, Student student);
//	    bool IsFriendPromocode(string promocode);
    }
}