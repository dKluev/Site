using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace Specialist.Entities.Context.Logic
{
    public class OrderSeparator
    {
        public OrderSeparator(Order order)
        {
            Order = order;
        }

        public List<OrderDetail> CourseOrderDetails
        {
            get {
            	var orderDetails = Order.OrderDetails;
            	return CourseDetails(orderDetails.AsQueryable()).ToList();
            }
        }
        public List<OrderDetail> CourseAndTrackCourseOrderDetails
        {
            get {
            	return Order.OrderDetails.Where(x => !x.IsTestCert).ToList();
            }
        }

    	public static IQueryable<OrderDetail> CourseDetails(IQueryable<OrderDetail> orderDetails) {
    		return orderDetails.Where(od => od.Track_TC == null && od.UserTestId == null);
    	}

    	public List<OrderDetail> TestCerts
        {
            get
            {
                return Order.OrderDetails.Where(od => od.IsTestCert).ToList();
            }
        }

        public bool HasDiscount
        {
            get
            {
                return Order.TotalPrice != Order.TotalPriceWithDescount;
            }
        }

        public List<OrderTrack> Tracks
        {
            get
            {
                var tracks =
                    from detail in Order.OrderDetails
                    where detail.Track_TC != null
                    group detail by detail.Track_TC into gr
                        select
                            new OrderTrack
                            {
                                OrderDetails = gr.ToList()
                            };
                return tracks.ToList();
            }
        }

        public Order Order  { get; set;}
    }
}