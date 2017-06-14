using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Web.Cms.ViewModel
{
    public class DiscountListVM
    {

        public Group Group { get; set; }

        public Student Student { get; set; }

        public decimal? GroupID { get; set; }

        public decimal? StudentID { get; set; }

        public string PriceType_TC { get; set; }

        public List<Discount> Discounts { get; set; }

//        public List<PriceType> PriceTypes { get; set; }
    }
}