using System;
using System.Collections.Generic;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;

namespace Specialist.Entities.Center.ViewModel
{
    public class CityVM:IViewModel
    {
        public City City { get; set; }

        public List<Complex> Complexes { get; set; }

    	public SimplePage Locations { get; set; }

    	public List<City> Cities { get; set; }

    	public string Title {
    		get { return City.CustomName; }
    	}

    	public List<Group> Groups { get; set; }
    }
}