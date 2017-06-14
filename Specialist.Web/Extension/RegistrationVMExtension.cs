using System.Collections.Generic;
using System.Web.Mvc;
using Specialist.Entities.Context.ViewModel;

namespace Specialist.Web.Extension
{
    public static class RegistrationVMExtension
    {

        public static List<SelectListItem> GetCountriesList(
              this RegistrationVM registrationVM)
        {
            var result = new List<SelectListItem>();
            var countries = registrationVM.Countries;
            var checkd = false;
            foreach (var country in countries)
            {
                if (registrationVM.CountryID != 0)
                {
                    if (country.Country_ID == registrationVM.CountryID)
                    {
                        checkd = true;
                    }
                }
                else
                {
                    if (country.CountryName == "Российская Федерация") checkd = true;
                }
                result.Add(new SelectListItem { Selected = checkd, Text = country.CountryName, Value = country.Country_ID.ToString() });
                checkd = false;
            }
            return result;
        }

        public static  List<SelectListItem> GetSourcesList(
            this RegistrationVM registrationVM)
        {
            var result = new List<SelectListItem>();
            var sources = registrationVM.Sources;
            foreach (var source in sources)
            {
                result.Add(new SelectListItem{Text = source.Name, Value = source.Source_TC});
            }
            return result;
        }

    }
}
