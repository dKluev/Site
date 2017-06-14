using System.Drawing;
using FluentValidation; using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity;
using SimpleUtils.Common.Extensions;
using SimpleUtils.Utils;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Common.Validators.Core;
using SimpleUtils;

namespace Specialist.Web.Common.Validators.Profile
{
    public class ChangePasswordVMValidator: ExValidator<ChangePasswordVM>
    {
        [Dependency]
        public IUserService UserService { get; set; }

        [Dependency]
        public IAuthService AuthService { get; set; }

        protected override void Init()
        {
            RuleFor(x => x.NewPassword)
                .Length(6,50)
                .When(x => !x.NewPassword.IsEmpty())
                .Equal(x => x.ConfirmPassword)
                .WithMessage("������ � ������������� ������ �� ���������")
                .When(x => !x.NewPassword.IsEmpty());

            var user = AuthService.CurrentUser;
            RuleFor(x => x.CurrentPassword)
                .Equal(user.Password)
                .WithMessage("������ �� �����");
            RuleFor(x => x.NewEmail).EmailAddress()
              .Must(EmailNotUse).WithMessage("������ E-mail ��� ������������")
			  .When(x => x.NewEmail != user.Email )
			  .Must(x => !StringUtils.IsSpecEmail(user.Email))
			  .WithMessage("����� @specialist.ru �������� ������")
              .When(x => x.NewEmail != user.Email );
        }

        private bool EmailNotUse(string email)
        {
            if (email.IsEmpty())
                return true;
            var user = UserService.GetByEmail(email);
            return user == null;
        }
    }
}