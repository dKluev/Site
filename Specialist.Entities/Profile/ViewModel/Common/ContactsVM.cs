using System.Collections.Generic;
using System.ComponentModel;
using Specialist.Entities.Passport;

namespace Specialist.Entities.Profile.ViewModel
{
    public class ContactsVM
    {
        [DisplayName("��������")]
        public string Phone { get; set; }

        [DisplayName("���������")]
        public string Mobile { get; set; }

        [DisplayName("�������")]
        public string WorkPhone { get; set; }

        public List<UserContact> Socials { get; set; }

    }
}