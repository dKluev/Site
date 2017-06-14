using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Context;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile.ViewModel;

namespace Specialist.Entities.Profile
{
    public class EditProfileVM : IViewModel
    {
        private bool _isCompany;
        [UIHint(Controls.Hidden)]
        public bool IsCompany
        {
            get
            {
                if (User.IsCompany)
                    return true;
                return _isCompany;
            }
            set { _isCompany = value; }
        }



        public EditProfileVM()
        {
            User = new User();
            UserAddress = new UserAddress();
            Contacts = new ContactsVM();
        }

        public List<UserContactType> ContactTypes { get; set; }

        public User User { get; set; }

        public UserAddress UserAddress { get; set; }

        public bool HasPhoto { get; set; }      

        [DisplayName("Юридический адресс")]
        public string LegalAddress { get; set; }

        [DisplayName("Фото")]
        [Example("Примечание: файл в формате .jpg, размером не более 500 kb")]
        [UIHint(Controls.File)]
        public string Photo { get; set; }

        public List<Country> Countries { get; set; }

        public ContactsVM Contacts { get; set; }

        public Company Company
        {
            get
            {
                return User.Company;
            }
            set
            {
                User.Company = value;
            }
        }

        public string Title
        {
            get { return "Редактирование профиля"; }
        }
    }
}