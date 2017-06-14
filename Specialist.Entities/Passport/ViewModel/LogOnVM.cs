using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SimpleUtils.Common.Extensions;
using SimpleUtils.FluentAttributes.Const;
using Specialist.Entities.Catalog;
using Specialist.Entities.Catalog.Interface;

namespace Specialist.Entities.Passport.ViewModel
{
    public class LogOnVM: IViewModel
    {

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [UIHint(Controls.Password)]
        public string Password { get; set; }

        [DisplayName("Запомнить")]
        public bool Remeber { get; set; }

        [UIHint(Controls.Hidden)]
        public string ReturnUrl {get;set;}

        public LogOnVM() {
            Remeber = true;
        }

	    public bool IsReturnAddSeminar {
		    get {
			    if (CommonConst.IsForTest) {
				    return ReturnUrl.GetOrDefault(x => x.ToLower().StartsWith("/course/addseminar/"));
			    }
			    return false;
		    }
	    }

        /*     public LogOnVM()
        {
            InputEmail = this.GetInputName(x => x.Email);
            InputPassword = this.GetInputName(x => x.Password);
            InputRemeberMe = this.GetInputName(x => x.RemeberMe);
            InputReturnUrl = this.GetInputName(x => x.ReturnUrl);
            
        }

        public string InputEmail { get; set; }

        public string InputPassword { get; set; }

        public string InputRemeberMe { get; set; }

        public string InputReturnUrl { get; set; }*/


        public string Title {
            get { return "Вход в Ваш личный кабинет"; }
        }
    }
}