using System.Collections.Generic;
using Specialist.Entities.Context;

namespace Specialist.Entities.Context.ViewModel
{
    public class RegistrationVM
    {
        public Order Order { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public decimal CountryID { get; set; }

        public string Source { get; set; }

        public string Index { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string PhisicalAddress { get; set; }

        public string Fax { get; set; }

        public string UrAddress { get; set; }

        public List<Country> Countries { get; set; }

        public List<Source> Sources { get; set; }
    }
}