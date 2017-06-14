using System.Collections.Generic;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.Context
{
    public class OrderTrack
    {
        public List<OrderDetail> OrderDetails { get; set; }

        public Course Track { get { return OrderDetails.First().Track; } }

        public string PriceTypeTC { get { return OrderDetails.First().PriceType_TC; } }

        public decimal Price {get { return OrderDetails.Sum(od => od.Price);}}
    }
}