using System;
using System.Text.RegularExpressions;
using FluentValidation; using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Passport;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Validators.Core;
using SimpleUtils;

namespace Specialist.Web.Common.Validators.Profile
{
    public class UserRegisterValidator: ExValidator<User>
    {
        [Dependency]
        public IUserService UserService { get; set; }

        public class CompanyValidator : ExValidator<Company>
        {
            protected override void Init()
            {
                RuleFor(x => x.CompanyName).NotEmpty().Length(1, 200);
                RuleFor(x => x.INN).NotEmpty().Length(1, 50);
				RuleFor(x => x.KPP).NotEmpty().Length(1, 50);
				RuleFor(x => x.LegalAddress).NotEmpty().Length(1,500);
            }
        }

        protected override void Init() {
	        var minYear = DateTime.Now.Year - 70;
	        var maxYear = DateTime.Now.Year - 10;
            RuleFor(x => x.LastName).NotEmpty().Length(1,50);
            RuleFor(x => x.FirstName).NotEmpty().Length(1,50);
            RuleFor(x => x.SecondName).NotEmpty().Length(1,50);
	        RuleFor(x => x.BirthDate).NotEmpty().Must(x => x.HasValue && (x.Value.Year >= minYear && x.Value.Year <= maxYear))
		        .WithMessage("√од должен быть между {0} и {1}".FormatWith(minYear,maxYear));
            RuleFor(x => x.Source_ID).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().Length(6, 20);
        	RuleFor(x => x.Email).Must(x => x.IsEmpty() || !Regex.IsMatch(x, "[а-€]"))
				.WithMessage("Email не должен содержать кириллицу");
            RuleFor(x => x.Email).EmailAddress().NotEmpty()
                .Must(EmailNotUse).WithMessage("ƒанный Email уже зарегистрирован на сайте, " +
             H.Anchor("/profile/restorepassword?email={0}", "¬ы можете восстановить пароль").OpenInDialog(),
			 x => x.Email);
            RuleFor(x => x.Company).SetValidator(
                UnityContainer.Resolve<CompanyValidator>());
        }

        private bool EmailNotUse(string email)
        {
            if(email.IsEmpty())
                return true;
	        if (StringUtils.IsSpecEmail(email))
		        return false;
            var user = UserService.GetByEmail(email);
            return user == null;
        }

         
    }
}