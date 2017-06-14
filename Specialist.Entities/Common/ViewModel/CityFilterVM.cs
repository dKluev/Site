using System;
using System.Collections.Generic;

namespace Specialist.Entities.ViewModel
{
    public class CityFilterVM
    {
        public List<Context.City> Cities { get; set; }

        public string GlobalCityTC { get; set; }

        public bool IsMain { get; set; }
    }
}