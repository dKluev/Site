using System.Collections.Generic;
using System.ComponentModel;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.ViewModel
{
    public class ContactsVM
    {
        [DisplayName("Домашний")]
        public string Phone { get; set; }

        [DisplayName("Мобильный")]
        public string Mobile { get; set; }

        [DisplayName("Рабочий")]
        public string WorkPhone { get; set; }

        public List<UserContact> Socials { get; set; }

    }
}