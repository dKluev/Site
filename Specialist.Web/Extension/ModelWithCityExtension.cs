using System.Collections.Generic;
using System.Web.Mvc;
using Specialist.Entities.ViewModel;

namespace Specialist.Web.Extension
{
    public static class ModelWithCityExtension
    {
        public static List<SelectListItem> GetAllCity(this CityFilterVM model)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem{Text = "Все", Value = string.Empty});
            foreach (var city in model.Cities)
            {
                result.Add(new SelectListItem{Text = city.CityName, Value = city.City_TC});
            }
            return result;
        }

       /* public static List<SelectListItem> GetAllCity(this EditCourseVMExtension model)
        {
            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "Все", Value = string.Empty });
            foreach (var city in model.Cities)
            {
                result.Add(new SelectListItem { Text = city.Name, Value = city.City_TC });
            }
            return result;
        }*/
    }
}