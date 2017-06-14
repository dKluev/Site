using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Specialist.Entities.Context;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Order;
using Specialist.Services.Interface;

namespace Specialist.Services.Order
{
    public class OrderDetailService : Repository<OrderDetail>, IOrderDetailService
    {
        public OrderDetailService(IContextProvider contextProvider) : base(contextProvider) {}

      /*  [Dependency]
        public ICourseService CourseService { get; set; }

        [Dependency]
        public IGroupService GroupService {get; set;}

        [Dependency]
        public IPriceService PriceService { get; set; }

        [Dependency]
        public IDiscountService DiscountService { get; set; }

        public OrderDetail GetOrderDetailByID(decimal ID)
        {
            var context = new OrderDataContext();
            var orderDetail = context.OrderDetails.
                Where(c => c.OrderDetailID == ID).FirstOrDefault();
//            orderDetail.Course = CourseService.GetByCourseTC(orderDetail.Course_TC);
//            PriceTypeTC = orderDetail.PriceType_TC;
//            orderDetail.PriceV = PriceService.GetAllPricesForCourse(orderDetail.Course_TC, null).FirstOrDefault(p => p.PriceType_TC == PriceTypeTC);
//            orderDetail.FirstPrice = orderDetail.PriceV.Price;
            if (orderDetail.Order.NumberOfStudents != null)
            {
                var num = Convert.ToInt32(orderDetail.Order.NumberOfStudents);
//                orderDetail.FirstPrice = orderDetail.FirstPrice*num;
            }
            if (orderDetail.Group_ID !=null)
            {
                orderDetail.Group = GroupService.GetGroupByID(orderDetail.Group_ID);
            }
            orderDetail.Discounts = DiscountService.GetDiscountsFor(orderDetail);
            return orderDetail;

        }

      

        public List<OrderDetail> GetOrderDetailsForTrackByID(int ID)
        {
            var context = new OrderDataContext();
            var orderDetail = context.OrderDetails.
                Where(c => c.OrderDetailID == ID).FirstOrDefault();
            if (orderDetail.Track_TC!=null)
            {
                var details = new List<OrderDetail>();
                var orderDetails = context.OrderDetails.
                    Where(c => c.Track_TC == orderDetail.Track_TC).OrderBy(
                    c => c.OrderDetailID).ToList();
                foreach (var od in orderDetails)
                {
                    details.Add(GetOrderDetailByID(Convert.ToInt32(od.OrderDetailID)));
                }
                return details;
            }
            return null;
        }*/

      /*  public OrderTrack GetOrderTrack (int ID)
        {
            var context = new OrderDataContext();
            var orderDetail = context.OrderDetails.
                Where(c => c.OrderDetailID == ID).FirstOrDefault();
            if (orderDetail.Track_TC != null)
            {
                var track = CourseService.GetByCourseTC(orderDetail.Track_TC);
                var courses = GetOrderDetailsForTrackByID(Convert.ToInt32(orderDetail.OrderDetailID));
                var ordertrack = new OrderTrack
                                     {
                                         OrderDetails = courses,
                                     };
                return ordertrack;
            }
            return null;
        }*/

   /*     public List<OrderTrack> GetOrderTracksBySession (Guid sessionID)
        {
            var context = new OrderDataContext();
            var orderDetails = context.OrderDetails.
                Where(c => c.Order.SessionID == sessionID && c.Track_TC!=null).ToList();
            var orderTrack = new List<OrderTrack>();
            foreach (var detail in orderDetails)
            {
                orderTrack.Add(GetOrderTrack(Convert.ToInt32(detail.OrderDetailID)));
            }
            return orderTrack;
        }*/

    /*    public List<OrderDetail> GetOrderDetailsBySession(Guid sessionID)
        {
            var context = new OrderDataContext();
            var orderDetails = context.OrderDetails.
                Where(c => c.Order.SessionID == sessionID).ToList();
            foreach (OrderDetail detail in orderDetails)
            {
                detail.Course = CourseService.GetByCourseTC(detail.Course_TC);

                if (detail.GroupID !=null)
            {
                detail.Group = GroupService.GetGroupByID(detail.GroupID);
            }
                PriceTypeTC = detail.PriceType_TC;
                detail.PriceType = PriceService.GetAllPricesForCourse(detail.Course_TC, null).
                    FirstOrDefault(p => p.PriceType_TC == PriceTypeTC);
                if(detail.PriceType != null)
                    detail.FirstPrice = detail.PriceType.Price;
                if (detail.Order.NumberOfStudents.HasValue)
                {
                    var num = detail.Order.NumberOfStudents.Value;
                    detail.FirstPrice = detail.FirstPrice * num;
                }
            }         

            return orderDetails;
        }*/

    /*    public List<OrderDetail> GetOrderDetailsForOrder(decimal OrderID)
        {
            var context = new OrderDataContext();
            var OrderDetails = new List<OrderDetail>();
            var Details = context.OrderDetails.Where
                (x => x.OrderID == OrderID).ToList();
            foreach (var detail in Details)
            {
                OrderDetails.Add(GetOrderDetailByID(Convert.ToInt32(detail.OrderDetailID)));
            }
            return OrderDetails;
        }*/

    }
}
