using System.Collections.Generic;
using SimpleUtils.Extension;
using Specialist.Entities.Context;
using System.Linq;

namespace Specialist.Entities.Profile.Const
{
    public static class ContactTypes
    {
        public const int Address = 1;//Почтовый адрес
        public const int Phone = 2;//Телефон
        public const int Fax = 3;//Факс
        public const int LegalAddress = 4;//Юридический адрес
        public const int Mobile = 5;//Мобильный телефон
        public const int Site = 6;//Site
        public const int Email = 7;//Email
        public const int WorkPhone = 8;
        public const int Twitter = 9;
        public const int VKontakte = 10;
        public const int Odnoklassniki = 11;
        public const int LiveJournal = 12;
        public const int Icq = 13;
        public const int Skype = 14;
        public const int LiveMessenger = 15;
        public const int Facebook = 16;

        public static class Specialist {
            public const decimal Site = 2;
            public const decimal Icq = 3;
            public const decimal Skype = 5;
            public const decimal Phone = 13;
            
        }


        public static class PhoneTypeTC
        {
            public const char Home = 'Д';
            public const char Work = 'Р';
            public const char Mobile = 'М';
        }

        public static char? GetPhoneTypeTC(int contactType) {
            switch (contactType) {
                case Mobile:
                    return PhoneTypeTC.Mobile;
                case WorkPhone:
                    return PhoneTypeTC.Work;
                case Phone:
                    return PhoneTypeTC.Home;
                default:
                    return null;
            }
        }

        public static class RegExp
        {
            public const string Twitter = @"^twitter\.com.*";
            public const string VKontakte = @"^vk\.com.*";
            public const string Facebook = @"^facebook\.com.*";
            public const string Odnoklassniki = @"^ok.ru.*";
            public const string LiveJournal = @"[\w-]*.livejournal.com";
            public const string Icq = @"^[0-9]{5,9}$";
            public const string Skype = @"[\w-.]+";
        }

        public static string GetName(int contactType) {
            foreach (var field in typeof(ContactTypes).GetFields()) {
                if ((int)field.GetValue(null) == contactType)
                    return field.Name;
            }
            return string.Empty;
        }


        public static string GetRegExp(int contactType)
        {
            foreach (var field in typeof(ContactTypes).GetFields())
            {
                if ((int)field.GetValue(null) == contactType) {
                    var fieldInfo = typeof(RegExp).GetField(field.Name);
                    if(fieldInfo == null)
                        return string.Empty;
                    return fieldInfo.GetValue(null).ToString();
                }
            }
            return string.Empty;
        }

        public static List<int> Phones() {
            return
                new List<int>
                {
                    Phone, WorkPhone, Mobile
                };
        }

        public static List<int> GetAllSocialServices()
        {
            return 
                new List<int>
                {
                    VKontakte, Facebook, Odnoklassniki, Twitter, LiveJournal, Skype, 
                };
        }



        public static List<int> ForProfile()
        {
            return 
                new List<int>
                {
                    Phone, Mobile, Site, Email, WorkPhone
                };
            
        }

        static Dictionary<decimal, int> SpecialistIdMapper = 
            new Dictionary<decimal, int> {
            {3, Icq},
            {5, Skype},
            {6, LiveMessenger},
            {Specialist.Phone, Phone},
        };

        static Dictionary<string, int> SpecialistValueMapper =
            new Dictionary<string, int> {
            {"livejournal", LiveJournal},
            {"vkontakte", VKontakte},
            {"twitter", Twitter},
            {"facebook", Facebook},
        };

        public static int? ConvertSpecialist(EmployeeContact contact) {
            decimal contactType = contact.ContactType_ID;
            if (contactType == Specialist.Site)
            {
                var pair = SpecialistValueMapper.FirstOrDefault(p =>
                    contact.ContactValue.Contains(p.Key));
                if (pair.Key != null)
                    return pair.Value;
            }
            if (SpecialistIdMapper.ContainsKey(contactType))
                return SpecialistIdMapper[contactType];

            return null;
        }
    }
}