using System.Collections.Generic;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.Const
{
    public static class PriceListTypes
    {
        public const char Common = 'Î';
      
        public const char Distance = 'Ä';

          public static char GetPriceListTypeTC(char prefix) {
              if(prefix == ' ')
                  return Common;
              return prefix;
          }
           /* foreach (var pair in _priceListTypeToCityTC)
            {
                if (pair.Value == cityTC)
                    return pair.Key;
            }
            return ' ';*/
    /*        var suffix = cities.First(x => x.City_TC == cityTC).Suffix;
            return suffix == string.Empty ? ' ' : suffix[0];
        }
//        public const char One = 'Ï';
//        public const char Four = 'Ð';
//        private static readonly Dictionary<char, string> _priceListTypeToCityTC;

//        static PriceListTypes()
//        {
//           _priceListTypeToCityTC = 
//                new Dictionary<char, string>
//                {
//                    {Common, Cities.Moscow},
//                    {Piter, Cities.Piter},
//                    {Rostov, Cities.Rostov},
//                };
//        }

   /*     public static char GetPriceListTypeTC(IEnumerable<City> cities, string cityTC)
        {*/
           /* foreach (var pair in _priceListTypeToCityTC)
            {
                if (pair.Value == cityTC)
                    return pair.Key;
            }
            return ' ';*/
    /*        var suffix = cities.First(x => x.City_TC == cityTC).Suffix;
            return suffix == string.Empty ? ' ' : suffix[0];
        }
*/
     /*   public static string GetCityTC(char priceListTypeTC)
        {
             foreach (var pair in _priceListTypeToCityTC)
            {
                if (pair.Key == priceListTypeTC)
                    return pair.Value;
            }
             return null;
        }*/
    }
}