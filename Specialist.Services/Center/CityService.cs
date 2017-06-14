using System;
using System.Collections.Generic;
using System.Data.Linq;
using Specialist.Entities.Const;
using Specialist.Entities.Context;
using System.Linq;
using Specialist.Services.Core;
using Specialist.Services.Core.Interface;
using Specialist.Services.Interface.Center;
using Specialist.Services.UnityInterception;
using Specialist.Services.Common.Extension;

namespace Specialist.Services.Center
{
    public class CityService: Repository<City>, ICityService
    {
        public CityService() : base(new ContextProvider())
        {
        }

        

        [Cached]
        public override IQueryable<City> GetAll()
        {
            var loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<City>(c => c.CitiesPriceTypes);
            context.LoadOptions = loadOptions;
            return base.GetAll().OrderBy(c => c.SortOrder)
				.Where(c => !Cities.Disable.Contains(c.City_TC));
        }

        [Cached]
        public virtual Dictionary<string, char> GetPrefixList() {
            var result = new Dictionary<string, char>();
            foreach (var city in GetAll()) {
                var citiesPriceType = city.CitiesPriceTypes.ToList()
                    .FirstOrDefault(cpt => char.IsNumber(cpt.PriceType_TC.First()));

                var prefix = ' ';
                if (citiesPriceType != null)
                    prefix = citiesPriceType.PriceType_TC.First();
                result.Add(city.City_TC, prefix);
            }
            return result;
        }

        public char GetCityPrefix(string cityTC)
        {
            return GetPrefixList()[cityTC];
        }
    }
}