using System.Collections.Generic;
using FluentValidation; using Microsoft.Practices.Unity;
using Specialist.Entities.Passport;
using Specialist.Entities.Profile;
using Specialist.Entities.Profile.ViewModel;
using Specialist.Services.Interface.Passport;
using Specialist.Web.Common.Validators.Core;
using Specialist.Web.Validators;
using Microsoft.Practices.Unity;

namespace Specialist.Web.Common.Validators.Profile
{
    public class EditProfileValidator: ExValidator<EditProfileVM>
    {
        public class ContactsValidator: ExValidator<ContactsVM>
        {
            protected override void Init()
            {
                RuleFor(x => x.Socials).SetValidator(
                    UnityContainer.Resolve<UserContactValidator>());
            }
        }
        public class UserValidator: ExValidator<User>
        {
	        [Dependency]
	        public IAuthService AuthService { get; set; }
            protected override void Init()
            {
                RuleFor(x => x.FirstName).NotEmpty();
            }
        }

        protected override void Init()
        {
            RuleFor(x => x.UserAddress).SetValidator(
                UnityContainer.Resolve<RegisterVMValidator.AddressValidator>());
            RuleFor(x => x.Contacts).SetValidator(
                UnityContainer.Resolve<ContactsValidator>());
            RuleFor(x => x.User).SetValidator(
                UnityContainer.Resolve<UserValidator>());
        }
    }
}