using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using SimpleUtils.FluentAttributes.Attributes;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog.Interface;
using Specialist.Entities.Const;
using Specialist.Entities.Context;

namespace Specialist.Entities.Passport.ViewModel
{
    public class RegisterVM: IViewModel
    {

        public User User { get; set; }

//        [DisplayName("Дата рождения")]
//        [Example("Пример: 20.01.1980")]
//		public string Birthday { get; set; }

        public UserAddress UserAddress { get; set; }

        public List<Country> Countries { get; set; }
        public List<WorkBranch> WorkBranches { get; set; }

        public List<Source> Sources { get; set; }

	    public Dictionary<decimal,List<object>>  Metiers { get; set; }

        [DisplayName("Телефон")]
        public PhoneVM Phone { get; set; }

        [DisplayName("Согласие на обработку персональных данных")]
        public bool PersonalData { get; set; }

//	    public DateTime? GetBirthday {
//		    get { return DateTime.ParseExact(Birthday, "dd.MM.yyyy", CultureInfo.InvariantCulture); }
//	    }

	    public RegisterVM()
        {

            User = new User();
            User.Company = new Company();
            Phone = new PhoneVM();
            UserAddress = new UserAddress();
        }


        [UIHint(Controls.Captcha)]
        [DisplayName("Введите число с картинки")]
        public string CaptchaText { get; set; }

        public bool CaptchaValid { get; set; }

        [UIHint(Controls.Hidden)]
        public string NextUrl { get; set; }

        [UIHint(Controls.Hidden)]
        public string CustomerType { get; set; }

        public bool IsCompany { get
        {
            return CustomerType == OrderCustomerType.Organization;
        }}

        public string Title {
            get { return "Регистрация нового пользователя"; }
        }
    }
}