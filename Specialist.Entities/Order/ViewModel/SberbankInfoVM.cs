using System.Collections.Generic;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.ViewModel;

namespace Specialist.Entities.Context.ViewModel
{
    public class SberbankInfoVM
    {
        public UserAddress UserAddress { get; set; }

        public ContactsVM Contacts { get; set; }

        public decimal OrderID { get; set; }

    	public Employee Manager { get; set; }
    }
}