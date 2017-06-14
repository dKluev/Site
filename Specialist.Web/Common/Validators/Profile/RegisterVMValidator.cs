using System;
using System.Web.UI.WebControls;
using FluentValidation; using Microsoft.Practices.Unity;
using Specialist.Entities.Passport;
using Specialist.Entities.Passport.ViewModel;
using Specialist.Web.Common.Validators.Core;
using Specialist.Web.Common.Validators.Profile;

namespace Specialist.Web.Validators
{
    public class RegisterVMValidator : ExValidator<RegisterVM>
    {
        public class PhoneValidator : ExValidator<PhoneVM>
        {
            protected override void Init()
            {
                RuleFor(x => x.Phone).NotEmpty()/*.Matches(@"^\(\d{2,7}\)\d{4,9}$")*/;
            }
        }

        public class AddressValidator : ExValidator<UserAddress>
        {
            protected override void Init()
            {
                RuleFor(x => x.City).NotEmpty().Length(0,50);
            	RuleFor(x => x.Address).Length(0, 300);
            }
        }

	    public const string PersonalDataMessage = "Необходимо ваше согласие на обработку персональных данных";

        protected override void Init()
        {
            RuleFor(x => x.CaptchaText).Must((m, x) => m.CaptchaValid)
                .WithMessage("Неверно введены цифры с картинки");
            RuleFor(x => x.PersonalData).Must(x => x)
                .WithMessage(PersonalDataMessage);

//	        RuleFor(x => x.Year).Must(x => !x.HasValue || (x >= 1900 && x <= DateTime.Today.Year))
//		        .WithMessage("Год должен быть между 1900 и " + DateTime.Today.Year);
           
            RuleFor(x => x.Phone).SetValidator(
                UnityContainer.Resolve<PhoneValidator>());

            RuleFor(x => x.UserAddress).SetValidator(
                UnityContainer.Resolve<AddressValidator>());

            RuleFor(x => x.User).SetValidator(
                UnityContainer.Resolve<UserRegisterValidator>());

         

        }
    }
}