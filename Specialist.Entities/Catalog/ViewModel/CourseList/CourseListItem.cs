using System;
using System.Collections.Generic;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Extension;
using Specialist.Entities.Context;
using Specialist.Entities.Interface;
using Specialist.Entities.Context.Extension;

namespace Specialist.Entities.ViewModel
{
    public class CourseListItem: ICourse
    {
        public Course Course { get; set; }

        public List<PriceView> Prices { get; set; }

	    public decimal? PriceCoefficent { get; set; }

        public decimal? GetPrice(string priceTypeTC)
        {
            return Prices.GetPrice(priceTypeTC);
        }

        public Group NearestGroup { get; set; }

        public DateTime? NearestDate
        {
            get
            {
                return NearestGroup.GetOrDefault(g => g.DateBeg);
            }
        }

        public short? Discount
        {
            get
            {
                return NearestGroup.GetOrDefault(g => g.Discount);
            }
        } 

        public string NearestGroupCity
        {
            get
            {
                if (NearestGroup == null)
                    return null;
                if(NearestGroup.BranchOffice.City == null)
                    return null;
                return NearestGroup.BranchOffice.City.CityName;
            }
        }

    }
}