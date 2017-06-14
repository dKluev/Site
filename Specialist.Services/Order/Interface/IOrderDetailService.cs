using System;
using System.Collections.Generic;
using Specialist.Entities.Context;
using Specialist.Services.Core.Interface;

namespace Specialist.Services.Interface.Order
{
    public interface IOrderDetailService: IRepository<OrderDetail>
    {
      /*   OrderDetail GetOrderDetailByID(decimal ID);
//         List<OrderDetail> GetOrderDetailsBySession(Guid sessionID);
        List<OrderTrack> GetOrderTracksBySession (Guid sessionID);
        OrderTrack GetOrderTrack (int ID);
        List<OrderDetail> GetOrderDetailsForOrder(decimal OrderID);*/
    } 
}
