using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using FluentValidation; using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using Specialist.Entities.Passport;
using Specialist.Entities.Passport.ViewModel;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Common.Html;
using Specialist.Web.Common.Validators.Core;
using Specialist.Web.Common.Validators.Profile;
using Specialist.Web.ViewModel.Orders;

namespace Specialist.Web.Validators
{
    public class SimpleRegUserValidator : ExValidator<SimpleRegUser>
    {
        [Dependency]
        public IUserService UserService { get; set; }

        protected override void Init()
        {
            RuleFor(x => x.Name).NotEmpty();
        	RuleFor(x => x.Email).Must(x => x.IsEmpty() || !Regex.IsMatch(x, "[а-я]"))
				.WithMessage("E-mail не должен содержать кириллицу");
            RuleFor(x => x.Email).EmailAddress().NotEmpty()
                .Must(EmailNotUse).WithMessage("Данный E-mail уже используется, " +
             H.Anchor("/profile/restorepassword?email={0}", "восстановить пароль").Class("open-in-dialog"),
			 x => x.Email);
        }


        private bool EmailNotUse(string email)
        {
            if(email.IsEmpty())
                return true;
            var user = UserService.GetByEmail(email);
            return user == null;
        }
    }
}