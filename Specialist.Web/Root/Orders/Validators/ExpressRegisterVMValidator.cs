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
    public class ExpressRegisterVMValidator : ExValidator<ExpressRegisterVM>
    {
        [Dependency]
        public IUserService UserService { get; set; }

        protected override void Init()
        {
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.SecondName).NotEmpty();
        	RuleFor(x => x.Email).Must(x => x.IsEmpty() || !Regex.IsMatch(x, "[�-�]"))
				.WithMessage("E-mail �� ������ ��������� ���������");
            RuleFor(x => x.Email).EmailAddress().NotEmpty()
                .Must(EmailNotUse).WithMessage("������ E-mail ��� ������������, " +
             H.Anchor("/profile/restorepassword?email={0}", "������������ ������").Class("open-in-dialog"),
			 x => x.Email);
        	RuleFor(x => x.PersonalData).Must(x => x)
				.WithMessage(RegisterVMValidator.PersonalDataMessage);
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